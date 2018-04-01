using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class SearchResultPage : Page
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;
        private LoadingWindow loadingWindow;
        private BackgroundWorker worker;
        private string patternToSearch;
        private IEnumerable<MovieModel> foundMovies;
        private IEnumerable<ParticipantModel> foundParticipants;
        private IEnumerable<UserModel> foundUsers;
        private string moneyBalance;

        public SearchResultPage(IMainController mainController, UserContext userContext, string patternToSearch)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;
            this.patternToSearch = patternToSearch;
            
            this.GreetingName.Text = string.Format("Hello, {0}!", userContext.FakeUser.Username);
            this.SearchResultTBlock.Text = string.Format("\"{0}\"", this.patternToSearch);
            this.MoneyBalance = userContext.FakeUser.MoneyBalance.ToString();
            this.SearchTBox.Focus();

            this.loadingWindow = new LoadingWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            this.worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
            loadingWindow.ShowDialog();
        }

        public string MoneyBalance
        {
            get => string.Format("{0} $", this.moneyBalance);
            set => this.moneyBalance = value;
        }

        private void DisplayMoviesSearchResult()
        {
            if (this.foundMovies.Any())
            {
                foreach (var movie in this.foundMovies)
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

                    this.FoundMoviesSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No movies found"
                };
                this.FoundMoviesSP.Children.Add(tb);
            }
        }

        private void DisplayParticipantsSearchResult()
        {
            if (this.foundParticipants.Any())
            {
                foreach (var participant in this.foundParticipants)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = string.Format("Id_{0}", participant.ParticipantId.ToString()),
                        TextDecorations = null,
                        FontSize = 15
                    };
                    hyperLink.Click += new RoutedEventHandler(this.ParticipantLinkClicked);
                    hyperLink.Inlines.Add(string.Format("{0} {1}", participant.FirstName, participant.LastName));

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    this.FoundActorsAndDirectorsSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No actors or directors found"
                };
                this.FoundActorsAndDirectorsSP.Children.Add(tb);
            }
        }

        private void DispkayUsersSearchResult()
        {
            if (this.foundUsers.Any())
            {
                foreach (var user in this.foundUsers)
                {
                    var tb = new TextBlock();

                    var hyperLink = new Hyperlink
                    {
                        Name = string.Format("Id_{0}", user.UserId.ToString()),
                        TextDecorations = null,
                        FontSize = 15
                    };
                    hyperLink.Click += new RoutedEventHandler(this.UserLinkClicked);
                    hyperLink.Inlines.Add(user.Username);

                    tb.Inlines.Add(hyperLink);
                    tb.Padding = new Thickness(5);

                    this.FoundUsersSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No users found"
                };
                this.FoundUsersSP.Children.Add(tb);
            }
        }
                
        private void ParticipantLinkClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UserLinkClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MovieLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new MovieInfoPage(this.mainController, this.userContext, hyperLinkName));
        }

        private void ProfileBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProfilePage(this.mainController, this.userContext));
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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.foundMovies = mainController.MovieController.SearchForMoviesByTitle(patternToSearch);
            this.foundParticipants = mainController.ParticipantController.FindParticipantsByNames(patternToSearch);
            this.foundUsers = mainController.UserController.FindUsersByUsername(patternToSearch);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingWindow.Close();
            var stackPanel = new StackPanel();

            if (e.Error != null)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock(e.Error.Message));
                ErrorDialog.DisplayError(stackPanel, "Search failed.");
            }
            else
            {
                DisplayMoviesSearchResult();
                DisplayParticipantsSearchResult();
                DispkayUsersSearchResult();
            }
        }
    }
}
