using GitHubPushHandler.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace GitHubPushHandler.Functions
{
    public class GetPushLogsFunction
    {
        private readonly ILogStore _logStore;
        private readonly ILogger _logger;

        public GetPushLogsFunction(ILogStore logStore, ILogger<GetPushLogsFunction> logger)
        {
            _logStore = logStore;
            _logger = logger;
        }

        [Function("GetPushLogsFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var logs = await _logStore.GetLogsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            response.WriteString(System.Text.Json.JsonSerializer.Serialize(logs));

            return response;
        }
    }
}
