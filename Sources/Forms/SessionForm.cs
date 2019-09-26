using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;

namespace Measurements.UI.Desktop.Forms
{
    //TODO: add test
    //TODO: add try - catch
    //TODO: rethink exception handler for ui to load async
    public partial class SessionForm : Form
    {
        private ISession _session;
        private bool _isInitialized;
        private Dictionary<bool, System.Drawing.Color> ConnectionStatusColor;
        public SessionForm(ISession session)
        {
            ConnectionStatusColor = new Dictionary<bool, System.Drawing.Color>() { { false, System.Drawing.Color.Red }, { true, System.Drawing.Color.Green } };
            _session = session;
            _isInitialized = true;
            InitializeComponent();
            Text = $"Сессия измерений [{session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
            SessionFormStatusStrip.ShowItemToolTips = true;
            ConnectionStatus = new ToolStripStatusLabel() { Name = "ConnectionStatusItem", Text = " ", ToolTipText = "Состояние соединения с БД", BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()] };
            SessionControllerSingleton.ConnectionFallen += ConnectionStatusHandler;
            SessionFormStatusStrip.Items.Add(ConnectionStatus);

            InitializeDetectorDropDownItems();
            SessionControllerSingleton.AvailableDetectorsListHasChanged += InitializeDetectorDropDownItems;
            InitializeTypeDropDownItems();

            CountsOptionsItem = new EnumItem(Enum.GetNames(typeof(CanberraDeviceAccessLib.AcquisitionModes)), "Режим набора");
            InitializeOptionsMenu(CountsOptionsItem, SetCountMode);
            CountsOptionsItem.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.CountMode.ToString()).First().PerformClick();

            SpreadOptionsItem = new EnumItem(Enum.GetNames(typeof(SpreadOptions)), "Режим распределения образцов");
            InitializeOptionsMenu(SpreadOptionsItem, SetSpreadMode);
            SpreadOptionsItem.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.SpreadOption.ToString()).First().PerformClick();

            SessionFormMenuStrip.Items.Add(MenuOptions);

            SessionFormListBoxIrrDates.SelectedValueChanged += IrrDateSelectionHandler;

        }

        //TODO: test connection should be async in the other case latency is possible
        private void ConnectionStatusHandler()
        {
            ConnectionStatus.BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()];
        }


        private void IrrDateSelectionHandler(object sender, EventArgs eventArgs)
        {
            _session.CurrentIrradiationDate = (DateTime)SessionFormListBoxIrrDates.SelectedItem;
            SessionFormDataGridViewIrradiations.DataSource = null;
            SessionFormDataGridViewIrradiations.DataSource = _session.IrradiationList;
        }

        private void InitializeTypeDropDownItems()
        {
            var detEndPostition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd) + 1;
            var typesItems = new EnumItem(Session.MeasurementTypes, "Тип");
            typesItems.DropDownItemClick += SetType;
            SessionFormMenuStrip.Items.Add(typesItems.EnumMenuItem);
            SessionFormStatusStrip.Items.Insert(detEndPostition, typesItems.EnumStatusLabel);
            if (!string.IsNullOrEmpty(_session.Type))
                typesItems.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.Type).First().PerformClick();
        }

        private void InitializeOptionsMenu(EnumItem OptionsItem, Action<string> del)
        {
            OptionsItem.DropDownItemClick += del;
            MenuOptions.DropDownItems.Add(OptionsItem.EnumMenuItem);
            SessionFormStatusStrip.Items.Add(OptionsItem.EnumStatusLabel);
        }

        private void SetType(string type)
        {
            _session.Type = type;
            SessionFormListBoxIrrDates.DataSource = null; 
            SessionFormListBoxIrrDates.DataSource = _session.IrradiationDateList;
        }
        private void SetCountMode(string option)
        {
            CanberraDeviceAccessLib.AcquisitionModes am;
            if (Enum.TryParse(option, out am))
                _session.CountMode = am;
            else
                _session.CountMode = CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime;
        }

        private void SetSpreadMode(string option)
        {
            SpreadOptions so;
            if (Enum.TryParse(option, out so))
                _session.SpreadOption = so;
            else
                _session.SpreadOption = SpreadOptions.container;
        }


        private void InitializeDetectorDropDownItems()
        {
            var allDetectors = SessionControllerSingleton.AvailableDetectors.Select(da => da).Union(_session.ManagedDetectors.Select(dm => dm)).OrderBy(md => md.Name).ToArray();

            if (_isInitialized)
            {
                DetectorsDropDownMenu = new ToolStripMenuItem() { Text = "Детекторы", CheckOnClick = false };
                DetectorsLabelStart = new ToolStripStatusLabel() { Name = "DetectorBegan", Text = "||Детекторы: ", ToolTipText = "Список детекторов подключенных к сессии" };
                DetectorsLabelEnd = new ToolStripStatusLabel() { Name = "DetectorEnd", Text = "||" };
            }
            else
            {
                DetectorsDropDownMenu.DropDownItems.Clear();
                RemoveDetectorsFromStatusLabel();
            }

            SessionFormStatusStrip.Items.Insert(1, DetectorsLabelStart);
            SessionFormStatusStrip.Items.Insert(2, DetectorsLabelEnd);

            var detectorsPosition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd);

            foreach (var det in allDetectors)
            {
                var cDet = det;
                var detItem = new DetectorItem(ref _session, ref cDet);
                DetectorsDropDownMenu.DropDownItems.Add(detItem.DetectorMenuItem);
                if (_session.ManagedDetectors.Contains(det))
                {
                    SessionFormStatusStrip.Items.Insert(SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd), detItem.DetectorStatusLabel);
                    DetectorsDropDownMenu.DropDownItems.OfType<ToolStripMenuItem>().Last().Checked = true;
                }
            }

            if (_session.ManagedDetectors.Count == 0)
            {
                SessionFormStatusStrip.Items.Insert(detectorsPosition, new ToolStripStatusLabel() { Name = "DetectorEmpty", Text = "---", ToolTipText = "К сессии не подключен ни один детектор" });
                detectorsPosition++;
            }

            SessionFormStatusStrip.Items.Insert(detectorsPosition + _session.ManagedDetectors.Count, DetectorsLabelEnd);

            if (_isInitialized)
                SessionFormMenuStrip.Items.Add(DetectorsDropDownMenu);

            //TODO: detectors menu close after click
            //      such implementation show menu on inactive session when available detectors have occurred
            //if (!_isInitialized)
            //    DetectorsDropDownMenu.ShowDropDown();

            _isInitialized = false;
        }

        private void RemoveDetectorsFromStatusLabel()
        {
            for (var i = SessionFormStatusStrip.Items.Count - 1; i >= 0; --i)
            {
                if (SessionFormStatusStrip.Items[i].Name.Contains("Detector"))
                    SessionFormStatusStrip.Items.RemoveAt(i);
            }
        }


    }
}
