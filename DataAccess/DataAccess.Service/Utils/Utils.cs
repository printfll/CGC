using System.Runtime.CompilerServices;

namespace Microsoft.CGC.DataAccess.Service
{
    public class Utils
    {
        public static string ProtoToJson<T>(T proto) where T : Google.Protobuf.IMessage
        {
            return Google.Protobuf.JsonFormatter.Default.Format(proto);
        }

        public static string GetActualAsyncMethodName([CallerMemberName]string name = null) => name;
    }
}
