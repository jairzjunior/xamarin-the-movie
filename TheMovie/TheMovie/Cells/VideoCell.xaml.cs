using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovie.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoCell : ContentView
    {
		public VideoCell()
		{
			InitializeComponent ();
		}
	}
}