using Google.Protobuf.WellKnownTypes;
using Grpc.Client.Base;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.Header;
using Protobuf.Www.Header.Nav;
using Protobuf.Www.Header.Profile;
using Protobuf.Www.Header.Title;
using Protobuf.Www.Header.Weather;
using static Grpc.Client.Base.ChannelInitializer;

namespace Grpc.Client.Www
{
    public class HeaderTitleDataService
    {
        private static readonly CacheController<TitleModel> TitleCache = new();

        static HeaderTitleDataService()
        {
            TitleCache.AddOrUpdate(Config.ConfigTitleModel);
        }

        private static readonly Title.TitleClient Client = new(Channel);

        public async Task<TitleModel> GetTitleAsync()
        {
            try
            {
                if (TitleCache.TryGetCachedValue(out var cache))
                {
                    return cache;
                }

                var result = await Client.GetTitleAsync(new Empty());
                TitleCache.AddOrUpdate(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new TitleModel();
            }
        }

        public async Task<StatusModel> ModifyTitleAsync(TitleModel title)
        {
            try
            {
                return await Client.ModifyTitleAsync(title);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }
    }

    public class HeaderProfileDataService
    {
        private static readonly CacheController<ProfileModel> ProfileCache = new();


        static HeaderProfileDataService()
        {
            ProfileCache.AddOrUpdate(Config.ConfigProfileModel);
        }

        private static readonly Profile.ProfileClient Client = new(Channel);

        public async Task<ProfileModel> GetProfileAsync()
        {
            try
            {
                if (ProfileCache.TryGetCachedValue(out var cache))
                {
                    return cache;
                }

                var result = await Client.GetProfileAsync(new Empty());
                ProfileCache.AddOrUpdate(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new ProfileModel();
            }
        }

        public async Task<StatusModel> ModifyProfileAsync(ProfileModel profile)
        {
            try
            {
                return await Client.ModifyProfileAsync(profile);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }
    }


    public class HeaderWeatherDataService
    {
        private static readonly CacheController<WeatherModel> WeatherCache = new();


        static HeaderWeatherDataService()
        {
        }

        private static readonly Weather.WeatherClient Client = new(Channel);

        public async Task<WeatherModel> GetWeatherAsync()
        {
            try
            {
                if (WeatherCache.TryGetCachedValue(out var weatherDto))
                {
                    return weatherDto;
                }

                var result = await Client.GetWeatherAsync(new Empty());
                WeatherCache.AddOrUpdate(result,0.3);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new WeatherModel();
            }
        }

        public async Task<StatusModel> ModifyLocationAsync(LocationModel location)
        {
            try
            {
                return await Client.ModifyLocationAsync(location);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }
    }

    public class HeaderNavDataService
    {
        private static readonly CacheController<NavModel> NavCache = new();


        static HeaderNavDataService()
        {
            NavCache.AddOrUpdate(Config.ConfigNavModel);
        }

        private static readonly Nav.NavClient Client = new(Channel);

        public async Task<NavModel> GetNavListAsync()
        {
            try
            {
                if (NavCache.TryGetCachedValue(out NavModel? cachedNavList))
                {
                    return cachedNavList;
                }
                
                var result = await Client.GetNavListAsync(new Empty());
                NavCache.AddOrUpdate(result);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new NavModel();
            }
        }

        public async Task<StatusModel> AddNavItem(NavItemModel navItem)
        {
            try
            {
                var result = await Client.AddNavItemAsync(navItem);
                return result;
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> RemoveNavItem(NavItemModel navItem)
        {
            try
            {
                var result = await Client.RemoveNavItemAsync(navItem);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> ModifyNavItem(NavItemModel navItem)
        {
            try
            {
                var result = await Client.ModifyNavItemAsync(navItem);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }
    }
}