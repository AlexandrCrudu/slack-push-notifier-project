## University project 
### Part of a minor bachelor's programme called 'cloud computing'

### Application description
This application links GitHub and Slack to streamline team communication. When code is pushed to a GitHub repository, it triggers serverless functions that fetch details about the push and commit. These details are then sent as a notification to a Slack channel, keeping the team informed about recent code updates in a timely and automated manner.

####  Key Features & Technologies
- Azure functions utilized for creating a serverless architecture
- CosmosDB integration for storing and managing notification logs
- Code is written in C# .NET
- Uses github webhooks to trigger the 'push' event
