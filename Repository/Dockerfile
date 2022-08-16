FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /build_output

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Master.sln ./
COPY . .
RUN dotnet restore -nowarn:msb3202,nu1503
WORKDIR /src/
RUN dotnet build -c Staging -o /build_output TrialPurgeScheduledTask/TrialPurgeScheduledTask.csproj

FROM build AS publish
RUN dotnet publish -c Staging -o /build_output TrialPurgeScheduledTask/TrialPurgeScheduledTask.csproj

FROM base AS final
WORKDIR /build_output
COPY --from=publish /build_output .
ENTRYPOINT ["dotnet", "TrialPurgeScheduledTask.dll"]