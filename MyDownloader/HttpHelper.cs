using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MyDownloader
{
    public class FileDownloader
    {
        public FileDownloader(string file_name, long file_size)
        {
            this.mFileName = file_name;
            this.mFileSize = file_size;
        }
        public string mFileName;
        public long mFileSize;
    }

    public class ThreadParam
    {
        public float mSpeed;
        public Int32 mId;
        public string mFileName;
        public long mBeginBlock;
        public long mEndBlock;
        public ManualResetEvent mStartEvent;
        public ManualResetEvent mDoneEvent;
        internal string url;
        public Int32 mDownloadSize;
    }


    class HttpHelper
    {
        public static string GetHttpResponse(string url, int tiem_out)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = tiem_out;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public static long GetHttpLength(string url, int time_out)
        {
            long length = 0L;
            try
            {
                var req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                req.Method = "HEAD";
                req.ContentType = "text/html;charset=UTF-8";
                req.UserAgent = null;
                req.Timeout = time_out;
                var tmpres = req.GetResponse();
                var res = (HttpWebResponse)tmpres;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    length = res.ContentLength;

                }
                res.Close();
                return length;
            }
            catch (WebException wex)
            {
                return 0L;
            }
        }

    }


    struct DownloadMission
    {
        public ManualResetEvent[] StartEvents;
        public ManualResetEvent[] DoneEvents;
        public ThreadParam[] Params;
        public Thread[] Threads;
        public int DataIndex;
        public float DownloadPercent;
        public long FileSize { get; internal set; }
    }
    class DownloadManager
    {
        private static DownloadManager m_instance;
        private List<FileDownloader> DownloadFiles;
        //public ManualResetEvent[] StartEvents;
        //public ManualResetEvent[] DoneEvents;
        //public ThreadParam[] Params;
        //public Thread[] Threads;        
        public List<DownloadMission> DownloadMissions;
        private List<Thread> DownloadMissionWaiter;
        public static DownloadManager GetDowanloadManager()
        {
            if (m_instance == null)
            {
                m_instance = new DownloadManager();
                m_instance.DownloadFiles = new List<FileDownloader>();
                m_instance.DownloadMissions = new List<DownloadMission>();
                m_instance.DownloadMissionWaiter = new List<Thread>();
            }
            return m_instance;
        }

        public void CutFile(ThreadParam[] param, long FileSize)
        {
            if (FileSize < 1024 * 1024 * 10)
            {
                param[0].mBeginBlock = 0;
                param[0].mEndBlock = FileSize - 1;
                for (Int32 i = 1; i < param.Length; i++)
                {
                    param[i].mBeginBlock = -1;
                }
            }
            else
            {
                long Block = FileSize / (long)param.Length;
                for (UInt32 i = 0; i < param.Length; i++)
                {
                    param[i].mBeginBlock = (i * Block);
                    param[i].mEndBlock = (param[i].mBeginBlock + Block - 1);
                }
                param[param.Length - 1].mEndBlock += FileSize % (UInt32)param.Length;
            }

        }

        public static void MergeFile(ThreadParam[] param)
        {
            //只有一个临时文件，即是完整文件,改掉文件名即可  ;
            if (param.Length == 1)
            {
                if (File.Exists(param[0].mFileName))
                {
                    File.Delete(param[0].mFileName);
                }
                File.Move(param[0].mFileName + param[0].mId.ToString(), param[0].mFileName);
                return;
            }

            FileStream OutStream = new FileStream(param[0].mFileName, FileMode.Create);
            FileStream InStream;
            Byte[] Buffer = new Byte[1024];
            Int32 ReadBytes = 0;
            for (Int32 i = 0; i < param.Length; ++i)
            {
                //读取临时文件;  
                InStream = new FileStream(param[i].mFileName + param[i].mId.ToString(), FileMode.Open);
                while (true)
                {
                    ReadBytes = InStream.Read(Buffer, 0, Buffer.Length);
                    if (ReadBytes <= 0)
                    {
                        break;
                    }
                    OutStream.Write(Buffer, 0, ReadBytes);
                }
                InStream.Close();
                //删除临时文件;
                File.Delete(param[i].mFileName + param[i].mId.ToString());
            }
            OutStream.Close();
        }


        public static void DownloadWaiterFunc(object o)
        {
            var mission = (DownloadMission)o;
            while(!WaitHandle.WaitAll(mission.DoneEvents,16))
            {
                float DownloadSpeed = 0;
                float DownloadSize = 0;
                foreach(var i in mission.Params)
                {
                    DownloadSize += i.mDownloadSize;
                    WaitHandle[] tmp = { i.mDoneEvent };
                    if(!WaitHandle.WaitAll(tmp, 1))
                        DownloadSpeed += i.mSpeed;
                    else
                    {
                        i.mSpeed = 0;
                    }


                }
                
                string DownloadSpeedStr = UnitTrans.GetHandSomeNumFromByteNum((long)DownloadSpeed);
                float percent = DownloadSize / mission.FileSize * 100.0f;
                mission.DownloadPercent = percent;
               // (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[mission.DataIndex].Cells[6].ValueType = Type.GetType("float");
                (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[mission.DataIndex].Cells[6].Value = (int)(mission.DownloadPercent*100);
                (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[mission.DataIndex].Cells[4].Value = DownloadSpeedStr + @"/s";
               Console.WriteLine(percent);
            }
            (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[mission.DataIndex].Cells[2].Value = "Migrating";
            MergeFile(mission.Params);
            (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[mission.DataIndex].Cells[2].Value = "Complate";
        }

        public static void AsyncDownloadFile(object o)
        {
            var Param = (ThreadParam)o;
            HttpWebRequest Request;
            HttpWebResponse Response;
            Stream HttpStream;
            FileStream OutStream;
            Int32 ReadBytes = 0;
            Byte[] Buffer = new Byte[1024];
            int statictime = System.Environment.TickCount;
            int CountTime = 0;
            int CountingBytenum = 0;
            while (true)
            {
                Param.mStartEvent.WaitOne();
                if (-1 == Param.mBeginBlock)
                {
                    Param.mStartEvent.Reset();
                    Param.mDoneEvent.Set();
                    continue;
                }

                try
                {
                    Request = WebRequest.Create(Param.url/* + "//" + Param.mFileName*/) as HttpWebRequest;
                    Request.AddRange(Param.mBeginBlock, Param.mEndBlock);

                    Response = Request.GetResponse() as HttpWebResponse;
                    //Response.StatusCode; 这个值为200表示一切OK  
                    HttpStream = Response.GetResponseStream();
                    OutStream = new FileStream(Param.mFileName + Param.mId.ToString(), FileMode.Create);
                    while (true)
                    {
                        ReadBytes = HttpStream.Read(Buffer, 0, Buffer.Length);
                        if (ReadBytes <= 0)
                        {
                            break;
                        }
                        Param.mDownloadSize += ReadBytes;
                        int atime = Environment.TickCount;
                        int deltatime = atime - statictime;
                        CountTime += deltatime;
                        if (CountTime >= 1000)
                        {
                            Param.mSpeed = CountingBytenum;
                            CountTime = 0;
                            CountingBytenum = 0;
                        }
                        else
                        {
                            CountingBytenum += ReadBytes;
                        }
                        statictime = atime;

                        OutStream.Write(Buffer, 0, ReadBytes);
                    }
                    OutStream.Close();
                    HttpStream.Close();
                    Response.Close();
                    Request.Abort();
                    Param.mStartEvent.Reset();
                    Param.mDoneEvent.Set();
                }
                catch (WebException e)
                {

                }
            }

        }

        public void AddMission(string url, int ThreadCount)
        {
            //UInt32.MaxValue
            long fileSize = HttpHelper.GetHttpLength(url, 6000);
            var MissionDataGridView = (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView;
            var allCells = MissionDataGridView.Rows[MissionDataGridView.RowCount - 2].Cells;
            allCells[1].Value = UnitTrans.GetHandSomeNumFromByteNum(fileSize);

            var tmpFileDownloader = new FileDownloader(url, fileSize);
            DownloadFiles.Add(tmpFileDownloader);
            var tmpMission = new DownloadMission();
            tmpMission.DataIndex = MissionDataGridView.RowCount - 2;
            tmpMission.FileSize = fileSize;
            tmpMission.StartEvents = new ManualResetEvent[ThreadCount];
            tmpMission.DoneEvents = new ManualResetEvent[ThreadCount];
            tmpMission.Params = new ThreadParam[ThreadCount];
            tmpMission.Threads = new Thread[ThreadCount];
            for (Int32 i = 0; i < ThreadCount; i++)
            {
                tmpMission.StartEvents[i] = new ManualResetEvent(false);
                tmpMission.DoneEvents[i] = new ManualResetEvent(false);
                tmpMission.Params[i] = new ThreadParam();
                tmpMission.Params[i].mId = i;
                tmpMission.Params[i].url = url;
                tmpMission.Params[i].mStartEvent = tmpMission.StartEvents[i];
                tmpMission.Params[i].mDoneEvent = tmpMission.DoneEvents[i];
                tmpMission.Threads[i] = new Thread(new ParameterizedThreadStart(AsyncDownloadFile));
                tmpMission.Threads[i].Start(tmpMission.Params[i]);
            }
            CutFile(tmpMission.Params, fileSize);

            for (Int32 j = 0; j < ThreadCount; j++)
            {
                tmpMission.Params[j].mFileName = Guid.NewGuid().ToString().ToLower();
                tmpMission.Params[j].mDoneEvent.Reset();
                tmpMission.Params[j].mStartEvent.Set();
            }
            Thread tmpWaiter = new Thread(new ParameterizedThreadStart(DownloadWaiterFunc));
            tmpWaiter.Start(tmpMission);
           
            DownloadMissions.Add(tmpMission);
            DownloadMissionWaiter.Add(tmpWaiter);
        }
    }
}
