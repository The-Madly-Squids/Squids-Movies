﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Logic;

namespace SquidsMovieApp.Program
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // cant be single instance - multiple users acces the DB at the same time
            builder.RegisterType<MovieAppDBContext>()
                .As<IMovieAppDBContext>()
                .InstancePerDependency();
            builder.RegisterType<MovieService>().AsSelf();
        }
    }
}