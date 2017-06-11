using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailPointer;

namespace Chapter4Tests
{
    [TestClass]
    public class TailPointerTests
    {

        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void WhenListIsEmptyAndItemToFindIsNull_NodeIsInserted()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);

            int? valueFromList = list.GetListElement(1);

            Assert.AreEqual<int>(1, valueFromList.Value);
            Assert.AreEqual<int>(1, list.Tail.Value.Value);
        }

        [TestMethod]
        public void WhenListIsNotEmptyAndItemToFindIsNull_ItemIsAddedToFrontOfList()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.InsertAfter(null, 2);

            List<int?> itemsInOrder = list.GetElementsOfListInOrder();

            Assert.AreEqual<int>(2, itemsInOrder[0].Value);
            Assert.AreEqual<int>(1, itemsInOrder[1].Value);
        }

        [TestMethod]
        public void WhenListIsNotEmptyAndItemToFindIsNotNull_ItemIsAddedAfterFoundItem()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.InsertAfter(1, 3);
            list.InsertAfter(1, 2);

            List<int?> itemsInOrder = list.GetElementsOfListInOrder();

            Assert.AreEqual<int>(1, itemsInOrder[0].Value);
            Assert.AreEqual<int>(2, itemsInOrder[1].Value);
            Assert.AreEqual<int>(3, itemsInOrder[2].Value);
            Assert.AreEqual<int>(3, list.Tail.Value.Value);
        }

        [TestMethod]
        public void WhenNewItemIsAddedAtEndOfList_TailIsUpdated()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.InsertAfter(1, 2);
            list.InsertAfter(2, 3);

            Assert.AreEqual<int>(3, list.Tail.Value.Value);
        }

        [TestMethod]
        public void WhenInsertAfterFails_FalseIsReturned()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            bool firstAttempt = list.InsertAfter(4, 1);
            list.InsertAfter(1, 3);
            bool secondAttempt = list.InsertAfter(5, 2);

            Assert.IsFalse(firstAttempt);
            Assert.IsFalse(secondAttempt);
        }

        [TestMethod]
        public void WhenListHasOneItem_DeleteItemSucceeds()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.Delete(1);

            List<int?> listItems = list.GetElementsOfListInOrder();

            Assert.AreEqual<int>(0, listItems.Count);
            Assert.IsNull(list.Tail);
        }

        [TestMethod]
        public void WhenListHasMoreThanOneItem_DeleteItemSucceeds()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.InsertAfter(1, 2);
            list.InsertAfter(2, 3);
            list.Delete(2);

            List<int?> listItems = list.GetElementsOfListInOrder();

            Assert.AreEqual<int>(2, listItems.Count);
            Assert.AreEqual<int>(3, list.Tail.Value.Value);
            Assert.AreEqual<int>(1, listItems[0].Value);
            Assert.AreEqual<int>(3, listItems[1].Value);

        }

        [TestMethod]
        public void WhenListHasMoreThanOneItemAndTailItemIsDeleted_TailPointerIsUpdated()
        {
            SinglyLinkedList<int?> list = new SinglyLinkedList<int?>();

            list.InsertAfter(null, 1);
            list.InsertAfter(1, 2);
            list.InsertAfter(2, 3);
            list.Delete(3);

            List<int?> listItems = list.GetElementsOfListInOrder();

            Assert.AreEqual<int>(2, listItems.Count);
            Assert.AreEqual<int>(2, list.Tail.Value.Value);
            Assert.AreEqual<int>(1, listItems[0].Value);
            Assert.AreEqual<int>(2, listItems[1].Value);

        }
    }
}
