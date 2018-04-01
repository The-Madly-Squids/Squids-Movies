using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private int movieId;
        private double squidFlixRating;
        private IEnumerable<GenreModel> movieGenresObjs;
        private IEnumerable<ReviewModel> movieReviews;
        private LoadingWindow loadingWindow;
        private BackgroundWorker worker;

        public MovieInfoPage(IMainController mainController, UserContext userContext, string movieId)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;

            this.GreetingName.Text = string.Format("Hello, {0}!", userContext.LoggedUser.Username);
            this.MoneyBalance = userContext.LoggedUser.MoneyBalance.ToString();
            this.SearchTBox.Focus();
            this.movieId = int.Parse(movieId.Split('_')[1]);

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

        private void FillMovieInfoPage()
        {
            // Image
            this.MoviePosterImg.Source = LoadImage(this.movie.Poster.Poster);

            // Basic info
            this.MovieTitleTBlock.Text = string.Format("{0} ({1})", this.movie.Title, this.movie.Year);
            this.MovieImdbRatingTBlock.Text = string.Format("Imdb rating: {0}", this.movie.ImdbRating);
            this.MovieSquidFlixRatingTBlock.Text = string.Format("SquidFlix rating: {0}", squidFlixRating);

            // Genres
            var movieGenres = new List<string>();

            foreach (var genreObj in this.movieGenresObjs)
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

            // Reviews
            DisplayReviews(false);

            // Likes + btn
            UpdateLikeCount();

            // Add to cart btn
            if (this.userContext.Cart.Any(x=>x.MovieId == this.movie.MovieId))
            {
                this.AddToCartBtn.IsEnabled = false;
            }

            if (this.mainController.UserController.GetBoughtMovies(this.userContext.LoggedUser.Username).Any(m => m.MovieId == this.movie.MovieId))
            {
                this.AddToCartBtn.IsEnabled = false;
            }
        }

        private void DisplayReviews(bool shouldClear)
        {
            if (shouldClear)
            {
                this.ReviewsSP.Children.Clear();
            }

            foreach (var review in this.movieReviews)
            {
                var reviewHolderSP = new StackPanel()
                {
                    Margin = new Thickness(10)
                };

                var nameAndScore = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };

                var authorName = new TextBlock()
                {
                    Margin = new Thickness(0, 0, 5, 5),
                    Text = string.Format("{0} - ", review.User.Username),
                    FontSize = 15
                };

                var rating = new TextBlock()
                {
                    Margin = new Thickness(0, 0, 5, 5),
                    Text = string.Format("{0}", review.Rating),
                    FontSize = 15
                };

                nameAndScore.Children.Add(authorName);
                nameAndScore.Children.Add(rating);

                reviewHolderSP.Children.Add(nameAndScore);

                if (!string.IsNullOrEmpty(review.Description))
                {
                    var comment = new TextBlock()
                    {
                        TextWrapping = TextWrapping.Wrap,
                        Text = review.Description,
                        FontSize = 15,
                        FontStyle = FontStyles.Italic
                    };

                    reviewHolderSP.Children.Add(comment);
                }

                this.ReviewsSP.Children.Add(reviewHolderSP);
            }

            if (this.ReviewsSP.Children.Count < 1)
            {
                var noReviews = new TextBlock()
                {
                    Text = "No reviews yet",
                    Margin = new Thickness(10)
                };

                this.ReviewsSP.Children.Add(noReviews);                
            }
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

        private void GiveReviewBtnClicked(object sender, RoutedEventArgs e)
        {
            var reviewWindow = new ReviewWindow(this.mainController, this.userContext, this.movie)
            {
                Owner = Application.Current.MainWindow,
                Title = this.movie.Title
            };

            reviewWindow.ShowDialog();

            this.movieReviews = this.mainController.MovieController.GetMovieReviews(this.movie.Title);
            this.squidFlixRating = this.mainController.MovieController.GetAverageRating(this.movie.Title);
            this.MovieSquidFlixRatingTBlock.Text = string.Format("SquidFlix rating: {0}", squidFlixRating);
            DisplayReviews(true);
        }

        private void AddToCartBtbClicked(object sender, RoutedEventArgs e)
        {
            this.userContext.AddToCart(this.movie);
            this.AddToCartBtn.IsEnabled = false;
        }

        private void CartBtnClicked(object sender, RoutedEventArgs e)
        {
            var cart = new CartWindow(this.mainController, this.userContext)
            {
                Owner = Application.Current.MainWindow
            };

            cart.ShowDialog();
        }

        private void UpdateLikeCount()
        {
            this.MovieLikesTBlock.Text = this.movie.LikedBy.Count().ToString();

            if (this.userContext.LoggedUser.LikedMovies.Any(m => m.MovieId == this.movieId))
            {
                this.LikeBtn.IsEnabled = false;
            }
        }

        private void LikeBtnClicked(object sender, RoutedEventArgs e)
        {
            this.LikeBtn.IsEnabled = false;
            this.mainController.UserController.LikeMovie(this.userContext.LoggedUser, this.movie);

            this.userContext.LoggedUser.LikedMovies.Add(this.movie);
            this.movie.LikedBy.Add(this.userContext.LoggedUser);

            this.movie = this.mainController.MovieController.GetMovieByTitle(this.movie.Title);

            UpdateLikeCount();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.movie = this.mainController.MovieController.GetMovieById(this.movieId);
            this.squidFlixRating = this.mainController.MovieController.GetAverageRating(this.movie.Title);
            this.movieGenresObjs = mainController.MovieController.GetMovieGenres(this.movie);
            this.movieReviews = mainController.MovieController.GetMovieReviews(this.movie.Title);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingWindow.Close();
            var stackPanel = new StackPanel();

            if (e.Error != null)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock(e.Error.Message));
                ErrorDialog.DisplayError(stackPanel, "Finding a movie failed.");
            }
            else
            {
                FillMovieInfoPage();
            }
        }
    }
}
