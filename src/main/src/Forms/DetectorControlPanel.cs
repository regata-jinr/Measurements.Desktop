/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Regata.Desktop.WinForms.Components;
using Regata.Core.Hardware;
using Regata.Core;
using Regata.Core.DataBase.Models;
using RCM=Regata.Core.Messages;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class DetectorControlPanel : Form
    {
        //TODO: deny decrease preset number below then elapsed time
        private CircularList<string> _namesArray;
        private Detector _currentDet;

        private IEnumerable<Detector> _dets;

        public DetectorControlPanel(IEnumerable<Detector> dets)
        {
            InitializeComponent();
            _dets = dets;
            _namesArray = new CircularList<string>(_dets.Select(d => d.Name).ToArray());

            foreach (var d in dets)
                d.StatusChanged += DetStatusChangedHandler;

            DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Left + 10, Screen.PrimaryScreen.Bounds.Bottom - Size.Height - 50);

            //_session.CurrentSampleChanged += SourcesInitialize;
            DCPNumericUpDownPresetHours.ValueChanged   += ChangePresetTimeHandler;
            DCPNumericUpDownPresetMinutes.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetSeconds.ValueChanged += ChangePresetTimeHandler;

            SourcesInitialize();

        }

        private uint PresetSeconds => _currentDet.PresetRealTime;
        private uint ElapsedSecond => (uint)_currentDet.ElapsedRealTime;
        private uint LeftSeconds => PresetSeconds - ElapsedSecond;

        public async void SourcesInitialize()
        {
            try
            {
                DCPLabelCurrentSrcName.Text = _namesArray.CurrentItem;
                Text = $"Панель управления детектором  {_namesArray.CurrentItem}";
                DCPLabelNextSrcName.Text = _namesArray.NextItem;
                DCPLabelPrevSrcName.Text = _namesArray.PrevItem;
                ProcessManager.SelectDetector(_namesArray.CurrentItem);
                _currentDet = _dets.Where(d => d.Name == _namesArray.CurrentItem).First();
                DetStatusChangedHandler(null, EventArgs.Empty);
                DCPLabelCurrentSumpleOnCurrentSrc.Text = _currentDet.CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnNextSrc.Text = _dets.Where(d => d.Name == _namesArray.NextItem).First().CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnPrevSrc.Text = _dets.Where(d => d.Name == _namesArray.PrevItem).First().CurrentMeasurement.ToString();

                var timePreset = TimeSpan.FromSeconds(PresetSeconds);
                DCPNumericUpDownPresetHours.Value = timePreset.Hours;
                DCPNumericUpDownPresetMinutes.Value = timePreset.Minutes;
                DCPNumericUpDownPresetSeconds.Value = timePreset.Seconds;

                var timeLeft = TimeSpan.FromSeconds(LeftSeconds);
                DCPNumericUpDownElapsedHours.Value = timeLeft.Hours;
                DCPNumericUpDownElapsedMinutes.Value = timeLeft.Minutes;
                DCPNumericUpDownElapsedSeconds.Value = timeLeft.Seconds;
                DCPLabelDeadTimeValue.Text = $"{_currentDet.DeadTime.ToString("f2")}%";
                await Task.Run(() => RefreshTime());
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_INIT_UNREG) { DetailedText = ex.ToString()});
            }
        }

        // FIXME: make async
        private async Task RefreshTime()
        {
            try
            {
                while (LeftSeconds > 0 && _currentDet.Status == DetectorStatus.busy)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    var time = TimeSpan.FromSeconds(LeftSeconds);

                    DCPNumericUpDownElapsedHours?.Invoke(new Action(() => { DCPNumericUpDownElapsedHours.Value = time.Hours; }));
                    DCPNumericUpDownElapsedMinutes?.Invoke(new Action(() => { DCPNumericUpDownElapsedMinutes.Value = time.Minutes; }));
                    DCPNumericUpDownElapsedSeconds?.Invoke(new Action(() => { DCPNumericUpDownElapsedSeconds.Value = time.Seconds; }));

                    DCPLabelDeadTimeValue?.Invoke(new Action(() => { DCPLabelDeadTimeValue.Text = $"{_currentDet.DeadTime.ToString()}%"; }));

                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_REFR_TIME_UNREG) { DetailedText = ex.ToString()});
            }
        }


        private void DCPButtonNextSrc_Click(object sender, EventArgs e)
        {
            _namesArray.Next();
            SourcesInitialize();
        }

        private void DCPButtonPrevSrc_Click(object sender, EventArgs e)
        {
            _namesArray.Prev();
            SourcesInitialize();
        }

        private void DCPButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                _currentDet.Clear();
                var time = TimeSpan.FromSeconds(PresetSeconds);
                DCPNumericUpDownElapsedHours.Value = time.Hours;
                DCPNumericUpDownElapsedMinutes.Value = time.Minutes;
                DCPNumericUpDownElapsedSeconds.Value = time.Seconds;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_CLR_UNREG) { DetailedText = ex.ToString()});
            }
        }


        private void DCPButtonStartPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentDet.Status == DetectorStatus.busy)
                {
                    _currentDet.Pause();
                    return;
                }

                if (_currentDet.Status == DetectorStatus.ready)
                    _currentDet.Start();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_PAUSE_UNREG) { DetailedText = ex.ToString()});
            }
        }

        private async void DetStatusChangedHandler(object sender, EventArgs e)
        {
            try
            {
                if (_currentDet.Status == DetectorStatus.busy)
                {
                    DCPButtonStartPause.Text = "Пауза";
                    await Task.Run(() => RefreshTime());
                }

                if (_currentDet.Status == DetectorStatus.ready && _currentDet.IsPaused)
                    DCPButtonStartPause.Text = "Продолжить";

                if (_currentDet.Status == DetectorStatus.ready && !_currentDet.IsPaused)
                    DCPButtonStartPause.Text = "Старт";
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DET_STAT_CHNG_UNREG) { DetailedText = ex.ToString()});
            }

        }

        private void HeightChangedHandler(object sender, EventArgs e)
        {
            try
            {
                //ISampleInfo si = _currentDet;
                //si.Height = float.Parse(DCPComboBoxHeight.GetItemText(DCPComboBoxHeight.SelectedItem));
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DET_HGHT_CHNG_UNREG) { DetailedText = ex.ToString()});
            }
        }

        private void DCPButtonStop_Click(object sender, EventArgs e)
        {
            try
            {
                _currentDet.Stop();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DET_STOP_UNREG) { DetailedText = ex.ToString()});
            }
        }

        private void DCPButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var itWasBusy = false;
                if (_currentDet.Status == DetectorStatus.busy)
                {
                    _currentDet.Pause();
                    itWasBusy = true;
                }

                var dr = saveFileDialogSaveCurrentSpectra.ShowDialog();
                if (dr == DialogResult.OK)
                    _currentDet.Save(saveFileDialogSaveCurrentSpectra.FileName);

                if (itWasBusy)
                    _currentDet.Start();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DET_SAVE_UNREG) { DetailedText = ex.ToString()});
            }
        }


        private void ChangePresetTimeHandler(object sender, EventArgs e)
        {
            try
            {
                var itWasBusy = false;
                if (_currentDet.Status == DetectorStatus.busy)
                {
                    _currentDet.Pause();
                    itWasBusy = true;
                }

                DCPNumericUpDownPresetHours.Enabled = false;
                DCPNumericUpDownPresetMinutes.Enabled = false;
                DCPNumericUpDownPresetSeconds.Enabled = false;


                var time = new TimeSpan((int)DCPNumericUpDownPresetHours.Value, (int)DCPNumericUpDownPresetMinutes.Value, (int)DCPNumericUpDownPresetSeconds.Value);

                _currentDet.PresetRealTime = (uint)time.TotalSeconds;

                if (LeftSeconds < 0)
                {
                    _currentDet.Clear();
                    return;
                }

                var timeLeft = TimeSpan.FromSeconds(LeftSeconds);
                DCPNumericUpDownElapsedHours.Value = timeLeft.Hours;
                DCPNumericUpDownElapsedMinutes.Value = timeLeft.Minutes;
                DCPNumericUpDownElapsedSeconds.Value = timeLeft.Seconds;

                if (itWasBusy)
                    _currentDet.Start();

                DCPNumericUpDownPresetHours.Enabled = true;
                DCPNumericUpDownPresetMinutes.Enabled = true;
                DCPNumericUpDownPresetSeconds.Enabled = true;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_TIME_CHNG_UNREG) { DetailedText = ex.ToString()});
            }
        }
    }  // public partial class DetectorControlPanel : Form
}      // namespace Regata.Desktop.WinForms.Measurements
