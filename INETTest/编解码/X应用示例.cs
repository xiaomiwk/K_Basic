using System;
using System.Collections.Generic;
using System.Text;
using Test.编解码.DTO;

namespace Test.编解码
{
    static class X应用示例
    {
        public static void 二进制()
        {
            Console.WriteLine("编码");
            var __编码 = new M注册请求 { 用户名 = "account", 密码 = "password" }.编码();
            Console.WriteLine(BitConverter.ToString(__编码));

            Console.WriteLine("解码");
            var __报文 = new M注册请求();
            __报文.解码(__编码);
            Console.WriteLine(__报文.ToString());
            Console.ReadLine();
        }

        public static void 结束符()
        {
            Console.WriteLine("编码");
            var __编码 = 编码键值对消息(new Dictionary<string, string> { { "功能", "注册" }, { "账号", "w" }, { "密码", "k" } });
            Console.WriteLine(BitConverter.ToString(__编码));

            Console.WriteLine("解码");
            Console.WriteLine(字典描述(解析键值对消息(__编码)));

            Console.ReadLine();
        }

        private static byte[] 编码键值对消息(Dictionary<string, string> __键值对)
        {
            var __字符串 = new StringBuilder();
            foreach (var __kv in __键值对)
            {
                __字符串.AppendFormat("{0}:{1}{2}", __kv.Key, __kv.Value, Environment.NewLine);
            }
            return Encoding.UTF8.GetBytes(__字符串 + "\0");
        }

        private static Dictionary<string, string> 解析键值对消息(byte[] __消息)
        {
            var __报文 = Encoding.UTF8.GetString(__消息);
            __报文 = __报文.Remove(__报文.Length - 1, 1);
            var __数组 = __报文.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var __字典 = new Dictionary<string, string>();
            for (int i = 0; i < __数组.Length; i++)
            {
                var __键值 = __数组[i].Split(':');
                var __键 = __键值[0];
                if (__键值.Length > 0)
                {
                    __字典[__键] = __键值[1];
                }
                else
                {
                    __字典[__键] = "";
                }
            }
            if (!__字典.ContainsKey("功能"))
            {
                Console.WriteLine("无效报文");
                return null;
            }
            return __字典;
        }

        private static string 字典描述(Dictionary<string, string> __字典)
        {
            var __描述 = new StringBuilder();
            foreach (var __kv in __字典)
            {
                __描述.AppendFormat("{0}:{1}; ", __kv.Key, __kv.Value);
            }
            return __描述.ToString();
        }
    }

}
