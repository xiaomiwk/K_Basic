using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using INET.会话;
using INET.传输;
using INET.模板;
using Test.会话.客户端;
using Test.会话.服务器;
using Test.编解码.DTO;
using System.Diagnostics;

namespace Test.会话
{
    static class X应用示例
    {
        public static Dictionary<Int16, Type> _报文字典 = new Dictionary<Int16, Type>
        {
            {0,typeof(M心跳)},
            {1,typeof(M注册请求)},
            {2,typeof(M注册响应)},
        };

        public static void TCP_消息头()
        {
            var __编解码器 = new N通用编解码(_报文字典, null);
            Console.WriteLine("创建服务器");
            var __服务器 = FN网络传输工厂.创建TCP服务器(new IPEndPoint(IPAddress.Any, 9000), __编解码器.消息头长度, __编解码器.解码消息长度);
            __服务器.名称 = "服务器";
            var __服务器上下文 = new N上下文(__编解码器, __服务器.名称);
            __服务器.收到消息 += __服务器上下文.收到报文;
            __服务器.客户端已连接 += q => Console.WriteLine("服务器: 与客户端连接 " + q.ToString());
            __服务器.客户端已断开 += q =>
            {
                __服务器上下文.注销节点(q);
                Console.WriteLine("服务器: 与客户端断开 " + q.ToString());
            };

            Console.WriteLine("配置服务器上下文");
            __服务器上下文.设置发送方法(__服务器.同步发送);
            __服务器上下文.订阅报文(typeof(M注册请求).FullName, () => new B处理注册S());
            __服务器上下文.订阅报文(typeof(M心跳).FullName, () => new B处理心跳S());

            __服务器.开启();

            Console.WriteLine("创建客户端");
            var __服务器节点 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            var __客户端 = FN网络传输工厂.创建TCP客户端(__服务器节点, new IPEndPoint(IPAddress.Any, 5555), __编解码器.消息头长度, __编解码器.解码消息长度);
            __客户端.名称 = "客户端";
            //__客户端.自动重连 = true;
            var __客户端上下文 = new N上下文(__编解码器, __客户端.名称);
            __客户端.收到消息 += __客户端上下文.收到报文;
            __客户端.已断开 += () =>
            {
                __客户端上下文.注销节点(__服务器节点);
                Console.WriteLine("客户端: 与服务器断开");
            };

            Console.WriteLine("配置客户端上下文");
            __客户端上下文.设置发送方法((__节点, __消息) => __客户端.同步发送(__消息));
            __客户端上下文.订阅报文(typeof(M心跳).FullName, () => new B处理心跳C());

            __客户端.开启();

            Console.WriteLine("开始交互");
            ////原始方式
            //var __客户端注册 = new B处理注册C();
            //__客户端注册.开启请求(__服务器节点, new M注册请求 { 用户名 = "account", 密码 = "password" }, __客户端上下文);

            //简易方式
            var __主动会话 = new N主动会话(__客户端上下文, __服务器节点);
            var __完成 = __主动会话.请求(new M注册请求 { 用户名 = "account", 密码 = "password" }, __会话参数 =>
            {
                var __注册报文S = (M注册响应)__会话参数.负载;
                if (__注册报文S != null)
                {
                    //验证账号
                    if (!__注册报文S.验证通过)
                    {
                        Console.WriteLine("客户端: 注册失败");
                    }
                    else
                    {
                        //验证成功
                        Console.WriteLine("客户端: 注册成功");

                        //心跳
                        Console.WriteLine("客户端: 开启心跳任务");
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(2000);
                            while (true)
                            {
                                __主动会话.通知(new M心跳());
                                Thread.Sleep(5000);
                            }
                        });
                    }
                    return true;
                }
                return false;
            });
            if (!__完成)
            {
                Console.WriteLine("客户端: 服务器未响应");
            }

            Thread.Sleep(3000);
            __客户端.断开();
            //Thread.Sleep(1000); //等待对方超时释放
            //__客户端.手动重连(10, 1000);

            //关闭
            Thread.Sleep(10000);
            Console.WriteLine("关闭");
            __客户端.关闭();
            __服务器.关闭();
            Console.WriteLine("按回车键结束");
            Console.ReadLine();
        }

    }


}
