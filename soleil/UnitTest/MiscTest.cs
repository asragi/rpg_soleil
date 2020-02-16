using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class MiscTest
    {
        public MiscTest()
        {
            var timer = Soleil.GameDateTime.GetInstance();
            timer.Initialize();
        }

        [TestMethod]
        public void TimerTest()
        {
            var timer = Soleil.GameDateTime.GetInstance();
            int before = timer.MinutesSum;
            timer.Pass(0, 0, 2);
            Assert.AreEqual(timer.MinutesSum, before + 2);
        }
    }
}
