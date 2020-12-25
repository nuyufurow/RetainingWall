using LinearAlgebra;

namespace Dr.RetainingWall
{
    class BoundaryCondition
    {
        static Matrix weiyibianjie01(int n, Matrix K)
        {
            int[] rowColIndexs = { 0 };
            if (n == 1)
            {
                rowColIndexs = new int[] { 4, 6 };
            }
            else if (n == 2)
            {
                rowColIndexs = new int[] { 4, 6, 7, 9 };
            }
            else if (n == 3)
            {
                rowColIndexs = new int[] { 4, 6, 7, 9, 10, 12 };
            }
            else if (n == 4)
            {
                rowColIndexs = new int[] { 4, 6, 7, 9, 10, 12, 13, 15 };
            }
            Matrix k = MatrixUtil.Part(K, rowColIndexs, rowColIndexs);
            return k;
        }

        static Matrix weiyibianjie02(int n, Matrix K)
        {
            int[] rowColIndexs = { 0 };
            if (n == 1)
            {
                rowColIndexs = new int[] { 3, 4, 6 };
            }
            else if (n == 2)
            {
                rowColIndexs = new int[] { 3, 4, 6, 7, 9 };
            }
            else if (n == 3)
            {
                rowColIndexs = new int[] { 3, 4, 6, 7, 9, 10, 12 };
            }
            else if (n == 4)
            {
                rowColIndexs = new int[] { 3, 4, 6, 7, 9, 10, 12, 13, 15 };
            }
            Matrix k = MatrixUtil.Part(K, rowColIndexs, rowColIndexs);
            return k;
        }
    }
}
