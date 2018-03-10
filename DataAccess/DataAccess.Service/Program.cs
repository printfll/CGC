// <copyright file="Program.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Grpc.Core;
    using Microsoft.CGC.DataAccess.Service;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        private static IConfiguration configuration;

        private static ILogger logger;

        private static Server server;

        public static void Main(string[] args)
        {
            // read configuration in config.json
            string assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(assemblyDir)
                .AddJsonFile("config.json");
            configuration = configurationBuilder.Build();

            InitializeLogging();
            InitializeService();

            Console.WriteLine("Type in 'Stop' to stop service");

            var line = string.Empty;
            while (true)
            {
                line = Console.ReadLine();
                if (line.Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    Shutdown();
                    return;
                }
            }
        }

        private static void InitializeLogging()
        {
            var loggerFactory = new LoggerFactory().AddConsole();
            logger = loggerFactory.CreateLogger(Environment.MachineName);
        }

        private static void InitializeService()
        {
            var host = configuration["service:host"];
            var port = configuration["service:port"];

            var services = new Dictionary<string, ServerServiceDefinition>()
            {
                { typeof(UserServiceImpl).Name, GRPC.UserService.BindService(new UserServiceImpl(logger)) },
                { typeof(AuthorizationServiceImpl).Name, GRPC.AuthorizationService.BindService(new AuthorizationServiceImpl(logger)) },
                { typeof(WorkflowServiceImpl).Name, GRPC.WorkflowService.BindService(new WorkflowServiceImpl(logger)) }
            };

            server = new Server
            {
                Ports = { new ServerPort(host, int.Parse(port), ServerCredentials.Insecure) }
            };

            foreach (var service in services)
            {
                server.Services.Add(service.Value);
            }

            server.Start();

            logger.LogInformation($"Services {string.Join("|", services.Keys)} are started");
        }

        /// <summary>
        /// Shut down services before exit
        /// </summary>
        private static void Shutdown()
        {
            Task.WaitAll(server.ShutdownAsync());
        }
    }
}
