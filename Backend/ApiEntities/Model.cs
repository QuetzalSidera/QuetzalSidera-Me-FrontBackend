using System.Text.Json;
using System.Text.RegularExpressions;
using Grpc.Share.Protos.ApiModels;
using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Tools;
using Microsoft.EntityFrameworkCore;
using Protobuf.Api;
using Protobuf.Shared.Text;
using Serilog;

namespace Backend.ApiEntities;

public class ApiContext : DbContext
{
    public DbSet<ApiAuthTokenEntity> ApiAuthTokens { get; set; }
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<FriendsCardEntity> FriendsCardCheckList { get; set; }

    public string DbPath { get; set; }

    public ApiContext()
    {
#if DEBUG
        string path = Environment.CurrentDirectory;
        DbPath = Path.Combine(path, "Data/api.db");
#else
        string volumePath = Environment.GetEnvironmentVariable("API_VOLUME_PATH") ?? "/app/Data/Api";
// 确保目录存在
        if (!Directory.Exists(volumePath))
        {
            Directory.CreateDirectory(volumePath);
        }

        DbPath = System.IO.Path.Combine(volumePath, "api.db");
#endif
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiAuthTokenEntity>()
            .HasKey(c => c.ApiAuthTokenGuid);
        modelBuilder.Entity<AdminEntity>().HasKey(a => a.MailBox);
        modelBuilder.Entity<FriendsCardEntity>((entity) =>
        {
            entity.HasKey(f => f.Guid);
            entity.Property(f => f.TitleDict)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<Dictionary<LangType, string>>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new Dictionary<LangType, string>()
                );
            entity.Property(f => f.CommentDict)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<Dictionary<LangType, string>>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new Dictionary<LangType, string>()
                );
            //种子数据配置
            modelBuilder.Entity<AdminEntity>().HasData(new AdminEntity
            {
                MailBox = "...",
            });
        });
    }
}

public class AdminEntity
{
    public string MailBox { get; set; } = Guid.NewGuid().ToString();
}

public class ApiAuthTokenEntity
{
    public string ApiAuthTokenGuid { get; set; } = Guid.NewGuid().ToString();
    public string AuthToken { get; set; } = string.Empty;
    public string UserGuid { get; set; } = string.Empty;
    public long CreateTimestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    public bool IsAdmin { get; set; } = false;

    public static explicit operator ApiAuthTokenModel(ApiAuthTokenEntity apiAuthTokenEntity)
    {
        return new ApiAuthTokenModel()
        {
            AuthToken = apiAuthTokenEntity.AuthToken,
            UserGuid = apiAuthTokenEntity.UserGuid,
            CreateTimestamp = apiAuthTokenEntity.CreateTimestamp,
            IsAdmin = apiAuthTokenEntity.IsAdmin
        };
    }

    public static explicit operator ApiAuthTokenEntity(ApiAuthTokenModel apiAuthTokenModel)
    {
        return new ApiAuthTokenEntity()
        {
            ApiAuthTokenGuid = Guid.NewGuid().ToString(),
            AuthToken = apiAuthTokenModel.AuthToken,
            UserGuid = apiAuthTokenModel.UserGuid,
            CreateTimestamp = apiAuthTokenModel.CreateTimestamp,
            IsAdmin = apiAuthTokenModel.IsAdmin
        };
    }
}

public class FriendsCardEntity
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public string CardPictureUrl { get; set; } = string.Empty;
    public string CardLink { get; set; } = string.Empty;
    public Dictionary<LangType, string> TitleDict { get; set; } = new();
    public Dictionary<LangType, string> CommentDict { get; set; } = new();
    public string AddOn { get; set; } = string.Empty;

    public static implicit operator FriendsCardEntity(FriendsCardModel model)
    {
        var ret = new FriendsCardEntity()
        {
            Guid = System.Guid.NewGuid().ToString(),
            CardPictureUrl = model.CardPictureUrl,
            CardLink = model.CardLink,
            TitleDict = model.CardTitle.TextDict,
            CommentDict = model.CardComment.TextDict
        };

        return ret;
    }

    public static implicit operator FriendsCardModel(FriendsCardEntity entity)
    {
        var ret = new FriendsCardModel()
        {
            CardLink = entity.CardLink,
            CardPictureUrl = entity.CardPictureUrl,
            CardComment = entity.CommentDict.ToTextModel(),
            CardTitle = entity.TitleDict.ToTextModel(),
        };
        return ret;
    }

    public static implicit operator FriendCardCheckListItem(FriendsCardEntity entity)
    {
        FriendCardCheckListItem item = new FriendCardCheckListItem()
        {
            Guid = entity.Guid,
            AddOn = entity.AddOn,
            FriendCard = (FriendsCardModel)entity
        };
        return item;
    }
}

public static class DatabaseHelper
{
    /// <summary>
    /// 5小时过期
    /// </summary>
    public const long ApiAuthTokenExpireTime = 5;

    public static ApiAuthTokenModel CreateNewApiAuthToken(string userGuid, string mailBox)
    {
        var apiAuthTokenEntity = new ApiAuthTokenEntity()
        {
            ApiAuthTokenGuid = Guid.NewGuid().ToString(),
            AuthToken = Guid.NewGuid().ToString(),
            UserGuid = userGuid,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        using var db = new ApiContext();
        if (db.Admins.Find(mailBox) != null)
            apiAuthTokenEntity.IsAdmin = true;
        db.ApiAuthTokens.Add(apiAuthTokenEntity);
        db.SaveChanges();
        return (ApiAuthTokenModel)apiAuthTokenEntity;
    }

    public static (bool IsAuth, bool IsAdmin) CheckAuth(ApiAuthTokenModel authTokenModel)
    {
        if (string.IsNullOrEmpty(authTokenModel.AuthToken) ||
            string.IsNullOrEmpty(authTokenModel.UserGuid))
            return (false, false);

        long nowTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        const long maxFreshTime = ApiAuthTokenExpireTime * 60 * 60;
        using var db = new ApiContext();
        var apiAuthTokenList =
            db.ApiAuthTokens.AsNoTracking().Where(a => a.UserGuid == authTokenModel.UserGuid).ToList();
        var validToken = apiAuthTokenList.FirstOrDefault(a =>
            a.AuthToken == authTokenModel.AuthToken &&
            a.UserGuid == authTokenModel.UserGuid &&
            ((nowTimestamp - a.CreateTimestamp) <= maxFreshTime));
        return validToken == null
            ? (false, false)
            : (true, validToken.IsAdmin);
    }

    public static void AddAdmins(List<string> mainBoxes)
    {
        using var db = new ApiContext();
        foreach (var mainBox in mainBoxes)
        {
            if (db.Admins.Find(mainBox) == null)
                db.Admins.Add(new AdminEntity() { MailBox = mainBox });
        }

        db.SaveChanges();
    }

    public static void AddAdmin(string mainBox)
    {
        AddAdmins([mainBox]);
    }

    public static List<string> GetAdmins()
    {
        using var db = new ApiContext();
        return db.Admins.AsNoTracking().Select(a => a.MailBox).ToList();
    }

    public static void RemoveAdmins(List<string> mailBoxes)
    {
        using var db = new ApiContext();
        foreach (var mailBox in mailBoxes)
        {
            if (!IsValidEmail(mailBox))
                continue;
            AdminEntity? admin = db.Admins.Find(mailBox);
            if (admin != null)
                db.Admins.Remove(admin);
        }

        db.SaveChanges();
    }

    public static void RemoveAdmin(string mailBox)
    {
        RemoveAdmins([mailBox]);
    }

    private static bool IsValidEmail(string mailBox)
    {
        if (string.IsNullOrWhiteSpace(mailBox))
            return false;

        // 常用的邮箱验证正则表达式
        const string pattern =
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        return Regex.IsMatch(mailBox, pattern);
    }


    public static string AddFriendCard(FriendsCardEntity entity)
    {
        if (string.IsNullOrEmpty(entity.CardPictureUrl) ||
            entity.CommentDict.Count == 0 || entity.TitleDict.Count == 0 || string.IsNullOrEmpty(entity.CardLink))
            return string.Empty;
        using var db = new ApiContext();
        string ret = Guid.NewGuid().ToString();
        entity.Guid = ret;
        db.FriendsCardCheckList.Add(entity);
        db.SaveChanges();
        return ret;
    }


    public static bool RemoveFriendCard(string guid)
    {
        if (string.IsNullOrEmpty(guid))
            return false;
        using var db = new ApiContext();
        FriendsCardEntity? entity = db.FriendsCardCheckList.Find(guid);
        if (entity == null)
            return true;
        db.FriendsCardCheckList.Remove(entity);
        db.SaveChanges();
        return true;
    }


    public static FriendsCardModel? GetFriendsCard(string guid)
    {
        using var db = new ApiContext();
        FriendsCardEntity? entity = db.FriendsCardCheckList.Find(guid);
        if (entity == null)
            return null;
        return entity;
    }

    public static List<FriendsCardEntity> GetFriendsCards()
    {
        using var db = new ApiContext();
        return db.FriendsCardCheckList.AsNoTracking().ToList();
    }

    public static void SelfClean()
    {
        try
        {
            using var db = new ApiContext();
            long nowTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            long expireTime = ApiAuthTokenExpireTime * 60 * 60;
            db.ApiAuthTokens.Where(a => nowTimestamp - a.CreateTimestamp > expireTime).ExecuteDelete();
            db.SaveChanges();
            Log.Information("操作成功Backend.ApiEntities.DatabaseHelper.SelfClean");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "操作错误Backend.ApiEntities.DatabaseHelper.SelfClean");
        }
    }
}