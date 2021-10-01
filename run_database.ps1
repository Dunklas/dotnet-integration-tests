# Start mssql
$dbpid = docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu 

# Create todos database
docker exec -it $dbpid /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "yourStrong(!)Password" `
    -Q "CREATE DATABASE todos"

# Run migrations
dotnet ef database update --project .\src\TodoApi\
