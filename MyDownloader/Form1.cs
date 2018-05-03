//using DataGridViewProgressBar;
using DataGridViewProgressBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDownloader
{

    class MissionDetailsDataObj : object
    {
        public ThreadParam[] Params;
        public MissionDetials _MissionDetials;
    }


    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            //this.MissionDataGridView.CellDoubleClick

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridViewProgressBarColumn ProgressBarColumn = new DataGridViewProgressBarColumn();
            this.MissionDataGridView.Columns.Add(ProgressBarColumn);
            ProgressBarColumn.DisplayIndex = 3;
            ProgressBarColumn.CellTemplate = new DataGridViewProgressBarCell();
            ProgressBarColumn.Name = "ProgressBar";
            //valueDataGridViewProgressBarColumn.ReadOnly = true;
            ProgressBarColumn.DataPropertyName = "value";
            ProgressBarColumn.HeaderText = "Progress";
            ProgressBarColumn.Maximum = 10000;
            ProgressBarColumn.Minimum = 0;
            //ProgressBarColumn.ValueType = Type.GetType("float");
            //this.MissionDataGridView.Rows[0].ReadOnly = true;
            //this.MissionDataGridView.Rows.Add();
            //this.MissionDataGridView.Rows[0].ReadOnly = true;
            //this.MissionDataGridView.Rows[0].Cells[6].Value = 20;
            this.MissionDataGridView.CellContentDoubleClick += MissionDataGridView_CellContentDoubleClick;

        }

        private void MissionDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var downloaderManager = DownloadManager.GetDowanloadManager();
            MissionDetials tmpMissionDetials = null;
            if (FormManager.GetFormManager().FormMap.ContainsKey("DetailsForm"))
            {
                tmpMissionDetials = FormManager.GetFormManager().FormMap["DetailsForm"] as MissionDetials;
            }
            else
            {
                tmpMissionDetials = new MissionDetials();
            }

            foreach (var i in downloaderManager.DownloadMissions)
            {
                if (i.DataIndex == e.RowIndex)
                {
                    int k = 0;
                    foreach (var j in i.Params)
                    {
                        object[] tmpData = { "Thread " + k.ToString(), j.mSpeed.ToString(), k, j.mBeginBlock.ToString(), j.mEndBlock.ToString(), j.mDownloadSize.ToString() };

                        int index = tmpMissionDetials.MissionDetialsDataView.Rows.Add(tmpData);
                        var rowData = tmpMissionDetials.MissionDetialsDataView.Rows[index];


                        //rowData.Cells[0].Value = ;
                        // rowData.Cells[1].Value = 
                        k++;
                    }
                    MissionDetailsDataObj tmpdataObj = new MissionDetailsDataObj();
                    tmpdataObj.Params = i.Params;
                    tmpdataObj._MissionDetials = tmpMissionDetials;
                    Thread DetialsUpdateThread = new Thread(DetialsUpdateThreadFunc);
                    DetialsUpdateThread.Start(tmpdataObj);
                    tmpMissionDetials.DetialsUpdateThread = DetialsUpdateThread;
                    if (!FormManager.GetFormManager().FormMap.ContainsKey("DetailsForm"))
                        FormManager.GetFormManager().FormMap.Add("DetailsForm", tmpMissionDetials);

                }
            }
            tmpMissionDetials.Show();


        }

        static void DetialsUpdateThreadFunc(object o)
        {
            while (true)
            {
                var Obj = (MissionDetailsDataObj)o;
                if (!Obj._MissionDetials.Visible)
                    continue;

                int i = 0;
                foreach (var j in Obj.Params)
                {
                    Obj._MissionDetials.MissionDetialsDataView.Rows[i].Cells[1].Value = UnitTrans.GetHandSomeNumFromByteNum((long)j.mSpeed) + @"/s";
                    Obj._MissionDetials.MissionDetialsDataView.Rows[i].Cells[5].Value = UnitTrans.GetHandSomeNumFromByteNum((long)j.mDownloadSize);
                    i++;
                }
                //Obj._MissionDetials.Refresh();
            }
        }
    }
}
