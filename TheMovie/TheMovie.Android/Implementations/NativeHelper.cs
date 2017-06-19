using TheMovie.Droid.Implementations;
using TheMovie.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(NativeHelper))]
namespace TheMovie.Droid.Implementations
{
    public class NativeHelper : INativeHelper
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}