using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDownloader
{
    public partial class NewMissionForm : Form
    {
        private int processCount;
        public NewMissionForm()
        {
            processCount = Environment.ProcessorCount;
            InitializeComponent();
            this.ThreadCountBox.Text = processCount.ToString();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ThreadCountBox.Text == "0" || urlBox.Text == "")
            {
                return;
            }

            var MissionDataGridView = (FormManager.GetFormManager().FormMap["MainForm"] as Form1).MissionDataGridView;
            // DataGridViewRow ss = new DataGridViewRow();
            object[] tmpdata = { FileNameBox.Text, "Unknow", this.AutoStartBox.Checked ? "Pause" : "Running", "0", "0", DateTime.Now.ToString(), 20 };
            MissionDataGridView.Rows.Add(tmpdata);
            MissionDataGridView.Rows[MissionDataGridView.RowCount - 2].ReadOnly = true;
            DownloadManager.GetDowanloadManager().AddMission(urlBox.Text, int.Parse(ThreadCountBox.Text));
            this.Hide();


        }

        private void NewMissionForm_Load(object sender, EventArgs e)
        {
            urlBox.Text = "";
            ThreadCountBox.Text = processCount.ToString();
        }

        
        
    }
}
