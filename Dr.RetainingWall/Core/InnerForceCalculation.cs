﻿using System;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    class InnerForceCalculation
    {
        public static Matrix neilijisuan01(double E, double[] A, double[] I, Matrix k, Vector f, int n, double[] Q, double[] H)
        {
            //本子函数用于求解节点位移和结构内力以及支座反力,适用于墙底固结
            //输入的k为总刚度矩阵（为引入位移边界条件划掉0行0列），f为等效节点荷载列向量（为引入位移边界条件划掉0行0列）,n为挡土墙层数
            //Q = hezaijisuan(n, H, r, h, p0, rg, rq);    //调用荷载计算子程序，计算所有节点的荷载设计值
            Vector u = Vector.MatMulColVec(k.Inverse(), f);       //左除总刚度矩阵，求解未知位移
            //VectorUtil.Vpa(ref u, 7);                             //将求解的位移保留7位有效数字
            if (n == 1)
            {
                Matrix k01 = StiffnessMatrix.PlaneFrameElementStiffness(E, A[0], I[0], H[0]);         //得到单元1的单元刚度矩阵
                Vector u01 = new Vector(new double[] { 0, 0, 0, u[0], 0, u[1] }, VectorType.Column);
                Vector F01 = Vector.MatMulColVec(k01, u01);
                double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                        //A点等效节点剪力
                double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;               //A点等效节点弯矩
                double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0];                        //B点等效节点剪力
                double Mb = -((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12);            //B点等效节点弯矩
                double[,] F = { { F01[2] + Ma, F01[5] + Mb }, { F01[1] + Fa, F01[4] + Fb } };
                return F;                                                                                      //输出所有节点的内力
            }
            //else if (n == 2)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, 0, u[0], 0, u[1], u[2], 0, u[3] }, VectorType.Column);  //回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);            //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                     //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;       //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                      //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));          //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                  //B点等效节点剪力
            //    double Mc = -((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12);    //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1];                      //C点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc }, VectorType.Column);           //支反力修正为真正结构的支反力
            //    return F;                                                                                          //输出所有节点的内力
            //}
            //else if (n == 3)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, 0, u[0], 0, u[1], u[2], 0, u[3], u[4], 0, u[5] }, VectorType.Column);  //回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);            //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                     //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;                    //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                                   //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));                       //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                               //B点等效节点剪力
            //    double Mc = -(((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12)
            //        - ((Q[2] - Q[3]) * H[2] * H[2] / 20 + Q[3] * H[2] * H[2] / 12));                       //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1]
            //        + (Q[2] - Q[3]) * H[2] * 7 / 20 + 0.5 * Q[3] * H[2];                                         //C点等效节点剪力
            //    double Md = -((Q[2] - Q[3]) * H[2] * H[2] / 30 + Q[3] * H[2] * H[2] / 12);                 //D点等效节点弯矩
            //    double Fd = (Q[2] - Q[3]) * H[2] * 3 / 20 + 0.5 * Q[3] * H[2];                                   //D点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc, 0, Fd, Md }, VectorType.Column);             //支反力修正为真正结构的支反力
            //    return F;                                                                                                       //输出所有节点的内力
            //}
            //else if (n == 4)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, 0, u[0], 0, u[1], u[2], 0, u[3], u[4], 0, u[5], u[6], 0, u[7] }, VectorType.Column); //回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);           //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                    //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;                   //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                                  //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));                      //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                              //B点等效节点剪力
            //    double Mc = -(((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12)
            //        - ((Q[2] - Q[3]) * H[2] * H[2] / 20 + Q[3] * H[2] * H[2] / 12));                      //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1] + (Q[2] - Q[3])
            //        * H[2] * 7 / 20 + 0.5 * Q[3] * H[2];                                                              //C点等效节点剪力
            //    double Md = -(((Q[2] - Q[3]) * H[2] * H[2] / 30 + Q[3] * H[2] * H[2] / 12)
            //        - ((Q[3] - Q[4]) * H[3] * H[3] / 20 + Q[4] * H[3] * H[3] / 12));                      //D点等效节点弯矩
            //    double Fd = (Q[2] - Q[3]) * H[2] * 3 / 20 + 0.5 * Q[3] * H[2] + (Q[3] - Q[4])
            //        * H[3] * 7 / 20 + 0.5 * Q[4] * H[3];                                                              //D点等效节点剪力
            //    double Me = -((Q[3] - Q[4]) * H[3] * H[3] / 30 + Q[4] * H[3] * H[3] / 12);                //E点等效节点弯矩
            //    double Fe = (Q[3] - Q[4]) * H[3] * 3 / 20 + 0.5 * Q[4] * H[3];                                  //E点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc, 0, Fd, Md, 0, Fe, Me }, VectorType.Column);  //支反力修正为真正结构的支反力
            //    return F;                                                                                                      //输出所有节点的内力
            //}
            return new Matrix(new double[,] { });
        }

        public static Matrix neilijisuan02(double E, double[] A, double[] I, Matrix k, Vector f, int n, double[] Q, double[] H)
        {
            //本子函数用于求解节点位移和结构内力以及支座反力,适用于墙底铰接
            //输入的k为总刚度矩阵（未引入位移边界条件划掉0行0列），f为等效节点荷载列向量（未引入位移边界条件划掉0行0列）,n为挡土墙层数
            //Q = hezaijisuan(n, H, r, h, p0, rg, rq);    //调用荷载计算子程序，计算所有节点的荷载设计值
            Vector u = Vector.MatMulColVec(k.Inverse(), f);       //左除总刚度矩阵，求解未知位移
            //VectorUtil.Vpa(ref u, 7);                             //将求解的位移保留7位有效数字
            if (n == 1)
            {
                Matrix k01 = StiffnessMatrix.PlaneFrameElementStiffness(E, A[0], I[0], H[0]);              //得到单元1的单元刚度矩阵
                Vector u01 = new Vector(new double[] { 0, 0, u[0], u[1], 0, u[2] }, VectorType.Column);
                Vector F01 = Vector.MatMulColVec(k01, u01);
                double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                             //A点等效节点剪力
                double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;                    //A点等效节点弯矩
                double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0];                             //B点等效节点剪力
                double Mb = -((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12);                 //B点等效节点弯矩
                double[,] F = { { F01[2] + Ma, F01[5] + Mb }, { F01[1] + Fa, F01[4] + Fb } };              //支反力修正为真正结构的支反力
                return F;                                                                                  //输出所有节点的内力
            }
            //else if (n == 2)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, u[0], u[1], 0, u[2], u[3], 0, u[4] }, VectorType.Column);//回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);            //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                     //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;       //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                      //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));          //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                  //B点等效节点剪力
            //    double Mc = -((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12);    //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1];                      //C点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc }, VectorType.Column);           //支反力修正为真正结构的支反力
            //    return F;                                                                                          //输出所有节点的内力
            //}
            //else if (n == 3)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, u[0], u[1], 0, u[2], u[3], 0, u[4], u[5], 0, u[6] }, VectorType.Column);//回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);            //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                     //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;                    //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                                   //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));                       //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                               //B点等效节点剪力
            //    double Mc = -(((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12)
            //        - ((Q[2] - Q[3]) * H[2] * H[2] / 20 + Q[3] * H[2] * H[2] / 12));                       //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1]
            //        + (Q[2] - Q[3]) * H[2] * 7 / 20 + 0.5 * Q[3] * H[2];                                         //C点等效节点剪力
            //    double Md = -((Q[2] - Q[3]) * H[2] * H[2] / 30 + Q[3] * H[2] * H[2] / 12);                 //D点等效节点弯矩
            //    double Fd = (Q[2] - Q[3]) * H[2] * 3 / 20 + 0.5 * Q[3] * H[2];                                   //D点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc, 0, Fd, Md }, VectorType.Column);             //支反力修正为真正结构的支反力
            //    return F;                                                                                                       //输出所有节点的内力
            //}
            //else if (n == 4)
            //{
            //    Vector U = new Vector(new double[] { 0, 0, u[0], u[1], 0, u[2], u[3], 0, u[4], u[5], 0, u[6], u[7], 0, u[8] }, VectorType.Column);//回带总体位移
            //    Vector F = Vector.MatMulColVec(K, U);           //求支反力，此支反力为未加上全部加上刚臂时，只有节点荷载的支反力
            //                                                    //支反力修正为真正结构的支反力，令F + 结构全部加上刚臂时的节点反力，得到真正结构的内力和支反力
            //    double Ma = (Q[0] - Q[1]) * H[0] * H[0] / 20 + Q[1] * H[0] * H[0] / 12;                   //A点等效节点弯矩
            //    double Fa = (Q[0] - Q[1]) * H[0] * 7 / 20 + 0.5 * Q[1] * H[0];                                  //A点等效节点剪力
            //    double Mb = -(((Q[0] - Q[1]) * H[0] * H[0] / 30 + Q[1] * H[0] * H[0] / 12)
            //        - ((Q[1] - Q[2]) * H[1] * H[1] / 20 + Q[2] * H[1] * H[1] / 12));                      //B点等效节点弯矩
            //    double Fb = (Q[0] - Q[1]) * H[0] * 3 / 20 + 0.5 * Q[1] * H[0] + (Q[1] - Q[2])
            //        * H[1] * 7 / 20 + 0.5 * Q[2] * H[1];                                                              //B点等效节点剪力
            //    double Mc = -(((Q[1] - Q[2]) * H[1] * H[1] / 30 + Q[2] * H[1] * H[1] / 12)
            //        - ((Q[2] - Q[3]) * H[2] * H[2] / 20 + Q[3] * H[2] * H[2] / 12));                      //C点等效节点弯矩
            //    double Fc = (Q[1] - Q[2]) * H[1] * 3 / 20 + 0.5 * Q[2] * H[1] + (Q[2] - Q[3])
            //        * H[2] * 7 / 20 + 0.5 * Q[3] * H[2];                                                              //C点等效节点剪力
            //    double Md = -(((Q[2] - Q[3]) * H[2] * H[2] / 30 + Q[3] * H[2] * H[2] / 12)
            //        - ((Q[3] - Q[4]) * H[3] * H[3] / 20 + Q[4] * H[3] * H[3] / 12));                      //D点等效节点弯矩
            //    double Fd = (Q[2] - Q[3]) * H[2] * 3 / 20 + 0.5 * Q[3] * H[2] + (Q[3] - Q[4])
            //        * H[3] * 7 / 20 + 0.5 * Q[4] * H[3];                                                              //D点等效节点剪力
            //    double Me = -((Q[3] - Q[4]) * H[3] * H[3] / 30 + Q[4] * H[3] * H[3] / 12);                //E点等效节点弯矩
            //    double Fe = (Q[3] - Q[4]) * H[3] * 3 / 20 + 0.5 * Q[4] * H[3];                                  //E点等效节点剪力
            //    F = F + new Vector(new double[] { 0, Fa, Ma, 0, Fb, Mb, 0, Fc, Mc, 0, Fd, Md, 0, Fe, Me }, VectorType.Column);  //支反力修正为真正结构的支反力
            //    return F;                                                                                                      //输出所有节点的内力
            //}
            return new Matrix(new double[,] { });
        }

        public static double[] kuazhongzuidaM(Matrix M, int n, double[] Q, double[] H)
        {

            //本子函数用于求出跨中最大正弯矩
            //输入的M列向量为各节点内力,n为挡墙层数
            //Q = hezaijisuan(n, H, r, h, p0, rg, rq);    //调用荷载计算子程序，
            double[] Mmax = new double[] { 0, 0, 0, 0 }; //定义一个1x4的0向量，其中第N个元素为第N层的最大弯矩设计值

            if (n == 1)
            {
                double x1 = (Q[1] + Math.Sqrt(Math.Pow(Q[1], 2) + 2 * (Q[0] - Q[1]) * M[1,1] / H[0])) * H[0] / (Q[1] - Q[0]);   //一层挡墙AB的剪力为0处的距离墙顶的距离
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
                double x1 = (Q[2] + Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * M[1,2] / H[1])) * H[1] / (Q[2] - Q[1]);   //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x2 = (Q[2] - Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * M[2, 3] / H[1])) * H[1] / (Q[2] - Q[1]);
                double x0;
                if (x1 > 0 && x1 < H[1])
                {
                    x0 = x1;
                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[2] + Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * (M[2, 3] + M[1,1]) / H[1])) * H[1] / (Q[2] - Q[1]);   //一层挡墙AB的剪力为0处的距离墙顶的距离
                double x22 = (Q[2] - Math.Sqrt(Math.Pow(Q[2], 2) + 2 * (Q[1] - Q[2]) * (M[2, 3] + M[1, 1]) / H[1])) * H[1] / (Q[2] - Q[1]);
                double x00;
                if (x11 > H[1] && x11 < H[0] + H[1])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                Mmax[0] = -(Q[1] - Q[2]) * Math.Pow(x00, 3) / (6 * H[1]) - 0.5 * Q[2] * Math.Pow(x00, 2) + M[2, 3] * x00 + M[1,1] * (x00 - H[1]);
                Mmax[0] = -(Q[1] - Q[2]) * Math.Pow(x0, 3) / (6 * H[1]) - 0.5 * Q[2] * Math.Pow(x0, 2) + M[2, 3] * x0;
            }
            else if (n == 3)
            {
                double x1 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * M[1,3] / H[2])) * H[2] / (Q[3] - Q[2]);              //三层挡墙CD的剪力为0处的距离墙顶的距离
                double x2 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * M[1,3] / H[2])) * H[2] / (Q[3] - Q[2]);
                double x0;
                if (x1 > 0 && x1 < H[2])
                {
                    x0 = x1;

                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1,2] + M[1,3]) / H[2])) * H[2] / (Q[3] - Q[2]);    //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x22 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1,2] + M[1,3]) / H[2])) * H[2] / (Q[3] - Q[2]);
                double x00;
                if (x11 > H[2] && x11 < H[2] + H[1])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                double x111 = (Q[3] + Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1,1] + M[1,2] + M[1,3]) / H[2])) * H[2] / (Q[3] - Q[2]);//一层挡墙AB的剪力为0处的距离墙顶的距离
                double x222 = (Q[3] - Math.Sqrt(Math.Pow(Q[3], 2) + 2 * (Q[2] - Q[3]) * (M[1,1] + M[1,2] + M[1,3]) / H[2])) * H[2] / (Q[3] - Q[2]);
                double x000;
                if (x111 > H[2] + H[1] && x111 < H[2] + H[1] + H[0])
                {
                    x000 = x111;
                }
                else
                {
                    x000 = x222;
                }
                Mmax[0] = -(Q[2] - Q[3]) * Math.Pow(x000, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x000, 2) + M[1,3] * x000 + M[1,2] * (x000 - H[2]) + M[1,1] * (x000 - H[2] - H[1]);
                Mmax[0] = -(Q[2] - Q[3]) * Math.Pow(x00, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x00, 2) + M[1,3] * x00 + M[1,2] * (x00 - H[2]);
                Mmax[1] = -(Q[2] - Q[3]) * Math.Pow(x0, 3) / (6 * H[2]) - 0.5 * Q[3] * Math.Pow(x0, 2) + M[1,3] * x0;
            }
            else if (n == 4)
            {

                double x1 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * M[1,4] / H[3])) * H[3] / (Q[4] - Q[3]);      //四层挡墙DE的剪力为0处的距离墙顶的距离
                double x2 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * M[1,4] / H[3])) * H[3] / (Q[4] - Q[3]);
                double x0;
                if (x1 > 0 && x1 < H[3])
                {
                    x0 = x1;
                }
                else
                {
                    x0 = x2;
                }
                double x11 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //三层挡墙CD的剪力为0处的距离墙顶的距离
                double x22 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x00;
                if (x11 > H[3] && x11 < H[3] + H[2])
                {
                    x00 = x11;
                }
                else
                {
                    x00 = x22;
                }
                double x111 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,2] + M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //二层挡墙BC的剪力为0处的距离墙顶的距离
                double x222 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,2] + M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x000;
                if (x111 > H[2] + H[3] && x111 < H[3] + H[2] + H[1])
                {
                    x000 = x111;
                }
                else
                {
                    x000 = x222;
                }
                double x1111 = (Q[4] + Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,1] + M[1,2] + M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);  //一层挡墙BC的剪力为0处的距离墙顶的距离
                double x2222 = (Q[4] - Math.Sqrt(Math.Pow(Q[4], 2) + 2 * (Q[3] - Q[4]) * (M[1,1] + M[1,2] + M[1,3] + M[1,4]) / H[3])) * H[3] / (Q[4] - Q[3]);
                double x0000;
                if (x1111 > H[3] + H[2] + H[1] && x1111 < H[3] + H[2] + H[1] + H[0])
                {
                    x0000 = x1111;
                }
                else
                {
                    x0000 = x2222;
                }
                Mmax[0] = -(Q[3] - Q[4]) * Math.Pow(x0000, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x0000, 2) + M[1,4] * x0000
                    + M[1,3] * (x0000 - H[3]) + M[1,2] * (x0000 - H[2] - H[3]) + M[1,1] * (x0000 - H[1] - H[2] - H[3]);
                Mmax[0] = -(Q[3] - Q[4]) * Math.Pow(x000, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x000, 2) + M[1,4] * x000
                    + M[1,3] * (x000 - H[3]) + M[1,2] * (x000 - H[2] - H[3]);
                Mmax[1] = -(Q[3] - Q[4]) * Math.Pow(x00, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x00, 2) + M[1,4] * x00 + M[1,3] * (x00 - H[3]);
                Mmax[2] = -(Q[3] - Q[4]) * Math.Pow(x0, 3) / (6 * H[3]) - 0.5 * Q[4] * Math.Pow(x0, 2) + M[1,4] * x0;
            }
            return Mmax;
        }

        public static Matrix neilitiaofu(Matrix M01, Matrix M02, double T)
        {
            //本子函数用于内力调幅内力组合
            //M01为neilijisuan01( K,k,f,n,Q,H )求得的墙底固结时，各节点内力
            //M02为neilijisuan02( K,k,f,n,Q,H )求得的墙底铰接时，各节点内力
            //M0为调幅后用于配筋的各节点内力，经推导，弯矩和剪力都可以用M01*T+M02*(1-T)调幅
            //T为内力调幅系数
            Matrix newM = M01 * T + M02 * (1 - T);
            return newM;
        }

        public static double[] neilizuhe(int n, Matrix Mg, double[] Mmaxg)
        {
            if (n == 1)
            {
                return new double[] { Mg[0,0], Mmaxg[0], Mg[0,1] };
            }
            else if (n == 2)
            {
                return new double[] { Mg[0,0], Mmaxg[0], Mg[0,1], Mmaxg[1], Mg[0,3] };
            }
            else if (n == 3)
            {
                return new double[] { Mg[0,0], Mmaxg[0], Mg[0,1], Mmaxg[1], Mg[0,3], Mmaxg[2], Mg[0,5] };
            }
            else if (n == 4)
            {
                return new double[] { Mg[0,0], Mmaxg[0], Mg[0,1], Mmaxg[1], Mg[0,3], Mmaxg[2], Mg[0,5], Mmaxg[3], Mg[0,7] };
            }
            return new double[] { };
        }

    }
}
