using festivalc.model;
using festivalc.service;
using Networking.networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace festivalc
{
    static class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IServer server = new ServerObjectProxy("127.0.0.1", 55555);
            Service service = new Service(server);
            Application.Run(new LoginForm(service));
        }
    }
}
