using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace Utility.扩展
{
    public static class HttpClient
    {
        public static string 发送请求(string __地址, Dictionary<string, string> __参数, string __方法, Encoding __请求编码)
        {
            using (var __请求 = new WebClient() { Encoding = __请求编码 })
            {
                //__请求.Headers.Add("charset", "UTF-8");
                Byte[] __响应 = null;
                if (__方法 == "GET")
                {
                    if (__参数 != null)
                    {
                        var __查询字符串 = new StringBuilder();
                        foreach (var item in __参数)
                        {
                            __查询字符串.AppendFormat("{0}={1}&", item.Key, item.Value);
                        }
                        if (__查询字符串.Length > 0)
                        {
                            __查询字符串.Remove(__查询字符串.Length - 1, 1);
                        }
                        __响应 = __请求.DownloadData(__地址 + "?" + __查询字符串.ToString());
                    }
                    else
                    {
                        __响应 = __请求.DownloadData(__地址);
                    }
                }
                else
                {
                    var __键值对 = new NameValueCollection();
                    if (__参数 != null)
                    {
                        foreach (var item in __参数)
                        {
                            __键值对.Add(item.Key, item.Value);
                        }
                    }
                    __响应 = __请求.UploadValues(__地址, __方法, __键值对);
                }
                return __请求编码.GetString(__响应);
            }
        }

    }
}
