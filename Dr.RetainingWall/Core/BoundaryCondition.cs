using LinearAlgebra;

namespace Dr.RetainingWall
{
    class BoundaryCondition
    {
        public static Matrix weiyibianjie01(int n, Matrix K)
        {
            int[] rowColIndexs = { 0 };
            if (n == 1)
            {
                rowColIndexs = new int[] { 3, 5 };
            }
            else if (n == 2)
            {
                rowColIndexs = new int[] { 3, 5, 6, 8 };
            }
            else if (n == 3)
            {
                rowColIndexs = new int[] { 3, 5, 6, 8, 9, 11 };
            }
            else if (n == 4)
            {
                rowColIndexs = new int[] { 3, 5, 6, 8, 9, 11, 12, 14 };
            }
            Matrix k = MatrixUtil.Part(K, rowColIndexs, rowColIndexs);
            return k;
        }

        public static Matrix weiyibianjie02(int n, Matrix K)
        {
            int[] rowColIndexs = { 0 };
            if (n == 1)
            {
                rowColIndexs = new int[] { 2, 3, 5 };
            }
            else if (n == 2)
            {
                rowColIndexs = new int[] { 2, 3, 5, 6, 8 };
            }
            else if (n == 3)
            {
                rowColIndexs = new int[] { 2, 3, 5, 6, 8, 9, 11 };
            }
            else if (n == 4)
            {
                rowColIndexs = new int[] { 2, 3, 5, 6, 8, 9, 11, 12, 14 };
            }
            Matrix k = MatrixUtil.Part(K, rowColIndexs, rowColIndexs);
            return k;
        }
    }
}
