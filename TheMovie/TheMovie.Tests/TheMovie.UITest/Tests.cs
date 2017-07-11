using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
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
        [Category("UI Test")]        
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        [Category("UI Test")]
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
                
                AppResult[] results = app.Query("search_src_text");
                Assert.AreEqual(searchTerm, results[0].Text);

                app.ClearText(c => c.Marked("SearchBar"));
            }            
        }

        [Test]
        [Category("UI Test")]
        public void SearchMovieByTitleSelectItem()
        {
            const string searchTerm = "Mad Max";

            app.Tap(c => c.Marked("Search"));
            app.EnterText(c => c.Marked("SearchBar"), searchTerm);
            app.PressEnter();
            app.Tap(c => c.Marked("ImageViewCell"));
            app.ScrollDown();
            app.ScrollUp();
            app.Back();

            AppResult[] results = app.Query("search_src_text");
            Assert.AreEqual(searchTerm, results[0].Text);            
        }

        [Test]
        [Category("UI Test")]
        public void SearchMovieWithoutResultByTitle()
        {
            const string searchTerm = "_WithoutResult_";
            const string resultMessage = "No results found.";

            app.Tap(c => c.Marked("Search"));
            app.EnterText(c => c.Marked("SearchBar"), searchTerm);
            app.PressEnter();
            
            Thread.Sleep(millisecondsTimeout: 5000);

            AppResult[] results = app.Query("message");
            Assert.AreEqual(resultMessage, results[0].Text);            
        }

        [Test]
        [Category("UI Test")]
        public void SearchMoviePagination()
        {
            const int minMoviesExpected = 40;
            const int totalScroll = 50;

            const string searchTerm = "Spider";

            app.Tap(c => c.Marked("Search"));
            app.EnterText(c => c.Marked("SearchBar"), searchTerm);
            app.PressEnter();

            var titles = new List<string>();

            for (int i = 0; i < totalScroll; i++)
            {
                AppResult[] result = app.Query("LabelTitle");
                foreach (var item in result)
                {
                    if (titles.Find(a => a.Equals(item.Text)) == null)
                    {
                        titles.Add(item.Text);
                    }
                }
                
                app.ScrollDown();
            }                                  

            Assert.GreaterOrEqual(titles.Count, minMoviesExpected);            
        }

        [Test]
        [Category("UI Test")]
        public void UpcomingMoviePagination()
        {
            const int minMoviesExpected = 40;
            const int totalScroll = 50;

            var titles = new List<string>();

            for (int i = 0; i < totalScroll; i++)
            {
                AppResult[] result = app.Query("LabelTitle");
                foreach (var item in result)
                {
                    if (titles.Find(a => a.Equals(item.Text)) == null)
                    {
                        titles.Add(item.Text);
                    }
                }

                app.ScrollDown();
            }

            Assert.GreaterOrEqual(titles.Count, minMoviesExpected);            
        }

        [Test]
        [Category("UI Test")]
        public void UpcomingMovieSelectItem()
        {            
            app.Tap(c => c.Marked("ImageViewCell"));
            app.ScrollDown();
            app.ScrollUp();
            
            AppResult[] title = app.Query("LabelTitle");
            Assert.AreNotEqual("", title);            
        }
    }
}
