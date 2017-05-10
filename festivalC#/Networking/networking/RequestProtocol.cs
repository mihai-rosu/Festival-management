using festivalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.networking
{
    public interface Request
    {
    }


    [Serializable]
    public class LoginRequest : Request
    {
        private User user;

        public LoginRequest(User user)
        {
            this.user = user;
        }

        public virtual User User
        {
            get
            {
                return user;
            }
        }
    }

    [Serializable]
    public class LogoutRequest : Request
    {
        private User user;

        public LogoutRequest(User user)
        {
            this.user = user;
        }

        public virtual User User
        {
            get
            {
                return user;
            }
        }
    }
    [Serializable]
    public class GetAllSpectacoleRequest : Request
    {


        public GetAllSpectacoleRequest()
        {

        }

    }
    [Serializable]
    public class GetNextIDBiletRequest : Request
    {

        public GetNextIDBiletRequest()
        {
        }


    }
  
    [Serializable]
    public class GetSpectacoleDateRequest : Request
    {
        private String data;

        public GetSpectacoleDateRequest(String data)
        {
            this.data = data;
        }

        public virtual String Data
        {
            get
            {
                return data;
            }
        }
    }
  

    [Serializable]
    public class SaveBiletRequest : Request
    {
        private Bilet bilet;

        public SaveBiletRequest(Bilet bilet)
        {
            this.bilet = bilet;
        }

        public virtual Bilet Bilet
        {
            get
            {
                return bilet;
            }
        }
    }
}
