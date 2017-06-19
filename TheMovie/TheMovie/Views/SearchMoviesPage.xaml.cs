using System;

using TheMovie.Models;
using TheMovie.ViewModels;

using Xamarin.Forms;

namespace TheMovie.Views
{
    public partial class SearchMoviesPage : ContentPage
    {
        SearchMoviesViewModel viewModel;

        public SearchMoviesPage()
        {
            InitializeComponent();

            ItemsListView.ItemAppearing += List_ItemAppearing;

            BindingContext = viewModel = new SearchMoviesViewModel();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBar.Focus();
        }

        private async void List_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var viewCellDetails = e.Item as Movie;
            int viewCellIndex = viewModel.SearchResults.IndexOf(viewCellDetails);
            if (viewModel.SearchResults.Count - 2 <= viewCellIndex)
            {
                await viewModel.NextPageAsync();
            }
        }
    }
}