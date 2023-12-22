using GitHubPushHandler.Models;

namespace GitHubPushHandler.Interfaces
{
    public interface ISlackNotifier
    {
        Task NotifyAsync(GitHubPayload payload);
    }
}
