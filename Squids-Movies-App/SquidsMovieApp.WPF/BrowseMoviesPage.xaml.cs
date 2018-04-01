using iTextSharp.text;
using iTextSharp.text.pdf;
using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers.Contracts;
using SquidsMovieApp.WPF.PdfReportUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void FillMovieGrid(IEnumerable<MovieModel> movies)
        {
            this.MovieDisplayGrid.Children.Clear();

            int gridCol = 0;
            int gridRow = 0;

            var stack = new StackPanel();

            foreach (var movie in movies)
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
                FillMovieGrid(this.allMovies);
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

        private void FilterBtnClicked(object sender, RoutedEventArgs e)
        {
            string genreName = this.GenresFilterCB.Text;

            if (genreName == "All")
            {
                this.FillMovieGrid(this.allMovies);
            }
            else
            {
                var genre = this.mainController.GenreController.GetGenreDto(genreName);
                var filteredMovies = this.mainController.MovieController.GetMoviesByGenre(genre);
                this.FillMovieGrid(filteredMovies);
            }
        }

        private void MoviePdfBtnClicked(object sender, RoutedEventArgs e)
        {
            Document doc = new Document(PageSize.A4);

            try
            {

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(
                  Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Report.pdf", FileMode.Create));
                doc.Open();
                PdfPTable tbl = new PdfPTable(8);
                DataTable dt = new GlobalData().GetData("SELECT * FROM Movies");
                foreach (DataColumn c in dt.Columns)
                {
                    tbl.AddCell(new Phrase(c.Caption));
                }
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //var fnt = new iTextSharp.text.Font(bf, 13.0f, 1, BaseColor.BLUE);
                foreach (DataRow row in dt.Rows)
                {

                    tbl.AddCell(new Phrase(row[0].ToString()));
                    tbl.AddCell(new Phrase(row[1].ToString()));
                    tbl.AddCell(new Phrase(row[2].ToString()));
                    tbl.AddCell(new Phrase(row[3].ToString()));
                    tbl.AddCell(new Phrase(row[4].ToString()));
                    tbl.AddCell(new Phrase(row[5].ToString()));
                    tbl.AddCell(new Phrase(row[6].ToString()));
                    tbl.AddCell(new Phrase(row[7].ToString()));
                }
                doc.Add(tbl);
                doc.Close();
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Report.pdf");
            }
            catch (Exception ae)
            {
                MessageBox.Show(ae.Message);
            }
        }
    }
}
