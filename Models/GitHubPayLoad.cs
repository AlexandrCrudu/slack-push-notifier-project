using Newtonsoft.Json;


namespace GitHubPushHandler.Models
{
    public class GitHubPayload
    {
        public string id { get; init; } = Guid.NewGuid().ToString();

        [JsonProperty("ref")]
        public string? BranchName { get; set; }

        public string pusherName { get; set; }

        [JsonProperty("pusher")]
        public PusherInfo? Pusher { get; set; }

        [JsonProperty("repository")]
        public RepositoryInfo Repository { get; set; }

        [JsonProperty("head_commit")]
        public CommitInfo? HeadCommit { get; set; }
    }
}
