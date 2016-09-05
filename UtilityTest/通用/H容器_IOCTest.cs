using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.通用;
using Microsoft.Practices.Unity;

namespace UtilityTestProject1
{
    [TestClass]
    public class H容器_IOCTest
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void 构造函数参数()
        {
            H容器.注入<IBDemo, BDemo>();
            H容器.注入<IB构造函数参数, B构造函数参数>();
            var __对象 = H容器.取出<IB构造函数参数>();
            Assert.AreEqual(8, __对象.IA.P1);
        }

        [TestMethod]
        public void 方法参数()
        {
            H容器.注入<IBDemo, BDemo>();
            H容器.注入<IB方法参数, B方法参数>();
            var __对象 = H容器.取出<IB方法参数>();
            Assert.AreEqual(8, __对象.IA.P1);
        }

        [TestMethod]
        public void 属性()
        {
            H容器.注入<IBDemo, BDemo>();
            H容器.注入<IB属性, B属性>();
            var __对象 = H容器.取出<IB属性>();
            Assert.AreEqual(8, __对象.IA.P1);
        }

        [TestMethod]
        public void 命名()
        {
            H容器.注入<IBDemo, BDemo>();
            H容器.注入<IBDemo, BDemo>(true, true, "1");
            var __A = H容器.取出<IBDemo>();
            var __B = H容器.取出<IBDemo>("1");
            __A.P1 = 1;
            Assert.AreEqual(1, __A.P1);
            Assert.AreEqual(8, __B.P1);
        }

        [TestMethod]
        public void 生命周期()
        {
            H容器.注入<IBDemo, BDemo1>();
            var __A = H容器.取出<IBDemo>();
            var __B = H容器.取出<IBDemo>();
            __A.P1 = 1;
            //Assert.AreEqual(__A, __B);
            Assert.AreEqual(__A.P1, __B.P1);

            H容器.注入<IBDemo, BDemo2>(false);
            var __C = H容器.取出<IBDemo>();
            var __D = H容器.取出<IBDemo>();
            __C.P1 = 1;
            Assert.AreNotEqual(__C.P1, __D.P1);
        }

        public interface IBDemo
        {
            int P1 { get; set; }
        }

        public interface IB构造函数参数
        {
            IBDemo IA { get; }
        }

        public interface IB属性
        {
            IBDemo IA { get; set; }
        }

        public interface IB方法参数
        {
            IBDemo IA { get; set; }
        }

        public class BDemo : IBDemo
        {
            public int P1 { get; set; }

            public BDemo() { P1 = 8; }
        }

        public class B构造函数参数 : IB构造函数参数
        {
            IBDemo _IA;

            public B构造函数参数(IBDemo __IA)
            {
                _IA = __IA;
            }

            public IBDemo IA { get { return _IA; } }
        }

        public class B属性 : IB属性
        {
            [Dependency]
            public IBDemo IA { get; set; }
        }

        public class B方法参数 : IB方法参数
        {
            public IBDemo IA { get; set; }

            [InjectionMethod]
            public void M(IBDemo __IA) {
                IA = __IA;
            }
        }

        public class BDemo1 : BDemo { }

        public class BDemo2 : BDemo { }
    }
}
