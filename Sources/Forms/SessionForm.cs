using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;
using Measurements.UI.Managers;
using System.Data;

namespace Measurements.UI.Desktop.Forms
{
    //TODO: add tests
    //TODO: add try - catch
    //TODO: rethink exception handler for ui to load async
    //TODO: change detectors should re draw datagrid view
    //TODO: change type should re draw datagrid view
    //TODO: change spread option should re draw datagrid view
    //TODO: reorganize the code

    public partial class SessionForm : Form
    {
        private ISession _session;
        private DataTable _displayedDataTable;
        private DataSet _displayedDataSet;
        private bool _isInitialized;
        private Dictionary<bool, System.Drawing.Color> ConnectionStatusColor;
        public SessionForm(ISession session)
        {
           
            ConnectionStatusColor = new Dictionary<bool, System.Drawing.Color>() { { false, System.Drawing.Color.Red }, { true, System.Drawing.Color.Green } };
            _session = session;
            _isInitialized = true;
            InitializeComponent();

            InitializeDisplayedTable();

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
            
            MeasurementsProgressBar = new ToolStripProgressBar();
            MeasurementsProgressBar.Value = 0;
            MeasurementsProgressBar.Alignment = ToolStripItemAlignment.Right;
            SessionFormStatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            MeasurementsProgressBar.ToolTipText = $"Прогресс измерений по образцам";


            SessionFormMenuStrip.Items.Add(MenuOptions);

            SessionFormListBoxIrrDates.SelectedValueChanged += IrrDateSelectionHandler;
            if (_session.ManagedDetectors.Any())
                SessionFormListBoxIrrDates.SetSelected(0, true);
            
            _countsForm = new CountsForm(_session.Counts);
            _countsForm.SaveCountsEvent += SaveCounts;

            CountsStatusLabel = new ToolStripStatusLabel() { Name = "CountsStatusLabel", Text = $"{_session.Counts}||", ToolTipText = "Продолжительность измерений. Кликните, чтобы изменить." };
            CountsStatusLabel.Click += CountsStatusLabel_Click;
            SessionFormStatusStrip.Items.Add(CountsStatusLabel);

            _session.MeasurementOfSampleDone += MeasurementDoneHandler;

               
            SessionFormStatusStrip.Items.Add(MeasurementsProgressBar);

            _session.SessionComplete += SessionCompleteHandler;

            FillDisplayedTable();


        }


        private void InitializeDisplayedTable()
        {
            _displayedDataSet = new DataSet("DisplayedDataSet");
            _displayedDataTable = new DataTable();
            SessionFormDisplayedDataBindingSource.DataSource = _displayedDataSet;
            SessionFormadvancedDataGridView.SetDoubleBuffered();
            SessionFormadvancedDataGridView.DataSource = SessionFormDisplayedDataBindingSource;

            _displayedDataTable = _displayedDataSet.Tables.Add("DisplayedTable");
            _displayedDataTable.Columns.Add("SetKey", typeof(string));
            _displayedDataTable.Columns.Add("SampleNumber", typeof(string));
            _displayedDataTable.Columns.Add("Container", typeof(int));
            _displayedDataTable.Columns.Add("PositionInContainer", typeof(int));
            _displayedDataTable.Columns.Add("Detector", typeof(string));
            _displayedDataTable.Columns.Add("DiskPosition", typeof(int));
            _displayedDataTable.Columns.Add("DeadTime", typeof(decimal));
            _displayedDataTable.Columns.Add("Height", typeof(decimal));
            _displayedDataTable.Columns.Add("File", typeof(string));
            _displayedDataTable.Columns.Add("Note", typeof(string));


            SessionFormDisplayedDataBindingSource.DataMember = _displayedDataTable.TableName;

            //advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns);

        }

        private void SessionCompleteHandler()
        {
            EnableControls();
            MessageBoxTemplates.InfoSync($"Сессия {_session.Name} завершила измерения всех образцов!");
        }

        private void MeasurementDoneHandler(MeasurementInfo currentMeasurement)
        {
            if (_session.MeasurementList.Any())
                MeasurementsProgressBar.Value++;

            var currentRow = _displayedDataTable.Rows.OfType<DataRow>().Where(dr => dr.ItemArray[0].ToString() == currentMeasurement.SetKey && dr.ItemArray[1].ToString() == currentMeasurement.SampleNumber).First();

            int currentRowIndex = _displayedDataTable.Rows.IndexOf(currentRow);

            //_displayedDataTable.Rows[currentRowIndex].ItemArray[6] = _session.ManagedDetectors.Where(d => d.Name == currentMeasurement.Detector).First().DeadTime;
            //_displayedDataTable.Rows[currentRowIndex].ItemArray[7] = currentMeasurement.Height;
            //_displayedDataTable.Rows[currentRowIndex].ItemArray[8] = currentMeasurement.FileSpectra;
            SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[6].Value = Math.Round(_session.ManagedDetectors.Where(d => d.Name == currentMeasurement.Detector).First().DeadTime, 2);
            SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[7].Value = currentMeasurement.Height;
            SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[8].Value = currentMeasurement.FileSpectra;


            HighlightCurrentSample();

        }

        private void FillDisplayedTable()
        {
            _displayedDataTable.Clear();
            if (_session.MeasurementList.Any())
                MeasurementsProgressBar.Maximum = _session.MeasurementList.Count();

            foreach (var ir in _session.IrradiationList)
            {
                var m = _session.MeasurementList.Where(mi => mi.IrradiationId == ir.Id).First();
                var row = new object[] 
                { 
                    ir.SetKey,
                    ir.SampleNumber,
                    ir.Container.HasValue ? ir.Container.Value : 0,
                    ir.Position.HasValue ? ir.Position.Value : 0,
                    m.Detector, _session.SpreadSamples[m.Detector].IndexOf(ir),
                    Math.Round(_session.ManagedDetectors.Where(d => d.Name == m.Detector).First().DeadTime,2),
                    m.Height.Value,
                    m.FileSpectra ?? "",
                    m.Note ?? "" 
                };

                _displayedDataTable.Rows.Add(row);
            }
        }

        //TODO: add heights list and dead time value
        private void InitializeHeightDropDownButton()
        {
            HeightDropDownButton = new ToolStripDropDownButton();
            HeightDropDownButton.Text = _session.Height.ToString();
            HeightDropDownButton.ToolTipText = "Высота над детектором";
        }

        private void CountsStatusLabel_Click(object sender, EventArgs e)
        {
            _countsForm.Show();
        }

        private void SaveCounts(int counts)
        {
            _session.Counts = counts;
            CountsStatusLabel.Text = $"{counts}||";
        }

        //TODO: test connection should be async in the other case latency is possible
        private void ConnectionStatusHandler()
        {
            ConnectionStatus.BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()];
        }


        private void IrrDateSelectionHandler(object sender, EventArgs eventArgs)
        {
            _session.CurrentIrradiationDate = (DateTime)SessionFormListBoxIrrDates.SelectedItem;
            //SessionFormDataGridViewIrradiations.DataSource = null;
            FillDisplayedTable();
            //SessionFormDataGridViewIrradiations.DataSource = _displayedList;
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
                DetectorsLabelStart = new ToolStripStatusLabel() { Name = "DetectorBegun", Text = "||Детекторы: ", ToolTipText = "Список детекторов подключенных к сессии" };
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

        private void MenuSaveSession_Click(object sender, EventArgs e)
        {
            var savesessionform = new SaveSessionForm(_session.Name);
            savesessionform.SaveSessionEvent += SaveSessionHandler;
            savesessionform.Show();
        }

        private void SaveSessionHandler(string name, bool isPublic)
        {
            var ic = new InfoContext();

            if (ic.Sessions.Where(s => s.Name == name).Any())
            {
                var res = MessageBox.Show($"Сессия с таким именем '{name}' уже существует в базе данных. Вы хотите обновить параметры сессии?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (res == DialogResult.No)
                    return;
            }

            _session.SaveSession(name, isPublic);


            Text = $"Сессия измерений [{_session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";

            SessionControllerSingleton.SessionsInfoListsChangedHaveOccurred();

        }

        private void DisableControls()
        {
            SessionFormListBoxIrrDates.Enabled = false;
            SessionFormMenuStrip.Enabled = false;
            CountsStatusLabel.Enabled = false;

        }

        private void EnableControls()
        {
            SessionFormListBoxIrrDates.Enabled = true;
            SessionFormMenuStrip.Enabled = true;
            CountsStatusLabel.Enabled = true;

        }

        private void SessionFormButtonStart_Click(object sender, EventArgs e)
        {
            if (!_session.MeasurementList.Any() || !_displayedDataTable.Rows.OfType<object>().Any())
            {
                MessageBoxTemplates.ErrorSync("Образцы для измерений не выбраны!");
                return;
            }

            if(!_session.ManagedDetectors.Any())
            {
                MessageBoxTemplates.ErrorSync("Ни один детектор не подключен к сессии");
                return;
            }

            if (_session.Type == null)
            {
                MessageBoxTemplates.ErrorSync("Выбирете тип проводимых измерений!");
                return;
            }

            if (_session.Counts == 0)
            {
                MessageBoxTemplates.ErrorSync("Задайте необходимую продолжительность измерений каждого образца!");
                return;
            }

            DisableControls();
            HighlightCurrentSample();
            _session.StartMeasurements();

        }


        private void HighlightCurrentSample()
        {
            
        }

        private void SessionFormButtonPause_Click(object sender, EventArgs e)
        {
            _session.PauseMeasurements();
            EnableControls();
        }
    }
}
