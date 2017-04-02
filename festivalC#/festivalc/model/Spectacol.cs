using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    public class Spectacol
    {
        private int id;
        private int idA;
        private string numeArtist;
        private string date;
        private string ora;
        private string locatie;
        private int locuridisponibile;
        private int locuriocupate;

        public Spectacol(int id, int idA, string date, string ora, string locatie, int nrlocuri, int locuriocupate)
        {
            this.id = id;
            this.idA = idA;
            this.date = date;
            this.ora = ora;
            this.locatie = locatie;
            this.locuridisponibile = nrlocuri;
            this.locuriocupate = locuriocupate;
        }

        public int Id {
            get { return id; }
            set { id = value; }
        }
        public int IdA{
            get { return idA; }
            set { idA = value; }
        }

        public string NumeArtist
        {
            get { return numeArtist; }
            set { numeArtist = value; }
        }

        public string Date {
            get { return date; }
            set { date = value; }
        }

        public string Ora
        {
            get { return ora; }
            set { ora = value; }
        }

        public string Locatie {
            get { return locatie; }
            set { locatie = value; }
        }
        public int Locuridisponibile {
            get { return locuridisponibile; }
            set { locuridisponibile = value; }
        }
        public int Locuriocupate {
            get { return locuriocupate; }
            set { locuriocupate = value; }
        }
    }
}
