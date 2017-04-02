using festivalc.model;
using festivalc.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.service
{
    public class LoginService
    {
        private UserDBRepository userRepo;

        public LoginService(UserDBRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public void login(String username, String pass)
        {
            User userL= userRepo.findOne(username);
        if (userL == null || !userL.Password.Equals(pass)) {
                throw new Exception("Authentication failed.");
            }
        }
    }
}
