using System.IO;

namespace MatrixMult
{
    public class Matrix
    {

        public Matrix()
        {
            this.Rows = 1;
            this.Cols = 1;
            Values = new double[1, 1] { { 0 } };
        }
        public Matrix(int Rows, int Cols)
        {
            this.Rows = Rows;
            this.Cols = Cols;
            Values = new double[Rows, Cols];
        }
        public Matrix(int Rows, int Cols, double[,] Values)
        {
            this.Rows = Rows;
            this.Cols = Cols;
            this.Values = Values;
        }
        public Matrix(double[,] Values)
        {
            this.Rows = Values.GetLength(0);
            this.Cols = Values.GetLength(1);
            this.Values = Values;
        }

        public double[,] Values { get; set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public double this[int row, int col]
        {
            get => Values[row, col];
            set => Values[row, col] = value;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {

            var resMatrix = new Matrix(matrix1.Rows, matrix1.Cols);
            for (var i = 0; i < matrix1.Rows; ++i)
            {
                for (var j = 0; j < matrix1.Cols; ++j)
                {
                    resMatrix.Values[i, j] = matrix1.Values[i, j] - matrix2.Values[i, j];
                }
            }
            return resMatrix;
        }
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {

            var resMatrix = new Matrix(matrix1.Rows, matrix1.Cols);
            for (var i = 0; i < matrix1.Rows; ++i)
            {
                for (var j = 0; j < matrix1.Cols; ++j)
                {
                    resMatrix.Values[i, j] = matrix1.Values[i, j] + matrix2.Values[i, j];
                }
            }
            return resMatrix;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {

            var resMatrix = new Matrix(matrix1.Rows, matrix2.Cols);
            for (var i = 0; i < matrix1.Rows; ++i)
            {
                for (var j = 0; j < matrix2.Cols; ++j)
                {
                    for (var t = 0; t < matrix1.Cols; ++t)
                    {
                        resMatrix.Values[i, j] += matrix1.Values[i, t] * matrix2.Values[t, j];
                    }
                }
            }
            return resMatrix;
        }
        public static Matrix operator *(Matrix matrix, double coeff)
        {

            for (var i = 0; i < matrix.Rows; ++i)
            {
                for (var j = 0; j < matrix.Cols; ++j)
                {
                    matrix.Values[i, j] *= coeff;
                }
            }
            return matrix;
        }
        public static Matrix operator *(double coeff, Matrix matrix)
        {

            for (var i = 0; i < matrix.Rows; ++i)
            {
                for (var j = 0; j < matrix.Cols; ++j)
                {
                    matrix.Values[i, j] *= coeff;
                }
            }
            return matrix;
        }

        public override string ToString()
        {

            string res = "";
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Cols; ++j)
                {
                    res += Values[i, j] + "\t";
                }
                res += "\r\n";

            }
            return res;
        }
    }
}
