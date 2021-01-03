using System;
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
}
