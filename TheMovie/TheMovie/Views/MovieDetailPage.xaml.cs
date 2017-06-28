using Xamarin.Forms;

namespace TheMovie.Views
{
    public partial class MovieDetailPage : ContentPage
    {       
        public MovieDetailPage()
        {
            InitializeComponent();
            
            GenresListView.ItemSelected += (sender, e) => {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };
        }        
    }
}
