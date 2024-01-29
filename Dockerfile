# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY *.sln .
COPY ["/Mini-Twitter.API/*.csproj", "./Mini-Twitter.API/"]
COPY ["/Mini-Twitter.Application/*.csproj", "./Mini-Twitter.Application/"]
COPY ["/Mini-Twitter.Domain/*.csproj", "./Mini-Twitter.Domain/"]
COPY ["/Mini-Twitter.Infrastructure/*.csproj", "./Mini-Twitter.Infrastructure/"]
COPY ["/Mini-Twitter.Tests/*.csproj", "./Mini-Twitter.Tests/"]
RUN dotnet restore
COPY . .
RUN dotnet build -c release -o output


# Publish Stage
From mcr.microsoft.com/dotnet/aspnet:7.0 as publish
WORKDIR /app
COPY --from=build /src/output .
ENTRYPOINT ["dotnet", "Mini-Twitter.API.dll"]
