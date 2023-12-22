using GitHubPushHandler.Interfaces;
using GitHubPushHandler.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GitHubPushHandler.Services
{
    public class SlackNotifier : ISlackNotifier
    {
        private const string SlackApiUrl = "https://slack.com/api/chat.postMessage";
        private readonly HttpClient _httpClient;

        public SlackNotifier(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task NotifyAsync(GitHubPayload payload)
        {
            var slackMessage = new
            {
                channel = "#slack-notification-assignment",
                text = $"{payload.Pusher?.pusherName} pushed to repository: {payload.Repository.Name}\n" +
                       $"Branch: {payload.BranchName}\n" +
                       $"Commit Message: {payload.HeadCommit?.Message}\n" +
                       $"Datetime of Push: {payload.HeadCommit?.Timestamp}\n"
            };

            var serializedMessage = JsonSerializer.Serialize(slackMessage);
            var stringContent = new StringContent(serializedMessage, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("UserSlackApiToken"));
            await _httpClient.PostAsync(SlackApiUrl, stringContent);
        }
    }

}
