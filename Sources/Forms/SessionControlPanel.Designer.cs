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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionControlPanel));
            this.SessionContorllerPanelMenuStrip = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логгированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.русскийRussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.английскийEnglishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionControlPaneldataGridViewSessions = new System.Windows.Forms.DataGridView();
            this.SessionControlPanelButtonLoadSession = new System.Windows.Forms.Button();
            this.SessionControlPanelButtonCreateSession = new System.Windows.Forms.Button();
            this.SessionControlPanelLabelTable = new System.Windows.Forms.Label();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionContorllerPanelMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionControlPaneldataGridViewSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // SessionContorllerPanelMenuStrip
            // 
            this.SessionContorllerPanelMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionContorllerPanelMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.опцииToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.SessionContorllerPanelMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SessionContorllerPanelMenuStrip.Name = "SessionContorllerPanelMenuStrip";
            this.SessionContorllerPanelMenuStrip.Size = new System.Drawing.Size(582, 25);
            this.SessionContorllerPanelMenuStrip.TabIndex = 0;
            this.SessionContorllerPanelMenuStrip.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.логгированиеToolStripMenuItem,
            this.языкToolStripMenuItem});
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.опцииToolStripMenuItem.Text = "Опции";
            // 
            // логгированиеToolStripMenuItem
            // 
            this.логгированиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.warningToolStripMenuItem,
            this.errorToolStripMenuItem});
            this.логгированиеToolStripMenuItem.Name = "логгированиеToolStripMenuItem";
            this.логгированиеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.логгированиеToolStripMenuItem.Text = "Логгирование";
            // 
            // языкToolStripMenuItem
            // 
            this.языкToolStripMenuItem.CheckOnClick = true;
            this.языкToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.русскийRussianToolStripMenuItem,
            this.английскийEnglishToolStripMenuItem});
            this.языкToolStripMenuItem.Name = "языкToolStripMenuItem";
            this.языкToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.языкToolStripMenuItem.Text = "Язык";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.debugToolStripMenuItem.Text = "debug";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.CheckOnClick = true;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.infoToolStripMenuItem.Text = "info";
            // 
            // warningToolStripMenuItem
            // 
            this.warningToolStripMenuItem.CheckOnClick = true;
            this.warningToolStripMenuItem.Name = "warningToolStripMenuItem";
            this.warningToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.warningToolStripMenuItem.Text = "warning";
            // 
            // errorToolStripMenuItem
            // 
            this.errorToolStripMenuItem.CheckOnClick = true;
            this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
            this.errorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.errorToolStripMenuItem.Text = "error";
            // 
            // русскийRussianToolStripMenuItem
            // 
            this.русскийRussianToolStripMenuItem.CheckOnClick = true;
            this.русскийRussianToolStripMenuItem.Name = "русскийRussianToolStripMenuItem";
            this.русскийRussianToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.русскийRussianToolStripMenuItem.Text = "Русский(Russian)";
            // 
            // английскийEnglishToolStripMenuItem
            // 
            this.английскийEnglishToolStripMenuItem.CheckOnClick = true;
            this.английскийEnglishToolStripMenuItem.Name = "английскийEnglishToolStripMenuItem";
            this.английскийEnglishToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.английскийEnglishToolStripMenuItem.Text = "Английский(English)";
            // 
            // SessionControlPaneldataGridViewSessions
            // 
            this.SessionControlPaneldataGridViewSessions.AllowUserToAddRows = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToDeleteRows = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToResizeColumns = false;
            this.SessionControlPaneldataGridViewSessions.AllowUserToResizeRows = false;
            this.SessionControlPaneldataGridViewSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SessionControlPaneldataGridViewSessions.BackgroundColor = System.Drawing.Color.White;
            this.SessionControlPaneldataGridViewSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SessionControlPaneldataGridViewSessions.Location = new System.Drawing.Point(12, 59);
            this.SessionControlPaneldataGridViewSessions.Name = "SessionControlPaneldataGridViewSessions";
            this.SessionControlPaneldataGridViewSessions.RowHeadersVisible = false;
            this.SessionControlPaneldataGridViewSessions.Size = new System.Drawing.Size(556, 301);
            this.SessionControlPaneldataGridViewSessions.TabIndex = 1;
            // 
            // SessionControlPanelButtonLoadSession
            // 
            this.SessionControlPanelButtonLoadSession.Location = new System.Drawing.Point(107, 389);
            this.SessionControlPanelButtonLoadSession.Name = "SessionControlPanelButtonLoadSession";
            this.SessionControlPanelButtonLoadSession.Size = new System.Drawing.Size(75, 23);
            this.SessionControlPanelButtonLoadSession.TabIndex = 2;
            this.SessionControlPanelButtonLoadSession.Text = "button1";
            this.SessionControlPanelButtonLoadSession.UseVisualStyleBackColor = true;
            // 
            // SessionControlPanelButtonCreateSession
            // 
            this.SessionControlPanelButtonCreateSession.Location = new System.Drawing.Point(383, 389);
            this.SessionControlPanelButtonCreateSession.Name = "SessionControlPanelButtonCreateSession";
            this.SessionControlPanelButtonCreateSession.Size = new System.Drawing.Size(75, 23);
            this.SessionControlPanelButtonCreateSession.TabIndex = 3;
            this.SessionControlPanelButtonCreateSession.Text = "button2";
            this.SessionControlPanelButtonCreateSession.UseVisualStyleBackColor = true;
            // 
            // SessionControlPanelLabelTable
            // 
            this.SessionControlPanelLabelTable.AutoSize = true;
            this.SessionControlPanelLabelTable.Location = new System.Drawing.Point(124, 43);
            this.SessionControlPanelLabelTable.Name = "SessionControlPanelLabelTable";
            this.SessionControlPanelLabelTable.Size = new System.Drawing.Size(35, 13);
            this.SessionControlPanelLabelTable.TabIndex = 4;
            this.SessionControlPanelLabelTable.Text = "label1";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(102, 21);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // SessionControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 432);
            this.Controls.Add(this.SessionControlPanelLabelTable);
            this.Controls.Add(this.SessionControlPanelButtonCreateSession);
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
            this.SessionContorllerPanelMenuStrip.ResumeLayout(false);
            this.SessionContorllerPanelMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionControlPaneldataGridViewSessions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SessionContorllerPanelMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem логгированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem русскийRussianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem английскийEnglishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.DataGridView SessionControlPaneldataGridViewSessions;
        private System.Windows.Forms.Button SessionControlPanelButtonLoadSession;
        private System.Windows.Forms.Button SessionControlPanelButtonCreateSession;
        private System.Windows.Forms.Label SessionControlPanelLabelTable;
    }
}