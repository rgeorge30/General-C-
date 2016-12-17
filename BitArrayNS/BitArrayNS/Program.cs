using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitArrayNS
    {
    class Program
        {
        static void Main(string[] args)
            {
            // Create array of 5 elements and 3 true values.
            bool[] array = new bool[5];
            array[0] = true;
            array[1] = false; // <-- False value is default
            array[2] = true;
            array[3] = false;
            array[4] = true;

            // Create BitArray from the array.
            BitArray bitArray = new BitArray(array);

            // Display all bits.
            foreach (bool bit in bitArray)
                {
                Console.WriteLine(bit);
                }
            //Display bits using index into bit array
            for (int i = 0; i < 5; i++)
                Console.WriteLine(bitArray[i]);
            }
        }
    }
