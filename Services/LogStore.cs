using GitHubPushHandler.Interfaces;
using GitHubPushHandler.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace GitHubPushHandler.Services
{
    public class LogStore : ILogStore
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public LogStore()
        {
            // Initialize Cosmos DB Client
            _cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnectionString"));

            // Get a reference to the database and the container
            var database = _cosmosClient.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName"));
            _container = database.GetContainer(Environment.GetEnvironmentVariable("NotificationLogsContainerName"));
        }

        public async Task LogAsync(GitHubPayload payload)
        {
            // Log the GitHub Payload to CosmosDB
            var partitionKey = new PartitionKey(payload.Pusher?.pusherName);

            await _container.CreateItemAsync(payload, partitionKey);
        }

        public async Task<IEnumerable<GitHubPayload>> GetLogsAsync()
        {
            var query = _container.GetItemLinqQueryable<GitHubPayload>(true);
            var iterator = query.ToFeedIterator();

            List<GitHubPayload> logs = new List<GitHubPayload>();
            while (iterator.HasMoreResults)
            {
                foreach (var log in await iterator.ReadNextAsync())
                {
                    logs.Add(log);
                }
            }

            return logs;
        }

    }
}
