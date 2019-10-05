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
            if (_countsForm != null)
                _countsForm.SaveCountsEvent -= SaveCounts;


            Measurements.UI.Managers.ProcessManager.CloseMvcg();
            _countsForm.Dispose();
            _session.Dispose();
            Measurements.Core.SessionControllerSingleton.AvailableDetectorsListHasChanged -= InitializeDetectorDropDownItems;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionForm));
            this.SessionFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveSession = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.SessionFormListBoxIrrDates = new System.Windows.Forms.ListBox();
            this.SessionFormButtonStart = new System.Windows.Forms.Button();
            this.SessionFormListBoxLabel = new System.Windows.Forms.Label();
            this.SessionFormIrradiationsDataLabel = new System.Windows.Forms.Label();
            this.SessionFormButtonPause = new System.Windows.Forms.Button();
            this.MenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormadvancedDataGridView = new Zuby.ADGV.AdvancedDataGridView();
            this.SessionFormDisplayedDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SessionFormButtonStop = new System.Windows.Forms.Button();
            this.SessionFormButtonClear = new System.Windows.Forms.Button();
            this.SessionFormLabelAllDetectors = new System.Windows.Forms.Label();
            this.SessionFormMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormadvancedDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormDisplayedDataBindingSource)).BeginInit();
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
            this.MenuSaveSession});
            this.MenuMenu.Name = "MenuMenu";
            this.MenuMenu.Size = new System.Drawing.Size(53, 20);
            this.MenuMenu.Text = "Меню";
            // 
            // MenuSaveSession
            // 
            this.MenuSaveSession.Name = "MenuSaveSession";
            this.MenuSaveSession.Size = new System.Drawing.Size(177, 22);
            this.MenuSaveSession.Text = "Сохранить сессию";
            this.MenuSaveSession.Click += new System.EventHandler(this.MenuSaveSession_Click);
            // 
            // SessionFormStatusStrip
            // 
            this.SessionFormStatusStrip.Location = new System.Drawing.Point(0, 443);
            this.SessionFormStatusStrip.Name = "SessionFormStatusStrip";
            this.SessionFormStatusStrip.Size = new System.Drawing.Size(1365, 22);
            this.SessionFormStatusStrip.TabIndex = 1;
            this.SessionFormStatusStrip.Text = "statusStrip1";
            // 
            // SessionFormListBoxIrrDates
            // 
            this.SessionFormListBoxIrrDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SessionFormListBoxIrrDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SessionFormListBoxIrrDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormListBoxIrrDates.FormattingEnabled = true;
            this.SessionFormListBoxIrrDates.ItemHeight = 16;
            this.SessionFormListBoxIrrDates.Location = new System.Drawing.Point(12, 56);
            this.SessionFormListBoxIrrDates.Name = "SessionFormListBoxIrrDates";
            this.SessionFormListBoxIrrDates.Size = new System.Drawing.Size(116, 306);
            this.SessionFormListBoxIrrDates.TabIndex = 2;
            // 
            // SessionFormButtonStart
            // 
            this.SessionFormButtonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonStart.BackColor = System.Drawing.Color.Green;
            this.SessionFormButtonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonStart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStart.Location = new System.Drawing.Point(1278, 397);
            this.SessionFormButtonStart.Name = "SessionFormButtonStart";
            this.SessionFormButtonStart.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonStart.TabIndex = 5;
            this.SessionFormButtonStart.Text = "Start";
            this.SessionFormButtonStart.UseVisualStyleBackColor = false;
            this.SessionFormButtonStart.Click += new System.EventHandler(this.SessionFormButtonStart_Click);
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
            this.SessionFormIrradiationsDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormIrradiationsDataLabel.AutoSize = true;
            this.SessionFormIrradiationsDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormIrradiationsDataLabel.Location = new System.Drawing.Point(586, 33);
            this.SessionFormIrradiationsDataLabel.Name = "SessionFormIrradiationsDataLabel";
            this.SessionFormIrradiationsDataLabel.Size = new System.Drawing.Size(298, 20);
            this.SessionFormIrradiationsDataLabel.TabIndex = 8;
            this.SessionFormIrradiationsDataLabel.Text = "Таблица образцов для измерений";
            // 
            // SessionFormButtonPause
            // 
            this.SessionFormButtonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonPause.BackColor = System.Drawing.Color.DimGray;
            this.SessionFormButtonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonPause.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonPause.Location = new System.Drawing.Point(1197, 397);
            this.SessionFormButtonPause.Name = "SessionFormButtonPause";
            this.SessionFormButtonPause.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonPause.TabIndex = 9;
            this.SessionFormButtonPause.Text = "Pause";
            this.SessionFormButtonPause.UseVisualStyleBackColor = false;
            this.SessionFormButtonPause.Click += new System.EventHandler(this.SessionFormButtonPause_Click);
            // 
            // MenuOptions
            // 
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.Size = new System.Drawing.Size(32, 19);
            this.MenuOptions.Text = "Опции";
            // 
            // SessionFormadvancedDataGridView
            // 
            this.SessionFormadvancedDataGridView.AllowUserToAddRows = false;
            this.SessionFormadvancedDataGridView.AllowUserToDeleteRows = false;
            this.SessionFormadvancedDataGridView.AllowUserToResizeRows = false;
            this.SessionFormadvancedDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormadvancedDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SessionFormadvancedDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SessionFormadvancedDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SessionFormadvancedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SessionFormadvancedDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.SessionFormadvancedDataGridView.FilterAndSortEnabled = true;
            this.SessionFormadvancedDataGridView.Location = new System.Drawing.Point(134, 56);
            this.SessionFormadvancedDataGridView.Name = "SessionFormadvancedDataGridView";
            this.SessionFormadvancedDataGridView.ReadOnly = true;
            this.SessionFormadvancedDataGridView.RowHeadersVisible = false;
            this.SessionFormadvancedDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SessionFormadvancedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.SessionFormadvancedDataGridView.Size = new System.Drawing.Size(1219, 306);
            this.SessionFormadvancedDataGridView.TabIndex = 10;
            // 
            // SessionFormButtonStop
            // 
            this.SessionFormButtonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonStop.BackColor = System.Drawing.Color.Maroon;
            this.SessionFormButtonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStop.Location = new System.Drawing.Point(1035, 397);
            this.SessionFormButtonStop.Name = "SessionFormButtonStop";
            this.SessionFormButtonStop.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonStop.TabIndex = 11;
            this.SessionFormButtonStop.Text = "Stop";
            this.SessionFormButtonStop.UseVisualStyleBackColor = false;
            this.SessionFormButtonStop.Click += new System.EventHandler(this.SessionFormButtonStop_Click);
            // 
            // SessionFormButtonClear
            // 
            this.SessionFormButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonClear.BackColor = System.Drawing.Color.White;
            this.SessionFormButtonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormButtonClear.ForeColor = System.Drawing.Color.Black;
            this.SessionFormButtonClear.Location = new System.Drawing.Point(1116, 397);
            this.SessionFormButtonClear.Name = "SessionFormButtonClear";
            this.SessionFormButtonClear.Size = new System.Drawing.Size(75, 43);
            this.SessionFormButtonClear.TabIndex = 12;
            this.SessionFormButtonClear.Text = "Clear";
            this.SessionFormButtonClear.UseVisualStyleBackColor = false;
            this.SessionFormButtonClear.Click += new System.EventHandler(this.SessionFormButtonClear_Click);
            // 
            // SessionFormLabelAllDetectors
            // 
            this.SessionFormLabelAllDetectors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormLabelAllDetectors.AutoSize = true;
            this.SessionFormLabelAllDetectors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionFormLabelAllDetectors.Location = new System.Drawing.Point(1040, 376);
            this.SessionFormLabelAllDetectors.Name = "SessionFormLabelAllDetectors";
            this.SessionFormLabelAllDetectors.Size = new System.Drawing.Size(313, 18);
            this.SessionFormLabelAllDetectors.TabIndex = 13;
            this.SessionFormLabelAllDetectors.Text = "Управление выбранными детекторами";
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 465);
            this.Controls.Add(this.SessionFormLabelAllDetectors);
            this.Controls.Add(this.SessionFormButtonClear);
            this.Controls.Add(this.SessionFormButtonStop);
            this.Controls.Add(this.SessionFormadvancedDataGridView);
            this.Controls.Add(this.SessionFormButtonPause);
            this.Controls.Add(this.SessionFormIrradiationsDataLabel);
            this.Controls.Add(this.SessionFormListBoxLabel);
            this.Controls.Add(this.SessionFormButtonStart);
            this.Controls.Add(this.SessionFormListBoxIrrDates);
            this.Controls.Add(this.SessionFormStatusStrip);
            this.Controls.Add(this.SessionFormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SessionFormMenuStrip;
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            this.SessionFormMenuStrip.ResumeLayout(false);
            this.SessionFormMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormadvancedDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormDisplayedDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SessionFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuMenu;
        private System.Windows.Forms.StatusStrip SessionFormStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveSession;
        private System.Windows.Forms.ListBox SessionFormListBoxIrrDates;
        private System.Windows.Forms.Button SessionFormButtonStart;
        private System.Windows.Forms.Label SessionFormListBoxLabel;
        private System.Windows.Forms.Label SessionFormIrradiationsDataLabel;
        private System.Windows.Forms.Button SessionFormButtonPause;


        // custom components
        private System.Windows.Forms.ToolStripMenuItem DetectorsDropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuOptions;

        private System.Windows.Forms.ToolStripStatusLabel DetectorsLabelStart;
        private System.Windows.Forms.ToolStripStatusLabel DetectorsLabelEnd;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionStatus;
        private EnumItem CountsOptionsItem;
        private EnumItem SpreadOptionsItem;
        private System.Windows.Forms.ToolStripStatusLabel CountsStatusLabel;
        private CountsForm _countsForm;
        private System.Windows.Forms.ToolStripDropDownButton HeightDropDownButton;
        private System.Windows.Forms.ToolStripProgressBar MeasurementsProgressBar;
        private Zuby.ADGV.AdvancedDataGridView SessionFormadvancedDataGridView;
        private System.Windows.Forms.BindingSource SessionFormDisplayedDataBindingSource;
        private System.Windows.Forms.Button SessionFormButtonStop;
        private System.Windows.Forms.Button SessionFormButtonClear;
        private System.Windows.Forms.Label SessionFormLabelAllDetectors;
    }
}