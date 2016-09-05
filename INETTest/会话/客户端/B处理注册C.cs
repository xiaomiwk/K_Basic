using System.Net;
using System.Threading;
using System.Threading.Tasks;
using INET.会话;
using Test.编解码.DTO;
using System;

namespace Test.会话.客户端
{
    internal class B处理注册C : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, N事务 事务, object 负载, IN上下文 __报文分派)
        {
            var __注册报文S = 负载 as M注册响应;
            if (__注册报文S != null)
            {
                //验证账号
                if (!__注册报文S.验证通过)
                {
                    Console.WriteLine("客户端: 注册失败");
                    return;
                }

                //验证成功
                Console.WriteLine("客户端: 注册成功");
                this.关闭请求();

                //心跳
                Console.WriteLine("客户端: 开启心跳任务");
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    while (true)
                    {
                        this.发送通知(__远端, new M心跳());
                        Thread.Sleep(5000);
                    }
                });

            }
        }
    }
}
