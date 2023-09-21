using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class ListOfMatrix
    {
        private List<Matrix> matrixes = new List<Matrix>();

        public ListOfMatrix() { }
        
        public ListOfMatrix(params Matrix[] inMatrixes)
        {
            matrixes.AddRange(inMatrixes);
        }

        public ListOfMatrix(ListOfMatrix inMatrixes)
        {
            matrixes.InsertRange(0, inMatrixes.matrixes);
        }

        public Matrix this[int index]
        {
            get
            {
                return matrixes[index];
            }
            set
            {
                matrixes[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return matrixes.Count;                
            }
        }

        public void Sort()
        {
            matrixes.Sort();
        }

        public Matrix First()
        {
            return matrixes.First<Matrix>();
        }

        public Matrix Last()
        {
            return matrixes.Last<Matrix>();
        }

        public void Add(Matrix NewMatrix)
        {
            matrixes.Add(NewMatrix);
        }

        public Matrix Max()
        {
            return matrixes.Max<Matrix>();
        }

        public Matrix Min()
        {
            return matrixes.Min<Matrix>();
        }

        public Matrix[] ToArray()
        {
            return matrixes.ToArray<Matrix>();
        }

        public string[] ToStringArray()
        {
            var resultArray = new string[matrixes.Count<Matrix>()];
            for (int i=0; i < resultArray.Length; i++)
                resultArray[i] = matrixes[i].ToString();
            return resultArray;
        }


    }
}
