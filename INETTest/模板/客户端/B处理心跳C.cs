using System.Net;
using INET.会话;

namespace Test.模板.客户端
{
    internal class B处理心跳C : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, IN事务报文 __报文, IN上下文 __报文分派)
        {
            H日志输出.记录("客户端: 收到心跳");
        }
    }
}
