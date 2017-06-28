using Prism.Mvvm;
using TheMovie.Interfaces;

using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        /// <summary>
        /// Get the api service instance.
        /// </summary>
        protected IApiService ApiService => DependencyService.Get<IApiService>();

        /// <summary>
        /// Private backing field to hold the busy.
        /// </summary>
        bool isBusy = false;
        /// <summary>
        /// Public property to set and get the state as busy.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        /// <summary>
        /// Private backing field to hold the title.
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }        
    }
}
