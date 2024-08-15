FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/ETicaret.Web.Host/ETicaret.Web.Host.csproj", "src/ETicaret.Web.Host/"]
COPY ["src/ETicaret.Web.Core/ETicaret.Web.Core.csproj", "src/ETicaret.Web.Core/"]
COPY ["src/ETicaret.Application/ETicaret.Application.csproj", "src/ETicaret.Application/"]
COPY ["src/ETicaret.Core/ETicaret.Core.csproj", "src/ETicaret.Core/"]
COPY ["src/ETicaret.EntityFrameworkCore/ETicaret.EntityFrameworkCore.csproj", "src/ETicaret.EntityFrameworkCore/"]
WORKDIR "/src/src/ETicaret.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ETicaret.Web.Host", "src/ETicaret.Web.Host"]
COPY ["src/ETicaret.Web.Core", "src/ETicaret.Web.Core"]
COPY ["src/ETicaret.Application", "src/ETicaret.Application"]
COPY ["src/ETicaret.Core", "src/ETicaret.Core"]
COPY ["src/ETicaret.EntityFrameworkCore", "src/ETicaret.EntityFrameworkCore"]
WORKDIR "/src/src/ETicaret.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ETicaret.Web.Host.dll"]
