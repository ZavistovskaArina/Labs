using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Row
    {
        private String symbols;

        public Row(String symbols)
        {
            this.symbols = symbols;
        }

        public String GetSymbols()
        {
            return symbols;
        }

	public override bool Equals(object obj)
	{
	    if(obj is Row)
	    {
		return ((Row)obj).symbols.Equals(symbols);
	    }

	    return false;
	}

	public override int GetHashCode()
	{
	      return symbols.GetHashCode();
	}
        //public void SetSymbols(String symbols)
        //{
        //    this.symbols = symbols;
        //}
    }
}
