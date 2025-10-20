using System.Net;
using System.Text.Json;
using Grpc.Share.Protos.WwwModels.Header;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Backend.ThirdParty.Weather;

public class HeWeatherClient
{
    private const string ApiKey = "...";
    private const string ApiKeyHeader = "...";
    private const string ApiHost = "...";

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true, // 忽略大小写
    };

    private static readonly HttpClientHandler Handler = new HttpClientHandler()
    {
        AllowAutoRedirect = true,
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
    };

    private static readonly HttpClient HeWeatherHttpClient = new(Handler);


    private async Task<string> ToLocationId(LocationModel model)
    {
        HttpRequestMessage request = new HttpRequestMessage();
        request.Headers.Add(ApiKeyHeader, ApiKey);
        request.RequestUri = new Uri("Https://" + ApiHost + "/geo/v2/city/lookup" + "?location=" + model.Location);
        request.Method = HttpMethod.Get;
        HttpResponseMessage httpResponse = await HeWeatherHttpClient.SendAsync(request);
        GeoApiResponse? response =
            JsonSerializer.Deserialize<GeoApiResponse>(await httpResponse.Content.ReadAsStringAsync(), _options);
        return response?.Location.FirstOrDefault()?.Id ?? string.Empty;
    }

    public async Task<WeatherModel> GetWeather(LocationModel model)
    {
        string locationId = await ToLocationId(model);

        HttpRequestMessage request = new HttpRequestMessage();
        request.Headers.Add(ApiKeyHeader, ApiKey);
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri("Https://" + ApiHost + "/v7/weather/now" + "?location=" + locationId);
        HttpResponseMessage httpResponse = await HeWeatherHttpClient.SendAsync(request);
        WeatherNowResponse? response =
            JsonSerializer.Deserialize<WeatherNowResponse>(await httpResponse.Content.ReadAsStringAsync(), _options);
        var ret = new WeatherModel()
        {
            Location = model,
            WeatherInfo = response?.Now.Text ?? string.Empty,
            WeatherPictureCss = "qi-" + response?.Now.Icon,
            Temp = response?.Now.Temp +"°C",
            Humidity = response?.Now.Humidity +"%",
            WindSpeed = response?.Now.WindSpeed +"m/s",
        };
        return ret;
    }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 'required' 修饰符或声明为可以为 null。

    public class GeoApiResponse
    {
        public string Code { get; set; }
        public LocationDto[] Location { get; set; }
        public Refer Refer { get; set; }
    }

    public class WeatherNowResponse
    {
        public string Code { get; set; }
        public string UpdateTime { get; set; }
        public string FxLink { get; set; }
        public Now Now { get; set; }
        public Refer Refer { get; set; }
    }

    public class LocationDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Adm2 { get; set; }
        public string Adm1 { get; set; }
        public string Country { get; set; }
        public string Tz { get; set; }
        public string UtcOffset { get; set; }
        public string IsDst { get; set; }
        public string Type { get; set; }
        public string Rank { get; set; }
        public string FxLink { get; set; }
    }


    public class Now
    {
        public string ObsTime { get; set; }
        public string Temp { get; set; }
        public string FeelsLike { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Wind360 { get; set; }
        public string WindDir { get; set; }
        public string WindScale { get; set; }
        public string WindSpeed { get; set; }
        public string Humidity { get; set; }
        public string Precip { get; set; }
        public string Pressure { get; set; }
        public string Vis { get; set; }
        public string Cloud { get; set; }
        public string Dew { get; set; }
    }

    public class Refer
    {
        public string[] Sources { get; set; }
        public string[] License { get; set; }
    }
}

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 'required' 修饰符或声明为可以为 null。