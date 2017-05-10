using festivalc.model;
using serverc.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.repository
{
    public class UserDBRepository
    {
        private ApplicationDbContext _context;

        public UserDBRepository() { }
        

        public User login(string userName, string password)
        {
            User user;
            using (_context = new ApplicationDbContext())
            {
                user = _context.Users.SingleOrDefault(c => c.Id == userName && c.Password == password);
            }

            return user;
        }
    }
}
