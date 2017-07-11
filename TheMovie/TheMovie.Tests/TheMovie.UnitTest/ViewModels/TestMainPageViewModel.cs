using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TheMovie.UnitTest.Mocks;
using TheMovie.UnitTest.Mocks.Views;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestFixture]
    public class TestMainPageViewModel
    {        
        private MainPageViewModel viewModel;
        private PrismApplicationMock app;        

        public TestMainPageViewModel()
        {            
            Xamarin.Forms.Mocks.MockForms.Init();
            app = new PrismApplicationMock();
            viewModel = new MainPageViewModel(app.NavigationService);
        }        

        [Test]
        [Category("Unit Test")]
        public async Task LoadUpcomingMovies()
        {
            await viewModel.ExecuteLoadUpcomingMoviesCommand();            
            Assert.AreNotEqual(0, viewModel.Movies.Count());
        }

        [Test]
        [Category("Unit Test")]
        public async Task LoadUpcomingMoviesPagination()
        {
            const int minMoviesExpected = 40;
            await viewModel.ExecuteLoadUpcomingMoviesCommand();
            await viewModel.ExecuteItemAppearingCommand(viewModel.Movies.Last());
            await viewModel.ExecuteItemAppearingCommand(viewModel.Movies.Last());
            await viewModel.ExecuteItemAppearingCommand(viewModel.Movies.Last());
            Assert.IsTrue(viewModel.Movies.Count() > minMoviesExpected);
        }

        [Test]
        [Category("Unit Test")]
        public async Task ShowSearchMovies()
        {            
            var nameView = new SearchMoviesPageMock().ToString();
            await viewModel.ExecuteShowSearchMoviesCommand();
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }

        [Test]
        [Category("Unit Test")]
        public async Task ShowMovieDetail()
        {            
            var nameView = new MovieDetailPageMock().ToString();
            var movie = viewModel.Movies.FirstOrDefault();
            await viewModel.ExecuteShowMovieDetailCommand(movie);
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }        
    }
}
