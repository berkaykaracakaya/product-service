# Build image
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
ENV ASPNETCORE_VERSION 5
WORKDIR /workdir

COPY . .

RUN dotnet nuget locals all -c
RUN dotnet restore ./Product.Api/Product.Api.csproj
RUN dotnet publish ./Product.Api/Product.Api.csproj -o /publish -c release

#Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY --from=build-env /publish /publish

WORKDIR /publish
ENTRYPOINT [ "dotnet", "Product.Api.dll" ]
