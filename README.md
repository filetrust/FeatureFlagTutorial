# FeatureFlagTutorial

## Overview

Small ASP.NET Core app demonstrating the use of Feature flags and how it is used to manage new features for a system.  The following summarises how we achieved our understanding this.

- Create a standard ASP.NET Core app
- Followed https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core
- Created an App configuration in Azure https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core2x
- Made code changes to
  - Program.cs
  - Startup.cs
  - Appsettings.json (to use the access policy)
- Validation of the Feature flag
  - In Controllerl code
  - In an If..Else code block.

## References

The following are helpful tutorials and guides that were useful in fulfilling this FeatureFlagTutorial demonstration

- https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core (describes how to setup a small tutorial to make use of a feature flag)
  - https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core2x (describes how a feature is controlled in Azure Portal)
- https://github.com/MicrosoftDocs/azure-docs/blob/master/articles/azure-app-configuration/use-feature-flags-dotnet-core.md (examples to show how we can check the feature flag for if..else code blocks)
  - https://stackoverflow.com/questions/59332948/feature-management-feature-toggle-feature-flags-not-working-with-azure-app-con (another example showing use of feature flag if..else code block checks)