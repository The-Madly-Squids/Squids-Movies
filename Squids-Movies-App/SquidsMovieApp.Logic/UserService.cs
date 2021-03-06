﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using SquidsMovieApp.Common.Constants;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Common.Exceptions;

namespace SquidsMovieApp.Logic
{
    public class UserService : IUserService
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private readonly IMapper mapper;

        public UserService(IMovieAppDBContext movieAppDbContext, IMapper mapper)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.mapper = mapper;
        }

        public void AddUser(UserModel user)
        {
            if (user == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var userToAdd = this.mapper.Map<User>(user);
            this.movieAppDbContext.Users.Add(userToAdd);
            this.movieAppDbContext.SaveChanges();
        }

        public void RemoveUser(UserModel user)
        {
            if (user == null)
            {
                throw new UserNotFoundException("User argument not found!");
            }

            var userObject = this.movieAppDbContext.Users
                .Where(x => x.UserId == user.UserId)
                .FirstOrDefault();

            if (userObject == null)
            {
                throw new UserNotFoundException("User not found in DB!");
            }

            this.movieAppDbContext.Users.Remove(userObject);
            this.movieAppDbContext.SaveChanges();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = this.movieAppDbContext.Users.ProjectTo<UserModel>();
            return users;
        }

        public UserModel GetUserByUsername(string userName)
        {
            var user = this.movieAppDbContext.Users
                .Where(x => x.Username == userName)
                .FirstOrDefault();

            if (user == null)
            {
                throw new NullReferenceException("No user found with that username!");
            }

            var userDto = this.mapper.Map<UserModel>(user);

            return userDto;
        }

        public UserModel GetUserByEmail(string email)
        {
            var user = this.movieAppDbContext.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (user == null)
            {
                throw new UserNotFoundException("A user with this email does not exist!");
            }

            var userDto = this.mapper.Map<UserModel>(user);

            return userDto;
        }

        public IEnumerable<ParticipantModel> GetLikedParticipants(UserModel user)
        {
            var likedParticipants = user.LikedParticipants;
            return likedParticipants;
        }

        public IEnumerable<MovieModel> GetLikedMovies(UserModel user)
        {
            var likedMovies = user.LikedMovies;
            return likedMovies;
        }

        public IEnumerable<MovieModel> GetBoughtMovies(UserModel user)
        {
            var boughtMovies = user.BoughtMovies;
            return boughtMovies;
        }

        public IEnumerable<UserModel> GetFollowers(UserModel user)
        {
            var followers = user.Followers;
            return followers;
        }

        public IEnumerable<UserModel> GetFollowed(UserModel user)
        {
            var followedUsers = user.Following;
            return followedUsers;
        }

        public decimal GetMoneyBalance(UserModel user)
        {
            var userPoco = Mapper.Map<User>(user);

            decimal moneyBalance = userPoco.MoneyBalance;
            return moneyBalance;
        }

        public void AddMoneyToBalance(UserModel user, decimal amount)
        {
            if (amount < GlobalConstants.MinAmountToAdd)
            {
                throw new ArgumentException("Amount cannot be less then 1!");
            }
            var userObject = this.movieAppDbContext.Users
                                .Where(x => x.UserId == user.UserId)
                                .FirstOrDefault();

            if (userObject == null)
            {
                throw new UserNotFoundException("User not found!");
            }
            
            userObject.MoneyBalance += amount;
            movieAppDbContext.SaveChanges();
        }

        public void LikeParticipant(UserModel user, ParticipantModel participant)
        {
            var userObject = this.movieAppDbContext.Users
                            .Where(x => x.UserId == user.UserId)
                            .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            var participantObject = this.movieAppDbContext.Participants
                              .Where(x => x.ParticipantId == participant.ParticipantId)
                              .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("Participant not found!");
            }

            if (userObject.LikedParticipants.Any(p => p.ParticipantId == participantObject.ParticipantId))
            {
                throw new ArgumentException("Participant already liked!");
            }

            userObject.LikedParticipants.Add(participantObject);
            participantObject.ParticipantLikedByUser.Add(userObject);
            this.movieAppDbContext.SaveChanges();
        }

        public void FollowUser(UserModel user, UserModel userToFollow)
        {
            var userObject = this.movieAppDbContext.Users
                             .Where(x => x.UserId == user.UserId)
                             .FirstOrDefault();

            var userToFollowObject = this.movieAppDbContext.Users
                                     .Where(x => x.UserId == userToFollow.UserId)
                                     .FirstOrDefault();

            if (userObject == null)
            {
                throw new UserNotFoundException("User that wants to follow not found!");
            }

            if (userToFollowObject == null)
            {
                throw new UserNotFoundException("User that will be followed not found!");
            }

            userObject.Following.Add(userToFollowObject);
            userToFollowObject.Followers.Add(userObject);
            this.movieAppDbContext.SaveChanges();
        }

        public void BuyMovie(UserModel user, MovieModel movie, decimal price)
        {

            var userObject = this.movieAppDbContext.Users
                            .Where(x => x.UserId == user.UserId)
                            .FirstOrDefault();

            var movieObject = this.movieAppDbContext.Movies
                              .Where(x => x.MovieId == movie.MovieId)
                              .FirstOrDefault();

            if (userObject == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            if (movieObject == null)
            {
                throw new ArgumentNullException("Movie not found!");
            }

            //In this case we should use transaction-like pattern
            try
            {
                if (userObject.MoneyBalance < price)
                {
                    throw new ArgumentException("Unsufficient money ballance to buy this movie!");
                }
                userObject.MoneyBalance -= price;
                userObject.BoughtMovies.Add(movieObject);
                movieObject.BoughtBy.Add(userObject);
                movieAppDbContext.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Sorry pich!");
            }
        }

        public void GiveReview(UserModel user, MovieModel movie, int reviewRating,
            string reviewDescription)
        {

            var userObject = this.movieAppDbContext.Users
                                .Where(x => x.UserId == user.UserId)
                                .FirstOrDefault();

            var movieObject = this.movieAppDbContext.Movies
                                .Where(x => x.MovieId == movie.MovieId)
                                .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("user not found!");
            }

            if (movieObject == null)
            {
                throw new ArgumentNullException("movie not found!");
            }

            var reviewObject = new Review()
            {
                Movie = movieObject,
                MovieId = movieObject.MovieId,
                Rating = reviewRating,
                Description = reviewDescription,
                User = userObject,
                UserId = userObject.UserId
            };

            this.movieAppDbContext.Reviews.Add(reviewObject);
            this.movieAppDbContext.SaveChanges();

        }

        public void EditUserFirstName(UserModel user, string newName)
        {
            var userObject = this.movieAppDbContext.Users
                                .Where(x => x.UserId == user.UserId)
                                .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("user not found!");
            }

            userObject.FirstName = newName;
            this.movieAppDbContext.SaveChanges();
        }

        public void EditUserLastName(UserModel user, string newName)
        {
            var userObject = this.movieAppDbContext.Users
                                .Where(x => x.UserId == user.UserId)
                                .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("user not found!");
            }

            userObject.LastName = newName;
            this.movieAppDbContext.SaveChanges();
        }

        public IEnumerable<UserModel> SearchUsersByUsername(string pattern)
        {
            var usersPoco = this.movieAppDbContext.Users
                              .Where(x => x.Username.Contains(pattern))
                              .ToList();

            var usersDto = mapper.Map<IList<UserModel>>(usersPoco);

            return usersDto;
        }

        public void RemoveMoneyFromBalance(UserModel user, decimal amount)
        {
            if (amount < GlobalConstants.MinAmountToAdd)
            {
                throw new ArgumentException($"Amount cannot be less than {GlobalConstants.MinAmountToAdd}!");
            }

            var userPoco = this.movieAppDbContext.Users
                                .Where(x => x.UserId == user.UserId)
                                .FirstOrDefault();

            if (userPoco == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var moneyAfterBuying = userPoco.MoneyBalance - amount;

            if (moneyAfterBuying < 0)
            {
                throw new InvalidOperationException("Insufficient money!");
            }

            userPoco.MoneyBalance = moneyAfterBuying;
            movieAppDbContext.SaveChanges();
        }

        public void LikeMovie(UserModel user, MovieModel movie)
        {
            var userObject = this.movieAppDbContext.Users
                           .Where(x => x.UserId == user.UserId)
                           .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("User not found");
            }

            var movieObject = this.movieAppDbContext.Movies
                              .Where(x => x.MovieId == movie.MovieId)
                              .FirstOrDefault();

            if (movieObject == null)
            {
                throw new ArgumentNullException("Movie not found");
            }

            var movieAlreadyLiked = userObject.LikedMovies
                .Where(x => x.MovieId == movieObject.MovieId)
                .FirstOrDefault();

            if (movieAlreadyLiked != null)
            {
                throw new ArgumentNullException("Movie already liked");
            }


            userObject.LikedMovies.Add(movieObject);
            movieObject.LikedBy.Add(userObject);
            this.movieAppDbContext.SaveChanges();
        }

        public void BuyMovie(UserModel user, MovieModel movie)
        {
            var userObject = this.movieAppDbContext.Users
                           .Where(x => x.UserId == user.UserId)
                           .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            var movieObject = this.movieAppDbContext.Movies
                              .Where(x => x.MovieId == movie.MovieId)
                              .FirstOrDefault();

            if (movieObject == null)
            {
                throw new ArgumentNullException("Movie not found!");
            }

            var movieAlreadyBought = userObject.BoughtMovies
                .Where(x => x.MovieId == movieObject.MovieId)
                .FirstOrDefault();

            if (movieAlreadyBought != null)
            {
                throw new ArgumentNullException("Movie already bought!");
            }

            userObject.BoughtMovies.Add(movieObject);
            movieObject.BoughtBy.Add(userObject);
            this.movieAppDbContext.SaveChanges();
        }
    }
}
