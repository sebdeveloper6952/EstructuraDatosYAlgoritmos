using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseStack
{
    class StackFrame<T>
    {
        private T item;
        private StackFrame<T> next;

        public StackFrame(T item, StackFrame<T> next)
        {
            this.item = item;
            this.next = next;
        }

        /// <summary>
        /// Gets the item stored in this frame.
        /// </summary>
        /// <returns></returns>
        public T GetItem()
        {
            return this.item;
        }

        /// <summary>
        /// Gets the next frame of this frame.
        /// </summary>
        /// <returns></returns>
        public StackFrame<T> GetNext()
        {
            return this.next;
        }

        /// <summary>
        /// Sets the item of this frame
        /// </summary>
        /// <param name="item"></param>
        public void SetItem(T item)
        {
            this.item = item;
        }

        /// <summary>
        /// Sets the next frame of this stack frame.
        /// </summary>
        /// <param name="next"></param>
        public void SetNext(StackFrame<T> next)
        {
            this.next = next;
        }
    }
}
