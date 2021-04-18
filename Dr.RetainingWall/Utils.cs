using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    internal static class MatrixUtil
    {
        internal static Matrix Part(Matrix entireMatrix, int[] rowIndexs, int[] colIndexs)
        {
            var eles = new double[rowIndexs.Length, colIndexs.Length];
            for (int i = 0; i < rowIndexs.Length; i++)
            {
                for (int j = 0; j < colIndexs.Length; j++)
                {
                    eles[i, j] = entireMatrix[rowIndexs[i], colIndexs[j]];
                }
            }
            Matrix partMatrix = new Matrix(eles);
            return partMatrix;
        }

        internal static void Vpa(ref Matrix m, int n)
        {
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    Math.Round(m[i, j], n);
                }
            }
        }
    }

    internal static class VectorUtil
    {
        internal static Vector Zeros(int n)
        {
            return new Vector(new double[n], VectorType.Column);
        }

        internal static void Vpa(ref Vector v, int n)
        {
            for (int i = 0; i < v.Count; i++)
            {
                v[i] = Math.Round(v[i], n);
            }
        }

        internal static Vector Scale(Vector v, double s)
        {
            Vector newV = v;
            for (int i = 0; i < v.Count; i++)
            {
                newV[i] = v[i] * s;
            }
            return newV;
        }
    }

    internal static class Util
    {
        internal static int ToInt(string str)
        {
            int n = 0;
            if (int.TryParse(str, out n) == false)
                throw new Exception(str + "不是数字");
            return n;
        }

        internal static double ToDouble(string str)
        {
            double d;
            if (double.TryParse(str.Trim(), out d) == false)
                throw new Exception(str + "不是数字");
            return d;
        }

        internal static string ToString(double[] arr)
        {
            string str = "[ ";


            foreach (var a in arr)
            {
                string strA = a.ToString();
                if (Math.Abs(a) > 0)
                {
                    strA = a.ToString("f2");
                }
                str += strA + " ";
            }
            str += "]";
            return str;
        }

        internal static string ToString(List<double> list)
        {
            string str = "[ ";


            foreach (var l in list)
            {
                string strA = l.ToString();
                if (Math.Abs(l) > 0)
                {
                    strA = l.ToString("f2");
                }
                str += strA + " ";
            }
            str += "]";
            return str;
        }

        internal static double Max(double a, double b, double c)
        {
            double max = Math.Max(a, b);
            return Math.Max(max, c);
        }

        internal static double Max(double a, double b, double c, double d)
        {
            double max = Math.Max(a, b);
            double max2 = Math.Max(max, c);
            return Math.Max(max2, d);
        }

        internal static double Min(double a, double b, double c)
        {
            double min = Math.Min(a, b);
            return Math.Min(min, c);
        }

        internal static double Min(double a, double b, double c, double d)
        {
            double min = Math.Min(a, b);
            double min2 = Math.Min(min, c);
            return Math.Min(min2, d);
        }

        internal static void ShowWarning(string warnMsg)
        {
#if DEBUG
            MessageBox.Show(warnMsg);
#endif
        }
    }
}
