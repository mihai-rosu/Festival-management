using festivalc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.repository
{
    public class ArtistDBRepository : ICrudRepository<int, Artist>
    {
        public ArtistDBRepository() { }

        public Artist findOne(int id)
        {
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,nume from Artist where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idA = dataR.GetInt32(0);
                        String nume = dataR.GetString(1);
                        Artist artist = new Artist(idA,nume);
                        return artist;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Artist> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<Artist> artistR = new List<Artist>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, nume from Artist";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idA = dataR.GetInt32(0);
                        String nume = dataR.GetString(1);
                        Artist artist = new Artist(idA, nume);
                        artistR.Add(artist);
                    }
                }
            }

            return artistR;
        }
        public void save(Artist entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Artist values (@idA, @nume)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idA";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = entity.Nume;
                comm.Parameters.Add(paramNume);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No artist added !");
            }

        }
        public void delete(int id)
        {
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Artist where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No artist deleted!");
            }
        }

    }
}
