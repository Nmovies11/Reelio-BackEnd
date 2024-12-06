#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./API/API.csproj"
RUN dotnet publish "./API/API.csproj" -c release -o /app --no-restore
RUN dotnet restore "./BLL/BLL.csproj"
RUN dotnet publish "./BLL/BLL.csproj" -c release -o /app --no-restore
RUN dotnet restore "./DAL/DAL.csproj"
RUN dotnet publish "./DAL/DAL.csproj" -c release -o /app --no-restore
RUN dotnet restore "./Common/Common.csproj"
RUN dotnet publish "./Common/Common.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS serve
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]