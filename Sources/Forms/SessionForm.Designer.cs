namespace Measurements.UI.Forms
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SessionFormToolStripMenuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuDetectors = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuType = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuOptionsCountModes = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuOptionsSpreadOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionFormToolStripMenuMenu,
            this.SessionFormToolStripMenuDetectors,
            this.SessionFormToolStripMenuType,
            this.SessionFormToolStripMenuOptions});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SessionFormToolStripMenuMenu
            // 
            this.SessionFormToolStripMenuMenu.Name = "SessionFormToolStripMenuMenu";
            this.SessionFormToolStripMenuMenu.Size = new System.Drawing.Size(53, 20);
            this.SessionFormToolStripMenuMenu.Text = "Меню";
            // 
            // SessionFormToolStripMenuDetectors
            // 
            this.SessionFormToolStripMenuDetectors.Name = "SessionFormToolStripMenuDetectors";
            this.SessionFormToolStripMenuDetectors.Size = new System.Drawing.Size(78, 20);
            this.SessionFormToolStripMenuDetectors.Text = "Детекторы";
            // 
            // SessionFormToolStripMenuType
            // 
            this.SessionFormToolStripMenuType.Name = "SessionFormToolStripMenuType";
            this.SessionFormToolStripMenuType.Size = new System.Drawing.Size(39, 20);
            this.SessionFormToolStripMenuType.Text = "Тип";
            // 
            // SessionFormToolStripMenuOptions
            // 
            this.SessionFormToolStripMenuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionFormToolStripMenuOptionsCountModes,
            this.SessionFormToolStripMenuOptionsSpreadOptions});
            this.SessionFormToolStripMenuOptions.Name = "SessionFormToolStripMenuOptions";
            this.SessionFormToolStripMenuOptions.Size = new System.Drawing.Size(56, 20);
            this.SessionFormToolStripMenuOptions.Text = "Опции";
            // 
            // SessionFormToolStripMenuOptionsCountModes
            // 
            this.SessionFormToolStripMenuOptionsCountModes.Name = "SessionFormToolStripMenuOptionsCountModes";
            this.SessionFormToolStripMenuOptionsCountModes.Size = new System.Drawing.Size(199, 22);
            this.SessionFormToolStripMenuOptionsCountModes.Text = "Режим отсчета";
            // 
            // SessionFormToolStripMenuOptionsSpreadOptions
            // 
            this.SessionFormToolStripMenuOptionsSpreadOptions.Name = "SessionFormToolStripMenuOptionsSpreadOptions";
            this.SessionFormToolStripMenuOptionsSpreadOptions.Size = new System.Drawing.Size(199, 22);
            this.SessionFormToolStripMenuOptionsSpreadOptions.Text = "Режим распределения";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuMenu;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuDetectors;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuType;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuOptions;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuOptionsCountModes;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuOptionsSpreadOptions;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}