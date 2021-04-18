using System;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dr.RetainingWall
{
    class ReinforcementCalculation
    {
        private double RebarArea(double h0, double M, double alpha1, double fc, double kxib, double fy, double roumin, double h)
        {
            double x = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M) / (alpha1 * fc * 1000));     //求AB点受压区高度mm
            double xAB0;
            if (x >= 0 && x <= kxib * h0)
            {
                xAB0 = x;
            }
            else
            {
                throw new Exception("超筋，请修改对应挡墙参数!!!");
            }
            double As = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
            double AsMin = 1000 * h * roumin;
            double As2 = Math.Max(As, AsMin);
            return As2;
        }

        public static List<double>[] peijinjisuan(double[] M, double cs, int n, double[] h, double fy, double fc, double ft)
        {
            //此子程序为配筋计算程序,输出计算所需钢筋面积
            // M为各点内力值[A        AB        B          BC          C         CD           D          DE          E   ]
            //            A点弯矩  AB跨中弯矩  B点弯矩   BC跨中弯矩   C点弯矩   CD跨中弯矩    D点弯矩    DE跨中弯矩   E点弯矩
            //cs为混凝土保护层厚度
            //配筋计算时均不考虑压筋的贡献！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //混凝土强度等级不大于C50
            //roumin为受弯构件单侧最小配筋率
            //每层的ft、fc均一样，不考虑各层砼和钢筋等级不同
            List<double>[] arrResult = { new List<double>(), new List<double>(), new List<double>(), new List<double>() };

            double beta1 = 0.8;
            double alpha1 = 1.0;
            double epsiloncu = 0.0033;
            double Es = 200000;
            double d = 16;                                                                      //假设钢筋直径为16mm
            double kxib = beta1 / (1 + fy / (Es * epsiloncu));                                  //界限相对受压区高度
            double roumin = Math.Max(0.002, 45 * ft / fy / 100);                                //单侧最小配筋率，挡墙配筋需同时满足受弯构件和剪力墙的构造要求

            //                                          E|==|
            //                                           |  |
            //                                           |  |
            //                                           |  |
            //                             D|==|        D|==|
            //                              |  |         |  |
            //                              |  |         |  |
            //                              |  |         |  |
            //                C|==|        C|==|        C|==|
            //                 |  |         |  |         |  |
            //                 |  |         |  |         |  |
            //                 |  |         |  |         |  |
            //    B|==|       B|==|        B|==|        B|==|
            //     |  |        |  |         |  |         |  |
            //     |  |        |  |         |  |         |  |
            //     |  |        |  |         |  |         |  |
            //    A|==|       A|==|        A|==|        A|==|
            //     一层        二层         三层         四层

            //挡墙为一层时
            if (n == 1)
            {
                double h0 = h[0] - d / 2 - cs;                                                                    //截面有效高度 
                double xA1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[0]) / (alpha1 * fc * 1000));      //求A点受压区高度mm
                double xA0;
                if (xA1 >= 0 && xA1 <= kxib * h0)
                {
                    xA0 = xA1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As11 = alpha1 * fc * 1000 * xA0 / fy;                                                 //A点计算配筋值
                double As1min = 1000 * h[0] * roumin;
                double As1 = Math.Max(As11, As1min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xAB1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));     //求AB点受压区高度mm
                double xAB0;
                if (xAB1 >= 0 && xAB1 <= kxib * h0)
                {
                    xAB0 = xAB1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
                double As2min = 1000 * h[0] * roumin;
                double As2 = Math.Max(As22, As2min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));                 //求B点受压区高度mm
                double xB0;
                if (xB1 >= 0 && xB1 <= kxib * h0)
                {
                    xB0 = xB1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As33 = alpha1 * fc * 1000 * xB0 / fy;                                             //B点计算配筋值 
                double As3min = 1000 * h[0] * roumin;
                double As3 = Math.Max(As33, As3min);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                //As ={ As1, As2, As3};

                arrResult = new List<double>[] {
                    new List<double>(new double[] {As1   , As2    , As3 }),
                    new List<double>(new double[] {As1   , As2    , As3 }),
                    new List<double>(new double[] {xA1   , xAB1   , 0   }),
                    new List<double>(new double[] {xA1/h0, xAB1/h0, kxib }),
                };
            }
            else if (n == 2)
            {
                double[] h0 = { h[0] - d / 2 - cs, h[1] - d / 2 - cs };                                   //各层截面有效高度
                double xA1 = h0[0] - Math.Sqrt(Math.Pow(h0[0], 2) - 2 * Math.Abs(M[0]) / (alpha1 * fc * 1000));           //求A点受压区高度mm
                double xA0;
                if (xA1 >= 0 && xA1 <= kxib * h0[0])
                {
                    xA0 = xA1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
                double As1min = 1000 * h[0] * roumin;
                double As1 = Math.Max(As11, As1min);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xABl = h0[0] - Math.Sqrt(Math.Pow(h0[0], 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
                double xAB0;
                if (xABl >= 0 && xABl <= kxib * h0[0])
                {
                    xAB0 = xABl;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
                double As2min = 1000 * h[0] * roumin;
                double As2 = Math.Max(As22, As2min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1l = h0[0] - Math.Sqrt(Math.Pow(h0[0], 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
                double xB0l;
                if (xB1l >= 0 && xB1l <= kxib * h0[0])
                {
                    xB0l = xB1l;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
                double As3lmin = 1000 * h[0] * roumin;
                double As3l = Math.Max(As33l, As3lmin);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1r = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
                double xB0r;
                if (xB1r >= 0 && xB1r <= kxib * h0[1])
                {
                    xB0r = xB1r;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
                double As3rmin = 1000 * h[1] * roumin;
                double As3r = Math.Max(As33r, As3rmin);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xBC1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
                double xBC0;
                if (xBC1 >= 0 && xBC1 <= kxib * h0[1])
                {
                    xBC0 = xBC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
                double As4min = 1000 * h[1] * roumin;
                double As4 = Math.Max(As44, As4min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[4]) / (alpha1 * fc * 1000));           //求C点受压区高度mm
                double xC0;
                if (xC1 >= 0 && xC1 <= kxib * h0[1])
                {
                    xC0 = xC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As55 = alpha1 * fc * 1000 * xC0 / fy;                                             //C点计算配筋值 
                double As5min = 1000 * h[1] * roumin;
                double As5 = Math.Max(As55, As5min);

                /////////////////////////////////////////////////////////////////////////////////////////////////////
                arrResult = new List<double>[] {
                    new List<double>(new double[] {As1      , As2       , Math.Max(As3l, As3r), As4       , As5       , 0   }),
                    new List<double>(new double[] {As1      , As2       , As3l                , As3r      , As4       , As5 }),
                    new List<double>(new double[] {xA1      , xABl      , xB1l                , xB1r      , xBC1      , 0   }),
                    new List<double>(new double[] {xA1/h0[0], xABl/h0[0], xB1l/h0[0]          , xB1r/h0[1], xBC1/h0[1], kxib}),
                };
            }
            else if (n == 3 || n == 4)
            {

                for (int i = 0; i < n; i++)
                {
                    double h0 = h[i] - d / 2 - cs;
                    for (int j = 0; j < 3; j++)
                    {
                        double x = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[2 * i + j]) / (alpha1 * fc * 1000));
                        if (x < 0 || x > kxib * h0)
                        {
                            //MessageBox.Show("超筋", "请修改" + i + "层挡墙参数！");
                            Util.ShowWarning("超筋, 请修改" + i + "层挡墙参数");
                        }
                        double varAs = alpha1 * fc * 1000 * x / fy;
                        double varAsMin = 1000 * h[i] * roumin;
                        arrResult[1].Add(Math.Max(varAs, varAsMin));
                        arrResult[2].Add(x);
                        arrResult[3].Add(x / h0);
                    }
                }

                arrResult[2][n * 3 - 1] = 0;
                arrResult[3][n * 3 - 1] = kxib;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i != n - 1 && j == 2)
                        {
                            continue;
                        }
                        if (i != 0 && j == 0)
                        {
                            arrResult[0].Add(Math.Max(arrResult[1][3 * i + j - 1], arrResult[1][3 * i + j]));
                        }
                        else
                        {
                            arrResult[0].Add(arrResult[1][3 * i + j]);
                        }
                    }
                }

                for (int i = 0; i < n - 1; i++)
                {
                    arrResult[0].Add(0);
                }

            }
            return arrResult;
        }


    }
}
