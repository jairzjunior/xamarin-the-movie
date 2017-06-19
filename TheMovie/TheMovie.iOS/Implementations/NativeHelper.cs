using TheMovie.iOS.Implementations;
using TheMovie.Interfaces;

using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(NativeHelper))]
namespace TheMovie.iOS.Implementations
{
    public class NativeHelper : INativeHelper
    {
        public void CloseApp()
        {
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
        }
    }
}