# dotnet-integration-tests

Purpose of this repo is to try out some ways of running integration tests towards a .NET web application.

## Run tests using "real" database running in Docker

In this branch, integration tests bootstraps the application using `WebApplicationFactory<T>`. However, the application is not customized in any way while running the integration tests. It uses the same database context as in "production". Due to this, a database must be up and running while running the integration tests.

### Run tests

    `./run_database.ps1`
    `dotnet test`

### Some thoughts

**(+)** Tested code is exactly the same as production code

**(-)** Database must be started manually, outside of the tests
