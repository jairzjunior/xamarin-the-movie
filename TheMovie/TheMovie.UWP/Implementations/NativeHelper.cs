using TheMovie.UWP.Implementations;
using TheMovie.Interfaces;

using Xamarin.Forms;
using System.Diagnostics;
using Windows.ApplicationModel.Core;

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