namespace MyDownloader
{
    partial class MissionDetials
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Hide();
            DetialsUpdateThread.Abort();
            DetialsUpdateThread = null;
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MissionDetialsDataView = new System.Windows.Forms.DataGridView();
            this.Thread = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new DataGridViewProgressBar.DataGridViewProgressBarColumn();
            this.StartLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownloadNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MissionDetialsDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // MissionDetialsDataView
            // 
            this.MissionDetialsDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MissionDetialsDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Thread,
            this.Speed,
            this.Progress,
            this.StartLocation,
            this.EndLocation,
            this.DownloadNum});
            this.MissionDetialsDataView.Location = new System.Drawing.Point(13, 13);
            this.MissionDetialsDataView.Name = "MissionDetialsDataView";
            this.MissionDetialsDataView.RowTemplate.Height = 23;
            this.MissionDetialsDataView.Size = new System.Drawing.Size(641, 135);
            this.MissionDetialsDataView.TabIndex = 0;
            // 
            // Thread
            // 
            this.Thread.HeaderText = "Thread";
            this.Thread.Name = "Thread";
            this.Thread.ReadOnly = true;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Speed";
            this.Speed.Name = "Speed";
            this.Speed.ReadOnly = true;
            // 
            // Progress
            // 
            this.Progress.HeaderText = "Progress";
            this.Progress.Maximum = ((long)(100));
            this.Progress.Minimum = ((long)(0));
            this.Progress.Name = "Progress";
            this.Progress.ReadOnly = true;
            // 
            // StartLocation
            // 
            this.StartLocation.HeaderText = "StartLocation";
            this.StartLocation.Name = "StartLocation";
            this.StartLocation.ReadOnly = true;
            // 
            // EndLocation
            // 
            this.EndLocation.HeaderText = "EndLocation";
            this.EndLocation.Name = "EndLocation";
            this.EndLocation.ReadOnly = true;
            // 
            // DownloadNum
            // 
            this.DownloadNum.HeaderText = "DownloadNum";
            this.DownloadNum.Name = "DownloadNum";
            this.DownloadNum.ReadOnly = true;
            // 
            // MissionDetials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 160);
            this.Controls.Add(this.MissionDetialsDataView);
            this.Name = "MissionDetials";
            this.Text = "MissionDetials";
            ((System.ComponentModel.ISupportInitialize)(this.MissionDetialsDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Thread;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private DataGridViewProgressBar.DataGridViewProgressBarColumn Progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownloadNum;
        public System.Windows.Forms.DataGridView MissionDetialsDataView;
    }
}