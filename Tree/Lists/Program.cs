using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lists
{
    /// If the item is found in the <see cref="IList{T}"/> then it's priority is increased by swapping it with it's predecessor in the <see cref="IList{T}"/>.
 /*       
    public static bool ProbabilitySearch<T>(this IList<T> list, T item)
        {
            Guard.ArgumentNull(list, "list");

            int i = 0;
            Comparer<T> comparer = Comparer<T>.Default;
            while (i < list.Count && !Compare.AreEqual(list[i], item, comparer))
            {
                i++;
            }

            if (i >= list.Count || !Compare.AreEqual(list[i], item, comparer))
            {
                return false;
            }

            // we can increase the items' priority as the item is not the first element in the array
            if (i > 0)
            {
                T temp = list[i - 1];
                list[i - 1] = list[i];
                list[i] = temp;
            }

            return true;
        }
  * */
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
