using Microsoft.Playwright;
using ProjectMars_Playwright.Models;
using ProjectMars_Playwright.TestStates;
using System;
using System.Threading.Tasks;

namespace ProjectMars_Playwright.Pages.Components
{
    public class SkillPage
    {
        private readonly IPage _page;
        private readonly TestStateInfo _state;

        // Base container for the active Skills tab
        private const string ActiveTab = "//div[@data-tab='second' and contains(@class, 'active')]";

        public SkillPage(IPage page, TestStateInfo state)
        {
            _page = page;
            _state = state;
        }

        // -------------------------------------------------------
        // PUBLIC ACTIONS
        // -------------------------------------------------------

        public async Task AddSkillAsync()
        {
            await _page.Locator($"{ActiveTab}//div[normalize-space()='Add New']").ClickAsync();

            await FillSkillFormAsync();
        }

        public async Task EditSkillAsync(Skill originalSkill)
        {
            string button =
                $"{ActiveTab}//table/tbody/tr[" +
                $"td[1][normalize-space()='{originalSkill.SkillName}'] and " +
                $"td[2][normalize-space()='{originalSkill.Level}']]" +
                "//i[contains(@class,'write icon')]";

            await _page.Locator(button).ClickAsync();

            await FillSkillFormAsync();
        }

        public async Task DeleteAllSkillsAsync()
        {
            string rows = $"{ActiveTab}//table[@class='ui fixed table']/tbody/tr";

            while (await _page.Locator(rows).CountAsync() > 0)
            {
                await _page.Locator($"{rows}[1]//i[contains(@class,'remove icon')]").ClickAsync();

                // Wait for snackbar message to confirm deletion
                await _page.Locator("//div[@class='ns-box-inner']").WaitForAsync();

                Console.WriteLine($"Deleted a skill at {DateTime.Now}");
            }
        }

        public async Task DeleteSkillIfExistsAsync(Skill data)
        {
            string deleteButton =
                $"{ActiveTab}//table/tbody/tr[" +
                $"td[1][normalize-space()='{data.SkillName}'] and " +
                $"td[2][normalize-space()='{data.Level}']]" +
                "//i[contains(@class,'remove icon')]";

            var locator = _page.Locator(deleteButton);

            if (await locator.CountAsync() > 0)
            {
                await locator.First.ClickAsync();
                Console.WriteLine($"Deleted Skill: {data.SkillName} ({data.Level})");
            }
            else
            {
                Console.WriteLine($"No skill found to delete: {data.SkillName} ({data.Level})");
            }
        }

        public async Task<bool> IsRecordPresentAsync()
        {
            string row =
                $"{ActiveTab}//table/tbody/tr[" +
                $"td[1][normalize-space()='{_state.CurrentSkill!.SkillName}'] and " +
                $"td[2][normalize-space()='{_state.CurrentSkill.Level}']]";

            return await _page.Locator(row).CountAsync() > 0;
        }

        // -------------------------------------------------------
        // PRIVATE HELPERS
        // -------------------------------------------------------

        private async Task FillSkillFormAsync()
        {
            // Fill skill text
            await _page.Locator("//input[@placeholder='Add Skill']")
                        .FillAsync(_state.CurrentSkill!.SkillName);

            // Select dropdown
            await _page.Locator("//select[@name='level']")
                        .SelectOptionAsync(new SelectOptionValue
                        {
                            Label = _state.CurrentSkill.Level
                        });

            // Click Add OR Update
            if (await _page.Locator("//input[@value='Add']").CountAsync() > 0)
            {
                await _page.Locator("//input[@value='Add']").ClickAsync();
            }
            else
            {
                await _page.Locator("//input[@value='Update']").ClickAsync();
            }
        }

        // -------------------------------------------------------
        // MESSAGE HANDLING
        // -------------------------------------------------------
        public async Task<string> GetActualMessageAsync(string expected)
        {
            var target = _page.Locator($"//div[@class='ns-box-inner' and contains(text(), '{expected}')]");

            await target.WaitForAsync();   // wait until THIS specific toast shows
            return (await target.InnerTextAsync()).Trim();
        }

        public async Task CloseMessageAsync()
        {
            var closeIcons = _page.Locator("//*[@class='ns-close']");
            int count = await closeIcons.CountAsync();

            if (count > 0)
            {
                await closeIcons.Nth(count - 1).ClickAsync();
            }
        }
    }
}
