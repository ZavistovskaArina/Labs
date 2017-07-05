using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Text_var7
    {
        private Row[] text;

        public Text_var7(String[] text)
        {
            Row[] rows = new Row[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                rows[i] = new Row(text[i]);
            }
            this.text = rows;
        }
        public String[] GetText()
        {
            String[] temp = new String[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                temp[i] = text[i].GetSymbols();
            }
            return temp;
        }
        public void AddRow(String row)
        {
            Row addRow = new Row(row);
            Row[] newText = new Row[text.Length + 1];
            int i = 0;
            for (; i < text.Length; i++)
            {
                newText[i] = text[i];
            }
            newText[i] = addRow;
            text = newText;
        }
        public String DelRow(int index)
        {
            Row result = null;
            if (index > text.Length - 1 || index < 0)
            {
                throw new ArgumentException("Index out of bounds");
            }
            else
            {
                Row[] newText = new Row[text.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    newText[i] = text[i];
                }
                for (int i = index + 1; i < text.Length; i++)
                {
                    newText[i - 1] = text[i];
                }
                result = text[index];
                text = newText;
            }
            return result.GetSymbols();
        }

        public void Clear()
        {
            text = null;
        }

        public double GetPeriod(char ch)
        {
            double count = 0, char_count=0;
            foreach (Row row in text)
            {
                foreach(var symbol in row.GetSymbols())
                {
                    count++;
                    if (symbol == ch)
                        char_count++;
                }
            }
            return char_count/count;
        }

        public void ChangeRow(string s1, string s2)
        {
            for(int j = 0; j < text.Length; j++)
            {
                string str = text[j].GetSymbols();
                string s3 = "";
                for (int i = 0; i < str.Length; i++)
                { 
                    if (IsSubstr(str,i,s1))
                    {
                        s3+=s2;
                        i += s1.Length - 1;
                    }
                    else s3 += str[i];
                }
                text[j] = new Row(s3);
            }
        }
        private static bool IsSubstr(string str, int index, string substr)
        {
            for (int i = 0; i < substr.Length; i++)
            {
                if (substr[i] != str[index + i]) return false;
            }
            return true;
        }
        
        public void DeleteRow()
        {
            text = text.Distinct().ToArray();
        }
    }
}
