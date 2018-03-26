using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
using System;
using System.Linq;

namespace SquidsMovieApp.Core.Providers
{
    public class AuthProvider
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private User loggedUser;

        public AuthProvider(IMovieAppDBContext movieAppDbContext)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.LoggedUser = null;
        }

        public User LoggedUser { get => loggedUser; private set => loggedUser = value; }

        public void Login(string email, string password)
        {
            var user = this.movieAppDbContext.Users
                .FirstOrDefault(x => x.Email.Equals(email.ToLower()));

            if (user != null && user.Password.Equals(password))
            {
                this.LoggedUser = user;
            }
            else
            {
                this.LoggedUser = null;
                throw new ArgumentException();
            }
        }

        public void Logout()
        {
            this.LoggedUser = null;
        }
    }
}
