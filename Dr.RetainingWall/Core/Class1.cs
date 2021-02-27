using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dr.RetainingWall;

namespace Dr.RetainingWall.Core
{
    class Class1
    {
        public double[][] zhengjinxuanjin(int n, double[] As, double[] h, double[] M, double cs, int C, double rg)
        {
            if (n == 1) //1层挡墙
            {
                double[][] A = new double[1][];
                double AsmaxAB = As[2];                         //AB跨中计算配筋
                if (AsmaxAB > 4909 || AsmaxAB <= 0) //超出选筋库时或之前配筋计算超筋时令其配筋为0
                {
                    throw new Exception("选筋超出选筋库");
                }

                var AssAB = Zuhezhengfujin.zhengjinshuchu(AsmaxAB);           //调用函数，输出AB点的配筋，[直径 间距 实选面积]
                double[] Asss = { AssAB[1], AssAB[2], 0, 0, AssAB[3] };       //与负筋统一格式

                // AB跨中选筋
                double[] d = { Asss[1], Math.Floor(1000 / Asss[2]), 0, 0 };   //i点的配筋直径、数量
                var w = Zuhezhengfujin.liefeng1(M[1 * 2], cs, d, Asss[5], h[1], C, rg);  //计算i跨裂缝
                if (w <= 0.2)
                {
                    A[1] = new double[] { Asss[1], Asss[2], Asss[3], Asss[4], Asss[5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                }
                else
                {
                    double Asss0 = Asss[5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                    Asss[5] = Asss[5] + 1;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                    if (Asss[5] > 4909)//超出选筋库时令其配筋为0
                    {
                        throw new Exception("选筋超出选筋库");
                    }

                    for (int j = 1; j <= 2000; j++)   //嵌套循环增加配筋面积，最多增加2000mm2
                    {

                        var AA = Zuhezhengfujin.zhengjinshuchu(Asss[5]);//再次选筋
                        Asss = new double[] { AA[1], AA[2], 0, 0, AA[3] }; //将选筋结果填入Asss矩阵中
                        d = new double[] { Asss[1], Math.Floor(1000 / Asss[2]), 0, 0 };
                        w = Zuhezhengfujin.liefeng1(M[1 * 2], cs, d, Asss[5], h[1], C, rg);
                        if (w <= 0.2)
                        {
                            A[1] = new double[] { Asss[1], Asss[2], Asss[3], Asss[4], Asss[5], w };
                            break;
                        }
                        else
                        {
                            Asss[5] = Asss[5] + 1;

                            if (Asss[5] > 4909)//超出选筋库时令其配筋为0
                            {
                                throw new Exception("选筋超出选筋库");
                            }

                            if (Asss[5] - Asss0 > 1000)
                            {
                                throw new Exception("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                            }
                        }
                    }
                }
                return A;
            }
            else if (n == 2)//2层挡墙
            {
                double[][] A = new double[2][];
                double AsmaxAB = As[2];//AB跨中计算配筋
                double AsmaxBC = As[4];//BC跨中计算配筋 
                if (Math.Max(AsmaxAB, AsmaxBC) > 4909 || Math.Min(AsmaxAB, AsmaxBC) <= 0)//超出选筋库时或配筋计算超筋时令其配筋为0
                {
                    throw new Exception("选筋超出选筋库");
                }

                double[] AssAB = Zuhezhengfujin.zhengjinshuchu(AsmaxAB);//调用函数，输出AB点的配筋，[直径 间距 实选面积]
                double[] AssBC = Zuhezhengfujin.zhengjinshuchu(AsmaxBC);//调用函数，输出BC点的配筋，[直径 间距 实选面积]
                double[][] Asss = {
                    new double[]{ AssAB[1], AssAB[2], 0, 0, AssAB[3]},
                    new double[]{ AssBC[1], AssBC[2], 0, 0, AssBC[3]}
                };//与负筋统一格式[直径 间距 直径 间距 实选面积]

                //跨中选筋
                for (int i = 1; i <= 2; i++)
                {
                    double[] d = { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };       //i层配筋直径、数量
                    double w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);//计算i跨裂缝
                    if (w <= 0.2)
                    {
                        A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = Asss[i][5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        Asss[i][5] = Asss[i][5] + 1;//裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                        if (Asss[i][5] > 4909)//超出选筋库时令其配筋为0
                        {
                            throw new Exception("选筋超出选筋库");
                        }

                        for (int j = 1; j <= 2000; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            double[] AA = Zuhezhengfujin.zhengjinshuchu(Asss[i][5]);//再次选筋
                            Asss[i] = new double[] { AA[1], AA[2], 0, 0, AA[3] };//将选筋结果填入Asss矩阵中
                            d = new double[] { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };
                            w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);
                            if (w <= 0.2)
                            {
                                A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };
                                break;
                            }
                            else
                            {
                                Asss[i][5] = Asss[i][5] + 1;
                                if (Asss[i][5] > 4909)                //超出选筋库时令其配筋为0
                                {
                                    throw new Exception("选筋超出选筋库");
                                }

                                if (Asss[i][5] - Asss0 > 1000)
                                {
                                    throw new Exception("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                }
                            }
                        }
                    }
                }
                return A;
            }
            else if (n == 3)//3层挡墙
            {
                double[][] A = new double[3][];
                double AsmaxAB = As[2];//AB跨中计算配筋
                double AsmaxBC = As[4];//BC跨中计算配筋
                double AsmaxCD = As[6];//CD跨中计算配筋  

                if (Util.Max(AsmaxAB, AsmaxBC, AsmaxCD) > 4909 || Util.Min(AsmaxAB, AsmaxBC, AsmaxCD) <= 0)//超出选筋库时或配筋计算超筋时令其配筋为0
                {
                    throw new Exception("选筋超出选筋库");
                }

                double[] AssAB = Zuhezhengfujin.zhengjinshuchu(AsmaxAB);//调用函数，输出AB点的配筋，[直径 间距 实选面积]
                double[] AssBC = Zuhezhengfujin.zhengjinshuchu(AsmaxBC);//调用函数，输出BC点的配筋，[直径 间距 实选面积]
                double[] AssCD = Zuhezhengfujin.zhengjinshuchu(AsmaxCD);//调用函数，输出CD点的配筋，[直径 间距 实选面积]
                double[][] Asss ={
                    new double[]{AssAB[1],  AssAB[2],  0,  0,  AssAB[3] },
                    new double[]{AssBC[1],  AssBC[2],  0,  0,  AssBC[3] },
                    new double[]{AssCD[1],  AssCD[2],  0,  0,  AssCD[3] }
                };//与负筋统一格式[直径 间距 直径 间距 实选面积]

                //跨中选筋     
                for (int i = 1; i <= 3; i++)
                {
                    double[] d = { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };//i层配筋直径、数量
                    double w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);//计算i跨裂缝
                    if (w <= 0.2)
                    {
                        A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = Asss[i][5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        Asss[i][5] = Asss[i][5] + 1;//裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算

                        if (Asss[i][5] > 4909)//超出选筋库时令其配筋为0
                        {
                            throw new Exception("选筋超出选筋库");
                        }
                        for (int j = 1; j <= 2000; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            double[] AA = Zuhezhengfujin.zhengjinshuchu(Asss[i][5]);//再次选筋
                            Asss[i] = new double[] { AA[1], AA[2], 0, 0, AA[3] };//将选筋结果填入Asss矩阵中
                            d = new double[] { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };
                            w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);
                            if (w <= 0.2)
                            {
                                A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };
                                break;
                            }
                            else
                            {
                                Asss[i][5] = Asss[i][5] + 1;
                                if (Asss[i][5] > 4909)//超出选筋库时令其配筋为0
                                {
                                    throw new Exception("选筋超出选筋库");
                                }
                                if (Asss[i][5] - Asss0 > 1000)
                                {
                                    throw new Exception("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                }
                            }
                        }
                    }
                }
                return A;
            }
            else if (n == 4)//4层挡墙
            {
                double[][] A = new double[4][];

                double AsmaxAB = As[2];//AB跨中计算配筋
                double AsmaxBC = As[4];//BC跨中计算配筋
                double AsmaxCD = As[6];//CD跨中计算配筋
                double AsmaxDE = As[8];//DE跨中计算配筋

                if (Util.Max(AsmaxAB, AsmaxBC, AsmaxCD, AsmaxDE) > 4909 || Util.Min(AsmaxAB, AsmaxBC, AsmaxCD, AsmaxDE) <= 0)//超出选筋库时或配筋计算超筋时令其配筋为0
                {
                    throw new Exception("选筋超出选筋库");
                }

                double[] AssAB = Zuhezhengfujin.zhengjinshuchu(AsmaxAB);//调用函数，输出AB点的配筋，[直径 间距 实选面积]
                double[] AssBC = Zuhezhengfujin.zhengjinshuchu(AsmaxBC);//调用函数，输出BC点的配筋，[直径 间距 实选面积]
                double[] AssCD = Zuhezhengfujin.zhengjinshuchu(AsmaxCD);//调用函数，输出CD点的配筋，[直径 间距 实选面积]
                double[] AssDE = Zuhezhengfujin.zhengjinshuchu(AsmaxDE);//调用函数，输出CD点的配筋，[直径 间距 实选面积]
                double[][] Asss = {
                    new double[]{AssAB[1],  AssAB[2],  0,  0,  AssAB[3] },
                    new double[]{AssBC[1],  AssBC[2],  0,  0,  AssBC[3] },
                    new double[]{AssCD[1],  AssCD[2],  0,  0,  AssCD[3] },
                    new double[]{AssDE[1],  AssDE[2],  0,  0,  AssDE[3] }
                };//与负筋统一格式[直径 间距 直径 间距 实选面积]

                //跨中选筋
                for (int i = 1; i <= 4; i++)
                {
                    double[] d = { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };//i层配筋直径、数量
                    double w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);// 计算i跨裂缝
                    if (w <= 0.2)
                    {
                        A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = Asss[i][5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        Asss[i][5] = Asss[i][5] + 1;//裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                        if (Asss[i][5] > 4909)//超出选筋库时令其配筋为0
                        {
                            throw new Exception("选筋超出选筋库");
                        }
                        for (int j = 1; j < 2000; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            double[] AA = Zuhezhengfujin.zhengjinshuchu(Asss[i][5]);//再次选筋
                            Asss[i] = new double[] { AA[1], AA[2], 0, 0, AA[3] };//将选筋结果填入Asss矩阵中
                            d = new double[] { Asss[i][1], Math.Floor(1000 / Asss[i][2]), 0, 0 };
                            w = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][5], h[i], C, rg);
                            if (w <= 0.2)
                            {
                                A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };
                                break;
                            }
                            else
                            {
                                Asss[i][5] = Asss[i][5] + 1;
                                if (Asss[i][5] > 4909)//超出选筋库时令其配筋为0
                                {
                                    throw new Exception("选筋超出选筋库");
                                }

                                if (Asss[i][5] - Asss0 > 1000)
                                {
                                    throw new Exception("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                }
                            }
                        }
                    }
                }
                return A;
            }
            return new double[1][];
        }
    }
}
