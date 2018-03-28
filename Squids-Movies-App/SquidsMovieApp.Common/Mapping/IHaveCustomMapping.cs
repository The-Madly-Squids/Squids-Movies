using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SquidsMovieApp.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
