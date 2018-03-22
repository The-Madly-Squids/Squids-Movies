using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
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
            this.loggedUser = null;
        }

        public User GetCurrentUser()
        {
            return this.loggedUser;
        }

        public void Login(string email, string password)
        {
            var user = this.movieAppDbContext.Users
                .FirstOrDefault(x => x.Email.Equals(email.ToLower()));

            if (user != null && user.Password.Equals(password))
            {
                this.loggedUser = user;
            }
            else
            {
                this.loggedUser = null;
                throw new ArgumentException();
            }
        }

        public void Logout()
        {
            this.loggedUser = null;
        }
    }
}
