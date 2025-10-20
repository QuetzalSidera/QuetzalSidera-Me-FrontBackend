using Grpc.Client.Base;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.WwwModels.Content;
using Protobuf.Shared.Status;
using Protobuf.Www.Content;
using static Grpc.Client.Base.ChannelInitializer;
using Grpc.Share.Protos.SharedModels;

namespace Grpc.Client.Www
{
    public class ContentDataService
    {
        private static readonly Content.ContentClient Client = new(Channel);

        /// <summary>
        /// Section唯一标识
        /// </summary>
        public record SectionId(string Path, string SelfId);

        /// <summary>
        /// Subsection唯一标识
        /// </summary>
        /// <param name="ParentId">父Section的id</param>
        public record SubsectionId(string Path, string ParentId, string SelfId);

        /// <summary>
        /// Card唯一标识
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="GrandparentId">父Section的父Section的id</param>
        /// <param name="ParentId">父Section的id</param>
        /// <param name="SelfId">自己的id</param>
        /// 对于无Subsection的card， string ParentId为空串
        public record CardId(string Path, string GrandparentId, string ParentId, string SelfId);

        /// <summary>
        /// Layout缓存
        /// </summary>
        private static readonly CacheController<string, LayoutModel> LayoutCache = new();

        static ContentDataService()
        {
            LayoutCache.AddOrUpdate(ConfigData.PathConfig.RootPath, Config.ConfigRootLayoutModel);
            LayoutCache.AddOrUpdate(ConfigData.PathConfig.ThankYouPath, Config.ConfigThankYouLayoutModel);
        }

        /// <summary>
        /// 从路径获取本路径下Layout信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>含SectionSummary</returns>
        public async Task<LayoutModel> GetLayoutAsync(string path)
        {
            try
            {
                if (LayoutCache.TryGetCachedValue(path, out var layout))
                {
                    return layout;
                }

                var result = await Client.GetLayoutAsync(new LayoutModel()
                {
                    Path = path,
                });
                LayoutCache.AddOrUpdate(path, result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new LayoutModel();
            }
        }

        public async Task<StatusModel> AddSubsectionAsync(SubsectionModel subsectionModel)
        {
            try
            {
                return await Client.AddSubsectionAsync(subsectionModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> RemoveSubsectionAsync(SubsectionModel subsectionModel)
        {
            try
            {
                var result = await Client.RemoveSubsectionAsync(subsectionModel);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }


        /// <summary>
        /// 父级存在 则可加 id自动生成 / 残缺=>错
        /// </summary>
        private async Task<StatusModel> AddCardAsync(CardModel cardModel)
        {
            try
            {
                return await Client.AddCardAsync(cardModel);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<StatusModel> AddAboutCardAsync(AboutCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddProjectsCardAsync(ProjectsCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddTechStackCardAsync(TechStackCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddHobbiesCardAsync(HobbiesCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddQuotesCardAsync(QuotesCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddFriendsCardAsync(FriendsCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        public async Task<Status> AddThankYouCardAsync(ThankYouCardModel card)
        {
            try
            {
                return await AddCardAsync((CardModel)card);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusEnum.ServerError;
            }
        }

        /// <summary>
        /// 父级存在 本级存在
        /// </summary>
        /// <returns></returns>
        public async Task<Status> RemoveCardAsync(CardModel cardModel)
        {
            try
            {
                return await Client.RemoveCardAsync(cardModel);
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