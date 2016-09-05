using System.Net;
using System.Threading;
using System.Threading.Tasks;
using INET.会话;
using Test.编解码.DTO;
using System;

namespace Test.会话.服务器
{
    internal class B处理注册S : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, N事务 事务, object 负载, IN上下文 __上下文)
        {
            var __注册报文C = 负载 as M注册请求;
            if (__注册报文C != null)
            {
                Console.WriteLine("服务器: 客户端请求注册 " + __远端.ToString());
                //验证账号
                Console.WriteLine("服务器: 验证账号");

                //验证成功
                Console.WriteLine("服务器: 验证通过");
                this.发送响应(__远端,事务, new M注册响应
                {
                    验证通过 = true,
                    用户名 = __注册报文C.用户名,
                    角色 = 0
                });
                this.关闭请求();

                //心跳
                Console.WriteLine("服务器: 开启心跳任务");
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
