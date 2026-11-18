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
        ChatOptions options = new()
        {
            MaxOutputTokens = 300,
            Temperature = 0.7f
        };
        var response = await _chatClient.GetResponseAsync(message, options, cancellationToken: cancellationToken);
        return new Models.ChatResponse
        {
            Response = response.Text ?? string.Empty,
            InputTokenCount = response.Usage?.InputTokenCount.ToString() ?? "N/A",
            OutputTokenCount = response.Usage?.OutputTokenCount.ToString() ?? "N/A"
        };
    }
}