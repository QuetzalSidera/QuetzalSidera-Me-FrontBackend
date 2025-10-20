using Google.Protobuf.WellKnownTypes;
using Grpc.Client.Base;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.Foot;
using Protobuf.Shared.Status;
using Protobuf.Shared.Text;
using Protobuf.Www.Foot;
using static Grpc.Client.Base.ChannelInitializer;

namespace Grpc.Client.Www
{
    public class FooterDataService
    {
        private static readonly Foot.FootClient Client = new(Channel);

        private static readonly CacheController<FootDto> FootCache = new();


        static FooterDataService()
        {
            FootCache.AddOrUpdate(Config.ConfigFootModel);
        }

        public async Task<FootModel> GetFootAsync()
        {
            try
            {
                if (FootCache.TryGetCachedValue(out var foot))
                {
                    return foot;
                }

                var result = await Client.GetFootAsync(new Empty());
                FootCache.AddOrUpdate(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new FootModel();
            }
        }

        public async Task<StatusModel> AddLink(LinkItemModel link)
        {
            try
            {
                return await Client.AddLinkAsync(link);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> RemoveLink(LinkItemModel link)
        {
            try
            {
                return await Client.RemoveLinkAsync(link);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> ModifyLink(LinkItemModel link)
        {
            try
            {
                return await Client.ModifyLinkAsync(link);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> ModifyFootCommentAsync(TextModel footComment)
        {
            try
            {
                return await Client.ModifyFootCommentAsync(new FootCommentModel()
                {
                    FootComment = footComment
                });
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