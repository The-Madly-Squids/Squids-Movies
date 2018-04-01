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
    public partial class ParticipantPreviewPage : Page
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;
        private ParticipantModel participantToShow;
        private string moneyBalance;

        public ParticipantPreviewPage(IMainController mainController, UserContext userContext, string participantIdToShow)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.userContext = userContext;
            
            int id = int.Parse(participantIdToShow.Split('_')[1]);
            this.participantToShow = this.mainController.ParticipantController.GetParticipantById(id);

            FillInfo();
        }

        public string MoneyBalance
        {
            get => string.Format("{0} $", this.moneyBalance);
            set => this.moneyBalance = value;
        }

        private void FillInfo()
        {
            this.GreetingName.Text = string.Format("Hello, {0}!", userContext.LoggedUser.Username);
            this.MoneyBalance = userContext.LoggedUser.MoneyBalance.ToString();
            this.SearchTBox.Focus();

            this.FullnameTBlock.Text = string.Format("{0} {1}", this.participantToShow.FirstName, this.participantToShow.LastName);
            this.BioTBlock.Text = this.participantToShow.Bio;

            if (this.userContext.LoggedUser.LikedParticipants.Any(x => x.ParticipantId == this.participantToShow.ParticipantId) ||
                this.mainController.UserController.GetUserByUsername(this.userContext.LoggedUser.Username).LikedParticipants.Any(x => x.ParticipantId == this.participantToShow.ParticipantId))
            {
                this.FollowBtn.IsEnabled = false;
            }

            GetParticipantMovies();
            GetParticipantFollowers(false);
        }

        private void GetParticipantMovies()
        {
            var roles = this.mainController.ParticipantController.GetAllMoviesPerParticipantById(this.participantToShow.ParticipantId);

            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = string.Format("Id_{0}", role.Movie.MovieId.ToString()),
                        TextDecorations = null,
                        FontSize = 15
                    };

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    hyperLink.Click += new RoutedEventHandler(this.MovieLinkClicked);
                    hyperLink.Inlines.Add(role.Movie.Title);

                    this.MoviesSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No movies yet."
                };

                this.MoviesSP.Children.Add(tb);
            }
        }

        private void GetParticipantFollowers(bool removeItems)
        {
            var followers = this.mainController.ParticipantController.GetParticipantFollowers(this.participantToShow.ParticipantId);

            if (removeItems)
            {
                this.FollowersSP.Children.Clear();
            }

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
                    Text = "No followers yet."
                };

                this.FollowersSP.Children.Add(tb);
            }
        }

        private void FollowerLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new UserPreviewPage(this.mainController, this.userContext, hyperLinkName));
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

            this.mainController.UserController.LikeParticipant(this.userContext.LoggedUser.Username, this.participantToShow.FirstName, this.participantToShow.LastName);
            this.GetParticipantFollowers(true);
        }

        private void MovieLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new MovieInfoPage(this.mainController, this.userContext, hyperLinkName));
        }
    }
}
