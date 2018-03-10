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
    public class UserServiceImpl : UserService.UserServiceBase, IService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceImpl"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public UserServiceImpl(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <inheritdoc />
        public override Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new AddUserResponse() { Status = 0};
            try
            {
                UserManager.Instance.AddUser(request.CurrentUser.Name, request.User.Name, request.User.Email);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }           
            return Task.FromResult(response);
        }

        public override Task<RemoveUserResponse> RemoveUser(RemoveUserRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new RemoveUserResponse() { Status = 0 };
            try
            {
                UserManager.Instance.AddUser(request.CurrentUser.Name, request.User.Name, request.User.Email);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<AddGroupResponse> AddGroup(AddGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new AddGroupResponse() { Status = 0 };
            try
            {
                UserManager.Instance.AddGroup(request.CurrentUser.Name, request.Group.Name);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<RemoveGroupResponse> RemoveGroup(RemoveGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new RemoveGroupResponse() { Status = 0 };
            try
            {
                UserManager.Instance.RemoveGroup(request.CurrentUser.Name, request.Group.Name);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<AddAccountToGroupResponse> AddAccountToGroup(AddAccountToGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new AddAccountToGroupResponse() { Status = 0 };
            try
            {
                UserManager.Instance.AddAccountToGroup(request.CurrentUser.Name, request.Account.Name, request.Group.Name);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<RemoveAccountFromGroupResponse> RemoveAccountFromGroup(RemoveAccountFromGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new RemoveAccountFromGroupResponse() { Status = 0 };
            try
            {
                UserManager.Instance.RemoveAccountFromGroup(request.CurrentUser.Name, request.Account.Name, request.Group.Name);
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new GetUserResponse() { Status = 0 };
            try
            {
                var user = UserManager.Instance.GetUser(request.CurrentUser.Name, request.UserName);
                response.User = new User() { Name = user.Name, Email = user.Email };
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<GetGroupResponse> GetGroup(GetGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new GetGroupResponse() { Status = 0 };
            try
            {
                var group = UserManager.Instance.GetGroup(request.CurrentUser.Name, request.GroupName);
                response.Group = new Group() { Name = group.Name, Owner = group.Owner };
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<GetOwnedGroupsResponse> GetOwnedGroups(GetOwnedGroupsRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new GetOwnedGroupsResponse() { Status = 0 };
            try
            {
                var groups = UserManager.Instance.GetOwnedGroups(request.CurrentUser.Name, request.User.Name);
                foreach (var group in groups)
                {
                    response.Groups.Add(new Group() { Name = group.Name, Owner = group.Owner });
                }
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<GetMembersOfGroupResponse> GetMembersOfGroup(GetMembersOfGroupRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new GetMembersOfGroupResponse() { Status = 0 };
            try
            {
                var accounts = UserManager.Instance.GetMembersOfGroup(request.CurrentUser.Name, request.Group.Name);
                foreach (var account in accounts)
                {
                    response.Accounts.Add(new Account() { Name = account.Name, Type = account.Type});
                }
            }
            catch (Exception e)
            {
                response.Status = 1;
                response.Message = e.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<GetMembershipsOfAccountResponse> GetMembershipsOfAccount(GetMembershipsOfAccountRequest request, ServerCallContext context)
        {
            this.Logger.LogInformation($"Operation:{Utils.GetActualAsyncMethodName()} Payload:{Utils.ProtoToJson(request)}");
            var response = new GetMembershipsOfAccountResponse() { Status = 0 };
            try
            {
                var groups = UserManager.Instance.GetMembershipsOfAccount(request.CurrentUser.Name, request.Account.Name);
                foreach (var group in groups)
                {
                    response.Groups.Add(new Group() { Name = group.Name, Owner = group.Owner });
                }
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
