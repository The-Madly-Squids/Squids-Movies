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
    public partial class ErrorWindow : Window
    {
        private StackPanel stackPanel;
        private string errorName = "Error";

        public ErrorWindow(StackPanel stackPanel)
        {
            InitializeComponent();
            this.stackPanel = stackPanel;
            this.ErrorContainer.Children.Add(stackPanel);
            DataContext = this;
        }

        public string ErrorName
        {
            get
            {
                return errorName;
            }
            set
            {
                errorName = value;
            }
        }
    }
}
