![GitHub badge](https://img.shields.io/github/workflow/status/twoteesbrett/alphacert-interview/.NET)

# Technical Assessment for AlphaCert
This pertains to the due diligence for AlphaCert. This has been forked from:

https://github.com/AlphaCertLabs/Interview

This repository states the requirements of the API.

# Refactoring
* Forked the project and created a develop branch as the working branch
* Upgraded to .NET 6 for long term support and updated NuGet packages
* Consolidated CanWeFixIt projects into one solution since it is simple
* Implement a minimal API
* Changed the DatabaseService to a repository so that it is abstracted; some other data source could be used later
* Removed SetupDatabase from the interface as this is implementation specific
* Converted to use EF
* Added seed data
* Fix the bug for getting MarketData where the filter was incorrectly set to return inactive items
* Moved the connection string to appsettings.json/environment variable
* Added integration tests
* Completed the method for MarketValuation

# Decisions
* Choosing EF to showcase code first skills
* Choosing integration tests over unit tests for end-to-end testing

# Ideation
* Persist data with a file rather than in-memory
* Secret Manager
* Custom exception handler/route
* Secure the API with authentication and authorisation
* CORS
* Logging
* Paging of data with offset and limit
* OpenAPI documentation with Swashbuckle