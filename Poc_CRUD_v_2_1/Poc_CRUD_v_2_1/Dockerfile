FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Poc_CRUD_v_2_1/Poc_CRUD_v_2_1.csproj", "Poc_CRUD_v_2_1/"]
RUN dotnet restore "Poc_CRUD_v_2_1/Poc_CRUD_v_2_1.csproj"
COPY . .
WORKDIR "/src/Poc_CRUD_v_2_1"
RUN dotnet build "Poc_CRUD_v_2_1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poc_CRUD_v_2_1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc_CRUD_v_2_1.dll"]