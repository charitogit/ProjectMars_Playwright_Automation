using Microsoft.Playwright;
using NUnit.Framework;
using ProjectMars_Playwright.Config;
using ProjectMars_Playwright.Helpers;
using ProjectMars_Playwright.Pages;
using ProjectMars_Playwright.TestStates;
using ProjectMarsCompetitionTask.Helpers;

public class TestBase
{
    protected EnvironmentSettings Settings;
    protected IPage Page;
    protected TestStateInfo State;
    private IBrowser _browser;
    private IPlaywright _playwright;
    private IBrowserContext _context;

    protected bool RequiresLogin = false;
      

    [SetUp]
    public async Task Setup()
    {
        // Load settings
        Settings= EnvironmentSettingsLoader.Load();

        // Initialize TestState
        State =new TestStateInfo();


        // Launch Playwright
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false   // change to true for CI
        });

        _context = await _browser.NewContextAsync();
        Page = await _context.NewPageAsync();

        // Auto-login if Requires Login
       

        if (RequiresLogin)
        {
            await Page.GotoAsync(Settings.BaseUrl);

            // Go to Sign In button
            await Page.ClickAsync("text=Sign In");   // <-- ADD THIS (adjust selector to your UI)

            var user = TestDataHelper.GetUserData("ValidUser");
            var signInPage = new SignInPage(Page);

            await signInPage.LoginAsync(user.Email, user.Password);
        }
    }

    [TearDown]
    public async Task Cleanup()
    {
        await _context.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
