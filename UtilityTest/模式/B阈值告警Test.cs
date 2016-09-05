using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilityTestProject1.模式
{
    [TestClass]
    public class B阈值告警Test
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
        public void 整数()
        {
            var __告警中 = false;
            var __阈值告警 = new Utility.模式.H阈值告警<int>(80, (a,b)=> a - b, (__告警, __缓存) => {
                __告警中 = __告警;
            }, 10, 7);
            __阈值告警.添加(new List<int> { 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, });
            Assert.IsTrue(__告警中);
            __阈值告警.添加(new List<int> { 70, 70, 70, 70, 70, 70, 70 });
            Assert.IsFalse(__告警中);
            __阈值告警.添加(new List<int> { 80, 80, 80, 80, 80, 80, 80 });
            Assert.IsTrue(__告警中);
        }

    }
}
