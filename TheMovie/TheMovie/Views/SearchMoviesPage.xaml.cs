using Xamarin.Forms;

namespace TheMovie.Views
{
    public partial class SearchMoviesPage : ContentPage
    {        
        public SearchMoviesPage()
        {
            InitializeComponent();            

            ItemsListView.ItemSelected += (sender, e) => {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                SearchBar.HeightRequest = 40;
            }
        }        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(SearchBar.Text))
            {
                SearchBar.Focus();
            }            
        }        
    }
}