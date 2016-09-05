using System;
using Test.模板;

namespace Test
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Console.WriteLine("=================传输 TCP_消息头 (按回车键开始)=================");
            //Console.ReadLine();
            //传输.X应用示例.TCP_消息头();

            //Console.WriteLine("=================传输 TCP_结束符 (按回车键开始)=================");
            //Console.ReadLine();
            //传输.X应用示例.TCP_结束符();

            //Console.WriteLine("=================标准会话 TCP_消息头 (按回车键开始)=================");
            //Console.ReadLine();
            //会话.X应用示例.TCP_消息头();

            Console.WriteLine("=================.NET序列化会话 TCP_消息头 (按回车键开始)=================");
            Console.ReadLine();
            X应用示例.TCP_消息头();


            //Console.WriteLine("=================传输 UDP (按回车键开始)=================");
            //Console.ReadLine();
            //传输.X应用示例.UDP();
        }
    }
}
