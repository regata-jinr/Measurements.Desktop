/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Regata.Desktop.WinForms.Components;
using System.Data;
using System.Diagnostics;

namespace Regata.Desktop.WinForms.Measurements
{

    enum MeasurementStatus { ready, error, inProgress, done }

    public partial class SessionForm : Form
    {
        //private BindingSource _bindingSource;
        //private event Action ChangeMeasurementStatus;
        //private ISession _session;
        //private readonly Dictionary<bool, System.Drawing.Color> ConnectionStatusColor;
        //private List<Measurement> _measurementsList;
        //private List<Irradiation> _irradiationList;
        //private Dictionary<string, List<Measurement>> _spreadedMeasurementsInfoes;
        //DetectorControlPanel DetectorsControlPanel;

        //private string User { get { return SessionControllerSingleton.ConnectionStringBuilder.UserID; } }

        //private DateTime? SelectedIrrJournalDate
        //{
        //    get 
        //    {
        //        try
        //        {
        //            if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedCells.Count != 0)
        //                return (DateTime)SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedCells[0].Value;
        //            return null;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //            return null;
        //        }
        //    }
        //}

        //private int? SelectedLoadNumber
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedCells.Count != 0 && _session.Type != "SLI")
        //                return (int)SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedCells[1].Value;
        //            return null;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //            return null;
        //        }
        //    }
        //}

        public SessionForm()
        {
            //try
            //{
            //    ConnectionStatusColor = new Dictionary<bool, System.Drawing.Color>() { { false, System.Drawing.Color.Red }, { true, System.Drawing.Color.Green } };
            //    _session = session;

            InitializeComponent();

            //    Text = $"Сессия измерений [{session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{User}]";

            //    SessionFormStatusStrip.ShowItemToolTips = true;

            //    ConnectionStatus = new ToolStripStatusLabel() { Name = "ConnectionStatusItem", Text = " ", ToolTipText = "Состояние соединения с БД", BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()] };
            //    SessionControllerSingleton.ConnectionFallen += ConnectionStatusHandler;
            //    SessionFormStatusStrip.Items.Add(ConnectionStatus);

            //    InitializeDetectorDropDownItems();
            //    SessionControllerSingleton.AvailableDetectorsListHasChanged += InitializeDetectorDropDownItems;
            //    InitializeTypeDropDownItems();

            //    SessionFormTableLayoutPanelDetectors.ResumeLayout(true);

            //    CountsOptionsItem = new EnumItem(Enum.GetNames(typeof(CanberraDeviceAccessLib.AcquisitionModes)), "Режим набора");
            //    InitializeOptionsMenu(CountsOptionsItem, SetCountMode);
            //    CountsOptionsItem.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.CountMode.ToString()).First().PerformClick();

            //    MeasurementsProgressBar = new ToolStripProgressBar();
            //    MeasurementsProgressBar.Value = 0;
            //    MeasurementsProgressBar.Alignment = ToolStripItemAlignment.Right;
            //    SessionFormStatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            //    MeasurementsProgressBar.ToolTipText = $"Прогресс измерений по образцам";

            //    SessionFormMenuStrip.Items.Add(MenuOptions);

            //    _session.MeasurementOfSampleDone += CurrentMeasurementDoneHandler;

            //    SessionFormStatusStrip.Items.Add(MeasurementsProgressBar);

            //    _session.SessionComplete += SessionCompleteHandler;

            //    SessionFormNumericUpDownSeconds.ValueChanged += DurationHandler;
            //    SessionFormNumericUpDownMinutes.ValueChanged += DurationHandler;
            //    SessionFormNumericUpDownHours.ValueChanged   += DurationHandler;
            //    SessionFormNumericUpDownDays.ValueChanged    += DurationHandler;

            //    foreach (var rb in SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Select(r => r).ToArray())
            //        rb.CheckedChanged += HeightRadioButtonCheckedChanged;

            //    SessionFormAdvancedDataGridViewIrradiationsJournals.SelectionChanged += SelectIrrJournalHandler;

            //    SessionFormCheckBoxHideAlreadyMeasured.Checked = true;
            //    SessionFormCheckBoxHideAlreadyMeasured.CheckedChanged += SessionFormCheckBoxShowAlreadyMeasured_CheckedChanged;

            //    SessionFormAdvancedDataGridViewMeasurementsJournal.CellValueChanged += UpdateMeasurementsJournal;

            //    _spreadedMeasurementsInfoes = new Dictionary<string, List<Measurement>>();

            //    SesionFormComboBoxExistedMeasurementsJournal.SelectedIndexChanged += SesionFormComboBoxExistedMeasurementsJournal_SelectedValueChanged;
            //}
            //catch (Exception ex)
            //{
            //    MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
            //    {
            //        exception = ex,
            //        Level = Core.Handlers.ExceptionLevel.Error
            //    });
            //}
        }

        //private void SesionFormComboBoxExistedMeasurementsJournal_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    InitializeDisplayedTable();
        //}

        //private void FormMeasurementJournalList()
        //{
        //    try
        //    {
        //        if (_session.Type == null) return;

        //        List<string> MeasurementsJournalIds = null;
        //        using (var ic = new InfoContext())
        //        {
        //            MeasurementsJournalIds = ic.Irradiations.Where(ir => ir.Type == _session.Type && ir.DateTimeStart.HasValue).Select(ir => new { ir.DateTimeStart.Value.Date, ir.LoadNumber }).Distinct().OrderByDescending(ir => ir.Date).ThenByDescending(ir => ir.LoadNumber).Select(ir => $"{ir.Date.ToShortDateString()};{ir.LoadNumber}").Distinct().ToList();
        //        }

        //        SesionFormComboBoxExistedMeasurementsJournal.DataSource = MeasurementsJournalIds;

        //        SesionFormComboBoxExistedMeasurementsJournal.SelectedItem = MeasurementsJournalIds.First();

        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.DataSource == null)
        //            InitializeDisplayedTable();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void UpdateMeasurementsJournal(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RowIndex < 0 || e.RowIndex >= SessionFormAdvancedDataGridViewMeasurementsJournal.Rows.Count) return;

        //        if (!DataValidation(e)) return;

        //        using (var ic = new InfoContext())
        //        {
        //            var currentMeasurement = ic.Measurements.Where(ir => ir.Id == (int)SessionFormAdvancedDataGridViewMeasurementsJournal.Rows[e.RowIndex].Cells["Id"].Value).First();

        //            var currentMeasurementInList = _measurementsList.Find(m => m.Id == currentMeasurement.Id);
        //            _measurementsList.Remove(currentMeasurementInList);

        //            System.Type measInfo = typeof(Measurement);
        //            var prop = measInfo.GetProperty(SessionFormAdvancedDataGridViewMeasurementsJournal.Columns[e.ColumnIndex].Name);

        //            if (!string.IsNullOrEmpty(SessionFormAdvancedDataGridViewMeasurementsJournal.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
        //                prop.SetValue(currentMeasurement, SessionFormAdvancedDataGridViewMeasurementsJournal.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        //            else
        //                prop.SetValue(currentMeasurement, null);

        //            _measurementsList.Add(currentMeasurement);
        //            _measurementsList.Sort((x, y) => x.Id.CompareTo(y.Id));
        //            ic.Measurements.Update(currentMeasurement);
        //            ic.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private bool DataValidation(DataGridViewCellEventArgs e)
        //{
        //    DataGridViewCell currentCell = SessionFormAdvancedDataGridViewMeasurementsJournal.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    DataGridViewCell savedValueOfCurrentCell = currentCell.Clone() as DataGridViewCell;

        //    try
        //    {
        //        var isValidated = true;

        //        DataGridViewColumn currentColumn = currentCell.OwningColumn;
        //        DataGridViewRow currentRow = currentCell.OwningRow;

        //        if (currentColumn.Name.Contains("DateTime"))
        //            isValidated = DateTime.TryParse(currentCell.Value.ToString(), out _);

        //        if (currentColumn.Name == "Note")
        //            isValidated = (currentCell.Value.ToString().Length < 300);

        //        if (!isValidated)
        //        {
        //            MessageBoxTemplates.WarningAsync($"Ошибка при валидации данных. Столбец - {currentColumn.Name}");
        //            currentCell.Value = savedValueOfCurrentCell.Value;
        //        }

        //        return isValidated;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //        currentCell.Value = savedValueOfCurrentCell.Value;
        //        return false;
        //    }
        //}

        //private void SessionFormCheckBoxShowAlreadyMeasured_CheckedChanged(object sender, EventArgs e)
        //{
        //    //ShowSamples();
        //    if (SessionFormAdvancedDataGridViewIrradiatedSamples.RowCount == 0 || _measurementsList == null) return;
        //    if (!SessionFormCheckBoxHideAlreadyMeasured.Checked)
        //    {
        //        foreach (DataGridViewRow hiddenRow in SessionFormAdvancedDataGridViewIrradiatedSamples.Rows.OfType<DataGridViewRow>().Where(dr => dr.Visible == false).ToArray())
        //            hiddenRow.Visible = true;
        //        SessionFormAdvancedDataGridViewIrradiatedSamples.FirstDisplayedScrollingRowIndex = 0;
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow hiddenRow in SessionFormAdvancedDataGridViewIrradiatedSamples.Rows)
        //        {
        //            if (hiddenRow == SessionFormAdvancedDataGridViewIrradiatedSamples.CurrentRow)
        //                SessionFormAdvancedDataGridViewIrradiatedSamples.CurrentCell = null;

        //            if (_measurementsList.Select(m => m.IrradiationId).Contains((int)hiddenRow.Cells["Id"].Value))
        //                hiddenRow.Visible = false;
        //        }
        //    }
        //}

        //private DateTime MJournalIrrDate
        //{
        //    get
        //    {
        //        try
        //        {
        //            var CurrentJournalId = SesionFormComboBoxExistedMeasurementsJournal.SelectedItem.ToString();
        //            return DateTime.Parse(CurrentJournalId.Split(';')[0]);
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //            return DateTime.Now.Date;
        //        }
        //    }
        //}

        //private int? MJournalIrrLoadNumber
        //{
        //    get
        //    {
        //        try
        //        {
        //            var CurrentJournalId = SesionFormComboBoxExistedMeasurementsJournal.SelectedItem.ToString();
        //            int? mJournalLoadNumber = null;
        //            if (_session.Type.Contains("LLI"))
        //                mJournalLoadNumber = int.Parse(CurrentJournalId.Split(';')[1]);
        //            return mJournalLoadNumber;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //            return null;
        //        }
        //    }
        //}

        //private void InitializeDisplayedTable()
        //{
        //    try
        //    {
        //        using (var ic = new InfoContext())
        //        {
        //            _measurementsList = ic.Measurements.Where(m =>
        //                                                      m.IrrJournalDate.Date == MJournalIrrDate &&
        //                                                      m.LoadNumber == MJournalIrrLoadNumber &&
        //                                                      m.Type == _session.Type)
        //                                               .ToList();
        //            _irradiationList = ic.Irradiations.Where(ir => _measurementsList.Select(m => m.IrradiationId).Contains(ir.Id)).ToList();
        //        }

        //        if (_irradiationList == null)
        //            _irradiationList = new List<Irradiation>();

        //        if (_measurementsList == null)
        //            _measurementsList = new List<Measurement>();

        //        var advbindSource = new  AdvancedBindingSource<Measurement>(_measurementsList);
        //        SessionFormAdvancedDataGridViewMeasurementsJournal.SetDoubleBuffered();
        //        _bindingSource = advbindSource.GetBindingSource();
        //        SessionFormAdvancedDataGridViewMeasurementsJournal.DataSource = _bindingSource;

        //        SetVisibilities(_session.Type);
        //        SessionFormAdvancedDataGridViewSearchToolBar.SetColumns(SessionFormAdvancedDataGridViewMeasurementsJournal.Columns);

        //        SessionFormCheckBoxShowAlreadyMeasured_CheckedChanged(null, EventArgs.Empty);

        //        foreach (DataGridViewRow row in SessionFormAdvancedDataGridViewMeasurementsJournal.Rows.OfType<DataGridViewRow>().Where(dr => !string.IsNullOrEmpty(dr.Cells["FileSpectra"].Value.ToString())).ToArray())
        //            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //        if (_measurementsList == null)
        //            _measurementsList = new List<Measurement>();
        //    }
        //}

        //private void SetVisibilities(string type)
        //{
        //    try
        //    {
        //        if (type == "SLI")
        //        {
        //            SetColumnProperties4SLI();
        //            SetSLIVisibilities();
        //        }
        //        if (type.Contains("LLI"))
        //        {
        //            SetColumnProperties4LLI();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = e, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SessionFormadvancedDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    SessionFormAdvancedDataGridViewMeasurementsJournal.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //}

        //private void DetectorsRadioButtonCheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
        //        var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
        //        if (colName != "Detector") return;

        //        foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
        //            cell.Value = Detector;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private string Detector
        //{
        //    get
        //    {
        //        if (!SessionFormTableLayoutPanelDetectors.Controls.OfType<RadioButton>().Any()) return "";
        //        return SessionFormTableLayoutPanelDetectors.Controls.OfType<RadioButton>().Where(r => r.Checked).First().Text;
        //    }
        //    set
        //    {
        //        if (!SessionFormTableLayoutPanelDetectors.Controls.OfType<RadioButton>().Any()) return;
        //        SessionFormTableLayoutPanelDetectors.Controls.OfType<RadioButton>().Where(r => r.Text == value.ToString()).First().Checked = true;
        //    }
        //}

        //private void DurationHandler(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var ts = new TimeSpan((int)SessionFormNumericUpDownDays.Value,
        //                              (int)SessionFormNumericUpDownHours.Value,
        //                              (int)SessionFormNumericUpDownMinutes.Value,
        //                              (int)SessionFormNumericUpDownSeconds.Value
        //                              );

        //        SessionFormNumericUpDownSeconds.Value = ts.Seconds;
        //        SessionFormNumericUpDownMinutes.Value = ts.Minutes;
        //        SessionFormNumericUpDownHours.Value = ts.Hours;
        //        SessionFormNumericUpDownDays.Value = ts.Days;

        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
        //        var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
        //        if (colName != "Duration") return;

        //        foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
        //            cell.Value = ts.TotalSeconds;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }

        //}

        //private int Duration
        //{
        //    get
        //    {
        //        try
        //        {
        //            var ts = new TimeSpan((int)SessionFormNumericUpDownDays.Value,
        //                              (int)SessionFormNumericUpDownHours.Value,
        //                              (int)SessionFormNumericUpDownMinutes.Value,
        //                              (int)SessionFormNumericUpDownSeconds.Value
        //                              );
        //            return (int)ts.TotalSeconds;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //            return 0;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            var ts = TimeSpan.FromSeconds(value);
        //            SessionFormNumericUpDownSeconds.Value = ts.Seconds;
        //            SessionFormNumericUpDownMinutes.Value = ts.Minutes;
        //            SessionFormNumericUpDownHours.Value = ts.Hours;
        //            SessionFormNumericUpDownDays.Value = ts.Days;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //        }
        //    }
        //}

        //private void HeightRadioButtonCheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0) return;
        //        var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
        //        if (colName != "Height") return;

        //        foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
        //            cell.Value = HeightGeometry;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }

        //}

        //private decimal HeightGeometry
        //{
        //    get
        //    {
        //        return decimal.Parse(SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Where(r => r.Checked).First().Text);
        //    }
        //    set
        //    {
        //        SessionFormGroupBoxHeights.Controls.OfType<RadioButton>().Where(r => r.Text == value.ToString()).First().Checked = true;
        //    }
        //}

        //private void SessionFormadvancedDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0 || SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count > 1) return;

        //        var colName = SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0].OwningColumn.Name;
        //        var firstCell =  SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells[0];

        //        if (colName == "Duration")
        //        {
        //            if (firstCell.Value != null && !string.IsNullOrEmpty(firstCell.Value.ToString()) && firstCell.Value != DBNull.Value)
        //                Duration = int.Parse(firstCell.Value.ToString());
        //            else
        //                Duration = 0;
        //        }

        //        if (colName == "Detector")
        //                Detector = firstCell.Value.ToString();

        //        if (colName == "Height")
        //            HeightGeometry = decimal.Parse(firstCell.Value.ToString());

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SessionCompleteHandler()
        //{
        //    try
        //    {
        //        EnableControls();
        //        MessageBoxTemplates.InfoSync($"Сессия {_session.Name} завершила измерения всех образцов!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void CurrentMeasurementDoneHandler(Measurement currentMeasurement)
        //{
        //    try
        //    {
        //        var det = _session.ManagedDetectors.Where(d => d.Name == currentMeasurement.Detector).First();

        //        InitializeDisplayedTable();
        //        if (!_spreadedMeasurementsInfoes[currentMeasurement.Detector].Where(m => m.Type == _session.Type && string.IsNullOrEmpty(m.FileSpectra)).Any())
        //        {
        //            MessageBoxTemplates.InfoAsync($"Детектор {det.Name} завершил измерения");
        //            return;
        //        }
        //        SetFirstNotMeasuredForDetector(det.Name);
        //        det.Start();
        //    }
        //    catch (ArgumentOutOfRangeException aoe)
        //    { }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }

        //}

        ////TODO: test connection should be async in the other case latency is possible
        //private void ConnectionStatusHandler()
        //{
        //    try
        //    {
        //        ConnectionStatus.BackColor = ConnectionStatusColor[SessionControllerSingleton.TestDBConnection()];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}


        //private void InitializeIrradiationsDatesTable()
        //{
        //    try
        //    {
        //        SessionFormAdvancedDataGridViewIrradiationsJournals.DataSource = null;
        //        if (_session.Type == null) return;
        //        List<IrradiationLogInfo> ssList = null;
        //        using (var ic = new InfoContext())
        //        {
        //                ssList = ic.Irradiations.Where(ir => ir.Type == _session.Type && ir.DateTimeStart.HasValue).Select( ir => new IrradiationLogInfo { DateTimeStart = ir.DateTimeStart.Value.Date, LoadNumber = ir.LoadNumber  }).Distinct().OrderByDescending(ir => ir.DateTimeStart).ToList();
        //        }

        //        if (ssList == null)
        //            ssList = new List<IrradiationLogInfo>();

        //        var advbindSource = new  AdvancedBindingSource<IrradiationLogInfo>(ssList);
        //        SessionFormAdvancedDataGridViewIrradiationsJournals.SetDoubleBuffered();
        //        var _SSbindingSource = advbindSource.GetBindingSource();
        //        SessionFormAdvancedDataGridViewIrradiationsJournals.DataSource = _SSbindingSource;
        //        SetColumnVisiblesForIrrDate();
        //        SessionFormAdvancedDataGridViewIrradiationsJournals.SelectionChanged += SelectIrrJournalHandler;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SetColumnVisiblesForIrrDate()
        //{
        //    if (_session.Type == "SLI")
        //    {
        //        SetColumnsProperties(ref SessionFormAdvancedDataGridViewIrradiationsJournals,
        //                           new string[]
        //                           { "LoadNumber" },
        //                           new Dictionary<string, string>() {
        //                            { "DateTimeStart", "Дата журнала" }
        //                           },
        //                           new string[0]
        //                           );
        //    }
        //    else
        //    {
        //        SetColumnsProperties(ref SessionFormAdvancedDataGridViewIrradiationsJournals,
        //                           new string[0],
        //                           new Dictionary<string, string>() {
        //                            { "DateTimeStart", "Дата журнала" },
        //                            { "LoadNumber",    "Номер загрузки" }
        //                           },
        //                           new string[0]
        //                           );
        //    }

        //}

        //private void SelectIrrJournalHandler(object sender, EventArgs e)
        //{
        //    ShowSamples();
        //}

        //private void ShowSamples()
        //{
        //    try
        //    {
        //        if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedRows.Count == 0)
        //            return;

        //        SessionFormAdvancedDataGridViewIrradiatedSamples.DataSource = null;

        //        List<Irradiation> SampleList = null;
        //        using (var ic = new InfoContext())
        //        {
        //            SampleList = ic.Irradiations.Where(ir => ir.Type == _session.Type && ir.DateTimeStart.HasValue && ir.DateTimeStart.Value.Date == SelectedIrrJournalDate.Value && ir.LoadNumber == SelectedLoadNumber).ToList();
        //        }

        //        SessionFormCheckBoxShowAlreadyMeasured_CheckedChanged(null, EventArgs.Empty);
        //        if (SampleList == null)
        //            SampleList = new List<Irradiation>();

        //        if (_session.Type.Contains("LLI"))
        //            SampleList = SampleList.OrderBy(ir => ir.Container).ThenBy(ir => ir.Position).ToList();

        //        var advbindSource = new  AdvancedBindingSource<Irradiation>(SampleList);
        //        SessionFormAdvancedDataGridViewIrradiatedSamples.SetDoubleBuffered();
        //        var bindingSource = advbindSource.GetBindingSource();
        //        SessionFormAdvancedDataGridViewIrradiatedSamples.DataSource = bindingSource;

        //        SetColumnsProperties(ref SessionFormAdvancedDataGridViewIrradiatedSamples,
        //            new string[] { "Id", "Type", "Weight", "DateTimeStart", "Duration", "DateTimeFinish", "Container", "Position", "Channel", "LoadNumber", "Rehandler", "Assistant", "SetKey", "SampleKey" },
        //            new Dictionary<string, string>()
        //            {
        //           {"CountryCode",  "Страна"},
        //           {"ClientNumber", "Клиент"},
        //           {"Year",         "Год"},
        //           {"SetNumber",    "Номер партии"},
        //           {"SetIndex",     "Индекс партии"},
        //           {"SampleNumber", "Номер образца"},
        //           {"Note",         "Примечание"},
        //            },
        //            new string[0]
        //            );

        //        SessionFormCheckBoxShowAlreadyMeasured_CheckedChanged(null, EventArgs.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SetColumnsProperties(ref Zuby.ADGV.AdvancedDataGridView adgv, string[] invisibles, Dictionary<string, string> columnHeaders, string[] readonlies)
        //{
        //    try
        //    {
        //        foreach (var colName in invisibles)
        //            adgv.Columns[colName].Visible = false;

        //        if (readonlies.Any())
        //        {
        //            foreach (var colName in readonlies)
        //                adgv.Columns[colName].ReadOnly = true;
        //        }
        //        else
        //        {
        //            foreach (DataGridViewColumn col in adgv.Columns)
        //                col.ReadOnly = true;
        //        }

        //        foreach (var colName in columnHeaders.Keys)
        //            adgv.Columns[colName].HeaderText = columnHeaders[colName];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //public static readonly string[] MeasurementTypes = {"SLI", "LLI-1", "LLI-2"};

        //private void InitializeTypeDropDownItems()
        //{
        //    try
        //    {
        //        var detEndPostition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd) + 1;
        //        var typesItems = new EnumItem(MeasurementTypes, "Тип");
        //        typesItems.CheckedChanged += SetType;
        //        SessionFormMenuStrip.Items.Add(typesItems.EnumMenuItem);
        //        SessionFormStatusStrip.Items.Insert(detEndPostition, typesItems.EnumStatusLabel);
        //        if (!string.IsNullOrEmpty(_session.Type))
        //            typesItems.EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(t => t.Text == _session.Type).First().PerformClick();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void InitializeOptionsMenu(EnumItem OptionsItem, Action<string> del)
        //{
        //    try
        //    {
        //        OptionsItem.CheckedChanged += del;
        //        MenuOptions.DropDownItems.Add(OptionsItem.EnumMenuItem);
        //        SessionFormStatusStrip.Items.Add(OptionsItem.EnumStatusLabel);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SetType(string type)
        //{
        //    try
        //    {
        //        _session.Type = type;
        //        FormMeasurementJournalList();
        //        InitializeIrradiationsDatesTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SetCountMode(string option)
        //{
        //    try
        //    {
        //        CanberraDeviceAccessLib.AcquisitionModes am;
        //        if (Enum.TryParse(option, out am))
        //            _session.CountMode = am;
        //        else
        //            _session.CountMode = CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void InitializeDetectorDropDownItems()
        //{
        //    try
        //    {
        //        var allDetectors = SessionControllerSingleton.AvailableDetectors.Select(da => da).Union(_session.ManagedDetectors.OrderBy(md => md.Name)).ToArray();

        //        if (DetectorsDropDownMenu == null)
        //        {
        //            DetectorsDropDownMenu = new ToolStripMenuItem() { Text = "Детекторы", CheckOnClick = false };

        //            DetectorsLabelStart = new ToolStripStatusLabel() { Name = "DetectorBegun", Text = "||Детекторы: ", ToolTipText = "Список детекторов подключенных к сессии" };
        //            DetectorsLabelEnd = new ToolStripStatusLabel() { Name = "DetectorEnd", Text = "||" };
        //        }
        //        else
        //        {
        //            DetectorsDropDownMenu.DropDownItems.Clear();
        //            RemoveDetectorsFromStatusLabel();
        //        }
        //        SessionFormTableLayoutPanelDetectors.Controls.Clear();
        //        SessionFormTableLayoutPanelDetectors.ColumnStyles.Clear();
        //        SessionFormTableLayoutPanelDetectors.ColumnCount = 1;
        //        SessionFormTableLayoutPanelDetectors.RowCount = 1;

        //        SessionFormStatusStrip.Items.Insert(1, DetectorsLabelStart);
        //        SessionFormStatusStrip.Items.Insert(2, DetectorsLabelEnd);

        //        var detectorsPosition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd);

        //        foreach (var det in allDetectors)
        //        {
        //            var cDet = det;
        //            var detItem = new DetectorItem(ref _session, ref cDet);
        //            DetectorsDropDownMenu.DropDownItems.Add(detItem.DetectorMenuItem);
        //            if (_session.ManagedDetectors.Contains(det))
        //            {
        //                SessionFormStatusStrip.Items.Insert(SessionFormStatusStrip.Items.IndexOf(DetectorsLabelEnd), detItem.DetectorStatusLabel);
        //                DetectorsDropDownMenu.DropDownItems.OfType<ToolStripMenuItem>().Last().Checked = true;
        //                detItem.DetectorRadioButton.CheckedChanged += DetectorsRadioButtonCheckedChanged;

        //                if (SessionFormTableLayoutPanelDetectors.Controls.Count == 0)
        //                {
        //                    SessionFormTableLayoutPanelDetectors.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        //                    SessionFormTableLayoutPanelDetectors.Controls.Add(detItem.DetectorRadioButton);
        //                    detItem.DetectorRadioButton.Checked = true;
        //                }
        //                else
        //                {
        //                    SessionFormTableLayoutPanelDetectors.ColumnCount += 1;
        //                    SessionFormTableLayoutPanelDetectors.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        //                    SessionFormTableLayoutPanelDetectors.Controls.Add(detItem.DetectorRadioButton);
        //                }
        //            }
        //        }

        //        if (_session.ManagedDetectors.Count == 0)
        //        {
        //            SessionFormStatusStrip.Items.Insert(detectorsPosition, new ToolStripStatusLabel() { Name = "DetectorEmpty", Text = "---", ToolTipText = "К сессии не подключен ни один детектор" });
        //            detectorsPosition++;
        //        }

        //        SessionFormStatusStrip.Items.Insert(detectorsPosition + _session.ManagedDetectors.Count, DetectorsLabelEnd);

        //        if (!SessionFormMenuStrip.Items.Contains(DetectorsDropDownMenu))
        //            SessionFormMenuStrip.Items.Add(DetectorsDropDownMenu);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }

        //}

        //private void RemoveDetectorsFromStatusLabel()
        //{
        //    try
        //    {
        //        for (var i = SessionFormStatusStrip.Items.Count - 1; i >= 0; --i)
        //        {
        //            if (SessionFormStatusStrip.Items[i].Name.Contains("Detector"))
        //                SessionFormStatusStrip.Items.RemoveAt(i);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void MenuSaveSession_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var savesessionform = new SaveSessionForm(_session.Name);
        //        savesessionform.SaveSessionEvent += SaveSessionHandler;
        //        savesessionform.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SaveSessionHandler(string name, bool isPublic)
        //{
        //    try
        //    {
        //        var ic = new InfoContext();

        //        if (ic.Sessions.Where(s => s.Name == name).Any())
        //        {
        //            var res = MessageBox.Show($"Сессия с таким именем '{name}' уже существует в базе данных. Вы хотите обновить параметры сессии?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
        //            if (res == DialogResult.No)
        //                return;
        //        }

        //        _session.SaveSession(name, isPublic);


        //        Text = $"Сессия измерений [{_session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";

        //        SessionControllerSingleton.SessionsInfoListsChangedHaveOccurred();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }

        //}

        //private void DisableControls()
        //{
        //    try
        //    {
        //        SessionFormMenuStrip.Enabled                        = false;
        //        SessionFormGroupBoxFormMeasurementsJournal.Enabled  = false;
        //        SessionFormGroupBoxDetectors.Enabled                = false;
        //        SessionFormGroupBoxChooseMeasurementJournal.Enabled = false;
        //        SessionFormGroupBoxIrradiationJournalsData.Enabled  = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void EnableControls()
        //{
        //    try
        //    {
        //        SessionFormMenuStrip.Enabled                        = true;
        //        SessionFormGroupBoxFormMeasurementsJournal.Enabled  = true;
        //        SessionFormGroupBoxDetectors.Enabled                = true;
        //        SessionFormGroupBoxChooseMeasurementJournal.Enabled = true;
        //        SessionFormGroupBoxIrradiationJournalsData.Enabled  = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private Irradiation GetRelatedSample(Measurement measurement) 
        //{
        //    var ir = _irradiationList.Where(i => i.Id == measurement.IrradiationId).First();
        //    if (ir == null)
        //        MessageBoxTemplates.WarningAsync("Программа не смогла найти данные в журнале облучений");
        //    return ir;
        //}



        //private void SetFirstNotMeasuredForDetector(string detName)
        //{
        //    try
        //    {
        //        var det =_session.ManagedDetectors.Where(d => d.Name == detName).First();

        //        var CurrentMeasurement = _spreadedMeasurementsInfoes[detName].Where(m => m.Detector == detName && string.IsNullOrEmpty(m.FileSpectra)).First();
        //        det.FillSampleInformation(CurrentMeasurement, GetRelatedSample(CurrentMeasurement));

        //        if (DetectorsControlPanel != null)
        //            DetectorsControlPanel.SourcesInitialize();
        //        HighlightMeasurement(det.CurrentMeasurement.Id, System.Drawing.Color.LightYellow);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SpreadSamplesToDetectors()
        //{
        //    try
        //    {
        //        _spreadedMeasurementsInfoes.Clear();
        //        foreach (var d in _session.ManagedDetectors)
        //        {
        //            var listOfMeasurementsOnDetector =_measurementsList.Where(m => m.Detector == d.Name && string.IsNullOrEmpty(m.FileSpectra)).ToList();
        //            if (listOfMeasurementsOnDetector.Any())
        //                _spreadedMeasurementsInfoes.Add(d.Name, listOfMeasurementsOnDetector);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SessionFormButtonStart_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!_irradiationList.Any() || !_measurementsList.Any() || !SessionFormAdvancedDataGridViewMeasurementsJournal.Rows.OfType<object>().Any())
        //        {
        //            MessageBoxTemplates.ErrorSync("Образцы для измерений не выбраны!");
        //            return;
        //        }

        //        if (!_session.ManagedDetectors.Any() || !SessionFormTableLayoutPanelDetectors.Controls.OfType<RadioButton>().Any())
        //        {
        //            MessageBoxTemplates.ErrorSync("Ни один детектор не подключен к сессии");
        //            return;
        //        }

        //        if (_session.Type == null)
        //        {
        //            MessageBoxTemplates.ErrorSync("Выбирете тип проводимых измерений!");
        //            return;
        //        }
                
        //        DisableControls();


        //        _session.ClearMeasurements();
        //        SpreadSamplesToDetectors();

        //        var mvcgProc =  Process.GetProcesses().Where(p => p.ProcessName == "mvcg").ToArray();
        //        foreach(var mvcgp in mvcgProc)
        //            mvcgp.Kill();

        //        ProcessManager.RunMvcg();
        //        System.Threading.Thread.Sleep(1000);
        //        foreach (var d in _session.ManagedDetectors)
        //        {
        //            d.Disconnect();
        //            ProcessManager.ShowDetectorInMvcg(d.Name);
        //            System.Threading.Thread.Sleep(1000);
        //            d.Connect();
        //        }

        //        foreach (var dName in _spreadedMeasurementsInfoes.Keys)
        //            SetFirstNotMeasuredForDetector(dName);

        //        _session.StartMeasurements();

        //        DetectorsControlPanel = new DetectorControlPanel(ref _session);
        //        DetectorsControlPanel.Show();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}



        //private void HighlightMeasurement(int mId, System.Drawing.Color color)
        //{
        //    try
        //    {
        //        SessionFormAdvancedDataGridViewMeasurementsJournal.Rows.OfType<DataGridViewRow>().Where(dr => (int)dr.Cells["Id"].Value == mId).First().DefaultCellStyle.BackColor = color;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SessionFormButtonPause_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _session.PauseMeasurements();
        //        EnableControls();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SessionFormButtonStop_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DetectorsControlPanel == null) return;

        //        var r = MessageBox.Show($"Вы пытаетесь остановить измерения.{Environment.NewLine}При нажатии кнопки Ok вся информация о текущих измерениях будет потеряна. Если Вы хотите сохранить информацию, сделайте это в ручную через панель детекторов.{Environment.NewLine}Если Вы хотите продолжить измерения нажмите - Cancel.", "Прерывание процесса измерений",MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        //        if (r == DialogResult.OK)
        //        {
        //            DetectorsControlPanel.Close();
        //            EnableControls();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SessionFormButtonClear_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _session.ClearMeasurements();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
        //        {
        //            exception = ex,
        //            Level = Core.Handlers.ExceptionLevel.Error
        //        });
        //    }
        //}

        //private void SessionFormlButtonAddSelectedToJournal_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        AddMeasurementsInfoFromIrradiationsJournal();

        //        InitializeDisplayedTable();
        //        SessionFormAdvancedDataGridViewMeasurementsJournal.FirstDisplayedScrollingRowIndex = SessionFormAdvancedDataGridViewMeasurementsJournal.RowCount - 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void AddMeasurementsInfoFromIrradiationsJournal()
        //{
        //    if (SessionFormAdvancedDataGridViewIrradiationsJournals.SelectedRows.Count != 1)
        //        throw new InvalidOperationException("Не выбран ни один журнал измерений!");

        //    if (SessionFormAdvancedDataGridViewIrradiatedSamples.SelectedRows.Count == 0)
        //        throw new ArgumentException("Не выбран ни один образец для добавления в журнал измерений!");

        //    if (_session.Type == "SLI")
        //        AddSLIMeasurementsInfoToMainTable();
        //    else
        //        AddLLIMeasurementsInfoToMainTable();
        //}


        //private void SessionFormAdvancedDataGridViewSearchToolBar_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        //{
        //    try
        //    {
        //        bool restartsearch = true;
        //        int startColumn = 0;
        //        int startRow = 0;
        //        if (!e.FromBegin)
        //        {
        //            bool endcol = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex + 1 >= SessionFormAdvancedDataGridViewMeasurementsJournal.ColumnCount;
        //            bool endrow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex + 1 >= SessionFormAdvancedDataGridViewMeasurementsJournal.RowCount;

        //            if (endcol && endrow)
        //            {
        //                startColumn = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex;
        //                startRow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex;
        //            }
        //            else
        //            {
        //                startColumn = endcol ? 0 : SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.ColumnIndex + 1;
        //                startRow = SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell.RowIndex + (endcol ? 1 : 0);
        //            }
        //        }
        //        DataGridViewCell c = SessionFormAdvancedDataGridViewMeasurementsJournal.FindCell(
        //        e.ValueToSearch,
        //        e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
        //        startRow,
        //        startColumn,
        //        e.WholeWord,
        //        e.CaseSensitive);
        //        if (c == null && restartsearch)
        //            c = SessionFormAdvancedDataGridViewMeasurementsJournal.FindCell(
        //                e.ValueToSearch,
        //                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
        //                0,
        //                0,
        //                e.WholeWord,
        //                e.CaseSensitive);
        //        if (c != null)
        //            SessionFormAdvancedDataGridViewMeasurementsJournal.CurrentCell = c;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SessionFormButtonRemoveSelectedFromJournal_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells.Count == 0)
        //        {
        //            MessageBoxTemplates.WarningAsync("Не выбран ни один образец для удаления из журнала измерений!");
        //            return;
        //        }

        //        foreach (DataGridViewCell cell in SessionFormAdvancedDataGridViewMeasurementsJournal.SelectedCells)
        //        {
        //            var row = cell.OwningRow;
        //            if (!_measurementsList.Select(m => m.Id).Contains((int)row.Cells["Id"].Value))
        //                continue;

        //            var selectedMeas = _measurementsList.Where(ir => ir.Id == (int)row.Cells["Id"].Value).First();
        //            Irradiation relatedIrrInfo = null;
        //            if (_irradiationList.Where(ir => ir.Id == selectedMeas.IrradiationId).Any())
        //                relatedIrrInfo = _irradiationList.Where(ir => ir.Id == selectedMeas.IrradiationId).First();

        //            using (var ic = new InfoContext())
        //            {
        //                ic.Measurements.Remove(selectedMeas);
        //                ic.SaveChanges();
        //            }

        //            _measurementsList.Remove(selectedMeas);
        //            if (relatedIrrInfo != null)
        //                _irradiationList.Remove(relatedIrrInfo);

        //            foreach (DataGridViewRow hiddenRow in SessionFormAdvancedDataGridViewIrradiatedSamples.Rows.OfType<DataGridViewRow>().Where(dr => dr.Visible == false).ToArray())
        //            {
        //                SessionFormAdvancedDataGridViewIrradiatedSamples.CurrentCell = null;

        //                if (selectedMeas.IrradiationId == (int)hiddenRow.Cells["Id"].Value)
        //                {
        //                    hiddenRow.Visible = true;
        //                    SessionFormAdvancedDataGridViewIrradiatedSamples.CurrentCell = hiddenRow.Cells[1];
        //                    break;
        //                }
        //            }
        //        }

        //        InitializeDisplayedTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
        //    }
        //}

        //private void SessionFormlButtonAddAllToJournal_Click(object sender, EventArgs e)
        //{
        //    if (_session.ManagedDetectors.Count == 0)
        //    {
        //        MessageBoxTemplates.ErrorSync("Не выбран ни один детектор!");
        //        return;
        //    }
        //    AddAllMeasurementsInfoToMainTable();
        //    InitializeDisplayedTable();
        //}

        //private void SessionFormToolStripMenuItemRefreshFormContent_Click(object sender, EventArgs e)
        //{

        //    if(_session.Type == null) return;
        //    InitializeIrradiationsDatesTable();
        //    ShowSamples();
        //    FormMeasurementJournalList();

        //}
    }
}
