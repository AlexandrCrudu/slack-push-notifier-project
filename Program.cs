using GitHubPushHandler.Interfaces;
using GitHubPushHandler.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddSingleton<HttpClient>();
        s.AddScoped<ISlackNotifier, SlackNotifier>();
        s.AddScoped<IGitHubEventProcessor, GitHubEventProcessor>();
        s.AddScoped<ILogStore, LogStore>();
    })
    .Build();

host.Run();
