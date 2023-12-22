using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GitHubPushHandler.Models
{
    public class CommitInfo
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }
    }
}
