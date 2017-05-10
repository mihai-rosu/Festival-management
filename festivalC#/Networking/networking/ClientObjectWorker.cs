using festivalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking.networking
{
    public class ClientObjectWorker : IClient
    {
        private IServer server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public ClientObjectWorker(IServer server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }

        public virtual void BiletAdded()
        {
            try
            {
                sendResponse(new BiletAddedResponse());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response handleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;

                User user = logReq.User;
                try
                {
                    lock (server)
                    {
                        server.login(user, this);
                    }
                    return new OkResponse();
                }
                catch (ConException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                User user = logReq.User;

                try
                {
                    lock (server)
                    {

                        server.logout(user, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (ConException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetAllSpectacoleRequest)
            {
                Console.WriteLine("GetPart request..");
                GetAllSpectacoleRequest getReq = (GetAllSpectacoleRequest)request;



                try
                {
                    IEnumerable<Spectacol> list;
                    lock (server)
                    {

                        list = server.GetAllSpectacole();
                    }

                    return new GetAllSpectacoleResponse(list);

                }
                catch (ConException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

           

            if (request is GetNextIDBiletRequest)
            {
                Console.WriteLine("GetCap request..");
                GetNextIDBiletRequest getReq = (GetNextIDBiletRequest)request;



                try
                {
                    int id;
                    lock (server)
                    {

                        id = server.GetNextIDBilet();
                    }

                    return new GetNextIDBiletResponse(id);

                }
                catch (ConException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetSpectacoleDateRequest)
            {
                Console.WriteLine("GetPbE request..");
                GetSpectacoleDateRequest getReq = (GetSpectacoleDateRequest)request;
                String date = getReq.Data;


                try
                {
                    IEnumerable<Spectacol> list;
                    lock (server)
                    {

                        list = server.GetSpectacoleDate(date);
                    }

                    return new GetSpectacoleDateResponse(list);

                }
                catch (ConException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is SaveBiletRequest)
            {
                Console.WriteLine("add request..");
                SaveBiletRequest getReq = (SaveBiletRequest)request;
                Bilet bilet = getReq.Bilet;


                try
                {

                    lock (server)
                    {

                        server.SaveBilet(bilet);
                    }

                    return new SaveBiletResponse(bilet);

                }
                catch (ConException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            return response;
        }


    }
}
