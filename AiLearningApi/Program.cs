using AiLearningApi.Services;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Read Groq configuration
var groqUrl = builder.Configuration["Groq:Endpoint"]
    ?? throw new InvalidOperationException("Groq endpoint is missing.");

var groqModel = builder.Configuration["Groq:ChatModel"]
    ?? throw new InvalidOperationException("Groq model is missing.");

var groqApiKey = builder.Configuration["Groq:ApiKey"]
    ?? throw new InvalidOperationException("Groq API key is missing.");

// Register the AI client
builder.Services.AddSingleton<IChatClient>(_ =>
{
    var openAIClient = new OpenAIClient(
        new ApiKeyCredential(groqApiKey),
        new OpenAIClientOptions
        {
            Endpoint = new Uri(groqUrl)
        });

    return openAIClient
        .GetChatClient(groqModel)
        .AsIChatClient();
});
builder.Services.AddScoped<ChatService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();