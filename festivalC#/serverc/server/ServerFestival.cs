using festivalc.model;
using festivalc.repository;
using Networking.networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverc.server
{
    class ServerFestival : IServer
    {
        private UserDBRepository userRepo;
        private ArtistDBRepository artistRepo;
        private SpectacolDBRepository spectacolRepo;
        private BiletDBRepository biletRepo;
        private readonly IDictionary<String, IClient> loggedClients;

        public ServerFestival(UserDBRepository userRepo,ArtistDBRepository artistRepo, SpectacolDBRepository spectacolRepo, BiletDBRepository biletRepo)
        {
            this.artistRepo = artistRepo;
            this.biletRepo = biletRepo;
            this.spectacolRepo = spectacolRepo;
            this.userRepo = userRepo;
            this.loggedClients = new Dictionary<String, IClient>();
        }

        public void login(User user, IClient client)
        {
            User userOk = new User();
            Console.WriteLine(user.Id + " " + user.Password);
            try
            {
                userOk = userRepo.login(user.Id, user.Password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (userOk != null)
            {
                if (loggedClients.ContainsKey(user.Id))
                    throw new ConException("User already logged in.");
                loggedClients[user.Id] = client;
            }
            else
                throw new ConException("Authentication failed.");
        }

        public void logout(User user, IClient client)
        {
            IClient localClient = loggedClients[user.Id];
            if (localClient == null)
                throw new ConException("User " + user.Id + " is not logged in.");
            loggedClients.Remove(user.Id);

        }

        public IEnumerable<Spectacol> GetAllSpectacole()
        {
            List<Spectacol> lista = new List<Spectacol>();
            IEnumerable<Spectacol> list = spectacolRepo.findAll();
            foreach(Spectacol spec in list)
            {
                Artist a = artistRepo.findOne(spec.IdA);
                spec.NumeArtist = a.Nume;
                lista.Add(spec);
            }
            return lista;
        }

        public IEnumerable<Spectacol> GetSpectacoleDate(string data)
        {
            List<Spectacol> lista = new List<Spectacol>();
            IEnumerable<Spectacol> list = spectacolRepo.findAll();
            foreach (Spectacol spec in list)
            {
                if (spec.Date == data)
                {
                    Artist a = artistRepo.findOne(spec.IdA);
                    spec.NumeArtist = a.Nume;
                    lista.Add(spec);
                }
            }
            return lista;
        }

        public int GetNextIDBilet()
        {
            return biletRepo.size() + 1;
        }

        public Bilet SaveBilet(Bilet bilet)
        {
            int idS = bilet.IdS;
            Spectacol s = spectacolRepo.findOne(idS);
            if (s.Locuridisponibile - bilet.Cantitate < 0)
                return null;
            else
            {
                s.Locuridisponibile = s.Locuridisponibile - bilet.Cantitate;
                s.Locuriocupate = s.Locuriocupate + bilet.Cantitate;
                spectacolRepo.delete(idS);
                spectacolRepo.save(s);
                biletRepo.save(bilet);
                return bilet;
            }
        }
    }
}
