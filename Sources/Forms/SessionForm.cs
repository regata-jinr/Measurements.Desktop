using System.Windows.Forms;
using Measurements.Core;

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
        }
    }
}
