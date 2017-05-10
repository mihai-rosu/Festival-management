using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    [Serializable]
    public class Bilet:HasId<int>
    {
        private int id;
        private int idS;
        private string cumparator;
        private int cantitate;

        public Bilet() { }

        public Bilet(int id, int idS, string cumparator, int cantitate)
        {
            this.id = id;
            this.idS = idS;
            this.cumparator = cumparator;
            this.cantitate = cantitate;
        }

        public int Id {
            get { return id; }
            set { id = value; }
        }
        public int IdS {
            get { return idS; }
            set { idS = value; }
        }
        public string Cumparator {
            get { return cumparator; }
            set { cumparator = value; }
        }
        public int Cantitate
        {
            get { return cantitate; }
            set { cantitate = value; }
        }
    }
}
