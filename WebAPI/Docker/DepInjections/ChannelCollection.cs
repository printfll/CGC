using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Microsoft.CGC.WebAPI
{
    public class ChannelCollection : IChannelCollection
    {
        public ChannelCollection()
        {
            this.Channels = new Dictionary<string, Channel>();
        }

        public Channel this[string name] => this.Channels[name];

        public IDictionary<string, Channel> Channels { get; }

        public void AddChannel(string name, Channel channel)
        {
            this.Channels.Add(name, channel);
        }
    }
}
