using SquidsMovieApp.Data.Context;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Models;
using System;
using System.Linq;

namespace SquidsMovieApp.Core.Providers
{
    public class AuthProvider
    {
        private readonly IUserService userService;
        private UserModel loggedUser;

        public AuthProvider(IUserService userService)
        {
            this.userService = userService;
            this.LoggedUser = null;
        }

        public UserModel LoggedUser { get => loggedUser; private set => loggedUser = value; }

        public void Login(string email, string password)
        {
            var user = this.userService.GetUserByEmail(email);

            if (user != null && user.Password.Equals(password))
            {
                this.LoggedUser = user;
            }
            else
            {
                this.LoggedUser = null;
                throw new NotImplementedException();
            }
        }

        public void Logout()
        {
            this.LoggedUser = null;
        }
    }
}
