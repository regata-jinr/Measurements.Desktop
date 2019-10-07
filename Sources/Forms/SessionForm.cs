using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;
using Measurements.UI.Managers;
using System.Data;
using System.Diagnostics;

namespace Measurements.UI.Desktop.Forms
{
    //TODO: add tests
    //TODO: rethink exception handler for ui to load async
    //TODO: change detectors should re draw datagrid view
    //TODO: change type should re draw datagrid view
    //TODO: reorganize the code

    public partial class SessionForm : Form
    {
        private ISession _session;
        private bool _isInitialized;
        private Dictionary<bool, System.Drawing.Color> ConnectionStatusColor;
        public SessionForm(ISession session)
        {

            try
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
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }

        }


        private void InitializeDisplayedTable()
        {
            try
            {
                SessionFormadvancedDataGridView.SetDoubleBuffered();
                SessionFormadvancedDataGridView.DataSource = SessionFormDisplayedDataBindingSource;


                DataGridViewColumn setKeyCol = new DataGridViewColumn() { Name = "SetKey", ValueType = typeof(string)};
                DataGridViewColumn SampleNumberCol = new DataGridViewColumn() { Name = "SampleNumber", ValueType = typeof(string)};
                DataGridViewColumn ContainerCol = new DataGridViewColumn() { Name = "Container", ValueType = typeof(int)};
                DataGridViewColumn PositionInContainerCol = new DataGridViewColumn() { Name = "PositionInContainer", ValueType = typeof(int)};
                DataGridViewColumn DiskPositionCol = new DataGridViewColumn() { Name = "DiskPosition", ValueType = typeof(int)};
                DataGridViewColumn DeadTimeCol = new DataGridViewColumn() { Name = "DeadTime", ValueType = typeof(decimal)};
                DataGridViewColumn FileCol = new DataGridViewColumn() { Name = "File", ValueType = typeof(string)};
                DataGridViewColumn NoteCol = new DataGridViewColumn() { Name = "Note", ValueType = typeof(string), ReadOnly = false };

                DataGridViewComboBoxColumn detectorListColumn = new DataGridViewComboBoxColumn();
                detectorListColumn.Name = "Detector";
                detectorListColumn.ValueType = typeof(string);
                detectorListColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                detectorListColumn.DataSource = _session.ManagedDetectors.Select(d => d.Name);

                DataGridViewComboBoxColumn heightListColumn = new DataGridViewComboBoxColumn();
                detectorListColumn.Name = "Height";
                detectorListColumn.ValueType = typeof(decimal);
                detectorListColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                detectorListColumn.DataSource = new List<decimal>(){ 2.5m, 5m, 10m, 20m};


                SessionFormadvancedDataGridView.Columns.Add(setKeyCol);
                SessionFormadvancedDataGridView.Columns.Add(SampleNumberCol);
                SessionFormadvancedDataGridView.Columns.Add(ContainerCol);
                SessionFormadvancedDataGridView.Columns.Add(PositionInContainerCol);
                SessionFormadvancedDataGridView.Columns.Add(detectorListColumn);
                SessionFormadvancedDataGridView.Columns.Add(DiskPositionCol);
                SessionFormadvancedDataGridView.Columns.Add(DeadTimeCol);
                SessionFormadvancedDataGridView.Columns.Add(heightListColumn);
                SessionFormadvancedDataGridView.Columns.Add(FileCol);
                SessionFormadvancedDataGridView.Columns.Add(NoteCol);
            }
            //advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns);
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }
        }

        private void SessionCompleteHandler()
        {
            try
            {
                EnableControls();
                MessageBoxTemplates.InfoSync($"Сессия {_session.Name} завершила измерения всех образцов!");
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

        private void MeasurementDoneHandler(MeasurementInfo currentMeasurement)
        {
            try
            {
                if (_session.MeasurementList.Any())
                    MeasurementsProgressBar.Value++;

                var currentRow = SessionFormadvancedDataGridView.Rows.OfType<DataGridViewRow>().Where(dr => dr.Cells[0].ToString() == currentMeasurement.SetKey && dr.Cells[1].ToString() == currentMeasurement.SampleNumber).First();

                int currentRowIndex = SessionFormadvancedDataGridView.Rows.IndexOf(currentRow);

                //_displayedDataTable.Rows[currentRowIndex].ItemArray[6] = _session.ManagedDetectors.Where(d => d.Name == currentMeasurement.Detector).First().DeadTime;
                //_displayedDataTable.Rows[currentRowIndex].ItemArray[7] = currentMeasurement.Height;
                //_displayedDataTable.Rows[currentRowIndex].ItemArray[8] = currentMeasurement.FileSpectra;
                SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[6].Value = Math.Round(_session.ManagedDetectors.Where(d => d.Name == currentMeasurement.Detector).First().DeadTime, 2);
                SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[7].Value = currentMeasurement.Height;
                SessionFormadvancedDataGridView.Rows[currentRowIndex].Cells[8].Value = currentMeasurement.FileSpectra;


                HighlightCurrentSample();
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

        private void FillDisplayedTable()
        {
            try
            {
                SessionFormadvancedDataGridView.Rows.Clear();
                if (_session.MeasurementList.Any())
                    MeasurementsProgressBar.Maximum = _session.MeasurementList.Count();

                foreach (var ir in _session.IrradiationList)
                {
                    var m = _session.MeasurementList.Where(mi => mi.IrradiationId == ir.Id).First();
                    if (m.Detector == null)
                        throw new ArgumentException("Не выбран ни один детектор!");
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

                    SessionFormadvancedDataGridView.Rows.Add(row);
                }
            }
            catch (ArgumentException ae)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ae,
                    Level = Core.Handlers.ExceptionLevel.Warn
                });
            }
            catch (Exception e)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = e,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
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
            try
            {
                _countsForm.Show();
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

        private void SaveCounts(int counts)
        {
            try
            {
                _session.Counts = counts;
                CountsStatusLabel.Text = $"{counts}||";
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

        //TODO: test connection should be async in the other case latency is possible
        private void ConnectionStatusHandler()
        {
            try
            {
                ConnectionStatus.BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()];
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


        private void IrrDateSelectionHandler(object sender, EventArgs eventArgs)
        {
            try
            {
                if (SessionFormListBoxIrrDates.SelectedItem == null) return;
                _session.CurrentIrradiationDate = (DateTime)SessionFormListBoxIrrDates.SelectedItem;
                FillDisplayedTable();
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

        private void InitializeTypeDropDownItems()
        {
            try
            {
                var detEndPostition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd) + 1;
                var typesItems = new EnumItem(Session.MeasurementTypes, "Тип");
                typesItems.DropDownItemClick += SetType;
                SessionFormMenuStrip.Items.Add(typesItems.EnumMenuItem);
                SessionFormStatusStrip.Items.Insert(detEndPostition, typesItems.EnumStatusLabel);
                if (!string.IsNullOrEmpty(_session.Type))
                    typesItems.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.Type).First().PerformClick();
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

        private void InitializeOptionsMenu(EnumItem OptionsItem, Action<string> del)
        {
            try
            {
                OptionsItem.DropDownItemClick += del;
                MenuOptions.DropDownItems.Add(OptionsItem.EnumMenuItem);
                SessionFormStatusStrip.Items.Add(OptionsItem.EnumStatusLabel);
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

        private void SetType(string type)
        {
            try
            {
                _session.Type = type;

                if (type == "SLI")
                {
                    SessionFormadvancedDataGridView.Columns[2].Visible = false;
                    SessionFormadvancedDataGridView.Columns[3].Visible = false;
                }
                else
                {
                    SessionFormadvancedDataGridView.Columns[2].Visible = true;
                    SessionFormadvancedDataGridView.Columns[3].Visible = true;
                }

                SessionFormListBoxIrrDates.DataSource = null;
                SessionFormListBoxIrrDates.DataSource = _session.IrradiationDateList;
                FillDisplayedTable();
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
        private void SetCountMode(string option)
        {
            try
            {
                CanberraDeviceAccessLib.AcquisitionModes am;
                if (Enum.TryParse(option, out am))
                    _session.CountMode = am;
                else
                    _session.CountMode = CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime;
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

        private void SetSpreadMode(string option)
        {
            try
            {
                SpreadOptions so;
                if (Enum.TryParse(option, out so))
                    _session.SpreadOption = so;
                else
                    _session.SpreadOption = SpreadOptions.container;
                FillDisplayedTable();
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


        private void InitializeDetectorDropDownItems()
        {
            try
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

                _isInitialized = false;
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

        private void RemoveDetectorsFromStatusLabel()
        {
            try
            {
                for (var i = SessionFormStatusStrip.Items.Count - 1; i >= 0; --i)
                {
                    if (SessionFormStatusStrip.Items[i].Name.Contains("Detector"))
                        SessionFormStatusStrip.Items.RemoveAt(i);
                }
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

        private void MenuSaveSession_Click(object sender, EventArgs e)
        {
            try
            {
                var savesessionform = new SaveSessionForm(_session.Name);
                savesessionform.SaveSessionEvent += SaveSessionHandler;
                savesessionform.Show();
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

        private void SaveSessionHandler(string name, bool isPublic)
        {
            try
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
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }

        }

        private void DisableControls()
        {
            try
            {
                SessionFormListBoxIrrDates.Enabled = false;
                SessionFormMenuStrip.Enabled = false;
                CountsStatusLabel.Enabled = false;
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

        private void EnableControls()
        {
            try
            {
                SessionFormListBoxIrrDates.Enabled = true;
                SessionFormMenuStrip.Enabled = true;
                CountsStatusLabel.Enabled = true;
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

        private void SessionFormButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                FillDisplayedTable();
                if (!_session.MeasurementList.Any() || !SessionFormadvancedDataGridView.Rows.OfType<object>().Any())
                {
                    MessageBoxTemplates.ErrorSync("Образцы для измерений не выбраны!");
                    return;
                }

                if (!_session.ManagedDetectors.Any())
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

                _session.ClearMeasurements();

                var mvcgProc =  Process.GetProcesses().Where(p => p.ProcessName == "mvcg").ToArray();
                foreach(var mvcgp in mvcgProc)
                    mvcgp.Kill();

                ProcessManager.RunMvcg();
                System.Threading.Thread.Sleep(1000);
                foreach (var d in _session.ManagedDetectors)
                {
                    d.Disconnect();
                    ProcessManager.ShowDetectorInMvcg(d.Name);
                    System.Threading.Thread.Sleep(1000);
                    d.Connect();
                }

                DisableControls();
                HighlightCurrentSample();
                
                var dcp = new DetectorControlPanel(ref _session);
                dcp.Show();

                _session.StartMeasurements();
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


        private void HighlightCurrentSample()
        {
            
        }

        private void SessionFormButtonPause_Click(object sender, EventArgs e)
        {
            try
            {
                _session.PauseMeasurements();
                EnableControls();
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

        private void SessionFormButtonStop_Click(object sender, EventArgs e)
        {
            try
            {
                var r = MessageBox.Show($"Вы пытаетесь остановить измерения.{Environment.NewLine}Если Вы хотите сохранить файл спектра, а также информацию о текущих измерениях в базу данных, а затем остановить измерения, нажмите Yes.{Environment.NewLine}Если Вы хотите сохранить файл спектра, но не хотите сохранять информацию в базу данных и при этом хотите остановить измерения нажмите - No.{Environment.NewLine}Если Вы хотите продолжить измерения нажмите - Cancel.", "Прерывание процесса измерений",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Exclamation);
                if (r == DialogResult.Yes)
                {
                    _session.PauseMeasurements();
                    foreach (var d in _session.ManagedDetectors)
                    {
                        var cd = d;
                        _session.SaveSpectra(ref cd);
                        _session.SaveMeasurement(ref cd);
                        ProcessManager.CloseDetector(d.Name);
                    }
                    _session.StopMeasurements();
                    EnableControls();

                    ProcessManager.CloseMvcg();
                    System.Threading.Thread.Sleep(2000);
                    return;
                }

                if (r == DialogResult.No)
                {
                    _session.PauseMeasurements();
                    foreach (var d in _session.ManagedDetectors)
                    {
                        var cd = d;
                        _session.SaveSpectra(ref cd);
                        ProcessManager.CloseDetector(d.Name);
                    }
                    _session.StopMeasurements();
                    EnableControls();
                    ProcessManager.CloseMvcg();
                    System.Threading.Thread.Sleep(2000);
                    return;
                }
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

        private void SessionFormButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                _session.ClearMeasurements();
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
    }
}
