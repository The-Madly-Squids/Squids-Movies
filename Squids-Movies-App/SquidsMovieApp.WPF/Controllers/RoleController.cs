using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.WPF.Controllers
{
    public class RoleController
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }
    }
}
