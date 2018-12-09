using Plugin.Share.Abstractions;
using System.Threading.Tasks;

namespace Mobile.Extensions
{
    public static class BrowserExtensions
    {
        public static async Task OpenBrowserAsync(this IShare crossShare, string url)
        {
            var options = new BrowserOptions
            {
                ChromeShowTitle = true,
                ChromeToolbarColor = new ShareColor(33, 150, 243),
                UseSafariReaderMode = true,
                UseSafariWebViewController = true
            };

            if (!url.Contains("http"))
            {
                url = $"http://{url}";
            }

            await crossShare.OpenBrowser(url, options);
        }
    }
}
