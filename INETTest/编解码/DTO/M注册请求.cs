using INET.编解码;

namespace Test.编解码.DTO
{
    internal class M注册请求 : IN可编码, IN可解码
    {
        public string 用户名 { get; set; }

        public string 密码 { get; set; }

        public int 解码(byte[] 数据)
        {
            var __解码 = new H字段解码(数据);
            用户名 = __解码.解码字节数组(20).ToUTF16();
            密码 = __解码.解码字节数组(20).ToASCII();
            return __解码.解码字节数;
        }

        public byte[] 编码()
        {
            var __编码 = new H字段编码();
            __编码.编码字段(用户名.ToUTF16(20), 密码.ToASCII(20));
            return __编码.获取结果();
        }

        public override string ToString()
        {
            return string.Format("注册(用户名:{0})", 用户名);
        }
    }
}
