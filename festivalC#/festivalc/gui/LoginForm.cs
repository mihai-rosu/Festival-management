using festivalc.gui;
using festivalc.repository;
using festivalc.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace festivalc
{
    public partial class LoginForm : Form
    {
        private LoginService service;

        public LoginForm(LoginService service)
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
                ArtistDBRepository artistRepo = new ArtistDBRepository();
                SpectacolDBRepository spectacolRepo = new SpectacolDBRepository();
                BiletDBRepository biletRepo = new BiletDBRepository();
                FestivalService festivalService = new FestivalService(artistRepo, spectacolRepo, biletRepo);
                FestivalForm mainForm = new FestivalForm(festivalService);
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
