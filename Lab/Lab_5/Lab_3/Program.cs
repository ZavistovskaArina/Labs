using SortedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var SortList = new MySortedList<int>();
            SortList.AddEvent += (source, elem) => Console.WriteLine("Add element to the list : " + elem.Element);
            SortList.RemoveEvent += (source, elem) => Console.WriteLine("Removed element : " + elem.Element);
            SortList.ClearEvent += () => Console.WriteLine("Clear the list\n");
            var array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            SortList.Add(7);
            SortList.Add(2);
            SortList.Add(5);
            SortList.Add(10);
            SortList.Add(1);
            SortList.Add(24);
            SortList.Add(3);
            for (int i = 0; i < SortList.Count(); i++)
            {
                Console.WriteLine(SortList.GetByIndex(i));
            }
            Console.WriteLine();
            /*Console.WriteLine("Size: {0} \n", SortList.Count);
            Console.WriteLine("Value with index 2: {0} \n", SortList.GetByIndex(2));
            Console.WriteLine("SortList copy to array: ");
            SortList.CopyTo(array, 2);
            Console.WriteLine();
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            if (SortList.Contains(15))
                Console.WriteLine("SortList contains 15 \n");
            else Console.WriteLine("SortList not contains 15 \n");
            foreach (int ele in SortList)
            {
                Console.WriteLine(ele);
            }*/
            //Console.WriteLine();
            if (SortList.Remove(24))
                Console.WriteLine("SortList contains 24 and remove it \n");
            else Console.WriteLine("SortList not contains 24 \n");
            //Console.WriteLine("IndexOf 2: {0} \n", SortList.IndexOf(2));
            Console.WriteLine("Remove from SortList value 2: {0} \n", SortList.RemoveValue(2));
            foreach (int ele in SortList)
            {
                Console.WriteLine(ele);
            }
            SortList.Clear();
            Console.ReadKey();
        }
    }
}
