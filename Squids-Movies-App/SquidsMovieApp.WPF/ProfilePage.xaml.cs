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
        private readonly IMainController mainController;
        private readonly AuthProvider authProvider;

        public ProfilePage(IMainController mainController, AuthProvider authProvider)
        {
            InitializeComponent();

            DataContext = this;
            this.mainController = mainController;
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

        private void FillFakeUserProfile()
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
                    hyperLink.TextDecorations = null;
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
                    hyperLink.TextDecorations = null;
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
                    hyperLink.TextDecorations = null;
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
                    hyperLink.TextDecorations = null;
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
                    hyperLink.TextDecorations = null;
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

        private void FillUserProfile()
        {
            this.Username = this.authProvider.LoggedUser.Username;
            this.Email = this.authProvider.LoggedUser.Email;
            this.MoneyBalance = this.authProvider.LoggedUser.MoneyBalance.ToString() + "$";

            // Firstname
            if (this.authProvider.LoggedUser.FirstName != null)
            {
                this.FirstName = this.authProvider.LoggedUser.FirstName;
            }
            else
            {
                this.FirstName = "N/A";
            }

            // Lastname
            if (this.authProvider.LoggedUser.LastName != null)
            {
                this.LastName = this.authProvider.LoggedUser.LastName;
            }
            else
            {
                this.LastName = "N/A";
            }

            // Age
            if (this.authProvider.LoggedUser.Age != null)
            {
                this.Age = this.authProvider.LoggedUser.Age;
            }
            else
            {
                this.Age = -1;
            }

            // Bought movies
            if (this.authProvider.LoggedUser.BoughtMovies.Any())
            {
                foreach (var movie in this.authProvider.LoggedUser.BoughtMovies)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = movie.Title;
                    hyperLink.TextDecorations = null;
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
            if (this.authProvider.LoggedUser.LikedMovies.Any())
            {
                foreach (var movie in this.authProvider.LoggedUser.LikedMovies)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = movie.Title;
                    hyperLink.TextDecorations = null;
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
            if (this.authProvider.LoggedUser.Followers.Any())
            {
                foreach (var follower in this.authProvider.LoggedUser.Followers)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = follower.Username;
                    hyperLink.TextDecorations = null;
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
            if (this.authProvider.LoggedUser.Following.Any())
            {
                foreach (var follower in this.authProvider.LoggedUser.Following)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = follower.Username;
                    hyperLink.TextDecorations = null;
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
            if (this.authProvider.LoggedUser.LikedParticipants.Any())
            {
                foreach (var participant in this.authProvider.LoggedUser.LikedParticipants)
                {
                    var hyperLink = new Hyperlink();
                    hyperLink.Name = participant.ParticipantId.ToString();
                    hyperLink.TextDecorations = null;
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

        private void EditFirstNameClicked(object sender, RoutedEventArgs e)
        {
            if (this.EditFirstNameTB.Visibility != Visibility.Visible)
            {
                this.EditFirstNameTB.Visibility = Visibility.Visible;
                this.SaveFirstNameBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.EditFirstNameTB.Visibility = Visibility.Hidden;
                this.SaveFirstNameBtn.Visibility = Visibility.Hidden;
            }
        }

        private void SaveFirstNameClicked(object sender, RoutedEventArgs e)
        {
            var newName = EditFirstNameTB.Text;
            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
            {
                var stackPanel = new StackPanel();
                var err = this.CreateErrorTextBlock("First name cannot be empty or whitespace!");
                stackPanel.Children.Add(err);
                this.DisplayError(stackPanel);
            }

            mainController.UserController.EditUserFirstName(authProvider.LoggedUser, newName);
            this.FirstName = newName;
            this.FirstNameTBlock.Text = newName;
            EditFirstNameClicked(sender, e);
        }

        private void EditLastNameClicked(object sender, RoutedEventArgs e)
        {
            if (this.EditLastNameTB.Visibility != Visibility.Visible)
            {
                this.EditLastNameTB.Visibility = Visibility.Visible;
                this.SaveLastNameBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.EditLastNameTB.Visibility = Visibility.Hidden;
                this.SaveLastNameBtn.Visibility = Visibility.Hidden;
            }
        }

        private void SaveLastNameClicked(object sender, RoutedEventArgs e)
        {
            var newName = EditLastNameTB.Text;

            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
            {
                var stackPanel = new StackPanel();
                var err = this.CreateErrorTextBlock("Last name cannot be empty or whitespace!");
                stackPanel.Children.Add(err);
                this.DisplayError(stackPanel);
            }

            mainController.UserController.EditUserLastName(authProvider.LoggedUser, newName);
            this.LastName = newName;
            this.LastNameTBlock.Text = newName;
            EditLastNameClicked(sender, e);
        }

        private void AddMoneyClicked(object sender, RoutedEventArgs e)
        {

        }

        private void ParticipantLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = int.Parse((sender as Hyperlink).Name);
            //this.NavigationService.Navigate(new MovieInfoPage(hyperLinkName));
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

        private void DisplayError(StackPanel stackPanel)
        {
            var errorWindow = new ErrorWindow(stackPanel)
            {
                Owner = Application.Current.MainWindow,
                ErrorName = "Editting failed."
            };

            errorWindow.ShowDialog();
        }

        private TextBlock CreateErrorTextBlock(string errorText)
        {
            var errorTextBlock = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.Red),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Text = errorText
            };

            return errorTextBlock;
        }
    }
}
