using System.Globalization;
using Backend.ApiEntities;
using Backend.ChatEntities;
using Backend.GrpcServer;
using Backend.Shared;
using Backend.WwwEntities;
using Grpc.Share.Config.Www;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Backend;

public static class Program
{
    static void Main(string[] args)
    {
        // 设置全局默认文化（影响所有线程）
        var culture = new CultureInfo("zh-CN");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(options =>
        {
            //端口脱敏
            options.ListenAnyIP(int.MaxValue,o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
        });

        builder.Services.AddDbContext<ChatContext>();
        builder.Services.AddDbContext<WwwContext>();
        builder.Services.AddDbContext<ApiContext>();

        // 注册 gRPC 服务
        builder.Services.AddGrpc();
        // 配置 Serilog
        // 获取日志路径
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

        var app = builder.Build();


        // 迁移所有数据库
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            // 迁移 Chat 数据库
            var chatContext = serviceProvider.GetRequiredService<ChatContext>();
            chatContext.Database.Migrate();

            // 迁移 Www 数据库  
            var wwwContext = serviceProvider.GetRequiredService<WwwContext>();
            wwwContext.Database.Migrate();

            // 迁移 Api 数据库
            var apiContext = serviceProvider.GetRequiredService<ApiContext>();
            apiContext.Database.Migrate();
        }


        // 映射 gRPC 服务类
        //Www
        app.MapGrpcService<HtmlHeaderService>();
        app.MapGrpcService<TitleService>();
        app.MapGrpcService<WeatherService>();
        app.MapGrpcService<NavService>();
        app.MapGrpcService<ProfileService>();
        app.MapGrpcService<ContentService>();
        app.MapGrpcService<FooterService>();

        //Chat
        app.MapGrpcService<ChatMessageService>();
        app.MapGrpcService<UserInfoService>();
        app.MapGrpcService<ChatSessionService>();

        //Api
        app.MapGrpcService<ApiAuthService>();
        app.MapGrpcService<CheckListService>();
        app.MapGrpcService<MetaDataService>();
        app.MapGet("/", () => "This is a gRPC server. Use a gRPC client to connect.");

        //初始化种子数据
        using var db = new WwwContext();
        if (!(db.Headers.Any(h => h.Id == WwwContext.Id)))
        {
            db.Headers.Add(new HeaderEntity()
            {
                Id = WwwContext.Id,
                HtmlHeader = Config.ConfigHtmlHeaderModel,
                Title = Config.ConfigTitleModel,
                Location = Config.ConfigLocationModel,
                Nav = Config.ConfigNavModel,
                Profile = Config.ConfigProfileModel,
            });
        }

        if (!db.Layouts.Any(l => l.Path == ConfigData.PathConfig.RootPath))
            db.Layouts.Add(new LayoutEntity
            {
                Path = ConfigData.PathConfig.RootPath,
                Layout = Config.ConfigRootLayoutModel
            });
        if (!db.Layouts.Any(l => l.Path == ConfigData.PathConfig.ThankYouPath))
            db.Layouts.Add(new LayoutEntity
            {
                Path = ConfigData.PathConfig.ThankYouPath,
                Layout = Config.ConfigThankYouLayoutModel
            });
        if (!db.Foot.Any(l => l.Id == WwwContext.Id))
            db.Foot.Add(new FooterEntity()
            {
                Id = WwwContext.Id,
                Foot = Config.ConfigFootModel,
            });
        db.SaveChanges();

        // 定时器任务
        const long chatSelfCleanInterval = 4 * 60 * 60 * 1000; //扫描数据库清理时间间隔(4h)
        TimerTask chatTimerTask = new TimerTask((_, _) => { ChatEntities.DatabaseHelper.SelfClean(); },
            chatSelfCleanInterval);
        const long apiSelfCleanInterval = 3 * 60 * 60 * 1000; //扫描数据库清理时间间隔(3h)
        TimerTask apiTimerTask = new TimerTask((_, _) => { ApiEntities.DatabaseHelper.SelfClean(); },
            apiSelfCleanInterval);
        app.Run();
    }
}