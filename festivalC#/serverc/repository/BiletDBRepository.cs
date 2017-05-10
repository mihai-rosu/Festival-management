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

        public int size()
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select count(*) as [SIZE] from Bilet";

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int size = dataR.GetInt32(0);
                        return size;
                    }
                }
            }
            return -1;
        }
        public Bilet findOne(int id)
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Bilet where idb=@idb";
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
                        string cumparator = dataR.GetString(2);
                        int cantitate = dataR.GetInt32(3);
                        Bilet bilet = new Bilet(idB, idS, cumparator, cantitate);
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
                        string cumparator = dataR.GetString(2);
                        int cantitate = dataR.GetInt32(3);
                        Bilet bilet = new Bilet(idB, idS, cumparator, cantitate);
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
                comm.CommandText = "insert into Bilet values (@idB, @idS, @cumparator, @cantitate)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idB";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramIdS = comm.CreateParameter();
                paramIdS.ParameterName = "@idS";
                paramIdS.Value = entity.IdS;
                comm.Parameters.Add(paramIdS);

                var paramCumparator = comm.CreateParameter();
                paramCumparator.ParameterName = "@cumparator";
                paramCumparator.Value = entity.Cumparator;
                comm.Parameters.Add(paramCumparator);

                var paramCantitate = comm.CreateParameter();
                paramCantitate.ParameterName = "@cantitate";
                paramCantitate.Value = entity.Cantitate;
                comm.Parameters.Add(paramCantitate);

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
