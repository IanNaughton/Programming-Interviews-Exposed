using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class ListItem<T>
    {
        public ListItem(T newValue)
        {
            Value = newValue;
        }

        public ListItem<T> Next { get; set; }

        public T Value { get; set; }
    }
}
