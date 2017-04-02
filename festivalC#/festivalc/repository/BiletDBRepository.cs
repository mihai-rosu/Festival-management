using festivalc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.repository
{
    public class BiletDBRepository : ICrudRepository<int,Bilet>
    {
        public BiletDBRepository() { }

        public Bilet findOne(int id)
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select idb,ids,pret from Bilet where idb=@idb";
                IDbDataParameter paramIdb = comm.CreateParameter();
                paramIdb.ParameterName = "@idb";
                paramIdb.Value = id;
                comm.Parameters.Add(paramIdb);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idB = dataR.GetInt32(0);
                        int idS = dataR.GetInt32(1);
                        double pret = dataR.GetDouble(2);
                        Bilet bilet = new Bilet(idB, idS, pret);
                        return bilet;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Bilet> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<Bilet> biletR = new List<Bilet>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select idb, ids, pret from Bilet";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idB = dataR.GetInt32(0);
                        int idS = dataR.GetInt32(1);
                        double pret = dataR.GetDouble(2);
                        Bilet bilet = new Bilet(idB, idS, pret);
                        biletR.Add(bilet);
                    }
                }
            }

            return biletR;
        }
        public void save(Bilet entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Bilet values (@idB, @idS, @pret)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idB";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramIdS = comm.CreateParameter();
                paramIdS.ParameterName = "@idS";
                paramIdS.Value = entity.IdS;
                comm.Parameters.Add(paramIdS);

                var paramPret = comm.CreateParameter();
                paramPret.ParameterName = "@pret";
                paramPret.Value = entity.Pret;
                comm.Parameters.Add(paramPret);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No ticket added !");
            }

        }
        public void delete(int id)
        {
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Bilet where idb=@idb";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@idb";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No ticket deleted!");
            }
        }

    }
}
