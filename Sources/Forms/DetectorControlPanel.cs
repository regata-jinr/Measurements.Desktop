using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            DCPLabelPresetTime.Text = _session.Counts.ToString();
            DCPLabelElapsedTime.Text = _session.Counts.ToString();
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
        }

        private void DCPButtonStartPause_Click(object sender, EventArgs e)
        {
            if (_currentDet.Status == DetectorStatus.busy)
                _currentDet.Pause();

            if (_currentDet.Status == DetectorStatus.ready)
                _currentDet.Start();


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

    }
}
