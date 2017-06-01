using System;
using System.Collections.Generic;
using System.Text;

namespace Base
{
    public class SinglyLinkedListBase<T>
    {
        protected void IterateOverStack(ListItem<T> currentItem, Func<ListItem<T>, bool> predicate, Action<ListItem<T>> actionToTake)
        {
            while (predicate(currentItem))
            {
                currentItem = currentItem.Next;
            }

            actionToTake(currentItem);
        }
    }
}
