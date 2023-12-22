using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubPushHandler.Models
{
    public class PusherInfo
    {
        [JsonProperty("name")]
        public string? pusherName { get; set; }
    }
}
