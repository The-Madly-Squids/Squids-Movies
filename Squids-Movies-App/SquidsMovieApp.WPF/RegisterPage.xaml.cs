using Autofac;
using Bytes2you.Validation;
using SquidsMovieApp.Common.Constants;
using SquidsMovieApp.Common.Exceptions;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
    public partial class RegisterPage : Page
    {
        private BackgroundWorker worker;
        private LoadingWindow loadingWindow;
        private string email;
        private string username;
        private string password;
        private readonly IMainController mainController;
        private readonly UserContext userContext;

        public RegisterPage(IMainController mainController, UserContext userContext)
        {
            InitializeComponent();
            EmailRegisterTB.Focus();
            this.mainController = mainController;
            this.userContext = userContext;

            this.worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            this.BirthDays = new List<int>();
            this.BirthMonths = new List<string>();
            this.BirthYears = new List<int>();
            FillDateOfBirth();

            DataContext = this;
        }

        public IList<int> BirthDays { get; private set; }
        public IList<string> BirthMonths { get; private set; }
        public IList<int> BirthYears { get; private set; }

        private void FillDateOfBirth()
        {
            for (int i = 1; i <= 31; i++)
            {
                this.BirthDays.Add(i);
            }

            for (int i = 0; i < 12; i++)
            {
                var currMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(i+1);
                BirthMonths.Add(currMonth);
            }

            for (int i = 1930, j = 0; i <= DateTime.Now.Year; i++, j++)
            {
                this.BirthYears.Add(i);
            }
        }

        private void GoBackBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RegisterBtnClicked(object sender, RoutedEventArgs e)
        {
            this.email = this.EmailRegisterTB.Text;
            this.username = this.UsernameRegisterTB.Text;
            this.password = this.PasswordRegisterPB.Password.ToString();
            var repeatedPassword = this.PasswordRepeatRegisterPB.Password.ToString();
            var stackPanel = new StackPanel();

            if (ValidateFields(stackPanel, email, username, password, repeatedPassword))
            {
               

                this.loadingWindow = new LoadingWindow()
                {
                    Owner = Application.Current.MainWindow,
                };

                worker.RunWorkerAsync();
                loadingWindow.ShowDialog();
            }
            else
            {
                ErrorDialog.DisplayError(stackPanel, "Registration failed.");
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mainController.UserController.RegisterUser(username, email, password);
                userContext.Login(email, password);
            }
            catch (UserException uex)
            {

                throw uex;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingWindow.Close();
            var stackPanel = new StackPanel();

            if (e.Error != null)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock(e.Error.Message));
                ErrorDialog.DisplayError(stackPanel, "Registration failed.");
            }
            else
            {
                this.NavigationService.Navigate(new ProfilePage(this.mainController, this.userContext));
            }
        }

        private bool ValidateFields(StackPanel stackPanel, string email, string username,
            string password, string repeatedPassword)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(email))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Email cannot be empty."));
                isValid = false;
            }
            else
            {
                try
                {
                    MailAddress ma = new MailAddress(email);
                }
                catch (SystemException)
                {
                    stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Invalid e-mail."));
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(username))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Username cannot be empty."));
                isValid = false;
            }
            else if (username.Length < GlobalConstants.MinUserUsernameLength || GlobalConstants.MaxUserUsernameLength < username.Length)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock($"Username must be between {GlobalConstants.MinUserUsernameLength} and {GlobalConstants.MaxUserUsernameLength} symbols long."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Password cannot be empty."));
                isValid = false;
            }
            else if (password.Length < GlobalConstants.MinUserPasswordLength)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock($"Password must be at least {GlobalConstants.MinUserPasswordLength} symbols."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(repeatedPassword))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Repeated password cannot be empty."));
                isValid = false;
            }

            if (password != repeatedPassword)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Passwords do not match."));
                isValid = false;
            }

            return isValid;
        }

        //private TextBlock CreateErrorTextBlock(string errorText)
        //{
        //    var errorTextBlock = new TextBlock
        //    {
        //        Foreground = new SolidColorBrush(Colors.Red),
        //        HorizontalAlignment = HorizontalAlignment.Center,
        //        FontWeight = FontWeights.Bold,
        //        FontSize = 14,
        //        Text = errorText
        //    };

        //    return errorTextBlock;
        //}

        //private void DisplayError(StackPanel stackPanel)
        //{
        //    var errorWindow = new ErrorWindow(stackPanel)
        //    {
        //        Owner = Application.Current.MainWindow,
        //        ErrorName = "Registration failed."
        //    };

        //    errorWindow.ShowDialog();
        //}
    }
}
