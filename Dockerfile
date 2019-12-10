FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
COPY src/ ./
RUN cd HarborLane.Api \
    &&dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/HarborLane.Api/out .
ENTRYPOINT ["dotnet", "HarborLane.Api.dll"]
