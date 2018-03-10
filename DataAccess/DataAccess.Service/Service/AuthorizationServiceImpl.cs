// <copyright file="UserService.cs" company="Microsoft">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Service
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Microsoft.CGC.DataAccess.GRPC;
    using Microsoft.CGC.DataAccess.Utility;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class for implementing user related operations
    /// </summary>
    public class AuthorizationServiceImpl : AuthorizationService.AuthorizationServiceBase, IService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceImpl"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public AuthorizationServiceImpl(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        public override Task<CheckPermissionResponse> CheckPermission(CheckPermissionRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new CheckPermissionResponse() { Status = 0 };
            try
            {
                var result = AuthorizationManager.Instance.CheckPermission(
                    request.CurrentUser.Name,
                    request.User.Name,
                    request.ResourceType,
                    new Guid(request.ResourceId),
                    request.Permission);
                response.HasPermission = result;
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }

            return Task.FromResult(response);
        }
    }
}
