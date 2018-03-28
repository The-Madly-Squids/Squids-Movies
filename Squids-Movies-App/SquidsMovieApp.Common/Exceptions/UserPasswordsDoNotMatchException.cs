using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Common.Exceptions
{
    public class UserPasswordsDoNotMatchException : UserException
    {
        public UserPasswordsDoNotMatchException()
        {
        }

        public UserPasswordsDoNotMatchException(string message) : base(message)
        {
        }

        public UserPasswordsDoNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
