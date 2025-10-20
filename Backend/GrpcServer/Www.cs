using System.Text.Json;
using Backend.ThirdParty.Weather;
using Backend.WwwEntities;
using Grpc.Core;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Protos.WwwModels.Foot;
using Grpc.Share.Protos.WwwModels.Header;
using Grpc.Share.Protos.WwwModels.HtmlHeader;
using Protobuf.Www.Content;
using Protobuf.Www.Foot;
using Protobuf.Www.Header.Nav;
using Protobuf.Www.Header.Profile;
using Protobuf.Www.Header.Title;
using Protobuf.Www.HtmlHeader.HtmlHeader;
using Google.Protobuf.WellKnownTypes;
using Serilog;

namespace Backend.GrpcServer;

public class HtmlHeaderService : HtmlHeader.HtmlHeaderBase
{
    public override Task<HtmlHeaderDto> GetHtmlHeader(Empty request, ServerCallContext context)
    {
        try
        {
            using var db = new WwwContext();
            return Task.FromResult((HtmlHeaderDto)(db.Headers.Find(WwwContext.Id)?.HtmlHeader ??
                                                   new HtmlHeaderModel()));
        }
        catch(Exception ex)
        {
            Log.Error(ex,"操作错误Backend.GrpcServer.HtmlHeaderService.GetHtmlHeader");
            return Task.FromResult((HtmlHeaderDto)new HtmlHeaderModel());
        }
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyHtmlHeader(HtmlHeaderDto request, ServerCallContext context)
    {
        try
        {
            HtmlHeaderModel htmlHeaderModel = request;
            using var db = new WwwContext();
            var header = db.Headers.Find(WwwContext.Id);
            if (header != null)
            {
                header.HtmlHeader = htmlHeaderModel;
                db.Entry(header).Property(h => h.HtmlHeader).IsModified = true;
            }

            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }
        catch (Exception ex)
        {
            
            Log.Error(ex,"操作错误Backend.GrpcServer.HtmlHeaderService.ModifyHtmlHeader");
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
        }
    }
}

public class TitleService :Title.TitleBase
{
    public override Task<TitleDto> GetTitle(Empty request, ServerCallContext context)
    {
        using var db = new WwwContext();
        return Task.FromResult((TitleDto)(db.Headers.Find(WwwContext.Id)?.Title ?? new TitleModel()));
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyTitle(TitleDto request, ServerCallContext context)
    {
        try
        {
            TitleModel titleModel = request;
            using var db = new WwwContext();
            var header = db.Headers.Find(WwwContext.Id);
            if (header != null)
            {
                header.Title = titleModel;
                db.Entry(header).Property(h => h.Title).IsModified = true;
            }

            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }
        catch (Exception ex)
        {
            Log.Error(ex,"操作错误Backend.GrpcServer.TitleService.ModifyTitle");
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
        }
    }
}

public class WeatherService : Protobuf.Www.Header.Weather.Weather.WeatherBase
{
    public override async Task<Protobuf.Www.Header.Weather.WeatherDto> GetWeather(Empty request, ServerCallContext context)
    {
        await using var db = new WwwContext();
        LocationModel? location = db.Headers.Find(WwwContext.Id)?.Location;
        Log.Information("Successfully Look Up Location: {@location}", location);
        HeWeatherClient client = new();
        var weather = await client.GetWeather(location ?? new LocationModel()
        {
            Location = "深圳"
        });
        Log.Information("Successfully Look Up Weather: {@weather}", weather);
        return weather;
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyLocation(
        Protobuf.Www.Header.Weather.LocationDto request, ServerCallContext context)
    {
        try
        {
            using var db = new WwwContext();
            Log.Information("Modify Location request: {@request}", request);
            var header = db.Headers.Find(WwwContext.Id);
            if (header != null)
            {
                header.Location = (LocationModel)request;
                db.Entry(header).Property(h => h.Location).IsModified = true;
            }

            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }
        catch (Exception ex)
        {
            Log.Error(ex,"操作错误Backend.GrpcServer.WeatherService.ModifyLocation");
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
        }
    }
}

public class NavService : Nav.NavBase
{
    public override Task<NavDto> GetNavList(Empty request, ServerCallContext context)
    {
        using var db = new WwwContext();
        return Task.FromResult((NavDto)(db.Headers.Find(WwwContext.Id)?.Nav ?? new NavModel()));
    }

    public override Task<global::Protobuf.Shared.Status.Status> AddNavItem(NavItemDto request, ServerCallContext context)
    {
        try
        {
            using var db = new WwwContext();
            var header = db.Headers.Find(WwwContext.Id);
            if (header == null)
                return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
            NavItemModel navItem = new NavItemModel()
            {
                PictureCss = request.PictureCss,
                Link = request.Link,
                Name = request.Name,
            };
            navItem.Name.Id = Guid.NewGuid().ToString();
            header.Nav.NavItems.Add(navItem);
            header.Nav = new NavModel()
            {
                NavItems = header.Nav.NavItems
            };
            db.Entry(header).Property(h => h.Nav).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }
        catch (Exception ex)
        {
            Log.Error(ex,"操作错误Backend.GrpcServer.NavService.AddNavItem");
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
        }
    }

    public override Task<global::Protobuf.Shared.Status.Status> RemoveNavItem(
        NavItemDto request, ServerCallContext context)
    {
        string guid = request.Name.Id;
        using var db = new WwwContext();
        var header = db.Headers.Find(WwwContext.Id);
        if (header != null)
        {
            header.Nav.NavItems.RemoveAll(navItemModel => navItemModel.Name.Id == guid);
            header.Nav = new NavModel()
            {
                NavItems = header.Nav.NavItems
            };
            db.Entry(header).Property(h => h.Nav).IsModified = true;
        }

        db.SaveChanges();
        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyNavItem(NavItemDto request,
        ServerCallContext context)
    {
        string guid = request.Name.Id;
        using var db = new WwwContext();
        var header = db.Headers.Find(WwwContext.Id);
        if (header != null)
        {
            var target = header.Nav.NavItems.FirstOrDefault(navItemModel => navItemModel.Name.Id == guid);
            if (target != null)
            {
                target.Name = request.Name;
                target.PictureCss = request.PictureCss;
                target.Link = request.Link;
            }

            header.Nav = JsonSerializer.Deserialize<NavModel>(JsonSerializer.Serialize(header.Nav)) ?? new NavModel();
            db.Entry(header).Property(h => h.Nav).IsModified = true;
        }

        db.SaveChanges();
        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
    }
}

public class ProfileService : Profile.ProfileBase
{
    public override Task<ProfileDto> GetProfile(Empty request, ServerCallContext context)
    {
        var db = new WwwContext();
        var header = db.Headers.Find(WwwContext.Id);
        return Task.FromResult((ProfileDto)(header?.Profile ?? new ProfileModel()));
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyProfile(ProfileDto request,
        ServerCallContext context)
    {
        var db = new WwwContext();
        var header = db.Headers.Find(WwwContext.Id);
        if (header != null)
        {
            header.Profile = request;
            db.Entry(header).Property(h => h.Profile).IsModified = true;
        }

        db.SaveChanges();

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
    }
}

public class ContentService : Content.ContentBase
{
    public override Task<Layout> GetLayout(Layout request, ServerCallContext context)
    {
        var db = new WwwContext();
        return Task.FromResult((Layout)(db.Layouts.Find(request.Path)?.Layout ?? new LayoutModel()));
    }

    public override Task<global::Protobuf.Shared.Status.Status> AddSubsection(Subsection request,
        ServerCallContext context)
    {
        var db = new WwwContext();
        var layoutEntity = db.Layouts.Find(request.Path);
        if (layoutEntity != null)
        {
            var layout = layoutEntity.Layout;

            var section = layout.Sections.Where(section => section.SectionTitle.Id == request.SectionTitle.Id)
                .ToList()
                .FirstOrDefault();
            if (section?.Subsections != null && section.Subsections.Count != 0)
            {
                SubsectionModel subsection = request;
                section.Subsections.Add(subsection);
            }

            layout.LayoutIdConfig();
            layoutEntity.Layout = JsonSerializer.Deserialize<LayoutModel>(JsonSerializer.Serialize(layout)) ??
                                  new LayoutModel();
            db.Entry(layoutEntity).Property(h => h.Layout).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }

    public override Task<global::Protobuf.Shared.Status.Status> RemoveSubsection(Subsection request,
        ServerCallContext context)
    {
        var db = new WwwContext();
        var layoutEntity = db.Layouts.Find(request.Path);
        if (layoutEntity != null)
        {
            var layout = layoutEntity.Layout;
            layout.Sections.FirstOrDefault(section => section.SectionTitle.Id == request.SectionTitle.Id)?.Subsections?
                .RemoveAll(subsection => subsection.SubsectionTitle.Id == request.SubsectionTitle.Id);
            layoutEntity.Layout = JsonSerializer.Deserialize<LayoutModel>(JsonSerializer.Serialize(layout)) ??
                                  new LayoutModel();
            layout.LayoutIdConfig();
            db.Entry(layoutEntity).Property(h => h.Layout).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }


    /// <summary>
    /// Add Card使用英文名进行标识
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<global::Protobuf.Shared.Status.Status> AddCard(Card request, ServerCallContext context)
    {
        return DatabaseHelper.AddCard(request)
            ? Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok)
            : Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.BadRequest); ;
    }

    public override Task<global::Protobuf.Shared.Status.Status> RemoveCard(Card request, ServerCallContext context)
    {
        var db = new WwwContext();
        var layoutEntity = db.Layouts.Find(request.Path);
        if (layoutEntity != null)
        {
            var layout = layoutEntity.Layout;
            var section = layout.Sections.FirstOrDefault(section => section.SectionTitle.Id == request.SectionTitle.Id);
            if (section == null)
                return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.BadRequest);
            if (section.Subsections != null && section.Subsections.Count != 0)
            {
                var subsection = section.Subsections.FirstOrDefault(subsection =>
                    subsection.SubsectionTitle.Id == request.SubsectionTitle.Id);
                if (subsection == null)
                    return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.BadRequest);
                subsection.Cards.RemoveAll(card => card.CardTitle.Id == request.CardTitle.Id);
            }

            else if (section.Cards != null && section.Cards.Count != 0)
            {
                section.Cards.RemoveAll(card => card.CardTitle.Id == request.CardTitle.Id);
            }
            else
            {
                return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
            }

            layout.LayoutIdConfig();
            layoutEntity.Layout = JsonSerializer.Deserialize<LayoutModel>(JsonSerializer.Serialize(layout)) ??
                                  new LayoutModel();
            db.Entry(layoutEntity).Property(h => h.Layout).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }
}

public class FooterService : Foot.FootBase
{
    public override Task<FootDto> GetFoot(
        global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
    {
        var db = new WwwContext();
        return Task.FromResult((FootDto)(db.Foot.Find(WwwContext.Id)?.Foot ?? new FootModel()));
    }

    public override Task<global::Protobuf.Shared.Status.Status> AddLink(LinkItemDto request, ServerCallContext context)
    {
        var db = new WwwContext();
        var foot = db.Foot.Find(WwwContext.Id);
        if (foot != null)
        {
            foot.Foot.Section.FirstOrDefault(section => section.SectionTitle.Id == request.SectionTitle.Id)?.ItemList
                .Add(request);
            foot.Foot.FootIdConfig();
            foot.Foot = JsonSerializer.Deserialize<FootModel>(JsonSerializer.Serialize(foot.Foot)) ?? new FootModel();
            db.Entry(foot).Property(h => h.Foot).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }

    public override Task<global::Protobuf.Shared.Status.Status> RemoveLink(
        Protobuf.Www.Foot.LinkItemDto request, ServerCallContext context)
    {
        var db = new WwwContext();
        var foot = db.Foot.Find(WwwContext.Id);
        if (foot != null)
        {
            foot.Foot.Section.FirstOrDefault(section => section.SectionTitle.Id == request.SectionTitle.Id)?.ItemList
                .RemoveAll(link => link.ItemTitle.Id == request.ItemTitle.Id);
            foot.Foot.FootIdConfig();
            foot.Foot = JsonSerializer.Deserialize<FootModel>(JsonSerializer.Serialize(foot.Foot)) ?? new FootModel();
            db.Entry(foot).Property(h => h.Foot).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }

    public override Task<global::Protobuf.Shared.Status.Status> ModifyLink(Protobuf.Www.Foot.LinkItemDto request,
        ServerCallContext context)
    {
        var db = new WwwContext();
        var foot = db.Foot.Find(WwwContext.Id);
        if (foot != null)
        {
            LinkItemModel? link = foot.Foot.Section
                .FirstOrDefault(section => section.SectionTitle.Id == request.SectionTitle.Id)?.ItemList
                .FirstOrDefault(link => link.ItemTitle.Id == request.ItemTitle.Id);
            if (link == null)
                return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.BadRequest);
            link.SectionTitle = request.SectionTitle;
            link.ItemTitle = request.ItemTitle;
            link.PictureCss = request.PictureCss;
            link.Link = request.Link;
            foot.Foot.FootIdConfig();
            foot.Foot = JsonSerializer.Deserialize<FootModel>(JsonSerializer.Serialize(foot.Foot)) ?? new FootModel();
            db.Entry(foot).Property(h => h.Foot).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }

    public override Task<Protobuf.Shared.Status.Status> ModifyFootComment(Protobuf.Www.Foot.FootCommentDto request,
        ServerCallContext context)
    {
        var db = new WwwContext();
        var foot = db.Foot.Find(WwwContext.Id);
        if (foot != null)
        {
            foot.Foot.FootComment = request;
            foot.Foot = JsonSerializer.Deserialize<FootModel>(JsonSerializer.Serialize(foot.Foot)) ?? new FootModel();
            db.Entry(foot).Property(h => h.Foot).IsModified = true;
            db.SaveChanges();
            return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.Ok);
        }

        return Task.FromResult((global::Protobuf.Shared.Status.Status)StatusEnum.ServerError);
    }
}