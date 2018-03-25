using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Core.Factories.Contracts;
using Bytes2you.Validation;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Program.Controllers
{
    class UserController
    {
        private readonly IUserService userService;
        private readonly IParticipantService participantService;
        private readonly IMapper mapper;
        private readonly IUserFactory factory;

        public UserController(IUserService userService, IParticipantService participantService,
            IMapper mapper, IUserFactory factory)
        {
            this.userService = userService;
            this.participantService = participantService;
            this.mapper = mapper;
            this.factory = factory;
        }

        public void CreateUser(string firstName, string lastName, int? age,
            string nickName, string email, string password, bool isAdmin, int moneyBalance)
        {
            Guard.WhenArgument(firstName, "user firstName")
                .IsNullOrEmpty()
                .Throw();
            Guard.WhenArgument(firstName.Length, "user firstName")
                .IsGreaterThan(20)
                .Throw();
            Guard.WhenArgument(lastName, "user firstName")
               .IsNullOrEmpty()
               .Throw();
            Guard.WhenArgument(lastName.Length, "user firstName")
                .IsGreaterThan(20)
                .Throw();
            if (age > 1200)
            {
                throw new ArgumentException("Age is over 1200 years!");
            }
            Guard.WhenArgument(nickName, "user firstName")
               .IsNullOrEmpty()
               .Throw();
            Guard.WhenArgument(nickName.Length, "user firstName")
                .IsGreaterThan(20)
                .Throw();
            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                throw new ArgumentException("Incorrect email!");
            }
            Guard.WhenArgument(password.Length, "Too short password")
                .IsLessThan(2)
                .Throw();
            Guard.WhenArgument(password.Length, "Too short password")
           .IsLessThan(2)
           .Throw();
            Guard.WhenArgument(password.Length, "Too long password")
           .IsGreaterThan(30)
           .Throw();
            Guard.WhenArgument(moneyBalance, "Incorrect money ballance")
            .IsLessThan(0)
            .Throw();

            var user = this.factory.CreateUserModel(firstName, lastName, age,
             nickName, email, password, isAdmin, moneyBalance);
            userService.AddUser(user);
        }

        public void RemoveUser(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return this.userService.GetAllUsers();
        }

        public UserModel GetUser(string userName)
        {
            Guard.WhenArgument(userName, "username")
                .IsNullOrEmpty()
                .Throw();

            var userDto = this.userService.GetUser(userName);

            return userDto;
        }

        public IEnumerable<ParticipantModel> GetLikedParticipants(string username)
        {
            Guard.WhenArgument(username, "username")
                .IsNotNullOrEmpty()
                .Throw();

            var userDto = this.userService.GetUser(username);
            var likedParticipants = userDto.LikedParticipants;

            return likedParticipants;

        }

        public IEnumerable<MovieModel> GetLikedMovies(string username)
        {
            Guard.WhenArgument(username, "username")
               .IsNotNullOrEmpty()
               .Throw();

            var userDto = this.userService.GetUser(username);
            var likedMovies = userDto.LikedMovies;

            return likedMovies;
        }

        public IEnumerable<MovieModel> GetBoughtMovies(string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetFollowers(string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetFollowed(string user)
        {
            throw new NotImplementedException();
        }

        public decimal GetMoneyBalance(string user)
        {
            throw new NotImplementedException();
        }

        public void AddMoneyToBalance(string user, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void LikeParticipant(string userName, string participantFirstName,
            string participantLastName)
        {
            Guard.WhenArgument(userName, "username")
                .IsNullOrEmpty()
                .Throw();

            var userDto = this.userService.GetUser(userName);

            Guard.WhenArgument(participantFirstName, "participant first name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(participantLastName, "participant last name")
                .IsNullOrEmpty()
                .Throw();

            var participantDto = this.participantService.GetParticipant(
                participantFirstName, participantLastName);

            this.userService.LikeParticipant(userDto, participantDto);
        }

        public void FollowUser(string userName, string userToFollowUsername)
        {
            throw new NotImplementedException();
        }

    }
}

