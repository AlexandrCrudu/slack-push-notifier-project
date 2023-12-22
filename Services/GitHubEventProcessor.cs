using GitHubPushHandler.Interfaces;
using GitHubPushHandler.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace GitHubPushHandler.Services
{
    public class GitHubEventProcessor : IGitHubEventProcessor
    {
        public GitHubPayload Process(HttpRequestData req)
        {
            var requestBody = new StreamReader(req.Body).ReadToEndAsync().Result;

            // Deserialize the requestBody into a GitHubPayload object
            var payload = JsonConvert.DeserializeObject<GitHubPayload>(requestBody) ?? throw new Exception("Could not parse request data");

            payload.pusherName = payload.Pusher.pusherName;

            return payload;
        }
    }
}
