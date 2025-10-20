using System.Text.Json;

namespace Grpc.Client
{
    public static class ChannelConfig
    {
        #if !DEBUG
        
        public const string Server = "...";
#else
        public const string Server = "...";
#endif
    }
}