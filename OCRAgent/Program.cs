using OCRAgent.Agent;
using OCRAgent.Plugins;
using OCRAgent.Storage;

namespace OCRAgent;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("OCRAgent (Sprint 0/1 Skeleton)");
        Console.WriteLine("Type 'exit' to quit.");

        // storage root: configurable
        var storageRoot = Environment.GetEnvironmentVariable("OCR_AGENT_STORAGE")
                          ?? Path.Combine(Environment.CurrentDirectory, "Storage");

        var store = new LocalFileImageStore(storageRoot);
        var plugin = new ImageProcessingPlugin(store);

        var agent = AgentRunner.CreateAgent(plugin.AsAITools());
        var samples = new AgentRunner(agent);

        await samples.RunConversationLoopAsync();
    }
}