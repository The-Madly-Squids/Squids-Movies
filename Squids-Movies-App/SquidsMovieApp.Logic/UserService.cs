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
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = this.movieAppDbContext.Users.ProjectTo<UserModel>();
            return users;
        }

        public IEnumerable<ParticipantModel> GetLikedParticipants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieModel> GetLikeddMovies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieModel> GetBoughtMovies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetFollowers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetFollowed()
        {
            throw new NotImplementedException();
        }

        public decimal GetMoneyBalance()
        {
            throw new NotImplementedException();
        }

        public void AddMoneyToBalance(decimal amount)
        {
            throw new NotImplementedException();
        }

        public void LikeParticipant(ParticipantModel participant)
        {
            throw new NotImplementedException();
        }

        public void FollowUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public void BuyMovie(MovieModel movie)
        {
            throw new NotImplementedException();
        }

        public void GiveReview(MovieModel movie)
        {
            throw new NotImplementedException();
        }
    }
}
