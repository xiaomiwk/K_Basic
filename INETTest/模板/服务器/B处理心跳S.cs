using System.Net;
using INET.会话;
using INET.模板;
using Test.模板.DTO;

namespace Test.模板.服务器
{
    internal class B处理心跳S : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, IN事务报文 __报文, IN上下文 __上下文)
        {
            var __二进制报文 = __报文 as P二进制报文;
            if (__二进制报文.对象 is M心跳)
            {
                H日志输出.记录("服务器: 收到心跳", __远端.ToString());
            }
        }
    }
}
