using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Core.Factories
{
    public class UserModelFactory : IUserFactory
    {
        public UserModel CreateUserModel(string firstName, string lastName, int? age,
            string username, string email, string password, bool isAdmin, decimal moneyBalance)
        {
            return new UserModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Username = username,
                Email = email,
                Password = password,
                IsAdmin = isAdmin,
                MoneyBalance = moneyBalance
            };
        }
    }
}
