using System.Globalization;
using Chat.QuetzalSidera.Me.Components;
using Chat.QuetzalSidera.Me.Service;
using Grpc.Client.Chat;
using Microsoft.AspNetCore.DataProtection;
using Serilog;

namespace Chat.QuetzalSidera.Me;

public class Program
{
    public static void Main(string[] args)
    {
        // 设置全局默认文化（影响所有线程）
        var culture = new CultureInfo("zh-CN");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
#if !DEBUG
        string volumePath = Environment.GetEnvironmentVariable("KEY_VOLUME_PATH")??"/app/Data/Key";
        // 持久化密钥到指定路径（容器外或挂载卷）
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(volumePath)) // 容器内路径
            .SetApplicationName("Chat.QuetzalSidera.Me"); // 同一服务必须一致
#else
        string path = Path.Combine(Environment.CurrentDirectory, "Data/Key");
        // 持久化密钥到指定路径（容器外或挂载卷）
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(path)) // 路径
            .SetApplicationName("Chat.QuetzalSidera.Me"); // 同一服务必须一致

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

        
        builder.Services.AddScoped<ChatMessageService>();
        builder.Services.AddScoped<ChatSessionService>();
        builder.Services.AddScoped<UserInfoService>();
        builder.Services.AddScoped<ChatService>();
        builder.Services.AddScoped<AppState.AppState>();
        builder.Services.AddHttpClient();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.MapStaticAssets();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}