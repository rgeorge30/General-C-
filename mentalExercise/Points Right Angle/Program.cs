using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Hw1: Given 3 points, find code to find out if any of them is a right angle
 *Hw2: Given 2 arrays of INT find common INT
 *Hw3Given sorted binary tree and 2 INT return total number of nodes in graph between teh 2 INTs
 *Hw4 Find first non repeating character in an array
 *TODO naming convention - including namespace (microsoft...). remove static
 *Beter than % 32
 *
*/
namespace Points_Right_Angle
{
    class Program
    {
        class Point
        {
            public double x;
            public double y;
            public Point(double px, double py)
            {
                x = px;
                y = py;
            }
        }
        //Find out if there is a right angle between 3 given points
        static bool isRightAngle(Point[] points)
        {
            double[] slope = new double[3];
            for (int i = 0; i < 3; i++)
                slope[i] = (points[(i + 1) % 3].y - points[i].y) / (points[(i + 1) % 3].x - points[i].x);
            
            //-Infinity and Infinity are valid double values
            //if slope1*slope2 = -1 then perpendicular
            for (int i = 0; i < 3; i++)
            {
                if(Double.IsInfinity(slope[i]) && slope[(i + 1) % 3]==0.0) return true;
                if (Double.IsInfinity(slope[(i + 1) % 3]) && slope[i] == 0.0) return true;
                if (slope[i] * slope[(i + 1) % 3] == -1 ) return true;
            }
            return false;
        }
        static void Swap(int[] input, int i, int j)
        {
            int temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
        static void QuickSort(int[] input, int start, int end)
        {
            if (start+1>=end )return;
            if (input ==null) return;
            //start is the pivot, two pointers one starting at start+1 and other starting at end -1
            int i = start + 1;
            int j = end - 1;
            while (i < j )
            {
                while (i< end && input[i]<input[start]) i++;
                while (j > start && input[j] > input[start]) j--;
                if (i<j && input[i]>input[j]) Swap(input, i,j);
            }
            if (start < j && input[start] > input[j]) Swap(input, start, j);
            QuickSort(input, start, j);
            QuickSort(input, j+1, end);
        }
        //Find all common interegers between 2 arrays of INT; assume the two arrays could be large
        static void FindCommonINT(int[] a, int[] b)
        {
            //Sort the two arrays 
            QuickSort(a, 0, a.Length);
            QuickSort(b, 0, b.Length);
            //use merge sort like process to get common INT;
            int i = 0; int j = 0;
            while (i < a.Length && j < b.Length)
            {
                if (a[i] == b[j])
                {
                    Console.Write(" {0}", a[i]);
                    i++; j++;
                }
                else if (a[i] > b[j]) j++; 
                    else i++;
            }
        }
        //Find first non repeating character in an array
        static void FindFirstNonRepChar(string input)
        {
            //Assume case insensitive - that is a matches with A
            //Assume punctuations, delimiters, escape char etc not counted
            //use 2 arrays - first is a bit map verifying how many times and one to store location if non repeating. Location has -1 if repeating
            int[] charMap = new int[32];
            int[] locMap = new int[32];
            for (int i=0; i<32; i++) {locMap[i] = -1;charMap[i] = 0;}
            //go thru the string and update Map and location
            for (int j =0; j < input.Length; j++)
            {
                char c = input[j];
                //figure out a better way - difference between a and A is 32
                int index = (c - 'A')%32;
                charMap[index]++;
                if (charMap[index] == 1) locMap[index] = j;
                else locMap[index] = -1;
            }
            int minLocation = input.Length;
            //Find min location that skipping -1
            for (int k = 0; k < 32; k++)
            {
                if (locMap[k] != -1)
                    if (locMap[k] < minLocation) minLocation = locMap[k];
            }
            if (minLocation != input.Length)
                Console.WriteLine(" First non repeating char at {0}: {1}", minLocation, input[minLocation]);
            else Console.WriteLine(" No non repeating char");
        }
        static void Main(string[] args)
        {
            /*
            Point[] points = new Point[3];
            points[0] = new Point(0.0, 0.0);
            points[1] = new Point(1.0, 0.0); 
            points[2] = new Point(0.0, 1.0);
            Console.WriteLine("{0}", isRightAngle(points));
            
            int[] input = { 10, 24, 20, 1, 15, 9, 6, 5, 12, 11 };
            QuickSort(input, 0, input.Length);
            foreach (int i in input) Console.WriteLine("{0}", i);
            
             
         int[] a = { 10, 24, 20, 1, 15, 9, 6, 5, 12, 11 };
         int[] b = { 1, 10, 22, 9, 13 };
         FindCommonINT(a, b);
      */
            //Console.WriteLine("{0}  {1}", ('b' - 'A')%32, 'B' - 'A'); 
      
            Console.ReadKey();
        }
    }
}
