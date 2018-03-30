using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Common.Constants;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SquidsMovieApp.WPF.Controllers
{
    public class UserController
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

        public void CreateUser(string userName, string email, string password,
            string firstName = null, string lastName = null,
            int? age = GlobalConstants.MinUserAge,
            decimal moneyBalance = GlobalConstants.MinAmount,
            bool isAdmin = false)
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
                .IsLessThan(GlobalConstants.MinUserUsernameLength)
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

        public void RegisterUser(string userName, string email, string password)
        {
            Guard.WhenArgument(userName, "user firstName")
               .IsNullOrEmpty()
               .Throw();

            Guard.WhenArgument(userName.Length, "user firstName")
                .IsGreaterThan(GlobalConstants.MaxUserUsernameLength)
                .IsLessThan(GlobalConstants.MinUserUsernameLength)
                .Throw();

            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                throw new ArgumentException("Incorrect email!");
            }

            Guard.WhenArgument(password.Length, "Too short password")
                .IsLessThan(GlobalConstants.MinUserPasswordLength)
                .Throw();

            var user = this.factory.CreateUserModel(null, null, GlobalConstants.MinUserAge,
                                userName, email, password, false, GlobalConstants.MinAmount);

            this.userService.AddUser(user);
        }

        public void RemoveUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentException("Invalid email!");
            }

            UserModel userToRemove = this.userService.GetUserByUsername(username);

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

        public UserModel GetUserByUsername(string userName)
        {
            Guard.WhenArgument(userName, "username")
                .IsNullOrEmpty()
                .Throw();

            return this.userService.GetUserByUsername(userName);
        }

        public UserModel GetUserByEmail(string email)
        {
            Guard.WhenArgument(email, "email")
                .IsNullOrEmpty()
                .Throw();

            return this.userService.GetUserByEmail(email);
        }

        public IEnumerable<ParticipantModel> GetLikedParticipants(string username)
        {
            Guard.WhenArgument(username, "username")
                .IsNullOrEmpty()
                .Throw();

            UserModel user = this.userService.GetUserByUsername(username);
            return this.userService.GetLikedParticipants(user);
        }

        public IEnumerable<MovieModel> GetLikedMovies(string username)
        {
            Guard.WhenArgument(username, "username")
               .IsNullOrEmpty()
               .Throw();

            var userDto = this.userService.GetUserByUsername(username);
            return this.userService.GetLikedMovies(userDto);
        }

        public IEnumerable<MovieModel> GetBoughtMovies(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            UserModel userDto = this.userService.GetUserByUsername(user);
            return this.userService.GetBoughtMovies(userDto);
        }

        public IEnumerable<UserModel> GetFollowers(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            UserModel userDto = this.userService.GetUserByUsername(user);
            return this.userService.GetFollowers(userDto);
        }

        public IEnumerable<UserModel> GetFollowed(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            UserModel userDto = this.userService.GetUserByUsername(user);
            return this.userService.GetFollowed(userDto);
        }

        public decimal GetMoneyBalance(string user)
        {
            Guard.WhenArgument(user, "userName")
                .IsNullOrEmpty()
                .Throw();

            UserModel userToGetBalance = this.userService.GetUserByUsername(user);
            return this.userService.GetMoneyBalance(userToGetBalance);
        }

        public void AddMoneyToBalance(string userName, decimal amount)
        {
            Guard.WhenArgument(userName, "user Name")
                .IsNullOrEmpty()
                .Throw();
            Guard.WhenArgument(amount, "Money Problem!")
                .IsLessThanOrEqual(0)
                .Throw();
            UserModel userToAddMonney = this.userService.GetUserByUsername(userName);
            this.userService.AddMoneyToBalance(userToAddMonney, amount);
        }

        public void LikeParticipant(string userName, string participantFirstName,
            string participantLastName)
        {
            Guard.WhenArgument(userName, "username")
                .IsNullOrEmpty()
                .Throw();

            var userDto = this.userService.GetUserByUsername(userName);

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
                .IsNullOrEmpty()
                .Throw();
            Guard.WhenArgument(userToFollowUsername, "User to be followed")
            .IsNullOrEmpty()
            .Throw();

            UserModel userToBeFollowed = this.userService.GetUserByUsername(userName);
            UserModel userWhoFollows = this.userService.GetUserByUsername(userToFollowUsername);
            this.userService.FollowUser(userWhoFollows, userToBeFollowed);
        }

        public void EditUserFirstName(UserModel user, string newName)
        {
            Guard.WhenArgument(user, "user")
                .IsNull()
                .Throw();

            Guard.WhenArgument(newName, "new first name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(newName.Length, "User first name length")
                .IsLessThan(GlobalConstants.MinUserFirstNameLength)
                .IsGreaterThan(GlobalConstants.MaxUserFirstNameLength)
                .Throw();

            this.userService.EditUserFirstName(user, newName);
        }

        public void EditUserLastName(UserModel user, string newName)
        {
            Guard.WhenArgument(user, "user")
                .IsNull()
                .Throw();

            Guard.WhenArgument(newName, "new last name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(newName.Length, "User last name length")
                .IsLessThan(GlobalConstants.MinUserLastNameLength)
                .IsGreaterThan(GlobalConstants.MaxUserLastNameLength)
                .Throw();

            this.userService.EditUserLastName(user, newName);
        }

        public IEnumerable<UserModel> FindUsersByUsername(string pattern)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentNullException("Invalid search pattern!");
            }

            return this.userService.FindUsersByUsername(pattern);
        }
    }
}

