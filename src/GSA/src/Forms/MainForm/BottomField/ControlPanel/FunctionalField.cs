/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        public TableLayoutPanel FunctionalLayoutPanel;

        public void InitializeFuntionalField()
        {
            FunctionalLayoutPanel = new TableLayoutPanel();
            FunctionalLayoutPanel.SuspendLayout();
            FunctionalLayoutPanel.ColumnCount = 3;
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            FunctionalLayoutPanel.Dock = DockStyle.Fill;
            FunctionalLayoutPanel.Name = "FunctionalLayoutPanel";
            FunctionalLayoutPanel.TabIndex = 25;
            mainForm.BottomLayoutPanel.Controls.Add(FunctionalLayoutPanel, 1, 0);

            FunctionalLayoutPanel.ResumeLayout(false);
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
