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
    public partial class BrowseMoviesPage : Page
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;
        private LoadingWindow loadingWindow;
        private BackgroundWorker worker;
        private string moneyBalance;
        private IEnumerable<GenreModel> allGenres;
        private IEnumerable<MovieModel> allMovies;

        public BrowseMoviesPage(IMainController mainController, UserContext userContext)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.userContext = userContext;
            DataContext = this;
            this.GreetingName.Text = string.Format("Hello, {0}!", userContext.LoggedUser.Username);
            this.MoneyBalance = userContext.LoggedUser.MoneyBalance.ToString();
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

        private void FillGenres()
        {
            foreach (var genre in this.allGenres)
            {
                this.GenresFilterCB.Items.Add(genre.GenreType);
            }
        }

        private void FillMovieGrid()
        {
            int gridCol = 0;
            int gridRow = 1;

            var stack = new StackPanel();

            foreach (var movie in this.allMovies)
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
                tb.SetValue(Grid.RowProperty, gridRow);
                tb.SetValue(Grid.ColumnProperty, gridCol);

                this.MovieDisplayGrid.Children.Add(tb);

                gridCol++;

                if (gridCol > 2)
                {
                    this.MovieDisplayGrid.RowDefinitions.Add(new RowDefinition());
                    gridRow++;
                    gridCol = 0;
                }
                
            }
        }

        private void MovieLinkClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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

        private void CartBtnClicked(object sender, RoutedEventArgs e)
        {
            var cart = new CartWindow(this.mainController, this.userContext)
            {
                Owner = Application.Current.MainWindow
            };

            cart.ShowDialog();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.allGenres = this.mainController.GenreController.GetAllGenres();
            this.allMovies = this.mainController.MovieController.GetAllMovies();
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
                FillGenres();
                FillMovieGrid();
            }
        }

        private void LogoutBtnClicked(object sender, RoutedEventArgs e)
        {
            this.userContext.Logout();
            this.NavigationService.Navigate(new LoginPage());
        }

        private void ExitBtnClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
