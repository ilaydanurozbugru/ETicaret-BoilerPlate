FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/ETicaret.Web.Mvc/ETicaret.Web.Mvc.csproj", "src/ETicaret.Web.Mvc/"]
COPY ["src/ETicaret.Web.Core/ETicaret.Web.Core.csproj", "src/ETicaret.Web.Core/"]
COPY ["src/ETicaret.Application/ETicaret.Application.csproj", "src/ETicaret.Application/"]
COPY ["src/ETicaret.Core/ETicaret.Core.csproj", "src/ETicaret.Core/"]
COPY ["src/ETicaret.EntityFrameworkCore/ETicaret.EntityFrameworkCore.csproj", "src/ETicaret.EntityFrameworkCore/"]
WORKDIR "/src/src/ETicaret.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ETicaret.Web.Mvc", "src/ETicaret.Web.Mvc"]
COPY ["src/ETicaret.Web.Core", "src/ETicaret.Web.Core"]
COPY ["src/ETicaret.Application", "src/ETicaret.Application"]
COPY ["src/ETicaret.Core", "src/ETicaret.Core"]
COPY ["src/ETicaret.EntityFrameworkCore", "src/ETicaret.EntityFrameworkCore"]
WORKDIR "/src/src/ETicaret.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ETicaret.Web.Mvc.dll"]
