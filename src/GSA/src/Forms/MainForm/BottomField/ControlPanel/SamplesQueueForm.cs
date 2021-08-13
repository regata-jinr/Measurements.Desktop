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

using Regata.Core;
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using RCUW = Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        //TODO: add title to each dgv
        private Form GenerateSamplesQueueForm()
        {
            Form f = new Form();
            f.Name = "SamplesQueueForm";
            f.Size = new System.Drawing.Size(mainForm.Size.Width, 700);
            f.MinimumSize = new System.Drawing.Size(mainForm.Size.Width, 700);

            try
            {
                if (!mainForm.MainRDGV.CurrentDbSet.Local.Any())
                    return null;

                if (!mainForm.MainRDGV.CurrentDbSet.Local.Where(m => !string.IsNullOrEmpty(m.Detector)).Any())
                    return null;

                var _ctrs = new List<ControlsGroupBox>(8);

                foreach (var d in mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.Detector).Distinct().OrderBy(d => d))
                {
                    if (string.IsNullOrEmpty(d)) continue;
                    _ctrs.Add(new ControlsGroupBox(new Control[] { GenerateDataGridView(mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.Detector == d).ToArray(), d) }, false) { Name = d, Text = d } );
                }

                f.Controls.Add(new ControlsGroupBox(_ctrs.ToArray(), false) { Name = "DetsSampleControlBox" } );

                RCUW.Labels.SetControlsLabels(f);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_GEN_QUE_FORM)
                {
                    DetailedText = ex.ToString()
                });
            }
            return f;
        }

        private DataGridView GenerateDataGridView(IEnumerable<Measurement> meas, string dName)
        {
            var dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Name = $"dgv_{dName}";
            dgv.DataSource = meas;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DataBindingComplete += (s, e) => HideRedundantColumnsAndReorderColumns(dgv);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.ReadOnly = true;

            return dgv;
        }

        private void HideRedundantColumnsAndReorderColumns(DataGridView dgv)
        {
            dgv.Columns["Id"].Visible = false;
            dgv.Columns["IrradiationId"].Visible = false;
            dgv.Columns["RegId"].Visible = false;
            dgv.Columns["CountryCode"].Visible = false;
            dgv.Columns["ClientNumber"].Visible = false;
            dgv.Columns["Year"].Visible = false;
            dgv.Columns["SetNumber"].Visible = false;
            dgv.Columns["SetIndex"].Visible = false;
            dgv.Columns["Type"].Visible = false;
            dgv.Columns["AcqMode"].Visible = false;
            dgv.Columns["DateTimeStart"].Visible = false;
            dgv.Columns["Duration"].Visible = false;
            dgv.Columns["Detector"].Visible = false;
            dgv.Columns["DateTimeFinish"].Visible = false;
            dgv.Columns["DeadTime"].Visible = false;
            dgv.Columns["FileSpectra"].Visible = false;
            dgv.Columns["Assistant"].Visible = false;
            dgv.Columns["Note"].Visible = false;
            dgv.Columns["SampleKey"].Visible = false;

            dgv.Columns["SetKey"].DisplayIndex = 0;
            dgv.Columns["SampleNumber"].DisplayIndex = 1;
            dgv.Columns["DiskPosition"].DisplayIndex = 2;
            dgv.Columns["Height"].DisplayIndex = 3;

            RCUW.Labels.SetControlsLabels(dgv);

        }


    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
