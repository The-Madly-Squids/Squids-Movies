using SquidsMovieApp.DTO;
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
    public partial class UserPreviewPage : Page
    {
        private IMainController mainController;
        private UserContext userContext;
        private UserModel userToShow;
        private string moneyBalance;

        public UserPreviewPage(IMainController mainController, UserContext userContext, string userNameToShow)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;

            GreetingName.Text = string.Format("Hello, {0}!", userContext.LoggedUser.Username);
            this.userToShow = this.mainController.UserController.GetUserByUsername(userNameToShow);

            FillUserProfile();
        }

        public string MoneyBalance
        {
            get => string.Format("{0} $", this.moneyBalance);
            set => this.moneyBalance = value;
        }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private void FillUserProfile()
        {
            this.Username = this.userToShow.Username;
            this.MoneyBalance = this.userContext.LoggedUser.MoneyBalance.ToString();

            // Firstname
            if (this.userToShow.FirstName != null)
            {
                this.FirstName = this.userToShow.FirstName;
            }
            else
            {
                this.FirstName = "N/A";
            }

            // Lastname
            if (this.userToShow.LastName != null)
            {
                this.LastName = this.userToShow.LastName;
            }
            else
            {
                this.LastName = "N/A";
            }

            this.MovieLikedTBlock.Text = $"Movies {this.userToShow.Username} likes";
            this.FollowersTBlock.Text = $"{this.userToShow.Username} followers";
            this.FollowsTBlock.Text = $"People {this.userToShow.Username} follows";
            this.ParticipantsLikedTBlock.Text = $"Actors and directors {this.userToShow.Username} follows";

            // Liked movies
            GetLikedMovies();

            // Followers
            GetFollowers();

            // Following
            GetFollowing();

            // Liked participants
            GetLikedParticipants();

            //Follow button
            if (this.userContext.LoggedUser.UserId == userToShow.UserId ||
                this.userContext.LoggedUser.Following.Any(x => x.UserId == this.userToShow.UserId) ||
                this.mainController.UserController.GetUserByUsername(this.userContext.LoggedUser.Username).Following.Any(x => x.UserId == this.userToShow.UserId))
            {
                this.FollowBtn.IsEnabled = false;
            }
        }

        private void SearchBtnClicked(object sender, RoutedEventArgs e)
        {
            var searchFieldText = this.SearchTBox.Text;

            if (string.IsNullOrWhiteSpace(searchFieldText) || string.IsNullOrEmpty(searchFieldText))
            {
                return;
            }

            this.NavigationService.Navigate(new SearchResultPage(this.mainController, this.userContext, searchFieldText));
        }

        private void ProfileBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProfilePage(this.mainController, this.userContext));
        }

        private void CartBtnClicked(object sender, RoutedEventArgs e)
        {
            var cart = new CartWindow(this.mainController, this.userContext)
            {
                Owner = Application.Current.MainWindow
            };

            cart.ShowDialog();
        }

        private void FollowBtnClicked(object sender, RoutedEventArgs e)
        {
            this.FollowBtn.IsEnabled = false;

            this.mainController.UserController.FollowUser(this.userContext.LoggedUser.Username, this.userToShow.Username);
        }

        private void GetLikedMovies()
        {
            var likedMovies = this.mainController.UserController.GetLikedMovies(this.userToShow.Username);

            if (likedMovies.Any())
            {
                foreach (var movie in likedMovies)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = string.Format("Id_{0}", movie.MovieId.ToString()),
                        TextDecorations = null,
                        FontSize = 15
                    };

                    hyperLink.Click += new RoutedEventHandler(this.MovieLinkClicked);
                    hyperLink.Inlines.Add(movie.Title);

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    this.LikedMoviesSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No liked movies to show."
                };

                this.LikedMoviesSP.Children.Add(tb);
            }
        }

        private void GetFollowers()
        {
            var followers = this.mainController.UserController.GetFollowers(this.userToShow.Username);

            if (followers.Any())
            {
                foreach (var follower in followers)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = follower.Username,
                        TextDecorations = null,
                        FontSize = 15
                    };

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    hyperLink.Click += new RoutedEventHandler(this.FollowerLinkClicked);
                    hyperLink.Inlines.Add(follower.Username);

                    this.FollowersSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No followers to show."
                };

                this.FollowersSP.Children.Add(tb);
            }
        }

        private void GetFollowing()
        {
            var following = this.mainController.UserController.GetFollowed(this.userToShow.Username);

            if (following.Any())
            {
                foreach (var follower in following)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = follower.Username,
                        TextDecorations = null,
                        FontSize = 15
                    };

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    hyperLink.Click += new RoutedEventHandler(this.FollowerLinkClicked);
                    hyperLink.Inlines.Add(follower.Username);

                    this.FollowingSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "Nothing to show."
                };

                this.FollowingSP.Children.Add(tb);
            }
        }

        private void GetLikedParticipants()
        {
            var likedParticipants = this.mainController.UserController.GetLikedParticipants(this.userToShow.Username);

            if (likedParticipants.Any())
            {
                foreach (var participant in likedParticipants)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = string.Format("Id_{0}", participant.ParticipantId.ToString()),
                        TextDecorations = null,
                        FontSize = 15
                    };

                    hyperLink.Click += new RoutedEventHandler(this.ParticipantLinkClicked);
                    hyperLink.Inlines.Add(participant.FirstName + " " + participant.LastName);

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    this.LikedParticipantsSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No actors or directors liked."
                };

                this.LikedParticipantsSP.Children.Add(tb);
            }
        }

        private void ParticipantLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = int.Parse((sender as Hyperlink).Name);
            //this.NavigationService.Navigate(new MovieInfoPage(hyperLinkName));
        }

        private void FollowerLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new UserPreviewPage(this.mainController, this.userContext, hyperLinkName));
        }

        private void MovieLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new MovieInfoPage(this.mainController, this.userContext, hyperLinkName));
        }
    }
}
