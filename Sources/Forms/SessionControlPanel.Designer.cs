namespace Measurements.UI.Forms
{
    partial class SessionControlPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionControlPanel));
            this.SessionContorllerPanelMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SessionControlerToolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControlerToolStripMenuItemOptionsVerbosity = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControlerToolStripMenuItemLang = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControllerRussianToolStripMenuItemLangRussian = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControllerRussianToolStripMenuItemLangEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControlPaneldataGridViewSessions = new System.Windows.Forms.DataGridView();
            this.SessionControlPanelButtonLoadSession = new System.Windows.Forms.Button();
            this.SessionControlPanelLabelTable = new System.Windows.Forms.Label();
            this.SessionControlPanelButtonCreateSession = new System.Windows.Forms.Button();
            this.SessionContorllerPanelMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionControlPaneldataGridViewSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // SessionContorllerPanelMenuStrip
            // 
            this.SessionContorllerPanelMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionContorllerPanelMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionControlerToolStripMenuItemOptions,
            this.ToolStripMenuItemAbout});
            this.SessionContorllerPanelMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SessionContorllerPanelMenuStrip.Name = "SessionContorllerPanelMenuStrip";
            this.SessionContorllerPanelMenuStrip.Size = new System.Drawing.Size(798, 25);
            this.SessionContorllerPanelMenuStrip.TabIndex = 0;
            this.SessionContorllerPanelMenuStrip.Text = "menuStrip1";
            // 
            // SessionControlerToolStripMenuItemOptions
            // 
            this.SessionControlerToolStripMenuItemOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionControlerToolStripMenuItemOptionsVerbosity,
            this.SessionControlerToolStripMenuItemLang});
            this.SessionControlerToolStripMenuItemOptions.Name = "SessionControlerToolStripMenuItemOptions";
            this.SessionControlerToolStripMenuItemOptions.Size = new System.Drawing.Size(58, 21);
            this.SessionControlerToolStripMenuItemOptions.Text = "Опции";
            this.SessionControlerToolStripMenuItemOptions.Visible = false;
            // 
            // SessionControlerToolStripMenuItemOptionsVerbosity
            // 
            this.SessionControlerToolStripMenuItemOptionsVerbosity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.warningToolStripMenuItem,
            this.errorToolStripMenuItem});
            this.SessionControlerToolStripMenuItemOptionsVerbosity.Name = "SessionControlerToolStripMenuItemOptionsVerbosity";
            this.SessionControlerToolStripMenuItemOptionsVerbosity.Size = new System.Drawing.Size(161, 22);
            this.SessionControlerToolStripMenuItemOptionsVerbosity.Text = "Логгирование";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.debugToolStripMenuItem.Text = "debug";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.CheckOnClick = true;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.infoToolStripMenuItem.Text = "info";
            // 
            // warningToolStripMenuItem
            // 
            this.warningToolStripMenuItem.CheckOnClick = true;
            this.warningToolStripMenuItem.Name = "warningToolStripMenuItem";
            this.warningToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.warningToolStripMenuItem.Text = "warning";
            // 
            // errorToolStripMenuItem
            // 
            this.errorToolStripMenuItem.CheckOnClick = true;
            this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
            this.errorToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.errorToolStripMenuItem.Text = "error";
            // 
            // SessionControlerToolStripMenuItemLang
            // 
            this.SessionControlerToolStripMenuItemLang.CheckOnClick = true;
            this.SessionControlerToolStripMenuItemLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionControllerRussianToolStripMenuItemLangRussian,
            this.SessionControllerRussianToolStripMenuItemLangEnglish});
            this.SessionControlerToolStripMenuItemLang.Name = "SessionControlerToolStripMenuItemLang";
            this.SessionControlerToolStripMenuItemLang.Size = new System.Drawing.Size(161, 22);
            this.SessionControlerToolStripMenuItemLang.Text = "Язык";
            // 
            // SessionControllerRussianToolStripMenuItemLangRussian
            // 
            this.SessionControllerRussianToolStripMenuItemLangRussian.CheckOnClick = true;
            this.SessionControllerRussianToolStripMenuItemLangRussian.Name = "SessionControllerRussianToolStripMenuItemLangRussian";
            this.SessionControllerRussianToolStripMenuItemLangRussian.Size = new System.Drawing.Size(192, 22);
            this.SessionControllerRussianToolStripMenuItemLangRussian.Text = "Русский(Russian)";
            // 
            // SessionControllerRussianToolStripMenuItemLangEnglish
            // 
            this.SessionControllerRussianToolStripMenuItemLangEnglish.CheckOnClick = true;
            this.SessionControllerRussianToolStripMenuItemLangEnglish.Name = "SessionControllerRussianToolStripMenuItemLangEnglish";
            this.SessionControllerRussianToolStripMenuItemLangEnglish.Size = new System.Drawing.Size(192, 22);
            this.SessionControllerRussianToolStripMenuItemLangEnglish.Text = "Английский(English)";
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(102, 21);
            this.ToolStripMenuItemAbout.Text = "О программе";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ShowAbout);
            // 
            // SessionControlPaneldataGridViewSessions
            // 
            this.SessionControlPaneldataGridViewSessions.AllowUserToAddRows = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToDeleteRows = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToResizeColumns = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToResizeRows = false;
            this.SessionControlPaneldataGridViewSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SessionControlPaneldataGridViewSessions.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SessionControlPaneldataGridViewSessions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SessionControlPaneldataGridViewSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SessionControlPaneldataGridViewSessions.DefaultCellStyle = dataGridViewCellStyle2;
            this.SessionControlPaneldataGridViewSessions.Location = new System.Drawing.Point(12, 59);
            this.SessionControlPaneldataGridViewSessions.MultiSelect = false;
            this.SessionControlPaneldataGridViewSessions.Name = "SessionControlPaneldataGridViewSessions";
            this.SessionControlPaneldataGridViewSessions.ReadOnly = true;
            this.SessionControlPaneldataGridViewSessions.RowHeadersVisible = false;
            this.SessionControlPaneldataGridViewSessions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SessionControlPaneldataGridViewSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SessionControlPaneldataGridViewSessions.Size = new System.Drawing.Size(774, 301);
            this.SessionControlPaneldataGridViewSessions.TabIndex = 1;
            this.SessionControlPaneldataGridViewSessions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SessionControlPanelButtonLoadSession_Click);
            // 
            // SessionControlPanelButtonLoadSession
            // 
            this.SessionControlPanelButtonLoadSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionControlPanelButtonLoadSession.Location = new System.Drawing.Point(12, 376);
            this.SessionControlPanelButtonLoadSession.Name = "SessionControlPanelButtonLoadSession";
            this.SessionControlPanelButtonLoadSession.Size = new System.Drawing.Size(193, 44);
            this.SessionControlPanelButtonLoadSession.TabIndex = 2;
            this.SessionControlPanelButtonLoadSession.Text = "Загрузить выделенную сессию";
            this.SessionControlPanelButtonLoadSession.UseVisualStyleBackColor = true;
            this.SessionControlPanelButtonLoadSession.Click += new System.EventHandler(this.SessionControlPanelButtonLoadSession_Click);
            // 
            // SessionControlPanelLabelTable
            // 
            this.SessionControlPanelLabelTable.AutoSize = true;
            this.SessionControlPanelLabelTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionControlPanelLabelTable.Location = new System.Drawing.Point(227, 38);
            this.SessionControlPanelLabelTable.Name = "SessionControlPanelLabelTable";
            this.SessionControlPanelLabelTable.Size = new System.Drawing.Size(321, 18);
            this.SessionControlPanelLabelTable.TabIndex = 4;
            this.SessionControlPanelLabelTable.Text = "Список сессий доступных для загрузки";
            // 
            // SessionControlPanelButtonCreateSession
            // 
            this.SessionControlPanelButtonCreateSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionControlPanelButtonCreateSession.Location = new System.Drawing.Point(593, 376);
            this.SessionControlPanelButtonCreateSession.Name = "SessionControlPanelButtonCreateSession";
            this.SessionControlPanelButtonCreateSession.Size = new System.Drawing.Size(193, 44);
            this.SessionControlPanelButtonCreateSession.TabIndex = 5;
            this.SessionControlPanelButtonCreateSession.Text = "Создать новую сессию";
            this.SessionControlPanelButtonCreateSession.UseVisualStyleBackColor = true;
            this.SessionControlPanelButtonCreateSession.Click += new System.EventHandler(this.SessionControlPanelButtonCreateSession_Click);
            // 
            // SessionControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 432);
            this.Controls.Add(this.SessionControlPanelButtonCreateSession);
            this.Controls.Add(this.SessionControlPanelLabelTable);
            this.Controls.Add(this.SessionControlPanelButtonLoadSession);
            this.Controls.Add(this.SessionControlPaneldataGridViewSessions);
            this.Controls.Add(this.SessionContorllerPanelMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SessionContorllerPanelMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SessionControlPanel";
            this.Text = "SessionControlPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseClick);
            this.SessionContorllerPanelMenuStrip.ResumeLayout(false);
            this.SessionContorllerPanelMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionControlPaneldataGridViewSessions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SessionContorllerPanelMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SessionControlerToolStripMenuItemOptions;
        private System.Windows.Forms.ToolStripMenuItem SessionControlerToolStripMenuItemOptionsVerbosity;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SessionControlerToolStripMenuItemLang;
        private System.Windows.Forms.ToolStripMenuItem SessionControllerRussianToolStripMenuItemLangRussian;
        private System.Windows.Forms.ToolStripMenuItem SessionControllerRussianToolStripMenuItemLangEnglish;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.DataGridView SessionControlPaneldataGridViewSessions;
        private System.Windows.Forms.Button SessionControlPanelButtonLoadSession;
        private System.Windows.Forms.Label SessionControlPanelLabelTable;
        private System.Windows.Forms.Button SessionControlPanelButtonCreateSession;
    }
}