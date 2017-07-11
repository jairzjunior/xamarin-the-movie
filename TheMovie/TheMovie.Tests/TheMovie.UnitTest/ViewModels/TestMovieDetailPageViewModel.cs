using NUnit.Framework;
using Prism.Navigation;
using TheMovie.Models;
using TheMovie.UnitTest.Mocks;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestFixture]
    public class TestMovieDetailPageViewModel
    {        
        private MovieDetailPageViewModel viewModel;
        private PrismApplicationMock app;        

        public TestMovieDetailPageViewModel()
        {            
            Xamarin.Forms.Mocks.MockForms.Init();
            app = new PrismApplicationMock();
            viewModel = new MovieDetailPageViewModel();
        }        

        [Test]
        [Category("Unit Test")]
        public void NavigatingToMovieDetail()
        {
            var movie = new Movie()
            {
                Id = 1
            };

            var parameters = new NavigationParameters();
            parameters.Add(nameof(movie), movie);

            viewModel.OnNavigatingTo(parameters);            
            Assert.AreNotEqual(null, viewModel.MovieDetail);
        }        
    }
}
