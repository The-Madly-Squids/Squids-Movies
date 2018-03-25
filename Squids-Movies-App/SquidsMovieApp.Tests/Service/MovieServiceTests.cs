using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
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

            var movieDtoToRemove = new MovieModel()
            {
                Title = "Test title",
                Runtime = 120
            };

            var movieObjectToReturn = new Movie()
            {
                Title = "Test title",
                Runtime = 120
            };

            mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieModel>()))
               .Returns(movieObjectToReturn);
            // Is this the correct way to simulate adding a movie in DB?
            // mock somehow ?
            effortContext.Movies.Add(movieObjectToReturn);
            effortContext.SaveChanges();

            // act
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


            var movieToAdd = new Movie()
            {
                Title = "Test title",
                Runtime = 120
            };

            effortContext.Movies.Add(movieToAdd);

            effortContext.SaveChanges();

            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            var result = sut.GetAllMovies();

            // [OLD]
            // is this correct? Can you use ProjectTo in test method?
            // you cant: throws https://github.com/AutoMapper/AutoMapper/issues/2090
            //var expectedResult = effortContext.Movies.ProjectTo<MovieModel>();

            // rethrows https://github.com/AutoMapper/AutoMapper/issues/2090
            // why?
            // because of ProjectTo() - how to fix?
            Assert.IsTrue(result.FirstOrDefault().Title == movieToAdd.Title);
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

            // how to isolate mapper ?
            // map here is broken - participantModel id is 0 
            var participantModel = Mapper.Map<ParticipantModel>(participantObject);
            var movieModel = Mapper.Map<MovieModel>(movieObject);
            var roleName = "Actor";

            // Act
            var sut = new MovieService(effort, mapperMock.Object);
            // once passed the participantModel/Object whatever loses its existance
            // becomes null. Debug to see;
            // Note: only happens for participantObject, movie is unaffected
            // nvm forget { set;} on ParticipantModel 
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
            // currently does not work - see end of test for details
            // Act
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
                FirstName = "Test",
                LastName = "Testov"
            };

            effort.Movies.Add(movieObject);
            effort.Participants.Add(participantObject);

            var roleObject = new Role()
            {
                Movie = movieObject,
                MovieId = movieObject.MovieId,
                Participant = participantObject,
                ParticipantId = participantObject.ParticipantId,
                RoleName = "Actor"
            };

            effort.Roles.Add(roleObject);
            effort.SaveChanges();

            var movieDtoArgument = Mapper.Map<MovieModel>(movieObject);

            // Act
            var sut = new MovieService(effort, mapperMock.Object);
            var result = sut.GetActors(movieDtoArgument).FirstOrDefault();

            // Assert
            // Again ProjecTo leads to 
            // : The type 'SquidsMovieApp.DTO.ParticipantModel' appears in two structurally
            // incompatible initializations within a single LINQ to Entities query
            Assert.AreEqual(result.FirstName, participantObject.FirstName);
        }

        [TestMethod]
        public void GetDirectorsShould_ReturnCorrectValueWhenCalled()
        {
            // For Paco - look above test this one will be the same with minor changes
            // above doesnt work - wait until we ask for help from the Trainers

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
