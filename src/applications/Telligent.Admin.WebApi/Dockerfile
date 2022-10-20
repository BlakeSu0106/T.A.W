#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Telligent.Admin.WebApi/Telligent.Admin.WebApi.csproj", "Telligent.Admin.WebApi/"]
COPY ["src/modules/Telligent.Admin.Application/Telligent.Admin.Application.csproj", "src/modules/Telligent.Admin.Application/"]
COPY ["src/modules/Telligent.Admin.Database/Telligent.Admin.Database.csproj", "src/modules/Telligent.Admin.Database/"]
COPY ["src/modules/Telligent.Admin.Domain/Telligent.Admin.Domain.csproj", "src/modules/Telligent.Admin.Domain/"]


RUN dotnet nuget add source http://teola.3rdchannel.com.tw/Nuget/nuget/ -n Telexpress
RUN dotnet restore "Telligent.Admin.WebApi/Telligent.Admin.WebApi.csproj"
COPY . .
WORKDIR "/src/Telligent.Admin.WebApi"
RUN dotnet build "Telligent.Admin.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telligent.Admin.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telligent.Admin.WebApi.dll"]
