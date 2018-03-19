using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Core.Factories.Contracts;

namespace SquidsMovieApp.Program.Controllers
{
    class UserController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IUserFactory factory;
    }
}
