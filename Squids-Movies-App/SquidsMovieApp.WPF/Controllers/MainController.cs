using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.WPF.Controllers
{
    public class MainController : IMainController
    {
        public MainController(MovieController movieController, ParticipantController participantController,
            RoleController roleController, UserController userController, GenreController genreController)
        {
            this.MovieController = movieController;
            this.ParticipantController = participantController;
            this.RoleController = roleController;
            this.UserController = userController;
            this.GenreController = genreController;
        }

        public MovieController MovieController { get; }
        public ParticipantController ParticipantController { get; }
        public RoleController RoleController { get; }
        public UserController UserController { get; }
        public GenreController GenreController { get; }
    }
}
