using GitHubPushHandler.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace GitHubPushHandler.functions
{
    public class PushNotificationFunction
    {
        private readonly ISlackNotifier _slackNotifier;
        private readonly ILogStore _logStore;
        private readonly IGitHubEventProcessor _gitHubEventProcessor;
        private readonly ILogger _logger;

        public PushNotificationFunction(ISlackNotifier slackNotifier, ILogStore logStore, IGitHubEventProcessor gitHubEventProcessor, ILogger<PushNotificationFunction> logger)
        {
            _slackNotifier = slackNotifier;
            _logStore = logStore;
            _gitHubEventProcessor = gitHubEventProcessor;
            _logger = logger;
        }

        [Function("PushNotificationFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var payload = _gitHubEventProcessor.Process(req);

            if (payload != null)
            {
                await _slackNotifier.NotifyAsync(payload);
                await _logStore.LogAsync(payload);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Processed GitHub push event.");
            return response;
        }
    }
}
