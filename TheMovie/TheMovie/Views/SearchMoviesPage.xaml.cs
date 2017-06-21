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
        }        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBar.Focus();
        }        
    }
}