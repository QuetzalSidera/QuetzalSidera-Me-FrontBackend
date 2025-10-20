using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.Header.Profile;

namespace Grpc.Share.Protos.WwwModels.Header;

public class ProfileModel
{
    public string HeaderPictureUrl { get; set; }  = string.Empty;
    public TextModel Name  { get; set; } = new();
    public TextModel? Summary { get; set; }  = new();
    public TextModel? Description { get; set; }  = new();

    public static implicit operator ProfileModel(ProfileDto dto)
    {
        if(dto == null)
            return new ProfileModel();
        var model = new ProfileModel()
        {
            HeaderPictureUrl = dto.HeaderPictureUrl,
            Name = dto.Name,
            Summary = dto.Summary,
            Description = dto.Description
        };
        return model;
    }

    public static implicit operator ProfileDto(ProfileModel model)
    {
        var dto = new ProfileDto()
        {
            HeaderPictureUrl = model.HeaderPictureUrl,
            Name = model.Name,
            Summary = model.Summary ?? new TextModel(),
            Description = model.Description ?? new TextModel()
        };
        return dto;
    }
}