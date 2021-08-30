- cd ./product-service
- docker-compose up --build
+ new Terminal
- cd Product.Infrastructure
- dotnet ef database update -c ProductDbContext -s ../Product.Api/Product.Api.csproj
- first terminal CTRL+C
- docker-compose up --build
- Project started in -> localhost:8085