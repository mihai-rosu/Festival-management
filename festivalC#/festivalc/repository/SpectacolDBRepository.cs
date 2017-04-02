using festivalc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.repository
{
    public class SpectacolDBRepository
    {
        public SpectacolDBRepository() { }
        public Spectacol findOne(int id)
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Spectacol where ids=@ids";
                IDbDataParameter paramIds = comm.CreateParameter();
                paramIds.ParameterName = "@ids";
                paramIds.Value = id;
                comm.Parameters.Add(paramIds);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int ids = dataR.GetInt32(0);
                        int ida = dataR.GetInt32(1);
                        string date = dataR.GetString(2);
                        string ora = dataR.GetString(3);
                        string locatie = dataR.GetString(4);
                        int nrlocuri = dataR.GetInt32(5);
                        int locuriocupate = dataR.GetInt32(7);
                        Spectacol spectacol = new Spectacol(ids, ida, date, ora, locatie, nrlocuri, locuriocupate);
                        return spectacol;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Spectacol> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<Spectacol> specR = new List<Spectacol>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Spectacol";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int ids = dataR.GetInt32(0);
                        int ida = dataR.GetInt32(1);
                        string date = dataR.GetString(2);
                        string ora = dataR.GetString(3);
                        string locatie = dataR.GetString(4);
                        int nrlocuri = dataR.GetInt32(5);
                        int locuriocupate = dataR.GetInt32(6);
                        Spectacol spectacol = new Spectacol(ids, ida, date, ora, locatie, nrlocuri, locuriocupate);
                        specR.Add(spectacol);
                    }
                }
            }

            return specR;
        }
        public void save(Spectacol entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Spectacol values (@ids, @ida,@date,@locatie,@nrlocuri,@locuriocupate)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@ids";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramIdA = comm.CreateParameter();
                paramIdA.ParameterName = "@ida";
                paramIdA.Value = entity.IdA;
                comm.Parameters.Add(paramIdA);

                var paramDate = comm.CreateParameter();
                paramDate.ParameterName = "@date";
                paramDate.Value = entity.Date;
                comm.Parameters.Add(paramDate);

                var paramLocatie = comm.CreateParameter();
                paramLocatie.ParameterName = "@locatie";
                paramLocatie.Value = entity.Locatie;
                comm.Parameters.Add(paramLocatie);

                var paramNrL = comm.CreateParameter();
                paramNrL.ParameterName = "@nrlocuri";
                paramNrL.Value = entity.Locuridisponibile;
                comm.Parameters.Add(paramNrL);

                var paramLOc = comm.CreateParameter();
                paramLOc.ParameterName = "@locuriocupate";
                paramLOc.Value = entity.Locuriocupate;
                comm.Parameters.Add(paramLOc);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No show added !");
            }

        }
        public void delete(int id)
        {
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Spectacol where ids=@ids";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@ids";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No show deleted!");
            }
        }
    }
}
