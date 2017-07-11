using NUnit.Framework;
using System.Linq;
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
        public void SearchMoviesByTitle()
        {
            viewModel.SearchTerm = "Title";
            viewModel.SearchCommand.Execute();
            Assert.AreNotEqual(0, viewModel.SearchResults.Count());
        }

        [Test]
        [Category("Unit Test")]
        public void SearchMoviesWithoutResultByTitle()
        {
            viewModel.SearchTerm = "_WithoutResult_";
            viewModel.SearchCommand.Execute();
            Assert.AreEqual(0, viewModel.SearchResults.Count());
        }

        [Test]
        [Category("Unit Test")]
        public void SearchMoviesByTitlePagination()
        {
            const int minMoviesExpected = 40;

            viewModel.SearchCommand.Execute();
            viewModel.ItemAppearingCommand.Execute(viewModel.SearchResults.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.SearchResults.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.SearchResults.Last());
            Assert.IsTrue(viewModel.SearchResults.Count() > minMoviesExpected);
        }        

        [Test]
        [Category("Unit Test")]
        public void ShowMovieDetail()
        {
            var nameView = new MovieDetailPageMock().ToString();
            var movie = viewModel.SearchResults.FirstOrDefault();
            viewModel.ShowMovieDetailCommand.Execute(movie);
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }
    }
}
