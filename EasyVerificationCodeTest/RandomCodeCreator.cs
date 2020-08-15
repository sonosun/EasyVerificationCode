using EasyVerificationCode.Common;
using NUnit.Framework;
using System;

namespace Tests
{
    public class RandomCodeCreatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreatRandomNumAndChar()
        {
            string code1 = RandomCodeCreator.CreatRandomNumAndChar(6);
            string code2 = RandomCodeCreator.CreatRandomNumAndChar(6);

            Console.WriteLine($"code1:{code1}");
            Console.WriteLine($"code2:{code2}");

            Assert.AreNotEqual(code1, code2);
        }

        [Test]
        public void TestCreatRandomNum()
        {
            string code1 = RandomCodeCreator.CreatRandomNum(6);
            string code2 = RandomCodeCreator.CreatRandomNum(6);

            Console.WriteLine($"code1:{code1}");
            Console.WriteLine($"code2:{code2}");

            Assert.AreNotEqual(code1, code2);
        }
    }
}