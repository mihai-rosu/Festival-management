using festivalc.model;
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

        Service service;

        public FestivalForm(Service service)
        {
            this.service = service;
            InitializeComponent();
            FillSpectacoleDataGridView(service.GetAllSpectacole());
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
            specTable.Columns.Add(new DataColumn("ID", typeof(int)));
            specTable.Columns.Add(new DataColumn("Artist", typeof(string)));
            specTable.Columns.Add(new DataColumn("Locatia", typeof(string)));
            specTable.Columns.Add(new DataColumn("Ora", typeof(string)));
            specTable.Columns.Add(new DataColumn("Disponibile", typeof(int)));

            foreach (var s in spectacole)
            {
                var row = specTable.NewRow();
                row[0] = s.Id;
                row[1] = s.NumeArtist;
                row[2] = s.Locatie;
                row[3] = s.Ora.ToString();
                row[4] = s.Locuridisponibile;

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
            IEnumerable <Spectacol> lista = service.GetSpectacoleDate(data);

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
            int id = service.GetNextIDBilet();
            String cumparator = cumparatorTextBox.Text;
            if (!cumparator.Equals(""))
            {
                try
                {
                    var row = srcDataGridView.SelectedRows[0];
                    int ids = Convert.ToInt32(row.Cells[0].Value);
                    int cantitate = Convert.ToInt32(cantitateTextBox.Text);
                    Bilet b = new Bilet(id, ids, cumparator, cantitate);
                    Bilet saved = service.SaveBilet(b);
                    if (saved != null)
                    {
                        this.FillSpectacoleDataGridView(service.GetAllSpectacole());
                        srcDataGridView.DataSource = null;
                    }
                    else
                    {
                        showErrorMessage("Nu sunt locuri disponibile");
                    }
                }
                catch (Exception ex)
                {
                    showErrorMessage(ex.Message);
                }
            }
            else
            {
                showErrorMessage("Field gol!");
            }


        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            service.logout();
            this.Hide();
            LoginForm loginForm = new LoginForm(service);
            loginForm.Show();
        }
    }
}
