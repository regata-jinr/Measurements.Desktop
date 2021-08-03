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
using System.Drawing;
using System.Windows.Forms;
using Regata.Core.Hardware;
using Regata.Core;
using RCM=Regata.Core.Messages;
using Regata.Core.Collections;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class DetectorControlPanel : Form
    {
        //TODO: deny decrease preset number below then elapsed time
        private CircleArray<Detector> _dets;


        public DetectorControlPanel(IEnumerable<Detector> dets)
        {
            InitializeComponent();
            _dets = new CircleArray<Detector>(dets);

            foreach (var d in dets)
                d.StatusChanged += DetStatusChangedHandler;

            DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Left + 10, Screen.PrimaryScreen.Bounds.Bottom - Size.Height - 50);

            DCPNumericUpDownPresetHours.ValueChanged   += ChangePresetTimeHandler;
            DCPNumericUpDownPresetMinutes.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetSeconds.ValueChanged += ChangePresetTimeHandler;

            Disposed += DetectorControlPanel_Disposed;

            Load += DetectorControlPanel_Load;


        }

        private async void DetectorControlPanel_Load(object sender, EventArgs e)
        {
            await SourcesInitialize();
        }

        private async void DetectorControlPanel_Disposed(object sender, EventArgs e)
        {
                await Detector.CloseMvcgAsync();
        }

        private uint PresetSeconds => _dets.Current.PresetRealTime;
        private uint ElapsedSecond => (uint)_dets.Current.ElapsedRealTime;
        private uint LeftSeconds => PresetSeconds - ElapsedSecond;

        public async Task SourcesInitialize()
        {
            try
            {
                DCPLabelCurrentSrcName.Text = _dets.Current.Name;
                Text = $"Панель управления детектором  {_dets.Current.Name}";
                DCPLabelNextSrcName.Text = _dets.Next.Name;
                DCPLabelPrevSrcName.Text = _dets.Prev.Name;
                await Detector.SelectDetectorAsync(_dets.Current.Name);
                DetStatusChangedHandler(null, EventArgs.Empty);
                DCPLabelCurrentSumpleOnCurrentSrc.Text = _dets.Current.CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnNextSrc.Text = _dets.Next.CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnPrevSrc.Text = _dets.Prev.CurrentMeasurement.ToString();

                var timePreset = TimeSpan.FromSeconds(PresetSeconds);
                DCPNumericUpDownPresetHours.Value = timePreset.Hours;
                DCPNumericUpDownPresetMinutes.Value = timePreset.Minutes;
                DCPNumericUpDownPresetSeconds.Value = timePreset.Seconds;

                var timeLeft = TimeSpan.FromSeconds(LeftSeconds);
                DCPNumericUpDownElapsedHours.Value = timeLeft.Hours;
                DCPNumericUpDownElapsedMinutes.Value = timeLeft.Minutes;
                DCPNumericUpDownElapsedSeconds.Value = timeLeft.Seconds;
                DCPLabelDeadTimeValue.Text = $"{_dets.Current.DeadTime.ToString("f2")}%";

                DCPComboBoxHeight.SelectedItem = _dets.Current.CurrentMeasurement.Height.Value;

                await Task.Run(() => RefreshTime());
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_INIT_UNREG) { DetailedText = ex.ToString()});
            }
        }

        private async Task RefreshTime()
        {
            try
            {
                while (LeftSeconds > 0 && _dets.Current.Status == DetectorStatus.busy)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    var time = TimeSpan.FromSeconds(LeftSeconds);

                    DCPNumericUpDownElapsedHours?.Invoke(  new Action(() => { DCPNumericUpDownElapsedHours.Value = time.Hours; }));
                    DCPNumericUpDownElapsedMinutes?.Invoke(new Action(() => { DCPNumericUpDownElapsedMinutes.Value = time.Minutes; }));
                    DCPNumericUpDownElapsedSeconds?.Invoke(new Action(() => { DCPNumericUpDownElapsedSeconds.Value = time.Seconds; }));

                    DCPLabelDeadTimeValue?.Invoke(new Action(() => { DCPLabelDeadTimeValue.Text = $"{_dets.Current.DeadTime.ToString()}%"; }));

                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_DCP_REFR_TIME_UNREG) { DetailedText = ex.ToString()});
            }
        }


        private async void DCPButtonNextSrc_Click(object sender, EventArgs e)
        {
            _dets.MoveForward();
            await SourcesInitialize();
        }

        private async void DCPButtonPrevSrc_Click(object sender, EventArgs e)
        {
            _dets.MoveBack();
            await SourcesInitialize();
        }

        private void DCPButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                _dets.Current.Clear();
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
                if (_dets.Current.Status == DetectorStatus.busy)
                {
                    _dets.Current.Pause();
                    return;
                }

                if (_dets.Current.Status == DetectorStatus.ready)
                    _dets.Current.Start();
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
                if (_dets.Current.Status == DetectorStatus.busy)
                {
                    DCPButtonStartPause.Text = "Пауза";
                    await Task.Run(() => RefreshTime());
                }

                if (_dets.Current.Status == DetectorStatus.ready && _dets.Current.IsPaused)
                    DCPButtonStartPause.Text = "Продолжить";

                if (_dets.Current.Status == DetectorStatus.ready && !_dets.Current.IsPaused)
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
                _dets.Current.Sample.Height = (float)DCPComboBoxHeight.SelectedItem;
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
                _dets.Current.Stop();
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
                if (_dets.Current.Status == DetectorStatus.busy)
                {
                    _dets.Current.Pause();
                    itWasBusy = true;
                }

                var dr = saveFileDialogSaveCurrentSpectra.ShowDialog();
                if (dr == DialogResult.OK)
                    _dets.Current.Save(saveFileDialogSaveCurrentSpectra.FileName);

                if (itWasBusy)
                    _dets.Current.Start();
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
                if (_dets.Current.Status == DetectorStatus.busy)
                {
                    _dets.Current.Pause();
                    itWasBusy = true;
                }

                DCPNumericUpDownPresetHours.Enabled = false;
                DCPNumericUpDownPresetMinutes.Enabled = false;
                DCPNumericUpDownPresetSeconds.Enabled = false;


                var time = new TimeSpan((int)DCPNumericUpDownPresetHours.Value, (int)DCPNumericUpDownPresetMinutes.Value, (int)DCPNumericUpDownPresetSeconds.Value);

                _dets.Current.PresetRealTime = (uint)time.TotalSeconds;

                if (LeftSeconds < 0)
                {
                    _dets.Current.Clear();
                    _dets.Current.Pause();
                    return;
                }

                var timeLeft = TimeSpan.FromSeconds(LeftSeconds);
                DCPNumericUpDownElapsedHours.Value = timeLeft.Hours;
                DCPNumericUpDownElapsedMinutes.Value = timeLeft.Minutes;
                DCPNumericUpDownElapsedSeconds.Value = timeLeft.Seconds;

                if (itWasBusy)
                    _dets.Current.Start();

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
