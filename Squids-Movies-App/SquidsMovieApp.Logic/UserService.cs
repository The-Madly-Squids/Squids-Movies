using AutoMapper;
using AutoMapper.QueryableExtensions;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Logic
{
    class UserService : IUserService
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

            var userToRemove = Mapper.Map<User>(user);
            movieAppDbContext.Users.Remove(userToRemove);
            movieAppDbContext.SaveChanges();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = this.movieAppDbContext.Users.ProjectTo<UserModel>();
            return users;
        }

        public IEnumerable<ParticipantModel> GetLikedDirectors(UserModel user)
        {
            var likedDirectors = new List<ParticipantModel>();
            foreach (Participant director in user.LikedDirectors)
            {
                var directorModel = mapper.Map<ParticipantModel>(director);
                likedDirectors.Add(directorModel);
            }
            return likedDirectors;
        }

        public IEnumerable<ParticipantModel> GetLikedActors(UserModel user)
        {
            var likedActors = new List<ParticipantModel>();
            foreach (Participant actor in user.LikedActors)
            {
                var actorModel = mapper.Map<ParticipantModel>(actor);
                likedActors.Add(actorModel);
            }
            return likedActors;
        }

        public IEnumerable<MovieModel> GetLikedMovies(UserModel user)
        {
            var likedMovies = new List<MovieModel>();
            foreach (Movie movie in user.LikedMovies)
            {
                var movieModel = mapper.Map<MovieModel>(movie);
                likedMovies.Add(movieModel);
            }
            return likedMovies;
        }

        public IEnumerable<MovieModel> GetBoughtMovies(UserModel user)
        {
            var boughtMovies = new List<MovieModel>();
            foreach (Movie movie in user.BoughtMovies)
            {
                var movieModel = mapper.Map<MovieModel>(movie);
                boughtMovies.Add(movieModel);
            }
            return boughtMovies;
        }

        public IEnumerable<UserModel> GetFollowers(UserModel user)
        {
            var followers = new List<UserModel>();
            foreach (User follower in user.Followers)
            {
                var followerModel = mapper.Map<UserModel>(follower);
                followers.Add(followerModel);
            }
            return followers;
        }

        public IEnumerable<UserModel> GetFollowed(UserModel user)
        {
            var followed = new List<UserModel>();
            foreach (User followedOne in user.Following)
            {
                var followerModel = mapper.Map<UserModel>(followedOne);
                followed.Add(followerModel);
            }
            return followed;
        }

        public decimal GetMoneyBalance(UserModel user)
        {
            decimal moneyBallance = user.MoneyBalance;
            return moneyBallance;
        }

        public void AddMoneyToBalance(UserModel user, decimal amount)
        {
                if (amount == 0)
                {
                    throw new ArgumentException();
                }
                decimal moneyBalance = user.MoneyBalance;
                moneyBalance += amount;
                movieAppDbContext.SaveChanges();
        }

        public void LikeActor(UserModel user, ParticipantModel actor)
        {
            var actorToAdd = mapper.Map<Participant>(actor);
            user.LikedActors.Add(actorToAdd);
            movieAppDbContext.SaveChanges();
        }

        public void LikeDirector(UserModel user, ParticipantModel director)
        {
            var directorToAdd = mapper.Map<Participant>(director);
            user.LikedDirectors.Add(directorToAdd);
            movieAppDbContext.SaveChanges();
        }

        public void FollowUser(UserModel user, UserModel userToFollow)
        {
            var userToBeFollowed = mapper.Map<User>(userToFollow);
            user.Following.Add(userToBeFollowed);
            movieAppDbContext.SaveChanges();
        }

        public void BuyMovie(UserModel user, MovieModel movie, decimal price)
        {
            //In this case we should use transaction-like pattern
            try
            {
                if (user.MoneyBalance < price)
                {
                    throw new ArgumentException("Unsufficient money ballance to buy this movie!");
                }
                user.MoneyBalance -= price;
                Movie movieToAdd = mapper.Map<Movie>(movie);
                user.BoughtMovies.Add(movieToAdd);
                movieAppDbContext.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Sorry pich!");
            }
        }

        public void GiveReview(UserModel user, ReviewModel review, MovieModel movie)
        {

            Movie movieToReview = mapper.Map<Movie>(movie);
            review.Movie = movieToReview;

            var reviewToAdd = mapper.Map<Review>(review);
            user.Reviews.Add(reviewToAdd);

            movieAppDbContext.SaveChanges();
        }
    }
}
