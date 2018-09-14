using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using TheMovie.Interfaces;
using TheMovie.UnitTest.Fakes.Services;
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPageMock>(nameof(MainPage));
            containerRegistry.RegisterForNavigation<SearchMoviesPageMock>(nameof(SearchMoviesPage));
            containerRegistry.RegisterForNavigation<MovieDetailPageMock>(nameof(MovieDetailPage));
            DependencyService.Register<IApiService, TmdbServiceFake>();
        }
    }
}
