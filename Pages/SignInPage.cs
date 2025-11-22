using Microsoft.Playwright;


namespace ProjectMars_Playwright.Pages
{
    public class SignInPage
    {
        private readonly IPage _page;

        public SignInPage(IPage page)
        {
            _page = page;
        }

        // Locators (Playwright uses CSS or text selectors)
        private ILocator EmailInput => _page.Locator("input[name='email']");
        private ILocator PasswordInput => _page.Locator("input[name='password']");
        private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        public async Task LoginAsync(string email, string password)
        {
            await EmailInput.FillAsync(email);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task AssertSuccessfulLoginAsync(string message)
        {
            var greeting = _page.Locator("//div[@id='account-profile-section']//span[contains(text(),'Hi')]");

            await greeting.WaitForAsync();

            string text = await greeting.InnerTextAsync();

            Assert.That(text, Is.EqualTo(message), "Failed to login.");
        }


        public async Task AssertFailedLoginAsync(string message)
        {
            var error = _page.Locator($"//*[contains(text(), '{message}')]");

            // Playwright smart wait until error appears
            await error.WaitForAsync();

            string text = await error.InnerTextAsync();

            Assert.That(text, Is.EqualTo(message), "Test failed.");
        }


        public async Task<bool> IsSignInFormVisibleAsync()
        {
            return await EmailInput.IsVisibleAsync() && await PasswordInput.IsVisibleAsync();
        }
    }
}
