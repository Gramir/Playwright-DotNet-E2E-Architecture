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

*(Configured to automatically capture Traces, Screenshots, and Videos on test failure to drastically reduce bug triage time).*

## 🏷️ Test Categories
Use NUnit categories to filter execution by intent:

- `Smoke`: critical happy-path flows
- `Regression`: invalid credentials and failure-path coverage
- `Infrastructure`: configuration, factory, and helper validation

Example filters:

```bash
dotnet test --filter "Category=Smoke"
dotnet test --filter "Category=Regression"
dotnet test --filter "Category=Infrastructure"
```