using festivalc.gui;
using festivalc.service;
using System;
using System.Windows.Forms;

namespace festivalc
{
    public partial class LoginForm : Form
    {
        private Service service;

        public LoginForm(Service service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                String user = usernameTextBox.Text;
                String pass = passwordTextBox.Text;
                service.login(user, pass);
                this.Hide();
                FestivalForm mainForm = new FestivalForm(service);
                mainForm.Show();
                
            }
            catch (Exception ex)
            {
                showErrorMessage(ex.Message);
            }
        }

        static void showErrorMessage(String text)
        {
            MessageBox.Show(text);
        }
    }
}
