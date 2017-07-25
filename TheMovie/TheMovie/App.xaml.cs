using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Prism.Unity;
using TheMovie.Views;
using Xamarin.Forms;

namespace TheMovie
{
    public partial class App : PrismApplication
    {        
        public App() : base(null) 
        {
        }
        
        public App(IPlatformInitializer initializer) : base(initializer) 
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SearchMoviesPage>();
            Container.RegisterTypeForNavigation<MovieDetailPage>();            
        }

        protected override void OnStart()
        {
            base.OnStart();            
            MobileCenter.Start(
                "android=0c100be3-d3e4-4653-b602-8efe90e6ef2f;" +
                "uwp=c10a525c-3170-4504-a567-aec798cc7be9;" +
                "ios=22a3003b-75e6-4058-a798-ad45a8e0d6a6;",
                typeof(Analytics), 
                typeof(Crashes)                
            );
        }
    }
}
