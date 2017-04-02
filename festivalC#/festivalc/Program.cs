using festivalc.model;
using festivalc.repository;
using festivalc.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace festivalc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ArtistDBRepository artistRepo = new ArtistDBRepository();
            BiletDBRepository biletRepo = new BiletDBRepository();
            SpectacolDBRepository spectacolRepo = new SpectacolDBRepository();
            UserDBRepository userRepo = new UserDBRepository();
            LoginService loginService = new LoginService(userRepo);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(loginService));
        }
    }
}
