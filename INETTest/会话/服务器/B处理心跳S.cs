using System.Net;
using INET.会话;
using Test.编解码.DTO;
using System;

namespace Test.会话.服务器
{
    internal class B处理心跳S : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, N事务 事务, object 负载, IN上下文 __上下文)
        {
            var __心跳报文 = 负载 as M心跳;
            if (__心跳报文 != null)
            {
                Console.WriteLine("服务器: 收到心跳 " + __远端.ToString());
            }
        }
    }
}
