using Prism.Unity;
using TheMovie.Views;
using Xamarin.Forms;

namespace TheMovie
{
    public partial class App : PrismApplication
    {        
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

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
    }
}
