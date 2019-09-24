using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Measurements.Core;

namespace Measurements.UI.Desktop.Components
{
    public class DetectorItem
    {
        public ToolStripMenuItem DetectorMenuItem;
        public ToolStripStatusLabel DetectorStatusLabel;
        private IDetector _det;
        private ISession _session;
        private Dictionary<DetectorStatus, System.Drawing.Color> StatusColor;

        public DetectorItem(ref ISession session, ref IDetector det)
        {
            _session = session;
            _det = det;
            DetectorMenuItem = new ToolStripMenuItem();
            DetectorStatusLabel = new ToolStripStatusLabel();

            StatusColor = new Dictionary<DetectorStatus, System.Drawing.Color>() { { DetectorStatus.busy, System.Drawing.Color.Yellow }, { DetectorStatus.off, System.Drawing.Color.Gray }, { DetectorStatus.ready, System.Drawing.Color.Green }, { DetectorStatus.error, System.Drawing.Color.Red } };

            DetectorMenuItem.CheckOnClick = true;
            DetectorMenuItem.Click += CheckHandler;

            DetectorStatusLabel.Name = $"{_det.Name}DetectorStatusLabel";
            DetectorStatusLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            Det_StatusChanged(null, EventArgs.Empty);
            
            det.StatusChanged += Det_StatusChanged;

            DetectorMenuItem.Text = _det.Name;
            DetectorMenuItem.Name = $"{_det.Name}Item";


        }

        private void Det_StatusChanged(object sender, EventArgs e)
        {
            DetectorStatusLabel.Text = _det.Name;
            DetectorStatusLabel.ToolTipText = $"Статус детектора - {_det.Status}";
            DetectorStatusLabel.BackColor = StatusColor[_det.Status];
        }

        private void CheckHandler(object sender, EventArgs e)
        {
            if (DetectorMenuItem.Checked)
                _session.AttachDetector(_det.Name);
            else
                _session.DetachDetector(_det.Name);

            DetectorMenuItem.ShowDropDown();
        }

    }
}
