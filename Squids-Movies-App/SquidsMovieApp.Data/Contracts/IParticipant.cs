using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Contracts
{
    interface IParticipant
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
        //int Rating { get; set; }

    }
}
