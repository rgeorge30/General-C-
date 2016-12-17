using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace mentalExercise
{
    public class Stk
    {
        string[] s;
        int index;
        int highVal;

        public Stk(int n)
        {
            s = new string[20];
            index = 0;
            highVal = n;
        }
        public void push(string str)
        {
            if (index!=highVal)
            s[index++] = str;
        }
        public void pop()
        {
            if (index != 0) 
            Console.WriteLine(s[--index]);
        }
    }
   /* public class StkL
    {
        string[] s;
        int index;
        int highVal;

        public StkL(int n)
        {
            s = new string[20];
            index = 0;
            highVal = n;
        }
        public void push(string str)
        {
            if (index != highVal)
                s[index++] = str;
        }
        public void pop()
        {
            if (index != 0)
                Console.WriteLine(s[--index]);
        }
    }
    */
    class Program
    {

        public int fib(int n)
        {
            if (n <= 2) return 1;
            return fib(n - 1) + fib(n - 2);
        }

        //find square root using newton's approach.
        /*
         *Square root of a number
 
Consider the problem of finding the square root of a number. There are many methods of computing square roots, and Newton's method is one.
 For example, if one wishes to find the square root of 612, this is equivalent to finding the solution to
 x^2 = 612
The function to use in Newton's method is then,
 f(x) = x^2-612
with derivative,
 F'(x) = 2x
With an initial guess of 10, the sequence given by Newton's method is
 x1 = x0-f(xo)/f'(xo) = 10-(10^2-612)/2*10 = 35.6
 x2 = x1-f(x1)/f'(x1) = 35.6 - (35.6^-612)/(2*35.6) = 26.395
 x3=                  =                             = 24.790
 x4 =                 =                             = 24.738
 x5 =                 =                             = 24.738
Where the correct digits are underlined. With only a few iterations one can obtain a solution accurate to many decimal places.
 
Solution of cos(x) = x3
 
Consider the problem of finding the positive number x with cos(x) = x3. We can rephrase that as finding the zero of f(x) = cos(x) − x3. 
We have f'(x) = −sin(x) − 3x2. Since cos(x) ≤ 1 for all x and x3 > 1 for x > 1, we know that our zero lies between 0 and 1. 
We try a starting value of x0 = 0.5. (Note that a starting value of 0 will lead to an undefined result, showing the importance of using a starting point that is close to the zero.)

         * Pranav: Given 3 points find code to find out if it is a right angle
         * Given 2 arrays of INT, find common INT
         * Given sorted binary tree and 2 int, return total number of nodes in graph btween 2 ints
         */
        public void QuickSort(List<int> lst)
        {
            if (lst.Count == 0) return;

        }

        static ArrayList getPermutationsOfString(string s) {
            ArrayList permutations = new ArrayList();
            if (s == null) { // error case
                return null;
            }
            else if (s.Length == 0) { // base case
                permutations.Add("");
                return permutations;
            }
            char first = s[0]; // get the first character
            string remainder = s.Remove(0, 1); // remove the first character
            ArrayList words = getPermutationsOfString(remainder);
            foreach (string word in words) {
                for (int j = 0; j <= word.Length; j++) {
                string newpermutation = word.Insert(j, first.ToString());
                permutations.Add(newpermutation);
                }
            }
            return permutations;
        }
        //List all subsets of a given set
        List<List<int>> AllSubsets(List<int> set)
        {
            List<List<int>> allsubsets = new List<List<int>>();
            int max = 1 << set.Count;
            for (int i = 0; i < max; i++)
            {
                List<int> subset = new List<int>();
                int k = i;
                int index = 0;
                while (k > 0)
                {
                    if ((k & 1) > 0) subset.Add(set[index]);
                    k >>= 1;
                    index++;
                }
                allsubsets.Add(subset);
            }
            return allsubsets;
        }
        static void Main(string[] args)
        {
          //  Program p = new Program();
            //int n = p.fib(5);
            //Console.WriteLine (n);
           /* Stk stk1 = new Stk(5);
            stk1.push("abc"); stk1.push("abc1"); stk1.push("abc2"); stk1.push("ab3c"); stk1.push("abc4");
            stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop(); stk1.pop();
            */
            ArrayList lst = getPermutationsOfString("abc");
            foreach ( String str in lst )
                Console.Write( "{0} ", str );

             Console.Read();
        }
    }
}
