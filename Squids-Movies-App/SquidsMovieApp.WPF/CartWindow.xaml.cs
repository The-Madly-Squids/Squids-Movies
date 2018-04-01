using SquidsMovieApp.DTO;
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
    public partial class CartWindow : Window
    {
        private readonly IMainController mainController;
        private readonly UserContext userContext;

        public CartWindow(IMainController mainController, UserContext userContext)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;

            FillCart(false);
        }

        private void FillCart(bool shouldClearItems)
        {
            double totalPrice = 0;

            if (shouldClearItems)
            {
                this.CartItemsMainSP.Children.Clear();
            }

            if (this.userContext.Cart.Any())
            {
                foreach (var item in this.userContext.Cart)
                {
                    totalPrice += item.Price;
                    CreateCartElement(item);
                }

                this.TotalPriceTBlock.Text = string.Format("${0}", totalPrice);
            }
            else
            {
                this.TotalPriceTBlock.Text = "$0";
                this.BuyBtn.IsEnabled = false;

                var noItems = new TextBlock()
                {
                    Text = "No movies added",
                    FontSize = 17,
                    FontStyle = FontStyles.Italic
                };

                this.CartItemsMainSP.Children.Add(noItems);
            }
        }

        private void CreateCartElement(MovieModel movie)
        {
            var borderMainContainer = new Border()
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(10),
                Margin = new Thickness(0,0,0,10)
            };

            var stackPanelInfo = new StackPanel();

            var title = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10),
                Text = movie.Title
            };

            var price = new TextBlock()
            {
                Margin = new Thickness(0, 0, 0, 10),
                Text = string.Format("${0}", movie.Price)
            };

            var removeBtn = new Button()
            {
                Margin = new Thickness(0, 0, 0, 10),
                Content = "Remove",
                Name = string.Format("Id_{0}", movie.MovieId)
            };

            removeBtn.Click += new RoutedEventHandler(RemoveMovieItemClicked);

            stackPanelInfo.Children.Add(title);
            stackPanelInfo.Children.Add(price);
            stackPanelInfo.Children.Add(removeBtn);

            borderMainContainer.Child = stackPanelInfo;

            this.CartItemsMainSP.Children.Add(borderMainContainer);
        }

        private void BuyBtnClicked(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveMovieItemClicked(object sender, RoutedEventArgs e)
        {
            var movieName = (sender as Button).Name;

            var movieId = int.Parse(movieName.Split('_')[1]);

            this.userContext.RemoveFromCart(movieId);

            FillCart(true);
        }

        private void CancelBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
