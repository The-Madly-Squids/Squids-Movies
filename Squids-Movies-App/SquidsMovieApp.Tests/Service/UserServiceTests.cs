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
            // Arrange
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();


            var userDtoArgument = new UserModel()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            var userObjectToReturn = new User()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
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
            // Arrange
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();


            var userDtoArgument = new UserModel()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                //Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            var userObjectToReturn = new User()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                //Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };
           
            mapperMock.Setup(x => x.Map<User>(It.IsAny<UserModel>()))
                        .Returns(userObjectToReturn);

            var sut = new UserService(effort, mapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => sut.AddUser(userDtoArgument));
        }

        [TestMethod]
        public void RemoveUserShould_CorrectlyRemoveUserFromDbWhenCalleWithValidData()
        {
            // Arrange
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObjectToReturn = new User()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            effort.Users.Add(userObjectToReturn);
            effort.SaveChanges();
            var userID = effort.Users
                .Where(x => x.Username == "HackMan")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userID,
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            mapperMock.Setup(x => x.Map<User>(It.IsAny<UserModel>()))
                        .Returns(userObjectToReturn);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.RemoveUser(userDtoArgument);
            effort.SaveChanges();

            // Assert
            Assert.AreEqual(0, effort.Users.Count());
        }

        [TestMethod]
        public void RemoveUserShould_ThrowWhenUserNotFoundInDataBase()
        {
            // Arrange
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObjectToReturn = new User()
            {
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            //effort.Users.Add(userObjectToReturn);
            //effort.SaveChanges();
            var userID = effort.Users
                .Where(x => x.Username == "HackMan")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                //UserId = userID,
                FirstName = "John",
                LastName = "Johnson",
                Username = "HackMan",
                Email = "partypooper1992@gmail.com",
                Password = "nikoganqmadapoznaesh"
            };

            mapperMock.Setup(x => x.Map<User>(It.IsAny<UserModel>()))
                        .Returns(userObjectToReturn);

            // Act
            var sut = new UserService(effort, mapperMock.Object);

            // Assert
            Assert.ThrowsException<System.NullReferenceException>(() => sut.RemoveUser(userDtoArgument));
        }

        [TestMethod]
        public void GetAllUsersShould_ReturnCorrectDataWhenCalled()
        {
            // method Includes ProjecTo() call - will throw exception
            // let it be for now
            // for Toni

            // Arrange
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            for (int i = 0; i < 10; i++)
            {
                var userObject = new User()
                {
                    FirstName = "Test" + i,
                    LastName = "Testove" + i,
                    Username = "Test" + i,
                    Email = "Test" + i + "@abv.com",
                    Password = "12345678"
                };

                effort.Users.Add(userObject);
            }

            effort.SaveChanges();
            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetAllUsers();

            // Assert
            foreach (var user in effort.Users)
            {
                var exists = result.Any(x => x.UserId == user.UserId);
                Assert.IsTrue(exists);
            }

        }

        [TestMethod]
        public void GetLikedParticipantsShould_ReturnCorrectResultWhenCalled()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678"
            };

            effort.Users.Add(userObject);

            for (int i = 0; i < 10; i++)
            {
                var participant = new Participant()
                {
                    FirstName = "John" + i,
                    LastName = "Johnson" + i
                };
                effort.Participants.Add(participant);
                userObject.LikedParticipants.Add(participant);
            }
            effort.SaveChanges();

            // replace with manual map
            var userDtoArgument = Mapper.Map<UserModel>(userObject);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetLikedParticipants(userDtoArgument);

            // Assert
            foreach (var p in userObject.LikedParticipants)
            {
                var exists = result.Any(x => x.ParticipantId == p.ParticipantId);
                Assert.IsTrue(exists);
            }
        }

        [TestMethod]
        public void GetUserShould_ReturnCorrectDataWhenCalledWithValidParmeters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetUserShould_ThrowWhenCalledWithInvalidParmeters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetLikedMovies_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetBoughtMovies_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetFollowers_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetFollowed_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetMoneyBalance_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddMoneyToBalanceShould_CorrectlyAddMoneyToUserWhenInvokedWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void LikeParticipantShould_CorrectlyAddParticipantWhenInvokedWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FollowUserShould_CorrectlyAddSelectedUserToFollowedCollectionWhenInvokedWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void BuyMovieShouldCorrectlyAddSelectedMovieToUserBoughtMoviesCollection_WhenInvokedWithValidParameters()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GiveReviewShouldCorrectlyAddReview_WhenInvokedWithValidParameters()
        {
            throw new NotImplementedException();
        }
    }
}
