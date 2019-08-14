FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["AdWorks.Production.Management.WebApi/AdWorks.Production.Management.WebApi.csproj", "AdWorks.Production.Management.WebApi/"]
RUN dotnet restore "AdWorks.Production.Management.WebApi/AdWorks.Production.Management.WebApi.csproj"
COPY . .
WORKDIR "/src/AdWorks.Production.Management.WebApi"
RUN dotnet build "AdWorks.Production.Management.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AdWorks.Production.Management.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AdWorks.Production.Management.WebApi.dll"]