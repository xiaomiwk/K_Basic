using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using INET.传输;
using System.Diagnostics;

namespace Test.传输
{
    static class X应用示例
    {
        public static readonly Func<byte[], int> 解码消息长度 = q => IPAddress.NetworkToHostOrder(BitConverter.ToInt32(q, 2));

        public static void TCP_消息头()
        {
            Console.WriteLine("创建服务器");
            var __消息头长度 = 6;
            var __服务器 = FN网络传输工厂.创建TCP服务器(new IPEndPoint(IPAddress.Any, 9000), __消息头长度, 解码消息长度);
            __服务器.名称 = "服务器";
            __服务器.收到消息 += __服务器.同步发送;
            __服务器.客户端已连接 += q => Console.WriteLine("服务器: 客户端已连接 " + q.ToString());
            __服务器.客户端已断开 += q => Console.WriteLine("服务器: 客户端已断开 " + q.ToString());
            __服务器.开启();

            Console.WriteLine("创建客户端");
            var __客户端 = FN网络传输工厂.创建TCP客户端(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000), new IPEndPoint(IPAddress.Any, 0), __消息头长度, 解码消息长度);
            __客户端.名称 = "客户端";
            __客户端.收到消息 += (__远端, __消息) => Console.WriteLine("客户端: 收到报文 " + BitConverter.ToString(__消息));
            __客户端.已断开 += () => Console.WriteLine("客户端: 服务器已断开");
            __客户端.开启();

            __客户端.同步发送(new byte[] { 0xAA, 0xAA, 0x00, 0x00, 0x00, 0x01, 0x8 });
            __客户端.同步发送(new byte[] { 0xAA, 0xAA, 0x00, 0x00, 0x00, 0x02, 0x8, 0x09 });

            Thread.Sleep(2000);
            Console.WriteLine("按回车键结束");
            __客户端.关闭();
            __服务器.关闭();
            Console.ReadLine();
        }

        public static void TCP_结束符()
        {
            Console.WriteLine("创建服务器");
            var __服务器 = FN网络传输工厂.创建TCP服务器(new IPEndPoint(IPAddress.Any, 9000), new List<byte[]> { Encoding.UTF8.GetBytes("\0") });
            __服务器.名称 = "服务器";
            __服务器.收到消息 += __服务器.同步发送;
            __服务器.客户端已连接 += q => Console.WriteLine("服务器: 客户端已连接 " + q.ToString());
            __服务器.客户端已断开 += q => Console.WriteLine("服务器: 客户端已断开 " + q.ToString());
            __服务器.开启();

            Console.WriteLine("创建客户端");
            var __客户端 = FN网络传输工厂.创建TCP客户端(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000), new IPEndPoint(IPAddress.Any, 0), new List<byte[]> { Encoding.UTF8.GetBytes("\0") });
            __客户端.名称 = "客户端";
            __客户端.收到消息 += (__远端, __消息) => Console.WriteLine("客户端: 收到报文 " + BitConverter.ToString(__消息));
            __客户端.已断开 += () => Console.WriteLine("客户端: 服务器已断开");
            __客户端.开启();

            __客户端.同步发送(Encoding.UTF8.GetBytes("hello world!\0"));
            __客户端.同步发送(Encoding.UTF8.GetBytes("hello wk!\0"));

            Thread.Sleep(2000);
            Console.WriteLine("按回车键结束");
            Console.ReadLine();
            __客户端.关闭();
            __服务器.关闭();
        }

        public static void UDP()
        {
            Console.WriteLine("创建服务器");
            var __服务器 = FN网络传输工厂.创建UDP节点(new IPEndPoint(IPAddress.Any, 9000));
            __服务器.名称 = "服务器";
            __服务器.收到消息 += __服务器.同步发送;
            __服务器.开启();

            Console.WriteLine("创建客户端");
            var __客户端 = FN网络传输工厂.创建UDP节点(new IPEndPoint(IPAddress.Any, 0));
            __客户端.名称 = "客户端";
            __客户端.收到消息 += (__远端, __消息) => Console.WriteLine("客户端: 收到报文 " +  BitConverter.ToString(__消息));
            __客户端.开启();

            __客户端.同步发送(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000), Encoding.UTF8.GetBytes("hello world!"));
            __客户端.同步发送(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000), Encoding.UTF8.GetBytes("hello wk!"));

            Thread.Sleep(2000);
            Console.WriteLine("按回车键结束");
            Console.ReadLine();
            __客户端.关闭();
            __服务器.关闭();
        }


    }

}
