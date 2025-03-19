# test-code-with-features
Delete me after 3-18-2026

# Child Sponsorship Demo Repository

This repository serves as a comprehensive technical demonstration environment tailored specifically for a child sponsorship organization's operations. It is designed to facilitate the exploration, testing, and validation of GitHub Advanced Security features, covering a broad spectrum of development technologies and practices used within our organization.

---

## Purpose

The primary goal of this repository is to provide a realistic and diverse codebase that represents the typical software development and infrastructure needs of a child sponsorship organization. It encompasses web applications, CRM solutions (Dynamics 365 and Power Apps), databases (SQL), serverless solutions (Azure Functions), and infrastructure as code.

This repository will specifically help us test and demonstrate:

- Secret scanning
- Dependency vulnerability scanning
- Code scanning (Static Application Security Testing)
- Infrastructure security checks

---

## Repository Structure

The repository is organized into distinct folders, each dedicated to a specific technological domain:

```
/child-sponsor-demo
├── /web-portal # Web application built with ASP.NET Core and React
├── /crm-powerapps # Dynamics 365 customizations and Power Apps canvas/model-driven apps
├── /database # SQL database schema and migration scripts
├── /azure-functions # Azure Functions for background processing and event handling
└── /infrastructure # Infrastructure as Code (IaC) using Terraform and ARM templates
```

---

## Technology Overview

### Web Portal

- **Frontend:** TypeScript/javascript
- **Backend:** ASP.NET Core Web API
- **Authentication:** Azure AD B2C/B2B integration

### CRM and Power Apps

- **Dynamics 365**: Custom entities, forms, and plugins
- **Power Apps**: Canvas apps and Model-driven apps for sponsorship management
- **Automation**: Power Automate workflows

### Database

- SQL Server Database stored procs
- Schema and migration scripts (T-SQL)
- Database seeding scripts for testing
- Snowflake related code
- Datalake related code. 

### Azure Functions

- Event-driven processing
- .NET 8 based Azure Functions
- Integration with Azure Service Bus and Storage

### Infrastructure

- Bicep scripts for Azure resources provisioning
- Powershell scripts
- Azure DevOps pipelines definitions for CI/CD

---

## Security Testing Goals

By leveraging GitHub Advanced Security, we aim to:

- Detect and remediate potential secrets leaked into source control
- Identify and address vulnerabilities in third-party dependencies
- Implement secure coding practices through static analysis
- Ensure infrastructure code meets security best practices and compliance standards

---

## Getting Started

### Clone Repository

```
git clone https://github.com/ChildrenInternational/test-code-with-features.git
```

### Running Locally

Follow the README.md in each sub-folder for specific instructions on setting up and running each component.

---


## License

This project is unlicensed.