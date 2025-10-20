using Google.Protobuf.WellKnownTypes;
using Grpc.Client.Base;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.HtmlHeader;
using Protobuf.Www.HtmlHeader.HtmlHeader;
using static Grpc.Client.Base.ChannelInitializer;

namespace Grpc.Client.Www
{
    public class HtmlHeaderDataService
    {
        private static readonly CacheController<HtmlHeaderModel> HtmlHeaderCache = new();


        static HtmlHeaderDataService()
        {
            HtmlHeaderCache.AddOrUpdate(Config.ConfigHtmlHeaderModel);
        }

        private static readonly HtmlHeader.HtmlHeaderClient Client = new(Channel);

        public async Task<HtmlHeaderModel> GetHtmlHeaderAsync()
        {
            try
            {
                if (HtmlHeaderCache.TryGetCachedValue(out var cache))
                {
                    return cache;
                }

                var result = await Client.GetHtmlHeaderAsync(new Empty());
                HtmlHeaderCache.AddOrUpdate(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new HtmlHeaderModel();
            }
        }

        public async Task<StatusModel> ModifyHtmlHeaderAsync(HtmlHeaderDto htmlHeader)
        {
            try
            {
                return await Client.ModifyHtmlHeaderAsync(htmlHeader);
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