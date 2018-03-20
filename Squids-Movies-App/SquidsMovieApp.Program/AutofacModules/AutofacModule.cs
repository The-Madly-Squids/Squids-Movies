//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Autofac;
//using AutoMapper;
//using SquidsMovieApp.Core;
//using SquidsMovieApp.Core.Contracts;
//using SquidsMovieApp.Core.Factories;
//using SquidsMovieApp.Core.Factories.Contracts;
//using SquidsMovieApp.Data.Context;
//using SquidsMovieApp.Logic;
//using SquidsMovieApp.Logic.Contracts;
//using SquidsMovieApp.Program.Controllers;

//namespace SquidsMovieApp.Program
//{
//    public class AutofacModule : Module
//    {
//        protected override void Load(ContainerBuilder builder)
//        {
//            // cant be single instance - multiple users acces the DB at the same time
//            builder.RegisterType<MovieAppDBContext>()
//                .As<IMovieAppDBContext>()
//                .InstancePerDependency();

//            // engine
//            builder.RegisterType<MovieAppEngine>()
//                .As<IEngine>()
//                .InstancePerDependency();

//            // services 
//            builder.RegisterType<MovieService>()
//                .As<IMovieService>()
//                .InstancePerDependency();

//            builder.RegisterType<RoleService>()
//                .As<IRoleService>()
//                .InstancePerDependency();

//            // controllers
//            builder.RegisterType<MovieController>()
//                .AsSelf()
//                .InstancePerDependency();

//            builder.RegisterType<RoleController>()
//                .AsSelf()
//                .InstancePerDependency();

//            // factories
//            builder.RegisterType<MovieModelFactory>()
//                .As<IMovieModelFactory>()
//                .InstancePerDependency();

//            builder.Register(x => Mapper.Instance);
//        }
//    }
//}
