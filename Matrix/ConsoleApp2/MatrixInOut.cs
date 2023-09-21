using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    static class MatrixInOut
    {
        public static void WriteToFile(string FileName, ListOfMatrix list)
        {
            try
            {
                File.WriteAllLines(FileName, list.ToStringArray());
            }
            catch
            {
                Console.WriteLine("Не удалось записать список матриц в файл {0}", FileName);
            }
        }


        public static ListOfMatrix ReadFromFile(string FileName)
        {
            string[] lines;
            ListOfMatrix list;
            try
            {
                lines = File.ReadAllLines(FileName);
                list = new ListOfMatrix();
                for (int i=0; i < lines.Count()-1; i+=2)
                {
                    list.Add(Matrix.Parse(string.Format("{0}\n{1}", lines[i], lines[i + 1])));
                }
            }
            catch
            {
                Console.WriteLine("Не удалось прочитать список матриц из файла {0}", FileName);
                return null;
            }
            return list;
        }

    }
}
