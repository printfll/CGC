namespace Microsoft.CGC.WebAPI
{
    using Grpc.Core;
    using System.Collections.Generic;

    public interface IChannelCollection
    {
        IDictionary<string, Channel> Channels { get;  }

        Channel this[string name] { get; }

        void AddChannel(string name, Channel channel);
    }
}
