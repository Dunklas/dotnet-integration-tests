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
