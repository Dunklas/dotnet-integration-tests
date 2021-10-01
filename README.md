# dotnet-integration-tests

Purpose of this repo is to try out some ways of running integration tests towards a .NET web application.

## Run tests using in memory database

In this branch, integration tests replaces the database used in the application with an in memory database. This is accomplished by launching the application from the test class (using a custom `WebApplicationFactory<T>`), where the database context is switched to an in memory implementation.
To see how this works, see [`InMemoryWebApplicationFactory.cs`](test/TodoApiTest/InMemoryWebApplicationFactory.cs), and how it's used in [`CreateTodo.cs`](test/TodoApiTest/CreateTodo.cs).

 **(+)** No need to externally start a database before running tests

 **(-)** Only possible if Entity Framework (or other ORM with possibility to easily switch to in-memory implementation) is used

 **(-)** Tested code differs from production code

## Setup

Launch a mssql database using docker:

    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

Connect to mssql database:

    docker exec -it <DOCKER_CONTAINER_ID> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "yourStrong(!)Password"

Create database:

    CREATE DATABASE todos;
    GO

Run database migrations:

    dotnet ef database update

Make sure the following is present in `appsettings.json`:
```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433; Database=todos; User=sa; Password=yourStrong(!)Password;"
}
```

## Fire some requests!

View swagger documentation at:

    https://localhost:5001/swagger/index.html

Create a todo item:

    Invoke-WebRequest -Uri https://localhost:5001/api/TodoItems -Method POST -Body '{ "Name": "Ta helg", "IsComplete": false}' -ContentType "application/json"

Get all todo items:

    Invoke-WebRequest -Uri https://localhost:5001/api/TodoItems -Method GET

Delete a todo item:

    Invoke-WebRequest -Uri https://localhost:5001/api/TodoItems/{id} -Method DELETE
