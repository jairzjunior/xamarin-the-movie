using NUnit.Framework;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using System.Threading.Tasks;
using TheMovie.Models;
using TheMovie.UnitTest.Mocks;
using TheMovie.UnitTest.Mocks.Views;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestFixture]
    public class TestSearchMoviesPageViewModel
    {        
        private SearchMoviesPageViewModel viewModel;
        private PrismApplicationMock app;        

        public TestSearchMoviesPageViewModel()
        {            
            Xamarin.Forms.Mocks.MockForms.Init();
            app = new PrismApplicationMock();
            var pageDialogService = new PageDialogServiceMock();
            viewModel = new SearchMoviesPageViewModel(app.NavigationService, pageDialogService);
        }

        [Test]
        [Category("Unit Test")]
        public async Task SearchMoviesByTitle()
        {
            viewModel.SearchTerm = "Title";
            await viewModel.ExecuteSearchCommand();
            Assert.AreNotEqual(0, viewModel.SearchResults.Count());
        }

        [Test]
        [Category("Unit Test")]
        public async Task SearchMoviesWithoutResultByTitle()
        {
            viewModel.SearchTerm = "_WithoutResult_";
            await viewModel.ExecuteSearchCommand();
            Assert.AreEqual(0, viewModel.SearchResults.Count());
        }

        [Test]
        [Category("Unit Test")]
        public async Task SearchMoviesByTitlePagination()
        {
            const int minMoviesExpected = 40;

            await viewModel.ExecuteSearchCommand();
            await viewModel.ExecuteItemAppearingCommand(viewModel.SearchResults.Last());
            await viewModel.ExecuteItemAppearingCommand(viewModel.SearchResults.Last());
            await viewModel.ExecuteItemAppearingCommand(viewModel.SearchResults.Last());
            Assert.IsTrue(viewModel.SearchResults.Count() > minMoviesExpected);
        }        

        [Test]
        [Category("Unit Test")]
        public async Task ShowMovieDetail()
        {
            var nameView = new MovieDetailPageMock().ToString();
            var movie = viewModel.SearchResults.FirstOrDefault();
            await viewModel.ExecuteShowMovieDetailCommand(movie);
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }
    }
}
