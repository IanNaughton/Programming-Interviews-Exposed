using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Stack
{
    /// <summary>
    /// This is your garden-variety LIFO 
    /// datastructure you remember from your 
    /// data structures class. 
    /// </summary>
    public class CustomStack<T> : SinglyLinkedListBase<T>
    {
        private ListItem<T> _head;

        public void Push(T value)
        {
            // Add a stack item
            var newItem = new ListItem<T>(value);

            if (_head == null)
            {
                _head = newItem;
            }
            else
            {
                ListItem<T> currentItem = _head;

                IterateOverStack(
                    currentItem, 
                    itemToTest => itemToTest.Next != null, 
                    topItem => currentItem = topItem
                );
                
                currentItem.Next = newItem; 
            }
        }

        public T Pop()
        {
            // Find the last element. At this point we have three options:

            // 1: There is no item on the stack
            if (_head == null)
            {
                throw new InvalidOperationException("There are no elements on the stack to pop! :O");
            }


            ListItem<T> poppedItem = null;

            // 2: The head is the only element
            if (_head.Next == null)
            {
                poppedItem = _head;
                _head = null;
            }
            else
            {
                // 3: There is more than one element
                ListItem<T> currentItem = _head;

                // Iterate until we find the second to last 
                // item in the list.
                IterateOverStack(
                    currentItem, 
                    itemToTest => itemToTest.Next.Next != null, 
                    secondToLastItem => {
                        poppedItem = secondToLastItem.Next;
                        secondToLastItem.Next = null;
                    }
                );

            }

            return poppedItem.Value;
        }
    }
}
