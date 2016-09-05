using System.Net;
using System.Threading;
using System.Threading.Tasks;
using INET.会话;
using INET.模板;
using Test.模板.DTO;

namespace Test.模板.服务器
{
    internal class B处理注册S : N处理报文
    {
        public override void 处理接收(IPEndPoint __远端, IN事务报文 __报文, IN上下文 __上下文)
        {
            var __二进制报文 = __报文 as P二进制报文;
            if (__二进制报文.对象 is M注册请求)
            {
                H日志输出.记录("服务器: 客户端请求注册", __远端.ToString());
                //验证账号
                H日志输出.记录("服务器: 验证账号");

                //验证成功
                H日志输出.记录("服务器: 验证通过");
                this.发送响应(__远端, new P二进制报文(new M注册成功{ 角色 = 0, 注册标识 = 1}));
                this.关闭请求();

                __上下文.注册标识[__远端] = 1;

                //心跳
                H日志输出.记录("服务器: 开启心跳任务");
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    while (true)
                    {
                        this.发送通知(__远端, new P二进制报文(new M心跳()));
                        Thread.Sleep(5000);
                    }
                });


            }
        }
    }
}
