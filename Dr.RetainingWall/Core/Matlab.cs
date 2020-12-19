using MatrixMult;

namespace Dr.RetainingWall
{
    class Matlab
    {
        public static Matrix FrameElementStiffness(double E, double A, double I, double L)
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

        public static void PlaneFrameAssemble(ref Matrix K, Matrix k, int i, int j)
        {
            K.Values[3 * i - 2, 3 * i - 2] = K.Values[3 * i - 2, 3 * i - 2] + k.Values[1, 1];
            K.Values[3 * i - 2, 3 * i - 1] = K.Values[3 * i - 2, 3 * i - 1] + k.Values[1, 2];
            K.Values[3 * i - 2, 3 * i] = K.Values[3 * i - 2, 3 * i] + k.Values[1, 3];
            K.Values[3 * i - 2, 3 * j - 2] = K.Values[3 * i - 2, 3 * j - 2] + k.Values[1, 4];
            K.Values[3 * i - 2, 3 * j - 1] = K.Values[3 * i - 2, 3 * j - 1] + k.Values[1, 5];
            K.Values[3 * i - 2, 3 * j] = K.Values[3 * i - 2, 3 * j] + k.Values[1, 6];
            K.Values[3 * i - 1, 3 * i - 2] = K.Values[3 * i - 1, 3 * i - 2] + k.Values[2, 1];
            K.Values[3 * i - 1, 3 * i - 1] = K.Values[3 * i - 1, 3 * i - 1] + k.Values[2, 2];
            K.Values[3 * i - 1, 3 * i] = K.Values[3 * i - 1, 3 * i] + k.Values[2, 3];
            K.Values[3 * i - 1, 3 * j - 2] = K.Values[3 * i - 1, 3 * j - 2] + k.Values[2, 4];
            K.Values[3 * i - 1, 3 * j - 1] = K.Values[3 * i - 1, 3 * j - 1] + k.Values[2, 5];
            K.Values[3 * i - 1, 3 * j] = K.Values[3 * i - 1, 3 * j] + k.Values[2, 6];
            K.Values[3 * i, 3 * i - 2] = K.Values[3 * i, 3 * i - 2] + k.Values[3, 1];
            K.Values[3 * i, 3 * i - 1] = K.Values[3 * i, 3 * i - 1] + k.Values[3, 2];
            K.Values[3 * i, 3 * i] = K.Values[3 * i, 3 * i] + k.Values[3, 3];
            K.Values[3 * i, 3 * j - 2] = K.Values[3 * i, 3 * j - 2] + k.Values[3, 4];
            K.Values[3 * i, 3 * j - 1] = K.Values[3 * i, 3 * j - 1] + k.Values[3, 5];
            K.Values[3 * i, 3 * j] = K.Values[3 * i, 3 * j] + k.Values[3, 6];
            K.Values[3 * j - 2, 3 * i - 2] = K.Values[3 * j - 2, 3 * i - 2] + k.Values[4, 1];
            K.Values[3 * j - 2, 3 * i - 1] = K.Values[3 * j - 2, 3 * i - 1] + k.Values[4, 2];
            K.Values[3 * j - 2, 3 * i] = K.Values[3 * j - 2, 3 * i] + k.Values[4, 3];
            K.Values[3 * j - 2, 3 * j - 2] = K.Values[3 * j - 2, 3 * j - 2] + k.Values[4, 4];
            K.Values[3 * j - 2, 3 * j - 1] = K.Values[3 * j - 2, 3 * j - 1] + k.Values[4, 5];
            K.Values[3 * j - 2, 3 * j] = K.Values[3 * j - 2, 3 * j] + k.Values[4, 6];
            K.Values[3 * j - 1, 3 * i - 2] = K.Values[3 * j - 1, 3 * i - 2] + k.Values[5, 1];
            K.Values[3 * j - 1, 3 * i - 1] = K.Values[3 * j - 1, 3 * i - 1] + k.Values[5, 2];
            K.Values[3 * j - 1, 3 * i] = K.Values[3 * j - 1, 3 * i] + k.Values[5, 3];
            K.Values[3 * j - 1, 3 * j - 2] = K.Values[3 * j - 1, 3 * j - 2] + k.Values[5, 4];
            K.Values[3 * j - 1, 3 * j - 1] = K.Values[3 * j - 1, 3 * j - 1] + k.Values[5, 5];
            K.Values[3 * j - 1, 3 * j] = K.Values[3 * j - 1, 3 * j] + k.Values[5, 6];
            K.Values[3 * j, 3 * i - 2] = K.Values[3 * j, 3 * i - 2] + k.Values[6, 1];
            K.Values[3 * j, 3 * i - 1] = K.Values[3 * j, 3 * i - 1] + k.Values[6, 2];
            K.Values[3 * j, 3 * i] = K.Values[3 * j, 3 * i] + k.Values[6, 3];
            K.Values[3 * j, 3 * j - 2] = K.Values[3 * j, 3 * j - 2] + k.Values[6, 4];
            K.Values[3 * j, 3 * j - 1] = K.Values[3 * j, 3 * j - 1] + k.Values[6, 5];
            K.Values[3 * j, 3 * j] = K.Values[3 * j, 3 * j] + k.Values[6, 6];
        }

        public static Matrix zonggangjuzhen(int n, double[] E, double[] A, double[] I, double[,] H) 
        {
            Matrix K = new Matrix((n + 1) * 3, (n + 1) * 3);
            if (n == 1) 
            {
                Matrix k1 = FrameElementStiffness(E[1], A[2], I[2], H[1, 1]);  //为单元刚度矩阵（同总刚度矩阵）
                K = k1;                                                        //单刚同总刚
            }
            else if(n == 2)
            {
                Matrix k1 = FrameElementStiffness(E[1], A[1], I[1], H[1, 1]);  // 一层单元刚度矩阵
                Matrix k2 = FrameElementStiffness(E[2], A[2], I[2], H[1, 2]);  // 二层单元刚度矩阵
                PlaneFrameAssemble(ref K, k1, 1, 2);                           // 将k1组装进K
                PlaneFrameAssemble(ref K, k2, 2, 3);                           // 将k2组装进K
            }
            return K;
        }
    }

}
