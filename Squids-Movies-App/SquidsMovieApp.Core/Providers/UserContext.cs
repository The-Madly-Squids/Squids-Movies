using System;
using System.Collections.Generic;
using SquidsMovieApp.Core.Contracts;
using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.Core.Providers
{
    public class UserContext : IUserContext
    {
        private readonly AuthProvider authProvider;

        public UserContext(AuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        public User CurrentUser { get => this.authProvider.GetCurrentUser(); }

        public IList<string> Permissions
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
