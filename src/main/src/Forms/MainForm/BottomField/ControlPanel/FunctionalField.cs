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
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            FunctionalLayoutPanel.Dock = DockStyle.Fill;
            FunctionalLayoutPanel.Name = "FunctionalLayoutPanel";
            FunctionalLayoutPanel.TabIndex = 25;
            mainForm.BottomLayoutPanel.Controls.Add(FunctionalLayoutPanel, 1, 0);

            FunctionalLayoutPanel.ResumeLayout(false);

        }



        //rf.buttonAddAllSamples.Click += (s, e) => 
        //{
        //    var sr = rf.TabsPane.SelectedRowsFirstDGV;
        //    if (sr.Count <= 0) return;
        //    var ind = rf.TabsPane.Pages.IndexOf(rf.TabsPane.ActiveTabPage);

        //    CreateNewMeasurementsRegister();

        //    using (var r = new RegataContext())
        //    {
        //        var meas = new List<Measurement>();

        //        foreach (DataGridViewRow row in rf.TabsPane[ind,1].Rows)
        //        {
        //            meas.Add(
        //                    new Measurement()
        //                    {
        //                        RegId         = CurrentMeasurementsRegister.Id,
        //                        Type          =  CurrentMeasurementsRegister.Type,
        //                        IrradiationId = (int)row.Cells[6].Value,
        //                        CountryCode   = row.Cells[0].Value.ToString(),
        //                        ClientNumber  = row.Cells[1].Value.ToString(),
        //                        Year          = row.Cells[2].Value.ToString(),
        //                        SetNumber     = row.Cells[3].Value.ToString(),
        //                        SetIndex      = row.Cells[4].Value.ToString(),
        //                        SampleNumber  = row.Cells[5].Value.ToString()
        //                    }
        //                    );
        //        }
        //        r.Measurements.AddRange(meas);
        //        r.SaveChanges();
        //        rf.MainRDGV.DataSource = meas;
        //    }


        //};

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
