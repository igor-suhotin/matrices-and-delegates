using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static ListOfMatrix MainList;
        delegate void Punkt();

        static Punkt[] punkts = new Punkt[7] { Exit, ReadFromConsole, ReadFromFile,  WriteList, WriteInfo, WriteOnDet, WriteSorted};


        static void Main(string[] args)
        {
            //Test();
            int choose;
            do
            {
                choose = Menu();
                punkts[choose]();
            } while (choose != 0);
        }

        static int Menu()
        {
            int choose, last=2;
            Console.WriteLine("Выберите действие\n0. Выход\n1. Ввод списка матриц с консоли\n2. Ввод списка матриц из файла");
            if (MainList != null)
            {
                Console.WriteLine("3. Вывод списка\n4. Вывод информации по списку");
                Console.WriteLine("5. Вывод всех матриц, определитель которых меньше заданного числа\n6. Вывод отсортированного списка");
                last = 6;
            }

            while(true)
            {
                if (int.TryParse(Console.ReadLine(), out choose) && choose >= 0 && choose <= last)
                    return choose;
                else
                    Console.WriteLine("Неправильный ввод, попробуйте ещё раз");
            }
        }

        static void Exit()
        {
            Console.WriteLine("Для выхода нажмите любой символ");
            Console.ReadKey();
        }

        static void ReadFromConsole()
        {
            int quantity;
            
            Console.WriteLine("Введите количество матриц в списке");
            if (! int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Неправильный ввод");
                return;
            }

            var tempList = new ListOfMatrix();
            try
            {
                for (int i = 0; i < quantity; i++)
                {
                    string lines = "";
                    Console.WriteLine("Ввод {0}-й матрицы", i+1);
                    for (int j = 1; j <= 2; j++)
                    {
                        Console.WriteLine("Введите два числа для {0}-й строки, разделённые пробелом", j);
                        lines += (Console.ReadLine() + '\n');
                    }

                    Console.WriteLine(lines);
                    tempList.Add(Matrix.Parse(lines));
                }
            }
            catch (Exception ex)
            {
                MainList = null;
                Console.WriteLine("Неправильный ввод данных\n{0}", ex.Message);
                return;
            }

            MainList = tempList;
        }

        static void ReadFromFile()
        {
            Console.WriteLine("Введите имя файла");
            string FileName = Console.ReadLine();

            ListOfMatrix tempList;
            if ((tempList = MatrixInOut.ReadFromFile(FileName)) == null)
            {
                Console.WriteLine("Cписок не прочитан");                
            }
            else
            {
                MainList = tempList;
            }
        }

        static void WriteList()
        {
            Console.WriteLine("===");
            for (int i = 0; i < MainList.Count; i++)
                Console.WriteLine("Матрица № {0}\n{1}", i+1, MainList[i]);
            Console.WriteLine("===");
        }

        static void WriteInfo()
        {
            Console.WriteLine("===\nКоличество матриц в списке - {0}\n===", MainList.Count);
        }

        static void WriteOnDet()
        {
            Console.WriteLine("Введите значение определителя");
            double det;
            if (!double.TryParse(Console.ReadLine(), out det))
            {
                Console.WriteLine("Неправильный ввод");
                return;
            }

            Console.WriteLine("===");
            for (int i = 0; i < MainList.Count; i++)
                if (MainList[i].Determinant < det)
                    Console.WriteLine("Матрица № {0}\n{1}", i + 1, MainList[i]);
            Console.WriteLine("===");
        }

        static void WriteSorted()
        {
            var tempList = new ListOfMatrix(MainList);
            tempList.Sort();
            Console.WriteLine("===");
            for (int i = 0; i < tempList.Count; i++)
                Console.WriteLine("Матрица № {0}\n{1}", i + 1, tempList[i]);
            Console.WriteLine("===");
        }
        // 
        static void Test()
        {
            var m = new Matrix();
            for (int i = 0; i < 4; i++)
            {
                m[1 + i / 2, 1 + i % 2] = (double)(i+1)/(i+2);
            }

            Console.WriteLine(m);
            m *= 2;
            Console.WriteLine(m.GetHashCode());
            //m--;
            Console.WriteLine(m);
            Console.WriteLine("4 * m * 3 / m = {0}", 4 * m * 3 / m);

            Matrix parsed;
            if (Matrix.TryParse("0 2\n 4  5", out parsed))
                Console.WriteLine("TryParse выполнено удачно, parsed=\n{0}", parsed);
            else
                Console.WriteLine("TryParse выполнено неудачно, parsed=\n{0}", parsed == null ? "null" : "bad value");

            if (Matrix.TryParse("0 \n 4  5", out parsed))
                Console.WriteLine("TryParse выполнено удачно, parsed=\n{0}", parsed);
            else
                Console.WriteLine("TryParse выполнено неудачно, parsed=\n{0}",  parsed == null ? "null" : "bad value");

            if (Matrix.TryParse(m.ToString(), out parsed))
                Console.WriteLine("TryParse выполнено удачно, parsed=\n{0}", parsed);
            else
                Console.WriteLine("TryParse выполнено неудачно, parsed=\n{0}", parsed == null ? "null" : "bad value");


            Console.ReadKey();
            Console.WriteLine();


            var list = new ListOfMatrix();
            Console.WriteLine("Количество элементов списка = {0}", list.Count);

            list = new ListOfMatrix(new Matrix(2, 1, 1, 2), new Matrix(0, 0, -1, 1));
            Console.WriteLine("Количество элементов списка = {0}", list.Count);
            Console.WriteLine("Первый элемент\n{0}", list.First());
            Console.WriteLine("Последний элемент\n{0}", list.Last());
            for (int i=0; i < list.Count; i++) 
                Console.WriteLine("list[{0}]\n{1}", i, list[i]);
            list.Sort();
            Console.WriteLine("После сортировки");
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine("list[{0}]\n{1}", i, list[i]);

            Console.WriteLine("Максимальный элемент\n{0}", list.Max());
            Console.WriteLine("Минимальный элемент\n{0}", list.Min());

            var arr = list.ToStringArray();
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine("arr[{0}]\n{1}", i, arr[i]);

            MatrixInOut.WriteToFile("list1.txt", list);
            MatrixInOut.WriteToFile("lis*t1.txt", list); // Недопустимый символ в имени файла - исключение при попытки записи

            ListOfMatrix list1 = MatrixInOut.ReadFromFile("list1.txt");
            for (int i = 0; i < list1.Count; i++)
                Console.WriteLine("list1[{0}]\n{1}", i, list1[i]);

            Console.ReadKey();

        }
    }
}
