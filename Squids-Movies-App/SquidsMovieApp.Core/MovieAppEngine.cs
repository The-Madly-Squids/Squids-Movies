using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Core.Contracts;
using SquidsMovieApp.Core.Providers;

namespace SquidsMovieApp.Core
{
    public class MovieAppEngine : IEngine
    {
        private readonly IUserContext userController;
        private AuthProvider authentication;

        public MovieAppEngine(IUserContext userController, AuthProvider authentication)
        {
            this.userController = userController;
            this.authentication = authentication;
        }

        public void Start()
        {
            while (true)
            {
                if (this.userController.CurrentUser == null)
                {
                    //this.authentication.Login(username, password);
                }
            }
        }
    }
}
