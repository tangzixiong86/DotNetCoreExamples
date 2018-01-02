using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Collections.Generic.Examples
{
    public class IComparerExamples
    {
        public static void Run()
        {
            // Creates and initializes a new ArrayList.
            List<string> myAL = new List<string>();
            myAL.Add("The");
            myAL.Add("quick");
            myAL.Add("brown");
            myAL.Add("fox");
            myAL.Add("jumps");
            myAL.Add("over");
            myAL.Add("the");
            myAL.Add("lazy");
            myAL.Add("dog");

            // Displays the values of the ArrayList.
            Console.WriteLine("The ArrayList initially contains the following values:");
            PrintIndexAndValues(myAL);

            // Sorts the values of the ArrayList using the default comparer.
            myAL.Sort();
            Console.WriteLine("After sorting with the default comparer:");
            PrintIndexAndValues(myAL);

            // Sorts the values of the ArrayList using the reverse case-insensitive comparer.
            IComparer<string> myComparer = new myReverserClass();
            myAL.Sort(myComparer);
            Console.WriteLine("After sorting with the reverse case-insensitive comparer:");
            PrintIndexAndValues(myAL);
        }
        public static void PrintIndexAndValues(IEnumerable myList)
        {
            int i = 0;
            foreach (Object obj in myList)
                Console.WriteLine("\t[{0}]:\t{1}", i++, obj);
            Console.WriteLine();
        }
    }
    public class myReverserClass : IComparer<string>
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        public int Compare(string x, string y)
        {
            return y.ToLower().CompareTo(x.ToLower());
        }

    }
}
