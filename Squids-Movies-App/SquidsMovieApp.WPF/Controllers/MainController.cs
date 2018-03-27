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
            RoleController roleController, UserController userController)
        {
            MovieController = movieController;
            ParticipantController = participantController;
            RoleController = roleController;
            UserController = userController;
        }

        public MovieController MovieController { get; }
        public ParticipantController ParticipantController { get; }
        public RoleController RoleController { get; }
        public UserController UserController { get; }
    }
}
