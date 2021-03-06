﻿using NUnit.Framework;
using RUCPs.Tools;

namespace RUCP_Test
{
    public class Tools
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void NumberUtils_CompareShort()
        {
            int result = NumberUtils.UshortCompare(1, 0);
            Assert.IsTrue(result == 1);
            result = NumberUtils.UshortCompare(15_000, 0);
            Assert.IsTrue(result == 1);
            result = NumberUtils.UshortCompare(1, 50_000);
            Assert.IsTrue(result == 1);
            result = NumberUtils.UshortCompare(0, 65_000);
            Assert.IsTrue(result == 1);

        }
    }
}
