# dotnet-integration-tests

Purpose of this repo is to try out some ways of running integration tests towards a .NET web application.

## Run tests using in memory database

In this branch, integration tests replaces the database used in the application with an in memory database. This is accomplished by launching the application from the test class (using a custom `WebApplicationFactory<T>`), where the database context is switched to an in memory implementation.
To see how this works, see [`InMemoryWebApplicationFactory.cs`](test/TodoApiTest/InMemoryWebApplicationFactory.cs), and how it's used in [`CreateTodo.cs`](test/TodoApiTest/CreateTodo.cs).

### Run tests

    dotnet test

### Some thoughts

 **(+)** No need to externally start a database before running tests

 **(-)** Only possible if Entity Framework (or other ORM with possibility to easily switch to in-memory implementation) is used

 **(-)** Tested code differs from production code
