FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . .
RUN dotnet restore
RUN dotnet publish /app/MeliGalax.Service.API/MeliGalax.Service.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .

# Time Zone
ENV TZ=America/Bogota
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Environment
ARG KeyId
ARG KeySecret
ARG KeyUrl

ENV KeyId=$KeyId
ENV KeySecret=$KeySecret
ENV KeyUrl=$KeyUrl

ENTRYPOINT ["dotnet", "MeliGalax.Service.API.dll"]
