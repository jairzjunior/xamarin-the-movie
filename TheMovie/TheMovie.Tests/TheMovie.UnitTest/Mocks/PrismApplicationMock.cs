using Prism.Navigation;
using Prism.Unity;
using TheMovie.Interfaces;
using TheMovie.UnitTest.Mocks.Views;
using TheMovie.Views;
using Xamarin.Forms;

namespace TheMovie.UnitTest.Mocks
{
    public class PrismApplicationMock : PrismApplication
    {
        public new INavigationService NavigationService => base.NavigationService;

        protected override void OnInitialized()
        {            
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }        

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();            
            Container.RegisterTypeForNavigation<MainPageMock>(nameof(MainPage));
            Container.RegisterTypeForNavigation<SearchMoviesPageMock>(nameof(SearchMoviesPage));
            Container.RegisterTypeForNavigation<MovieDetailPageMock>(nameof(MovieDetailPage));
            DependencyService.Register<IApiService, TmdbServiceMock>();
        }

        public INavigationService CreateNavigationServiceForPage(Page page)
        {
            return CreateNavigationService(page);
        }
    }
}
