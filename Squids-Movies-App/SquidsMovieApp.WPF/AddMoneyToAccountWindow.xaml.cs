using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.WPF.Controllers;
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
using System.Windows.Shapes;

namespace SquidsMovieApp.WPF
{
    /// <summary>
    /// Interaction logic for AddMoneyToAccountWindow.xaml
    /// </summary>
    public partial class AddMoneyToAccountWindow : Window
    {
        private readonly IMainController mainController;
        private readonly AuthProvider authProvider;

        public AddMoneyToAccountWindow(IMainController mainController, AuthProvider authProvider)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.authProvider = authProvider;

            this.MoneyTB.Focus();
        }

        private void TransferBtnClicked(object sender, RoutedEventArgs e)
        {
            decimal moneyToAdd = 0m;

            try
            {
                moneyToAdd = decimal.Parse(this.MoneyTB.Text.Replace(',', '.'));
            }
            catch (FormatException)
            {
                var stack = new StackPanel();
                stack.Children.Add(ErrorDialog.CreateErrorTextBlock("Invalid money format!"));
                ErrorDialog.DisplayError(stack, "Transfer failed.");
                return;
            }

            //this.authProvider.FakeUser.MoneyBalance += moneyToAdd;
            this.authProvider.LoggedUser.MoneyBalance += moneyToAdd;

            mainController.UserController.AddMoneyToBalance(authProvider.LoggedUser.Username, moneyToAdd);
            this.Close();
        }

        private void CancelBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
