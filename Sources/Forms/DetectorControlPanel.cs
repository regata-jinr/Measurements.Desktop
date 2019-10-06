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
        private ISession _session;
        private CircularList<string> _namesArray;
        private IDetector _currentDet;
        public DetectorControlPanel(ref ISession session)
        {
            _session = session;

            InitializeComponent();
            _namesArray = new CircularList<string>(_session.ManagedDetectors.Select(d => d.Name).ToArray());

            foreach (var d in _session.ManagedDetectors)
            {
                d.CurrentSample = _session.SpreadSamples[d.Name][0];
                d.CurrentMeasurement = _session.MeasurementList.Where(m => m.IrradiationId == d.CurrentSample.Id).First();
                d.FillFileInfo();
                d.StatusChanged += DetStatusChangedHandler;
            }

            DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Left + 10, Screen.PrimaryScreen.Bounds.Bottom - Size.Height - 50);

            var time = TimeSpan.FromSeconds(_session.Counts);
            DCPNumericUpDownPresetHours.Value = time.Hours;
            DCPNumericUpDownPresetMinutes.Value = time.Minutes;
            DCPNumericUpDownPresetSeconds.Value = time.Seconds;

            DCPNumericUpDownElapsedHours.Value = time.Hours;
            DCPNumericUpDownElapsedMinutes.Value = time.Minutes;
            DCPNumericUpDownElapsedSeconds.Value = time.Seconds;

            DCPNumericUpDownPresetHours.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetMinutes.ValueChanged += ChangePresetTimeHandler;
            DCPNumericUpDownPresetSeconds.ValueChanged += ChangePresetTimeHandler;

            SourcesInitialize();
         

        }

        private void SourcesInitialize()
        {
            DCPLabelCurrentSrcName.Text = _namesArray.CurrentItem;
            Text = $"Панель управления детектором  {_namesArray.CurrentItem}";
            DCPLabelNextSrcName.Text = _namesArray.NextItem;
            DCPLabelPrevSrcName.Text = _namesArray.PrevItem;
            ProcessManager.SelectDetector(_namesArray.CurrentItem);
            _currentDet = _session.ManagedDetectors.Where(d => d.Name == _namesArray.CurrentItem).First();
            DetStatusChangedHandler(null, EventArgs.Empty);
            DCPComboBoxHeight.SelectedItem = DCPComboBoxHeight.Items.OfType<string>().Where(ch => ch.ToString() == _currentDet.CurrentMeasurement.Height.ToString()).First();
            DCPLabelCurrentSumpleOnCurrentSrc.Text = _currentDet.CurrentSample.ToString();
            DCPLabelCurrentSumpleOnNextSrc.Text = _session.ManagedDetectors.Where(d => d.Name == _namesArray.NextItem).First().CurrentSample.ToString();
            DCPLabelCurrentSumpleOnPrevSrc.Text = _session.ManagedDetectors.Where(d => d.Name == _namesArray.PrevItem).First().CurrentSample.ToString();

        }


        private void RefreshTime()
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

                DCPNumericUpDownElapsedHours.Invoke(new Action(() => { DCPNumericUpDownElapsedHours.Value = time.Hours; }));
                DCPNumericUpDownElapsedMinutes.Invoke(new Action(() => { DCPNumericUpDownElapsedMinutes.Value = time.Minutes; }));
                DCPNumericUpDownElapsedSeconds.Invoke(new Action(() => { DCPNumericUpDownElapsedSeconds.Value = time.Seconds; }));
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
            _currentDet.Clear();

            var time = TimeSpan.FromSeconds(_session.Counts);
            DCPNumericUpDownElapsedHours.Value = time.Hours;
            DCPNumericUpDownElapsedMinutes.Value = time.Minutes;
            DCPNumericUpDownElapsedSeconds.Value = time.Seconds;
        }

        private async void DCPButtonStartPause_Click(object sender, EventArgs e)
        {
            if (_currentDet.Status == DetectorStatus.busy)
            {
                _currentDet.Pause();
                return;
            }

            if (_currentDet.Status == DetectorStatus.ready)
                _currentDet.Start();

            await Task.Run(() => RefreshTime()); 
        }

        private void DetStatusChangedHandler(object sender, EventArgs e)
        {
            if (_currentDet.Status == DetectorStatus.ready && _currentDet.IsPaused)
            {
                DCPButtonStartPause.Text = "Продолжить";
                return;
            }

            if (_currentDet.Status == DetectorStatus.busy && !_currentDet.IsPaused)
            {
                DCPButtonStartPause.Text = "Пауза";
                return;
            }

            if (_currentDet.Status == DetectorStatus.ready && !_currentDet.IsPaused)
            {
                DCPButtonStartPause.Text = "Старт";
                return;
            }
        }

        private void HeightChangedHandler(object sender, EventArgs e)
        {
            _currentDet.CurrentMeasurement.Height = decimal.Parse(DCPComboBoxHeight.Text);
            _currentDet.SetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_T_SGEOMTRY, DCPComboBoxHeight.Text); // height
        }

        private void DCPButtonStop_Click(object sender, EventArgs e)
        {

            //_currentDet.Stop();
        }

        private void DCPButtonSave_Click(object sender, EventArgs e)
        {
            if (_currentDet.Status == DetectorStatus.busy)
                DCPButtonStartPause.PerformClick();

            var dr = saveFileDialogSaveCurrentSpectra.ShowDialog();
            if(dr == DialogResult.OK)
                _currentDet.Save(saveFileDialogSaveCurrentSpectra.FileName);

            DCPButtonStartPause.PerformClick();
        }


        private void ChangePresetTimeHandler(object sender, EventArgs e)
        {
            var time = new TimeSpan((int)DCPNumericUpDownPresetHours.Value, (int)DCPNumericUpDownPresetMinutes.Value, (int)DCPNumericUpDownPresetSeconds.Value);

            _currentDet.SetParameterValue(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL, time.TotalSeconds);
        }
    }
}
