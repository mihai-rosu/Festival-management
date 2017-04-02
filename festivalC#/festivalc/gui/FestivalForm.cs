using festivalc.model;
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

namespace festivalc.gui
{
    public partial class FestivalForm : Form
    {
        FestivalService service;

        public FestivalForm(FestivalService service)
        {
            this.service = service;
            InitializeComponent();
            FillSpectacoleDataGridView(service.getAllSpectacole());
        }

        private void FillSpectacoleDataGridView(IEnumerable<Spectacol> spectacole)
        {
            var specTable = new DataTable();
            specTable.Columns.Add(new DataColumn("ID", typeof(int)));
            specTable.Columns.Add(new DataColumn("Artist", typeof(string)));
            specTable.Columns.Add(new DataColumn("Data", typeof(string)));
            specTable.Columns.Add(new DataColumn("Locatie", typeof(string)));
            specTable.Columns.Add(new DataColumn("Disponibile", typeof(int)));
            specTable.Columns.Add(new DataColumn("Vandute", typeof(int)));

            foreach (var s in spectacole)
            {
                var row = specTable.NewRow();
                row[0] = s.Id;
                row[1] = s.NumeArtist;
                row[2] = s.Date.ToString();
                row[3] = s.Locatie;
                row[4] = s.Locuridisponibile;
                row[5] = s.Locuriocupate;

                specTable.Rows.Add(row);
            }

            spectacolDataGridView.DataSource = null;
            spectacolDataGridView.DataSource = specTable;

            for (int i = 0; i < spectacolDataGridView.Columns.Count; i++)
            {
                spectacolDataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void FillSrcDataGridView(IEnumerable<Spectacol> spectacole)
        {
            var specTable = new DataTable();
            specTable.Columns.Add(new DataColumn("Artist", typeof(string)));
            specTable.Columns.Add(new DataColumn("Locatia", typeof(string)));
            specTable.Columns.Add(new DataColumn("Ora", typeof(string)));
            specTable.Columns.Add(new DataColumn("Disponibile", typeof(int)));

            foreach (var s in spectacole)
            {
                var row = specTable.NewRow();
                row[0] = s.NumeArtist;
                row[1] = s.Locatie;
                row[2] = s.Ora.ToString();
                row[3] = s.Locuridisponibile;

                specTable.Rows.Add(row);
            }

            srcDataGridView.DataSource = null;
            srcDataGridView.DataSource = specTable;

            for (int i = 0; i < srcDataGridView.Columns.Count; i++)
            {
                srcDataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String data = searchDateTimePicker.Value.ToString("yyyy-MM-dd");
            IEnumerable <Spectacol> lista = service.getSpectacoleDate(data);

            if (!lista.Any() == true)
                showErrorMessage("Nu sunt spectacole in  aceea data !");
            else
            {
                FillSrcDataGridView(lista);
            }
        }

        static void showErrorMessage(String text)
        {
            MessageBox.Show(text);
        }

        private void cumparaButton_Click(object sender, EventArgs e)
        {

        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDBRepository userRepo = new UserDBRepository();
            LoginService loginService = new LoginService(userRepo);
            LoginForm loginForm = new LoginForm(loginService);
            loginForm.Show();
        }
    }
}
