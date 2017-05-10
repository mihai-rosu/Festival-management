using festivalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.networking
{
    public interface Response
    {
    }

    [Serializable]
    public class OkResponse : Response
    {

    }

    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }
    [Serializable]
    public class GetAllSpectacoleResponse : Response
    {
        private IEnumerable<Spectacol> list;

        public GetAllSpectacoleResponse(IEnumerable<Spectacol> list)
        {
            this.list = list;
        }

        public virtual IEnumerable<Spectacol> List
        {
            get
            {
                return list;
            }
        }
    }
    [Serializable]
    public class GetSpectacoleDateResponse : Response
    {
        private IEnumerable<Spectacol> list;

        public GetSpectacoleDateResponse(IEnumerable<Spectacol> list)
        {
            this.list = list;
        }

        public virtual IEnumerable<Spectacol> List
        {
            get
            {
                return list;
            }
        }
    }

    [Serializable]
    public class SaveBiletResponse : Response
    {
        private Bilet bilet;

        public SaveBiletResponse(Bilet bilet)
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

    [Serializable]
    public class GetNextIDBiletResponse : Response
    {
        private int id;

        public GetNextIDBiletResponse(int id)
        {
            this.id = id;
        }

        public virtual int Id
        {
            get
            {
                return id;
            }
        }
    }
    
    public interface UpdateResponse : Response
    {
    }

    [Serializable]
    public class BiletAddedResponse : UpdateResponse
    {

        public BiletAddedResponse()
        {
        }

    }
}
