using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    [Serializable]
    public class User:HasId<String>
    {
        private String username;
        private String password;

        public User() { }


        public User(String username, String password)
        {
            this.username = username;
            this.password = password;
        }

        public String Id
        {
            get { return username; }
            set { username = value; }
        }
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
