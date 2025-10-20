using Protobuf.Www.Header.Weather;

namespace Grpc.Share.Protos.WwwModels.Header;

public class WeatherModel
{
    public LocationModel Location { get; set; } = new();
    public string WeatherInfo { get; set; } = string.Empty;
    public string WeatherPictureCss { get; set; } = string.Empty;
    public string Temp { get; set; } = string.Empty;
    public string Humidity { get; set; } = string.Empty;
    public string WindSpeed { get; set; } = string.Empty;

    public static implicit operator WeatherModel(WeatherDto dto)
    {
        if(dto == null)
            return new WeatherModel();
        return new WeatherModel()
        {
            Location = dto.Location,
            WeatherInfo = dto.WeatherInfo,
            WeatherPictureCss = dto.WeatherPictureCss,
            Temp = dto.Temp,
            Humidity = dto.Humidity,
            WindSpeed = dto.WindSpeed
        };
    }

    public static implicit operator WeatherDto(WeatherModel model)
    {
        return new WeatherDto()
        {
            Location = model.Location,
            WeatherInfo = model.WeatherInfo,
            WeatherPictureCss = model.WeatherPictureCss,
            Temp = model.Temp,
            Humidity = model.Humidity,
            WindSpeed = model.WindSpeed
        };
    }
}

public class LocationModel
{
    public string Location { get; set; } = string.Empty;

    public static implicit operator LocationModel(LocationDto dto)
    {
        if(dto == null)
            return new LocationModel();
        return new LocationModel()
        {
            Location = dto.Location
        };
    }

    public static implicit operator LocationDto(LocationModel model)
    {
        return new LocationDto()
        {
            Location = model.Location
        };
    }
}