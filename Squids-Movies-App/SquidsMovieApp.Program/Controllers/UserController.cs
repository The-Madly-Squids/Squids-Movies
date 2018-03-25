using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Core.Factories.Contracts;
using Bytes2you.Validation;

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
            string userName, string email, string password, int? age = 0, int moneyBalance = 0, bool isAdmin = false)
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

            if (age != null && age > 1200)
            {
                throw new ArgumentException("Age is over 1200 years!");
            }

            Guard.WhenArgument(userName, "user firstName")
               .IsNullOrEmpty()
               .Throw();

            Guard.WhenArgument(userName.Length, "user firstName")
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
             userName, email, password, isAdmin, moneyBalance);

            userService.AddUser(user);
        }
    }
}

