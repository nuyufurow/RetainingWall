using System;
using System.Collections.Generic;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    class InnerForceCalculation
    {
        public static double[,] neilijisuan(bool isHinge, double E, double[] A, double[] I, Matrix k, Vector f, int n, double[] Q, double[] H)
        {
            double[,] FF = new double[2, 2 * n];

            //本子函数用于求解节点位移和结构内力以及支座反力,适用于墙底固结
            //输入的k为总刚度矩阵（为引入位移边界条件划掉0行0列），f为等效节点荷载列向量（为引入位移边界条件划掉0行0列）,n为挡土墙层数
            //Q = hezaijisuan(n, H, r, h, p0, rg, rq);    //调用荷载计算子程序，计算所有节点的荷载设计值
            Vector u = Vector.MatMulColVec(k.Inverse(), f);       //左除总刚度矩阵，求解未知位移

            List<Vector> F0 = new List<Vector>();
            List<double[]> M = new List<double[]>();
            List<double[]> F = new List<double[]>();
            for (int j = 0; j < n; j++)
            {
                Matrix kp = StiffnessMatrix.PlaneFrameElementStiffness(E, A[j], I[j], H[j]);
                double[] arrU = new double[6];
                if (!isHinge)
                {
                    arrU = j == 0 ? new double[] { 0, 0, 0, u[2 * j], 0, u[2 * j + 1] } : new double[] { u[2 * j - 2], 0, u[2 * j - 1], u[2 * j], 0, u[2 * j + 1] };
                }
                else
                {
                    arrU = j == 0 ? new double[] { 0, 0, u[2 * j], u[2 * j + 1], 0, u[2 * j + 2] } : new double[] { u[2 * j - 1], 0, u[2 * j], u[2 * j + 1], 0, u[2 * j + 2] };
                }

                Vector up = new Vector(arrU, VectorType.Column);  //回带总体位移
                Vector Fp = Vector.MatMulColVec(kp, up);
                F0.Add(Fp);
            }
            for (int j = 0; j < n + 1; j++)
            {
                double Mlp = j == 0 ? 0 : -((Q[j - 1] - Q[j]) * Math.Pow(H[j - 1], 2) / 30 + Q[j] * Math.Pow(H[j - 1], 2) / 12);
                double Mrp = j == n ? 0 : (Q[j] - Q[j + 1]) * Math.Pow(H[j], 2) / 20 + Q[j + 1] * Math.Pow(H[j], 2) / 12;
                M.Add(new double[] { Mlp, Mrp });

                double Flp = j == 0 ? 0 : (Q[j - 1] - Q[j]) * H[j - 1] * 3 / 20 + 0.5 * Q[j] * H[j - 1];
                double Frp = j == n ? 0 : (Q[j] - Q[j + 1]) * H[j] * 7 / 20 + 0.5 * Q[j + 1] * H[j];
                F.Add(new double[] { Flp, Frp });
            }

            for (int i = 0; i < n; i++)
            {
                FF[0, 2 * i] = F0[i][2] + M[i][1];
                FF[0, 2 * i + 1] = F0[i][5] + M[i + 1][0];

                FF[1, i] += (F0[i][1] + F[i][1]);
                FF[1, i + 1] += (F0[i][4] + F[i + 1][0]);
            }

            return FF;
        }

        public static double[] kuazhongzuidaM(Matrix M, int n, double[] Q, double[] H)
        {

            //本子函数用于求出跨中最大正弯矩
            //输入的M列向量为各节点内力,n为挡墙层数
            //Q = hezaijisuan(n, H, r, h, p0, rg, rq);    //调用荷载计算子程序，
            double[] Mmax = new double[] { 0, 0, 0, 0 }; //定义一个1x4的0向量，其中第N个元素为第N层的最大弯矩设计值

            if (n == 1)
            {
                double x1 = (Q[1] + Math.Sqrt(Math.Pow(Q[1], 2) + 2 * (Q[0] - Q[1]) * M[1, 1] / H[0])) * H[0] / (Q[1] - Q[0]);   //一层挡墙AB的剪力为0处的距离墙顶的距离
                double x2 = (Q[1] - Math.Sqrt(Math.Pow(Q[1], 2) + 2 * (Q[0] - Q[1]) * M[1, 1] / H[0])) * H[0] / (Q[1] - Q[0]);
                double x0;
                if (x1 > 0 && x1 < H[0])
                {
                    x0 = x1;
                }
                else
                {
                    x0 = x2;
                }
                Mmax[0] = -(Q[0] - Q[1]) * Math.Pow(x0, 3) / (6 * H[0]) - 0.5 * Q[1] * Math.Pow(x0, 2) + M[1, 1] * x0;
            }
            else if (n == 2)
            {
                double x1 = (Q[2] + Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * M[1, 2] / H[1])) * H[1] / (Q[2] - Q[1]);   //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x2 = (Q[2] - Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * M[1, 2] / H[1])) * H[1] / (Q[2] - Q[1]);
                double x0;
                if (x1 > 0 && x1 < H[1])
                {
                    x0 = x1;
                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[2] + Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * (M[1, 2] + M[1, 1]) / H[1])) * H[1] / (Q[2] - Q[1]);   //一层挡墙AB的剪力为0处的距离墙顶的距离
                double x22 = (Q[2] - Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * (M[1, 2] + M[1, 1]) / H[1])) * H[1] / (Q[2] - Q[1]);
                double x00;
                if (x11 > H[1] && x11 < H[0] + H[1])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                Mmax[0] = -(Q[1] - Q[2]) * Math.Pow(x00, 3) / (6 * H[1]) - 0.5 * Q[2] * Math.Pow(x00, 2) + M[1, 2] * x00 + M[1, 1] * (x00 - H[1]);
                Mmax[1] = -(Q[1] - Q[2]) * Math.Pow(x0, 3) / (6 * H[1]) - 0.5 * Q[2] * Math.Pow(x0, 2) + M[1, 2] * x0;
            }
            else if (n == 3)
            {
                double x1 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * M[1, 3] / H[2])) * H[2] / (Q[3] - Q[2]);              //三层挡墙CD的剪力为0处的距离墙顶的距离
                double x2 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * M[1, 3] / H[2])) * H[2] / (Q[3] - Q[2]);
                double x0;
                if (x1 > 0 && x1 < H[2])
                {
                    x0 = x1;

                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1, 2] + M[1, 3]) / H[2])) * H[2] / (Q[3] - Q[2]);    //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x22 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1, 2] + M[1, 3]) / H[2])) * H[2] / (Q[3] - Q[2]);
                double x00;
                if (x11 > H[2] && x11 < H[2] + H[1])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                double x111 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1, 1] + M[1, 2] + M[1, 3]) / H[2])) * H[2] / (Q[3] - Q[2]);//一层挡墙AB的剪力为0处的距离墙顶的距离
                double x222 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1, 1] + M[1, 2] + M[1, 3]) / H[2])) * H[2] / (Q[3] - Q[2]);
                double x000;
                if (x111 > H[2] + H[1] && x111 < H[2] + H[1] + H[0])
                {
                    x000 = x111;
                }
                else
                {
                    x000 = x222;
                }
                Mmax[0] = -(Q[2] - Q[3]) * Math.Pow(x000, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x000, 2) + M[1, 3] * x000 + M[1, 2] * (x000 - H[2]) + M[1, 1] * (x000 - H[2] - H[1]);
                Mmax[1] = -(Q[2] - Q[3]) * Math.Pow(x00, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x00, 2) + M[1, 3] * x00 + M[1, 2] * (x00 - H[2]);
                Mmax[2] = -(Q[2] - Q[3]) * Math.Pow(x0, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x0, 2) + M[1, 3] * x0;
            }
            else if (n == 4)
            {

                double x1 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * M[1, 4] / H[3])) * H[3] / (Q[4] - Q[3]);      //四层挡墙DE的剪力为0处的距离墙顶的距离
                double x2 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * M[1, 4] / H[3])) * H[3] / (Q[4] - Q[3]);
                double x0;
                if (x1 > 0 && x1 < H[3])
                {
                    x0 = x1;
                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //三层挡墙CD的剪力为0处的距离墙顶的距离
                double x22 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x00;
                if (x11 > H[3] && x11 < H[3] + H[2])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                double x111 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 2] + M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x222 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 2] + M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x000;
                if (x111 > H[2] + H[3] && x111 < H[3] + H[2] + H[1])
                {
                    x000 = x111;
                }
                else
                {
                    x000 = x222;
                }
                double x1111 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 1] + M[1, 2] + M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //一层挡墙BC的剪力为0处的距离墙顶的距离
                double x2222 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1, 1] + M[1, 2] + M[1, 3] + M[1, 4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x0000;
                if (x1111 > H[3] + H[2] + H[1] && x1111 < H[3] + H[2] + H[1] + H[0])
                {
                    x0000 = x1111;
                }
                else
                {
                    x0000 = x2222;
                }
                Mmax[0] = -(Q[3] - Q[4]) * Math.Pow(x0000, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x0000, 2) + M[1, 4] * x0000
                    + M[1, 3] * (x0000 - H[3]) + M[1, 2] * (x0000 - H[2] - H[3]) + M[1, 1] * (x0000 - H[1] - H[2] - H[3]);
                Mmax[1] = -(Q[3] - Q[4]) * Math.Pow(x000, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x000, 2) + M[1, 4] * x000
                    + M[1, 3] * (x000 - H[3]) + M[1, 2] * (x000 - H[2] - H[3]);
                Mmax[2] = -(Q[3] - Q[4]) * Math.Pow(x00, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x00, 2) + M[1, 4] * x00 + M[1, 3] * (x00 - H[3]);
                Mmax[3] = -(Q[3] - Q[4]) * Math.Pow(x0, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x0, 2) + M[1, 4] * x0;
            }
            return Mmax;
        }

        public static Matrix neilitiaofu(double[,] M01, double[,] M02, double T)
        {
            //本子函数用于内力调幅内力组合
            //M01为neilijisuan01( K,k,f,n,Q,H )求得的墙底固结时，各节点内力
            //M02为neilijisuan02( K,k,f,n,Q,H )求得的墙底铰接时，各节点内力
            //M0为调幅后用于配筋的各节点内力，经推导，弯矩和剪力都可以用M01*T+M02*(1-T)调幅
            //T为内力调幅系数

            Matrix mM1 = new Matrix(M01);
            Matrix mM2 = new Matrix(M02);

            Matrix newM = mM1 * T + mM2 * (1 - T);
            return newM;
        }

        public static double[] neilizuhe(int n, Matrix Mg, double[] Mmaxg)
        {
            if (n == 1)
            {
                return new double[] { Mg[0, 0], Mmaxg[0], Mg[0, 1] };
            }
            else if (n == 2)
            {
                return new double[] { Mg[0, 0], Mmaxg[0], Mg[0, 1], Mmaxg[1], Mg[0, 3] };
            }
            else if (n == 3)
            {
                return new double[] { Mg[0, 0], Mmaxg[0], Mg[0, 1], Mmaxg[1], Mg[0, 3], Mmaxg[2], Mg[0, 5] };
            }
            else if (n == 4)
            {
                return new double[] { Mg[0, 0], Mmaxg[0], Mg[0, 1], Mmaxg[1], Mg[0, 3], Mmaxg[2], Mg[0, 5], Mmaxg[3], Mg[0, 7] };
            }
            return new double[] { };
        }

    }
}
