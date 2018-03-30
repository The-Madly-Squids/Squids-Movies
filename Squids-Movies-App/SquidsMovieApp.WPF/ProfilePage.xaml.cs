using Autofac;
using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SquidsMovieApp.WPF
{
    public partial class ProfilePage : Page
    {
        private readonly AuthProvider authProvider;

        public ProfilePage(IMainController mainController, AuthProvider authProvider)
        {
            InitializeComponent();

            DataContext = this;
            this.authProvider = authProvider;
            GreetingName.Text = string.Format("Hello, {0}!", authProvider.FakeUser.Username);
            FillUserProfile();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string MoneyBalance { get; set; }
        public IList<ParticipantModel> LikedParticipants { get; set; }
        public IList<MovieModel> LikedMovies { get; set; }
        public IList<MovieModel> BoughtMovies { get; set; }
        public IList<ReviewModel> Reviews { get; set; }
        public IList<UserModel> Following { get; set; }
        public IList<UserModel> Followers { get; set; }

        private void FillUserProfile()
        {
            this.Username = this.authProvider.FakeUser.Username;
            this.Email = this.authProvider.FakeUser.Email;
            this.MoneyBalance = this.authProvider.FakeUser.MoneyBalance.ToString() + "$";

            // Firstname
            if (this.authProvider.FakeUser.FirstName != null)
            {
                this.FirstName = this.authProvider.FakeUser.FirstName;
            }
            else
            {
                this.FirstName = "N/A";
            }

            // Lastname
            if (this.authProvider.FakeUser.LastName != null)
            {
                this.LastName = this.authProvider.FakeUser.LastName;
            }
            else
            {
                this.LastName = "N/A";
            }

            // Age
            if (this.authProvider.FakeUser.Age != null)
            {
                this.Age = this.authProvider.FakeUser.Age;
            }
            else
            {
                this.Age = -1;
            }

            // Bought movies
            if (this.authProvider.FakeUser.BoughtMovies.Any())
            {
                foreach (var movie in this.authProvider.FakeUser.BoughtMovies)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = movie.Title;
                    hyperLink.Click += new RoutedEventHandler(this.MovieLinkClicked);
                    hyperLink.Inlines.Add(movie.Title);

                    this.BoughtMoviesLB.Items.Add(hyperLink);
                }
            }
            else
            {
                this.BoughtMoviesLB.Items.Add("No bought movies to show.");
            }

            // Liked movies
            if (this.authProvider.FakeUser.LikedMovies.Any())
            {
                foreach (var movie in this.authProvider.FakeUser.LikedMovies)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = movie.Title;
                    hyperLink.Click += new RoutedEventHandler(this.MovieLinkClicked);
                    hyperLink.Inlines.Add(movie.Title);

                    this.LikedMoviesLB.Items.Add(hyperLink);
                }
            }
            else
            {
                this.LikedMoviesLB.Items.Add("No liked movies to show.");
            }

            // Followers
            if (this.authProvider.FakeUser.Followers.Any())
            {
                foreach (var follower in this.authProvider.FakeUser.Followers)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = follower.Username;
                    hyperLink.Click += new RoutedEventHandler(this.FollowerLinkClicked);
                    hyperLink.Inlines.Add(follower.Username);

                    this.FollowersLB.Items.Add(hyperLink);
                }
            }
            else
            {
                this.FollowersLB.Items.Add("No followers to show.");
            }

            // Following
            if (this.authProvider.FakeUser.Following.Any())
            {
                foreach (var follower in this.authProvider.FakeUser.Following)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = follower.Username;
                    hyperLink.Click += new RoutedEventHandler(this.FollowerLinkClicked);
                    hyperLink.Inlines.Add(follower.Username);

                    this.FollowingLB.Items.Add(hyperLink);
                }
            }
            else
            {
                this.FollowingLB.Items.Add("You are not following anyone.");
            }

            // Liked participants
            if (this.authProvider.FakeUser.LikedParticipants.Any())
            {
                foreach (var participant in this.authProvider.FakeUser.LikedParticipants)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = participant.ParticipantId.ToString();
                    hyperLink.Click += new RoutedEventHandler(this.ParticipantLinkClicked);
                    hyperLink.Inlines.Add(participant.FirstName + " " + participant.LastName);

                    this.LikedParticipantsLB.Items.Add(hyperLink);
                }
            }
            else
            {
                this.LikedParticipantsLB.Items.Add("No actors or directors you like.");
            }
        }

        private void ParticipantLinkClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FollowerLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            //this.NavigationService.Navigate(new MovieInfoPage(hyperLinkName));
        }

        private void MovieLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new MovieInfoPage(hyperLinkName));
        }
    }
}
