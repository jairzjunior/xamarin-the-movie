using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TheMovie.UITest
{
    [TestFixture(Platform.Android)]    
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void SearchMovieByTitles()
        {
            var searchTerms = new string[] 
            {
                "Mad Max",
                "Resident",
                "Wonder",
                "John"
            };

            app.Tap(c => c.Marked("Search"));

            foreach (var searchTerm in searchTerms)
            {                
                app.EnterText(c => c.Marked("SearchBar"), searchTerm);
                app.PressEnter();
                app.ScrollDown();
                app.ScrollDown();
                app.ScrollDown();
                app.ScrollUp();
                app.ScrollUp();
                app.ScrollUp();
                app.ClearText(c => c.Marked("SearchBar"));
            }            
        }

        [Test]
        public void SearchMovieByTitleSelectItem()
        {
            var searchTerm = "Mad Max";

            app.Tap(c => c.Marked("Search"));
            app.EnterText(c => c.Marked("SearchBar"), searchTerm);
            app.PressEnter();
            app.Tap(c => c.Marked("ImageViewCell"));
            app.ScrollDown();
            app.ScrollUp();
        }

        [Test]
        public void UpcomingMoviePagination()
        {
            for (int i = 0; i < 100; i++)
            {
                app.ScrollDown();
            }                                   
        }

        [Test]
        public void UpcomingMovieSelectItem()
        {            
            app.Tap(c => c.Marked("ImageViewCell"));
            app.ScrollDown();
            app.ScrollUp();
        }
    }
}

