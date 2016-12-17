using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Applied_Math
{
    class Program
    {
        /*
         * (define gcd a b )//greatest common divisor
(if b==0) a
else gcd (b (a%b)

         */ 
        int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
                return a;
        }
        int Lcm(int a, int b)
        {
            //lcm*gcd=a*b
            return a * b / Gcd(a, b);
        }
        int Fib(int n)
        {
            if (n == 0 || n == 1) return n;
            return Fib(n - 1)+ Fib(n-2);
        }

        //Given 2 lines determine if they intersect
        struct Line
        {
            public double _slope;
            public double _yIntercept;
        };
        bool LineIntersect(Line line1, Line line2)
        {
            // if slope and y interesect are the same, they are the same line
            const double epsilon = 0.000001;
            //Understand limitations of floating point representations. Never check for equality with==
            if (Math.Abs(line1._slope - line2._slope) < epsilon && Math.Abs(line1._yIntercept - line2._yIntercept) <epsilon)
                return true;
            //if slope is same; they do not intersect
            if (Math.Abs(line1._slope - line2._slope) < epsilon) return false; 
            //check for intersection = extra points
            //m1x+a = m2x+b => (m1-m2)x = b-a => x = (b-a)/(m1-m2); y = m1x+a
                       
            return true;
        }
        /*Print average of stream of numbers
One brute force approach is to compute average at each time, when a number is entered. Each average calculation will be O(n). Hence the total time taken will be O(n2).
Better Approach:
When a new number is entered, we consider the previous average and compute the new average in constant time, i.e O(1) 
       new average = (old average*(n-1)+ new num)/n
         void streamAvg(double arr[], int n)
         {
            float avg = 0;
            for(int i = 0; i < n; i++)
            {
               avg = (avg * i + arr[i])/(i+1);

               printf("Average of %d numbers is %f \n", i+1, avg);
            }
         }

        */

        //find number of trailing 0s in n factorial
        //count number of 2&5, 0s in stream of numbers till n
        int FindNumTrailingZeroes(int num /*OfNFactorial*/)
        {
            int count = 0;
            if (num < 0) {
            Console.Write("Factorial is not defined for negative numbers");
            return 0;
            }
            for (int i = 5; num / i > 0; i *= 5) 
                count += num / i;
            
            return count;
        }
        //Add two numbers without using + or other arithmetic operations
        //
        int Add_No_Arithm(int a, int b)
        {
            if (b == 0) return a;
            int sum = a ^ b; // add without carrying
            int carry = (a & b) << 1; // carry, but don’t add
            return Add_No_Arithm(sum, carry); // recurse
        }
        //Implement *, -, / using only +
        int Multiply_No_Arithm(int a, int b)
        {
            if (a < b) return Multiply_No_Arithm(b, a);//optimize for perf
            int result =0;
            for (int i = 0; i < b; i++)
                result = Add_No_Arithm(result, a);
            return result;
        }
        int Minus_No_Arithm(int a, int b)
        {
            return Add_No_Arithm(a, -b);
           // int result = 0;
           // return result;
        }
        bool DifferentSigns(int a, int b)
        {
            return ((a < 0 && b > 0) || (a > 0 && b < 0)) ? true : false;
        }

        /*IMP TODO a better way to do div (and multiplication) is to use the power of 2. For example, 87/7.. shift left 7 --becomes 14--28--56 (count=8). 87-56 = 39. 39-28 = 11 (count =8+4) 11-7 (count = 8+4+1) 
        if we dont do this approach, we have to do minus lot of times and is wasteful (e.g. 100000/2)
        */

        int Div_No_Arithm(int a, int b)
        {
            /*
             * Division is the trickiest, because we usually think of 21 / 3 as something like “if you divide a 21 foot board into 3 pieces, how big is each piece?” If we think about it the other way around (e.g., the reciprocal), it’s a little easier: “I divided a 21 foot board in x pieces and got pieces of 3 feet each, how many pieces were there?” From here, we can see that if we continuously subtract 3 feet from 21 feet, we'll know how many pieces there are.That is, we continuously subtract b from a and count how many times we can do that.
             */
            int result = Math.Abs(a);
            int divisor = Math.Abs(b);// this is critical otherwise wont work
            int nCount = 0;
            while (result > divisor)
            {
                result = Minus_No_Arithm(result, divisor); nCount++;
            }
            //round up if result*2>divisor
            if (result * 2 >= divisor) nCount++;
            //Take care of signs
            if (DifferentSigns(a, b)) nCount = -nCount;
            return nCount;
        }
        //find max of 2 num withou using any comparison operator
        int FindMaxNotUsingOper(int n1, int n2)
        {
            //Let c = a - b. Let k = the most significant bit of c. Return a - k * c
            int c = n1 - n2;
            int k = (c >> 31) & 0x1;
            return(n1 - k * c);
        }

        /*
         * Line of best fit:
         * more accurate way of finding the line of best fit is the least square method . 
Use the following steps to find the equation of line of best fit for a set of ordered pairs. 
Step 1: Calculate the mean of the x-values and the mean of the y-values. 
Step 2: Compute the sum of the squares of the x-values. 
Step 3: Compute the sum of each x-value multiplied by its corresponding y-value. 
Step 4: Calculate the slope of the line using the formula: 
m = (sigma(xy)- (sigma(x)*sigma(y)/n) )  /
         (sigma (x^2) - (sigma (x))^2/n )
 where n is the total number of data points. 

Step 5: Compute the y-intercept of the line by using the formula: 

        b=Ybar-m*Xbar


where Xbar and Ybar are the mean of the x- and y-coordinates of the data points respectively. 

         */
        //Given a two dimensional graph with points on it, find a line which passes the most number of points
        /*First calculate line between every two points using slope and y intercept. We have a bunch of line segments, represented as a slope and y-intercept, and we want to find the most common slope and y-intercept.How can we find the most common one? 
This is really no different than the old “find the most common number in a list of numbers” problem.We just iterate through the lines segments and use a hash table to count the num¬ber of times we’ve seen each line.

        struct Point
        {
            public double X;
            public double Y;
        }
        Line GetLine(Point P1, Point P2)
        {
            //

            //return new Line(
        }

         */ 
        //Write a function int BitSwapReqd(int A, int B) to determine the number of bits required to convert integer A to integer B.	pg
        //Each ‘1’ in the xor will represent one different bit between A and B. We then simply need to count the number of bits that are ‘1’.
        int BitSwapReqd(int a, int b)
        {
            int nCount = 0;
            for (int c = a ^ b; c != 0; c = c >> 1) {
                nCount += c & 1;
            }
            return nCount;
        }
        //Given an integer, print the next smallest and next largest number that have the same number of 1 bits in their binary representation
        //to find next largest: find the 0 in least significant position having one before it, swap the two
        //to find next smallest: find the 1 in least significant position having 0 before it, swap the two
        int BitSet(int input, int position, bool b)
        {
            //get mask
           int OneMask = 1<<position;
           int ZeroMask = ~(1<<position);
            //SetBits
            if(b)
            {
                input |= OneMask; 
            }
            else{
                input &= ZeroMask;
            }
            return input;
        }
        bool GetBit(int input, int position)
        {
            if((input &= (1<<position))>0)
                return true;
            else return false;
        }
        void FindNextBit(int input, int startPosition, bool isOne)
        {
        }
        void GetNextWithSame1(bool isLarge)
        {
        }

        //Generate nth prime number
        int GenPrime(int num) {
            int count=0;
		int numTest=1;
		int finalPrime=0;
		
		Console.WriteLine("Enter a value 'N' to find the 'Nth' prime number.");
		int primeN = num;
		
		do
		{
			double limit = Math.sqrt(numTest);
			boolean prime = true;
		
			if(numTest==1)
			{
				prime = false;
			}
				
			else if(limit<2)
			{	
				prime=true;
			}
			
			else
			{
	            //check if any number from 2 to square root of number can divide the number
					for(int divisor = 2; divisor <= limit && prime; divisor++)
					{	
						if(numTest % divisor == 0)
						{	
							prime = false;
							break;
						}
			/*
						else
						{
							prime=true;
							finalPrime = numTest;
						}
			*/			
					}
			}
			
			if(prime==true)
			{
				count=count+1;
				finalPrime=numTest;
				numTest++;
				Console.WriteLine("The current prime number stored is: " + finalPrime);
			}
			else
			{
				numTest++;
			}
			
		}while(count<primeN);
        }


        

        /*return subsets of a set
        When we’re generating a set, we have two choices for each element: (1) the element is
        in the set (the “yes” state) or (2) the element is not in the set (the “no” state). This means
        that each subset is a sequence of yesses / nos—e.g., “yes, yes, no, no, yes, no”
        »» This gives us 2^n possible subsets. How can we iterate through all possible sequences
        of “yes” / “no” states for all elements? If each “yes” can be treated as a 1 and each “no” can
        be treated as a 0, then each subset can be represented as a binary string.
        »» Generating all subsets then really just comes down to generating all binary numbers
        (that is, all integers). Easy!
         */
      /*  List<List<int>> AllSubsets(List<int> set) {
        //List of Lists data structure
        List<List<int>> allsubsets = new List<List<int>>();
    //2^n possible subset
        int max = 1 << set.Count;
        for (int i = 0; i < max; i++) {
        List<int> subset = new List<int>();
        int k = i;
        int index = 0;
        while (k > 0) {
        if ((k & 1) > 0) subset.Add(set[index]);
        k >>= 1;
        index++;
        }
        allsubsets.Add(subset);
        }
        return allsubsets;
    /*

        static void Main(string[] args)
        {
            Program p = new Program();
            //Console.WriteLine("GCD is {0}", p.Gcd(10, 4));
            //Console.WriteLine("LCM is {0}", p.Lcm(4, 10));
            //Console.WriteLine("Fib {0} is {1}", 5, p.Fib(5));
            //Console.WriteLine("Add no arithm {0}", p.Add_No_Arithm(459, 674));
            //Console.WriteLine("Multiply_No_Arithm {0}", p.Multiply_No_Arithm(3, 2));
            //Console.WriteLine("Minus_No_Arithm {0}", p.Minus_No_Arithm(10, 50));
            //Console.WriteLine("Div_No_Arithm {0}", p.Div_No_Arithm(11, -2));
            Console.WriteLine("FindMax_No_Arithm {0}", p.FindMaxNotUsingOper(-23, 3));
            Console.ReadKey();
        }
    }
}

/*
 * 10 4;6 2;4 0
*/
/*

DataStructure-Program to add two sparse matrices.
#include <stdio.h>
#include <conio.h>
#include <alloc.h>
#define MAX1 3
#define MAX2 3
#define MAXSIZE 9
#define BIGNUM 100
struct sparse
{
int *sp ;
int row ;
int *result ;
} ;
void initsparse ( struct sparse * ) ;
void create_array ( struct sparse * ) ;
int count ( struct sparse ) ;
void display ( struct sparse ) ;
void create_tuple ( struct sparse *, struct sparse ) ;
void display_tuple ( struct sparse ) ;
void addmat ( struct sparse *, struct sparse, struct sparse ) ;
void display_result ( struct sparse ) ;
void delsparse ( struct sparse * ) ;
void main( )
{
struct sparse s[5] ;
int i ;
clrscr( ) ;
for ( i = 0 ; i <= 4 ; i++ )
initsparse ( &s[i] ) ;
create_array ( &s[0] ) ;
create_tuple ( &s[1], s[0] ) ;
display_tuple ( s[1] ) ;
create_array ( &s[2] ) ;
create_tuple ( &s[3], s[2] ) ;
display_tuple ( s[3] ) ;
addmat ( &s[4], s[1], s[3] ) ;
printf ( "\nResult of addition of two matrices: " ) ;
display_result ( s[4] ) ;
for ( i = 0 ; i <= 4 ; i++ )
delsparse ( &s[i] ) ;
getch( ) ;
}
// initialises structure elements 
void initsparse ( struct sparse *p )
{
p -> sp = NULL ;
p -> result = NULL ;
}
// dynamically creates the matrix
void create_array ( struct sparse *p )
{
int n, i ;
// allocate memory 
p -> sp = ( int * ) malloc ( MAX1 * MAX2 * sizeof ( int ) ) ;
// add elements to the array 
for ( i = 0 ; i < MAX1 * MAX2 ; i++ )
{
printf ( "Enter element no. %d:", i ) ;
scanf ( "%d", &n ) ;
* ( p -> sp + i ) = n ;
}
}
// displays the contents of the matrix 
void display ( struct sparse s )
{
int i ;
// traverses the entire matrix 
for ( i = 0 ; i < MAX1 * MAX2 ; i++ )
{
// positions the cursor to the new line for every new row 
if ( i % MAX2 == 0 )
printf ( "\n" ) ;
printf ( "%d\t", * ( s.sp + i ) ) ;
}
}
// counts the number of non-zero elements 
int count ( struct sparse s )
{
int cnt = 0, i ;
for ( i = 0 ; i < MAX1 * MAX2 ; i++ )
{
if ( * ( s.sp + i ) != 0 )
cnt++ ;
}
return cnt ;
}
// creates an array that stores information about non-zero elements 
void create_tuple ( struct sparse *p, struct sparse s )
{
int r = 0 , c = -1, l = -1, i ;
// get the total number of non-zero elements
//and add 1 to store total no. of rows, cols, and non-zero values 
p -> row = count ( s ) + 1 ;
// allocate memory 
p -> sp = ( int * ) malloc ( p -> row * 3 * sizeof ( int ) ) ;
// store information about
total no. of rows, cols, and non-zero values 
* ( p -> sp + 0 ) = MAX1 ;
* ( p -> sp + 1 ) = MAX2 ;
* ( p -> sp + 2 ) = p -> row - 1 ;
l = 2 ;
// scan the array and store info. about non-zero values in the 3-tuple 
for ( i = 0 ; i < MAX1 * MAX2 ; i++ )
{
c++ ;
// sets the row and column values 
if ( ( ( i % MAX2 ) == 0 ) && ( i != 0 ) )
{
r++ ;
c = 0 ;
}
// checks for non-zero element row, column and non-zero element value is assigned to the matrix 
if ( * ( s.sp + i ) != 0 )
{
l++ ;
* ( p -> sp + l ) = r ;
l++ ;
* ( p -> sp + l ) = c ;
l++ ;
* ( p -> sp + l ) = * ( s.sp + i ) ;
}
}
}
// displays the contents of the matrix 
void display_tuple ( struct sparse s )
{
int i, j ;
// traverses the entire matrix 
printf ( "\nElements in a 3-tuple: \n" ) ;
j = ( * ( s.sp + 2 ) * 3 ) + 3 ;
for ( i = 0 ; i < j ; i++ )
{
// positions the cursor to the new line for every new row 
if ( i % 3 == 0 )
printf ( "\n" ) ;
printf ( "%d\t", * ( s.sp + i ) ) ;
}
printf ( "\n" ) ;
}
// carries out addition of two matrices 
void addmat ( struct sparse *p, struct sparse s1, struct sparse s2 )
{
int i = 1, j = 1, k = 1 ;
int elem = 1 ;
int max, amax, bmax ;
int rowa, rowb, cola, colb, vala, valb ;
// get the total number of non-zero values from both the matrices 
amax = * ( s1.sp + 2 ) ;
bmax = * ( s2.sp + 2 ) ;
max = amax + bmax ;
// allocate memory for result 
p -> result = ( int * ) malloc ( MAXSIZE * 3 * sizeof ( int ) ) ;
while ( elem <= max )
{
// check if i < max. non-zero values
in first 3-tuple and get the values 
if ( i <= amax )
{
rowa = * ( s1.sp + i * 3 + 0 ) ;
cola = * ( s1.sp + i * 3 + 1 ) ;
vala = * ( s1.sp + i * 3 + 2 ) ;
}
else
rowa = cola = BIGNUM ; 
// check if j < max. non-zero values in secon 3-tuple and get the values 
if ( j <= bmax )
{
rowb = * ( s2.sp + j * 3 + 0 ) ;
colb = * ( s2.sp + j * 3 + 1 ) ;
valb = * ( s2.sp + j * 3 + 2 ) ;
}
else
rowb = colb = BIGNUM ;
// if row no. of both 3-tuple are same 
if ( rowa == rowb )
{
// if col no. of both 3-tuple are same 
if ( cola == colb )
{
// add tow non-zero values
store in result 
* ( p -> result + k * 3 + 0 ) = rowa ;
* ( p -> result + k * 3 + 1 ) = cola ;
* ( p -> result + k * 3 + 2 ) = vala + valb ;
i++ ;
j++ ;
max-- ;
}
// if col no. of first 3-tuple is < col no. of second 3-tuple, then add info. as it is to result 
if ( cola < colb )
{
* ( p -> result + k * 3 + 0 ) = rowa ;
* ( p -> result + k * 3 + 1 ) = cola ;
* ( p -> result + k * 3 + 2 ) = vala ;
i++ ;
}
// if col no. of first 3-tuple is > col no. of second 3-tuple, then add info. as it is to result 
if ( cola > colb )
{
* ( p -> result + k * 3 + 0 ) = rowb ;
* ( p -> result + k * 3 + 1 ) = colb ;
* ( p -> result + k * 3 + 2 ) = valb ;
j++ ;
}
k++ ;
}
// if row no. of first 3-tuple is < row no. of second 3-tuple, then add info. as it is to result 
if ( rowa < rowb )
{
* ( p -> result + k * 3 + 0 ) = rowa ;
* ( p -> result + k * 3 + 1 ) = cola ;
* ( p -> result + k * 3 + 2 ) = vala ;
i++ ;
k++ ;
}
// if row no. of first 3-tuple is > row no. of second 3-tuple, then add info. as it is to result 
if ( rowa > rowb )
{
* ( p -> result + k * 3 + 0 ) = rowb ;
* ( p -> result + k * 3 + 1 ) = colb ;
* ( p -> result + k * 3 + 2 ) = valb ;
j++ ;
k++ ;
}
elem++ ;
}
// add info about the total no. of rows, cols, and non-zero values that the resultant array contains to the result 
* ( p -> result + 0 ) = MAX1 ;
* ( p -> result + 1 ) = MAX2 ;
* ( p -> result + 2 ) = max ;
}
// displays the contents of the matrix 
void display_result ( struct sparse s )
{
int i ;
// traverses the entire matrix 
for ( i = 0 ; i < ( * ( s.result + 0 + 2 ) + 1 ) * 3 ; i++ )
{
// positions the cursor to the new line for every new row 
if ( i % 3 == 0 )
printf ( "\n" ) ;
printf ( "%d\t", * ( s.result + i ) ) ;
}
}
// deallocates memory 
void delsparse ( struct sparse *p )
{
if ( p -> sp != NULL )
free ( p -> sp ) ;
if ( p -> result != NULL )
free ( p -> result ) ;
} 

*/