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
using SquidsMovieApp.Common.Constants;

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

        public void CreateUser(string firstName, string lastName,
            string userName, string email, string password, int? age = GlobalConstants.MinUserAge, decimal moneyBalance = GlobalConstants.MinAmount, bool isAdmin = false)
        {
            Guard.WhenArgument(firstName, "user firstName")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(firstName.Length, "user firstName")
                .IsGreaterThan(GlobalConstants.MaxUserFirstNameLength)
                .Throw();

            Guard.WhenArgument(lastName, "user firstName")
               .IsNullOrEmpty()
               .Throw();

            Guard.WhenArgument(lastName.Length, "user firstName")
                .IsGreaterThan(GlobalConstants.MaxUserLastNameLength)
                .Throw();

            if (age != null && (age < GlobalConstants.MinUserAge || GlobalConstants.MaxUserAge < age))
            {
                throw new ArgumentException($"Age must be between {GlobalConstants.MinUserAge} and {GlobalConstants.MaxUserAge} years!");
            }

            Guard.WhenArgument(userName, "user firstName")
               .IsNullOrEmpty()
               .Throw();

            Guard.WhenArgument(userName.Length, "user firstName")
                .IsGreaterThan(GlobalConstants.MaxUserUsernameLength)
                .Throw();

            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                throw new ArgumentException("Incorrect email!");
            }

            Guard.WhenArgument(password.Length, "Too short password")
                .IsLessThan(GlobalConstants.MinUserPasswordLength)
                .Throw();

            Guard.WhenArgument(moneyBalance, "Incorrect money ballance")
                .IsLessThan(GlobalConstants.MinAmount)
                .Throw();

            var user = this.factory.CreateUserModel(firstName, lastName, age,
                                userName, email, password, isAdmin, moneyBalance);

            this.userService.AddUser(user);
        }

        public void RemoveUser(string email)
        {
            if (email == null)
            {
                throw new ArgumentException("Invalid email!");
            }

            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                throw new ArgumentException("Invali e-mail!");
            }

            UserModel userToRemove = this.userService.GetAllUsers()
            .Where(x => x.Email == email)
            .FirstOrDefault();

            if (userToRemove == null)
            {
                throw new ArgumentNullException("No such user by this e-mail!");
            }

            this.userService.RemoveUser(userToRemove);
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

            return this.userService.GetUser(userName);
        }

        public IEnumerable<ParticipantModel> GetLikedParticipants(string username)
        {
            Guard.WhenArgument(username, "username")
                .IsNotNullOrEmpty()
                .Throw();

            return this.userService.GetUser(username).LikedParticipants;
        }

        public IEnumerable<MovieModel> GetLikedMovies(string username)
        {
            Guard.WhenArgument(username, "username")
               .IsNotNullOrEmpty()
               .Throw();

            var userDto = this.userService.GetUser(username);
            return this.userService.GetLikedMovies(userDto);
        }

        public IEnumerable<MovieModel> GetBoughtMovies(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            return this.userService.GetUser(user).BoughtMovies;
        }

        public IEnumerable<UserModel> GetFollowers(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            return this.userService.GetUser(user).Followers;
        }

        public IEnumerable<UserModel> GetFollowed(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            return this.userService.GetUser(user).Following;
        }

        public decimal GetMoneyBalance(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            UserModel userToGetBalance = this.userService.GetUser(user);
            return this.userService.GetMoneyBalance(userToGetBalance);
        }

        public void AddMoneyToBalance(string user, decimal amount)
        {
            Guard.WhenArgument(user, "user Name")
                .IsNullOrEmpty()
                .Throw();
            Guard.WhenArgument(amount, "Money Problem!")
                .IsLessThanOrEqual(0)
                .Throw();
            UserModel userToAddMonney = this.userService.GetUser(user);
            this.userService.AddMoneyToBalance(userToAddMonney, amount);
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
            Guard.WhenArgument(userName, "User who follows")
                .IsNotNullOrEmpty()
                .Throw();
            Guard.WhenArgument(userToFollowUsername, "User to be followed")
            .IsNotNullOrEmpty()
            .Throw();

            UserModel userToBeFollowed = this.userService.GetUser(userName);
            UserModel userWhoFollows = this.userService.GetUser(userToFollowUsername);
            this.userService.FollowUser(userWhoFollows, userToBeFollowed);
        }
    }
}

