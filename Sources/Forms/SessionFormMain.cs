using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;
using Measurements.UI.Managers;
using Measurements.Core.Handlers;
using System.Data;
using System.Diagnostics;
using Measurements.UI.Models;

namespace Measurements.UI.Desktop.Forms
{
    //TODO: add tests
    //TODO: rethink exception handler for ui to load async
    //TODO: change detectors should re draw datagrid view
    //TODO: change type should re draw datagrid view
    //TODO: reorganize the code

    public partial class SessionForm : Form
    {
        private BindingSource _bindingSource;
        private ISession _session;
        private Dictionary<bool, System.Drawing.Color> ConnectionStatusColor;
        private List<MeasurementInfo> _measurementsList;

        public SessionForm(ISession session)
        {
            try
            {
                ConnectionStatusColor = new Dictionary<bool, System.Drawing.Color>() { { false, System.Drawing.Color.Red }, { true, System.Drawing.Color.Green } };
                _session = session;

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

                MeasurementsProgressBar = new ToolStripProgressBar();
                MeasurementsProgressBar.Value = 0;
                MeasurementsProgressBar.Alignment = ToolStripItemAlignment.Right;
                SessionFormStatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                MeasurementsProgressBar.ToolTipText = $"Прогресс измерений по образцам";

                SessionFormMenuStrip.Items.Add(MenuOptions);

                _session.MeasurementOfSampleDone += MeasurementDoneHandler;

                SessionFormStatusStrip.Items.Add(MeasurementsProgressBar);

                _session.SessionComplete += SessionCompleteHandler;

                SessionFormNumericUpDownSeconds.ValueChanged += DurationHandler;
                SessionFormNumericUpDownMinutes.ValueChanged += DurationHandler;
                SessionFormNumericUpDownHours.ValueChanged   += DurationHandler;
                SessionFormNumericUpDownDays.ValueChanged    += DurationHandler;

                foreach (var rb in SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Select(r => r).ToArray())
                    rb.CheckedChanged += HeightRadioButtonCheckedChanged;

                SessionFormAdvancedDataGridViewIrradiationsJournals.SelectionChanged += SelectIrrJournalHandler;

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
                using (var ic = new InfoContext())
                {
                    _measurementsList = ic.Measurements.Where(ir => ir.DateTimeStart.HasValue &&
                                                             ir.DateTimeStart.Value.Date == _session.CurrentIrradiationDate.Date &&
                                                             ir.Type == _session.Type)
                                                       .ToList();
                }

                if (_measurementsList == null)
                    _measurementsList = new List<MeasurementInfo>();

                var advbindSource = new  AdvancedBindingSource<MeasurementInfo>(_measurementsList);
                SessionFormAdvancedDataGridViewMeasurementsJournal.SetDoubleBuffered();
                _bindingSource = advbindSource.GetBindingSource();
                SessionFormAdvancedDataGridViewMeasurementsJournal.DataSource = _bindingSource;

                //if (_session.Type.Contains("LLI"))
                //    _bindingSource.Sort = "Container, Position";

                SetColumnVisibles(_session.Type);
                SessionFormAdvancedDataGridViewSearchToolBar.SetColumns(SessionFormAdvancedDataGridViewMeasurementsJournal.Columns);

            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
                if (_measurementsList == null)
                    _measurementsList = new List<MeasurementInfo>();
            }
        }
        private void SetColumnVisibles(string type)
        {
            if (type == "SLI")
                SetColumnProperties4SLI();
            if (type.Contains("LLI"))
                SetColumnProperties4LLI();
        }

        private void SessionFormadvancedDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            SessionFormAdvancedDataGridViewMeasurementsJournal.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DetectorsRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
                var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
                if (colName != "Detector") return;

                foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
                    cell.Value = short.Parse(SessionFormGroupBoxDetectors.Controls.OfType<RadioButton>().Where(r => r.Checked).First().Text);
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }

        }

        private string Detector
        {
            get
            {
                return SessionFormGroupBoxDetectors.Controls.OfType<RadioButton>().Where(r => r.Checked).First().Text;
            }
            set
            {
                SessionFormGroupBoxDetectors.Controls.OfType<RadioButton>().Where(r => r.Text == value.ToString()).First().Checked = true;
            }
        }

        private void DurationHandler(object sender, EventArgs e)
        {
            try
            {
                var ts = new TimeSpan((int)SessionFormNumericUpDownDays.Value,
                                      (int)SessionFormNumericUpDownHours.Value,
                                      (int)SessionFormNumericUpDownMinutes.Value,
                                      (int)SessionFormNumericUpDownSeconds.Value
                                      );

                SessionFormNumericUpDownSeconds.Value = ts.Seconds;
                SessionFormNumericUpDownMinutes.Value = ts.Minutes;
                SessionFormNumericUpDownHours.Value = ts.Hours;
                SessionFormNumericUpDownDays.Value = ts.Days;

                if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
                var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
                if (colName != "Duration") return;

                foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
                    cell.Value = ts.TotalSeconds;
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }

        }

        private int Duration
        {
            get
            {
                var ts = new TimeSpan((int)SessionFormNumericUpDownDays.Value,
                                      (int)SessionFormNumericUpDownHours.Value,
                                      (int)SessionFormNumericUpDownMinutes.Value,
                                      (int)SessionFormNumericUpDownSeconds.Value
                                      );
                return (int)ts.TotalSeconds;
            }
            set
            {
                var ts = TimeSpan.FromSeconds(value);
                SessionFormNumericUpDownSeconds.Value = ts.Seconds;
                SessionFormNumericUpDownMinutes.Value = ts.Minutes;
                SessionFormNumericUpDownHours.Value   = ts.Hours;
                SessionFormNumericUpDownDays.Value    = ts.Days;
            }
        }

        private void HeightRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
                var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
                if (colName != "Height") return;

                foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
                    cell.Value = HeightGeometry;
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }

        }

        private decimal HeightGeometry
        {
            get
            {
                return decimal.Parse(SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Where(r => r.Checked).First().Text);
            }
            set
            {
                SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Where(r => r.Text == value.ToString()).First().Checked = true;
            }
        }

        private void SessionFormadvancedDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0 || SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count > 1) return;

                var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
                var firstCell =  SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0];


                if (colName == "Duration")
                {
                    if (firstCell.Value != null && !string.IsNullOrEmpty(firstCell.Value.ToString()) && firstCell.Value != DBNull.Value)
                        Duration = int.Parse(firstCell.Value.ToString());
                    else
                        Duration = 0;
                }

                if (colName == "Detector")
                        Detector = firstCell.Value.ToString();

                if (colName == "Height")
                    HeightGeometry = decimal.Parse(firstCell.Value.ToString());

            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
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
              
                HighlightCurrentSample();
            }
            catch (ArgumentOutOfRangeException aoe)
            { }
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


        private void InitializeIrradiationsDatesTable()
        {
            try
            {
                SessionFormAdvancedDataGridViewIrradiationsJournals.DataSource = null;
                if (_session.Type == null) return;
                List<IrradiationLogInfo> ssList = null;
                using (var ic = new InfoContext())
                {
                        ssList = ic.Irradiations.Where(ir => ir.Type == _session.Type && ir.DateTimeStart.HasValue).Select( ir => new IrradiationLogInfo { DateTimeStart = ir.DateTimeStart.Value.Date, LoadNumber = ir.LoadNumber  }).Distinct().OrderByDescending(ir => ir.DateTimeStart).ToList();
                }

                if (ssList == null)
                    ssList = new List<IrradiationLogInfo>();

                var advbindSource = new  AdvancedBindingSource<IrradiationLogInfo>(ssList);
                SessionFormAdvancedDataGridViewIrradiationsJournals.SetDoubleBuffered();
                var _SSbindingSource = advbindSource.GetBindingSource();
                SessionFormAdvancedDataGridViewIrradiationsJournals.DataSource = _SSbindingSource;
                SetColumnVisiblesForIrrDate();
                SessionFormAdvancedDataGridViewIrradiationsJournals.SelectionChanged += SelectIrrJournalHandler;
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

        private void SetColumnVisiblesForIrrDate()
        {
            if (_session.Type == "SLI")
            {
                SetColumnsProperties(ref SessionFormAdvancedDataGridViewIrradiationsJournals,
                                   new string[]
                                   { "LoadNumber" },
                                   new Dictionary<string, string>() {
                                    { "DateTimeStart", "Дата журнала" }
                                   },
                                   new string[0]
                                   );
            }
            else
            {
                SetColumnsProperties(ref SessionFormAdvancedDataGridViewIrradiationsJournals,
                                   new string[0],
                                   new Dictionary<string, string>() {
                                    { "DateTimeStart", "Дата журнала" },
                                    { "LoadNumber",    "Номер загрузки" }
                                   },
                                   new string[0]
                                   );
            }

        }

        private void SelectIrrJournalHandler(object sender, EventArgs e)
        {
            ShowSamples();
        }

        private void ShowSamples()
        {
            return;
            SessionFormAdvancedDataGridViewIrradiatedSamples.DataSource = null;

            if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedRows.Count == 0)
                return;

            var selCells = SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedRows[0].Cells;
            List<MeasurementInfo> SampleList = null;
            using (var ic = new InfoContext())
            {
                //SampleList = ic.Irradiations.Where(s => ).ToList();
            }

            if (SampleList == null)
                SampleList = new List<MeasurementInfo>();

            var advbindSource = new  AdvancedBindingSource<MeasurementInfo>(SampleList);
            SessionFormAdvancedDataGridViewIrradiatedSamples.SetDoubleBuffered();
            var bindingSource = advbindSource.GetBindingSource();
            SessionFormAdvancedDataGridViewIrradiatedSamples.DataSource = bindingSource;

            SetColumnsProperties(ref SessionFormAdvancedDataGridViewMeasurementsJournal,
                new string[] {  },
                new Dictionary<string, string>()
                {
                    { "",          "" },
                    { "",   "" },
                    { "",        "" }
                },
                new string[0]

                );

        }

        private void SetColumnsProperties(ref Zuby.ADGV.AdvancedDataGridView adgv, string[] invisibles, Dictionary<string, string> columnHeaders, string[] readonlies)
        {
            try
            {
                foreach (var colName in invisibles)
                    adgv.Columns[colName].Visible = false;

                if (readonlies.Any())
                {
                    foreach (var colName in readonlies)
                        adgv.Columns[colName].ReadOnly = true;
                }
                else
                {
                    foreach (DataGridViewColumn col in adgv.Columns)
                        col.ReadOnly = true;
                }

                foreach (var colName in columnHeaders.Keys)
                    adgv.Columns[colName].HeaderText = columnHeaders[colName];
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }
        }

        private void InitializeTypeDropDownItems()
        {
            try
            {
                var detEndPostition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd) + 1;
                var typesItems = new EnumItem(Session.MeasurementTypes, "Тип");
                typesItems.CheckedChanged += SetType;
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
                OptionsItem.CheckedChanged += del;
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
                InitializeDisplayedTable();
                InitializeIrradiationsDatesTable();

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

        private void InitializeDetectorDropDownItems()
        {
            try
            {
                var allDetectors = SessionControllerSingleton.AvailableDetectors.Select(da => da).Union(_session.ManagedDetectors.Select(dm => dm)).OrderBy(md => md.Name).ToArray();

                if (DetectorsDropDownMenu == null)
                {
                    DetectorsDropDownMenu = new ToolStripMenuItem() { Text = "Детекторы", CheckOnClick = false };

                    DetectorsLabelStart = new ToolStripStatusLabel() { Name = "DetectorBegun", Text = "||Детекторы: ", ToolTipText = "Список детекторов подключенных к сессии" };
                    DetectorsLabelEnd = new ToolStripStatusLabel() { Name = "DetectorEnd", Text = "||" };
                }
                else
                {
                    DetectorsDropDownMenu.DropDownItems.Clear();
                    SessionFormGroupBoxDetectors.Controls.Clear();
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
                        detItem.DetectorRadioButton.CheckedChanged += DetectorsRadioButtonCheckedChanged;

                        if (SessionFormGroupBoxDetectors.Controls.Count == 0)
                            detItem.DetectorRadioButton.Location = new System.Drawing.Point(10, 38);
                        else
                        {
                            var lastRbWidth  = SessionFormGroupBoxDetectors.Controls.OfType<RadioButton>().Last().Size.Width;
                            var lastRbX  = SessionFormGroupBoxDetectors.Controls.OfType<RadioButton>().Last().Location.X;
                            detItem.DetectorRadioButton.Location = new System.Drawing.Point(lastRbX + lastRbWidth + 5, 38);
                        }

                        SessionFormGroupBoxDetectors.Controls.Add(detItem.DetectorRadioButton);
                    }
                }

                if (_session.ManagedDetectors.Count == 0)
                {
                    SessionFormStatusStrip.Items.Insert(detectorsPosition, new ToolStripStatusLabel() { Name = "DetectorEmpty", Text = "---", ToolTipText = "К сессии не подключен ни один детектор" });
                    detectorsPosition++;
                }

                SessionFormStatusStrip.Items.Insert(detectorsPosition + _session.ManagedDetectors.Count, DetectorsLabelEnd);

                if (!SessionFormMenuStrip.Items.Contains(DetectorsDropDownMenu))
                    SessionFormMenuStrip.Items.Add(DetectorsDropDownMenu);
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
                if (!_session.MeasurementList.Any() || !SessionFormAdvancedDataGridViewMeasurementsJournal.Rows.OfType<object>().Any())
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

        private void SessionFormlButtonAddSelectedToJournal_Click(object sender, EventArgs e)
        {
            try
            {
                AddMeasurementsInfoFromIrradiationsJournal();

                InitializeDisplayedTable();

            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }
        }

        private void AddMeasurementsInfoFromIrradiationsJournal()
        {
            if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedRows.Count != 1)
                throw new InvalidOperationException("Не выбран ни один журнал измерений!");

            if (SessionFormAdvancedDataGridViewIrradiatedSamples.SelectedRows.Count == 0)
                throw new ArgumentException("Не выбран ни один образец для добавления в журнал измерений!");

            if (_session.Type == "SLI")
                AddSLIMeasurementsInfoToMainTable();
            else
                AddLLIMeasurementsInfoToMainTable();
        }


        private void SessionFormAdvancedDataGridViewSearchToolBar_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            try
            {
                bool restartsearch = true;
                int startColumn = 0;
                int startRow = 0;
                if (!e.FromBegin)
                {
                    bool endcol = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex + 1 >= SessionFormAdvancedDataGridViewMeasurementsJournal.ColumnCount;
                    bool endrow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex + 1 >= SessionFormAdvancedDataGridViewMeasurementsJournal.RowCount;

                    if (endcol && endrow)
                    {
                        startColumn = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex;
                        startRow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex;
                    }
                    else
                    {
                        startColumn = endcol ? 0 : SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex + 1;
                        startRow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex + (endcol ? 1 : 0);
                    }
                }
                DataGridViewCell c = SessionFormAdvancedDataGridViewMeasurementsJournal.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
                if (c == null && restartsearch)
                    c = SessionFormAdvancedDataGridViewMeasurementsJournal.FindCell(
                        e.ValueToSearch,
                        e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                        0,
                        0,
                        e.WholeWord,
                        e.CaseSensitive);
                if (c != null)
                    SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell = c;
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }
        }
    }
}
