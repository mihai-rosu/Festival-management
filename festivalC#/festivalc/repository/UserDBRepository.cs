using festivalc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.repository
{
    public class UserDBRepository
    {
        public UserDBRepository() { }

        public User findOne(String id)
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from User where username=@username";
                IDbDataParameter paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = id;
                comm.Parameters.Add(paramUsername);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        String username = dataR.GetString(0);
                        String password = dataR.GetString(1);
                        User user = new User(username, password);
                        return user;
                    }
                }
            }
            return null;
        }

        

    }
}
