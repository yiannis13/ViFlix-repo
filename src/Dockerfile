FROM microsoft/dotnet:2.1-sdk-alpine AS build

RUN mkdir /app
RUN mkdir /app/aspnetapp
RUN mkdir /app/ViFlix
RUN mkdir /app/ViFlix.Tests
RUN mkdir /app/ViFlix.DataAccess
RUN mkdir /app/Common

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY /ViFlix/*.csproj ./ViFlix/
COPY /ViFlix.Tests/*.csproj ./ViFlix.Tests/
COPY /Common/*.csproj ./Common/
COPY /ViFlix.DataAccess/*.csproj ./ViFlix.DataAccess/
RUN dotnet restore

# copy everything else and build app
COPY . ./aspnetapp/
WORKDIR /app/aspnetapp
RUN dotnet publish /app/aspnetapp/ViFlix -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/aspnetapp/out ./
ENTRYPOINT ["dotnet", "ViFlix.dll"]
CMD sleep 1d