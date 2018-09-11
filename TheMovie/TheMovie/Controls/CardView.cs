using Xamarin.Forms;

namespace TheMovie.Controls
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 0;
            if (Device.RuntimePlatform == Device.iOS)
            {
                HasShadow = false;                
                BorderColor = Color.Transparent;
                BackgroundColor = Color.White;
            }
        }
    }
}
