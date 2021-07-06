/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Desktop.WinForms.Components;
using Regata.Core.UI.WinForms;
using Regata.Core.DataBase.Models;

namespace Regata.Desktop.WinForms.Measurements
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
            this.SessionFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveSession = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormToolStripMenuItemRefreshFormContent = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.SessionFormButtonStart = new System.Windows.Forms.Button();
            this.SessionFormButtonPause = new System.Windows.Forms.Button();
            this.MenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionFormButtonStop = new System.Windows.Forms.Button();
            this.SessionFormButtonClear = new System.Windows.Forms.Button();
            this.SessionFormControlPanel = new System.Windows.Forms.GroupBox();
            this.SessionFormGroupBoxFormMeasurementsJournal = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SessionFormlButtonAddAllToJournal = new System.Windows.Forms.Button();
            this.SessionFormlButtonAddSelectedToJournal = new System.Windows.Forms.Button();
            this.SessionFormButtonRemoveSelectedFromJournal = new System.Windows.Forms.Button();
            this.SessionFormGroupBoxDuration = new System.Windows.Forms.GroupBox();
            this.SessionFormLabelDays = new System.Windows.Forms.Label();
            this.SessionFormNumericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.SessionFormNumericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.SessionFormLabelMinutes = new System.Windows.Forms.Label();
            this.SessionFormNumericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.SessionFormLabelSeconds = new System.Windows.Forms.Label();
            this.SessionFormLabelHours = new System.Windows.Forms.Label();
            this.SessionFormNumericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.SessionFormGroupBoxDetectors = new System.Windows.Forms.GroupBox();
            this.SessionFormTableLayoutPanelDetectors = new System.Windows.Forms.TableLayoutPanel();
            this.SessionFormGroupBoxHeights = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.SessionFormsRadioButtonHeight2_5 = new System.Windows.Forms.RadioButton();
            this.SessionFormMenuStrip.SuspendLayout();
            this.SessionFormControlPanel.SuspendLayout();
            this.SessionFormGroupBoxFormMeasurementsJournal.SuspendLayout();
            this.SessionFormGroupBoxDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownHours)).BeginInit();
            this.SessionFormGroupBoxDetectors.SuspendLayout();
            this.SessionFormGroupBoxHeights.SuspendLayout();
            this.SuspendLayout();
            // 
            // SessionFormMenuStrip
            // 
            this.SessionFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMenu});
            this.SessionFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SessionFormMenuStrip.Name = "SessionFormMenuStrip";
            this.SessionFormMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.SessionFormMenuStrip.Size = new System.Drawing.Size(1443, 24);
            this.SessionFormMenuStrip.TabIndex = 0;
            this.SessionFormMenuStrip.Text = "menuStrip1";
            // 
            // MenuMenu
            // 
            this.MenuMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSaveSession,
            this.SessionFormToolStripMenuItemRefreshFormContent});
            this.MenuMenu.Name = "MenuMenu";
            this.MenuMenu.Size = new System.Drawing.Size(53, 20);
            this.MenuMenu.Text = "Меню";
            // 
            // MenuSaveSession
            // 
            this.MenuSaveSession.Name = "MenuSaveSession";
            this.MenuSaveSession.Size = new System.Drawing.Size(201, 22);
            this.MenuSaveSession.Text = "Сохранить сессию";
            //this.MenuSaveSession.Click += new System.EventHandler(this.MenuSaveSession_Click);
            // 
            // SessionFormToolStripMenuItemRefreshFormContent
            // 
            this.SessionFormToolStripMenuItemRefreshFormContent.Name = "SessionFormToolStripMenuItemRefreshFormContent";
            this.SessionFormToolStripMenuItemRefreshFormContent.Size = new System.Drawing.Size(201, 22);
            this.SessionFormToolStripMenuItemRefreshFormContent.Text = "Обновить содержимое";
            //this.SessionFormToolStripMenuItemRefreshFormContent.Click += new System.EventHandler(this.SessionFormToolStripMenuItemRefreshFormContent_Click);
            // 
            // SessionFormStatusStrip
            // 
            this.SessionFormStatusStrip.Location = new System.Drawing.Point(0, 908);
            this.SessionFormStatusStrip.Name = "SessionFormStatusStrip";
            this.SessionFormStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.SessionFormStatusStrip.Size = new System.Drawing.Size(1443, 22);
            this.SessionFormStatusStrip.TabIndex = 1;
            this.SessionFormStatusStrip.Text = "statusStrip1";
            // 
            // SessionFormButtonStart
            // 
            this.SessionFormButtonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonStart.BackColor = System.Drawing.Color.Green;
            this.SessionFormButtonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormButtonStart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStart.Location = new System.Drawing.Point(136, 71);
            this.SessionFormButtonStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormButtonStart.Name = "SessionFormButtonStart";
            this.SessionFormButtonStart.Size = new System.Drawing.Size(88, 34);
            this.SessionFormButtonStart.TabIndex = 5;
            this.SessionFormButtonStart.Text = "Start";
            this.SessionFormButtonStart.UseVisualStyleBackColor = false;
            //this.SessionFormButtonStart.Click += new System.EventHandler(this.SessionFormButtonStart_Click);
            // 
            // SessionFormButtonPause
            // 
            this.SessionFormButtonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonPause.BackColor = System.Drawing.Color.DimGray;
            this.SessionFormButtonPause.Enabled = false;
            this.SessionFormButtonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormButtonPause.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonPause.Location = new System.Drawing.Point(8, 71);
            this.SessionFormButtonPause.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormButtonPause.Name = "SessionFormButtonPause";
            this.SessionFormButtonPause.Size = new System.Drawing.Size(88, 34);
            this.SessionFormButtonPause.TabIndex = 9;
            this.SessionFormButtonPause.Text = "Pause";
            this.SessionFormButtonPause.UseVisualStyleBackColor = false;
            //this.SessionFormButtonPause.Click += new System.EventHandler(this.SessionFormButtonPause_Click);
            // 
            // MenuOptions
            // 
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.Size = new System.Drawing.Size(32, 19);
            this.MenuOptions.Text = "Опции";
            // 
            // SessionFormButtonStop
            // 
            this.SessionFormButtonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonStop.BackColor = System.Drawing.Color.Maroon;
            this.SessionFormButtonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormButtonStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SessionFormButtonStop.Location = new System.Drawing.Point(8, 23);
            this.SessionFormButtonStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormButtonStop.Name = "SessionFormButtonStop";
            this.SessionFormButtonStop.Size = new System.Drawing.Size(88, 32);
            this.SessionFormButtonStop.TabIndex = 11;
            this.SessionFormButtonStop.Text = "Stop";
            this.SessionFormButtonStop.UseVisualStyleBackColor = false;
            //this.SessionFormButtonStop.Click += new System.EventHandler(this.SessionFormButtonStop_Click);
            // 
            // SessionFormButtonClear
            // 
            this.SessionFormButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormButtonClear.BackColor = System.Drawing.Color.White;
            this.SessionFormButtonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormButtonClear.ForeColor = System.Drawing.Color.Black;
            this.SessionFormButtonClear.Location = new System.Drawing.Point(136, 23);
            this.SessionFormButtonClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormButtonClear.Name = "SessionFormButtonClear";
            this.SessionFormButtonClear.Size = new System.Drawing.Size(88, 33);
            this.SessionFormButtonClear.TabIndex = 12;
            this.SessionFormButtonClear.Text = "Clear";
            this.SessionFormButtonClear.UseVisualStyleBackColor = false;
            //this.SessionFormButtonClear.Click += new System.EventHandler(this.SessionFormButtonClear_Click);
            // 
            // SessionFormControlPanel
            // 
            this.SessionFormControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormControlPanel.Controls.Add(this.SessionFormButtonPause);
            this.SessionFormControlPanel.Controls.Add(this.SessionFormButtonStart);
            this.SessionFormControlPanel.Controls.Add(this.SessionFormButtonStop);
            this.SessionFormControlPanel.Controls.Add(this.SessionFormButtonClear);
            this.SessionFormControlPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormControlPanel.Location = new System.Drawing.Point(1201, 680);
            this.SessionFormControlPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormControlPanel.Name = "SessionFormControlPanel";
            this.SessionFormControlPanel.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormControlPanel.Size = new System.Drawing.Size(232, 111);
            this.SessionFormControlPanel.TabIndex = 16;
            this.SessionFormControlPanel.TabStop = false;
            this.SessionFormControlPanel.Text = "Управление измерениями";
            // 
            // SessionFormGroupBoxFormMeasurementsJournal
            // 
            this.SessionFormGroupBoxFormMeasurementsJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormGroupBoxFormMeasurementsJournal.Controls.Add(this.button2);
            this.SessionFormGroupBoxFormMeasurementsJournal.Controls.Add(this.button1);
            this.SessionFormGroupBoxFormMeasurementsJournal.Controls.Add(this.SessionFormlButtonAddAllToJournal);
            this.SessionFormGroupBoxFormMeasurementsJournal.Controls.Add(this.SessionFormlButtonAddSelectedToJournal);
            this.SessionFormGroupBoxFormMeasurementsJournal.Controls.Add(this.SessionFormButtonRemoveSelectedFromJournal);
            this.SessionFormGroupBoxFormMeasurementsJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormGroupBoxFormMeasurementsJournal.Location = new System.Drawing.Point(9, 808);
            this.SessionFormGroupBoxFormMeasurementsJournal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxFormMeasurementsJournal.Name = "SessionFormGroupBoxFormMeasurementsJournal";
            this.SessionFormGroupBoxFormMeasurementsJournal.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxFormMeasurementsJournal.Size = new System.Drawing.Size(1424, 92);
            this.SessionFormGroupBoxFormMeasurementsJournal.TabIndex = 24;
            this.SessionFormGroupBoxFormMeasurementsJournal.TabStop = false;
            this.SessionFormGroupBoxFormMeasurementsJournal.Text = "Управление журналом";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(1120, 21);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(270, 59);
            this.button2.TabIndex = 20;
            this.button2.Text = "Просмотр образцов на детекторах";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(564, 22);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(270, 59);
            this.button1.TabIndex = 19;
            this.button1.Text = "Копировать выделенную запись";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SessionFormlButtonAddAllToJournal
            // 
            this.SessionFormlButtonAddAllToJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SessionFormlButtonAddAllToJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.SessionFormlButtonAddAllToJournal.Location = new System.Drawing.Point(286, 21);
            this.SessionFormlButtonAddAllToJournal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormlButtonAddAllToJournal.Name = "SessionFormlButtonAddAllToJournal";
            this.SessionFormlButtonAddAllToJournal.Size = new System.Drawing.Size(270, 61);
            this.SessionFormlButtonAddAllToJournal.TabIndex = 18;
            this.SessionFormlButtonAddAllToJournal.Text = "Добавить образцы из журналов измерений";
            this.SessionFormlButtonAddAllToJournal.UseVisualStyleBackColor = true;
            //this.SessionFormlButtonAddAllToJournal.Click += new System.EventHandler(this.SessionFormlButtonAddAllToJournal_Click);
            // 
            // SessionFormlButtonAddSelectedToJournal
            // 
            this.SessionFormlButtonAddSelectedToJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SessionFormlButtonAddSelectedToJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.SessionFormlButtonAddSelectedToJournal.Location = new System.Drawing.Point(8, 21);
            this.SessionFormlButtonAddSelectedToJournal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormlButtonAddSelectedToJournal.Name = "SessionFormlButtonAddSelectedToJournal";
            this.SessionFormlButtonAddSelectedToJournal.Size = new System.Drawing.Size(270, 61);
            this.SessionFormlButtonAddSelectedToJournal.TabIndex = 6;
            this.SessionFormlButtonAddSelectedToJournal.Text = "Добавить образы из журналов облучений";
            this.SessionFormlButtonAddSelectedToJournal.UseVisualStyleBackColor = true;
            //this.SessionFormlButtonAddSelectedToJournal.Click += new System.EventHandler(this.SessionFormlButtonAddSelectedToJournal_Click);
            // 
            // SessionFormButtonRemoveSelectedFromJournal
            // 
            this.SessionFormButtonRemoveSelectedFromJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SessionFormButtonRemoveSelectedFromJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.SessionFormButtonRemoveSelectedFromJournal.Location = new System.Drawing.Point(842, 21);
            this.SessionFormButtonRemoveSelectedFromJournal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormButtonRemoveSelectedFromJournal.Name = "SessionFormButtonRemoveSelectedFromJournal";
            this.SessionFormButtonRemoveSelectedFromJournal.Size = new System.Drawing.Size(270, 59);
            this.SessionFormButtonRemoveSelectedFromJournal.TabIndex = 17;
            this.SessionFormButtonRemoveSelectedFromJournal.Text = "Удалить выделенную запись";
            this.SessionFormButtonRemoveSelectedFromJournal.UseVisualStyleBackColor = true;
            //this.SessionFormButtonRemoveSelectedFromJournal.Click += new System.EventHandler(this.SessionFormButtonRemoveSelectedFromJournal_Click);
            // 
            // SessionFormGroupBoxDuration
            // 
            this.SessionFormGroupBoxDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormLabelDays);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormNumericUpDownDays);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormNumericUpDownMinutes);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormLabelMinutes);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormNumericUpDownSeconds);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormLabelSeconds);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormLabelHours);
            this.SessionFormGroupBoxDuration.Controls.Add(this.SessionFormNumericUpDownHours);
            this.SessionFormGroupBoxDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormGroupBoxDuration.Location = new System.Drawing.Point(9, 680);
            this.SessionFormGroupBoxDuration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxDuration.Name = "SessionFormGroupBoxDuration";
            this.SessionFormGroupBoxDuration.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxDuration.Size = new System.Drawing.Size(260, 122);
            this.SessionFormGroupBoxDuration.TabIndex = 23;
            this.SessionFormGroupBoxDuration.TabStop = false;
            this.SessionFormGroupBoxDuration.Text = "Продолжительность измерения выбранных образцов";
            // 
            // SessionFormLabelDays
            // 
            this.SessionFormLabelDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormLabelDays.Location = new System.Drawing.Point(7, 61);
            this.SessionFormLabelDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SessionFormLabelDays.Name = "SessionFormLabelDays";
            this.SessionFormLabelDays.Size = new System.Drawing.Size(56, 24);
            this.SessionFormLabelDays.TabIndex = 16;
            this.SessionFormLabelDays.Text = "Дни";
            this.SessionFormLabelDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SessionFormNumericUpDownDays
            // 
            this.SessionFormNumericUpDownDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormNumericUpDownDays.Location = new System.Drawing.Point(7, 89);
            this.SessionFormNumericUpDownDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormNumericUpDownDays.Name = "SessionFormNumericUpDownDays";
            this.SessionFormNumericUpDownDays.Size = new System.Drawing.Size(56, 22);
            this.SessionFormNumericUpDownDays.TabIndex = 15;
            // 
            // SessionFormNumericUpDownMinutes
            // 
            this.SessionFormNumericUpDownMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormNumericUpDownMinutes.Location = new System.Drawing.Point(133, 89);
            this.SessionFormNumericUpDownMinutes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormNumericUpDownMinutes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SessionFormNumericUpDownMinutes.Name = "SessionFormNumericUpDownMinutes";
            this.SessionFormNumericUpDownMinutes.Size = new System.Drawing.Size(58, 22);
            this.SessionFormNumericUpDownMinutes.TabIndex = 13;
            // 
            // SessionFormLabelMinutes
            // 
            this.SessionFormLabelMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormLabelMinutes.Location = new System.Drawing.Point(133, 61);
            this.SessionFormLabelMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SessionFormLabelMinutes.Name = "SessionFormLabelMinutes";
            this.SessionFormLabelMinutes.Size = new System.Drawing.Size(58, 24);
            this.SessionFormLabelMinutes.TabIndex = 14;
            this.SessionFormLabelMinutes.Text = "Мин.";
            this.SessionFormLabelMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SessionFormNumericUpDownSeconds
            // 
            this.SessionFormNumericUpDownSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormNumericUpDownSeconds.Location = new System.Drawing.Point(198, 89);
            this.SessionFormNumericUpDownSeconds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormNumericUpDownSeconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SessionFormNumericUpDownSeconds.Name = "SessionFormNumericUpDownSeconds";
            this.SessionFormNumericUpDownSeconds.Size = new System.Drawing.Size(52, 22);
            this.SessionFormNumericUpDownSeconds.TabIndex = 9;
            // 
            // SessionFormLabelSeconds
            // 
            this.SessionFormLabelSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormLabelSeconds.Location = new System.Drawing.Point(198, 61);
            this.SessionFormLabelSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SessionFormLabelSeconds.Name = "SessionFormLabelSeconds";
            this.SessionFormLabelSeconds.Size = new System.Drawing.Size(52, 24);
            this.SessionFormLabelSeconds.TabIndex = 10;
            this.SessionFormLabelSeconds.Text = "Сек.";
            this.SessionFormLabelSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SessionFormLabelHours
            // 
            this.SessionFormLabelHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormLabelHours.Location = new System.Drawing.Point(70, 61);
            this.SessionFormLabelHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SessionFormLabelHours.Name = "SessionFormLabelHours";
            this.SessionFormLabelHours.Size = new System.Drawing.Size(56, 24);
            this.SessionFormLabelHours.TabIndex = 12;
            this.SessionFormLabelHours.Text = "Часы";
            this.SessionFormLabelHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SessionFormNumericUpDownHours
            // 
            this.SessionFormNumericUpDownHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormNumericUpDownHours.Location = new System.Drawing.Point(70, 89);
            this.SessionFormNumericUpDownHours.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormNumericUpDownHours.Name = "SessionFormNumericUpDownHours";
            this.SessionFormNumericUpDownHours.Size = new System.Drawing.Size(56, 22);
            this.SessionFormNumericUpDownHours.TabIndex = 11;
            // 
            // SessionFormGroupBoxDetectors
            // 
            this.SessionFormGroupBoxDetectors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormGroupBoxDetectors.Controls.Add(this.SessionFormTableLayoutPanelDetectors);
            this.SessionFormGroupBoxDetectors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormGroupBoxDetectors.Location = new System.Drawing.Point(600, 680);
            this.SessionFormGroupBoxDetectors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxDetectors.Name = "SessionFormGroupBoxDetectors";
            this.SessionFormGroupBoxDetectors.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxDetectors.Size = new System.Drawing.Size(593, 122);
            this.SessionFormGroupBoxDetectors.TabIndex = 25;
            this.SessionFormGroupBoxDetectors.TabStop = false;
            this.SessionFormGroupBoxDetectors.Text = "Сменить детектор для выбранного образца";
            // 
            // SessionFormTableLayoutPanelDetectors
            // 
            this.SessionFormTableLayoutPanelDetectors.AutoScroll = true;
            this.SessionFormTableLayoutPanelDetectors.ColumnCount = 1;
            this.SessionFormTableLayoutPanelDetectors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SessionFormTableLayoutPanelDetectors.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.SessionFormTableLayoutPanelDetectors.Location = new System.Drawing.Point(7, 27);
            this.SessionFormTableLayoutPanelDetectors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormTableLayoutPanelDetectors.Name = "SessionFormTableLayoutPanelDetectors";
            this.SessionFormTableLayoutPanelDetectors.RowCount = 1;
            this.SessionFormTableLayoutPanelDetectors.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SessionFormTableLayoutPanelDetectors.Size = new System.Drawing.Size(578, 79);
            this.SessionFormTableLayoutPanelDetectors.TabIndex = 27;
            // 
            // SessionFormGroupBoxHeights
            // 
            this.SessionFormGroupBoxHeights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionFormGroupBoxHeights.Controls.Add(this.radioButton3);
            this.SessionFormGroupBoxHeights.Controls.Add(this.radioButton2);
            this.SessionFormGroupBoxHeights.Controls.Add(this.radioButton1);
            this.SessionFormGroupBoxHeights.Controls.Add(this.SessionFormsRadioButtonHeight2_5);
            this.SessionFormGroupBoxHeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SessionFormGroupBoxHeights.Location = new System.Drawing.Point(277, 680);
            this.SessionFormGroupBoxHeights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxHeights.Name = "SessionFormGroupBoxHeights";
            this.SessionFormGroupBoxHeights.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormGroupBoxHeights.Size = new System.Drawing.Size(315, 122);
            this.SessionFormGroupBoxHeights.TabIndex = 26;
            this.SessionFormGroupBoxHeights.TabStop = false;
            this.SessionFormGroupBoxHeights.Text = "Установить высоту выбранного образца";
            // 
            // radioButton3
            // 
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radioButton3.Location = new System.Drawing.Point(237, 61);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(69, 24);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "20";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radioButton2.Location = new System.Drawing.Point(152, 61);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 24);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "10";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radioButton1.Location = new System.Drawing.Point(79, 61);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 24);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "5";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // SessionFormsRadioButtonHeight2_5
            // 
            this.SessionFormsRadioButtonHeight2_5.Checked = true;
            this.SessionFormsRadioButtonHeight2_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SessionFormsRadioButtonHeight2_5.Location = new System.Drawing.Point(7, 61);
            this.SessionFormsRadioButtonHeight2_5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SessionFormsRadioButtonHeight2_5.Name = "SessionFormsRadioButtonHeight2_5";
            this.SessionFormsRadioButtonHeight2_5.Size = new System.Drawing.Size(65, 24);
            this.SessionFormsRadioButtonHeight2_5.TabIndex = 0;
            this.SessionFormsRadioButtonHeight2_5.TabStop = true;
            this.SessionFormsRadioButtonHeight2_5.Text = "2.5";
            this.SessionFormsRadioButtonHeight2_5.UseVisualStyleBackColor = true;
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 930);
            this.Controls.Add(this.SessionFormGroupBoxHeights);
            this.Controls.Add(this.SessionFormGroupBoxDetectors);
            this.Controls.Add(this.SessionFormGroupBoxFormMeasurementsJournal);
            this.Controls.Add(this.SessionFormGroupBoxDuration);
            this.Controls.Add(this.SessionFormControlPanel);
            this.Controls.Add(this.SessionFormStatusStrip);
            this.Controls.Add(this.SessionFormMenuStrip);
            this.MainMenuStrip = this.SessionFormMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            this.SessionFormMenuStrip.ResumeLayout(false);
            this.SessionFormMenuStrip.PerformLayout();
            this.SessionFormControlPanel.ResumeLayout(false);
            this.SessionFormGroupBoxFormMeasurementsJournal.ResumeLayout(false);
            this.SessionFormGroupBoxDuration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SessionFormNumericUpDownHours)).EndInit();
            this.SessionFormGroupBoxDetectors.ResumeLayout(false);
            this.SessionFormGroupBoxHeights.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SessionFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuMenu;
        private System.Windows.Forms.StatusStrip SessionFormStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveSession;
        private System.Windows.Forms.Button SessionFormButtonStart;
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
        private System.Windows.Forms.ToolStripDropDownButton HeightDropDownButton;
        private System.Windows.Forms.ToolStripProgressBar MeasurementsProgressBar;
        //private RDataGridView<Measurement> SessionFormAdvancedDataGridViewMeasurementsJournal;
        private System.Windows.Forms.Button SessionFormButtonStop;
        private System.Windows.Forms.Button SessionFormButtonClear;
        private System.Windows.Forms.GroupBox SessionFormControlPanel;
        private System.Windows.Forms.GroupBox SessionFormGroupBoxFormMeasurementsJournal;
        private System.Windows.Forms.Button SessionFormlButtonAddSelectedToJournal;
        private System.Windows.Forms.Button SessionFormButtonRemoveSelectedFromJournal;
        private System.Windows.Forms.GroupBox SessionFormGroupBoxDuration;
        private System.Windows.Forms.Label SessionFormLabelDays;
        private System.Windows.Forms.NumericUpDown SessionFormNumericUpDownDays;
        private System.Windows.Forms.NumericUpDown SessionFormNumericUpDownMinutes;
        private System.Windows.Forms.Label SessionFormLabelMinutes;
        private System.Windows.Forms.NumericUpDown SessionFormNumericUpDownSeconds;
        private System.Windows.Forms.Label SessionFormLabelSeconds;
        private System.Windows.Forms.Label SessionFormLabelHours;
        private System.Windows.Forms.NumericUpDown SessionFormNumericUpDownHours;
        private System.Windows.Forms.GroupBox SessionFormGroupBoxDetectors;
        private System.Windows.Forms.GroupBox SessionFormGroupBoxHeights;
        private System.Windows.Forms.RadioButton SessionFormsRadioButtonHeight2_5;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button SessionFormlButtonAddAllToJournal;
        private System.Windows.Forms.TableLayoutPanel SessionFormTableLayoutPanelDetectors;
        private System.Windows.Forms.ToolStripMenuItem SessionFormToolStripMenuItemRefreshFormContent;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}