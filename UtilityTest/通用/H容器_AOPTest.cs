using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.通用;

namespace UtilityTestProject1
{
    [TestClass]
    public class H容器_AOPTest
    {
        [TestInitialize]
        public void Initialize()
        {
            //记录日志，查看日志格式
            H调试.初始化();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void 接口拦截()
        {
            IA __IA = H容器.接口拦截<IA>(new BA1());
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
        }

        [TestMethod]
        public void 透明拦截()
        {
            BA2 __IA = H容器.透明拦截(new BA2());
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
        }

        [TestMethod]
        public void 透明拦截2()
        {
            IA __IA = H容器.透明拦截<IA>(new BA1());
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
        }

        [TestMethod]
        public void 虚方法拦截()
        {
            BA3 __IA = H容器.虚方法拦截< BA3>();
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
        }

        [TestMethod]
        public void 多接口拦截()
        {
            IA __IA = H容器.接口拦截<IA>(new BA4(), new Type[] { typeof(IC) }, (__methodBase, __target, __args) =>
            {
                if (__methodBase == typeof(IC).GetMethod("M4"))
                {
                }
                return new Tuple<object, object[]>(100, null);
                //return new Tuple<object, object[]>(((IC)__target).M4(), null); //死循环
            });
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
            var __result = ((IC)__IA).M4();
            Console.WriteLine(__result);
        }

        [TestMethod]
        public void 多接口注入()
        {
            H容器.注入<IA, BA4>(true, false);
            IA __IA = H容器.取出<IA>();
            __IA.E1 += Console.WriteLine;
            Console.WriteLine(__IA.P1);
            __IA.P1 = 1;
            Console.WriteLine(__IA.P1);
            __IA.M1();
            __IA.M2(1, "A");
            string __arg1;
            string __arg2 = "2";
            Console.WriteLine(__IA.M3(new DTO { P1 = 2, P2 = "S" }, out __arg1, ref __arg2));
            __IA.OnE1(new DTO { P1 = 3, P2 = "SS" });
            var __result = ((IC)__IA).M4();
            Console.WriteLine(__result);
        }

        public interface IA
        {
            int P1 { get; set; }

            void M1();

            void M2(int a1, string a2);

            int M3(DTO a1, out string a2, ref string a3);

            event Action<DTO> E1;

            void OnE1(DTO obj);
        }

        public interface IC
        {
            int M4();
        }

        private class BA
        {
            public int P1 { get; set; }

            public void M1()
            {

            }

            public void M2(int a1, string a2)
            {
            }

            public int M3(DTO a1, out string a2, ref string a3)
            {
                a2 = "k";
                return new Random().Next(100);
            }

            public event Action<DTO> E1;

            public virtual void OnE1(DTO obj)
            {
                var handler = E1;
                if (handler != null) handler(obj);
            }
        }

        private class BA1 : BA, IA
        {
        }

        private class BA2 : MarshalByRefObject
        {
            public int P1 { get; set; }

            public void M1()
            {

            }

            public void M2(int a1, string a2)
            {
            }

            public int M3(DTO a1, out string a2, ref string a3)
            {
                a2 = "k";
                return new Random().Next(100);
            }

            public event Action<DTO> E1;

            public virtual void OnE1(DTO obj)
            {
                var handler = E1;
                if (handler != null) handler(obj);
            }
        }

        public class BA3
        {
            public int P1 { get; set; }

            public virtual void M1()
            {
                M4();
            }

            public void M2(int a1, string a2)
            {
            }

            public int M3(DTO a1, out string a2, ref string a3)
            {
                a2 = "k";
                return new Random().Next(100);
            }

            protected virtual void M4()
            {

            }


            public event Action<DTO> E1;

            public virtual void OnE1(DTO obj)
            {
                var handler = E1;
                if (handler != null) handler(obj);
            }
        }

        private class BA4 : BA, IA, IC
        {
            public int M4()
            {
                return 100;
            }
        }

        public class DTO
        {
            public int P1 { get; set; }
            public string P2 { get; set; }
        }

    }
}
