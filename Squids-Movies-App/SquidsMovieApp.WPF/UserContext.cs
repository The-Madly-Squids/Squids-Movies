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
        private IList<MovieModel> cart;

        public UserContext(IUserService userService)
        {
            this.userService = userService;
            this.LoggedUser = null;
            this.cart = new List<MovieModel>();
        }

        public IList<MovieModel> Cart => new List<MovieModel>(this.cart);
        
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

        public void RemoveFromCart(int movieIdToRemove)
        {
            if (movieIdToRemove < 1)
            {
                throw new ArgumentNullException("Invalid movie id!");
            }

            var movieToRemove = this.cart.Where(x => x.MovieId == movieIdToRemove).FirstOrDefault();

            if (movieToRemove == null)
            {
                throw new ArgumentNullException("Invalid movie!");
            }

            this.cart.Remove(movieToRemove);
        }

        public void RemoveAllFromCart()
        {
            this.cart.Clear();
        }
    }
}
