using AutoMapper;
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
                throw new ArgumentException();
            }

            var userToAdd = this.mapper.Map<User>(user);
            this.movieAppDbContext.Users.Add(userToAdd);
            this.movieAppDbContext.SaveChanges();
        }

        public void RemoveUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentException();
            }

            var userObject = this.movieAppDbContext.Users
                .Where(x => x.UserId == user.UserId)
                .FirstOrDefault();

            if (userObject == null)
            {
                throw new ArgumentNullException("No such user!");
            }

            this.movieAppDbContext.Users.Remove(userObject);
            this.movieAppDbContext.SaveChanges();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = this.movieAppDbContext.Users.ProjectTo<UserModel>();
            return users;
        }

        public UserModel GetUser(string userName)
        {
            var user = this.movieAppDbContext.Users
                .Where(x => x.Username == userName)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
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
                throw new ArgumentNullException("User not found!");
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
            decimal moneyBallance = user.MoneyBalance;
            return moneyBallance;
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
                throw new ArgumentNullException("User not found!");
            }

            decimal moneyBalance = userObject.MoneyBalance;
            moneyBalance += amount;
            movieAppDbContext.SaveChanges();
        }

        public void LikeParticipant(UserModel user, ParticipantModel participant)
        {
            //// [OLD]
            //var actorToAdd = mapper.Map<Participant>(actor);
            //user.LikedActors.Add(actorToAdd);
            //movieAppDbContext.SaveChanges();


            // possibly pass in the argument directly user/participant object
            // instead of going through GetX() method in controller which returns
            // DTO and then by ID here?
            var userObject = this.movieAppDbContext.Users
                            .Where(x => x.UserId == user.UserId)
                            .FirstOrDefault();

            var participantObject = this.movieAppDbContext.Participants
                              .Where(x => x.ParticipantId == participant.ParticipantId)
                              .FirstOrDefault();


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
                throw new ArgumentNullException("User that wants to follow not found!");
            }

            if (userToFollowObject == null)
            {
                throw new ArgumentNullException("User that will be followed not found!");
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
                throw new ArgumentNullException("User not found!");
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
    }
}
