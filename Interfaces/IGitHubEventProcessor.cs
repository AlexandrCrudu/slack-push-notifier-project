using GitHubPushHandler.Models;
using Microsoft.Azure.Functions.Worker.Http;

namespace GitHubPushHandler.Interfaces
{
    public interface IGitHubEventProcessor
    {
        GitHubPayload Process(HttpRequestData req);
    }
}
