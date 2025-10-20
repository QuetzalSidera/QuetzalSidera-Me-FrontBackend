using Grpc.Net.Client;
namespace Grpc.Client.Base
{
    public static class ChannelInitializer
    {
        public static readonly GrpcChannel Channel;

        static ChannelInitializer()
        {
            Channel = GrpcChannel.ForAddress(ChannelConfig.Server);
        }
    }
}