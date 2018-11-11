using System;
using MyLinkedListLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MyLinkedListLibUnitTest
{
    [TestClass]
    public class UnitTestList
    {
        [TestMethod]
        public void TestCount()
        {
            {
                MyLinkedList<int> list = new MyLinkedList<int>();
                Assert.AreEqual(0, list.Count);

                list.Add(1);
                list.Add(2);
                list.Add(3);
                Assert.AreEqual(3, list.Count);

                list.Clear();
                Assert.AreEqual(0, list.Count);
            }
        }

        [TestMethod]
        public void TestInsert()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            int[] testDataOne = new int[] { 1, 2, 3 };
            var outputDataOne = list.ToArray();
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataOne, outputDataOne));

            list.Insert(2, 6);
            list.Insert(1, 5);
            int[] testDataTwo = new int[] { 1, 5, 2, 6, 3 };
            var outputDataTwo = list.ToArray();
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataTwo, outputDataTwo));

            Assert.AreEqual(false, Enumerable.SequenceEqual(testDataTwo, outputDataOne));
        }

        [TestMethod]
        public void TestReusable()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            int[] testDataOne = new int[] { 1, 2, 3 };
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataOne, list.ToArray()));
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataOne, list.ToArray()));
        }

        [TestMethod]
        public void TestRemove()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(5);

            list.RemoveAt(1);
            int[] testDataOne = new int[] { 1, 3, 4, 5, 5 };
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataOne, list.ToArray()));

            list.Remove(5);
            int[] testDataTwo = new int[] { 1, 3, 4, 5 };
            Assert.AreEqual(true, Enumerable.SequenceEqual(testDataTwo, list.ToArray()));
        }

        [TestMethod]
        public void TestSearch()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(5);

            Assert.AreEqual(true, list.Contains(1));
            Assert.AreEqual(true, list.Contains(5));
            Assert.AreEqual(false, list.Contains(555));

            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(3, list.IndexOf(4));
            Assert.AreEqual(-1, list.IndexOf(222));
        }

        [TestMethod]
        public void TestValues()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);

            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);

            list[5] = 88;
            Assert.AreEqual(88, list[5]);
        }
    }
}
