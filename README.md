#QA Automation Framework (Playwright + .NET)

## 📌 Project Overview
This repository serves as a showcase of an **End-to-End (E2E) Automation Framework** built from the ground up using **C#, .NET 9, and Playwright**. 

Instead of a basic implementation, this project is designed to demonstrate advanced software engineering principles applied to Quality Assurance, ensuring maximum scalability, maintainability, and resilience for large-scale applications.

## 🛠️ Tech Stack
* **Language:** C# (.NET 9)
* **Engine:** Microsoft.Playwright.NUnit
* **Test Runner:** NUnit
* **CI/CD:** GitHub Actions (Automated workflow)

## 🏛️ Architecture & Design Patterns
This framework moves beyond the traditional Page Object Model (POM) by implementing advanced structural patterns to prevent the "God Class" anti-pattern and simplify test creation.

* **Facade Pattern (Application Manager):** Tests do not instantiate individual pages. A centralized `App` registry manages lazy initialization of all modules, keeping the test classes extremely clean (`App.Login.LoginAsAsync(...)`).
* **Composition over Inheritance (Component Objects):** Shared UI elements (like Navbars or Alerts) are extracted into isolated `Components` rather than being forced into a `BasePage`. 
* **Data-Driven Testing (DDT):** NUnit's `[TestCase]` attributes are utilized to test multiple negative scenarios without code duplication.
* **Zero Magic Strings:** All test data, routes, and credentials are abstracted into centralized static data classes.
* **Semantic Web-First Assertions:** Strict usage of accessibility locators (`GetByRole`, `GetByLabel`) combined with Playwright's auto-waiting features to eliminate flaky tests and hardcoded waits.

## 🚀 How to Run Locally

1. **Clone the repository and restore dependencies:**
   ```bash
   dotnet restore
   ```
2. **Install Playwright browsers (Required first time):**
   ```bash
   dotnet build
   pwsh bin/Debug/net9.0/playwright.ps1 install
   ```
3. **Execute the test suite:**
   ```bash
   dotnet test
   ```

## 📊 CI/CD Integration & Artifacts
The framework is fully integrated with **GitHub Actions**. On every push or pull request to the `main` branch, the suite executes headlessly.

Implemented:
- Smoke gate in CI (`Category=UI` + `Category=Smoke`) before full-suite execution
- Automatic screenshot + trace capture on failed UI tests via `BaseTest` tear-down
- Video recording enabled through Playwright context options (`TestResults/Videos`)

## 🧪 Test Layers
The suite is physically and logically separated:

- `Playwright_DotNet_E2E/Tests/UI`: end-to-end browser validation of product behavior
- `Playwright_DotNet_E2E/Tests/Infrastructure`: framework-level validation (DI, factories, service wiring)

## 🏷️ Test Categories
Use NUnit categories to filter execution by intent:

- `UI`: browser-driven end-to-end tests
- `Smoke`: critical happy-path flows
- `Regression`: invalid credentials and failure-path coverage
- `Infrastructure`: configuration, factory, and helper validation
- `Unit`: pure non-browser tests (fast validation of framework logic)

Example filters:

```bash
dotnet test --filter "Category=UI"
dotnet test --filter "Category=Smoke"
dotnet test --filter "Category=Regression"
dotnet test --filter "Category=Infrastructure"
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=UI&Category=Smoke"
```

## ✅ How To Validate CI/CD Is Working

1. Push a branch or open a Pull Request.
2. Open GitHub `Actions` tab and verify workflow `E2E CI` runs.
3. Confirm `smoke-gate` and `full-suite` jobs are green.
4. Download workflow artifacts:
   - `smoke-results`
   - `full-suite-results`
5. Inspect diagnostics inside artifacts:
   - `*.trx` test reports
   - `TestResults/Artifacts` screenshots
   - `TestResults/Traces` trace zip files (open with `trace.playwright.dev`)
   - `TestResults/Videos` recorded videos