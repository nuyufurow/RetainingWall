using System;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall.Core
{
    class ReinforcementCalculation
    {
        public static Vector peijinjisuan(Vector M, double cs, int n, double h, double fy, double fc, double ft)
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

            //挡墙为一层时
            if (n == 1)
            {
                double h0 = h - d / 2 - cs;                                                                    //截面有效高度 
                double xA1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[1]) / (alpha1 * fc * 1000));      //求A点受压区高度mm
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
                double As1min = 1000 * h * roumin;
                double As1 = Math.Max(As11, As1min);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xAB1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[2]) / (alpha1 * fc * 1000));     //求AB点受压区高度mm
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
                double As2min = 1000 * h * roumin;
                double As2 = Math.Max(As22, As2min);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                double xB1 = h0 - Math.Sqrt(Math.Pow(h0, 2) - 2 * Math.Abs(M[3]) / (alpha1 * fc * 1000));                 //求B点受压区高度mm
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
                double As3min = 1000 * h * roumin;
                double As3 = Math.Max(As33, As3min);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                //As ={ As1, As2, As3};
                return new Vector(new double[] { As1, As2, As3 }, VectorType.Column);
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            elseif n== 2
   h0 =[h(1) - d / 2 - cs h(2) - d / 2 - cs];                                           //各层截面有效高度
            xA1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(1)) / (alpha1 * fc * 1000));           //求A点受压区高度mm
            if xA1 >= 0 && xA1 <= kxib * h0(1)
                xA0 = xA1;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
            As1min = 1000 * h(1) * roumin;
            As1 = max(As11, As1min);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            xAB1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(2)) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
            if xAB1 >= 0 && xAB1 <= kxib * h0(1)
                xAB0 = xAB1;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
            As2min = 1000 * h(1) * roumin;
            As2 = max(As22, As2min);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            xB1l = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
            if xB1l >= 0 && xB1l <= kxib * h0(1)
                xB0l = xB1l;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
            As3lmin = 1000 * h(1) * roumin;
            As3l = max(As33l, As3lmin);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            xB1r = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
            if xB1r >= 0 && xB1r <= kxib * h0(2)
                xB0r = xB1r;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
            As3rmin = 1000 * h(2) * roumin;
            As3r = max(As33r, As3rmin);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            xBC1 = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(4)) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
            if xBC1 >= 0 && xBC1 <= kxib * h0(2)
                xBC0 = xBC1;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
            As4min = 1000 * h(2) * roumin;
            As4 = max(As44, As4min);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            xC1 = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(5)) / (alpha1 * fc * 1000));           //求C点受压区高度mm
            if xC1 >= 0 && xC1 <= kxib * h0(2)
                xC0 = xC1;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As55 = alpha1 * fc * 1000 * xC0 / fy;                                             //C点计算配筋值 
            As5min = 1000 * h(2) * roumin;
            As5 = max(As55, As5min);

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            As =[As1 As2 max(As3l, As3r) As4 As5];

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            elseif n== 3
   h0 =[h(1) - d / 2 - cs h(2) - d / 2 - cs h(3) - d / 2 - cs];                               //各层截面有效高度
            xA1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(1)) / (alpha1 * fc * 1000));           //求A点受压区高度mm
            if xA1 >= 0 && xA1 <= kxib * h0(1)
                xA0 = xA1;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
            As1min = 1000 * h(1) * roumin;
            As1 = max(As11, As1min);

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            xAB1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(2)) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
            if xAB1 >= 0 && xAB1 <= kxib * h0(1)
                xAB0 = xAB1;
            else
                disp('超筋，请修改1层挡墙参数!!!!');
            return
        end
   As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
            As2min = 1000 * h(1) * roumin;
            As2 = max(As22, As2min);

            //////////////////////////////////////////////////////////////////////////////////////////////////
            xB1l = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
            if xB1l >= 0 && xB1l <= kxib * h0(1)
                xB0l = xB1l;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
            As3lmin = 1000 * h(1) * roumin;
            As3l = max(As33l, As3lmin);

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            xB1r = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
            if xB1r >= 0 && xB1r <= kxib * h0(2)
                xB0r = xB1r;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
            As3rmin = 1000 * h(2) * roumin;
            As3r = max(As33r, As3rmin);

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            xBC1 = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(4)) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
            if xBC1 >= 0 && xBC1 <= kxib * h0(2)
                xBC0 = xBC1;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
            As4min = 1000 * h(2) * roumin;
            As4 = max(As44, As4min);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            xC1l = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(5)) / (alpha1 * fc * 1000));          //求C左侧点受压区高度mm
            if xC1l >= 0 && xC1l <= kxib * h0(2)
                xC0l = xC1l;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As55l = alpha1 * fc * 1000 * xC0l / fy;                                           //C点左侧计算配筋值 
            As5lmin = 1000 * h(2) * roumin;
            As5l = max(As55l, As5lmin);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            xC1r = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(5)) / (alpha1 * fc * 1000));          //求C右侧点受压区高度mm
            if xC1r >= 0 && xC1r <= kxib * h0(3)
                xC0r = xC1r;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As55r = alpha1 * fc * 1000 * xC0r / fy;                                           //C点右侧计算配筋值 
            As5rmin = 1000 * h(3) * roumin;
            As5r = max(As55r, As5rmin);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            xCD1 = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(6)) / (alpha1 * fc * 1000));          //求CD点受压区高度mm
            if xCD1 >= 0 && xCD1 <= kxib * h0(3)
                xCD0 = xCD1;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As66 = alpha1 * fc * 1000 * xCD0 / fy;                                            //CD点计算配筋值 
            As6min = 1000 * h(3) * roumin;
            As6 = max(As66, As6min);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            xD1 = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(7)) / (alpha1 * fc * 1000));           //求D点受压区高度mm
            if xD1 >= 0 && xD1 <= kxib * h0(3)
                xD0 = xD1;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As77 = alpha1 * fc * 1000 * xD0 / fy;                                             //D点计算配筋值 
            As7min = 1000 * h(3) * roumin;
            As7 = max(As77, As7min);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            As =[As1 As2 max(As3l, As3r) As4 max(As5l, As5r) As6 As7];

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            elseif n== 4
   h0 =[h(1) - d / 2 - cs h(2) - d / 2 - cs h(3) - d / 2 - cs];                               //各层截面有效高度
            xA1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(1)) / (alpha1 * fc * 1000));           //求A点受压区高度mm
            if xA1 >= 0 && xA1 <= kxib * h0(1)
                xA0 = xA1;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As11 = alpha1 * fc * 1000 * xA0 / fy;                                             //A点计算配筋值
            As1min = 1000 * h(1) * roumin;
            As1 = max(As11, As1min);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            xAB1 = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(2)) / (alpha1 * fc * 1000));          //求AB点受压区高度mm
            if xAB1 >= 0 && xAB1 <= kxib * h0(1)
                xAB0 = xAB1;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As22 = alpha1 * fc * 1000 * xAB0 / fy;                                            //AB点计算配筋值
            As2min = 1000 * h(1) * roumin;
            As2 = max(As22, As2min);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            xB1l = h0(1) - sqrt(h0(1) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点左侧受压区高度mm
            if xB1l >= 0 && xB1l <= kxib * h0(1)
                xB0l = xB1l;
            else
                disp('超筋，请修改1层挡墙参数!!!');
            return
        end
   As33l = alpha1 * fc * 1000 * xB0l / fy;                                           //B点左侧计算配筋值
            As3lmin = 1000 * h(1) * roumin;
            As3l = max(As33l, As3lmin);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            xB1r = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(3)) / (alpha1 * fc * 1000));          //求B点右侧受压区高度mm
            if xB1r >= 0 && xB1r <= kxib * h0(2)
                xB0r = xB1r;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As33r = alpha1 * fc * 1000 * xB0r / fy;                                           //B点右侧计算配筋值
            As3rmin = 1000 * h(2) * roumin;
            As3r = max(As33r, As3rmin);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            xBC1 = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(4)) / (alpha1 * fc * 1000));          //求BC点受压区高度mm
            if xBC1 >= 0 && xBC1 <= kxib * h0(2)
                xBC0 = xBC1;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As44 = alpha1 * fc * 1000 * xBC0 / fy;                                            //BC点计算配筋值
            As4min = 1000 * h(2) * roumin;
            As4 = max(As44, As4min);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            xC1l = h0(2) - sqrt(h0(2) ^ 2 - 2 * abs(M(5)) / (alpha1 * fc * 1000));          //求C左侧点受压区高度mm
            if xC1l >= 0 && xC1l <= kxib * h0(2)
                xC0l = xC1l;
            else
                disp('超筋，请修改2层挡墙参数!!!');
            return
        end
   As55l = alpha1 * fc * 1000 * xC0l / fy;                                           //C点左侧计算配筋值 
            As5lmin = 1000 * h(2) * roumin;
            As5l = max(As55l, As5lmin);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            xC1r = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(5)) / (alpha1 * fc * 1000));          //求C右侧点受压区高度mm
            if xC1r >= 0 && xC1r <= kxib * h0(3)
                xC0r = xC1r;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As55r = alpha1 * fc * 1000 * xC0r / fy;                                           //C点右侧计算配筋值 
            As5rmin = 1000 * h(3) * roumin;
            As5r = max(As55r, As5rmin);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            xCD1 = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(6)) / (alpha1 * fc * 1000));          //求CD点受压区高度mm
            if xCD1 >= 0 && xCD1 <= kxib * h0(3)
                xCD0 = xCD1;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As66 = alpha1 * fc * 1000 * xCD0 / fy;                                            //CD点计算配筋值 
            As6min = 1000 * h(3) * roumin;
            As6 = max(As66, As6min);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            xD1l = h0(3) - sqrt(h0(3) ^ 2 - 2 * abs(M(7)) / (alpha1 * fc * 1000));          //求D点左侧受压区高度mm
            if xD1l >= 0 && xD1l <= kxib * h0(3)
                xD0l = xD1l;
            else
                disp('超筋，请修改3层挡墙参数!!!');
            return
        end
   As77l = alpha1 * fc * 1000 * xD0l / fy;                                           //D点左侧计算配筋值
            As7lmin = 1000 * h(3) * roumin;
            As7l = max(As77l, As7lmin);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            xD1r = h0(4) - sqrt(h0(4) ^ 2 - 2 * abs(M(7)) / (alpha1 * fc * 1000));          //求D点右侧受压区高度mm
            if xD1r >= 0 && xD1r <= kxib * h0(4)
                xD0r = xD1r;
            else
                disp('超筋，请修改4层挡墙参数!!!');
            return
        end
   As77r = alpha1 * fc * 1000 * xD0r / fy;                                           //D点右侧计算配筋值
            As7rmin = 1000 * h(4) * roumin;
            As7r = max(As77r, As7rmin);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            xDE1 = h0(4) - sqrt(h0(4) ^ 2 - 2 * abs(M(8)) / (alpha1 * fc * 1000));          //求DE点受压区高度mm
            if xDE1 >= 0 && xDE1 <= kxib * h0(4)
                xDE0 = xDE1;
            else
                disp('超筋，请修改4层挡墙参数!!!');
            return
        end
   As88 = alpha1 * fc * 1000 * xDE0 / fy;                                            //DE点计算配筋值
            As8min = 1000 * h(4) * roumin;
            As8 = max(As88, As8min);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            xE1 = h0(4) - sqrt(h0(4) ^ 2 - 2 * abs(M(9)) / (alpha1 * fc * 1000));           //求E点受压区高度mm
            if xE1 >= 0 && xE1 <= kxib * h0(4)
                xE0 = xE1;
            else
                disp('超筋，请修改4层挡墙参数!!!');
            return
        end
   As99 = alpha1 * fc * 1000 * xE0 / fy;                                             //E点计算配筋值 
            As9min = 1000 * h(4) * roumin;
            As9 = max(As99, As9min);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            As =[As1 As2 max(As3l, As3r) As4 max(As5l, As5r) As6 max(As7l, As7r) As8 As9];


        }


    }
}
