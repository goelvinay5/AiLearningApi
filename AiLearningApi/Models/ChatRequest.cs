using System.ComponentModel.DataAnnotations;

namespace AiLearningApi.Models
{
    public class ChatRequest
    {
        [Required]
        [MinLength(1)]
        public string Message { get; set; } = string.Empty;
    }
}
