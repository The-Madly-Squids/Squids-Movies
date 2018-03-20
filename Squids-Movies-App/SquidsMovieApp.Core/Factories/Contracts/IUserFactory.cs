using SquidsMovieApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Core.Factories.Contracts
{
    public interface IUserFactory
    {
        UserModel CreateUserModel(string firstName, string lastName, int? age, string nickName,
            string email, string password, bool isAdmin, int moneyBalance);
    }
}
