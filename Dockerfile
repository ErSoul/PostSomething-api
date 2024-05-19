FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY ./ ./
RUN dotnet publish --configuration Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-composite as release

EXPOSE 8080/tcp

ENV ASPNETCORE_ENVIRONMENT "Development"
ENV ConnectionStrings__InMemoryConnection "sample-database"
ENV JWT__Key "jrwoigjwroemfkdlgjwoijtgiowrpa[[q0er315i35ji3ofkrfmdj"
ENV JWT__Issuer "http://localhost:3000"
ENV JWT__Audience "http://localhost:3000"
ENV AppURL "localhost:3000"

COPY --from=build /app /app
CMD ["dotnet", "/app/PostSomething-api.dll"]