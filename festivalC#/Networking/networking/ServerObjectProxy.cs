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
    public class ServerObjectProxy : IServer
    {
        private string host;
        private int port;

        private IClient client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public ServerObjectProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public virtual void login(User user, IClient client)
        {
            initializeConnection();
            sendRequest(new LoginRequest(user));
            Response response = readResponse();
            if (response is OkResponse)
            {
                this.client = client;
                return;
            }
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                closeConnection();
                throw new ConException(err.Message);
            }
        }


        public virtual void logout(User user, IClient client)
        {
            sendRequest(new LogoutRequest(user));
            Response response = readResponse();
            closeConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ConException(err.Message);
            }
        }

        public virtual IEnumerable<Spectacol> GetAllSpectacole()
        {
            sendRequest(new GetAllSpectacoleRequest());
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ConException(err.Message);
            }
            GetAllSpectacoleResponse resp = (GetAllSpectacoleResponse)response;
            IEnumerable<Spectacol> list = resp.List;
            return list;
        }


        public virtual IEnumerable<Spectacol> GetSpectacoleDate(String date)
        {
            sendRequest(new GetSpectacoleDateRequest(date));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ConException(err.Message);
            }
            GetSpectacoleDateResponse resp = (GetSpectacoleDateResponse)response;
            IEnumerable<Spectacol> list = resp.List;
            return list;
        }

        public virtual int GetNextIDBilet()
        {
            sendRequest(new GetNextIDBiletRequest());
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ConException(err.Message);
            }
            GetNextIDBiletResponse resp = (GetNextIDBiletResponse)response;
            int id = resp.Id;
            return id;
        }

        public virtual Bilet SaveBilet(Bilet bilet)
        {
            sendRequest(new SaveBiletRequest(bilet));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ConException(err.Message);
            }
            SaveBiletResponse resp = (SaveBiletResponse)response;
            Bilet b = resp.Bilet;
            return b;

        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                //output.close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new ConException("Error sending object " + e);
            }

        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }


        private void handleUpdate(UpdateResponse update)
        {
            if (update is BiletAddedResponse)
            {

                BiletAddedResponse biletAdded = (BiletAddedResponse)update;
                try
                {
                    client.BiletAdded();
                }
                catch (ConException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        handleUpdate((UpdateResponse)response);
                    }
                    else
                    {

                        lock (responses)
                        {


                            responses.Enqueue((Response)response);

                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }
    }
}
