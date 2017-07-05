using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                String[] r = new String[4];
                r[0] = "kkkawq";
                r[1] = "fsdgfdgfdddhfgj";
                r[2] = "ddd";
                r[3] = "ddd";
                Text_var7 t = new Text_var7(r);
                show(t);
                
                String row = "sssss";
                t.AddRow(row);
                show(t);
                String del = t.DelRow(4);
                Console.WriteLine("Row was delete: " + del);
                show(t);

                double period = t.GetPeriod('k');
                Console.WriteLine("Period of symbol 'k' in text {0}", period);
                Console.WriteLine("Rows was change: ");
                t.ChangeRow("ddd", "123");
                show(t);
                t.DeleteRow();
                show(t);

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
        public static void show(Text_var7 text)
        {
            for (int i = 0; i < text.GetText().Length; i++)
            {
                Console.WriteLine(text.GetText()[i]);
            }
            Console.WriteLine();
        }
    }
}
