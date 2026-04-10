# Login Automation Assessment (Playwright + .NET)

## Project summary

This project automates login validation scenarios against:

- `https://the-internet.herokuapp.com`

It covers:

- Successful login flow.
- Invalid username flow.
- Invalid password flow.

## Tech stack

- .NET 9 (`net9.0`)
- Microsoft.Playwright.NUnit
- NUnit


## Approach and Design

The suite follows a Page Object Model with test foundations split by responsibility:

- `OrigamiPlaywright/Core/BaseTest.cs`: test base, shared navigation, centralized base URL.
- `OrigamiPlaywright/Pages/BasePage.cs`: shared page behaviors and common elements.
- `OrigamiPlaywright/Pages/LoginPage.cs`: login actions and login page locators.
- `OrigamiPlaywright/Pages/SecurePage.cs`: secure area locators and post-login assertions.
- `OrigamiPlaywright/Tests/LoginTests.cs`: login test suite (happy path + negative paths).
- `OrigamiPlaywright/Data/Constants.cs`: centralized routes and test data constants.

The goal was to keep the solution simple, reliable, and easy to extend, without over-engineering.

Some design decisions to reduce flakiness and improve maintainability:

- Page objects are re-instantiated per test via setup to keep state isolated.
- Assertions validate navigation + key UI affordances + message text to reduce false positives.
- Negative scenarios are parameterized with `TestCase` to keep intent explicit while avoiding duplication.
- Locators favor accessibility-first patterns (`GetByRole`, `GetByLabel`) where possible.
- Playwright auto-waiting is leveraged to reduce timing-related flakiness.

## Repository structure

```text
OrigamiPlaywright.sln
OrigamiPlaywright/
  Core/
    BaseTest.cs
  Data/
    Constants.cs
  Pages/
    BasePage.cs
    LoginPage.cs
    SecurePage.cs
  Tests/
    LoginTests.cs
  OrigamiPlaywright.csproj
```

## Prerequisites

- Windows (current development environment)
- .NET SDK 9.0+
- Internet access to run tests against the target site

## Setup

From the solution root:

```powershell
dotnet restore
```

Install Playwright browsers (required at least once per environment):

```powershell
dotnet build
pwsh ./OrigamiPlaywright/bin/Debug/net9.0/playwright.ps1 install
```

## Run tests

From the solution root:

```powershell
dotnet test
```

Optional: run with detailed output:

```powershell
dotnet test -v normal
```


## Next improvements

- Externalize environment configuration (base URL, credentials, secrets).
- Add CI workflow to run tests on pull requests.
- Add failure artifacts (trace, screenshot, video) for faster triage.
- Expand coverage with logout, session behavior, and authorization boundary scenarios.
- Add tagging strategy and filtered test execution for larger suites.

## Notes

This implementation focuses on:

- Keeping the framework simple and maintainable
- Writing reliable tests around real user behavior
- Structuring the project so it can scale without major refactoring
