namespace MyDownloader
{
    partial class NewMissionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ThreadCountBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.Lable2 = new System.Windows.Forms.Label();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.AutoStartBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "url:";
            // 
            // urlBox
            // 
            this.urlBox.Location = new System.Drawing.Point(109, 6);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(497, 21);
            this.urlBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "thread count:";
            // 
            // ThreadCountBox
            // 
            this.ThreadCountBox.Location = new System.Drawing.Point(109, 31);
            this.ThreadCountBox.Name = "ThreadCountBox";
            this.ThreadCountBox.Size = new System.Drawing.Size(31, 21);
            this.ThreadCountBox.TabIndex = 3;
            this.ThreadCountBox.Text = "1";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(531, 83);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Lable2
            // 
            this.Lable2.AutoSize = true;
            this.Lable2.Location = new System.Drawing.Point(14, 59);
            this.Lable2.Name = "Lable2";
            this.Lable2.Size = new System.Drawing.Size(65, 12);
            this.Lable2.TabIndex = 5;
            this.Lable2.Text = "file name:";
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(109, 59);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(167, 21);
            this.FileNameBox.TabIndex = 6;
            // 
            // AutoStartBox
            // 
            this.AutoStartBox.AutoSize = true;
            this.AutoStartBox.Location = new System.Drawing.Point(12, 83);
            this.AutoStartBox.Name = "AutoStartBox";
            this.AutoStartBox.Size = new System.Drawing.Size(78, 16);
            this.AutoStartBox.TabIndex = 7;
            this.AutoStartBox.Text = "AutoStart";
            this.AutoStartBox.UseVisualStyleBackColor = true;
            // 
            // NewMissionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 118);
            this.Controls.Add(this.AutoStartBox);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.Lable2);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ThreadCountBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlBox);
            this.Controls.Add(this.label1);
            this.Name = "NewMissionForm";
            this.Text = "NewMissionForm";
            this.Load += new System.EventHandler(this.NewMissionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ThreadCountBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label Lable2;
        private System.Windows.Forms.TextBox FileNameBox;
        public System.Windows.Forms.CheckBox AutoStartBox;
    }
}