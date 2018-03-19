using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        void AddUser(UserModel user);
        void RemoveUser(UserModel user);

    }
}
