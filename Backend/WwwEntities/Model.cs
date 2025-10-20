using System.Text.Json;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Protos.WwwModels.Foot;
using Grpc.Share.Protos.WwwModels.Header;
using Grpc.Share.Protos.WwwModels.HtmlHeader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Protobuf.Shared.Status;
using Protobuf.Www.Content;

namespace Backend.WwwEntities;

public class WwwContext : DbContext
{
    public const long Id = 1;
    public DbSet<LayoutEntity> Layouts { get; set; }
    public DbSet<HeaderEntity> Headers { get; set; }
    public DbSet<FooterEntity> Foot { get; set; }

    //TODO
    public string DbPath { get; }

    public WwwContext()
    {
#if DEBUG
        string path = Environment.CurrentDirectory;
        DbPath = Path.Combine(path, "Data/www.db");
#else
 string volumePath = Environment.GetEnvironmentVariable("WWW_VOLUME_PATH") ?? "/app/Data/Www";
// 确保目录存在
        if (!Directory.Exists(volumePath))
        {
            Directory.CreateDirectory(volumePath);
        }

        DbPath = System.IO.Path.Combine(volumePath, "www.db");
#endif
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        base.OnConfiguring(optionsBuilder);

        // 忽略 EFCore 的“模型未同步”误报
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LayoutEntity>(entity =>
        {
            entity.HasKey(e => e.Path);
            entity.Property(e => e.Layout)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<LayoutModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new LayoutModel()
                );
        });

        modelBuilder.Entity<HeaderEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HtmlHeader)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<HtmlHeaderModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new HtmlHeaderModel()
                );
            entity.Property(e => e.Title)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<TitleModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new TitleModel()
                );
            entity.Property(e => e.Location)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<LocationModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new LocationModel()
                );
            entity.Property(e => e.Nav)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<NavModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new NavModel()
                );
            entity.Property(e => e.Profile)
                .HasColumnType("TEXT")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    v => JsonSerializer.Deserialize<ProfileModel>(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }) ?? new ProfileModel()
                );
        });
        modelBuilder.Entity<FooterEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Foot)
                    .HasColumnType("TEXT")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }),
                        v => JsonSerializer.Deserialize<FootModel>(v, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }) ?? new FootModel()
                    );
            }
        );
    }
}

public class LayoutEntity
{
    public string Path { get; set; } = string.Empty;
    public LayoutModel Layout { get; set; } = new();
}

public class HeaderEntity
{
    public long Id { get; set; } = 1;
    public HtmlHeaderModel HtmlHeader { get; set; } = new();
    public TitleModel Title { get; set; } = new();
    public LocationModel Location { get; set; } = new();
    public NavModel Nav { get; set; } = new();
    public ProfileModel Profile { get; set; } = new();
}

public class FooterEntity
{
    public long Id { get; set; } = 1;
    public FootModel Foot { get; set; } = new();
}

public static class DatabaseHelper
{
    public static bool AddCard(Card request)
    {
        var db = new WwwContext();
        var layoutEntity = db.Layouts.Find(request.Path);
        if (layoutEntity != null)
        {
            var layout = layoutEntity.Layout;
            var section = layout.Sections.FirstOrDefault(section =>
                section.SectionTitle.TextEnUs == request.SectionTitle.TextEnUs);
            if (section == null)
                return false;
            if (section.Subsections != null && section.Subsections.Count != 0)
            {
                var subsection = section.Subsections.FirstOrDefault(subsection =>
                    subsection.SubsectionTitle.TextEnUs == request.SubsectionTitle.TextEnUs);
                if (subsection == null)
                    return false;
                subsection.Cards.Add(request);
            }

            else if (section.Cards != null && section.Cards.Count != 0)
            {
                section.Cards.Add(request);
            }
            else
            {
                return false;
            }

            layout.LayoutIdConfig();
            layoutEntity.Layout = JsonSerializer.Deserialize<LayoutModel>(JsonSerializer.Serialize(layout)) ??
                                  new LayoutModel();
            db.Entry(layoutEntity).Property(h => h.Layout).IsModified = true;
            db.SaveChanges();
            return true;
        }

        return false;
    }
}