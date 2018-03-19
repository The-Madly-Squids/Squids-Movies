using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Logic
{
    class UserService : IUserService
    {
        public void AddUser(UserModel user)
        {
            
        }

        public void RemoveUser(UserModel user)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<UserModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

       
    }
}
