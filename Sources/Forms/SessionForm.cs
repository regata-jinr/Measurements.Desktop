using System.Windows.Forms;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Desktop.Components;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SessionForm : Form
    {
        private ISession _session;
        public SessionForm(ISession session) 
        {
            _session = session;
            Text = $"Сессия измерений [{session.Name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";

            InitializeComponent();

            DetectorsDropDownMenu = new DropDownMenuItem("Детекторы", SessionControllerSingleton.AvailableDetectors.Select(d => d.Name).Union(_session.ManagedDetectors.Select(md => md.Name)).OrderBy(str => str).ToArray(),true);

            foreach (var md in _session.ManagedDetectors)
                DetectorsDropDownMenu[md.Name].Checked = true;

            SessionFormMenuStrip.Items.Add(DetectorsDropDownMenu);

            TypesDropDownMenu = new DropDownMenuItem("Тип", Session.MeasurementTypes);
            if(!string.IsNullOrEmpty(_session.Type))
                TypesDropDownMenu[_session.Type].Checked = true;

            SessionFormMenuStrip.Items.Add(TypesDropDownMenu);


            MenuOptions.DropDownItems.AddRange()

            SessionFormMenuStrip.Items.Add(MenuOptions);

            //SessionFormMenuStrip.Items.Add(new DropDownMenuItem("", System.Enum.GetNames(typeof(SpreadOptions))));
        }
    }
}
