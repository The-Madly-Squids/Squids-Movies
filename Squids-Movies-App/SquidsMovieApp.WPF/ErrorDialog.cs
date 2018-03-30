using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SquidsMovieApp.WPF
{
    public static class ErrorDialog
    {
        public static void DisplayError(StackPanel stackPanel, string windowTitle)
        {
            var errorWindow = new ErrorWindow(stackPanel)
            {
                Owner = Application.Current.MainWindow,
                ErrorName = windowTitle
            };

            errorWindow.ShowDialog();
        }

        public static TextBlock CreateErrorTextBlock(string errorText)
        {
            var errorTextBlock = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.Red),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Text = errorText
            };

            return errorTextBlock;
        }
    }
}
