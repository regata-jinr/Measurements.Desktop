using Measurements.UI.Desktop.Components;

namespace Measurements.UI.Desktop.Forms
{ 
    partial class SessionForm
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
            _session.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionForm));
            this.SessionFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveSession = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuResetAll = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.SessionFormListBoxIrrDates = new System.Windows.Forms.ListBox();
            this.SessionFormDataGridViewIrradiations = new System.Windows.Forms.DataGridView();
            this.SessionFormButtonStart = new System.Windows.Forms.Button();
            this.SessionFormListBoxLabel = new System.Windows.Forms.Label();
            this.SessionFormIrradiationsDataLabel = new System.Windows.Forms.Label();
            this.SessionFormButtonStop = new System.Windows.Forms.Button();
            this.SessionFormButtonClear = new System.Windows.Forms.Button();
            this.MenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormDataGridViewIrradiations)).BeginInit();
            this.SuspendLayout();
            // 
            // SessionFormMenuStrip
            // 
            this.SessionFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMenu});
            this.SessionFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SessionFormMenuStrip.Name = "SessionFormMenuStrip";
            this.SessionFormMenuStrip.Size = new System.Drawing.Size(1365, 24);
            this.SessionFormMenuStrip.TabIndex = 0;
            this.SessionFormMenuStrip.Text = "menuStrip1";
            // 
            // MenuMenu
            // 
            this.MenuMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSaveSession,
            this.MenuResetAll});
            this.MenuMenu.Name = "MenuMenu";
            this.MenuMenu.Size = new System.Drawing.Size(53, 20);
            this.MenuMenu.Text = "Меню";
            // 
            // MenuSaveSession
            // 
            this.MenuSaveSession.Name = "MenuSaveSession";
            this.MenuSaveSession.Size = new System.Drawing.Size(192, 22);
            this.MenuSaveSession.Text = "Сохранить сессию";
            // 
            // MenuResetAll
            // 
            this.MenuResetAll.Name = "MenuResetAll";
            this.MenuResetAll.Size = new System.Drawing.Size(192, 22);
            this.MenuResetAll.Text = "Сбросить параметры";
            // 
            // SessionFormStatusStrip
            // 
            this.SessionFormStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.SessionFormStatusStrip.Name = "SessionFormStatusStrip";
            this.SessionFormStatusStrip.Size = new System.Drawing.Size(1365, 22);
            this.SessionFormStatusStrip.TabIndex = 1;
            this.SessionFormStatusStrip.Text = "statusStrip1";
            // 
            // SessionFormListBoxIrrDates
            // 
            this.SessionFormListBoxIrrDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SessionFormListBoxIrrDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormListBoxIrrDates.FormattingEnabled = true;
            this.SessionFormListBoxIrrDates.ItemHeight = 16;
            this.SessionFormListBoxIrrDates.Location = new System.Drawing.Point(12, 56);
            this.SessionFormListBoxIrrDates.Name = "SessionFormListBoxIrrDates";
            this.SessionFormListBoxIrrDates.Size = new System.Drawing.Size(120, 306);
            this.SessionFormListBoxIrrDates.TabIndex = 2;
            // 
            // SessionFormDataGridViewIrradiations
            // 
            this.SessionFormDataGridViewIrradiations.AllowUserToAddRows = false;
            this.SessionFormDataGridViewIrradiations.AllowUserToDeleteRows = false;
            this.SessionFormDataGridViewIrradiations.AllowUserToResizeColumns = false;
            this.SessionFormDataGridViewIrradiations.AllowUserToResizeRows = false;
            this.SessionFormDataGridViewIrradiations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SessionFormDataGridViewIrradiations.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SessionFormDataGridViewIrradiations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SessionFormDataGridViewIrradiations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SessionFormDataGridViewIrradiations.DefaultCellStyle = dataGridViewCellStyle2;
            this.SessionFormDataGridViewIrradiations.Location = new System.Drawing.Point(156, 56);
            this.SessionFormDataGridViewIrradiations.Name = "SessionFormDataGridViewIrradiations";
            this.SessionFormDataGridViewIrradiations.ReadOnly = true;
            this.SessionFormDataGridViewIrradiations.RowHeadersVisible = false;
            this.SessionFormDataGridViewIrradiations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SessionFormDataGridViewIrradiations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.SessionFormDataGridViewIrradiations.Size = new System.Drawing.Size(1197, 306);
            this.SessionFormDataGridViewIrradiations.TabIndex = 4;
            // 
            // SessionFormButtonStart
            // 
            this.SessionFormButtonStart.BackColor = System.Drawing.Color.Green;
            this.SessionFormButtonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonStart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStart.Location = new System.Drawing.Point(1278, 382);
            this.SessionFormButtonStart.Name = "SessionFormButtonStart";
            this.SessionFormButtonStart.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonStart.TabIndex = 5;
            this.SessionFormButtonStart.Text = "Start";
            this.SessionFormButtonStart.UseVisualStyleBackColor = false;
            // 
            // SessionFormListBoxLabel
            // 
            this.SessionFormListBoxLabel.AutoSize = true;
            this.SessionFormListBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormListBoxLabel.Location = new System.Drawing.Point(12, 40);
            this.SessionFormListBoxLabel.Name = "SessionFormListBoxLabel";
            this.SessionFormListBoxLabel.Size = new System.Drawing.Size(116, 16);
            this.SessionFormListBoxLabel.TabIndex = 7;
            this.SessionFormListBoxLabel.Text = "Даты облучений";
            // 
            // SessionFormIrradiationsDataLabel
            // 
            this.SessionFormIrradiationsDataLabel.AutoSize = true;
            this.SessionFormIrradiationsDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormIrradiationsDataLabel.Location = new System.Drawing.Point(586, 33);
            this.SessionFormIrradiationsDataLabel.Name = "SessionFormIrradiationsDataLabel";
            this.SessionFormIrradiationsDataLabel.Size = new System.Drawing.Size(298, 20);
            this.SessionFormIrradiationsDataLabel.TabIndex = 8;
            this.SessionFormIrradiationsDataLabel.Text = "Таблица образцов для измерений";
            // 
            // SessionFormButtonStop
            // 
            this.SessionFormButtonStop.BackColor = System.Drawing.Color.Maroon;
            this.SessionFormButtonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStop.Location = new System.Drawing.Point(1069, 382);
            this.SessionFormButtonStop.Name = "SessionFormButtonStop";
            this.SessionFormButtonStop.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonStop.TabIndex = 9;
            this.SessionFormButtonStop.Text = "Stop";
            this.SessionFormButtonStop.UseVisualStyleBackColor = false;
            // 
            // SessionFormButtonClear
            // 
            this.SessionFormButtonClear.BackColor = System.Drawing.Color.LightGray;
            this.SessionFormButtonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SessionFormButtonClear.Location = new System.Drawing.Point(1175, 382);
            this.SessionFormButtonClear.Name = "SessionFormButtonClear";
            this.SessionFormButtonClear.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonClear.TabIndex = 10;
            this.SessionFormButtonClear.Text = "Clear";
            this.SessionFormButtonClear.UseVisualStyleBackColor = false;
            // 
            // MenuOptions
            // 
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.CheckOnClick = false;
            this.MenuOptions.Size = new System.Drawing.Size(32, 19);
            this.MenuOptions.Text = "Опции";
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 450);
            this.Controls.Add(this.SessionFormButtonClear);
            this.Controls.Add(this.SessionFormButtonStop);
            this.Controls.Add(this.SessionFormIrradiationsDataLabel);
            this.Controls.Add(this.SessionFormListBoxLabel);
            this.Controls.Add(this.SessionFormButtonStart);
            this.Controls.Add(this.SessionFormDataGridViewIrradiations);
            this.Controls.Add(this.SessionFormListBoxIrrDates);
            this.Controls.Add(this.SessionFormStatusStrip);
            this.Controls.Add(this.SessionFormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SessionFormMenuStrip;
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            this.SessionFormMenuStrip.ResumeLayout(false);
            this.SessionFormMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormDataGridViewIrradiations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SessionFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuMenu;
        private System.Windows.Forms.StatusStrip SessionFormStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveSession;
        private System.Windows.Forms.ToolStripMenuItem MenuResetAll;
        private System.Windows.Forms.ListBox SessionFormListBoxIrrDates;
        private System.Windows.Forms.DataGridView SessionFormDataGridViewIrradiations;
        private System.Windows.Forms.Button SessionFormButtonStart;
        private System.Windows.Forms.Label SessionFormListBoxLabel;
        private System.Windows.Forms.Label SessionFormIrradiationsDataLabel;
        private System.Windows.Forms.Button SessionFormButtonStop;
        private System.Windows.Forms.Button SessionFormButtonClear;


        // custom components
        private System.Windows.Forms.ToolStripMenuItem DetectorsDropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuOptions;

        private System.Windows.Forms.ToolStripStatusLabel DetectorsLabelStart;
        private System.Windows.Forms.ToolStripStatusLabel DetectorsLabelEnd;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionStatus;
        private EnumItem CountsOptionsItem;
        private EnumItem SpreadOptionsItem;
    }
}