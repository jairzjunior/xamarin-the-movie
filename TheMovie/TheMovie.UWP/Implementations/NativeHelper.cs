using TheMovie.Interfaces;
using TheMovie.UWP.Implementations;
using Windows.ApplicationModel.Core;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeHelper))]
namespace TheMovie.UWP.Implementations
{
    public class NativeHelper : INativeHelper
    {
        public void CloseApp()
        {
            CoreApplication.Exit();
        }
    }
}