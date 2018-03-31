using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using SquidsMovieApp.Core;
using SquidsMovieApp.Core.Factories;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Logic;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;

namespace SquidsMovieApp.WPF.AutofacModules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // cant be single instance - multiple users acces the DB at the same time
            builder.RegisterType<MovieAppDBContext>()
                .As<IMovieAppDBContext>()
                .InstancePerDependency();

            // services 
            builder.RegisterType<MovieService>()
                .As<IMovieService>()
                .InstancePerDependency();

            builder.RegisterType<RoleService>()
                .As<IRoleService>()
                .InstancePerDependency();

            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerDependency();

            builder.RegisterType<ParticipantService>()
                .As<IParticipantService>()
                .InstancePerDependency();

            // controllers
            builder.RegisterType<MovieController>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<RoleController>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<UserController>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<ParticipantController>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<MainController>()
                .As<IMainController>()
                .InstancePerDependency();

            // factories
            builder.RegisterType<MovieModelFactory>()
                .As<IMovieModelFactory>()
                .InstancePerDependency();

            builder.RegisterType<ParticipantModelFactory>()
               .As<IParticipantFactory>()
               .InstancePerDependency();

            builder.RegisterType<UserModelFactory>()
               .As<IUserFactory>()
               .InstancePerDependency();

            // common
            builder.RegisterType<UserContext>()
                .AsSelf()
                .SingleInstance();

            builder.Register(x => Mapper.Instance);

            // wpf
            //builder.RegisterType<MainWindow>().AsSelf();
            //builder.RegisterType<RegisterPage>().AsSelf();
            //builder.RegisterType<LoginPage>().AsSelf();
            //builder.RegisterType<ErrorWindow>().AsSelf();
            //builder.RegisterType<ProfilePage>().AsSelf();
        }
    }
}
