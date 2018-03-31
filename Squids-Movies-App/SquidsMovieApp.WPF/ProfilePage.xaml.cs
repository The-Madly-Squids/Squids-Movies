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
            //this.GreetingName.Text = string.Format("Hello, {0}!", userContext.FakeUser.Username);
            FillUserProfile();
            //FillFakeUserProfile();
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

        private void FillFakeUserProfile()
        {
            this.Username = this.userContext.FakeUser.Username;
            this.Email = this.userContext.FakeUser.Email;
            this.MoneyBalance = this.userContext.FakeUser.MoneyBalance.ToString();

            // Firstname
            if (this.userContext.FakeUser.FirstName != null)
            {
                this.FirstName = this.userContext.FakeUser.FirstName;
            }
            else
            {
                this.FirstName = "N/A";
            }

            // Lastname
            if (this.userContext.FakeUser.LastName != null)
            {
                this.LastName = this.userContext.FakeUser.LastName;
            }
            else
            {
                this.LastName = "N/A";
            }

            // Age
            if (this.userContext.FakeUser.Age != null)
            {
                this.Age = this.userContext.FakeUser.Age;
            }
            else
            {
                this.Age = -1;
            }

            // Bought movies
            if (this.userContext.FakeUser.BoughtMovies.Any())
            {
                foreach (var movie in this.userContext.FakeUser.BoughtMovies)
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

            // Liked movies
            if (this.userContext.FakeUser.LikedMovies.Any())
            {
                foreach (var movie in this.userContext.FakeUser.LikedMovies)
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

            // Followers
            if (this.userContext.FakeUser.Followers.Any())
            {
                foreach (var follower in this.userContext.FakeUser.Followers)
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

            // Following
            if (this.userContext.FakeUser.Following.Any())
            {
                foreach (var follower in this.userContext.FakeUser.Following)
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

            // Liked participants
            if (this.userContext.FakeUser.LikedParticipants.Any())
            {
                foreach (var participant in this.userContext.FakeUser.LikedParticipants)
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
            if (this.userContext.LoggedUser.BoughtMovies.Any())
            {
                foreach (var movie in this.userContext.LoggedUser.BoughtMovies)
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

            // Liked movies
            if (this.userContext.LoggedUser.LikedMovies.Any())
            {
                foreach (var movie in this.userContext.LoggedUser.LikedMovies)
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

            // Followers
            if (this.userContext.LoggedUser.Followers.Any())
            {
                foreach (var follower in this.userContext.LoggedUser.Followers)
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

            // Following
            if (this.userContext.LoggedUser.Following.Any())
            {
                foreach (var follower in this.userContext.LoggedUser.Following)
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

            // Liked participants
            if (this.userContext.LoggedUser.LikedParticipants.Any())
            {
                foreach (var participant in this.userContext.LoggedUser.LikedParticipants)
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

            //this.MoneyBalance = userContext.FakeUser.MoneyBalance.ToString();
            this.MoneyBalance = userContext.LoggedUser.MoneyBalance.ToString();
            this.WalletTB.Text = this.MoneyBalance;
            this.UserBalanceNav.Text = this.MoneyBalance;
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
