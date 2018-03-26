using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquidsMovieApp.Data.Context;
using Effort;
using AutoMapper;
using Moq;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;
using System.Linq;
using SquidsMovieApp.Models;

namespace SquidsMovieApp.Tests.Service
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void AddUserShould_CorrectlyAddUserToDataBaseWhenCalledWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();


            var userDtoArgument = new UserModel()
            {
                FirstName = "John",
                LastName = "Johnson",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            var userObjectToReturn = new User()
            {
                FirstName = "John",
                LastName = "Johnson",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            mapperMock.Setup(x => x.Map<User>(It.IsAny<UserModel>()))
                        .Returns(userObjectToReturn);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.AddUser(userDtoArgument);

            // Assert
            Assert.AreEqual(1, effort.Users.Count());
        }

        [TestMethod]
        public void AddUserShould_ThrowWhenCalledWithInvalidParameters()
        {
            // for Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RemoveUserShould_CorrectlyRemoveUserFromDbWhenCalleWithValidData()
        {
            // for Toni
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RemoveUserShould_ThrowWhenUserNotFoundInDataBase()
        {
            // for Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetAllUsersShould_ReturnCorrectDataWhenCalled()
        {
            // method Includes ProjecTo() call - will throw exception
            // let it be for now
            // for Toni

            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetLikedParticipantsShould_ReturnCorrectResultWhenCalled()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userDtoArgument = new UserModel()
            {
                FirstName = "John",
                LastName = "Johnson",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.GetLikedParticipants(userDtoArgument);

            // Assert
            Assert.AreEqual(0, userDtoArgument);
        }
    }
}
