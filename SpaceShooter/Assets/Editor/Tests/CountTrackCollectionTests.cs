using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Editor.Tests
{
    [TestFixture]
    public class CountTrackCollectionTests
    {
        [Test]
        public void Count_WhenCollectionIsEmpty_ReturnsZero()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>();

            int count = collection.Count;

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Count_WhenCollectionIsNotEmpty_ReturnsNumberOfElements()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>(new List<int> { 1, 2, 3 });

            int count = collection.Count;

            Assert.AreEqual(3, count);
        }

        [Test]
        public void Add_WhenElementAdded_CallsOnAddElementEvent()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>();
            int addedElement = 42;
            bool addElementEventCalled = false;
            collection.OnAddElement += element =>
            {
                Assert.AreEqual(addedElement, element);
                addElementEventCalled = true;
            };

            collection.Add(addedElement);

            Assert.IsTrue(addElementEventCalled);
        }

        [Test]
        public void Remove_WhenElementRemoved_CallsOnRemoveElementEvent()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>(new List<int> { 1, 2, 3 });
            int removedElement = 2;
            bool removeElementEventCalled = false;
            collection.OnRemoveElement += element =>
            {
                Assert.AreEqual(removedElement, element);
                removeElementEventCalled = true;
            };

            collection.Remove(removedElement);

            Assert.IsTrue(removeElementEventCalled);
        }

        [Test]
        public void RemoveAt_WhenIndexIsValid_RemovesElementAndCallsOnRemoveElementEvent()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>(new List<int> { 1, 2, 3 });
            int removedIndex = 1;
            int removedElement = collection[removedIndex];
            bool removeElementEventCalled = false;
            collection.OnRemoveElement += element =>
            {
                Assert.AreEqual(removedElement, element);
                removeElementEventCalled = true;
            };

            collection.RemoveAt(removedIndex);

            Assert.AreEqual(2, collection.Count);
            Assert.IsFalse(collection.Collection.Contains(removedElement));
            Assert.IsTrue(removeElementEventCalled);
        }

        [Test]
        public void Clear_RemovesAllElementsAndCallsOnRemoveElementEventForEach()
        {
            CountTrackCollection<int> collection = new CountTrackCollection<int>(new List<int> { 1, 2, 3 });
            int removeElementEventCount = 0;
            collection.OnRemoveElement += element =>
            {
                removeElementEventCount++;
            };

            collection.Clear();

            Assert.IsEmpty(collection.Collection);
            Assert.AreEqual(3, removeElementEventCount);
        }
    }
}
