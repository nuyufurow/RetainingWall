using LinearAlgebra;

namespace Dr.RetainingWall
{
    class StiffnessMatrix
    {
        public static Matrix PlaneFrameElementStiffness(double E, double A, double I, double L)
        {
            double w1 = E * A / L;
            double w2 = 12 * E * I / (L * L * L);
            double w3 = 6 * E * I / (L * L);
            double w4 = 4 * E * I / L;
            double w5 = 2 * E * I / L;
            Matrix k = new Matrix(new double[,]{
                { w1,   0,   0, -w1,   0,   0},
                {  0,  w2,  w3,   0, -w2,  w3},
                {  0,  w3,  w4,   0, -w3,  w5},
                {-w1,   0,   0,  w1,   0,   0},
                {  0, -w2, -w3,   0,  w2, -w3},
                {  0,  w3,  w5,   0, -w3,  w4}
            });
            return k;
        }

        private static void PlaneFrameAssemble(ref Matrix K, Matrix k, int i, int j)
        {
            //直接刚度法，总刚矩阵组装
            //PlaneFrameAssemble   根据单元矩阵和节点号进行总刚矩阵的组装
            K[3 * i - 3, 3 * i - 3] = K[3 * i - 3, 3 * i - 3] + k[0, 0];
            K[3 * i - 3, 3 * i - 2] = K[3 * i - 3, 3 * i - 2] + k[0, 1];
            K[3 * i - 3, 3 * i - 1] = K[3 * i - 3, 3 * i - 1] + k[0, 2];
            K[3 * i - 3, 3 * j - 3] = K[3 * i - 3, 3 * j - 3] + k[0, 3];
            K[3 * i - 3, 3 * j - 2] = K[3 * i - 3, 3 * j - 2] + k[0, 4];
            K[3 * i - 3, 3 * j - 1] = K[3 * i - 3, 3 * j - 1] + k[0, 5];

            K[3 * i - 2, 3 * i - 3] = K[3 * i - 2, 3 * i - 3] + k[1, 0];
            K[3 * i - 2, 3 * i - 2] = K[3 * i - 2, 3 * i - 2] + k[1, 1];
            K[3 * i - 2, 3 * i - 1] = K[3 * i - 2, 3 * i - 1] + k[1, 2];
            K[3 * i - 2, 3 * j - 3] = K[3 * i - 2, 3 * j - 3] + k[1, 3];
            K[3 * i - 2, 3 * j - 2] = K[3 * i - 2, 3 * j - 2] + k[1, 4];
            K[3 * i - 2, 3 * j - 1] = K[3 * i - 2, 3 * j - 1] + k[1, 5];

            K[3 * i - 1, 3 * i - 3] = K[3 * i - 1, 3 * i - 3] + k[2, 0];
            K[3 * i - 1, 3 * i - 2] = K[3 * i - 1, 3 * i - 2] + k[2, 1];
            K[3 * i - 1, 3 * i - 1] = K[3 * i - 1, 3 * i - 1] + k[2, 2];
            K[3 * i - 1, 3 * j - 3] = K[3 * i - 1, 3 * j - 3] + k[2, 3];
            K[3 * i - 1, 3 * j - 2] = K[3 * i - 1, 3 * j - 2] + k[2, 4];
            K[3 * i - 1, 3 * j - 1] = K[3 * i - 1, 3 * j - 1] + k[2, 5];

            K[3 * j - 3, 3 * i - 3] = K[3 * j - 3, 3 * i - 3] + k[3, 0];
            K[3 * j - 3, 3 * i - 2] = K[3 * j - 3, 3 * i - 2] + k[3, 1];
            K[3 * j - 3, 3 * i - 1] = K[3 * j - 3, 3 * i - 1] + k[3, 2];
            K[3 * j - 3, 3 * j - 3] = K[3 * j - 3, 3 * j - 3] + k[3, 3];
            K[3 * j - 3, 3 * j - 2] = K[3 * j - 3, 3 * j - 2] + k[3, 4];
            K[3 * j - 3, 3 * j - 1] = K[3 * j - 3, 3 * j - 1] + k[3, 5];

            K[3 * j - 2, 3 * i - 3] = K[3 * j - 2, 3 * i - 3] + k[4, 0];
            K[3 * j - 2, 3 * i - 2] = K[3 * j - 2, 3 * i - 2] + k[4, 1];
            K[3 * j - 2, 3 * i - 1] = K[3 * j - 2, 3 * i - 1] + k[4, 2];
            K[3 * j - 2, 3 * j - 3] = K[3 * j - 2, 3 * j - 3] + k[4, 3];
            K[3 * j - 2, 3 * j - 2] = K[3 * j - 2, 3 * j - 2] + k[4, 4];
            K[3 * j - 2, 3 * j - 1] = K[3 * j - 2, 3 * j - 1] + k[4, 5];

            K[3 * j - 1, 3 * i - 3] = K[3 * j - 1, 3 * i - 3] + k[5, 0];
            K[3 * j - 1, 3 * i - 2] = K[3 * j - 1, 3 * i - 2] + k[5, 1];
            K[3 * j - 1, 3 * i - 1] = K[3 * j - 1, 3 * i - 1] + k[5, 2];
            K[3 * j - 1, 3 * j - 3] = K[3 * j - 1, 3 * j - 3] + k[5, 3];
            K[3 * j - 1, 3 * j - 2] = K[3 * j - 1, 3 * j - 2] + k[5, 4];
            K[3 * j - 1, 3 * j - 1] = K[3 * j - 1, 3 * j - 1] + k[5, 5];
        }

        public static Matrix zonggangjuzhen(int n, double[] E, double[] A, double[] I, double[] H) 
        {
            Matrix K = Matrix.Ones((n + 1) * 3, (n + 1) * 3);
            if (n == 1)
            {
                Matrix k1 = PlaneFrameElementStiffness(E[0], A[0], I[0], H[0]);  //为单元刚度矩阵
                K = k1;                                                          //单刚同总刚
            }
            else if (n == 2)
            {
                Matrix k1 = PlaneFrameElementStiffness(E[0], A[0], I[0], H[0]);  //一层单元刚度矩阵
                Matrix k2 = PlaneFrameElementStiffness(E[1], A[1], I[1], H[1]);  //二层单元刚度矩阵
                PlaneFrameAssemble(ref K, k1, 1, 2);                             //将k1组装进K
                PlaneFrameAssemble(ref K, k2, 2, 3);                             //将k2组装进K
            }
            else if (n == 3)
            {
                Matrix k1 = PlaneFrameElementStiffness(E[0], A[0], I[0], H[0]);  //一层单元刚度矩阵
                Matrix k2 = PlaneFrameElementStiffness(E[1], A[1], I[1], H[1]);  //二层单元刚度矩阵
                Matrix k3 = PlaneFrameElementStiffness(E[2], A[2], I[2], H[2]);  //三层单元刚度矩阵
                PlaneFrameAssemble(ref K, k1, 1, 2);                             //将k1组装进K
                PlaneFrameAssemble(ref K, k2, 2, 3);                             //将k2组装进K
                PlaneFrameAssemble(ref K, k3, 3, 4);                             //将k3组装进K
            }
            else if (n == 4)
            {
                Matrix k1 = PlaneFrameElementStiffness(E[1], A[1], I[1], H[1]);  //一层单元刚度矩阵
                Matrix k2 = PlaneFrameElementStiffness(E[2], A[2], I[2], H[2]);  //二层单元刚度矩阵
                Matrix k3 = PlaneFrameElementStiffness(E[3], A[3], I[3], H[3]);  //三层单元刚度矩阵
                Matrix k4 = PlaneFrameElementStiffness(E[4], A[4], I[4], H[4]);  //四层单元刚度矩阵
                PlaneFrameAssemble(ref K, k1, 1, 2);                             //将k1组装进K
                PlaneFrameAssemble(ref K, k2, 2, 3);                             //将k2组装进K
                PlaneFrameAssemble(ref K, k3, 3, 4);                             //将k3组装进K
                PlaneFrameAssemble(ref K, k4, 4, 5);                             //将k4组装进K
            }
            return K;
        }
    }

}
