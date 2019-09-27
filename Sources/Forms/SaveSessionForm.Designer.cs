namespace Measurements.UI.Desktop.Forms
{
    partial class SaveSessionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveSessionForm));
            this.labelSaveSession = new System.Windows.Forms.Label();
            this.textBoxSaveSessionName = new System.Windows.Forms.TextBox();
            this.checkBoxSaveSessionIsGlobal = new System.Windows.Forms.CheckBox();
            this.buttonSaveSessionSave = new System.Windows.Forms.Button();
            this.buttonSaveSessionCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSaveSession
            // 
            this.labelSaveSession.AutoSize = true;
            this.labelSaveSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaveSession.Location = new System.Drawing.Point(36, 9);
            this.labelSaveSession.Name = "labelSaveSession";
            this.labelSaveSession.Size = new System.Drawing.Size(139, 16);
            this.labelSaveSession.TabIndex = 3;
            this.labelSaveSession.Text = "Укажите имя сессии";
            // 
            // textBoxSaveSessionName
            // 
            this.textBoxSaveSessionName.Location = new System.Drawing.Point(12, 28);
            this.textBoxSaveSessionName.Name = "textBoxSaveSessionName";
            this.textBoxSaveSessionName.Size = new System.Drawing.Size(197, 20);
            this.textBoxSaveSessionName.TabIndex = 0;
            // 
            // checkBoxSaveSessionIsGlobal
            // 
            this.checkBoxSaveSessionIsGlobal.AutoSize = true;
            this.checkBoxSaveSessionIsGlobal.Location = new System.Drawing.Point(12, 54);
            this.checkBoxSaveSessionIsGlobal.Name = "checkBoxSaveSessionIsGlobal";
            this.checkBoxSaveSessionIsGlobal.Size = new System.Drawing.Size(197, 30);
            this.checkBoxSaveSessionIsGlobal.TabIndex = 1;
            this.checkBoxSaveSessionIsGlobal.Text = "Сессия должна быть видна всем \r\nпользователям";
            this.checkBoxSaveSessionIsGlobal.UseVisualStyleBackColor = true;
            // 
            // buttonSaveSessionSave
            // 
            this.buttonSaveSessionSave.Location = new System.Drawing.Point(134, 90);
            this.buttonSaveSessionSave.Name = "buttonSaveSessionSave";
            this.buttonSaveSessionSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSessionSave.TabIndex = 2;
            this.buttonSaveSessionSave.Text = "Сохранить";
            this.buttonSaveSessionSave.UseVisualStyleBackColor = true;
            this.buttonSaveSessionSave.Click += new System.EventHandler(this.buttonSaveSessionSave_Click);
            // 
            // buttonSaveSessionCancel
            // 
            this.buttonSaveSessionCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSaveSessionCancel.Location = new System.Drawing.Point(12, 90);
            this.buttonSaveSessionCancel.Name = "buttonSaveSessionCancel";
            this.buttonSaveSessionCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSessionCancel.TabIndex = 4;
            this.buttonSaveSessionCancel.Text = "Отмена";
            this.buttonSaveSessionCancel.UseVisualStyleBackColor = true;
            // 
            // SaveSessionForm
            // 
            this.AcceptButton = this.buttonSaveSessionSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSaveSessionCancel;
            this.ClientSize = new System.Drawing.Size(218, 123);
            this.Controls.Add(this.buttonSaveSessionCancel);
            this.Controls.Add(this.buttonSaveSessionSave);
            this.Controls.Add(this.checkBoxSaveSessionIsGlobal);
            this.Controls.Add(this.textBoxSaveSessionName);
            this.Controls.Add(this.labelSaveSession);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveSessionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaveSessionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSaveSession;
        private System.Windows.Forms.TextBox textBoxSaveSessionName;
        private System.Windows.Forms.CheckBox checkBoxSaveSessionIsGlobal;
        private System.Windows.Forms.Button buttonSaveSessionSave;
        private System.Windows.Forms.Button buttonSaveSessionCancel;
    }
}