using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    public class Artist:HasId<int>
    {
        private int id;
        private string nume;

        public Artist(int id,string nume)
        {
            this.Id = id;
            this.Nume = nume;
        }

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Nume {
            get { return nume; }
            set { nume = value; }
        }
    }
}
