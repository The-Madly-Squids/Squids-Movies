﻿using SquidsMovieApp.DTO;
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
    public partial class ReviewWindow : Window
    {
        private IMainController mainController;
        private UserContext userContext;
        private readonly MovieModel movieBeingRated;

        public ReviewWindow(IMainController mainController, UserContext userContext, MovieModel movieBeingRated)
        {
            InitializeComponent();
            DataContext = this;
            this.mainController = mainController;
            this.userContext = userContext;
            this.movieBeingRated = movieBeingRated;
            this.ReviewTBox.Focus();
        }

        private void FinishBtnClicked(object sender, RoutedEventArgs e)
        {
            var newReview = new ReviewModel()
            {
                Description = this.ReviewTBox.Text,
                Rating = int.Parse(((ComboBoxItem)this.RatingsCB.SelectedItem).Content.ToString())
            };

            this.mainController.MovieController.PostMovieReview(newReview, this.movieBeingRated.MovieId, this.userContext.LoggedUser.UserId);
            this.Close();
        }

        private void CancelBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
