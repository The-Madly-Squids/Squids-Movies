using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.WPF.Controllers.Contracts
{
    public interface IMainController
    {
        MovieController MovieController { get; }
        ParticipantController ParticipantController { get; }
        RoleController RoleController { get; }
        UserController UserController { get; }
        GenreController GenreController { get; }
    }
}
