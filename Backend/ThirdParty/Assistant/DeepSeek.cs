using DeepSeek.Core;
using DeepSeek.Core.Models;
using Grpc.Share.Protos.ChatModels;
using Protobuf.Chat;

namespace Backend.ThirdParty.Assistant;

public class DeepSeekClient
{
    private const string ApiKey = "sk...";

    private readonly List<Message> _systemMessage =
        [Message.NewSystemMessage(AgentPrompt.AiAgent), Message.NewSystemMessage(AgentPrompt.Alice),Message.NewSystemMessage(AgentPrompt.AliceEn)];

    /// <summary>
    /// Deepseek封装方法，新对话需要插在List尾部
    /// </summary>
    /// <param name="messages"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<string> SendMessageAsync(List<ChatMessageModel> messages)
    {
        foreach (var message in messages)
        {
            Console.WriteLine(message.Message);
        }

        var client = new DeepSeek.Core.DeepSeekClient(ApiKey);
        var request = new ChatRequest
        {
            Messages = messages.Select(m => m.ToDeepSeekMessage()).ToList(),
            // 指定模型
            Model = DeepSeekModels.ChatModel
        };
        foreach (var message in _systemMessage)
        {
            request.Messages.Insert(0, message);
        }
        var choices = client.ChatStreamAsync(request, CancellationToken.None);
        if (choices == null)
        {
#if DEBUG
            yield return client.ErrorMsg ?? "发生未知错误:choices == null";
            yield break;
#else
            yield return "服务器繁忙，请稍后再试";
            yield break;
#endif
        }

        await foreach (var response in choices)
        {
            yield return response.Delta?.Content ?? string.Empty;
        }
    }
}

public static class ChatMessageModelExtension
{
    public static Message ToDeepSeekMessage(this ChatMessageModel message)
    {
        switch (message.Talker)
        {
            case Talker.Agent:
                return Message.NewAssistantMessage(message.Message);
            case Talker.User:
                return Message.NewUserMessage(message.Message);
            default:
                throw new ArgumentOutOfRangeException(nameof(message.Talker), message.Talker, null);
        }
    }
}