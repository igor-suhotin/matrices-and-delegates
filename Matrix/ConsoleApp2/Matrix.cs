using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Matrix: IComparable<Matrix>
    {
        private double[,] data = new double[2, 2];
        public Matrix() { }
        public Matrix(double item11, double item12, double item21, double item22)
        {
            data[0, 0] = item11;
            data[0, 1] = item12;
            data[1, 0] = item21;
            data[1, 1] = item22;
        }

        public double this[int row, int column]
        {
            get { return data[row - 1, column - 1]; }
            set { data[row - 1, column - 1] = value; }
        }

        public double Determinant
        {
            get
            {
                return data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];
            }
        }
        public override string ToString()
        {
            return String.Format("{0} {1}\n{2} {3}", data[0, 0], data[0, 1], data[1, 0], data[1, 1]);
        }

        public static Matrix operator +(Matrix first, Matrix second)
        {
            return new Matrix(
                first.data[0, 0] + second.data[0, 0],
                first.data[0, 1] + second.data[0, 1],
                first.data[1, 0] + second.data[1, 0],
                first.data[1, 1] + second.data[1, 1]
                 );
        }

        public static Matrix operator -(Matrix first, Matrix second)
        {
            return new Matrix(
                first.data[0, 0] - second.data[0, 0],
                first.data[0, 1] - second.data[0, 1],
                first.data[1, 0] - second.data[1, 0],
                first.data[1, 1] - second.data[1, 1]
                );
        }

        public static Matrix operator ++(Matrix first)
        {
            return first + 1;
        }

        public static Matrix operator --(Matrix first)
        {
            return first - 1;
        }

        public static Matrix operator +(Matrix first, double second)
        {
            return new Matrix(
                first.data[0, 0] + second, first.data[0, 1] + second,
                first.data[1, 0] + second, first.data[1, 1] + second
                );
        }

        public static Matrix operator +(double first, Matrix second)
        {
            return new Matrix(
                second.data[0, 0] + first, second.data[0, 1] + first,
                second.data[1, 0] + first, second.data[1, 1] + first
                );
        }

        public static Matrix operator -(Matrix first, double second)
        {
            return new Matrix(
                first.data[0, 0] - second, first.data[0, 1] - second,
                first.data[1, 0] - second, first.data[1, 1] - second
                );
        }

        public static Matrix operator -(double first, Matrix second)
        {
            return new Matrix(
                second[1, 1] - first, second[1, 2] - first,
                second[2, 1] - first, second[2, 2] - first
                );
        }

        public static Matrix operator *(Matrix first, double second)
        {
            return new Matrix(
                first.data[0, 0] * second, first.data[0, 1] * second,
                first.data[1, 0] * second, first.data[1, 1] * second
                );
        }

        public static Matrix operator *(double first, Matrix second)
        {
            return new Matrix(
                second.data[0, 0] * first, second.data[0, 1] * first,
                second.data[1, 0] * first, second.data[1, 1] * first
                );
        }

        public static Matrix operator /(Matrix first, double second)
        {
            return new Matrix(
                first.data[0, 0] / second, first.data[0, 1] / second,
                first.data[1, 0] / second, first.data[1, 1] / second
                );
        }

        public static Matrix operator *(Matrix first, Matrix second)
        {
            return new Matrix(
                first.data[0, 0] * second.data[0, 0] + first.data[0, 1] * second.data[1, 0],
                first.data[0, 0] * second.data[0, 1] + first.data[0, 1] * second.data[1, 1],
                first.data[1, 0] * second.data[0, 0] + first.data[1, 1] * second.data[1, 0],
                first.data[1, 0] * second.data[0, 1] + first.data[1, 1] * second.data[1, 1]
                );
        }

        public Matrix Unit()
        {
            return new Matrix(1, 0, 0, 1);
        }

        /*
         * исходная матрица
            A11 A12
            A21 A22
            транспонированная
            A11 A21
            A12 A22
            союзная
            A22 -A12
            -A21 A11
            обратная
            A22/D -A12/D
            -A21/D A11/D
        */
        public Matrix Inverse()
        {
            var det = this.Determinant;
            if (det == 0)
                throw new DivideByZeroException("Обратная матрица не может быть вычислена, так как определитель равен нулю");
            return new Matrix(data[1, 1] / det, -data[0, 1] / det, -data[1, 0] / det, data[0, 0] / det);
        }

        public static Matrix operator /(Matrix first, Matrix second)
        {
            return first * second.Inverse();
        }

        public static Boolean operator <(Matrix first, Matrix second)
        {
            return first.Determinant < second.Determinant;
        }

        public static Boolean operator >(Matrix first, Matrix second)
        {
            return first.Determinant > second.Determinant;
        }

        public override Boolean Equals(object other)
        {
            if (other == null || !(other is Matrix)) return false;
            return data[0, 0] == ((Matrix) other).data[0, 0] && data[0, 1] == ((Matrix) other).data[0, 1] &&
                   data[1, 0] == ((Matrix) other).data[1, 0] && data[1, 1] == ((Matrix) other).data[1, 1];
        }

        public override int GetHashCode()
        {
            return String.Format("{0}_{1}_{2}_{3}", data[0, 0], data[0, 1], data[1, 0], data[1, 1]).GetHashCode();
        }

        public static Boolean operator ==(Matrix first, Matrix second)
        {
            if (System.Object.ReferenceEquals(first, second)) return true;
            if ((object)first == null) return false;
            
            return first.Equals(second);
        }

        public static Boolean operator !=(Matrix first, Matrix second)
        {
            if (System.Object.ReferenceEquals(first, second)) return false;
            if ((object)first == null) return true;

            return ! first.Equals(second);
        }

        public static Matrix Parse(string str)
        {
            string[] lines = str.Split(new char[] { '\n' });
            if (lines.Length < 2) throw new FormatException(@"Невозможно получить данные: нет символа \n");
            string[][] items = new string[2][];
            items[0] = lines[0].Trim().Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (items[0].Length != 2) throw new FormatException(@"Невозможно получить данные: в первой строке не два значения");
            items[1] = lines[1].Trim().Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (items[1].Length != 2) throw new FormatException(@"Невозможно получить данные: во второй строке не два значения");

            return new Matrix(Double.Parse(items[0][0]), Double.Parse(items[0][1]), Double.Parse(items[1][0]), Double.Parse(items[1][1]));
        }

        public static bool TryParse(string str, out Matrix matr)
        {
            matr = null;
            bool success = true;

            try
            {
                matr = Parse(str);
            }
            catch
            {
                success = false;
            }

            return success;
        }
                
        public int CompareTo(Matrix other)
        {
            if (this == other) return 0;
            if (this < other) return -1;
            return 1;

        }
    }
}