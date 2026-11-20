using Microsoft.Extensions.AI;

namespace AiLearningApi.Services;

public class ChatService
{
    private readonly IChatClient _chatClient;

    public ChatService(IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<Models.ChatResponse> GetResponseAsync(string message, CancellationToken cancellationToken)
    {
        var messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, "You are a helpful AI assistant. " +  "Explain technical concepts in simple language."),
            new ChatMessage(ChatRole.User, message)
        };
        var options = new ChatOptions
        {
            Temperature = 0.7f,
            MaxOutputTokens = 500
        };
        var response = await _chatClient.GetResponseAsync(messages, options, cancellationToken);
        return new Models.ChatResponse
        {
            Response = response.Text ?? string.Empty,
            InputTokenCount = response.Usage?.InputTokenCount.ToString() ?? "N/A",
            OutputTokenCount = response.Usage?.OutputTokenCount.ToString() ?? "N/A"
        };
    }
}