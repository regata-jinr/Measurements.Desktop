namespace Measurements.UI.Desktop.Forms
{
    partial class CountsForm
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
            this.labelHours = new System.Windows.Forms.Label();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelSeconds = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonSaveCounts = new System.Windows.Forms.Button();
            this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(40, 10);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(35, 13);
            this.labelHours.TabIndex = 3;
            this.labelHours.Text = "Часы";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(130, 13);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(46, 13);
            this.labelMinutes.TabIndex = 4;
            this.labelMinutes.Text = "Минуты";
            // 
            // labelSeconds
            // 
            this.labelSeconds.AutoSize = true;
            this.labelSeconds.Location = new System.Drawing.Point(218, 13);
            this.labelSeconds.Name = "labelSeconds";
            this.labelSeconds.Size = new System.Drawing.Size(51, 13);
            this.labelSeconds.TabIndex = 5;
            this.labelSeconds.Text = "Секунды";
            // 
            // buttonReset
            // 
            this.buttonReset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonReset.Location = new System.Drawing.Point(69, 55);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Сброс";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonSaveCounts
            // 
            this.buttonSaveCounts.Location = new System.Drawing.Point(166, 55);
            this.buttonSaveCounts.Name = "buttonSaveCounts";
            this.buttonSaveCounts.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveCounts.TabIndex = 7;
            this.buttonSaveCounts.Text = "Сохранить";
            this.buttonSaveCounts.UseVisualStyleBackColor = true;
            this.buttonSaveCounts.Click += new System.EventHandler(this.buttonSaveCounts_Click);
            // 
            // numericUpDownHours
            // 
            this.numericUpDownHours.Location = new System.Drawing.Point(43, 29);
            this.numericUpDownHours.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownHours.Name = "numericUpDownHours";
            this.numericUpDownHours.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownHours.TabIndex = 8;
            // 
            // numericUpDownMinutes
            // 
            this.numericUpDownMinutes.Location = new System.Drawing.Point(133, 29);
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownMinutes.TabIndex = 9;
            // 
            // numericUpDownSeconds
            // 
            this.numericUpDownSeconds.Location = new System.Drawing.Point(221, 29);
            this.numericUpDownSeconds.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownSeconds.Name = "numericUpDownSeconds";
            this.numericUpDownSeconds.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownSeconds.TabIndex = 10;
            // 
            // CountsForm
            // 
            this.AcceptButton = this.buttonSaveCounts;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonReset;
            this.ClientSize = new System.Drawing.Size(317, 86);
            this.Controls.Add(this.numericUpDownSeconds);
            this.Controls.Add(this.numericUpDownMinutes);
            this.Controls.Add(this.numericUpDownHours);
            this.Controls.Add(this.buttonSaveCounts);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelSeconds);
            this.Controls.Add(this.labelMinutes);
            this.Controls.Add(this.labelHours);
            this.Name = "CountsForm";
            this.Text = "CountsForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelSeconds;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonSaveCounts;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
    }
}