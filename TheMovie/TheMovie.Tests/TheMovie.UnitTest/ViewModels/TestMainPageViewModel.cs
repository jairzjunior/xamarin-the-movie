using NUnit.Framework;
using System.Linq;
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
        public void LoadUpcomingMovies()
        {
            viewModel.LoadUpcomingMoviesCommand.Execute();            
            Assert.AreNotEqual(0, viewModel.Movies.Count());
        }

        [Test]
        [Category("Unit Test")]
        public void LoadUpcomingMoviesPagination()
        {
            const int minMoviesExpected = 40;
            viewModel.LoadUpcomingMoviesCommand.Execute();
            viewModel.ItemAppearingCommand.Execute(viewModel.Movies.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.Movies.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.Movies.Last());
            Assert.IsTrue(viewModel.Movies.Count() > minMoviesExpected);
        }

        [Test]
        [Category("Unit Test")]
        public void ShowSearchMovies()
        {            
            var nameView = new SearchMoviesPageMock().ToString();
            viewModel.ShowSearchMoviesCommand.Execute();
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }

        [Test]
        [Category("Unit Test")]
        public void ShowMovieDetail()
        {            
            var nameView = new MovieDetailPageMock().ToString();
            var movie = viewModel.Movies.FirstOrDefault();
            viewModel.ShowMovieDetailCommand.Execute(movie);
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }        
    }
}
