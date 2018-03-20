using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.Core.Contracts
{
    public interface IUserContext
    {
        User CurrentUser { get; }

        IList<string> Permissions { get; }
    }
}
