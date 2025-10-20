FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Chat.QuetzalSidera.Me/Chat.QuetzalSidera.Me.csproj", "Chat.QuetzalSidera.Me/"]
RUN dotnet restore "Chat.QuetzalSidera.Me/Chat.QuetzalSidera.Me.csproj"
COPY . .
WORKDIR "/src/Chat.QuetzalSidera.Me"
RUN dotnet build "./Chat.QuetzalSidera.Me.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Chat.QuetzalSidera.Me.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
##文化信息：中文中国
ENV LANG=zh_CN.UTF-8
ENV LANGUAGE=zh_CN:zh
ENV LC_ALL=zh_CN.UTF-8
ENV TZ=Asia/Shanghai


#脱敏
EXPOSE 00000
ENTRYPOINT ["dotnet", "Chat.QuetzalSidera.Me.dll"]
