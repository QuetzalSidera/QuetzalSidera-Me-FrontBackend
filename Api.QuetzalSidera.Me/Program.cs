using System.Globalization;
using System.Text.Json;
using Api.QuetzalSidera.Me.AuthHandler;
using Api.QuetzalSidera.Me.ContentHandler;
using Api.QuetzalSidera.Me.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using Serilog;

namespace Api.QuetzalSidera.Me;

public static class Program
{
    public static void Main(string[] args)
    {
        // 设置全局默认文化（影响所有线程）
        var culture = new CultureInfo("zh-CN");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
#if !DEBUG
        //从环境变量读取卷路径
        string volumePath = Environment.GetEnvironmentVariable("KEY_VOLUME_PATH")??"/app/Data/Key";
        // 持久化密钥到指定路径（容器外或挂载卷）
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(volumePath)) // 容器内路径
            .SetApplicationName("Api.QuetzalSidera.Me");

#else
        string path = Path.Combine(Environment.CurrentDirectory, "Data/Key");
        // 持久化密钥到指定路径（容器外或挂载卷）
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(path)) // 路径
            .SetApplicationName("Api.QuetzalSidera.Me"); // 同一服务必须一致

#endif
#if!DEBUG
        var logPath = Environment.GetEnvironmentVariable("LOG_VOLUME_PATH") ?? "/app/Data/Logs";

        // 确保日志目录存在
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }
#else
        var logPath = Path.Combine(Environment.CurrentDirectory, "Data/Logs");
#endif
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                Path.Combine(logPath, "Backend-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        builder.Host.UseSerilog(); // 使用 Serilog 作为日志框架
        
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.WriteIndented = false;
        });

        var app = builder.Build();
// 配置中间件管道
        app.UseRouting();

        app.UseAuthorization();
        // API不使用Cookie进行跨站攻击预防
        // app.UseAntiforgery();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapGet("/", () => new Result<string>()
        {
            Status = 0,
            Message = nameof(ErrorCode.Ok),
            Data="欢迎调用开放平台API👏"
        });
        //鉴权相关
        //AuthToken
        app.MapGet($"{AuthHandler.AuthHandler.Route}",
            async ([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password) =>
            await AuthHandler.AuthHandler.GetAuth(email, password)
        ).DisableAntiforgery();
        app.MapGet($"{AuthHandler.AuthHandler.CheckAuthRoute}",
            async ([FromQuery(Name = "user_guid")] string guid, [FromQuery(Name = "auth_token")] string authToken) =>
            await AuthHandler.AuthHandler.CheckAuth(guid, authToken)
        ).DisableAntiforgery();

        //Admin
        app.MapGet(AdminHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken) =>
        {
            var check = await AuthHandler.AuthHandler.CheckAuth(guid, authToken);
            if (check.Status != ErrorCode.Ok)
                return new Result<List<string>>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = null
                };
            return await AdminHandler.GetAdmins();
        }).DisableAntiforgery();

        app.MapPost(AdminHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
                [FromHeader(Name = "AuthToken")] string authToken, [FromForm(Name = "mailBox")] string mailBox) =>
            {
                var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
                if (!check)
                    return new Result()
                    {
                        Status = ErrorCode.Forbidden,
                        Message = "Forbidden",
                    };
                return await AdminHandler.AddAdmin(mailBox);
            }
        ).DisableAntiforgery();

        app.MapDelete(AdminHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
                [FromHeader(Name = "AuthToken")] string authToken, [FromForm(Name = "mailBox")] string mailBox) =>
            {
                var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
                if (!check)
                    return new Result()
                    {
                        Status = ErrorCode.Forbidden,
                        Message = "Forbidden",
                    };
                return await AdminHandler.DeleteAdmin(mailBox);
            }
        ).DisableAntiforgery();

        //Friend Card
        app.MapPost(FriendCardHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken, [FromBody] FriendCardHandler.Param param) =>
        {
            var check = await AuthHandler.AuthHandler.CheckAuth(guid, authToken);
            if (check.Status != ErrorCode.Ok)
                return new Result<string>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = null
                };
            return await FriendCardHandler.AddCard(param);
        }).DisableAntiforgery();

        app.MapGet(FriendCardHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken, [FromQuery(Name = "cardGuid")] string cardGuid) =>
        {
            var check = await AuthHandler.AuthHandler.CheckAuth(guid, authToken);
            if (check.Status != ErrorCode.Ok)
                return new Result<bool>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = false
                };
            return await FriendCardHandler.CheckStatus(cardGuid);
        }).DisableAntiforgery();

        app.MapGet(FriendCardHandler.RouteAll, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result<List<FriendCardHandler.Param>>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = null
                };
            return await FriendCardHandler.GetCheckList();
        }).DisableAntiforgery();

        app.MapDelete(FriendCardHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken, [FromQuery(Name = "cardGuid")] string cardGuid) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                };
            return await FriendCardHandler.RejectCheck(cardGuid);
        }).DisableAntiforgery();

        app.MapPost(FriendCardHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken, [FromQuery(Name = "cardGuid")] string cardGuid) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                };
            return await FriendCardHandler.PassCheck(cardGuid);
        }).DisableAntiforgery();

//Location
        app.MapGet(LocationHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result<string>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = null
                };
            return await LocationHandler.GetLocation();
        }).DisableAntiforgery();

        app.MapPost(LocationHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken, [FromForm(Name = "location")] string location) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                };
            return await LocationHandler.ModifyLocation(location);
        }).DisableAntiforgery();

        //UserNum
        app.MapGet(MetaDataHandler.MetaDataHandler.Route, async ([FromHeader(Name = "Authorization")] string guid,
            [FromHeader(Name = "AuthToken")] string authToken) =>
        {
            var check = await AuthHandler.AuthHandler.IsAdmin(guid, authToken);
            if (!check)
                return new Result<long>()
                {
                    Status = ErrorCode.Forbidden,
                    Message = "Forbidden",
                    Data = 0
                };
            return await MetaDataHandler.MetaDataHandler.GetNum();
        }).DisableAntiforgery();

        app.Run();
    }
}