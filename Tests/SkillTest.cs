using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using ProjectMars_Playwright.Pages.Components;
using ProjectMarsCompetitionTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMars_Playwright.Tests
{
    
    public class SkillTest:TestBase
    {
       
        private SkillPage _skillPage;

        public SkillTest()   // <-- Constructor runs BEFORE TestBase Setup
        {
            RequiresLogin = true;
        }

        [SetUp]
        public async Task SetupTest()
        {
          
            _skillPage = new SkillPage(Page, State);

            // open Skills tab
            await Page.Locator("a[data-tab='second']").ClickAsync();
        }

        [Test]
        public async Task AddSkill_ShouldSucceed()
        {
            State.CurrentSkill = TestDataHelper.GetSkillData("validSkill");

            await _skillPage.DeleteSkillIfExistsAsync(State.CurrentSkill);

            await _skillPage.AddSkillAsync();

            string message = await _skillPage.GetActualMessageAsync(State.CurrentSkill.ExpectedMessage);

            Assert.That(message,Does.Contain(State.CurrentSkill.ExpectedMessage).IgnoreCase);

            Assert.IsTrue(await _skillPage.IsRecordPresentAsync());
        }

        [Test]
        public async Task AddDuplicateSkill_ShouldShowError()
        {
            State.CurrentSkill = TestDataHelper.GetSkillData("duplicateSkill");

            await _skillPage.DeleteSkillIfExistsAsync(State.CurrentSkill);

            await _skillPage.AddSkillAsync();    
                                                 

            await _skillPage.AddSkillAsync();   

            string message = await _skillPage.GetActualMessageAsync(State.CurrentSkill.ExpectedMessage);

            Assert.That(message,
                Does.Contain(State.CurrentSkill.ExpectedMessage).IgnoreCase);
        }

        [Test]
        public async Task EditSkill_ShouldSucceed()
        {
       
            State.CurrentSkill = TestDataHelper.GetSkillData("validSkill");

            await _skillPage.DeleteSkillIfExistsAsync(State.CurrentSkill);
            await _skillPage.AddSkillAsync();

            State.NewSkill = TestDataHelper.GetSkillData("editSkill");

            await _skillPage.EditSkillAsync(State.CurrentSkill);
                    
            State.CurrentSkill = State.NewSkill;

          //Assertion
            string message = await _skillPage.GetActualMessageAsync(State.NewSkill.ExpectedMessage);

            Assert.That(message,
                Does.Contain(State.NewSkill.ExpectedMessage).IgnoreCase,
                "Skill edit message did not match expected.");

           
            Assert.IsTrue(await _skillPage.IsRecordPresentAsync(),
                "Edited skill record was not found.");
        }

        [Test]
        public async Task DeleteSkill_ShouldSucceed()
        {
           
            State.CurrentSkill = TestDataHelper.GetSkillData("deleteSkill");

            await _skillPage.DeleteSkillIfExistsAsync(State.CurrentSkill);
                       
            await _skillPage.AddSkillAsync();
            await _skillPage.GetActualMessageAsync("added");   
                      
            await _skillPage.DeleteSkillIfExistsAsync(State.CurrentSkill);

            //Assertion
            string message = await _skillPage.GetActualMessageAsync(State.CurrentSkill.ExpectedMessage);

            Assert.That(message,
                Does.Contain(State.CurrentSkill.ExpectedMessage).IgnoreCase,
                "Delete skill message mismatch.");

            Assert.IsFalse(await _skillPage.IsRecordPresentAsync(),
                "Skill record still exists after deletion.");
        }







    }
}
