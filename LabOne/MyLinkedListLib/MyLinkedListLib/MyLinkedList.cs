using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinkedListLib
{
    public class MyLinkedList<T> : IList<T>
    {
        private int _count;
        private ListNode<T> _first;
        private ListNode<T> _last;

        private ListNode<T> GetNode(int index)
        {
            ListNode<T> it = _first;
            for (int i = 0; i < index; ++i)
            {
                it = it.nextNode;
            }
            return it;
        }

        private class ListNode<U> : IEnumerator<U>
        {
            public U nodeValue;
            public ListNode<U> nextNode = null;
            public ListNode()
            {
            }
            public ListNode(U nodeValue, ListNode<U> nextNode)
            {
                this.nodeValue = nodeValue;
                this.nextNode = nextNode;
            }
            public ListNode(ListNode<U> node)
            {
                this.nodeValue = node.nodeValue;
                this.nextNode = node.nextNode;
            }

            public U Current
            {
                get { return nodeValue; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IDisposable.Dispose()
            {
                nextNode = null;
            }

            bool IEnumerator.MoveNext()
            {
                if (nextNode == null)
                {
                    return false;
                }
                else
                {
                    nodeValue = nextNode.nodeValue;
                    nextNode = nextNode.nextNode;

                    return true;
                }
            }

            void IEnumerator.Reset()
            {
                nextNode = null;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException();
                return GetNode(index).nodeValue;
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException();
                GetNode(index).nodeValue = value;
            }
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            Insert(_count, item);
        }

        public void Clear()
        {
            _count = 0;
            _first = null;
            _last = null;
        }

        public bool Contains(T item)
        {
            foreach (var value in this)
            {
                if (value.Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = 0;
            foreach (var value in this)
            {
                array[index + arrayIndex] = value;
                index++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_count == 0)
                return new ListNode<T>();
            var enumeratorNode = new ListNode<T>();
            enumeratorNode.nextNode = _first;
            return enumeratorNode;
        }

        public int IndexOf(T item)
        {
            int index = 0;
            foreach (var value in this)
            {
                if (value.Equals(item))
                    return index;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException();

            if (_count == 0)
            {
                _first = new ListNode<T>(item, null);
                _last = _first;
            }
            else if (index == 0)
            {
                var firstCopy = new ListNode<T>(_first);
                _first = new ListNode<T>(item, firstCopy);
            }
            else if (index == _count)
            {
                var newNode = new ListNode<T>(item, null);
                _last.nextNode = newNode;
                _last = newNode;
            }
            else
            {
                var prevNode = GetNode(index - 1);
                var nextNode = prevNode.nextNode;
                var newNode = new ListNode<T>(item, nextNode);
                prevNode.nextNode = newNode;
            }
            ++_count;
        }

        public bool Remove(T item)
        {
            if (_count == 0)
                return false;

            int pos = IndexOf(item);
            if (pos != -1)
            {
                RemoveAt(pos);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException();

            if (_count == 1)
            {
                Clear();
                return;
            }
            else if (index == 0)
            {
                var oldFirst = _first;
                _first = _first.nextNode;
                oldFirst.nextNode = null;
            }
            else if (index == _count - 1)
            {
                _last = GetNode(index);
                _last.nextNode = null;
            }
            else
            {
                var prevNode = GetNode(index - 1);
                var deletedNode = prevNode.nextNode;
                var nextNode = deletedNode.nextNode;

                prevNode.nextNode = nextNode;
                deletedNode.nextNode = null;
            }
            _count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
