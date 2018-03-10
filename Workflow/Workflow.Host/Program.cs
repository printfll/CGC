namespace Microsoft.CGC.Workflow.Host
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Grpc.Core;

    using Microsoft.CGC.Comm;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        private static IConfigurationRoot configurationRoot;

        private static ILogger logger;

        private static Channel channel;

        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder().AddXmlFile("Microsoft.CGC.Workflow.Host.dll.config");
            configurationRoot = configBuilder.Build();

            InitializeLogging();

            var host = configurationRoot["settings:add:host:value"];
            var port = configurationRoot["settings:add:port:value"];

            channel = new Channel(host, int.Parse(port), ChannelCredentials.Insecure);

            Console.WriteLine("Client is started");

            Task.Run(() => CheckPermission("huatai_user1", "Delete", "Experiment", new Guid("7FD93CB3-413C-48F3-A921-C34A38409A78")));
            Task.Run(() => CheckPermission("test_user", "Delete", "Experiment", new Guid("7FD93CB3-413C-48F3-A921-C34A38409A78")));

            Task.Run(() => GetUsers(new List<Guid>() { new Guid("a858207a-2d2f-43ba-b94b-c1f97a56bb39") }));
            Console.Read();

            Shutdown();
        }

        private static void InitializeLogging()
        {
            var loggerFactory = new LoggerFactory().AddConsole();
            logger = loggerFactory.CreateLogger(Environment.MachineName);
        }

        private static async void CheckPermission(string userAlias, string action, string resourceType, Guid resourceId)
        {
            var client = new UserService.UserServiceClient(channel);
            var request = new PermissionCheckRequest()
                              {
                                  UserName = userAlias,
                                  Action = action,
                                  ResourceType = resourceType,
                                  ResourceId = resourceId
                              };

            var response = await client.HasPermissionAsync(request);
            var result = response.Payload.Deserialize();

            logger.LogInformation($"Permission check for user '{userAlias}' request '{action}' on resource '{resourceType}':'{resourceId}' is: '{result.HasPermission}'");
        }

        private static async void GetUsers(List<Guid> userIds)
        {
            var client = new UserService.UserServiceClient(channel);
            var request = new GetUsersRequest()
            {
                UserIds = userIds
            };

            var response = await client.GetUsersAsync(request);
            var result = response.Payload.Deserialize();
        }


        /// <summary>
        /// Shutdown channel before exit
        /// </summary>
        private static void Shutdown()
        {
            logger.LogInformation($"Channel state: {channel.State}");
            Task.WaitAll(channel.ShutdownAsync());
            logger.LogInformation($"Channel state: {channel.State}");
        }
    }
}
