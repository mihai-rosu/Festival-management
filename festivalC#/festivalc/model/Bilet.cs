using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    public class Bilet:HasId<int>
    {
        private int id;
        private int idS;
        private double pret;

        public Bilet(int id, int idS, double pret)
        {
            this.id = id;
            this.idS = idS;
            this.pret = pret;
        }

        public int Id {
            get { return id; }
            set { id = value; }
        }
        public int IdS {
            get { return idS; }
            set { idS = value; }
        }
        public double Pret {
            get { return pret; }
            set { pret = value; }
        }
    }
}
