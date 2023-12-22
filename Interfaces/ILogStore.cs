using GitHubPushHandler.Models;

namespace GitHubPushHandler.Interfaces
{

    public interface ILogStore
    {
        Task LogAsync(GitHubPayload payload);
        Task<IEnumerable<GitHubPayload>> GetLogsAsync();

    }
}
