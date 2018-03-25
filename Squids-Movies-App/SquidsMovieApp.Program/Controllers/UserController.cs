using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Core.Factories.Contracts;
using Bytes2you.Validation;
using SquidsMovieApp.Common.Constants;

namespace SquidsMovieApp.Program.Controllers
{
    class UserController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IUserFactory factory;

        public UserController(IUserService userService, IMapper mapper, IUserFactory factory)
        {
            this.userService = userService;
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

            userService.AddUser(user);
        }

        public void RemoveUser(string email)
        {
            throw new NotImplementedException();
        }

        public void GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void GetLikedParticipants(string username)
        {

        }
    }
}

