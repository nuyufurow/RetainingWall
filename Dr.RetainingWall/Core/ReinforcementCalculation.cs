using System;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

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

        public static double[] peijinjisuan(double[] M, double cs, int n, double[] h, double fy, double fc, double ft)
        {
            //此子程序为配筋计算程序,输出计算所需钢筋面积
            // M为各点内力值[A        AB        B          BC          C         CD           D          DE          E   ]
            //            A点弯矩  AB跨中弯矩  B点弯矩   BC跨中弯矩   C点弯矩   CD跨中弯矩    D点弯矩    DE跨中弯矩   E点弯矩
            //cs为混凝土保护层厚度
            //配筋计算时均不考虑压筋的贡献！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //混凝土强度等级不大于C50
            //roumin为受弯构件单侧最小配筋率
            //每层的ft、fc均一样，不考虑各层砼和钢筋等级不同
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
                return new double[] { As1, As2, As3 };
            }
            else if (n == 2)
            {
                double[] h0 = { h[1] - d / 2 - cs, h[2] - d / 2 - cs };                                   //各层截面有效高度
                double xA1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));           //求A点受压区高度mm
                double xA0;
                if (xA1 >= 0 && xA1 <= kxib * h0[1])
                {
                    xA0 = xA1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
                double As1min = 1000 * h[1] * roumin;
                double As1 = Math.Max(As11, As1min);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xAB1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
                double xAB0;
                if (xAB1 >= 0 && xAB1 <= kxib * h0[1])
                {
                    xAB0 = xAB1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
                double As2min = 1000 * h[1] * roumin;
                double As2 = Math.Max(As22, As2min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1l = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
                double xB0l;
                if (xB1l >= 0 && xB1l <= kxib * h0[1])
                {
                    xB0l = xB1l;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
                double As3lmin = 1000 * h[1] * roumin;
                double As3l = Math.Max(As33l, As3lmin);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1r = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
                double xB0r;
                if (xB1r >= 0 && xB1r <= kxib * h0[2])
                {
                    xB0r = xB1r;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
                double As3rmin = 1000 * h[2] * roumin;
                double As3r = Math.Max(As33r, As3rmin);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xBC1 = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[4]) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
                double xBC0;
                if (xBC1 >= 0 && xBC1 <= kxib * h0[2])
                {
                    xBC0 = xBC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
                double As4min = 1000 * h[2] * roumin;
                double As4 = Math.Max(As44, As4min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1 = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[5]) / (alpha1 * fc * 1000));           //求C点受压区高度mm
                double xC0;
                if (xC1 >= 0 && xC1 <= kxib * h0[2])
                {
                    xC0 = xC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As55 = alpha1 * fc * 1000 * xC0 / fy;                                             //C点计算配筋值 
                double As5min = 1000 * h[2] * roumin;
                double As5 = Math.Max(As55, As5min);

                /////////////////////////////////////////////////////////////////////////////////////////////////////
                return new double[] { As1, As2, Math.Max(As3l, As3r), As4, As5 };
            }
            else if (n == 3)
            {
                double[] h0 = { h[1] - d / 2 - cs, h[2] - d / 2 - cs, h[3] - d / 2 - cs };                               //各层截面有效高度
                double xA1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));           //求A点受压区高度mm
                double xA0;
                if (xA1 >= 0 && xA1 <= kxib * h0[1])
                {
                    xA0 = xA1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
                double As1min = 1000 * h[1] * roumin;
                double As1 = Math.Max(As11, As1min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                double xAB1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
                double xAB0;
                if (xAB1 >= 0 && xAB1 <= kxib * h0[1])
                {
                    xAB0 = xAB1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
                double As2min = 1000 * h[1] * roumin;
                double As2 = Math.Max(As22, As2min);

                //////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1l = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
                double xB0l;
                if (xB1l >= 0 && xB1l <= kxib * h0[1])
                {
                    xB0l = xB1l;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
                double As3lmin = 1000 * h[1] * roumin;
                double As3l = Math.Max(As33l, As3lmin);

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1r = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
                double xB0r;
                if (xB1r >= 0 && xB1r <= kxib * h0[2])
                {
                    xB0r = xB1r;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
                double As3rmin = 1000 * h[2] * roumin;
                double As3r = Math.Max(As33r, As3rmin);

                /////////////////////////////////////////////////////////////////////////////////////////////////////
                double xBC1 = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[4]) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
                double xBC0;
                if (xBC1 >= 0 && xBC1 <= kxib * h0[2])
                {
                    xBC0 = xBC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
                double As4min = 1000 * h[2] * roumin;
                double As4 = Math.Max(As44, As4min);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1l = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[5]) / (alpha1 * fc * 1000));          //求C左侧点受压区高度mm
                double xC0l;
                if (xC1l >= 0 && xC1l <= kxib * h0[2])
                {
                    xC0l = xC1l;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As55l = alpha1 * fc * 1000 * xC0l / fy;                                           //C点左侧计算配筋值 
                double As5lmin = 1000 * h[2] * roumin;
                double As5l = Math.Max(As55l, As5lmin);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1r = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[5]) / (alpha1 * fc * 1000));          //求C右侧点受压区高度mm
                double xC0r;
                if (xC1r >= 0 && xC1r <= kxib * h0[3])
                {
                    xC0r = xC1r;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As55r = alpha1 * fc * 1000 * xC0r / fy;                                           //C点右侧计算配筋值 
                double As5rmin = 1000 * h[3] * roumin;
                double As5r = Math.Max(As55r, As5rmin);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xCD1 = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[6]) / (alpha1 * fc * 1000));          //求CD点受压区高度mm
                double xCD0;
                if (xCD1 >= 0 && xCD1 <= kxib * h0[3])
                {
                    xCD0 = xCD1;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As66 = alpha1 * fc * 1000 * xCD0 / fy;                                            //CD点计算配筋值 
                double As6min = 1000 * h[3] * roumin;
                double As6 = Math.Max(As66, As6min);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                double xD1 = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[7]) / (alpha1 * fc * 1000));           //求D点受压区高度mm
                double xD0;
                if (xD1 >= 0 && xD1 <= kxib * h0[3])
                {
                    xD0 = xD1;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As77 = alpha1 * fc * 1000 * xD0 / fy;                                             //D点计算配筋值 
                double As7min = 1000 * h[3] * roumin;
                double As7 = Math.Max(As77, As7min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                return new double[] { As1, As2, Math.Max(As3l, As3r), As4, Math.Max(As5l, As5r), As6, As7 };
            }
            else if (n == 4)
            {
                double[] h0 = { h[1] - d / 2 - cs, h[2] - d / 2 - cs, h[3] - d / 2 - cs };                               //各层截面有效高度
                double xA1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));           //求A点受压区高度mm
                double xA0;
                if (xA1 >= 0 && xA1 <= kxib * h0[1])
                {
                    xA0 = xA1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
                double As1min = 1000 * h[1] * roumin;
                double As1 = Math.Max(As11, As1min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xAB1 = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
                double xAB0;
                if (xAB1 >= 0 && xAB1 <= kxib * h0[1])
                {
                    xAB0 = xAB1;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
                double As2min = 1000 * h[1] * roumin;
                double As2 = Math.Max(As22, As2min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1l = h0[1] - Math.Sqrt(Math.Pow(h0[1], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
                double xB0l;
                if (xB1l >= 0 && xB1l <= kxib * h0[1])
                {
                    xB0l = xB1l;
                }
                else
                {
                    throw new Exception("超筋，请修改1层挡墙参数!!!");
                }
                double As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
                double As3lmin = 1000 * h[1] * roumin;
                double As3l = Math.Max(As33l, As3lmin);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1r = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
                double xB0r;
                if (xB1r >= 0 && xB1r <= kxib * h0[2])
                {
                    xB0r = xB1r;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
                double As3rmin = 1000 * h[2] * roumin;
                double As3r = Math.Max(As33r, As3rmin);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xBC1 = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[4]) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
                double xBC0;
                if (xBC1 >= 0 && xBC1 <= kxib * h0[2])
                {
                    xBC0 = xBC1;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
                double As4min = 1000 * h[2] * roumin;
                double As4 = Math.Max(As44, As4min);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1l = h0[2] - Math.Sqrt(Math.Pow(h0[2], 2) - 2 * Math.Abs(M[5]) / (alpha1 * fc * 1000));          //求C左侧点受压区高度mm
                double xC0l;
                if (xC1l >= 0 && xC1l <= kxib * h0[2])
                {
                    xC0l = xC1l;
                }
                else
                {
                    throw new Exception("超筋，请修改2层挡墙参数!!!");
                }
                double As55l = alpha1 * fc * 1000 * xC0l / fy;                                           //C点左侧计算配筋值 
                double As5lmin = 1000 * h[2] * roumin;
                double As5l = Math.Max(As55l, As5lmin);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xC1r = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[5]) / (alpha1 * fc * 1000));          //求C右侧点受压区高度mm
                double xC0r;
                if (xC1r >= 0 && xC1r <= kxib * h0[3])
                {
                    xC0r = xC1r;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As55r = alpha1 * fc * 1000 * xC0r / fy;                                           //C点右侧计算配筋值 
                double As5rmin = 1000 * h[3] * roumin;
                double As5r = Math.Max(As55r, As5rmin);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xCD1 = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[6]) / (alpha1 * fc * 1000));          //求CD点受压区高度mm
                double xCD0;
                if (xCD1 >= 0 && xCD1 <= kxib * h0[3])
                {
                    xCD0 = xCD1;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As66 = alpha1 * fc * 1000 * xCD0 / fy;                                            //CD点计算配筋值 
                double As6min = 1000 * h[3] * roumin;
                double As6 = Math.Max(As66, As6min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xD1l = h0[3] - Math.Sqrt(Math.Pow(h0[3], 2) - 2 * Math.Abs(M[7]) / (alpha1 * fc * 1000));          //求D点左侧受压区高度mm
                double xD0l;
                if (xD1l >= 0 && xD1l <= kxib * h0[3])
                {
                    xD0l = xD1l;
                }
                else
                {
                    throw new Exception("超筋，请修改3层挡墙参数!!!");
                }
                double As77l = alpha1 * fc * 1000 * xD0l / fy;                                           //D点左侧计算配筋值
                double As7lmin = 1000 * h[3] * roumin;
                double As7l = Math.Max(As77l, As7lmin);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xD1r = h0[4] - Math.Sqrt(Math.Pow(h0[4], 2) - 2 * Math.Abs(M[7]) / (alpha1 * fc * 1000));          //求D点右侧受压区高度mm
                double xD0r;
                if (xD1r >= 0 && xD1r <= kxib * h0[4])
                {
                    xD0r = xD1r;
                }
                else
                {
                    throw new Exception("超筋，请修改4层挡墙参数!!!");
                }
                double As77r = alpha1 * fc * 1000 * xD0r / fy;                                           //D点右侧计算配筋值
                double As7rmin = 1000 * h[4] * roumin;
                double As7r = Math.Max(As77r, As7rmin);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xDE1 = h0[4] - Math.Sqrt(Math.Pow(h0[4], 2) - 2 * Math.Abs(M[8]) / (alpha1 * fc * 1000));          //求DE点受压区高度mm
                double xDE0;
                if (xDE1 >= 0 && xDE1 <= kxib * h0[4])
                {
                    xDE0 = xDE1;
                }
                else
                {
                    throw new Exception("超筋，请修改4层挡墙参数!!!");
                }
                double As88 = alpha1 * fc * 1000 * xDE0 / fy;                                            //DE点计算配筋值
                double As8min = 1000 * h[4] * roumin;
                double As8 = Math.Max(As88, As8min);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xE1 = h0[4] - Math.Sqrt(Math.Pow(h0[4], 2) - 2 * Math.Abs(M[9]) / (alpha1 * fc * 1000));           //求E点受压区高度mm
                double xE0;
                if (xE1 >= 0 && xE1 <= kxib * h0[4])
                {
                    xE0 = xE1;
                }
                else
                {
                    throw new Exception("超筋，请修改4层挡墙参数!!!");
                }
                double As99 = alpha1 * fc * 1000 * xE0 / fy;                                             //E点计算配筋值 
                double As9min = 1000 * h[4] * roumin;
                double As9 = Math.Max(As99, As9min);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                return new double[]{ As1, As2, Math.Max(As3l, As3r), As4, Math.Max(As5l, As5r), As6, Math.Max(As7l, As7r), As8, As9};

            }
            return new double[] { };
        }


    }
}
