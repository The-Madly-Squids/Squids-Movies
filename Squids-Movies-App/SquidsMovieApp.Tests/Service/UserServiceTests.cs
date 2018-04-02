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
using System.Collections.Generic;

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

            List<UserModel> userList = new List<UserModel>();

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

                var userToList = new UserModel()
                {
                    FirstName = "Test" + i,
                    LastName = "Testove" + i,
                    Username = "Test" + i,
                    Email = "Test" + i + "@abv.com",
                    Password = "12345678"
                };

                userList.Add(userToList);
            }

            effort.SaveChanges();


            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetAllUsers();

            // Assert
            //foreach (var user in result)
            //{
            //    var exists = userList.Any(x => x.Username == user.Username);
            //    Assert.IsTrue(exists);
            //}
            Assert.AreEqual(10, result.Count());

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
            effort.SaveChanges();

            var userDtoArgument = Mapper.Map<UserModel>(userObject);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetUserByUsername("Test");

            // Assert
            Assert.AreEqual("Test", result.Username);

        }

        [TestMethod]
        public void GetUserShould_ThrowWhenCalledWithInvalidParmeters()
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
            effort.SaveChanges();

            var userDtoArgument = Mapper.Map<UserModel>(userObject);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);

            // Assert
            Assert.ThrowsException<NullReferenceException>(() => sut.GetUserByUsername("Test_wrong"));
        }

        [TestMethod]
        public void GetLikedMovies_ShouldReturnCorrectDataWhenCaledWithValidParameters()
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
            effort.SaveChanges();

            List<Movie> likedMovies = new List<Movie>();
            for (int i = 0; i < 10; i++)
            {
                var movie = new Movie()
                {
                    Title = "Terminator" + i,
                    Runtime = 150
                };
                userObject.LikedMovies.Add(movie);
                effort.SaveChanges();
                likedMovies.Add(movie);
            }
            var userDtoArgument = Mapper.Map<UserModel>(userObject);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetLikedMovies(userDtoArgument);
            // Assert
            foreach (var movie in result)
            {
                bool areSame = likedMovies.Any(x => x.Title == movie.Title);
                Assert.IsTrue(areSame);
            }
        }

        [TestMethod]
        public void GetBoughtMovies_ShouldReturnCorrectDataWhenCaledWithValidParameters()
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
            effort.SaveChanges();

            List<Movie> boughtMovies = new List<Movie>();
            for (int i = 0; i < 10; i++)
            {
                var movie = new Movie()
                {
                    Title = "Terminator" + i,
                    Runtime = 150
                };
                userObject.BoughtMovies.Add(movie);
                effort.SaveChanges();
                boughtMovies.Add(movie);
            }
            var userDtoArgument = Mapper.Map<UserModel>(userObject);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetBoughtMovies(userDtoArgument);
            // Assert
            foreach (var movie in result)
            {
                bool areSame = boughtMovies.Any(x => x.Title == movie.Title);
                Assert.IsTrue(areSame);
            }
        }

        [TestMethod]
        public void GetFollowers_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678"
            };


            effort.Users.Add(userObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678"
            };

            List<User> followersList = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                var user = new User()
                {
                    FirstName = "Test" + i,
                    LastName = "Testove" + i,
                    Username = "Test" + i,
                    Email = "Test" + i + "@abv.com",
                    Password = "12345678"
                };

                effort.Users.Add(user);
                userObject.Followers.Add(user);
                followersList.Add(user);
                effort.SaveChanges();
            }
            //var userDtoArgument = Mapper.Map<UserModel>(userObject);
            //mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
            //  .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var results = sut.GetFollowers(userDtoArgument);
            // Assert
            foreach (var user in results)
            {
                bool areSame = followersList.Any(x => x.Username == user.Username);
                Assert.IsTrue(areSame);
            }
        }

        [TestMethod]
        public void GetFollowed_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678"
            };


            effort.Users.Add(userObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678"
            };

            List<User> followedList = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                var user = new User()
                {
                    FirstName = "Test" + i,
                    LastName = "Testove" + i,
                    Username = "Test" + i,
                    Email = "Test" + i + "@abv.com",
                    Password = "12345678"
                };

                effort.Users.Add(user);
                userObject.Following.Add(user);
                followedList.Add(user);
                effort.SaveChanges();
            }
            //var userDtoArgument = Mapper.Map<UserModel>(userObject);
            //mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
            //  .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var results = sut.GetFollowed(userDtoArgument);
            // Assert
            foreach (var user in results)
            {
                bool areSame = followedList.Any(x => x.Username == user.Username);
                Assert.IsTrue(areSame);
            }
        }

        [TestMethod]
        public void GetMoneyBalance_ShouldReturnCorrectDataWhenCaledWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };


            effort.Users.Add(userObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetMoneyBalance(userDtoArgument);
            // Assert
            Assert.AreEqual(result, userDtoArgument.MoneyBalance);
        }

        [TestMethod]
        public void AddMoneyToBalanceShould_CorrectlyAddMoneyToUserWhenInvokedWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };


            effort.Users.Add(userObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.AddMoneyToBalance(userDtoArgument, 5000M);
            //var money = sut.GetMoneyBalance(userDtoArgument);
            // Assert
            Assert.AreEqual(6000M, userObject.MoneyBalance);
        }

        [TestMethod]
        public void LikeParticipantShould_CorrectlyAddParticipantWhenInvokedWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            var participantObject = new Participant()
            {
                FirstName = "participantFirstName",
                LastName = "participantLastName",
                Age = 30
            };

            effort.Users.Add(userObject);
            effort.Participants.Add(participantObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            var participantIdToAdd = effort.Participants
                .Where(x => x.FirstName == "participantFirstName")
                .FirstOrDefault()
                .ParticipantId;

            var participantDtoArgument = new ParticipantModel()
            {
                ParticipantId = participantIdToAdd,
                FirstName = "participantFirstName",
                LastName = "participantLastName",
                Age = 30
            };
           
            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.LikeParticipant(userDtoArgument, participantDtoArgument);
            var likedParticipant = effort.Users
                .Where(x => x.UserId == userIdToAdd)
                .FirstOrDefault()
                .LikedParticipants
                .FirstOrDefault();
            // Assert
            Assert.AreEqual(likedParticipant.FirstName, participantDtoArgument.FirstName);
        }

        [TestMethod]
        public void FollowUserShould_CorrectlyAddSelectedUserToFollowedCollectionWhenInvokedWithValidParameters()
        {
            // Act
            var effort = new MovieAppDBContext(
                                Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var userObject = new User()
            {
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            effort.Users.Add(userObject);
            effort.SaveChanges();

            var userIdToAdd = effort.Users
                .Where(x => x.Username == "TestOne")
                .FirstOrDefault()
                .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "TestOne",
                Email = "Test@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            var userToFollow = new User()
            {
                FirstName = "TestFollow",
                LastName = "TestoveFollow",
                Username = "TestOneFollow",
                Email = "TestFollow@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            effort.Users.Add(userToFollow);
            effort.SaveChanges();

            var userToFollowIdToAdd = effort.Users
                .Where(x => x.Username == "TestOneFollow")
                .FirstOrDefault()
                .UserId;

            var userToFollowDtoArgument = new UserModel()
            {
                UserId = userToFollowIdToAdd,
                FirstName = "TestFollow",
                LastName = "TestoveFollow",
                Username = "TestOneFollow",
                Email = "TestFollow@abv.com",
                Password = "12345678",
                MoneyBalance = 1000
            };

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.FollowUser(userDtoArgument, userToFollowDtoArgument);
            var followedUser = effort.Users
                .Where(x => x.UserId == userDtoArgument.UserId)
                .FirstOrDefault()
                .Following
                .FirstOrDefault();
            // Assert
            Assert.AreEqual(followedUser.UserId, userToFollowDtoArgument.UserId);

        }

        [TestMethod]
        public void BuyMovieShouldCorrectlyAddSelectedMovieToUserBoughtMoviesCollection_WhenInvokedWithValidParameters()
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
            effort.SaveChanges();

            List<Movie> boughtMovies = new List<Movie>();
            for (int i = 0; i < 10; i++)
            {
                var movie = new Movie()
                {
                    Title = "Terminator" + i,
                    Runtime = 150
                };
                userObject.BoughtMovies.Add(movie);
                effort.SaveChanges();
                boughtMovies.Add(movie);
            }
            var userDtoArgument = Mapper.Map<UserModel>(userObject);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            var result = sut.GetBoughtMovies(userDtoArgument);
            // Assert
            foreach (var movie in result)
            {
                bool areSame = boughtMovies.Any(x => x.Title == movie.Title);
                Assert.IsTrue(areSame);
            }
        }

        [TestMethod]
        public void GiveReviewShouldCorrectlyAddReview_WhenInvokedWithValidParameters()
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
            effort.SaveChanges();

            var userIdToAdd = effort.Users
               .Where(x => x.Username == "Test")
               .FirstOrDefault()
               .UserId;

            var userDtoArgument = new UserModel()
            {
                UserId = userIdToAdd,
                FirstName = "Test",
                LastName = "Testove",
                Username = "Test",
                Email = "Test@abv.com",
                Password = "12345678"
            };

            var movie = new Movie()
                {
                    Title = "Terminator",
                    Runtime = 150
                };
                userObject.BoughtMovies.Add(movie);
                effort.SaveChanges();
         
            //var userDtoArgument = Mapper.Map<UserModel>(userObject);
            var movieDtoArgument = Mapper.Map<MovieModel>(movie);
            mapperMock.Setup(x => x.Map<UserModel>(It.IsAny<User>()))
              .Returns(userDtoArgument);

            // Act
            var sut = new UserService(effort, mapperMock.Object);
            sut.GiveReview(userDtoArgument, movieDtoArgument, 10, "Very good movie!");
            // Assert
            var review = effort.Reviews.FirstOrDefault();
            Assert.AreEqual(review.UserId, userDtoArgument.UserId);
        }
    }
}
