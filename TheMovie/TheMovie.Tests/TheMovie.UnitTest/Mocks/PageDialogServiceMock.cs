using System;
using System.Threading.Tasks;
using Prism.Services;

namespace TheMovie.UnitTest.Mocks
{
    public class PageDialogServiceMock : IPageDialogService
    {
        public Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            return Task.FromResult("");
        }

        public Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons)
        {
            return Task.FromResult("");
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            return Task.FromResult(true);
        }

        public Task DisplayAlertAsync(string title, string message, string cancelButton)
        {
            return Task.FromResult("");
        }
    }
}
