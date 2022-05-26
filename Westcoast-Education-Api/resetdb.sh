set -x
dotnet ef database drop --force 
dotnet ef database update
dotnet run