using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Image represented by NxN matrix. Rotate it 90 degrees
//rotate layer by layer starting at outside and going in.
//save top, left-> top, bottom -> left;right-> bottom, savedTop-> right
namespace Image_Rotate
{
    
    class Program
    {
        static void Rotate90(int[,] matrix, int n)
        {
            for (int layer = 0; layer < n / 2; layer++)
            {
                /*
                int topR = 0 + layer; 
                int leftC = 0 + layer; 
                int bottomR = n - 1-layer; 
                int rightC = n - 1-layer;
                for (int x = leftC; x<rightC; x++)
                {
                    //save top, left-> top, bottom -> left;right-> bottom, savedTop-> right
                    int saveTop = matrix[topR+layer,x];
                    matrix[topR,x]= matrix[bottomR-x,x];
                    matrix[bottomR-x,x]= matrix[bottomR,rightC-x];
                    matrix[bottomR,rightC-x] = matrix[topR+x,rightC];
                    matrix[topR+x,rightC]=saveTop;
                }
                 * */
                int first = layer;
                int last = n - 1- layer;
                for (int i = first; i < last; i++)
                {
                    int offset = i - first; //very important - without this alg wont work. I substituted (last-offset) for all i when trying to do myself Grrr..
                    //save top, left-> top, bottom -> left;right-> bottom, savedTop-> right
                    int saveTop = matrix[first, i];
                    matrix[first, i] = matrix[last-offset, first];//left-> top
                    matrix[last - offset, first] = matrix[last, last - offset];//bottom -> left
                    matrix[last, last - offset] = matrix[i, last];//right-> bottom
                    matrix[i,last] = saveTop;
                }
            }
        }
        static void Main(string[] args)
        {
            int[,] input = new int[3,3] {{1,2,3},{4,5,6},{7,8,9}};
            Rotate90(input, 3);
            foreach (int i in input)
            {
                Console.Write("{0} ", i);
            }
            Console.ReadLine();

        }
    }
}
