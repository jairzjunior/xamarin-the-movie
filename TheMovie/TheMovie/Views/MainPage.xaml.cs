using Plugin.Connectivity;
using TheMovie.Interfaces;

using Xamarin.Forms;

namespace TheMovie.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
            
            ItemsListView.ItemSelected += (sender, e) => {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };
        }        
    }
}
