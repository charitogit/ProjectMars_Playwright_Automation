using NUnit.Framework;
using ProjectMars_Playwright.Pages;
using ProjectMarsCompetitionTask.Helpers;

namespace ProjectMars_Playwright.Tests
{
    public class SignInTests : TestBase
    {
        private SignInPage _signInPage;

        [SetUp]
        public async Task SetupTest()
        {
            await base.Setup();   // initializes Playwright, browser, and Page
            _signInPage = new SignInPage(Page);
        }

        [Test]
        public async Task ValidUser_CanLogin()
        {
        
            await Page.GotoAsync(Settings.BaseUrl); 

            await Page.ClickAsync("text=Sign In"); 

            var user = TestDataHelper.GetUserData("ValidUser");
                      
            await _signInPage.LoginAsync(user.Email,user.Password );
           
            await _signInPage.AssertSuccessfulLoginAsync(user.Greeting );
        }

        [Test]
        public async Task InvalidPassword_ShowError()
        {
            await Page.GotoAsync(Settings.BaseUrl); 
            await Page.ClickAsync("text=Sign In"); 
            var user = TestDataHelper.GetUserData("InvalidPassword");
            await _signInPage.LoginAsync( user.Email, user.Password );
            await _signInPage.AssertFailedLoginAsync(user.ErrorMessage);
        }
    }
}
