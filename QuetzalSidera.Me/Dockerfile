FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuetzalSidera.Me/QuetzalSidera.Me.csproj", "QuetzalSidera.Me/"]
RUN dotnet restore "QuetzalSidera.Me/QuetzalSidera.Me.csproj"
COPY . .
WORKDIR "/src/QuetzalSidera.Me"
RUN dotnet build "./QuetzalSidera.Me.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuetzalSidera.Me.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
##文化信息：中文中国
ENV LANG=zh_CN.UTF-8
ENV LANGUAGE=zh_CN:zh
ENV LC_ALL=zh_CN.UTF-8
ENV TZ=Asia/Shanghai


#脱敏
EXPOSE 00000
ENTRYPOINT ["dotnet", "QuetzalSidera.Me.dll"]
