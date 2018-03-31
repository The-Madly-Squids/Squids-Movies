using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class MovieInfoPage : Page
    {
        private IMainController mainController;
        private UserContext userContext;
        private string moneyBalance;
        private MovieModel movie;
        private readonly string movieId;
        private double squidFlixRating;

        public MovieInfoPage(IMainController mainController, UserContext userContext, string movieId)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;
            this.movieId = movieId;

            this.GreetingName.Text = string.Format("Hello, {0}!", userContext.FakeUser.Username);
            //fix
            this.MoneyBalance = userContext.FakeUser.MoneyBalance.ToString();
            this.SearchTBox.Focus();
            GetMovieToDisplay(movieId);

            FillMovieInfoPage();
        }

        public string MoneyBalance
        {
            get => string.Format("{0} $", this.moneyBalance);
            set => this.moneyBalance = value;
        }

        private void FillMovieInfoPage()
        {
            this.MoviePosterImg.Source = LoadImage(this.movie.Poster.Poster);

            this.MovieTitleTBlock.Text = string.Format("{0} ({1})", this.movie.Title, this.movie.Year);
            this.MovieImdbRatingTBlock.Text = string.Format("Imdb rating: {0}", this.movie.ImdbRating);
            this.MovieSquidFlixRatingTBlock.Text = string.Format("SquidFlix rating: {0}", squidFlixRating);
            var movieGenresObjs = mainController.MovieController.GetMovieGenres(this.movie);
            var movieGenres = new List<string>();

            foreach (var genreObj in movieGenresObjs)
            {
                movieGenres.Add(genreObj.GenreType);
            }

            var tb = new TextBlock()
            {
                Text = string.Join(" | ", movieGenres),
                FontSize = 20
            };

            this.MovieGenresSP.Children.Add(tb);
            this.MovieRuntimeTBlock.Text = string.Format("{0} min.", this.movie.Runtime);
            this.MovieRatedTBlock.Text = this.movie.Rated;
            this.MoviePlotTBlock.Text = this.movie.Plot;
            this.MoviePriceTBlock.Text = string.Format("${0}", this.movie.Price);
        }

        private void GetMovieToDisplay(string id)
        {
            var movieId = int.Parse(id.Split('_')[1]);
            this.movie = this.mainController.MovieController.GetMovieById(movieId);
            this.squidFlixRating = this.mainController.MovieController.GetRating(this.movie.Title);

        }

        private ImageSource LoadImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
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
    }
}
