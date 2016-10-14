using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Utility.存储
{
    public static class HXML
    {
        public static XDocument 加载文件(string __文件路径)
        {
            using (var __文件流 = File.OpenRead(H路径.获取绝对路径(__文件路径)))
            {
                return XDocument.Load(__文件流);
            }
        }
    }
}
