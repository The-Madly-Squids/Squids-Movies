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
    public partial class AddMoneyToAccountWindow : Window
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;

        public AddMoneyToAccountWindow(IMainController mainController, UserContext userContext)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;

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


            mainController.UserController.AddMoneyToBalance(userContext.LoggedUser.Username, moneyToAdd);
            this.userContext.LoggedUser.MoneyBalance += moneyToAdd;
            this.Close();
        }

        private void CancelBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
