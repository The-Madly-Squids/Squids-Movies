using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Core.Contracts
{
    public interface IEngine
    {
        void Start(string email, string password);
    }
}
