using festivalc.model;
using Networking.networking;
using System;
using System.Collections.Generic;

namespace festivalc.service
{
    public class Service : IClient
    {
        public event EventHandler<FestivalEventArgs> updateEvent;
        private readonly IServer server;
        private User currentUser;

        public Service(IServer server)
        {
            this.server = server;
            currentUser = null;
        }

        public void BiletAdded()
        {
            FestivalEventArgs eventArgs = new FestivalEventArgs(UserEvent.BiletAdded, null);
            OnUserEvent(eventArgs);
        }

        protected virtual void OnUserEvent(FestivalEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public IEnumerable<Spectacol> GetAllSpectacole()
        {
            return server.GetAllSpectacole();
        }

        public IEnumerable<Spectacol> GetSpectacoleDate(String date)
        {
            return server.GetSpectacoleDate(date);
        }

        public int GetNextIDBilet()
        {
            return server.GetNextIDBilet();
        }

        public Bilet SaveBilet(Bilet b)
        {
            return server.SaveBilet(b);
        }

        public void login(String nume, String parola)
        {
            User user = new User(nume, parola);
            server.login(user, this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }
    }
}
