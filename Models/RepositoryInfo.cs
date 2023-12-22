using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GitHubPushHandler.Models
{
    public class RepositoryInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
