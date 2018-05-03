using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDownloader
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormManager.GetFormManager();
            Form1 mainForm = new Form1();
            NewMissionForm newMissionForm = new NewMissionForm();
            newMissionForm.Visible = false;
            FormManager.GetFormManager().FormMap.Add("MainForm", mainForm);
            FormManager.GetFormManager().FormMap.Add("NewMissionForm", newMissionForm);
            Timer timer = new Timer();
            timer.Interval = 16;
            timer.Tick += new EventHandler(Tick);
            timer.Start();
            Application.Run(mainForm);

        }

        static void Tick(object o, EventArgs a)
        {
            (FormManager.GetFormManager().FormMap["MainForm"] as Form1).Refresh();
            if (FormManager.GetFormManager().FormMap.ContainsKey("DetailsForm"))
                if (!FormManager.GetFormManager().FormMap["DetailsForm"].IsDisposed)
                {
                    FormManager.GetFormManager().FormMap["DetailsForm"].Refresh();
                }
            //foreach (var i in DownloadManager.GetDowanloadManager().DownloadMissions)
            //{
            //   // (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView.Rows[i.DataIndex].Cells[6].Value = (int)i.DownloadPercent;
            //   // (FormManager.GetFormManager().FormMap["MainForm"] as Form1).Refresh();
            //}
        }
    }
}
