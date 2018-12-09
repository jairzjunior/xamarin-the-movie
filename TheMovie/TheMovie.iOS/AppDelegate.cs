using FFImageLoading.Forms.Platform;
using Foundation;
using PanCardView.iOS;
using UIKit;

namespace TheMovie.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			CachedImageRenderer.Init();
            CardsViewRenderer.Preserve();

            global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
