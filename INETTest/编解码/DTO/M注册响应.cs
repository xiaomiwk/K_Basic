using INET.编解码;

namespace Test.编解码.DTO
{
    internal class M注册响应 : IN可编码, IN可解码
    {
        public bool 验证通过 { get; set; }

        public string 用户名 { get; set; }

        /// <summary>
        /// 验证通过时存在
        /// </summary>
        public int 角色 { get; set; }

        /// <summary>
        /// 验证不通过时存在
        /// </summary>
        private short 回复信息长度 { get; set; }

        /// <summary>
        /// 验证不通过时存在
        /// </summary>
        public string 回复信息描述 { get; set; }

        public int 解码(byte[] 数据)
        {
            var __解码 = new H字段解码(数据);
            验证通过 = __解码.解码字节() == 0;
            用户名 = __解码.解码字节数组(20).ToUTF16();
            if (验证通过)
            {
                角色 = __解码.解码Int32();
            }
            else
            {
                回复信息长度 = __解码.解码Int16();
                回复信息描述 = __解码.解码字节数组(回复信息长度).ToUTF16();
            }
            return __解码.解码字节数;
        }

        public byte[] 编码()
        {
            var __编码 = new H字段编码();
            __编码.编码字段((byte)(验证通过 ? 0 : 1), 用户名.ToUTF16(20));
            if (this.验证通过)
            {
                __编码.编码字段(角色);
            }
            else
            {
                回复信息长度 = (short)(回复信息描述.Length * 2);
                __编码.编码字段(回复信息长度, 回复信息描述.ToUTF16(回复信息长度));
            }
            return __编码.获取结果();
        }

        public override string ToString()
        {
            if (验证通过)
            {
                return string.Format("用户名:{0}; 验证通过:{1}; 角色:{2}", 用户名, 验证通过, 角色);
            }
            return string.Format("用户名:{0}; 验证通过:{1}, {2}", 用户名, 验证通过, 回复信息描述);
        }
    }
}
