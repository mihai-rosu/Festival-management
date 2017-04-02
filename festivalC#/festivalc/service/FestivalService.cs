using festivalc.model;
using festivalc.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.service
{
    public class FestivalService
    {
        ArtistDBRepository artistRepo;
        SpectacolDBRepository spectacolRepo;
        BiletDBRepository biletRepo;

        public FestivalService(ArtistDBRepository artistRepo, SpectacolDBRepository spectacolRepo, BiletDBRepository biletRepo)
        {
            this.artistRepo = artistRepo;
            this.spectacolRepo = spectacolRepo;
            this.biletRepo = biletRepo;
        }

        public IEnumerable<Spectacol> getAllSpectacole()
        {
            IEnumerable<Spectacol> list = spectacolRepo.findAll();
            foreach (Spectacol spec in list)
            {
                Artist a = artistRepo.findOne(spec.IdA);
                spec.NumeArtist = a.Nume;
            }
            return list;
        }

        public List<Spectacol> getSpectacoleDate(String data)
        {
            IEnumerable<Spectacol> list = spectacolRepo.findAll();
            List<Spectacol> resultList = new List<Spectacol>();
            foreach (Spectacol spec in list)
                if (spec.Date.Equals(data))
                {
                    Artist a = artistRepo.findOne(spec.IdA);
                    spec.NumeArtist = a.Nume;
                    resultList.Add(spec);
                }

            return resultList;
        }
    }
}
