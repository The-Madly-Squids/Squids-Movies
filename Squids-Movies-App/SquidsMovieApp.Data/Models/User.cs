using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Contracts;
using SquidsMovieApp.Data.Models.Abstract;
using Bytes2you.Validation;


namespace SquidsMovieApp.Data.Models
{
    public partial class User : Person, IUser
    {
        public User(string firstName, string lastName, int age)
            : base(firstName, lastName, age)
        {

        }

       
        public ICollection<Participant> LikedActors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<Participant> LikedDirectors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<Movie> LikedMovies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<Movie> BoughtMovies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<User> Following { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<User> Followers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Nickname
        {
            get
            {
                return this.Nickname;
            }
          set
            {
                
            }
        }

        public string Email
        {
            get
            {
                return this.Email;
            }
          set
            {
                var addr = new System.Net.Mail.MailAddress(value);

                if (addr.Address == value)
                {
                    this.Email = value;
                }
                else
                {
                    throw new ArgumentException("Email not correct!");
                }
            }
        }

        public string Password
        { get => throw new NotImplementedException();
          set => throw new NotImplementedException();
        }
        public int MoneyBalance
        { get => throw new NotImplementedException();
          set => throw new NotImplementedException();
        }
    }
}
