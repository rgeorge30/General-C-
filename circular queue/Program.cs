using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace circular_queue
    {
    class CQueue
        {
        const int MAX = 3;
        int[] _array = new int[MAX];
        int headIndex = 0;
        int tailIndex = 0;
        public void Enqueue(int val)
            {
            if(isQueueFull()) 
                {
                Console.WriteLine(" Queue is full; {0} cannot be enqueued", val);
                throw new System.IndexOutOfRangeException();
                }
            _array[(++tailIndex) % MAX] = val;
            
            }
        public int Dequeue()
            {
            if (isQueueEmpty())
                {
                Console.WriteLine(" Queue is Empty;  cannot be dequeued");
                throw new System.IndexOutOfRangeException();
                }
            return _array[(++headIndex) % MAX];
            }
        public bool isQueueFull()
            {
            //tail+1 mod MAX =head
            if (headIndex == (tailIndex+1)%MAX)
                return true;
            else
                return false;
            }
        public bool isQueueEmpty()
            {
            //headIndex and tailIndex are the same; head always points to an empty location 
            if (headIndex == tailIndex)
                return true;
            else
                return false;
            }

        }
    class Program
        {
        static void Main(string[] args)
            {
            CQueue cq= new CQueue();
            cq.Enqueue(2);
            cq.Enqueue(3);
            Console.WriteLine("{0}", cq.isQueueEmpty());
            Console.WriteLine("{0}", cq.isQueueFull());
            try
                {
                cq.Enqueue(4);
                }
            catch( System.IndexOutOfRangeException)
                    {//do nothing
                }
            cq.Dequeue();
            cq.Enqueue(4);
            try
                {
                cq.Enqueue(5);
                }
            catch( System.IndexOutOfRangeException)
                    {//do nothing
                }
            Console.ReadLine();
            }
        }
    }
