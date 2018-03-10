// <copyright file="WorkflowService.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Service
{
    using Microsoft.CGC.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.CGC.DataAccess.GRPC;

    /// <summary>
    /// Class for implementing job/experiment related operation
    /// </summary>
    public class WorkflowServiceImpl : WorkflowService.WorkflowServiceBase, IService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowService"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="contextBuilder">
        /// The context builder.
        /// </param>
        public WorkflowServiceImpl(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets the db context builder.
        /// </summary>
        public DbContextOptionsBuilder<EntityContext> DbContextBuilder { get;  }

        /*/// <inheritdoc />
        public override async Task<AddOrUpdateEntityResponse> AddModule(Module request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Adding module {request.Name}");

            var response = new AddOrUpdateEntityResponse();
            return Task.FromResult(response);
        }

        /// <inheritdoc />
        public override async Task<IMessage<GetModuleResponse>> GetModule(IMessage<GetEntityRequest> request, ServerCallContext context)
        {
            var input = request.Payload.Deserialize();

            this.Logger.LogInformation($"Getting module(s): {string.Join(",", input.EntityIds)}");

            var response = new GetModuleResponse();
            return Message.From(response);
        }

        /// <inheritdoc />
        public override async Task<IMessage<AddOrUpdateEntityResponse>> AddJob(IMessage<Job> request, ServerCallContext context)
        {
            var input = request.Payload.Deserialize();

            this.Logger.LogInformation($"Adding experiment {input.Name}");

            var response = new AddOrUpdateEntityResponse();
            return Message.From(response);
        }

        /// <inheritdoc />
        public override async Task<IMessage<GetJobResponse>> GetJob(IMessage<GetEntityRequest> request, ServerCallContext context)
        {
            var input = request.Payload.Deserialize();

            this.Logger.LogInformation($"Getting experiment(s): {string.Join(",", input.EntityIds)}");

            var response = new GetJobResponse();
            return Message.From(response);
        }*/
    }
}
