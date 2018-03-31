using SquidsMovieApp.Common.Exceptions;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SquidsMovieApp.WPF
{
    public class UserContext
    {
        private readonly IUserService userService;
        private UserModel loggedUser;
        private UserModel fakeUser;
        private IList<MovieModel> cart;

        public UserContext(IUserService userService)
        {
            this.userService = userService;
            this.LoggedUser = null;
            this.cart = new List<MovieModel>();

            this.fakeUser = new UserModel()
            {
                Username = "Test",
                Email = "test@test.test",
                FirstName = "TesttTesttTesttTesttTesttTestt",
                LastName = "Testov",
                MoneyBalance = 12.34m,
                BoughtMovies = new List<MovieModel>()
                {
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie1",
                        Plot = "TestMovie1 Plot"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie2",
                        Plot = "TestMovie2 Plottttt"
                    },
                    new MovieModel()
                    {
                        Title = "TestMovie3",
                        Plot = "TestMovie3 Plotttttt"
                    }
                },
                LikedMovies = new List<MovieModel>(),
                Followers = new List<UserModel>(),
                Following = new List<UserModel>(),
                LikedParticipants = new List<ParticipantModel>()                
            };
        }

        public IList<MovieModel> Cart => new List<MovieModel>(this.cart);

        public UserModel FakeUser => this.fakeUser;

        public UserModel LoggedUser { get => loggedUser; private set => loggedUser = value; }

        public void Login(string email, string password)
        {
            var user = this.userService.GetUserByEmail(email);

            if (user == null)
            {
                throw new UserNotFoundException("A user with this email does not exist!");
            }

            if (user != null && user.Password.Equals(password))
            {
                this.LoggedUser = user;
            }
            else
            {
                this.LoggedUser = null;
                throw new UserPasswordsDoNotMatchException("Password is incorrect!");
            }
        }

        public void Logout()
        {
            this.LoggedUser = null;
        }

        public void AddToCart(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Invalid movie!");
            }

            this.cart.Add(movie);
        }

        public void RemoveFromCart(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Added movie to cart is null!");
            }

            this.cart.Remove(movie);
        }
    }
}
