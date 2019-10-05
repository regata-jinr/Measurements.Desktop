using Measurements.UI.Managers;

namespace Measurements.UI.Desktop.Forms
{
    partial class DetectorControlPanel
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
            foreach (var d in _session.ManagedDetectors)
                ProcessManager.CloseDetector(d.Name);

            ProcessManager.CloseMvcg();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectorControlPanel));
            this.DCPButtonPrevSrc = new System.Windows.Forms.Button();
            this.DCPButtonNextSrc = new System.Windows.Forms.Button();
            this.DCPButtonClear = new System.Windows.Forms.Button();
            this.DCPButtonSave = new System.Windows.Forms.Button();
            this.DCPButtonStartPause = new System.Windows.Forms.Button();
            this.DCPLabelNextSrcName = new System.Windows.Forms.Label();
            this.DCPLabelPresetTime = new System.Windows.Forms.Label();
            this.DCPLabelElapsedTime = new System.Windows.Forms.Label();
            this.DCPLabelTimeTitle = new System.Windows.Forms.Label();
            this.DCPLabelSplitter = new System.Windows.Forms.Label();
            this.DCPLabelPrevSrcName = new System.Windows.Forms.Label();
            this.DCPLabelCurrentSrcName = new System.Windows.Forms.Label();
            this.DCPComboBoxHeight = new System.Windows.Forms.ComboBox();
            this.DCPLabelHeight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DCPButtonPrevSrc
            // 
            this.DCPButtonPrevSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonPrevSrc.Location = new System.Drawing.Point(48, 96);
            this.DCPButtonPrevSrc.Name = "DCPButtonPrevSrc";
            this.DCPButtonPrevSrc.Size = new System.Drawing.Size(75, 23);
            this.DCPButtonPrevSrc.TabIndex = 0;
            this.DCPButtonPrevSrc.Text = "<<<";
            this.DCPButtonPrevSrc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.DCPButtonPrevSrc.UseVisualStyleBackColor = true;
            this.DCPButtonPrevSrc.Click += new System.EventHandler(this.DCPButtonPrevSrc_Click);
            // 
            // DCPButtonNextSrc
            // 
            this.DCPButtonNextSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonNextSrc.Location = new System.Drawing.Point(129, 96);
            this.DCPButtonNextSrc.Name = "DCPButtonNextSrc";
            this.DCPButtonNextSrc.Size = new System.Drawing.Size(75, 23);
            this.DCPButtonNextSrc.TabIndex = 1;
            this.DCPButtonNextSrc.Text = ">>>";
            this.DCPButtonNextSrc.UseVisualStyleBackColor = true;
            this.DCPButtonNextSrc.Click += new System.EventHandler(this.DCPButtonNextSrc_Click);
            // 
            // DCPButtonClear
            // 
            this.DCPButtonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonClear.Location = new System.Drawing.Point(75, 41);
            this.DCPButtonClear.Name = "DCPButtonClear";
            this.DCPButtonClear.Size = new System.Drawing.Size(97, 23);
            this.DCPButtonClear.TabIndex = 2;
            this.DCPButtonClear.Text = "Отчистить";
            this.DCPButtonClear.UseVisualStyleBackColor = true;
            this.DCPButtonClear.Click += new System.EventHandler(this.DCPButtonClear_Click);
            // 
            // DCPButtonSave
            // 
            this.DCPButtonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonSave.Location = new System.Drawing.Point(12, 12);
            this.DCPButtonSave.Name = "DCPButtonSave";
            this.DCPButtonSave.Size = new System.Drawing.Size(111, 23);
            this.DCPButtonSave.TabIndex = 3;
            this.DCPButtonSave.Text = "Сохранить";
            this.DCPButtonSave.UseVisualStyleBackColor = true;
            // 
            // DCPButtonStartPause
            // 
            this.DCPButtonStartPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonStartPause.Location = new System.Drawing.Point(129, 12);
            this.DCPButtonStartPause.Name = "DCPButtonStartPause";
            this.DCPButtonStartPause.Size = new System.Drawing.Size(116, 23);
            this.DCPButtonStartPause.TabIndex = 4;
            this.DCPButtonStartPause.Text = "Старт";
            this.DCPButtonStartPause.UseVisualStyleBackColor = true;
            this.DCPButtonStartPause.Click += new System.EventHandler(this.DCPButtonStartPause_Click);
            // 
            // DCPLabelNextSrcName
            // 
            this.DCPLabelNextSrcName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelNextSrcName.Location = new System.Drawing.Point(210, 96);
            this.DCPLabelNextSrcName.Name = "DCPLabelNextSrcName";
            this.DCPLabelNextSrcName.Size = new System.Drawing.Size(35, 23);
            this.DCPLabelNextSrcName.TabIndex = 5;
            this.DCPLabelNextSrcName.Text = "D5";
            this.DCPLabelNextSrcName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DCPLabelPresetTime
            // 
            this.DCPLabelPresetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelPresetTime.Location = new System.Drawing.Point(354, 30);
            this.DCPLabelPresetTime.Name = "DCPLabelPresetTime";
            this.DCPLabelPresetTime.Size = new System.Drawing.Size(83, 49);
            this.DCPLabelPresetTime.TabIndex = 7;
            this.DCPLabelPresetTime.Text = "99";
            this.DCPLabelPresetTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DCPLabelElapsedTime
            // 
            this.DCPLabelElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelElapsedTime.Location = new System.Drawing.Point(259, 30);
            this.DCPLabelElapsedTime.Name = "DCPLabelElapsedTime";
            this.DCPLabelElapsedTime.Size = new System.Drawing.Size(89, 49);
            this.DCPLabelElapsedTime.TabIndex = 8;
            this.DCPLabelElapsedTime.Text = "0";
            this.DCPLabelElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DCPLabelTimeTitle
            // 
            this.DCPLabelTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelTimeTitle.Location = new System.Drawing.Point(275, 7);
            this.DCPLabelTimeTitle.Name = "DCPLabelTimeTitle";
            this.DCPLabelTimeTitle.Size = new System.Drawing.Size(171, 28);
            this.DCPLabelTimeTitle.TabIndex = 11;
            this.DCPLabelTimeTitle.Text = "Таймер измерений";
            this.DCPLabelTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelSplitter
            // 
            this.DCPLabelSplitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelSplitter.Location = new System.Drawing.Point(336, 30);
            this.DCPLabelSplitter.Name = "DCPLabelSplitter";
            this.DCPLabelSplitter.Size = new System.Drawing.Size(24, 49);
            this.DCPLabelSplitter.TabIndex = 12;
            this.DCPLabelSplitter.Text = "|";
            // 
            // DCPLabelPrevSrcName
            // 
            this.DCPLabelPrevSrcName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelPrevSrcName.Location = new System.Drawing.Point(12, 96);
            this.DCPLabelPrevSrcName.Name = "DCPLabelPrevSrcName";
            this.DCPLabelPrevSrcName.Size = new System.Drawing.Size(35, 23);
            this.DCPLabelPrevSrcName.TabIndex = 13;
            this.DCPLabelPrevSrcName.Text = "D5";
            this.DCPLabelPrevSrcName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DCPLabelCurrentSrcName
            // 
            this.DCPLabelCurrentSrcName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelCurrentSrcName.Location = new System.Drawing.Point(107, 70);
            this.DCPLabelCurrentSrcName.Name = "DCPLabelCurrentSrcName";
            this.DCPLabelCurrentSrcName.Size = new System.Drawing.Size(35, 23);
            this.DCPLabelCurrentSrcName.TabIndex = 14;
            this.DCPLabelCurrentSrcName.Text = "D5";
            this.DCPLabelCurrentSrcName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPComboBoxHeight
            // 
            this.DCPComboBoxHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPComboBoxHeight.FormattingEnabled = true;
            this.DCPComboBoxHeight.Items.AddRange(new object[] {
            "2.5",
            "5",
            "10",
            "20"});
            this.DCPComboBoxHeight.Location = new System.Drawing.Point(396, 99);
            this.DCPComboBoxHeight.Name = "DCPComboBoxHeight";
            this.DCPComboBoxHeight.Size = new System.Drawing.Size(41, 24);
            this.DCPComboBoxHeight.TabIndex = 15;
            this.DCPComboBoxHeight.Text = "20";
            this.DCPComboBoxHeight.SelectedIndexChanged += new System.EventHandler(this.HeightChangedHandler);
            // 
            // DCPLabelHeight
            // 
            this.DCPLabelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelHeight.Location = new System.Drawing.Point(326, 100);
            this.DCPLabelHeight.Name = "DCPLabelHeight";
            this.DCPLabelHeight.Size = new System.Drawing.Size(64, 19);
            this.DCPLabelHeight.TabIndex = 16;
            this.DCPLabelHeight.Text = "Высота";
            this.DCPLabelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DetectorControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 131);
            this.Controls.Add(this.DCPLabelHeight);
            this.Controls.Add(this.DCPComboBoxHeight);
            this.Controls.Add(this.DCPLabelCurrentSrcName);
            this.Controls.Add(this.DCPLabelPrevSrcName);
            this.Controls.Add(this.DCPLabelSplitter);
            this.Controls.Add(this.DCPLabelTimeTitle);
            this.Controls.Add(this.DCPLabelElapsedTime);
            this.Controls.Add(this.DCPLabelPresetTime);
            this.Controls.Add(this.DCPLabelNextSrcName);
            this.Controls.Add(this.DCPButtonStartPause);
            this.Controls.Add(this.DCPButtonSave);
            this.Controls.Add(this.DCPButtonClear);
            this.Controls.Add(this.DCPButtonNextSrc);
            this.Controls.Add(this.DCPButtonPrevSrc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetectorControlPanel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Панель управления детектором";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DCPButtonPrevSrc;
        private System.Windows.Forms.Button DCPButtonNextSrc;
        private System.Windows.Forms.Button DCPButtonClear;
        private System.Windows.Forms.Button DCPButtonSave;
        private System.Windows.Forms.Button DCPButtonStartPause;
        private System.Windows.Forms.Label DCPLabelNextSrcName;
        private System.Windows.Forms.Label DCPLabelPresetTime;
        private System.Windows.Forms.Label DCPLabelElapsedTime;
        private System.Windows.Forms.Label DCPLabelTimeTitle;
        private System.Windows.Forms.Label DCPLabelSplitter;
        private System.Windows.Forms.Label DCPLabelPrevSrcName;
        private System.Windows.Forms.Label DCPLabelCurrentSrcName;
        private System.Windows.Forms.ComboBox DCPComboBoxHeight;
        private System.Windows.Forms.Label DCPLabelHeight;
    }
}