namespace Microsoft.CGC.WebAPI
{
    using Grpc.Core;
    using Microsoft.CGC.Comm;

    public interface IUserServiceClient
    {
        UserService.UserServiceClient Client { get; }
    }

    public class UserServiceClient : IUserServiceClient
    {
        public UserServiceClient(Channel channel)
        {
            this.Client = new UserService.UserServiceClient(channel);
        }

        public UserService.UserServiceClient Client { get; }
    }
}
