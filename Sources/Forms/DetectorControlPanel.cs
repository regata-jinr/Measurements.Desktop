using System;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Measurements.Core;
using Measurements.UI.Desktop.Components;
using Measurements.UI.Managers;

namespace Measurements.UI.Desktop.Forms
{
    public partial class DetectorControlPanel : Form
    {
        //TODO: deny decrease preset number below then elapsed time
        private ISession _session;
        private CircularList<string> _namesArray;
        private IDetector _currentDet;
        public DetectorControlPanel(ref ISession session)
        {
            _session = session;

            InitializeComponent();
            _namesArray = new CircularList<string>(_session.ManagedDetectors.Select(d => d.Name).ToArray());

            foreach (var d in _session.ManagedDetectors)
                d.StatusChanged += DetStatusChangedHandler;

            DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Left + 10, Screen.PrimaryScreen.Bounds.Bottom - Size.Height - 50);

            //_session.CurrentSampleChanged += SourcesInitialize;
            DCPNumericUpDownPresetHours.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetMinutes.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetSeconds.ValueChanged += ChangePresetTimeHandler;

            SourcesInitialize();

        }

        private async void SourcesInitialize()
        {
            try
            {
                DCPLabelCurrentSrcName.Text = _namesArray.CurrentItem;
                Text = $"Панель управления детектором  {_namesArray.CurrentItem}";
                DCPLabelNextSrcName.Text = _namesArray.NextItem;
                DCPLabelPrevSrcName.Text = _namesArray.PrevItem;
                ProcessManager.SelectDetector(_namesArray.CurrentItem);
                _currentDet = _session.ManagedDetectors.Where(d => d.Name == _namesArray.CurrentItem).First();
                DetStatusChangedHandler(null, EventArgs.Empty);
                //DCPComboBoxHeight.SelectedItem = DCPComboBoxHeight.Items.OfType<string>().Where(ch => (Math.Abs(decimal.Parse(ch) - _currentDet.CurrentMeasurement.Height.Value) < 0.5m)).First();
                DCPLabelCurrentSumpleOnCurrentSrc.Text = _currentDet.CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnNextSrc.Text = _session.ManagedDetectors.Where(d => d.Name == _namesArray.NextItem).First().CurrentMeasurement.ToString();
                DCPLabelCurrentSumpleOnPrevSrc.Text = _session.ManagedDetectors.Where(d => d.Name == _namesArray.PrevItem).First().CurrentMeasurement.ToString();
                int PresetSeconds = int.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL).ToString());
                int ElapsedSecond = (int)Math.Round(decimal.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL).ToString()),0);
                int LeftSeconds = PresetSeconds - ElapsedSecond;

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
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }
        }

        private void RefreshTime()
        {
            try
            {
                int PresetSeconds = int.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL).ToString());
                int ElapsedSecond = (int)Math.Round(decimal.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL).ToString()),0);
                int LeftSeconds = PresetSeconds - ElapsedSecond;


                while (LeftSeconds > 0 && _currentDet.Status == DetectorStatus.busy)
                {
                    System.Threading.Thread.Sleep(1000);
                    PresetSeconds = int.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL).ToString());
                    ElapsedSecond = (int)Math.Round(decimal.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL).ToString()), 0);
                    LeftSeconds = PresetSeconds - ElapsedSecond;

                    var time = TimeSpan.FromSeconds(LeftSeconds);

                    DCPNumericUpDownElapsedHours?.Invoke(new Action(() => { DCPNumericUpDownElapsedHours.Value = time.Hours; }));
                    DCPNumericUpDownElapsedMinutes?.Invoke(new Action(() => { DCPNumericUpDownElapsedMinutes.Value = time.Minutes; }));
                    DCPNumericUpDownElapsedSeconds?.Invoke(new Action(() => { DCPNumericUpDownElapsedSeconds.Value = time.Seconds; }));

                    DCPLabelDeadTimeValue?.Invoke(new Action(() => { DCPLabelDeadTimeValue.Text = $"{_currentDet.DeadTime.ToString()}%"; }));

                }
            }
            catch
            { }
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
            _currentDet.Clear();
            int PresetSeconds = int.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL).ToString());
            var time = TimeSpan.FromSeconds(PresetSeconds);
            DCPNumericUpDownElapsedHours.Value = time.Hours;
            DCPNumericUpDownElapsedMinutes.Value = time.Minutes;
            DCPNumericUpDownElapsedSeconds.Value = time.Seconds;
        }


        private void DCPButtonStartPause_Click(object sender, EventArgs e)
        {
            if (_currentDet.Status == DetectorStatus.busy)
            {
                _currentDet.Pause();
                return;
            }

            if (_currentDet.Status == DetectorStatus.ready)
                _currentDet.Start();
        }

        private async void DetStatusChangedHandler(object sender, EventArgs e)
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

        private void HeightChangedHandler(object sender, EventArgs e)
        {
            _currentDet.CurrentMeasurement.Height = decimal.Parse(DCPComboBoxHeight.Text);
            _currentDet.SetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_T_SGEOMTRY, DCPComboBoxHeight.Text); // height
            _currentDet.AddEfficiencyCalibrationFile(_currentDet.CurrentMeasurement.Height.Value);
        }

        private void DCPButtonStop_Click(object sender, EventArgs e)
        {
            _currentDet.Stop();
        }

        private void DCPButtonSave_Click(object sender, EventArgs e)
        {
            var itWasBusy = false;
            if (_currentDet.Status == DetectorStatus.busy)
            {
                _currentDet.Pause();
                itWasBusy = true;
            }

            var dr = saveFileDialogSaveCurrentSpectra.ShowDialog();
            if(dr == DialogResult.OK)
                _currentDet.Save(saveFileDialogSaveCurrentSpectra.FileName);

            if (itWasBusy)
                _currentDet.Start();

        }


        private void ChangePresetTimeHandler(object sender, EventArgs e)
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

            _currentDet.SetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL, time.TotalSeconds);

            int PresetSeconds = (int)time.TotalSeconds;
            int ElapsedSecond = (int)Math.Round(decimal.Parse(_currentDet.GetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL).ToString()),0);
            int LeftSeconds = PresetSeconds - ElapsedSecond;

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
    }
}
