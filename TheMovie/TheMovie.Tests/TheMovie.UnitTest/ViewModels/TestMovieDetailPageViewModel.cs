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
        private readonly MovieDetailPageViewModel viewModel;
        private readonly PrismApplicationMock app;        

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

            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };

            viewModel.OnNavigatingTo(parameters);            
            Assert.AreNotEqual(null, viewModel.Movie);
        }        
    }
}
