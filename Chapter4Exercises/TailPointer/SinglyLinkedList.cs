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


        public bool Delete(T value)
        {
           

            return false;
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
                            _tail = currentItem;
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
