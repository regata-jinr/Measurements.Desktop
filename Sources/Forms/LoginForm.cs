using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Measurements.UI.Managers;
using System.Linq;

namespace Measurements.UI.Desktop.Forms
{
    public partial class LoginForm : Form
    {
        private string _connectionStringBase;
        private string _connectionStringFull;
        public static string CurrentVersion => $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major}.{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor}.{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build}";


        public LoginForm()
        {
            MessageBoxTemplates.UIHandleException();
            var config = new Measurements.Configurator.ConfigManager();
            _connectionStringBase = config.GenConnectionStringBase;
#if DEBUG
            _connectionStringBase = @"Server=RUMLAB\REGATALOCAL;Database=NAA_DB_TEST;Trusted_Connection=True;";
#endif
            InitializeComponent();
            Text = $"Regata Measurements UI - {CurrentVersion}";
            textBoxLoginFormUser.Focus();

            if (System.Diagnostics.Process.GetProcesses().Count(p => p.ProcessName == System.Diagnostics.Process.GetCurrentProcess().ProcessName) >= 2)
            {
                MessageBoxTemplates.ErrorSync("Программа измерений уже запущена");
                Application.Exit();
            }
        }

        public TextBox textboxPin;
        public Button  addPinCodeButton;
        public Label pinLabel;
        private bool isPinButtonClicked = false;
        private string _user;
        private string _password;
        private string _pin;
        private bool _isPin = false;

        private void CheckIfUserHasPin(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLoginFormUser.Text))
                return;
            var sm = SecurityManager.GetCredential($"Pin_{textBoxLoginFormUser.Text}");
            if (sm == null)
                return;
            label2.Text = "Пин код:";
            _isPin = true;
        }

        private void ButtonLoginFormEnter_Click(object sender, EventArgs e)
        {

            var isPinCorrect = false;
            if (_isPin)
            {
                _pin = textBoxLoginFormPassword.Text;
                _user = textBoxLoginFormUser.Text;
                isPinCorrect =  CheckPin();
            }

            if (isPinCorrect)
            {
                var sm = SecurityManager.GetCredential($"Password_{_user}");
                if (sm == null)
                {
                    MessageBoxTemplates.ErrorSync("Пароль связанный с пин-кодом не найден. Попробуйте создать пин-код заново");
                    return;
                }
                _password = sm.Password;
            }
            else
            {
                MessageBoxTemplates.ErrorSync("Неправильный логин или пин-код");
                return;
            }

            if (CheckPassword())
            {
                var scpf = new SessionControlPanel(_connectionStringFull);
                scpf.Show();
                Hide();
            }
            else
                MessageBoxTemplates.ErrorSync("Неправильный логин или пароль");

        }

        private void ButtonLoginFormCreatePin_Click(object sender, EventArgs e)
        {
            if (isPinButtonClicked)
            {
                this.Controls.Clear();
                this.InitializeComponent();
                Text = $"Regata Measurements UI - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
                isPinButtonClicked = false;
            }
            else
                CreatePinExtensions();


        }

        private void CreatePinExtensions()
        {
            int x = this.Size.Height - buttonLoginFormCreatePin.Location.Y - buttonLoginFormCreatePin.Size.Height; // distance between textboxes and buttons
            int y = textBoxLoginFormPassword.Location.Y - textBoxLoginFormUser.Location.Y - textBoxLoginFormUser.Size.Height; //distance between textboxes
            this.Size = new Size(this.Size.Width, this.Size.Height + y + textBoxLoginFormPassword.Size.Height);


            textboxPin = new TextBox();
            textboxPin.Location = new System.Drawing.Point(textBoxLoginFormPassword.Location.X, textBoxLoginFormPassword.Location.Y + y + textBoxLoginFormPassword.Size.Height);
            textboxPin.Size = new System.Drawing.Size(136, 22);
            this.Controls.Add(textboxPin);

            pinLabel = new Label();
            pinLabel.Location = new System.Drawing.Point(label1.Location.X, textBoxLoginFormPassword.Location.Y + y + textBoxLoginFormPassword.Size.Height);
            pinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pinLabel.Size = new System.Drawing.Size(75, 28);
            pinLabel.Text = "Пин-код:";
            label2.Text = "Пароль:";
            pinLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.Controls.Add(pinLabel);

            buttonLoginFormCreatePin.Location = new System.Drawing.Point(buttonLoginFormCreatePin.Location.X - 15, buttonLoginFormCreatePin.Location.Y +  x );

            buttonLoginFormEnter.Location = new System.Drawing.Point(buttonLoginFormEnter.Location.X + 20, buttonLoginFormEnter.Location.Y + x);
            isPinButtonClicked = true;
            buttonLoginFormCreatePin.Text = "Скрыть";

            addPinCodeButton = new Button();
            addPinCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addPinCodeButton.Location = new System.Drawing.Point(-15 + (-buttonLoginFormCreatePin.Location.X - buttonLoginFormCreatePin.Size.Width + buttonLoginFormEnter.Location.X), buttonLoginFormEnter.Location.Y);
            addPinCodeButton.Size = new System.Drawing.Size(85, 45);
            addPinCodeButton.Text = "Сохранить пин-код";
            addPinCodeButton.UseVisualStyleBackColor = true;
            addPinCodeButton.Click += new System.EventHandler(addPinCodeButton_Click);
            this.Controls.Add(addPinCodeButton);

        }
        // TODO: add password checks function
        // TODO: add credentials saver
        // TODO: check if pin exist then instead password check pin
        private void addPinCodeButton_Click(object sender, EventArgs e)
        {
            if (textboxPin == null)
                return;
            if (string.IsNullOrEmpty(textboxPin.Text) || string.IsNullOrEmpty(textBoxLoginFormUser.Text) || string.IsNullOrEmpty(textBoxLoginFormPassword.Text))
            {
                MessageBoxTemplates.ErrorSync("При создании пин-кода все поля должны быть заполнены");
                return;
            }

            if (int.TryParse(textboxPin.Text, out _) && textboxPin.Text.Length == 4)
            {
                _user = textBoxLoginFormUser.Text;
                _password = textBoxLoginFormPassword.Text;
                _pin = textboxPin.Text;

                if (CheckPassword())
                {
                    if (SecurityManager.SetCredential($"Password_{textBoxLoginFormUser.Text}", textBoxLoginFormUser.Text, textBoxLoginFormPassword.Text) && SecurityManager.SetCredential($"Pin_{textBoxLoginFormUser.Text}", textBoxLoginFormUser.Text, textboxPin.Text))
                    {
                        MessageBoxTemplates.InfoAsync("Пин-код успешно сохранен");
                        _isPin = true;
                        this.Controls.Clear();
                        this.InitializeComponent();
                        Text = $"Regata Measurements UI - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
                        isPinButtonClicked = false;
                        textBoxLoginFormUser.Text = _user;
                        textBoxLoginFormPassword.Text = _pin;
                    }
                    else
                        MessageBoxTemplates.ErrorSync("Проблема с установкой пин-кода. Возможно, Вы уже создавали пин-код"); 
                }
                else
                    MessageBoxTemplates.ErrorSync("Программа не может установить соединение с базой данных. Проверьте указанный логин или пароль.");


            }
            else
                MessageBoxTemplates.ErrorSync("Пин-код должен быть целым четырехзначным числом");
        }


        private bool CheckPassword()
        {
            var isConnected = false;
            _connectionStringFull = $"{_connectionStringBase}User Id={_user};Password={_password};";
            using (var sqlCon = new SqlConnection(_connectionStringFull))
            {
                sqlCon.Open();
                isConnected = true;
            }
            return isConnected;
        }

        private bool CheckPin()
        {
            var isCorrect = false;

            var sm = SecurityManager.GetCredential($"Pin_{_user}");
            if (sm == null)
                return false;

            if (_pin == sm.Password && _user == sm.Login)
                isCorrect = true;

            return isCorrect;
        }


    }
}
