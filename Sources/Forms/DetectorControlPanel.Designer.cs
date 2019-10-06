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
            this.DCPLabelPrevSrcName = new System.Windows.Forms.Label();
            this.DCPLabelCurrentSrcName = new System.Windows.Forms.Label();
            this.DCPComboBoxHeight = new System.Windows.Forms.ComboBox();
            this.DCPLabelHeight = new System.Windows.Forms.Label();
            this.DCPLabelCurrentSumpleOnCurrentSrc = new System.Windows.Forms.Label();
            this.DCPLabelCurrentSumpleOnNextSrc = new System.Windows.Forms.Label();
            this.DCPLabelCurrentSumpleOnPrevSrc = new System.Windows.Forms.Label();
            this.DCPButtonStop = new System.Windows.Forms.Button();
            this.DCPNumericUpDownElapsedHours = new System.Windows.Forms.NumericUpDown();
            this.DCPNumericUpDownElapsedMinutes = new System.Windows.Forms.NumericUpDown();
            this.DCPNumericUpDownElapsedSeconds = new System.Windows.Forms.NumericUpDown();
            this.DCPNumericUpDownPresetSeconds = new System.Windows.Forms.NumericUpDown();
            this.DCPNumericUpDownPresetMinutes = new System.Windows.Forms.NumericUpDown();
            this.DCPNumericUpDownPresetHours = new System.Windows.Forms.NumericUpDown();
            this.DCPLabelHours = new System.Windows.Forms.Label();
            this.DCPLabelMinutes = new System.Windows.Forms.Label();
            this.DCPLabelSeconds = new System.Windows.Forms.Label();
            this.saveFileDialogSaveCurrentSpectra = new System.Windows.Forms.SaveFileDialog();
            this.DCPLabelPresetTitle = new System.Windows.Forms.Label();
            this.DCPTimeLeftTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetHours)).BeginInit();
            this.SuspendLayout();
            // 
            // DCPButtonPrevSrc
            // 
            this.DCPButtonPrevSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonPrevSrc.Location = new System.Drawing.Point(80, 128);
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
            this.DCPButtonNextSrc.Location = new System.Drawing.Point(161, 128);
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
            this.DCPButtonClear.Location = new System.Drawing.Point(12, 41);
            this.DCPButtonClear.Name = "DCPButtonClear";
            this.DCPButtonClear.Size = new System.Drawing.Size(111, 23);
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
            this.DCPButtonSave.Click += new System.EventHandler(this.DCPButtonSave_Click);
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
            this.DCPLabelNextSrcName.Location = new System.Drawing.Point(242, 128);
            this.DCPLabelNextSrcName.Name = "DCPLabelNextSrcName";
            this.DCPLabelNextSrcName.Size = new System.Drawing.Size(35, 23);
            this.DCPLabelNextSrcName.TabIndex = 5;
            this.DCPLabelNextSrcName.Text = "D5";
            this.DCPLabelNextSrcName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DCPLabelPrevSrcName
            // 
            this.DCPLabelPrevSrcName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelPrevSrcName.Location = new System.Drawing.Point(39, 129);
            this.DCPLabelPrevSrcName.Name = "DCPLabelPrevSrcName";
            this.DCPLabelPrevSrcName.Size = new System.Drawing.Size(35, 23);
            this.DCPLabelPrevSrcName.TabIndex = 13;
            this.DCPLabelPrevSrcName.Text = "D5";
            this.DCPLabelPrevSrcName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DCPLabelCurrentSrcName
            // 
            this.DCPLabelCurrentSrcName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelCurrentSrcName.Location = new System.Drawing.Point(139, 79);
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
            this.DCPComboBoxHeight.Location = new System.Drawing.Point(454, 149);
            this.DCPComboBoxHeight.Name = "DCPComboBoxHeight";
            this.DCPComboBoxHeight.Size = new System.Drawing.Size(41, 24);
            this.DCPComboBoxHeight.TabIndex = 15;
            this.DCPComboBoxHeight.Text = "20";
            this.DCPComboBoxHeight.SelectedIndexChanged += new System.EventHandler(this.HeightChangedHandler);
            // 
            // DCPLabelHeight
            // 
            this.DCPLabelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelHeight.Location = new System.Drawing.Point(384, 149);
            this.DCPLabelHeight.Name = "DCPLabelHeight";
            this.DCPLabelHeight.Size = new System.Drawing.Size(64, 24);
            this.DCPLabelHeight.TabIndex = 16;
            this.DCPLabelHeight.Text = "Высота";
            this.DCPLabelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelCurrentSumpleOnCurrentSrc
            // 
            this.DCPLabelCurrentSumpleOnCurrentSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelCurrentSumpleOnCurrentSrc.Location = new System.Drawing.Point(86, 102);
            this.DCPLabelCurrentSumpleOnCurrentSrc.Name = "DCPLabelCurrentSumpleOnCurrentSrc";
            this.DCPLabelCurrentSumpleOnCurrentSrc.Size = new System.Drawing.Size(141, 23);
            this.DCPLabelCurrentSumpleOnCurrentSrc.TabIndex = 17;
            this.DCPLabelCurrentSumpleOnCurrentSrc.Text = "RU-01-19-26-j-01";
            this.DCPLabelCurrentSumpleOnCurrentSrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelCurrentSumpleOnNextSrc
            // 
            this.DCPLabelCurrentSumpleOnNextSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelCurrentSumpleOnNextSrc.Location = new System.Drawing.Point(193, 154);
            this.DCPLabelCurrentSumpleOnNextSrc.Name = "DCPLabelCurrentSumpleOnNextSrc";
            this.DCPLabelCurrentSumpleOnNextSrc.Size = new System.Drawing.Size(131, 23);
            this.DCPLabelCurrentSumpleOnNextSrc.TabIndex = 18;
            this.DCPLabelCurrentSumpleOnNextSrc.Text = "RU-01-19-26-j-01";
            this.DCPLabelCurrentSumpleOnNextSrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelCurrentSumpleOnPrevSrc
            // 
            this.DCPLabelCurrentSumpleOnPrevSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelCurrentSumpleOnPrevSrc.Location = new System.Drawing.Point(-1, 154);
            this.DCPLabelCurrentSumpleOnPrevSrc.Name = "DCPLabelCurrentSumpleOnPrevSrc";
            this.DCPLabelCurrentSumpleOnPrevSrc.Size = new System.Drawing.Size(136, 23);
            this.DCPLabelCurrentSumpleOnPrevSrc.TabIndex = 19;
            this.DCPLabelCurrentSumpleOnPrevSrc.Text = "RU-01-19-26-j-01";
            this.DCPLabelCurrentSumpleOnPrevSrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPButtonStop
            // 
            this.DCPButtonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPButtonStop.Location = new System.Drawing.Point(129, 41);
            this.DCPButtonStop.Name = "DCPButtonStop";
            this.DCPButtonStop.Size = new System.Drawing.Size(116, 23);
            this.DCPButtonStop.TabIndex = 20;
            this.DCPButtonStop.Text = "Стоп";
            this.DCPButtonStop.UseVisualStyleBackColor = true;
            this.DCPButtonStop.Click += new System.EventHandler(this.DCPButtonStop_Click);
            // 
            // DCPNumericUpDownElapsedHours
            // 
            this.DCPNumericUpDownElapsedHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownElapsedHours.Location = new System.Drawing.Point(315, 102);
            this.DCPNumericUpDownElapsedHours.Name = "DCPNumericUpDownElapsedHours";
            this.DCPNumericUpDownElapsedHours.ReadOnly = true;
            this.DCPNumericUpDownElapsedHours.Size = new System.Drawing.Size(39, 44);
            this.DCPNumericUpDownElapsedHours.TabIndex = 24;
            // 
            // DCPNumericUpDownElapsedMinutes
            // 
            this.DCPNumericUpDownElapsedMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownElapsedMinutes.Location = new System.Drawing.Point(362, 102);
            this.DCPNumericUpDownElapsedMinutes.Name = "DCPNumericUpDownElapsedMinutes";
            this.DCPNumericUpDownElapsedMinutes.ReadOnly = true;
            this.DCPNumericUpDownElapsedMinutes.Size = new System.Drawing.Size(61, 44);
            this.DCPNumericUpDownElapsedMinutes.TabIndex = 25;
            // 
            // DCPNumericUpDownElapsedSeconds
            // 
            this.DCPNumericUpDownElapsedSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownElapsedSeconds.Location = new System.Drawing.Point(429, 102);
            this.DCPNumericUpDownElapsedSeconds.Name = "DCPNumericUpDownElapsedSeconds";
            this.DCPNumericUpDownElapsedSeconds.ReadOnly = true;
            this.DCPNumericUpDownElapsedSeconds.Size = new System.Drawing.Size(55, 44);
            this.DCPNumericUpDownElapsedSeconds.TabIndex = 26;
            // 
            // DCPNumericUpDownPresetSeconds
            // 
            this.DCPNumericUpDownPresetSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownPresetSeconds.Location = new System.Drawing.Point(429, 41);
            this.DCPNumericUpDownPresetSeconds.Name = "DCPNumericUpDownPresetSeconds";
            this.DCPNumericUpDownPresetSeconds.Size = new System.Drawing.Size(55, 44);
            this.DCPNumericUpDownPresetSeconds.TabIndex = 29;
            // 
            // DCPNumericUpDownPresetMinutes
            // 
            this.DCPNumericUpDownPresetMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownPresetMinutes.Location = new System.Drawing.Point(361, 41);
            this.DCPNumericUpDownPresetMinutes.Name = "DCPNumericUpDownPresetMinutes";
            this.DCPNumericUpDownPresetMinutes.Size = new System.Drawing.Size(62, 44);
            this.DCPNumericUpDownPresetMinutes.TabIndex = 28;
            // 
            // DCPNumericUpDownPresetHours
            // 
            this.DCPNumericUpDownPresetHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPNumericUpDownPresetHours.Location = new System.Drawing.Point(315, 41);
            this.DCPNumericUpDownPresetHours.Name = "DCPNumericUpDownPresetHours";
            this.DCPNumericUpDownPresetHours.Size = new System.Drawing.Size(39, 44);
            this.DCPNumericUpDownPresetHours.TabIndex = 27;
            // 
            // DCPLabelHours
            // 
            this.DCPLabelHours.Location = new System.Drawing.Point(315, 7);
            this.DCPLabelHours.Name = "DCPLabelHours";
            this.DCPLabelHours.Size = new System.Drawing.Size(38, 16);
            this.DCPLabelHours.TabIndex = 30;
            this.DCPLabelHours.Text = "Часы";
            this.DCPLabelHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelMinutes
            // 
            this.DCPLabelMinutes.Location = new System.Drawing.Point(359, 7);
            this.DCPLabelMinutes.Name = "DCPLabelMinutes";
            this.DCPLabelMinutes.Size = new System.Drawing.Size(64, 16);
            this.DCPLabelMinutes.TabIndex = 31;
            this.DCPLabelMinutes.Text = "Минуты";
            this.DCPLabelMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPLabelSeconds
            // 
            this.DCPLabelSeconds.Location = new System.Drawing.Point(426, 7);
            this.DCPLabelSeconds.Name = "DCPLabelSeconds";
            this.DCPLabelSeconds.Size = new System.Drawing.Size(64, 16);
            this.DCPLabelSeconds.TabIndex = 32;
            this.DCPLabelSeconds.Text = "Секунды";
            this.DCPLabelSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveFileDialogSaveCurrentSpectra
            // 
            this.saveFileDialogSaveCurrentSpectra.DefaultExt = "cnf";
            this.saveFileDialogSaveCurrentSpectra.Filter = "Spectra Files(*.cnf)|*.cnf|All Files(*.*)|*.*";
            this.saveFileDialogSaveCurrentSpectra.InitialDirectory = "C:\\GENIE2K\\CAMFILES";
            this.saveFileDialogSaveCurrentSpectra.RestoreDirectory = true;
            // 
            // DCPLabelPresetTitle
            // 
            this.DCPLabelPresetTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPLabelPresetTitle.Location = new System.Drawing.Point(338, 25);
            this.DCPLabelPresetTitle.Name = "DCPLabelPresetTitle";
            this.DCPLabelPresetTitle.Size = new System.Drawing.Size(137, 13);
            this.DCPLabelPresetTitle.TabIndex = 33;
            this.DCPLabelPresetTitle.Text = "Установлено";
            this.DCPLabelPresetTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCPTimeLeftTitle
            // 
            this.DCPTimeLeftTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DCPTimeLeftTitle.Location = new System.Drawing.Point(338, 86);
            this.DCPTimeLeftTitle.Name = "DCPTimeLeftTitle";
            this.DCPTimeLeftTitle.Size = new System.Drawing.Size(137, 13);
            this.DCPTimeLeftTitle.TabIndex = 34;
            this.DCPTimeLeftTitle.Text = "Осталось";
            this.DCPTimeLeftTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DetectorControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 185);
            this.Controls.Add(this.DCPTimeLeftTitle);
            this.Controls.Add(this.DCPLabelPresetTitle);
            this.Controls.Add(this.DCPLabelSeconds);
            this.Controls.Add(this.DCPLabelMinutes);
            this.Controls.Add(this.DCPLabelHours);
            this.Controls.Add(this.DCPNumericUpDownPresetSeconds);
            this.Controls.Add(this.DCPNumericUpDownPresetMinutes);
            this.Controls.Add(this.DCPNumericUpDownPresetHours);
            this.Controls.Add(this.DCPNumericUpDownElapsedSeconds);
            this.Controls.Add(this.DCPNumericUpDownElapsedMinutes);
            this.Controls.Add(this.DCPNumericUpDownElapsedHours);
            this.Controls.Add(this.DCPButtonStop);
            this.Controls.Add(this.DCPLabelCurrentSumpleOnPrevSrc);
            this.Controls.Add(this.DCPLabelCurrentSumpleOnNextSrc);
            this.Controls.Add(this.DCPLabelCurrentSumpleOnCurrentSrc);
            this.Controls.Add(this.DCPLabelHeight);
            this.Controls.Add(this.DCPComboBoxHeight);
            this.Controls.Add(this.DCPLabelCurrentSrcName);
            this.Controls.Add(this.DCPLabelPrevSrcName);
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
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownElapsedSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCPNumericUpDownPresetHours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DCPButtonPrevSrc;
        private System.Windows.Forms.Button DCPButtonNextSrc;
        private System.Windows.Forms.Button DCPButtonClear;
        private System.Windows.Forms.Button DCPButtonSave;
        private System.Windows.Forms.Button DCPButtonStartPause;
        private System.Windows.Forms.Label DCPLabelNextSrcName;
        private System.Windows.Forms.Label DCPLabelPrevSrcName;
        private System.Windows.Forms.Label DCPLabelCurrentSrcName;
        private System.Windows.Forms.ComboBox DCPComboBoxHeight;
        private System.Windows.Forms.Label DCPLabelHeight;
        private System.Windows.Forms.Label DCPLabelCurrentSumpleOnCurrentSrc;
        private System.Windows.Forms.Label DCPLabelCurrentSumpleOnNextSrc;
        private System.Windows.Forms.Label DCPLabelCurrentSumpleOnPrevSrc;
        private System.Windows.Forms.Button DCPButtonStop;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownElapsedHours;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownElapsedMinutes;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownElapsedSeconds;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownPresetSeconds;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownPresetMinutes;
        private System.Windows.Forms.NumericUpDown DCPNumericUpDownPresetHours;
        private System.Windows.Forms.Label DCPLabelHours;
        private System.Windows.Forms.Label DCPLabelMinutes;
        private System.Windows.Forms.Label DCPLabelSeconds;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSaveCurrentSpectra;
        private System.Windows.Forms.Label DCPLabelPresetTitle;
        private System.Windows.Forms.Label DCPTimeLeftTitle;
    }
}