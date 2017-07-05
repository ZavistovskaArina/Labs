using System;
using SortedList;
using NUnit.Framework;

namespace TestSortedList
{
    [TestFixture]
    public class TestMySortedList
    {
        [Test]
        public void SortedList_DefaultConstructor()
        {
            var sortedList = new MySortedList<int>();
            Assert.AreEqual(0, sortedList.Count);
        }

        [Test]
        public void SortedList_ParameterConstructor()
        {
            var list = new MySortedList<int>();
            list.Add(5);
            list.Add(2);
            var sortedList = new MySortedList<int>(list);
            Assert.AreEqual(sortedList.Contains(2), true);
        }

        [Test]
        public void SortedList_ParameterConstructor_Exception()
        {
            var list = new MySortedList<string>();
            try
            {
                var sortedlist = new MySortedList<string>(list);
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.Message, Throws.ArgumentException);
            }
        }

        [Test]
        public void SortedList_AddItem_ItemAdded()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(7);
            Assert.AreEqual(true, sortedList.Contains(7));
        }

        [Test]
        public void SortedList_SortItem_SortTrue()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(9);
            sortedList.Add(1);
            var flag = sortedList.GetByIndex(0) < sortedList.GetByIndex(1);
            Assert.AreEqual(true, flag);
        }

        [Test]
        public void SortedList_RemoveItem_ReturnTrueFalse()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(5);
            Assert.AreEqual(true, sortedList.RemoveValue(5));
            Assert.AreEqual(false, sortedList.RemoveValue(5));
        }

        [Test]
        public void SortedList_RemoveItem_ItemRemoved()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(6);
            sortedList.Add(2);
            sortedList.Add(10);
            sortedList.Remove(2);
            Assert.AreEqual(2, sortedList.Count);
        }

        [Test]
        public void SortedList_Enumerator_PrintAllList()
        {
            var sortedList = new MySortedList<int>();
            var enumerator = sortedList.GetEnumerator();
            var i = 0;
            sortedList.Add(0);
            sortedList.Add(2);
            sortedList.Add(4);
            sortedList.Add(6);
            sortedList.Add(8);
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                Assert.AreEqual(item, i);
                i += 2;
            }
        }

        [Test]
        public void SortedList_ClearList_CountIsZero()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(0);
            sortedList.Clear();
            Assert.AreEqual(sortedList.Count, 0);
        }

        [Test]
        public void SortedList_ContainsItem_ReturnTrueFalse()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(3);
            Assert.AreEqual(true, sortedList.Contains(3));
            Assert.AreEqual(false, sortedList.Contains(5));
        }
        
        [Test]
        public void SortedList_IndexOfItem_ItemIndex()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(4);
            sortedList.Add(3);
            Assert.AreEqual(0, sortedList.IndexOf(3));
        }

        [Test]
        public void SortedList_IndexOfItem_Exception()
        {
            var sortedList = new MySortedList<string>();
            try
            {
                var index = sortedList.IndexOf(null);
            }
            catch(InvalidOperationException ex)
            {
                StringAssert.Contains(ex.Message, "Item is empty");
            }
        }

        [Test]
        public void SortedList_GetByIndex_EmptyException()
        {
            var sortedList = new MySortedList<int>();
            try
            {
                var item = sortedList.GetByIndex(0);
            }
            catch (InvalidOperationException ex)
            {
                StringAssert.Contains("SortList is empty", ex.Message);
            }
        }

        [Test]
        public void SortedList_GetByIndex_RangeException()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(2);
            sortedList.Add(6);
            sortedList.Add(1);
            try
            {
                var item = sortedList.GetByIndex(5);
            }
            catch(IndexOutOfRangeException ex)
            {
                StringAssert.Contains("Index out of range", ex.Message);
            }
        }

        [Test]
        public void SortedList_GetByIndex_ItemByIndex()
        {
            var sortedList = new MySortedList<int>();
            sortedList.Add(3);
            sortedList.Add(0);
            Assert.AreEqual(3, sortedList.GetByIndex(1));
        }

        [Test]
        public void SortedList_CopyToArray_ItemAdded()
        {
            var sortedList = new MySortedList<int>();
            var array = new[] { 0, 1, 2, 3, 4, 5 };
            sortedList.Add(1);
            sortedList.CopyTo(array, 5);
            Assert.AreEqual(array[5], 1);
        }

        [Test]
        public void SortedList_CopyToArray_NullException()
        {
            var sortedList = new MySortedList<int>();
            int[] array = new int[3];
            sortedList.Add(1);
            try
            {
                sortedList.CopyTo(array, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.Message, Throws.ArgumentNullException);
            }
        }

        [Test]
        public void SortedList_AddEvent_IsTrue()
        {
            var sortedList = new MySortedList<int>();
            bool flag = false;
            sortedList.AddEvent += (s, e) => { flag = true; };
            sortedList.Add(2);
            Assert.IsTrue(flag);
        }

        [Test]
        public void SortedList_RemoveEvent_IsTrue()
        {
            var sortedList = new MySortedList<int>();
            bool flag = false;
            sortedList.Add(2);
            sortedList.Add(3);
            sortedList.RemoveEvent += (s, e) => { flag = true; };
            sortedList.RemoveValue(2);
            Assert.IsTrue(flag);
        }

        [Test]
        public void SortedList_ClearEvent_IsTrue()
        {
            var sortedList = new MySortedList<int>();
            bool flag = false;
            sortedList.Add(2);
            sortedList.Add(3);
            sortedList.ClearEvent += () => { flag = true; };
            sortedList.Clear();
            Assert.IsTrue(flag);
        }
    }
}
