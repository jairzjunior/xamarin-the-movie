using Plugin.Connectivity;
using System;
using TheMovie.Interfaces;
using TheMovie.Models;
using TheMovie.ViewModels;

using Xamarin.Forms;

namespace TheMovie.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            ItemsListView.ItemAppearing += List_ItemAppearing;

            BindingContext = viewModel = new MainPageViewModel();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Movie;            
            if (item == null)
                return;

            viewModel.ShowMovieCommand.Execute(item);

            // Manually deselect item
            ItemsListView.SelectedItem = null;            
        }

        private async void List_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var viewCellDetails = e.Item as Movie;
            int viewCellIndex = viewModel.Movies.IndexOf(viewCellDetails);
            if (viewModel.Movies.Count - 2 <= viewCellIndex)
            {
                await viewModel.NextPageUpcomingMoviesAsync();
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Connectivity", "You need an internet connection. Check your connection and try again.", "OK");                
                DependencyService.Get<INativeHelper>().CloseApp();
            };
        }
    }
}
