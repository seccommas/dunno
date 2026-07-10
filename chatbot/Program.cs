using Microsoft.Agents.AI;
using OpenAI;

var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new InvalidOperationException("set openai key");

var agent = new OpenAIAgent(apiKey)
    .GetChatClient("gpt-4o-mini")
    .CreateAIAgent(
        name: "Host",
        instruction: ""
        );

Console.WriteLine("quản trò đã sẵn sàng");

var thread = agent.GetNewThread();

while (true)
{
    Console.Write("You > ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;
    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    Console.WriteLine("Host > ");
    await foreach (var update in agent.RunStreamingAsync(input, thread))
    {
        Console.Write(update.ToString);
    }
    Console.WriteLine("\n");
}
