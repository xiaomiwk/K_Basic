using System.Net;
using INET.会话;
using System;

namespace Test.会话.客户端
{
    internal class B处理心跳C : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端,  N事务 事务, object 负载, IN上下文 __报文分派)
        {
            Console.WriteLine("客户端: 收到心跳");
        }
    }
}
