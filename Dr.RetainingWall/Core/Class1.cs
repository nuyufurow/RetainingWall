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

        public static double[][] fujinxuanjin150(int n, double[] As, double ft, double fy, double[] h, double[] M, double cs, int C, double rg)
        {
            //本子函数用于负筋自动选筋，按照此子程序计算得到的配筋为最节省配筋
            //本子函数即根据裂缝选筋liefeng1(M, cs, d, As, h, C, rg)
            //n为挡墙层数
            //As为peijinjisuan(M, cs, n, fy, fc, ft)中计算得到各点的计算配筋
            //As =[A      AB      B       BC       C        CD        D       DE       E]
            //As(1)   As(2)   As(3)   As(4)   As(5)     As(6)     As(7)    As(8)   As(9)
            //受力钢筋最小直径为12mm，间距最大为150mm
            double roumin = Math.Max(0.002, 0.45 * ft / (100 * fy));        //最小配筋率
            if (n == 1)//1层挡墙
            {
                double[][] A = new double[1][];
                double AsmaxA = As[1];//A点计算配筋
                if (AsmaxA > 6544 || AsmaxA <= 0)
                {
                    throw new Exception("A点选筋超出选筋库");
                }

                double Astong = roumin * h[1] * 1000;//另通长筋 = 构造钢筋
                double[] AssA = Zuhezhengfujin.fujinshuchu(150, AsmaxA, Astong);//调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
                double[] Asss = { AssA[1], AssA[2], AssA[3], AssA[4], AssA[5] };//B点配筋为负筋通长筋,裂缝不用验算

                //A点的选筋
                double[] d = { Asss[1], Math.Floor(1000 / Asss[2]), Asss[3], Math.Floor(1000 / Asss[4]) };//A点的配筋直径、数量
                double w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[5], h[1], C, rg);//计算A点裂缝

                double[] A1 = new double[6];
                if (w <= 0.2)
                {
                    A1 = new double[] { Asss[1], Asss[2], Asss[3], Asss[4], Asss[5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                }
                else
                {
                    double Asss0 = Asss[5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                    Asss[5] = Asss[5] + 10;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算

                    if (Asss[5] > 6544)
                    {
                        throw new Exception("A点选筋超出选筋库");
                    }

                    for (int j = 1; j <= 200; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                    {
                        double[] AA = Zuhezhengfujin.fujinshuchu(150, Asss[5], Astong);//再次选筋

                        Asss = new double[] { AA[1], AA[2], AA[3], AA[4], AA[5] };//将选筋结果填入Asss矩阵中
                        d = new double[] { Asss[1], Math.Floor(1000 / Asss[2]), Asss[3], Math.Floor(1000 / Asss[4]) };
                        w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[5], h[1], C, rg);
                        if (w <= 0.2)
                        {
                            A1 = new double[] { Asss[1], Asss[2], Asss[3], Asss[4], Asss[5], w };//计算得到A点配筋和裂缝宽度
                            break;
                        }
                        else
                        {
                            Asss[5] = Asss[5] + 10;
                            if (Asss[5] > 6544)
                            {
                                throw new Exception("A点选筋超出选筋库");
                            }

                            if (Asss[5] - Asss0 > 1000)
                            {
                                throw new Exception("A点按裂缝选筋增加钢筋过大，请修改挡墙参数");
                            }
                        }
                    }
                }
                A = new double[][]{
                    new double[] { A1[1], A1[2], A1[3], A1[4], A1[5], A1[6] },
                    new double[] { A1[1], A1[2], 0, 0, Math.PI * Math.Pow(A1[2], 2) * 1000 / (4 * A1[2]), 0 }};
                return A;
            }
            else if (n == 2)
            {
                double[][] A = new double[2][];
                double AsmaxA = As[1];//A点计算配筋
                double AsmaxB = As[3];//B点计算配筋

                if (Math.Max(AsmaxA, AsmaxB) > 6544 || Math.Min(AsmaxA, AsmaxB) <= 0)
                {
                    throw new Exception("选筋超出选筋库");
                }

                double Astong = Math.Max(roumin * h[1] * 1000, roumin * h[2] * 1000);//1层通长筋 = 构造钢筋，AB两点都用此值
                double[] AssA = Zuhezhengfujin.fujinshuchu(150, AsmaxA, Astong);//调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
                double[] AssB = Zuhezhengfujin.fujinshuchu(150, AsmaxB, Astong);//调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
                double[][] Asss = new double[][]{
                    new double[] {AssA[1], AssA[2], AssA[3], AssA[4], AssA[5] },
                    new double[] {AssB[1], AssB[2], AssB[3], AssB[4], AssB[5]}};
                //C点配筋为负筋通长筋,裂缝不用验算

                //A点配筋
                for (int i = 1; i <= 2; i++)
                {
                    double[] d = new double[] { Asss[i][1], Math.Floor(1000 / Asss[i][2]), Asss[i][3], Math.Floor(1000 / Asss[i][4]) };//A点的配筋直径、数量
                    double w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[i][5], h[1], C, rg);//计算A点裂缝
                    if (w <= 0.2)
                    {
                        A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = Asss[i][5];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        Asss[i][5] = Asss[i][5] + 10;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                        if (Asss[i][5] > 6544)
                        {
                            throw new Exception("选筋超出选筋库");
                        }

                        for (int j = 1; j <= 200; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            double[] AA = Zuhezhengfujin.fujinshuchu(150, Asss[i][5], Astong);//再次选筋
                            Asss[i] = new double[] { AA[1], AA[2], AA[3], AA[4], AA[5] };//将选筋结果填入Asss矩阵中
                            d = new double[] { Asss[i][1], Math.Floor(1000 / Asss[i][2]), Asss[i][3], Math.Floor(1000 / Asss[i][4]) };
                            w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[i][5], h[1], C, rg);
                            if (w <= 0.2)
                            {
                                A[i] = new double[] { Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], Asss[i][5], w };//计算得到A点配筋和裂缝宽度
                                break;
                            }
                            else
                            {
                                Asss[i][5] = Asss[i][5] + 10;
                                if (Asss[i][5] > 6544)
                                {
                                    throw new Exception("选筋超出选筋库");
                                }

                                if (Asss[i][5] - Asss0 > 1000)
                                {
                                    throw new Exception("A点按裂缝选筋增加钢筋过大，请修改挡墙参数");
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

