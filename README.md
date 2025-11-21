# ProjectMars Playwright Automation (Practice Project)

This repository is a practice exercise where I converted selected Selenium + NUnit tests into Playwright for .NET.
The goal is to learn Playwright fundamentals, improve my understanding of async workflows, and get familiar with modern UI automation patterns.

# What This Project Includes
✔ Playwright Test Setup

Browser, context, and page initialization

NUnit test structure using async/await

settings.json configuration (base URL, etc.)

✔ Page Object Model (POM)

Converted from Selenium into Playwright locators:

SignInPage

SkillPage (Add, Edit, Delete)

✔ JSON-Based Test Data

Valid user credentials

Valid skill

Duplicate skill

Edit skill scenarios

✔ Simple TestState Pattern

Used for:

CurrentSkill

OriginalSkill

NewSkill

Helpful for edit/update test scenarios.

# Implemented Skill Tests
Sign In 

Add Skill

Add Duplicate Skill

Edit Skill

Delete Skill

```
📁 Folder Structure
ProjectMars_Playwright/
 ├── Pages/
 │   ├── SignInPage.cs
 │   └── Components/
 │       └── SkillPage.cs
 ├── Tests/
 │   ├── SignInTest.cs
 │   └── SkillTest.cs
 ├── TestStates/
 ├── TestData/
 │   └── Skill/*.json
 ├── Config/
 ├── Helpers/
 ├── TestBase.cs
 └── settings.json
```
# Running the Tests
Install Playwright browsers:
dotnet playwright install

Run tests:
dotnet test

# About This Repo

This is not a full production framework — it is a clean learning project.
I built it to:
Practice Playwright for .NET
Compare the experience with Selenium

Prepare for automation roles where Playwright is preferred

# Author
Charito Artates
Junior Test Engineer – Christchurch, NZ
Learning Playwright | C# | UI Automation
