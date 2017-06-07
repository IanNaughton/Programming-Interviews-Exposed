using Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace TailPointer
{
    public class SinglyLinkedList<T> : SinglyLinkedListBase<T>
    {
        private ListItem<T> _head;
        private ListItem<T> _tail;

        /// <summary>
        /// Just a method to support unit testing around the problem
        /// </summary>
        public ListItem<T> Tail {
            get
            {
                return _tail;
            }
        }

        /// <summary>
        /// Quick and dirty method to support unit testing :o
        /// </summary>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public T GetListElement(T valueToFind)
        {
            T itemToReturn = default(T); 
            IterateOverStack(_head, 
                currentItem => currentItem.Next != null && !currentItem.Value.Equals(valueToFind), 
                foundItem => {  
                    itemToReturn = foundItem.Value;
                });
            return itemToReturn;
        }

        /// <summary>
        /// Returning a list from a list? LOL WUT?
        /// </summary>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public List<T> GetElementsOfListInOrder()
        {
            List<T> itemsInOrder = new List<T>();

            ListItem<T> currentItem = _head;

            while (currentItem != null)
            {
                itemsInOrder.Add(currentItem.Value);
                currentItem = currentItem.Next;
            }
               
            return itemsInOrder;
        }


        public bool Delete(T valueToDelete)
        {
            // We have a few different scenarios to account for here

            // 1: The list is empty
            if (ListIsEmpty())
            {
                throw new InvalidOperationException("You can't delete items from an empty list ya dingus!");
            }

            // 2: The list has only one element
            if (ListHasOneItemAndItIsTheItemToDelete(valueToDelete))
            {
                DeleteFirstListItem(valueToDelete);
                return true;
            }
            // 3: The list has n elements
            else
            {
                return FindAndDeleteItem(valueToDelete);
            }
        }

        public bool ListIsEmpty()
        {
            return _head == null;
        }

        public bool ListHasOneItemAndItIsTheItemToDelete(T valueToDelete)
        {
            return _head.Next == null && _head.Value.Equals(valueToDelete);
        }

        public void DeleteFirstListItem(T valueToDelete)
        {
            _head = null;
            _tail = _head;
        }

        public bool FindAndDeleteItem(T valueToDelete)
        {
            bool itemFound = false;

            IterateOverStack(
                _head,
                currentItem => currentItem.Next != null && !currentItem.Next.Value.Equals(valueToDelete),
                itemBeforeTheItemToDelete =>
                {
                    // So if we ended our search on a non-null element, then that means 
                    // we found what we were looking for! 
                    if (itemBeforeTheItemToDelete.Next != null)
                    {
                        itemBeforeTheItemToDelete.Next = itemBeforeTheItemToDelete.Next.Next;
                        itemFound = true;

                        // If we deleted the last item in the list, update the tail! 
                        if (itemBeforeTheItemToDelete.Next == null)
                        {
                            _tail = itemBeforeTheItemToDelete;
                        }

                    }
                }
            );
            return itemFound;
        }

        /// <summary>
        /// I'm really kind of proud of the consistent level of 
        /// abstraction in this method :) 
        /// </summary>
        /// <param name="valueToFind"></param>
        /// <param name="valueToInsert"></param>
        /// <returns></returns>
        public bool InsertAfter(T valueToFind, T valueToInsert)
        {
            ListItem<T> newItem = new ListItem<T>(valueToInsert);
            bool successfullyInserted = false;
            // There are three different situations to account for 

            // 1: Empty List
            if (ListIsEmptyAndValueToFindIsNull(valueToFind))
            {
                _head = newItem;
                _tail = _head;
                successfullyInserted = true;
            }
            // 2: valueToFind is null (insert at front of list)
            else if (ListIsNotEmptyAndValueToFindIsNull(valueToFind))
            {
                InsertAtFrontOfList(newItem);
                successfullyInserted = true;
            }
            // 3: valueToFind is not null and head is not null
            else if (ListIsNotEmptyAndValueToFindIsNotNull(valueToFind))
            {
                successfullyInserted = InsertAfterFoundValue(valueToFind, newItem);
            }
            return successfullyInserted;
        }

        private void InsertAtFrontOfList(ListItem<T> newHead)
        {
            newHead.Next = _head;
            _head = newHead;
        }

        private bool InsertAfterFoundValue(T valueToFind, ListItem<T> newItem)
        {
            bool wasFoundAndInserted = false;
            base.IterateOverStack(
                _head,
                currentItem => currentItem != null && !currentItem.Value.Equals(valueToFind),
                currentItem =>
                {
                    if (currentItem != null)
                    {
                        newItem.Next = currentItem.Next;
                        currentItem.Next = newItem;
                        wasFoundAndInserted = true;

                        // If the item we just inserted is at the end of the list,
                        // update the tail field! 
                        if (newItem.Next == null)
                        {
                            _tail = newItem;
                        }

                    }
                }
            );
            return wasFoundAndInserted;
        }

        private bool ListIsEmptyAndValueToFindIsNull(T valueToFind)
        {
            return _head == null && valueToFind == null;
        }

        private bool ListIsNotEmptyAndValueToFindIsNull(T valueToFind)
        {
            return _head != null && valueToFind == null;
        }

        private bool ListIsNotEmptyAndValueToFindIsNotNull(T valueToFind)
        {
            return _head != null && valueToFind != null;
        }
    }
}
