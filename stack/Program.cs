using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stack
    {
    // stack using array
    class Stack
        {
        const int MAX = 3;
        int[] _array = new int[MAX];
        int headIndex = -1;

        public void Push(int val)
            {
            if (isFull())
                {
                Console.WriteLine(" Stack is full; {0} cannot be pushed", val);
                throw new System.IndexOutOfRangeException();
                }
            _array[++headIndex] = val;
            }
        public int Pop()
            {
            if (isEmpty())
                {
                Console.WriteLine(" Stack is empty;  cannot be popped");
                throw new System.IndexOutOfRangeException();
                }
            return _array[headIndex--];
            }
        public bool isEmpty()
            {
            //if headIndex ==-1 empty
            if (headIndex == -1)
                return true;
            else
                return false;
            }
        public bool isFull()
            {
            //if headIndex == MAX-1 full
            if (headIndex == MAX-1)
                return true;
            else
                return false;
            }

        }
    class Program
        {
        static void Main(string[] args)
            {
            Stack st = new Stack();
            st.Push(2);
            st.Push(3);
            st.Push(3);
            try
                {
                st.Push(4);
                }
            catch (System.IndexOutOfRangeException)
                {//do nothing
                }
            st.Pop();
            st.Push(6);
            try
                {
                st.Push(5);
                }
            catch (System.IndexOutOfRangeException)
                {//do nothing
                }
            st.Pop();
            st.Pop();
            st.Pop();
            try
                {
                st.Pop();
                }
            catch (System.IndexOutOfRangeException)
                {//do nothing
                }
            Console.ReadLine();
            }
        }
    }
