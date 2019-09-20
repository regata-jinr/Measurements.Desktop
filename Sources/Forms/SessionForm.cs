using System.Windows.Forms;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SessionForm : Form
    {
        private ISession _session;
        public SessionForm(ISession session) : this()
        {
            _session = session;
            Text = $"Сессия измерений [{session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
        }

        public SessionForm()
        {
            InitializeComponent();
            Text = $"Сессия измерений [Untitled session]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
            _session = new Session();
            SessionFormMenuStrip.Items.Add(new DropDownMenuItem("Детекторы",SessionControllerSingleton.AvailableDetectors.Select(d => d.Name).ToArray(),true));
            SessionFormMenuStrip.Items.Add(new DropDownMenuItem("Тип", Session.MeasurementTypes));
            //SessionFormMenuStrip.Items.Add(new DropDownMenuItem("", System.Enum.GetNames(typeof(SpreadOptions))));
        }
    }
}
