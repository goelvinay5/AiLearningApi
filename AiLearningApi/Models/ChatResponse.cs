namespace AiLearningApi.Models
{
    public class ChatResponse
    {
        public string Response { get; set; } = string.Empty;
        public string InputTokenCount { get; set; } = string.Empty;
        public string OutputTokenCount { get; set; } = string.Empty;
    }
}
