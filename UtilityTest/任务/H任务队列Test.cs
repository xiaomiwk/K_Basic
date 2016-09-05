using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.任务;
using Utility.通用;
using System.Threading;
using System.Diagnostics;

namespace UtilityTestProject1.任务
{
    [TestClass]
    public class H任务队列Test
    {

        [TestInitialize]
        public void Initialize()
        {
            ////记录日志，查看日志格式
            //H调试.初始化();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void 任务队列250_100_10()
        {
            var __队列数 = 250;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            任务队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 线程队列250_100_10()
        {
            var __队列数 = 250;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            线程队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 任务队列500_100_10()
        {
            var __队列数 = 500;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            任务队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 线程队列500_100_10()
        {
            var __队列数 = 500;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            线程队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 任务队列1000_100_10()
        {
            var __队列数 = 1000;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            任务队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 线程队列1000_100_10()
        {
            var __队列数 = 1000;
            var __每队列任务数 = 100;
            var __每任务耗时 = 10;
            线程队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 任务队列1000_500_10()
        {
            var __队列数 = 1000;
            var __每队列任务数 = 500;
            var __每任务耗时 = 10;
            任务队列(__队列数, __每队列任务数, __每任务耗时);
        }

        [TestMethod]
        public void 线程队列1000_500_10()
        {
            var __队列数 = 1000;
            var __每队列任务数 = 500;
            var __每任务耗时 = 10;
            线程队列(__队列数, __每队列任务数, __每任务耗时);
        }

        void 任务队列(int __队列数, int __每队列任务数, int __每任务耗时)
        {
            var __结果 = 0;
            var __队列 = new H任务队列();
            for (int i = 0; i < __队列数; i++)
            {
                for (int j = 0; j < __每队列任务数; j++)
                {
                    __队列.添加事项(i.ToString(), j, q =>
                    {
                        Interlocked.Increment(ref __结果);
                        Thread.Sleep(__每任务耗时);
                    }, true);
                }
            }
            for (int i = 0; i < __队列数; i++)
            {
                __队列.关闭队列(i.ToString());
            }
            Assert.AreEqual(__队列数 * __每队列任务数, __结果);
        }

        void 线程队列(int __队列数, int __每队列任务数, int __每任务耗时)
        {
            var __结果 = 0;
            var __队列 = new H线程队列();
            for (int i = 0; i < __队列数; i++)
            {
                for (int j = 0; j < __每队列任务数; j++)
                {
                    __队列.添加事项(i.ToString(), "", j, q =>
                    {
                        Interlocked.Increment(ref __结果);
                        Thread.Sleep(__每任务耗时);
                    }, true);
                }
            }
            for (int i = 0; i < __队列数; i++)
            {
                __队列.关闭队列(i.ToString());
            }
            Assert.AreEqual(__队列数 * __每队列任务数, __结果);
        }
    }
}
