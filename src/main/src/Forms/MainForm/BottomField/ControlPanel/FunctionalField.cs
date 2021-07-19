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

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

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

    } //public static class SessionFormInit
}     // namespace Regata.Desktop.WinForms.Measurements
