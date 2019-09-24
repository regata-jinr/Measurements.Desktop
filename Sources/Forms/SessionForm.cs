using System.Windows.Forms;
using System;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SessionForm : Form
    {
        private ISession _session;
        private bool _isInitialized;
        public SessionForm(ISession session) 
        {
            _session = session;
            _isInitialized = true;
            InitializeComponent();
            SessionFormStatusStrip.ShowItemToolTips = true;

            InitializeDetectorDropDownItems();
            SessionControllerSingleton.AvailableDetectorsListHasChanged += InitializeDetectorDropDownItems;

            Text = $"Сессия измерений [{session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";

        }


        private void InitializeDetectorDropDownItems()
        {
            var allDetectors = SessionControllerSingleton.AvailableDetectors.Select(da => da).Union(_session.ManagedDetectors.Select(dm => dm)).OrderBy(md => md.Name).ToArray();

            if (_isInitialized)
            {
                DetectorsDropDownMenu = new ToolStripMenuItem() { Text = "Детекторы", CheckOnClick = false };
                DetectorsLabel = new ToolStripStatusLabel() { Name = "DetectorBegan", Text = "Детекторы: ", ToolTipText = "Список детекторов подключенных к сессии" };
            }
            else
            {
                DetectorsDropDownMenu.DropDownItems.Clear();
                RemoveDetectorsFromStatusLabel();
            }

            SessionFormStatusStrip.Items.Add(DetectorsLabel);

            var detectorsPosition = SessionFormStatusStrip.Items.IndexOf(DetectorsLabel) + 1;

            foreach (var det in allDetectors)
            {
                var cDet = det;
                var detItem = new DetectorItem(ref _session, ref cDet);
                DetectorsDropDownMenu.DropDownItems.Add(detItem.DetectorMenuItem);
                if (_session.ManagedDetectors.Contains(det))
                {
                    SessionFormStatusStrip.Items.Insert(detectorsPosition, detItem.DetectorStatusLabel);
                    DetectorsDropDownMenu.DropDownItems.OfType<ToolStripMenuItem>().Last().Checked = true;
                }
            }

            if (_session.ManagedDetectors.Count == 0)
            {
                SessionFormStatusStrip.Items.Insert(detectorsPosition, new ToolStripStatusLabel() { Name = "DetectorEmpty", Text = "---", ToolTipText = "К сессии не подключен ни один детектор" });
                detectorsPosition++;
            }

            SessionFormStatusStrip.Items.Insert(detectorsPosition + _session.ManagedDetectors.Count,new ToolStripStatusLabel() { Name = "DetectorEnd", Text = "||" });

            if (_isInitialized)
                SessionFormMenuStrip.Items.Add(DetectorsDropDownMenu);

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
