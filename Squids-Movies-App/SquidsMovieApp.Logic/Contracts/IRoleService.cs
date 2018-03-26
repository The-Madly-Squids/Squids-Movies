using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IRoleService
    {
        void AddRole(MovieModel movie, ParticipantModel participant, string roleName);
        IEnumerable<Role> ParticipantRolesPerMovie(ParticipantModel participant,
            MovieModel movie);
    }
}
