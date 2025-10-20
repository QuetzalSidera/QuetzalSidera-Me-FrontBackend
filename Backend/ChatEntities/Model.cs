using System.Collections.Concurrent;
using System.Text.Json;
using Backend.ApiEntities;
using Backend.Shared;
using Backend.ThirdParty.Email;
using Grpc.Share.Protos.ChatModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.ChatEntities;

//TODO 后端关闭时，前端记得返回错误信息而不是直接崩服✅
//TODO API与www的编辑功能与测试✅
//TODO 公安联网备案与部署✅
//TODO 开放平台API文档编写✅
//TODO AI提示词改写✅
//TODO 致谢✅
//TODO SelfClean✅
//TODO 线程锁与前端加载状态✅
public class ChatContext : DbContext
{
    public DbSet<UserInfoModel> Users { get; set; }
    public DbSet<VerifyCodeModel> VerifyCodes { get; set; }
    public DbSet<ChatHistoryEntity> ChatHistories { get; set; }

    public DbSet<AuthTokenEntity> AuthTokens { get; set; }

    public string DbPath { get; }

    public ChatContext()
    {
#if DEBUG
        string path = Environment.CurrentDirectory;
        DbPath = Path.Combine(path, "Data/chat.db");
#else
 string volumePath = Environment.GetEnvironmentVariable("CHAT_VOLUME_PATH") ?? "/app/Data/Chat";
// 确保目录存在
        if (!Directory.Exists(volumePath))
        {
            Directory.CreateDirectory(volumePath);
        }

        DbPath = System.IO.Path.Combine(volumePath, "chat.db");
#endif
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfoModel>()
            .HasKey(c => c.UserGuid); // 指定Id为主键
        modelBuilder.Entity<VerifyCodeModel>()
            .HasKey(c => c.MailBox); //注意验证码的主键是邮箱，因为此时还没有用户guid定Id为主键
        modelBuilder.Entity<AuthTokenEntity>()
            .HasKey(c => c.AuthTokenGuid);

        modelBuilder.Entity<ChatHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.UserGuid);
                entity.Property(e => e.History)
                    .HasColumnType("TEXT")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }),
                        v => JsonSerializer.Deserialize<ChatHistoryModel>(v, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }) ?? new ChatHistoryModel()
                    );
            }
        );
    }
}

public enum AuthTokenType
{
    Login,
    Traveler,
    None
}

public class ChatHistoryEntity
{
    public string UserGuid { get; set; } = string.Empty;
    public ChatHistoryModel History { get; set; } = new();

    public static implicit operator ChatHistoryEntity(ChatHistoryModel historyModel)
    {
        return new ChatHistoryEntity()
        {
            UserGuid = historyModel.UserGuid,
            History = historyModel,
        };
    }

    public static implicit operator ChatHistoryModel(ChatHistoryEntity historyEntity)
    {
        return historyEntity.History;
    }
}

/// <summary>
/// 同时服务于网页端与Api端的AuthTokenModel
/// </summary>
public class AuthTokenEntity
{
    public string AuthTokenGuid { get; set; } = string.Empty;

    /// <summary>
    /// 用户Guid
    /// </summary>
    public string UserGuid { get; set; } = string.Empty;

    /// <summary>
    /// Cookie字符串
    /// </summary>
    public string CookieString { get; set; } = string.Empty;

    /// <summary>
    /// 创建时 时间戳
    /// </summary>
    public long CreateTimestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    /// <summary>
    /// 上一次访问时 时间戳
    /// </summary>
    public long LastAccessTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    /// <summary>
    /// 是否是游客用户
    /// </summary>
    public bool IsRegistered { get; set; } = false;

    public static implicit operator AuthTokenModel(AuthTokenEntity authTokenEntity)
    {
        return new AuthTokenModel()
        {
            UserGuid = authTokenEntity.UserGuid,
            CookieString = authTokenEntity.CookieString,
            IsRegistered = authTokenEntity.IsRegistered,
            CreateTimestamp = authTokenEntity.CreateTimestamp,
        };
    }

    public static implicit operator AuthTokenEntity(AuthTokenModel authTokenModel)
    {
        return new AuthTokenEntity()
        {
            AuthTokenGuid = Guid.NewGuid().ToString(),
            UserGuid = authTokenModel.UserGuid,
            CookieString = authTokenModel.CookieString,
            IsRegistered = authTokenModel.IsRegistered,
            CreateTimestamp = authTokenModel.CreateTimestamp,
            LastAccessTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
    }
}

public static class DatabaseHelper
{
    /// <summary>
    /// 查询是否有记录，无论游客还是登录用户都会返回true
    /// </summary>
    /// <param name="authTokenModel"></param>
    /// <returns></returns>
    public static bool IsAuthed(AuthTokenModel authTokenModel)
    {
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            authTokenModel.CreateTimestamp == 0)
            return false;
        using ChatContext db = new ChatContext();
        var result = db.AuthTokens.FirstOrDefault(a => (a.UserGuid == authTokenModel.UserGuid)
                                                       && (a.CookieString == authTokenModel.CookieString)
                                                       && (a.CreateTimestamp == authTokenModel.CreateTimestamp)
        );
        if (result == null)
            return false;
        result.LastAccessTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        db.Entry(result).State = EntityState.Modified;
        db.SaveChanges();
        return true;
    }
    
    public static AuthTokenType CheckAuthType(AuthTokenModel authTokenModel)
    {
        using ChatContext db = new ChatContext();
        var result = db.AuthTokens.FirstOrDefault(a => (a.UserGuid == authTokenModel.UserGuid)
                                                       && (a.CookieString == authTokenModel.CookieString)
                                                       && (a.CreateTimestamp == authTokenModel.CreateTimestamp)
        );
        if (result == null)
            return AuthTokenType.None;
        result.LastAccessTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        db.Entry(result).State = EntityState.Modified;
        db.SaveChanges();
        return result.IsRegistered ? AuthTokenType.Login : AuthTokenType.Traveler;
    }

    public static ChatHistoryModel? LookUpForHistory(AuthTokenModel authTokenModel)
    {
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            authTokenModel.CreateTimestamp == 0)
            return null;
        if (!IsAuthed(authTokenModel))
            return null;
        using ChatContext db = new ChatContext();
        var result = db.ChatHistories.AsNoTracking().Where(c => (c.UserGuid == authTokenModel.UserGuid))
            .Select(c => c.History);
        return result.FirstOrDefault();
    }

    private static readonly ConcurrentDictionary<string, Lock> HistoryLocks = new ();

    /// <summary>
    /// Merge用户的对话历史记录
    /// </summary>
    /// <param name="chatHistoryModel"></param>
    /// <returns></returns>
    public static bool MergeHistory(ChatHistoryModel chatHistoryModel)
    {
        if (string.IsNullOrEmpty(chatHistoryModel.UserGuid))
            return false;
        if (chatHistoryModel.History.Count == 0)
            return true;
        if (!HistoryLocks.ContainsKey(chatHistoryModel.UserGuid))
            HistoryLocks.TryAdd(chatHistoryModel.UserGuid, new Lock());
        lock (HistoryLocks[chatHistoryModel.UserGuid])
        {
            using ChatContext db = new ChatContext();
            var result = db.ChatHistories.AsNoTracking().FirstOrDefault(e => e.UserGuid == chatHistoryModel.UserGuid);
            if (result == null)
                return false;
            //入库前分配Guid，并补充可能缺失的属性
            foreach (var session in chatHistoryModel.History)
            {
                if (session.SessionGuid == string.Empty)
                    session.SessionGuid = Guid.NewGuid().ToString();
                if (session.CreateTimestamp == 0)
                    session.CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                foreach (var message in session.Content)
                {
                    if (message.MessageGuid == string.Empty)
                        message.MessageGuid = Guid.NewGuid().ToString();
                    if (message.Timestamp == 0)
                        message.Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                }
            }

            db.Attach(result);
            result.History.History.AddRange(chatHistoryModel.History);
            result.History.History.Sort((a, b) => b.CreateTimestamp.CompareTo(a.CreateTimestamp));
            db.Entry(result).Property(e => e.History).IsModified = true;
            db.Entry(result).State = EntityState.Modified;
            db.SaveChanges();
        }

        return true;
    }

    public static ChatSessionModel? CreateSession(AuthTokenModel authTokenModel)
    {
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            authTokenModel.CreateTimestamp == 0)
            return null;
        if (!IsAuthed(authTokenModel))
            return null;
        if (!HistoryLocks.ContainsKey(authTokenModel.UserGuid))
            HistoryLocks.TryAdd(authTokenModel.UserGuid, new Lock());
        lock (HistoryLocks[authTokenModel.UserGuid])
        {
            using ChatContext db = new ChatContext();
            var history = db.ChatHistories.AsNoTracking().FirstOrDefault(e => e.UserGuid == authTokenModel.UserGuid);
            if (history == null)
                return null;
            var session = new ChatSessionModel()
            {
                Title = "新对话",
                Content = [],
                SessionGuid = Guid.NewGuid().ToString(),
                CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            };
            db.Attach(history);
            history.History.History.Insert(0, session);
            db.Entry(history).Property(e => e.History).IsModified = true;
            db.Entry(history).State = EntityState.Modified;
            db.SaveChanges();
            return session;
        }
    }

    public static ChatSessionModel? LookUpForSession(AuthTokenModel authTokenModel, string sessionGuid)
    {
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            authTokenModel.CreateTimestamp == 0 || string.IsNullOrEmpty(sessionGuid))
            return null;
        if (!IsAuthed(authTokenModel))
            return null;
        ChatHistoryModel? history = LookUpForHistory(authTokenModel);
        var sessions = history?.History;
        return sessions?.FirstOrDefault(s => s.SessionGuid == sessionGuid);
    }
    
    /// <summary>
    /// 保存会话Session(并非新建)
    /// </summary>
    public static bool UpdateSession(AuthTokenModel authTokenModel, string sessionGuid, List<ChatMessageModel> messages,
        string? title = null)
    {
        //数据完整性检验
        if (string.IsNullOrEmpty(sessionGuid))
            return false;
        bool messagesCheck = messages.TrueForAll(message =>
        {
            var messageGuid = message.MessageGuid != string.Empty;
            var timestamp = message.Timestamp != 0;
            var isThinking = !message.IsThinkMessage;
            return messageGuid && timestamp && isThinking;
        });
        if (!messagesCheck)
            return false;
        if (!IsAuthed(authTokenModel))
            return false;

        if (!HistoryLocks.ContainsKey(authTokenModel.UserGuid))
            HistoryLocks.TryAdd(authTokenModel.UserGuid, new Lock());
        lock (HistoryLocks[authTokenModel.UserGuid])
        {
            using ChatContext db = new ChatContext();

            var historyEntity = db.ChatHistories.Find(authTokenModel.UserGuid);
            if (historyEntity == null)
                return false;
            try
            {
                var rawSession = historyEntity.History.History.Where(s => s.SessionGuid == sessionGuid).ToList()
                    .FirstOrDefault();
                if (title != null && rawSession != null)
                    rawSession.Title = title;
                if (rawSession != null)
                {
                    rawSession.Content.AddRange(messages);
                    db.Entry(historyEntity).Property(h => h.History).IsModified = true;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Log.Error(e, "操作错误Backend.ChatEntities.DatabaseHelper.UpdateSession");
                return false;
            }
        }
    }

    /// <summary>
    /// 为userGuid在History表中建立空表项
    /// </summary>
    /// <param name="userGuid"></param>
    public static void CreateHistory(string userGuid)
    {
        if (string.IsNullOrEmpty(userGuid))
            return;
        using ChatContext db = new ChatContext();
        var history = new ChatHistoryEntity()
        {
            UserGuid = userGuid,
            History = new ChatHistoryModel()
            {
                UserGuid = userGuid,
                History = new List<ChatSessionModel>()
            }
        };
        db.ChatHistories.Add(history);
        db.SaveChanges();
    }

    /// <summary>
    /// 新建游客AuthToken
    /// </summary>
    /// <returns></returns>
    public static AuthTokenModel CreateTempAuthToken()
    {
        using ChatContext db = new ChatContext();
        AuthTokenModel a = new AuthTokenModel()
        {
            UserGuid = Guid.NewGuid().ToString(),
            CookieString = Guid.NewGuid().ToString(),
            IsRegistered = false,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        AuthTokenEntity authTokenEntity = a;
        authTokenEntity.LastAccessTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        db.AuthTokens.Add(authTokenEntity);
        db.SaveChanges();
        return a;
    }

    /// <summary>
    /// 为userGuid新建非游客的AuthToken
    /// </summary>
    public static AuthTokenModel? CreateAuthToken(string userGuid)
    {
        if (string.IsNullOrWhiteSpace(userGuid))
            return null;
        using ChatContext db = new ChatContext();
        AuthTokenModel a = new AuthTokenModel()
        {
            UserGuid = userGuid,
            CookieString = Guid.NewGuid().ToString(),
            IsRegistered = true,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        AuthTokenEntity authTokenEntity = a;
        authTokenEntity.LastAccessTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        db.AuthTokens.Add(authTokenEntity);
        db.SaveChanges();
        return a;
    }

    /// <summary>
    /// 查询已注册用户的账号信息
    /// </summary>
    /// <param name="authTokenModel"></param>
    /// <returns></returns>
    public static UserInfoModel? LookUpForUser(AuthTokenModel authTokenModel)
    {
        //没有记录或者是游客 返回null
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            !authTokenModel.IsRegistered || authTokenModel.CreateTimestamp == 0)
            return null;
        if (!IsAuthed(authTokenModel))
            return null;
        using ChatContext db = new ChatContext();
        var result = db.Users.AsNoTracking().FirstOrDefault(u => u.UserGuid == authTokenModel.UserGuid);
        Log.Information("Successfully looked up user, info begin");
        Log.Information("用户 {Mailbox} (NickName: {NickName}) 查询成功", result?.MailBox, result?.NickName);
        Log.Information("Successfully looked up user, info end");
        return result;
    }

    public static UserInfoModel? LookUpForUser(string mailBox)
    {
        //没有记录或者是游客 返回null
        if (string.IsNullOrEmpty(mailBox))
            return null;
        using ChatContext db = new ChatContext();
        var result = db.Users.AsNoTracking().FirstOrDefault(u => u.MailBox == mailBox);
        return result;
    }

    public static AuthTokenModel? PasswordLogin(UserInfoModel userInfoModel)
    {
        if (string.IsNullOrEmpty(userInfoModel.MailBox) || string.IsNullOrEmpty(userInfoModel.Password))
            return null;
        using ChatContext db = new ChatContext();
        var user = db.Users.AsNoTracking().FirstOrDefault(u => u.MailBox == userInfoModel.MailBox);
        if (user == null)
        {
            return null;
        }

        bool result = PasswordHasher.VerifyPassword(userInfoModel.Password, user.HashedPassword);
        return result ? CreateAuthToken(user.UserGuid) : null;
    }

    /// <summary>
    /// 原本存在User返回false
    /// </summary>
    /// <param name="userInfoModel"></param>
    /// <returns></returns>
    public static UserInfoModel? CreateUser(UserInfoModel userInfoModel)
    {
        if (string.IsNullOrEmpty(userInfoModel.MailBox) || string.IsNullOrEmpty(userInfoModel.NickName) ||
            string.IsNullOrEmpty(userInfoModel.Password))
            return null;
        using ChatContext db = new ChatContext();
        var result = db.Users.AsNoTracking().FirstOrDefault(u => u.MailBox == userInfoModel.MailBox);
        if (result != null)
            return null;
        Log.Information("Successfully created user, info begin");
        Log.Information("用户 {Mailbox} (NickName: {NickName}) 创建成功", userInfoModel.MailBox, userInfoModel.NickName);
        Log.Information("Successfully created user, info end");
        UserInfoModel newUser = new UserInfoModel()
        {
            UserGuid = Guid.NewGuid().ToString(),
            MailBox = userInfoModel.MailBox,
            HashedPassword = PasswordHasher.HashPassword(userInfoModel.Password),
            NickName = userInfoModel.NickName,
        };
        db.Users.Add(newUser);
        db.SaveChanges();
        return newUser;
    }

    /// <summary>
    /// 存在此邮箱下的VerifyCode则更新，否则添加
    /// </summary>
    /// <returns></returns>
    public static void InsertVerifyCode(VerifyCodeModel verifyCodeModel)
    {
        if (string.IsNullOrEmpty(verifyCodeModel.MailBox) ||
            string.IsNullOrEmpty(verifyCodeModel.VerifyCode) ||
            verifyCodeModel.CreateTimestamp == 0
           )
        {
            return;
        }

        using ChatContext db = new ChatContext();
        var verifyCode = db.VerifyCodes.AsNoTracking().FirstOrDefault(v => v.MailBox == verifyCodeModel.MailBox);
        if (verifyCode == null)
        {
            db.VerifyCodes.Add(verifyCodeModel);
        }
        else
        {
            db.Attach(verifyCode);
            db.Entry(verifyCode).State = EntityState.Modified;
            // verifyCode.MailBox = verifyCodeModel.MailBox;//不能修改主键
            verifyCode.VerifyCode = verifyCodeModel.VerifyCode;
            verifyCode.CreateTimestamp = verifyCodeModel.CreateTimestamp;
            verifyCode.VerifyCodeType = verifyCodeModel.VerifyCodeType;
        }

        db.SaveChanges();
    }

    public static bool VerifyVerifyCode(VerifyCodeModel verifyCodeModel)
    {
        using ChatContext db = new ChatContext();
        var answer = db.VerifyCodes.AsNoTracking().FirstOrDefault(v => v.MailBox == verifyCodeModel.MailBox);
        if (answer == null)
        {
            return false;
        }

        //验证码是否过期
        long now = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (now - answer.CreateTimestamp > Mail163.ExpireMinutes * 60)
        {
            Log.Information("验证码Mailbox: {Mailbox},Code: {VerifyCode} 已过期", answer.MailBox, answer.VerifyCode);
            db.Attach(answer);
            db.Entry(answer).State = EntityState.Deleted;
            db.SaveChanges();
            return false;
        }

        //只验证验证码，不验证VerifyCodeType
        if (answer.VerifyCode == verifyCodeModel.VerifyCode)
        {
            //验证成功，从数据库中移除表项
            db.Attach(answer);
            db.Entry(answer).State = EntityState.Deleted;
            db.SaveChanges();
            return true;
        }
        else
        {
            //验证失败不移除
            return false;
        }
    }

    /// <summary>
    /// 移除AuthTokenModel
    /// </summary>
    /// <param name="authTokenModel"></param>
    /// <returns></returns>
    public static bool RemoveAuthToken(AuthTokenModel authTokenModel)
    {
        using var db = new ChatContext();
        if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
            authTokenModel.CreateTimestamp == 0)
            return false;
        var result = db.AuthTokens.AsNoTracking().FirstOrDefault(entity =>
            entity.UserGuid == authTokenModel.UserGuid &&
            entity.CookieString == authTokenModel.CookieString &&
            entity.IsRegistered == authTokenModel.IsRegistered &&
            entity.CreateTimestamp == authTokenModel.CreateTimestamp);
        if (result == null)
            return false;
        db.Entry(result).State = EntityState.Deleted;
        db.SaveChanges();
        return true;
    }

    /// <summary>
    /// 适用于删除账户
    /// </summary>
    /// <param name="authTokenModel"></param>
    /// <returns></returns>
    public static bool DeleteAccount(AuthTokenModel authTokenModel)
    {
        try
        {
            using var chatContext = new ChatContext();
            if (string.IsNullOrEmpty(authTokenModel.UserGuid) || string.IsNullOrEmpty(authTokenModel.CookieString) ||
                !authTokenModel.IsRegistered || authTokenModel.CreateTimestamp == 0)
                return false;
            //删除所有此用户Guid下的AuthToken
            var authTokenEntities = chatContext.AuthTokens.Where(entity => entity.UserGuid == authTokenModel.UserGuid);
            if (authTokenEntities.Any())
            {
                foreach (var authTokenEntity in authTokenEntities)
                {
                    chatContext.AuthTokens.Remove(authTokenEntity);
                }
            }

            //删除所有此用户Guid下的ChatHistory
            var history = chatContext.ChatHistories.Find(authTokenModel.UserGuid);
            if (history != null)
            {
                chatContext.ChatHistories.Remove(history);
            }

            //删除所有此用户Guid下的UserInfo
            var userInfo = chatContext.Users.Find(authTokenModel.UserGuid);
            string? mailbox = null;
            if (userInfo != null)
            {
                mailbox = userInfo.MailBox;
                chatContext.Users.Remove(userInfo);
            }

            chatContext.SaveChanges();
            //短信验证码保留 避免用户在短时间内重注册 导致误删

            //删除API鉴权Token
            using var apiContext = new ApiContext();
            var authTokens = apiContext.ApiAuthTokens.AsNoTracking()
                .Where(entity => entity.UserGuid == authTokenModel.UserGuid);

            if (authTokens.Any())
            {
                foreach (var authToken in authTokens)
                {
                    apiContext.Attach(authToken);
                    apiContext.Entry(authToken).State = EntityState.Deleted;
                }
            }

            //删除Api Admin权限
            if (mailbox != null)
            {
                var admin = apiContext.Admins.Find(mailbox);
                if (admin != null)
                {
                    apiContext.Admins.Remove(admin);
                }
            }

            apiContext.SaveChanges();
            Log.Information("成功注销用户 Mailbox:{mailbox}", mailbox);
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "操作错误Backend.ChatEntities.DatabaseHelper.DeleteAccount");
            return false;
        }
    }

    /// <summary>
    /// 数据库的自我清洁方法定时任务
    /// </summary>
    /// <remarks>
    ///    <p>1.清理距创建时间长于48小时“且”在12小时内没有更新访问记录的已注册用户AuthToken</p>
    ///    <p>2.清理距创建时间长于24小时“或”时间长于12小时没更新访问记录的游客AuthToken与相应的对话记录</p>
    ///    <p>3.清理时间长于5分钟的验证码记录</p>
    ///    <p>4.对于每个历史记录按照SessionGuid与MessageGuid进行去重</p>
    /// </remarks>
    public static void SelfClean()
    {
        try
        {
            //1.已注册用户
            //48小时过期
            const long authTokenExpireTimeFromCreateOfLoginUser = 48 * 60 * 60;
            //12小时过期
            const long authTokenExpireTimeFromLastVisitOfLoginUser = 12 * 60 * 60;
            //2.游客
            //24小时过期
            const long authTokenExpireTimeFromCreateOfNotLoginUser = 24 * 60 * 60;
            //12小时过期
            const long authTokenExpireTimeFromLastVisitOfNotLoginUser = 12 * 60 * 60;
            //3.短信验证码
            //5分钟过期
            const long verifyCodeExpireTime = Mail163.ExpireMinutes * 60;
            //当前时间戳
            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            //清理AuthToken
            using var db = new ChatContext();

            //清理时间长于48小时的用户AuthToken 清理时间长于24小时或时间长于12小时没更新访问记录的游客AuthToken
            //要删除历史对话记录的游客Guid
            List<string> guidToDeletes = db.AuthTokens
                .Where(authTokenEntity =>
                        (now - authTokenEntity.CreateTimestamp >= authTokenExpireTimeFromCreateOfNotLoginUser &&
                         !authTokenEntity.IsRegistered) || //游客且距创建长于24小时
                        (now - authTokenEntity.LastAccessTime >= authTokenExpireTimeFromLastVisitOfNotLoginUser &&
                         !authTokenEntity.IsRegistered) //游客且距上次访问长于12小时
                ).Select(t => t.UserGuid).ToList();
            db.AuthTokens.Where((authTokenEntity) =>
                        (now - authTokenEntity.CreateTimestamp >= authTokenExpireTimeFromCreateOfNotLoginUser &&
                         !authTokenEntity.IsRegistered) || //游客且距创建长于24小时
                        (now - authTokenEntity.LastAccessTime >= authTokenExpireTimeFromLastVisitOfNotLoginUser &&
                         !authTokenEntity.IsRegistered) || //游客且距上次访问长于12小时
                        (now - authTokenEntity.CreateTimestamp >=
                         authTokenExpireTimeFromCreateOfLoginUser && //距创建时间长于48小时
                         (now - authTokenEntity.LastAccessTime >=
                          authTokenExpireTimeFromLastVisitOfLoginUser) && //距上次访问时间长于12小时
                         authTokenEntity.IsRegistered) //已注册用户
                )
                .ExecuteDelete();

            //清理时间长于12小时的游客对话记录
            db.ChatHistories.Where(h => guidToDeletes.Contains(h.UserGuid)).ExecuteDelete();
            //清理时间长于5分钟的验证码记录
            db.VerifyCodes.Where(v => (now - v.CreateTimestamp) >= verifyCodeExpireTime).ExecuteDelete();
            //对于每个历史记录按照SessionGuid进行去重
            db.SaveChanges();

            // 对于每个历史记录按照SessionGuid与MessageGuid进行去重
            // 直接获取所有实体，JSON 字段会自动反序列化
            var entities = db.ChatHistories.ToList();

            foreach (var entity in entities)
            {
                db.Entry(entity).State = EntityState.Modified;
                if (entity.History.History.Count != 0)
                {
                    // 对 ChatSessionModel 的 Id 进行去重
                    entity.History.History = entity.History.History
                        .GroupBy(s => s.SessionGuid)
                        .Select(g => g.First())
                        .ToList();
                    // 对 ChatSessionModel 的每一个message Id 进行去重
                    // foreach (var session in entity.History.History)
                    // {
                    //     session.Content = session.Content.GroupBy(message => message.MessageGuid).Select(g => g.First())
                    //         .OrderBy(message => message.Timestamp).ToList();
                    // }
                }
            }

            db.SaveChanges();
            Log.Information("操作成功Backend.ChatEntities.DatabaseHelper.SelfClean");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "操作错误Backend.ChatEntities.DatabaseHelper.SelfClean");
        }
    }
}