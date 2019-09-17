using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Measurements.UI.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public TextBox textboxPin;
        public Button  addPinCodeButton;
        public Label pinLabel;
        private bool isPinButtonClicked = false;

        private void ButtonLoginFormEnter_Click(object sender, EventArgs e)
        {

        }

        private void ButtonLoginFormCreatePin_Click(object sender, EventArgs e)
        {
            if (isPinButtonClicked)
            {
                this.Controls.Clear();
                this.InitializeComponent();
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
                Managers.MessageBoxTemplates.Error("При создании пин-кода все поля должны быть заполнены");
                return;
            }

            if (int.TryParse(textboxPin.Text, out _) && textboxPin.Text.Length == 4)
            {


            }
            else
                Managers.MessageBoxTemplates.Error("Пин-код должен быть целым четырехзначным числом");

        }


    }
}
