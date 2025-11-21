# ProjectMars Playwright Exercise
Learning Playwright by Converting My Selenium Framework to Playwright (.NET)

This repository is my personal learning exercise where I take parts of my existing Selenium + NUnit automation framework (Project Mars) and rebuild them using Playwright for .NET.

My goals for this project:

✔ Learn Playwright

✔ Practice converting Selenium-style code

✔ Understand async/await, locators, and auto-waiting

✔ Build confidence for Playwright-focused Test Engineer roles in NZ

This is not a production framework, but a clean practice project where I explore modern UI automation patterns.

# What I Practiced in This Project
✔ Setting up Playwright for .NET

Creating a TestBase with browser, context, and page

Running NUnit tests with async/await

Installing Playwright browsers

✔ Converting Page Object Model from Selenium → Playwright

Examples converted:

Sign In

Skills module (Add, Edit, Delete)

✔ Replacing Selenium waits with Playwright auto-waits

No more WaitToBeVisible

Using Locator.WaitForAsync()

Relying on Playwright’s built-in auto-wait

✔ Using JSON test data

Valid user

Invalid password

Valid skill

Duplicate skill

Edit skill

Delete skill

✔ Trying the TestState pattern

Helps track:

CurrentSkill

OriginalSkill (before editing)

NewSkill (after editing)

(Also helpful for future cleanup improvements.)

## 📁 Folder Structure (Simple & Beginner-Friendly)

```
ProjectMars_Playwright/
 ├── Pages/
 │   ├── SignInPage.cs
 │   ├── SkillPage.cs
 ├── Tests/
 │   ├── SignInTest.cs
 │   └── SkillTest.cs
 ├── TestStates/
     ├── TestStateInfo.cs
 ├── TestData/
 │   └── Credential/*.json
 │   └── Skill/*.json
 ├── Config/
 ├── Helpers/
 ├── TestBase.cs
 └── settings.json
```

No over-engineering — just enough structure to learn Playwright properly.

# Skills Module Tests I Created

✔ Sign in

✔ Add Skill

✔ Add Duplicate Skill

✔ Edit Skill

✔ Delete Skill

✔ Invalid cases:

missing name (in progress)

missing level (in progress)

long text (in progress)

special characters (in progress)

These helped me practice Playwright locators, auto-waiting, and toast message handling.

# How to Run
1. Install Playwright browsers
dotnet playwright install

2. Run tests
dotnet test

# Why I Built This

I’m currently applying for Junior Test Automation roles in New Zealand, and many companies now use Playwright either alongside or instead of Selenium.

I was curious about:

how Playwright compares to Selenium

what advantages it offers

how easily I can adapt my Selenium experience to a newer tool

whether the same test design and POM principles still apply

I already have hands-on experience with:

Selenium + NUnit

JSON test data

Page Object Model (POM)

Test design and structure

So I built this repo as a learning exercise — taking parts of my Selenium framework and rewriting them in Playwright for .NET.
This helps me strengthen my understanding, prepare for interviews, and demonstrate that I can learn modern automation tools quickly.

# Author

Charito Artates
Junior Test Engineer • Christchurch, NZ
Learning Playwright | C# | UI Automation

Note: This is a learning exercise, not a finished automation framework.
I will continue improving it as I learn more.
