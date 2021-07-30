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

using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private void AssignRecordsMainRDGV<T>(string prop, T val)
        {
            foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
            {
                var m = _regataContext.Measurements.Where(m => m.Id == (int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value).FirstOrDefault();
                if (m == null) continue;
                var setPropValue = m.GetType().GetProperty(prop).GetSetMethod();
                setPropValue.Invoke(m, new object[] { val });
                _regataContext.Update(m);
            }
            _regataContext.SaveChanges();
            mainForm.MainRDGV.Refresh();
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements