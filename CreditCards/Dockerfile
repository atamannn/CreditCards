FROM mcr.microsoft.com/dotnet/sdk:2.1 as build-image
WORKDIR /build
COPY . .
RUN dotnet restore && dotnet build 
RUN dotnet publish -c Release

# ------------------------------------------------------------

FROM mcr.microsoft.com/dotnet/aspnet:2.1 as build
WORKDIR /app
COPY --from=build-image /build/bin/Release/netcoreapp2.1 /app
EXPOSE 8080/tcp

FROM mcr.microsoft.com/windows/servercore/iis:windowsservercore-ltsc2022
WORKDIR /app
COPY --from=build . .
ENTRYPOINT ["dotnet", "exec", "CreditCards.dll"]