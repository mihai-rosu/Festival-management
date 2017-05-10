using festivalc.repository;
using Networking.networking;
using serverc.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serverc
{
    class ServerMain
    {
        static void Main()
        {
            UserDBRepository userRepo = new UserDBRepository();
            BiletDBRepository biletRepo = new BiletDBRepository();
            SpectacolDBRepository spectacolRepo = new SpectacolDBRepository();
            ArtistDBRepository artistRepo = new ArtistDBRepository();
            IServer serverImpl = new ServerFestival(userRepo, artistRepo, spectacolRepo, biletRepo);
            
            // IChatServer serviceImpl = new ChatServerImpl();
            SerialServer server = new SerialServer("127.0.0.1", 55555, serverImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }

        public class SerialServer : ConcurrentServer
        {
            private IServer server;
            private ClientObjectWorker worker;
            public SerialServer(string host, int port, IServer server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer...");
            }
            protected override Thread createWorker(TcpClient client)
            {
                worker = new ClientObjectWorker(server, client);
                return new Thread(new ThreadStart(worker.run));
            }
        }
    }
}
