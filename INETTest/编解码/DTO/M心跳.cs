using INET.编解码;

namespace Test.编解码.DTO
{
    internal class M心跳 : IN可编码, IN可解码
    {
        public int 解码(byte[] 数据)
        {
            return 0;
        }

        public byte[] 编码()
        {
            return new byte[0];
        }        
        
        public override string ToString()
        {
            return "心跳";
        }


    }
}
