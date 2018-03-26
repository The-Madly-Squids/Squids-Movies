using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SquidsMovieApp.Tests.Service
{
    [TestClass]
    public class MovieServiceTests
    {
        [ClassInitialize]
        public static void InitilizeAutomapper(TestContext context)
        {
            AutomapperConfiguration.Initialize();
        }

        [TestMethod]
        public void AddMovieShouldCorrectlyAddMovieToDb_WhenCalledWithValidData()
        {
            // arrange
            var effortContext = new MovieAppDBContext(
                        Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var movieObjectToReturn = new Movie()
            {
                Title = "Test Movie Object",
                Runtime = 120
            };

            mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieModel>()))
                .Returns(movieObjectToReturn);

            var movieModelToAdd = new MovieModel()
            {
                Title = "Test Movie model",
                Runtime = 120
            };

            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            sut.AddMovie(movieModelToAdd);

            Assert.AreEqual(1, effortContext.Movies.Count());
        }

        [TestMethod]
        public void AddMovieShould_ThrowWhenCalledWithInvalidData()
        {
            // For Plamen
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RemoveMovieShould_CorrectlyRemoveMovieFromDB()
        {
            // Arrange
            var effortContext = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();

            var movieObjectToRemove = new Movie()
            {
                Title = "Test title",
                Runtime = 120
            };

            effortContext.Movies.Add(movieObjectToRemove);
            effortContext.SaveChanges();

            var movieDtoToRemove = Mapper.Map<MovieModel>(movieObjectToRemove);

            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            sut.RemoveMovie(movieDtoToRemove);

            //Assert
            Assert.AreEqual(0, effortContext.Movies.Count());
        }

        [TestMethod]
        public void RemoveMovieShould_ThrowWhenCalledWithInvalidData()
        {
            // For Plamen
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RemoveMovieShould_ThrowWhenMovieToRemoveNotFoundInDataBase()
        {
            // For Toni
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetAllMoviesShould_ReturnCorrectDataWhenCalled()
        {
            // Arrange
            var effortContext = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();

            for (var i = 0; i < 10; i++)
            {
                var movieToAdd = new Movie()
                {
                    Title = "Test title" + i,
                    Runtime = 120 + i
                };

                effortContext.Movies.Add(movieToAdd);
            }
            effortContext.SaveChanges();

            var moviesDTOsListToReturn = Mapper.Map<IList<MovieModel>>(
                    effortContext.Movies);

            mapperMock.Setup(x => x.Map<IList<MovieModel>>(
                                                    It.IsAny<IList<Movie>>()))
                .Returns(moviesDTOsListToReturn);


            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            var result = sut.GetAllMovies();

            // Assert
            foreach (var movieDto in result)
            {
                var exists = effortContext.Movies.Any(x => x.MovieId == movieDto.MovieId);
                Assert.IsTrue(exists);
            }

            // [OLD]
            // is this correct? Can you use ProjectTo in test method?
            // you cant: throws https://github.com/AutoMapper/AutoMapper/issues/2090
            //var expectedResult = effortContext.Movies.ProjectTo<MovieModel>();

            // rethrows https://github.com/AutoMapper/AutoMapper/issues/2090
            // why?
            // because of ProjectTo() - how to fix?
            //Assert.IsTrue(result.FirstOrDefault().Title == movieToAdd.Title);
        }

        [TestMethod]
        public void GetAllParticipantsPerMovieShould_ReturnCorrectParticipants()
        {
            // For Plamen
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddMovieParticipantShouldCorrectlyAddParticipantToMovie_WhenCalledWithValidData()
        {
            // Assert
            var effort = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();

            var movieObject = new Movie()
            {
                Title = "Test Movie",
                Year = 1990
            };

            var participantObject = new Participant()
            {
                FirstName = "Arnold",
                LastName = "Ivanov"
            };

            effort.Movies.Add(movieObject);
            effort.Participants.Add(participantObject);
            effort.SaveChanges();

            var participantModel = Mapper.Map<ParticipantModel>(participantObject);
            var movieModel = Mapper.Map<MovieModel>(movieObject);
            var roleName = "Actor";

            // Act
            var sut = new MovieService(effort, mapperMock.Object);
            sut.AddMovieParticipant(movieModel, participantModel, roleName);

            // Assert
            var participantAddedToMovie = movieObject.Participants.FirstOrDefault();
            var movieAddedToParticipant = participantObject.Movies.FirstOrDefault();
            var roleIsCorrect = effort.Roles.FirstOrDefault().RoleName == "Actor";

            // Unit test should fail for only one reason
            // either change the method to be SRP or separate the test into 3 parts
            Assert.IsTrue(participantAddedToMovie == participantObject);
            Assert.IsTrue(movieAddedToParticipant == movieObject);
            Assert.IsTrue(roleIsCorrect);
        }

        [TestMethod]
        public void AddMovieParticipantShould_ThrowWhenMovieNotFoundInDataBase()
        {
            // For Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddMovieParticipantShould_ThrowWhenParticipantNotFoundInDataBase()
        {
            // For Toni
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetRatingShould_ReturnCorrectValueWhenCalled()
        {
            // for Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetActorsShould_ReturnCorrectValueWhenCalled()
        {
            // Act
            var effort = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();

            var movieObject = new Movie()
            {
                Title = "Test Movie",
                Year = 1990
            };
            effort.Movies.Add(movieObject);

            for (int i = 0; i < 10; i++)
            {
                var participantObject = new Participant()
                {
                    FirstName = "Test" + i,
                    LastName = "Testov"
                };

                var roleObject = new Role()
                {
                    Movie = movieObject,
                    Participant = participantObject,
                    RoleName = "Actor"
                };

                effort.Participants.Add(participantObject);
                effort.Roles.Add(roleObject);
            }
            effort.SaveChanges();

            var participantDTOsListToReturn = Mapper.Map<IList<ParticipantModel>>(
                effort.Participants);

            var movieDtoArgument = Mapper.Map<MovieModel>(movieObject);

            mapperMock.Setup(x => x.Map<IList<ParticipantModel>>(
                                                    It.IsAny<IList<Participant>>()))
                .Returns(participantDTOsListToReturn);

            // Act
            var sut = new MovieService(effort, mapperMock.Object);
            var result = sut.GetActors(movieDtoArgument);

            // Assert
            foreach (var actorPoco in effort.Participants)
            {
                var exists = result.Any(x => x.ParticipantId == actorPoco.ParticipantId);
                Assert.IsTrue(exists);
            }
        }

        [TestMethod]
        public void GetDirectorsShould_ReturnCorrectValueWhenCalled()
        {
            // For Paco - look above test this one will be the same with minor changes

            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetMovieGenresShould_ReturnCorrectValueWhenCalled()
        {
            // for Toni
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetUsersWhoBoughtMovieShould_ReturnCorrectValueWhenCalled()
        {
            // for Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetUsersWhoLikedMovieShould_ReturnCorrectValueWhenCalled()
        {
            // for Toni
            throw new NotImplementedException();
        }
    }
}
