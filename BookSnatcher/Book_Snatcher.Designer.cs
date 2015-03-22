namespace BookSnatcher
{
    partial class Book_Snatcher
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Book_Snatcher));
            this.SelectectedFolder = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.UpperLeftLabel = new System.Windows.Forms.Label();
            this.bottomRightLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.NextPageLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PrcNameLbl = new System.Windows.Forms.Label();
            this.ProcessName = new System.Windows.Forms.TextBox();
            this.ProcessSet = new System.Windows.Forms.Button();
            this.PagesLbl = new System.Windows.Forms.Label();
            this.PageNmbrSet = new System.Windows.Forms.Button();
            this.PageNumber = new System.Windows.Forms.TextBox();
            this.SlowMode = new System.Windows.Forms.CheckBox();
            this.RightButtonNext = new System.Windows.Forms.CheckBox();
            this.CurrentCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectectedFolder
            // 
            this.SelectectedFolder.Location = new System.Drawing.Point(10, 93);
            this.SelectectedFolder.Name = "SelectectedFolder";
            this.SelectectedFolder.ReadOnly = true;
            this.SelectectedFolder.Size = new System.Drawing.Size(352, 20);
            this.SelectectedFolder.TabIndex = 0;
            this.SelectectedFolder.Text = "Choose a folder for the screenshots to go";
            this.SelectectedFolder.TextChanged += new System.EventHandler(this.SelectectedFolder_TextChanged);
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(370, 92);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 1;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // UpperLeftLabel
            // 
            this.UpperLeftLabel.AutoSize = true;
            this.UpperLeftLabel.Location = new System.Drawing.Point(16, 3);
            this.UpperLeftLabel.Name = "UpperLeftLabel";
            this.UpperLeftLabel.Size = new System.Drawing.Size(133, 13);
            this.UpperLeftLabel.TabIndex = 2;
            this.UpperLeftLabel.Text = "Upper Left Corner: Not Set";
            // 
            // bottomRightLbl
            // 
            this.bottomRightLbl.AutoSize = true;
            this.bottomRightLbl.Location = new System.Drawing.Point(5, 16);
            this.bottomRightLbl.Name = "bottomRightLbl";
            this.bottomRightLbl.Size = new System.Drawing.Size(144, 13);
            this.bottomRightLbl.TabIndex = 4;
            this.bottomRightLbl.Text = "Bottom Right Corner: Not Set";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Set: Insert";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Set: Home";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(370, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 53);
            this.Start.TabIndex = 7;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // NextPageLbl
            // 
            this.NextPageLbl.AutoSize = true;
            this.NextPageLbl.Location = new System.Drawing.Point(16, 30);
            this.NextPageLbl.Name = "NextPageLbl";
            this.NextPageLbl.Size = new System.Drawing.Size(133, 13);
            this.NextPageLbl.TabIndex = 8;
            this.NextPageLbl.Text = "Next Page Button: Not Set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Set: Delete";
            // 
            // PrcNameLbl
            // 
            this.PrcNameLbl.AutoSize = true;
            this.PrcNameLbl.Location = new System.Drawing.Point(6, 49);
            this.PrcNameLbl.Name = "PrcNameLbl";
            this.PrcNameLbl.Size = new System.Drawing.Size(143, 13);
            this.PrcNameLbl.TabIndex = 10;
            this.PrcNameLbl.Text = "Window TItle Name: Not Set";
            // 
            // ProcessName
            // 
            this.ProcessName.Location = new System.Drawing.Point(163, 47);
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.Size = new System.Drawing.Size(126, 20);
            this.ProcessName.TabIndex = 11;
            this.ProcessName.Text = "Enter the window title";
            this.ProcessName.Enter += new System.EventHandler(this.ProcessName_Enter);
            this.ProcessName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessName_KeyPress);
            this.ProcessName.Leave += new System.EventHandler(this.ProcessName_Leave);
            // 
            // ProcessSet
            // 
            this.ProcessSet.Location = new System.Drawing.Point(290, 45);
            this.ProcessSet.Name = "ProcessSet";
            this.ProcessSet.Size = new System.Drawing.Size(32, 23);
            this.ProcessSet.TabIndex = 12;
            this.ProcessSet.Text = "Set";
            this.ProcessSet.UseVisualStyleBackColor = true;
            this.ProcessSet.Click += new System.EventHandler(this.ProcessSet_Click);
            // 
            // PagesLbl
            // 
            this.PagesLbl.AutoSize = true;
            this.PagesLbl.Location = new System.Drawing.Point(18, 69);
            this.PagesLbl.Name = "PagesLbl";
            this.PagesLbl.Size = new System.Drawing.Size(131, 13);
            this.PagesLbl.TabIndex = 13;
            this.PagesLbl.Text = "Number of Pages: Not Set";
            // 
            // PageNmbrSet
            // 
            this.PageNmbrSet.Location = new System.Drawing.Point(290, 65);
            this.PageNmbrSet.Name = "PageNmbrSet";
            this.PageNmbrSet.Size = new System.Drawing.Size(32, 23);
            this.PageNmbrSet.TabIndex = 15;
            this.PageNmbrSet.Text = "Set";
            this.PageNmbrSet.UseVisualStyleBackColor = true;
            this.PageNmbrSet.Click += new System.EventHandler(this.PageNmbrSet_Click);
            // 
            // PageNumber
            // 
            this.PageNumber.Location = new System.Drawing.Point(163, 67);
            this.PageNumber.Name = "PageNumber";
            this.PageNumber.Size = new System.Drawing.Size(126, 20);
            this.PageNumber.TabIndex = 14;
            this.PageNumber.Text = "Enter a number";
            this.PageNumber.Enter += new System.EventHandler(this.PageNumber_Enter);
            this.PageNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PageNumber_KeyPress);
            this.PageNumber.Leave += new System.EventHandler(this.PageNumber_Leave);
            // 
            // SlowMode
            // 
            this.SlowMode.AutoSize = true;
            this.SlowMode.Location = new System.Drawing.Point(361, 69);
            this.SlowMode.Name = "SlowMode";
            this.SlowMode.Size = new System.Drawing.Size(97, 17);
            this.SlowMode.TabIndex = 16;
            this.SlowMode.Text = "Slow Computer";
            this.SlowMode.UseVisualStyleBackColor = true;
            // 
            // RightButtonNext
            // 
            this.RightButtonNext.AutoSize = true;
            this.RightButtonNext.Location = new System.Drawing.Point(223, 29);
            this.RightButtonNext.Name = "RightButtonNext";
            this.RightButtonNext.Size = new System.Drawing.Size(107, 17);
            this.RightButtonNext.TabIndex = 17;
            this.RightButtonNext.Text = "Use Right Button";
            this.RightButtonNext.UseVisualStyleBackColor = true;
            this.RightButtonNext.CheckedChanged += new System.EventHandler(this.RightButtonNext_CheckedChanged);
            // 
            // CurrentCount
            // 
            this.CurrentCount.AutoSize = true;
            this.CurrentCount.Location = new System.Drawing.Point(475, 66);
            this.CurrentCount.Name = "CurrentCount";
            this.CurrentCount.Size = new System.Drawing.Size(41, 52);
            this.CurrentCount.TabIndex = 18;
            this.CurrentCount.Text = "Current\r\nCount:\r\n0\r\n\r\n";
            this.CurrentCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(465, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 26);
            this.label3.TabIndex = 19;
            this.label3.Text = "Hit Escape \r\nto cancel\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Book_Snatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(532, 120);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CurrentCount);
            this.Controls.Add(this.RightButtonNext);
            this.Controls.Add(this.SlowMode);
            this.Controls.Add(this.PageNmbrSet);
            this.Controls.Add(this.PageNumber);
            this.Controls.Add(this.PagesLbl);
            this.Controls.Add(this.ProcessSet);
            this.Controls.Add(this.ProcessName);
            this.Controls.Add(this.PrcNameLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NextPageLbl);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bottomRightLbl);
            this.Controls.Add(this.UpperLeftLabel);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.SelectectedFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Book_Snatcher";
            this.Text = "Book Snatcher";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Book_Snatcher_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SelectectedFolder;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Label UpperLeftLabel;
        private System.Windows.Forms.Label bottomRightLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label NextPageLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PrcNameLbl;
        private System.Windows.Forms.TextBox ProcessName;
        private System.Windows.Forms.Button ProcessSet;
        private System.Windows.Forms.Label PagesLbl;
        private System.Windows.Forms.Button PageNmbrSet;
        private System.Windows.Forms.TextBox PageNumber;
        private System.Windows.Forms.CheckBox SlowMode;
        private System.Windows.Forms.CheckBox RightButtonNext;
        private System.Windows.Forms.Label CurrentCount;
        private System.Windows.Forms.Label label3;
    }
}

