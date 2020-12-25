using LinearAlgebra;

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
    }
}
