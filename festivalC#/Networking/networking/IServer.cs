using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using festivalc.model;

namespace Networking.networking
{
    public interface IServer
    {
        void login(User user, IClient client);
        void logout(User user, IClient client);
        IEnumerable<Spectacol> GetAllSpectacole();
        IEnumerable<Spectacol> GetSpectacoleDate(string data);
        int GetNextIDBilet();
        Bilet SaveBilet(Bilet bilet);
    }
}
