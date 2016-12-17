using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

//Using BitArray and BitVector32
// See SimpleStringPermutationandComb for additional uses of BitArray
namespace BitArrayNS1
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
            //Do XOR
            BitArray bitArray1 = new BitArray(array);
            BitArray bitArray2 = bitArray1.Xor(bitArray);

            //Use BitVector32 to create masks
            BitVector32 vector = new BitVector32(0);//specifying 0 as initial value make sure all bits are clear
            int firstBit = BitVector32.CreateMask();//uses first bit if no param specified
            int secondBit = BitVector32.CreateMask(firstBit);//passing previous bit mask as param 
            int thirdBit = BitVector32.CreateMask(secondBit);
            vector[firstBit] = true;
            vector[secondBit + thirdBit] = true;
            //vector[secondBit | thirdBit] = true;// this also works
            
            Console.WriteLine("Bit vector {0}", vector);
            Console.WriteLine("Bit vector {0}", vector.Data);
            Console.WriteLine("Bit vector {0}", vector.ToString());

            //Using BitVector32 to do bit packing - ie taking several smaller number and packing them into one large number
            //Create sections of various sizes to store the smaller numebers - note there is no "new"
            BitVector32.Section firstSection = BitVector32.CreateSection(10);
            BitVector32.Section secSection = BitVector32.CreateSection(50, firstSection);
            BitVector32.Section thirdSection = BitVector32.CreateSection(500, secSection);
            BitVector32 packedBits = new BitVector32(0);
            packedBits[firstSection] = 10;
            packedBits[secSection] = 20;
            packedBits[thirdSection] = 490;
            Console.WriteLine(" first sec {0} sec sec {1} third sec {2}", packedBits[firstSection], packedBits[secSection], packedBits[thirdSection]);
            Console.ReadLine();
            }
        }
    }
