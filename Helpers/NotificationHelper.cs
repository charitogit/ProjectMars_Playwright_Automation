using Microsoft.Playwright;

namespace ProjectMars_Playwright.Helpers
{
    public class NotificationHelper
    {
        private readonly IPage _page;

        private const string MessageBoxXPath = "//div[@class='ns-box-inner']";
        private const string CloseIconXPath = "//*[@class='ns-close']";

        public NotificationHelper(IPage page)
        {
            _page = page;
        }

        // Reusable: Get latest toast message
        public async Task<string> GetActualMessageAsync()
        {
            var messages = _page.Locator(MessageBoxXPath);
            int count = await messages.CountAsync();

            var latest = messages.Nth(count - 1);  // newest message

            await latest.WaitForAsync();
            return (await latest.InnerTextAsync()).Trim();
        }

        // Reusable: Close popup
        public async Task CloseMessageAsync()
        {
            var closeButtons = _page.Locator(CloseIconXPath);

            if (await closeButtons.CountAsync() > 0)
            {
                await closeButtons.First.ClickAsync();
            }
        }
    }
}
