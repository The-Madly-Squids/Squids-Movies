using Autofac;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SquidsMovieApp.DTO;
using SquidsMovieApp.WPF.Controllers;
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
    public partial class ProfilePage : Page
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;
        private string moneyBalance;

        public ProfilePage(IMainController mainController, UserContext userContext)
        {
            InitializeComponent();

            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;

            GreetingName.Text = string.Format("Hello, {0}!", userContext.LoggedUser.Username);
            FillUserProfile();
            this.UserProfileNav.IsEnabled = false;
            this.SearchTBox.Focus();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string MoneyBalance
        {
            get => string.Format("{0} $", this.moneyBalance);
            set => this.moneyBalance = value;
        }
        public IList<ParticipantModel> LikedParticipants { get; set; }
        public IList<MovieModel> LikedMovies { get; set; }
        public IList<MovieModel> BoughtMovies { get; set; }
        public IList<ReviewModel> Reviews { get; set; }
        public IList<UserModel> Following { get; set; }
        public IList<UserModel> Followers { get; set; }

        private void FillUserProfile()
        {
            this.Username = this.userContext.LoggedUser.Username;
            this.Email = this.userContext.LoggedUser.Email;
            this.MoneyBalance = this.userContext.LoggedUser.MoneyBalance.ToString();

            // Firstname
            if (this.userContext.LoggedUser.FirstName != null)
            {
                this.FirstName = this.userContext.LoggedUser.FirstName;
            }
            else
            {
                this.FirstName = "N/A";
            }

            // Lastname
            if (this.userContext.LoggedUser.LastName != null)
            {
                this.LastName = this.userContext.LoggedUser.LastName;
            }
            else
            {
                this.LastName = "N/A";
            }

            // Age
            if (this.userContext.LoggedUser.Age != null)
            {
                this.Age = this.userContext.LoggedUser.Age;
            }
            else
            {
                this.Age = -1;
            }
            // Bought movies
            GetBoughtMovies();

            // Liked movies
            GetLikedMovies();

            // Followers
            GetFollowers();

            // Following
            GetFollowing();

            // Liked participants
            GetLikedParticipants();
        }

        private void GetBoughtMovies()
        {
            var boughtMovies = this.mainController.UserController.GetBoughtMovies(this.userContext.LoggedUser.Username);

            if (boughtMovies.Any())
            {
                foreach (var movie in boughtMovies)
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

                    this.BoughtMoviesSP.Children.Add(tb);
                }
            }
            else
            {
                var tb = new TextBlock
                {
                    Text = "No bought movies to show."
                };

                this.BoughtMoviesSP.Children.Add(tb);
            }
        }

        private void GetLikedMovies()
        {
            var likedMovies = this.mainController.UserController.GetLikedMovies(this.userContext.LoggedUser.Username);

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
            var followers = this.mainController.UserController.GetFollowers(this.userContext.LoggedUser.Username);

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
            var following = this.mainController.UserController.GetFollowed(this.userContext.LoggedUser.Username);

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
                    Text = "You are not following anyone."
                };

                this.FollowingSP.Children.Add(tb);
            }
        }

        private void GetLikedParticipants()
        {
            var likedParticipants = this.mainController.UserController.GetLikedParticipants(this.userContext.LoggedUser.Username);

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
                    Text = "No actors or directors you like."
                };

                this.LikedParticipantsSP.Children.Add(tb);
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
                var err = ErrorDialog.CreateErrorTextBlock("First name cannot be empty or whitespace!");
                stackPanel.Children.Add(err);
                ErrorDialog.DisplayError(stackPanel, "Editting failed");
                return;
            }

            mainController.UserController.EditUserFirstName(userContext.LoggedUser, newName);
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
                var err = ErrorDialog.CreateErrorTextBlock("Last name cannot be empty or whitespace!");
                stackPanel.Children.Add(err);
                ErrorDialog.DisplayError(stackPanel, "Editting failed");
                return;
            }

            mainController.UserController.EditUserLastName(userContext.LoggedUser, newName);
            this.LastName = newName;
            this.LastNameTBlock.Text = newName;
            EditLastNameClicked(sender, e);
        }

        private void AddMoneyClicked(object sender, RoutedEventArgs e)
        {
            var transferWindow = new AddMoneyToAccountWindow(this.mainController, this.userContext);
            transferWindow.Owner = Application.Current.MainWindow;
            transferWindow.ShowDialog();
            
            this.MoneyBalance = userContext.LoggedUser.MoneyBalance.ToString();
            this.WalletTB.Text = this.MoneyBalance;
            this.UserBalanceNav.Text = this.MoneyBalance;
        }

        private void ParticipantLinkClicked(object sender, RoutedEventArgs e)
        {
            var hyperLinkName = (sender as Hyperlink).Name;
            this.NavigationService.Navigate(new ParticipantPreviewPage(this.mainController, this.userContext, hyperLinkName));
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

        private void BrowseBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BrowseMoviesPage(this.mainController, this.userContext));
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
