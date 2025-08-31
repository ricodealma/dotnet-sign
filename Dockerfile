FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Dotnet.Sign.App/Dotnet.Sign.App.csproj", "src/Dotnet.Sign.App/"]
COPY ["src/Dotnet.Sign.Domain/Dotnet.Sign.Domain.csproj", "src/Dotnet.Sign.Domain/"]
COPY ["src/Dotnet.Sign.Infra/Dotnet.Sign.Infra.csproj", "src/Dotnet.Sign.Infra/"]


RUN dotnet restore "src/Dotnet.Sign.App/Dotnet.Sign.App.csproj"
COPY . .
WORKDIR "/src/src/Dotnet.Sign.App"
RUN dotnet build "Dotnet.Sign.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Sign.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Dotnet.Sign.App.dll"]