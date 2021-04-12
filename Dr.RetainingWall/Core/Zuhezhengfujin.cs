using LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dr.RetainingWall
{
    class Zuhezhengfujin
    {
        public static double[] zhengjinshuchu(double space, double Asmax)
        {
            double[] diameterOfRebar = { 12, 14, 16, 18, 20, 25 };
            double[] rebarData = new double[3];
            foreach (double d in diameterOfRebar)
            {
                double a = Math.PI * Math.Pow(d, 2) / 4 * 1000 / space;
                if (Asmax < a)
                {
                    rebarData = new double[] { d, space, a };
                    break;
                }
            }
            if (rebarData[2] == 0)
            {
                throw new Exception("超出选筋库!");
            }
            return rebarData;
        }

        public static double[] fujinshuchu(int sd, double Asmax, double Astong)
        {
            double[] rebarData = { 0, 0, 0, 0, 0 };
            double delta = 1000;
            double[] diameterOfRebar = { 12, 14, 16, 18, 20, 22, 25 };
            double[,] indexs = {
                { 1, 1, 1, 0, 0, 0, 0},
                { 1, 1, 1, 1, 0, 0, 0},
                { 1, 1, 1, 1, 1, 0, 0},
                { 0, 1, 1, 1, 1, 1, 0},
                { 0, 0, 1, 1, 1, 1, 1},
                { 0, 0, 0, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1, 1, 1}
            };

            for (int i = 0; i < 7; i++)
            {
                double Ast = Math.Pow(diameterOfRebar[i], 2) * Math.PI / 4 * 1000 / sd;
                if (Astong < Ast)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (indexs[i, j] == 1)
                        {
                            double Asm1 = Ast + Math.Pow(diameterOfRebar[j], 2) * Math.PI / 4 * 1000 / (2 * sd);
                            double Asm2 = Ast + Math.Pow(diameterOfRebar[j], 2) * Math.PI / 4 * 1000 / sd;
                            if (Asmax < Asm1 && Asm1 - Asmax < delta)
                            {
                                delta = Asm1 - Asmax;
                                rebarData = new double[] { diameterOfRebar[i], sd, diameterOfRebar[j], 2 * sd, Asm1 };
                            }
                            if (Asmax < Asm2 && Asm2 - Asmax < delta)
                            {
                                delta = Asm2 - Asmax;
                                rebarData = new double[] { diameterOfRebar[i], sd, diameterOfRebar[j], sd, Asm2 };
                            }
                        }
                    }
                    if (rebarData[0] != 0)
                    {
                        break;
                    }
                }
            }
            return rebarData;
        }



        public static double[] shuipingjin(int n, double[] h)
        {
            double[] As = new double[6];
            double roumin = 0.15 / 100;
            double[] ass = new double[n];
            double[] diameter = { 12, 14, 16, 18, 20, 22, 25 };
            if (n == 1)
            {
                ass[1] = 1000 * h[0] * roumin;
                for (int i = 0; i < 7; i++)
                {
                    double a = Math.PI * Math.Pow(diameter[i], 2) / 4 * 1000 / 150;
                    if (ass[1] < a)
                    {
                        As = new double[] { diameter[i], 150, 0, 0, a, 0 };
                    }
                }
            }
            return As;
        }

        private static double[] GetConcreteProperty(int concreteGrade)
        {
            Dictionary<int, double[]> dicConcrteProperty = new Dictionary<int, double[]>();
            dicConcrteProperty.Add(20, new double[] { 9.6, 2.55 * Math.Pow(10, 5), 1.54, 1.10 });
            dicConcrteProperty.Add(25, new double[] { 11.9, 2.80 * Math.Pow(10, 5), 1.78, 1.27 });
            dicConcrteProperty.Add(30, new double[] { 14.3, 3.00 * Math.Pow(10, 5), 2.01, 1.43 });
            dicConcrteProperty.Add(35, new double[] { 16.7, 3.15 * Math.Pow(10, 5), 2.20, 1.57 });
            dicConcrteProperty.Add(40, new double[] { 19.1, 3.25 * Math.Pow(10, 5), 2.39, 1.71 });
            dicConcrteProperty.Add(45, new double[] { 21.1, 3.35 * Math.Pow(10, 5), 2.51, 1.80 });
            dicConcrteProperty.Add(50, new double[] { 23.1, 3.45 * Math.Pow(10, 5), 2.64, 1.89 });

            double[] conProperties;
            dicConcrteProperty.TryGetValue(concreteGrade, out conProperties);
            return conProperties;
        }

        public static double liefeng1(double M, double cs, double[] d, double As, double h, int C, double rg)
        {
            //本子函数用于计算裂缝宽度，与liefeng子函数不同，此子函数仅用于单个点的裂缝计算
            double alphacr = 1.9;
            double[] CC = GetConcreteProperty(C);                 //调用子函数，提取混凝土抗拉强度设计值
            double ftk = CC[2];
            double Mq = M / rg;                                  //计算准永久荷载内力，注解同liefeng子函数

            //d的所有格式均为[直径1  数量1  直径2  数量2]，跨中时，直径2和数量2值为0
            double deq = (d[1] * Math.Pow(d[0], 2) + d[3] * Math.Pow(d[2], 2)) / (d[1] * d[0] + d[3] * d[2]);                   //等效直径计算
            double dmax = Math.Max(d[0], d[2]);                                                      //为所选两种钢筋直径的较大值
            double h0 = h - cs - dmax / 2;                                                           //为计算高度
            double seigemas = Math.Abs(Mq) / (0.87 * h0 * As);
            double Ate = 0.5 * 1000 * h;
            double route = As / Ate;
            if (route < 0.01)
            {
                route = 0.01;
            }
            double fai = 1.1 - 0.65 * ftk / (route * seigemas);
            if (fai < 0.2)
            {
                fai = 0.2;
            }
            else if (fai > 1.0)
            {
                fai = 1.0;
            }
            double w = alphacr * fai * seigemas * (1.9 * cs + 0.08 * deq / route) / 200000;
            return w;
        }

        public static List<double[]> zhengjinxuanjin(double space, int n, List<double> As, double[] h, double[] M, double cs, int C, double rg)
        {
            double AsmaxOfLibrary = Math.PI * Math.Pow(25, 2) / 4 * 1000 / space;

            List<double[]> A = new List<double[]>(n);
            if (n == 1) //1层挡墙
            {
                double AsmaxAB = As[1];                         //AB跨中计算配筋
                if (AsmaxAB > AsmaxOfLibrary || AsmaxAB <= 0) //超出选筋库时或之前配筋计算超筋时令其配筋为0
                {
                    throw new Exception("选筋超出选筋库");
                }

                var AssAB = Zuhezhengfujin.zhengjinshuchu(space, AsmaxAB);           //调用函数，输出AB点的配筋，[直径 间距 实选面积]
                double[] Asss = { AssAB[0], AssAB[1], 0, 0, AssAB[2] };       //与负筋统一格式

                // AB跨中选筋
                double[] d = { Asss[0], Math.Floor(1000 / Asss[1]), 0, 0 };   //i点的配筋直径、数量
                var w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[4], h[0], C, rg);  //计算i跨裂缝
                if (w <= 0.2)
                {
                    A.Add(new double[] { Asss[0], Asss[1], Asss[2], Asss[3], Asss[4], w });//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                }
                else
                {
                    double Asss0 = Asss[4];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                    Asss[4] = Asss[4] + 1;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                    if (Asss[4] > AsmaxOfLibrary)//超出选筋库时令其配筋为0
                    {
                        throw new Exception("选筋超出选筋库");
                    }

                    for (int j = 1; j <= 2000; j++)   //嵌套循环增加配筋面积，最多增加2000mm2
                    {

                        var AA = Zuhezhengfujin.zhengjinshuchu(space, Asss[4]);//再次选筋
                        Asss = new double[] { AA[0], AA[1], 0, 0, AA[2] }; //将选筋结果填入Asss矩阵中
                        d = new double[] { Asss[0], Math.Floor(1000 / Asss[1]), 0, 0 };
                        w = Zuhezhengfujin.liefeng1(M[1], cs, d, Asss[4], h[0], C, rg);
                        if (w <= 0.2)
                        {
                            A.Add(new double[] { Asss[0], Asss[1], Asss[2], Asss[3], Asss[4], w });
                            break;
                        }
                        else
                        {
                            Asss[4] = Asss[4] + 1;

                            if (Asss[4] > AsmaxOfLibrary)//超出选筋库时令其配筋为0
                            {
                                MessageBox.Show("选筋超出选筋库");
                                A.Add(new double[6]);
                                A.Add(new double[6]);
                                A.Add(new double[6]);
                                return A;
                            }

                            if (Asss[4] - Asss0 > 1000)
                            {
                                MessageBox.Show("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                A.Add(new double[6]);
                                A.Add(new double[6]);
                                A.Add(new double[6]);
                                return A;
                            }
                        }
                    }
                }
            }
            else//2层挡墙
            {
                for (int i = 0; i < n; i++)
                {
                    double varAsMiddle = As[2 * i + 1];//AB跨中计算配筋
                    if (varAsMiddle > AsmaxOfLibrary || varAsMiddle <= 0)
                    {
                        throw new Exception("选筋超出选筋库");
                    }
                    double[] arrAsMiddle = Zuhezhengfujin.zhengjinshuchu(space, varAsMiddle);//调用函数，输出跨中的配筋，[直径 间距 实选面积]

                    double[] d = { arrAsMiddle[0], Math.Floor(1000 / arrAsMiddle[1]), 0, 0 };       //i层配筋直径、数量
                    double w = Zuhezhengfujin.liefeng1(M[2 * i + 1], cs, d, arrAsMiddle[2], h[i], C, rg);//计算i跨裂缝
                    if (w <= 0.2)
                    {
                        A.Add(new double[] { arrAsMiddle[0], arrAsMiddle[1], 0, 0, arrAsMiddle[2], w });//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = arrAsMiddle[2];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        arrAsMiddle[2] = arrAsMiddle[2] + 1;//裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                        if (arrAsMiddle[2] > AsmaxOfLibrary)//超出选筋库时令其配筋为0
                        {
                            MessageBox.Show("超出选筋库！");
                            A.Add(new double[6]);
                            A.Add(new double[6]);
                            A.Add(new double[6]);
                            return A;
                        }

                        for (int j = 1; j <= 2000; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            arrAsMiddle = Zuhezhengfujin.zhengjinshuchu(space, arrAsMiddle[2]);//再次选筋
                            d = new double[] { arrAsMiddle[0], Math.Floor(1000 / arrAsMiddle[1]), 0, 0 };
                            w = Zuhezhengfujin.liefeng1(M[2 * i + 1], cs, d, arrAsMiddle[2], h[i], C, rg);
                            if (w <= 0.2)
                            {
                                A.Add(new double[] { arrAsMiddle[0], arrAsMiddle[1], 0, 0, arrAsMiddle[2], w });
                                break;
                            }
                            else
                            {
                                arrAsMiddle[2] = arrAsMiddle[2] + 1;
                                if (arrAsMiddle[2] > AsmaxOfLibrary)                //超出选筋库时令其配筋为0
                                {
                                    MessageBox.Show("选筋超出选筋库");
                                    A.Add(new double[6]);
                                    A.Add(new double[6]);
                                    A.Add(new double[6]);
                                    return A;
                                }

                                if (arrAsMiddle[2] - Asss0 > 1000)
                                {
                                    MessageBox.Show("按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                    A.Add(new double[6]);
                                    A.Add(new double[6]);
                                    A.Add(new double[6]);
                                    return A;
                                }
                            }
                        }
                    }
                }
            }

            return A;
        }

        public static List<double[]> peijinxietiao(double space, double[][] A, double[] M, double cs, double[] h, int C, double rg)
        {
            double[] d = { 12, 14, 16, 18, 20, 22, 25 };
            double[,] indexs = {
                { 1, 1, 1, 0, 0, 0, 0},
                { 1, 1, 1, 1, 0, 0, 0},
                { 1, 1, 1, 1, 1, 0, 0},
                { 0, 1, 1, 1, 1, 1, 0},
                { 0, 0, 1, 1, 1, 1, 1},
                { 0, 0, 0, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1, 1, 1}
            };

            double[] amax = A[0];
            double[] amin = A[1];
            double pm = M[2];
            bool isSingle = false;
            if (A[0][0] < A[1][0])
            {
                amax = A[1];
                amin = A[0];
                pm = M[0];
                isSingle = true;
            }

            amin[0] = amax[0];
            for (int i = 0; i < 7; i++)
            {
                if (amin[0] == d[i])
                {
                    double delta = 1000;
                    double area = amin[4];
                    for (int j = 0; j < 7; j++)
                    {
                        if (indexs[i, j] == 1)
                        {
                            double area1 = Math.PI * Math.Pow(d[i], 2) / 4 * 1000 / space + Math.PI * Math.Pow(d[j], 2) / 4 * 1000 / space;
                            double area2 = Math.PI * Math.Pow(d[i], 2) / 4 * 1000 / space + Math.PI * Math.Pow(d[j], 2) / 4 * 1000 / (2 * space);

                            double[] dd = { amin[0], Math.Floor(1000 / amin[1]), d[j], Math.Floor(1000 / (2 * space)) };
                            double w1 = Zuhezhengfujin.liefeng1(pm, cs, dd, area2, h[0], C, rg);
                            double w2 = Zuhezhengfujin.liefeng1(pm, cs, dd, area2, h[1], C, rg);
                            double w = isSingle ? w1 : Math.Max(w1, w2);

                            if (area2 - area > -0.001 && area2 - area < delta)
                            {
                                delta = area2 - area;
                                amin[2] = d[j];
                                amin[3] = 2 * space;
                                amin[4] = area2;
                                amin[5] = w;
                            }
                            else if (area2 - area < -0.001 && w < 0.2)
                            {
                                delta = area2 - area;
                                amin[2] = d[j];
                                amin[3] = 2 * space;
                                amin[4] = area2;
                                amin[5] = w;
                            }

                            dd = new double[] { amin[0], Math.Floor(1000 / amin[1]), d[j], Math.Floor(1000 / space) };
                            w1 = Zuhezhengfujin.liefeng1(pm, cs, dd, area1, h[0], C, rg);
                            w2 = Zuhezhengfujin.liefeng1(pm, cs, dd, area1, h[1], C, rg);
                            w = isSingle ? w1 : Math.Max(w1, w2);

                            if (area1 - area > -0.001 && area1 - area < delta)
                            {
                                delta = area1 - area;
                                amin[2] = d[j];
                                amin[3] = space;
                                amin[4] = area1;
                                amin[5] = w;
                            }
                            else if (area1 - area < -0.001 && w < 0.2)
                            {
                                delta = area1 - area;
                                amin[2] = d[j];
                                amin[3] = space;
                                amin[4] = area1;
                                amin[5] = w;
                            }
                        }
                    }
                    break;
                }
            }

            List<double[]> result = isSingle ? new List<double[]> { amin, amax } : new List<double[]> { amax, amin };

            return result;
        }

        public static double[] tongchang(double space, double ft, double fy, double h)
        {
            //此子函数用于给顶点选通长筋
            //h为指定某一层的截面厚度
            double roumin = Math.Max(0.002, 0.45 * ft / (100 * fy));//最小配筋率

            //第6列全部为0是裂缝宽度为0，为了和选筋子程序里的格式统一
            double[][] AAA ={
                new double[]{ 12, space, 0, 0, Math.Pow(12, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 14, space, 0, 0, Math.Pow(14, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 16, space, 0, 0, Math.Pow(16, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 18, space, 0, 0, Math.Pow(18, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 20, space, 0, 0, Math.Pow(20, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 22, space, 0, 0, Math.Pow(22, 2) * Math.PI / 4 * 1000 / space, 0 },
                new double[]{ 25, space, 0, 0, Math.Pow(25, 2) * Math.PI / 4 * 1000 / space, 0 } };
            double As = 1000 * h * roumin;

            if (As <= AAA[0][4])
            {
                return AAA[0];
            }
            else if (As > AAA[0][4] && As <= AAA[1][4])
            {
                return AAA[1];
            }
            else if (As > AAA[1][4] && As <= AAA[2][4])
            {
                return AAA[2];
            }
            else if (As > AAA[2][4] && As <= AAA[3][4])
            {
                return AAA[3];
            }
            else if (As > AAA[3][4] && As <= AAA[4][4])
            {
                return AAA[4];
            }
            else if (As > AAA[4][4] && As <= AAA[5][4])
            {
                return AAA[5];
            }
            else if (As > AAA[5][4] && As <= AAA[6][4])
            {
                return AAA[6];
            }
            return new double[] { };
        }


        public static List<double[]> fujinxuanjin(int space, int n, List<double> As, double ft, double fy, double[] h, double[] M, double cs, int C, double rg)
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
                double AsmaxA = As[0];//A点计算配筋
                if (AsmaxA > 6544 || AsmaxA <= 0)
                {
                    throw new Exception("A点选筋超出选筋库");
                }

                double Astong = roumin * h[0] * 1000;//另通长筋 = 构造钢筋
                double[] AssA = Zuhezhengfujin.fujinshuchu(space, AsmaxA, Astong);//调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
                double[] Asss = { AssA[0], AssA[1], AssA[2], AssA[3], AssA[4] };//B点配筋为负筋通长筋,裂缝不用验算

                //A点的选筋
                double[] d = { Asss[0], Math.Floor(1000 / Asss[1]), Asss[2], Math.Floor(1000 / Asss[3]) };//A点的配筋直径、数量
                double w = Zuhezhengfujin.liefeng1(M[0], cs, d, Asss[4], h[0], C, rg);//计算A点裂缝

                double[] A1 = new double[6];
                if (w <= 0.2)
                {
                    A1 = new double[] { Asss[0], Asss[1], Asss[2], Asss[3], Asss[4], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                }
                else
                {
                    double Asss0 = Asss[4];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                    Asss[4] = Asss[4] + 10;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算

                    if (Asss[4] > 6544)
                    {
                        throw new Exception("A点选筋超出选筋库");
                    }

                    for (int j = 1; j <= 200; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                    {
                        double[] AA = Zuhezhengfujin.fujinshuchu(space, Asss[4], Astong);//再次选筋

                        Asss = new double[] { AA[0], AA[1], AA[2], AA[3], AA[4] };//将选筋结果填入Asss矩阵中
                        d = new double[] { Asss[0], Math.Floor(1000 / Asss[1]), Asss[2], Math.Floor(1000 / Asss[3]) };
                        w = Zuhezhengfujin.liefeng1(M[0], cs, d, Asss[4], h[0], C, rg);
                        if (w <= 0.2)
                        {
                            A1 = new double[] { Asss[0], Asss[1], Asss[2], Asss[3], Asss[4], w };//计算得到A点配筋和裂缝宽度
                            break;
                        }
                        else
                        {
                            Asss[4] = Asss[4] + 10;
                            if (Asss[4] > 6544)
                            {
                                throw new Exception("A点选筋超出选筋库");
                            }

                            if (Asss[4] - Asss0 > 1000)
                            {
                                throw new Exception("A点按裂缝选筋增加钢筋过大，请修改挡墙参数");
                            }
                        }
                    }
                }

                List<double[]> A = new List<double[]>();
                A.Add(new double[] { A1[0], A1[1], A1[2], A1[3], A1[4], A1[5] });
                A.Add(new double[] { A1[0], A1[1], 0, 0, Math.PI * Math.Pow(A1[0], 2) * 1000 / (4 * A1[1]), 0 });
                return A;
            }
            else if (n == 2)
            {
                double[][] Ak = new double[2][];
                double AsmaxA = As[0];//A点计算配筋
                double AsmaxB = As[2];//B点计算配筋

                if (Math.Max(AsmaxA, AsmaxB) > 6544 || Math.Min(AsmaxA, AsmaxB) <= 0)
                {
                    throw new Exception("选筋超出选筋库");
                }

                double Astong = Math.Max(roumin * h[0] * 1000, roumin * h[1] * 1000);//1层通长筋 = 构造钢筋，AB两点都用此值
                double[] AssA = Zuhezhengfujin.fujinshuchu(space, AsmaxA, Astong);//调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
                double[] AssB = Zuhezhengfujin.fujinshuchu(space, AsmaxB, Astong);//调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
                double[][] Asss = new double[][]{
                    new double[] {AssA[0], AssA[1], AssA[2], AssA[3], AssA[4]},
                    new double[] {AssB[0], AssB[1], AssB[2], AssB[3], AssB[4]}};
                //C点配筋为负筋通长筋,裂缝不用验算

                //A点配筋
                for (int i = 0; i < 2; i++)
                {
                    double[] d = new double[] { Asss[i][0], Math.Floor(1000 / Asss[i][1]), Asss[i][2], Math.Floor(1000 / Asss[i][3]) };//A点的配筋直径、数量
                    double wl = i == 0 ? 0 : Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][4], h[i - 1], C, rg);//计算A点裂缝
                    double wr = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][4], h[i], C, rg);//计算A点裂缝
                    double w = Math.Max(wl, wr);
                    if (w <= 0.2)
                    {
                        Ak[i] = new double[] { Asss[i][0], Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = Asss[i][4];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        Asss[i][4] = Asss[i][4] + 10;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                        if (Asss[i][4] > 6544)
                        {
                            throw new Exception("选筋超出选筋库");
                        }

                        for (int j = 1; j <= 200; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            double[] AA = Zuhezhengfujin.fujinshuchu(space, Asss[i][4], Astong);//再次选筋
                            Asss[i] = new double[] { AA[0], AA[1], AA[2], AA[3], AA[4] };//将选筋结果填入Asss矩阵中
                            d = new double[] { Asss[i][0], Math.Floor(1000 / Asss[i][1]), Asss[i][2], Math.Floor(1000 / Asss[i][3]) };
                            if (i == 0)
                            {
                                w = Zuhezhengfujin.liefeng1(M[0], cs, d, Asss[i][4], h[0], C, rg);
                            }
                            else
                            {
                                wl = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][4], h[i - 1], C, rg);
                                wr = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, Asss[i][4], h[i], C, rg);
                                w = Math.Max(wl, wr);
                            }

                            if (w <= 0.2)
                            {
                                Ak[i] = new double[] { Asss[i][0], Asss[i][1], Asss[i][2], Asss[i][3], Asss[i][4], w };//计算得到A点配筋和裂缝宽度
                                break;
                            }
                            else
                            {
                                Asss[i][4] = Asss[i][4] + 10;
                                if (Asss[i][4] > 6544)
                                {
                                    throw new Exception("选筋超出选筋库");
                                }

                                if (Asss[i][4] - Asss0 > 1000)
                                {
                                    throw new Exception("A点按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                }
                            }
                        }
                    }
                }

                List<double[]> A = new List<double[]>();
                A = peijinxietiao(space, Ak, M, cs, h, C, rg);
                A.Add(tongchang(space, ft, fy, h[1]));
                return A;
            }
            else
            {
                List<double[]> A = new List<double[]>();

                double[][] Ak = new double[n][];

                double[] arrAsNeed = new double[n];
                double[] arrAsTong = new double[n];
                List<double[]> arrAs = new List<double[]>();
                for (int i = 0; i < n; i++)
                {
                    arrAsNeed[i] = As[2 * i];
                    if (arrAsNeed[i] > 6544 || arrAsNeed[i] <= 0)
                    {
                        throw new Exception("选筋超出选筋库");
                    }
                    arrAsTong[i] = i == 0 ? Math.Max(roumin * h[i] * 1000, roumin * h[i + 1] * 1000)
                        : Math.Max(roumin * h[i - 1] * 1000, roumin * h[i] * 1000);

                    arrAs.Add(fujinshuchu(space, arrAsNeed[i], arrAsTong[i]));
                }

                for (int i = 0; i < n; i++)
                {
                    double[] d = new double[] { arrAs[i][0], Math.Floor(1000 / arrAs[i][1]), arrAs[i][2], Math.Floor(1000 / arrAs[i][3]) };//A点的配筋直径、数量
                    double wl = i == 0 ? 0 : liefeng1(M[2 * i], cs, d, arrAs[i][4], h[i - 1], C, rg);//计算A点裂缝
                    double wr = liefeng1(M[2 * i], cs, d, arrAs[i][4], h[i], C, rg);//计算A点裂缝
                    double w = Math.Max(wl, wr);
                    if (w <= 0.2)
                    {
                        Ak[i] = new double[] { arrAs[i][0], arrAs[i][1], arrAs[i][2], arrAs[i][3], arrAs[i][4], w };//A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
                    }
                    else
                    {
                        double Asss0 = arrAs[i][4];//Asss0用于做标记，判断后面增加面积是否超过1000mm2
                        arrAs[i][4] = arrAs[i][4] + 10;//裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                        if (arrAs[i][4] > 6544)
                        {
                            throw new Exception("选筋超出选筋库");
                        }

                        for (int j = 1; j <= 200; j++)//嵌套循环增加配筋面积，最多增加2000mm2
                        {
                            arrAs[i] = fujinshuchu(space, arrAs[i][4], arrAsTong[0]);//再次选筋
                            d = new double[] { arrAs[i][0], Math.Floor(1000 / arrAs[i][1]), arrAs[i][2], Math.Floor(1000 / arrAs[i][3]) };
                            if (i == 0)
                            {
                                w = Zuhezhengfujin.liefeng1(M[0], cs, d, arrAs[i][4], h[0], C, rg);
                            }
                            else
                            {
                                wl = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, arrAs[i][4], h[i - 1], C, rg);
                                wr = Zuhezhengfujin.liefeng1(M[2 * i], cs, d, arrAs[i][4], h[i], C, rg);
                                w = Math.Max(wl, wr);
                            }

                            if (w <= 0.2)
                            {
                                Ak[i] = new double[] { arrAs[i][0], arrAs[i][1], arrAs[i][2], arrAs[i][3], arrAs[i][4], w };//计算得到A点配筋和裂缝宽度
                                break;
                            }
                            else
                            {
                                arrAs[i][4] = arrAs[i][4] + 10;
                                if (arrAs[i][4] > 6544)
                                {
                                    throw new Exception("选筋超出选筋库");
                                }

                                if (arrAs[i][4] - Asss0 > 1000)
                                {
                                    throw new Exception("A点按裂缝选筋增加钢筋过大，请修改挡墙参数");
                                }
                            }
                        }
                    }

                    if (i == 0)
                    {
                        //continue;
                    }
                    else if (i == 1)
                    {
                        A = peijinxietiao(space, Ak, M, cs, h, C, rg);
                    }
                    else
                    {
                        A.Add(Ak[i]);
                    }
                }
                A.Add(tongchang(space, ft, fy, h[n - 1]));
                return A;
            }
        }

        public static List<double[]> zuhezhengfujin(int n, List<double> As, double ft, double fy, double[] h, double[] M, double cs, int C, double rg)
        {
            //此子函数用于将正筋选筋和负筋选筋结果组合成一个矩阵，方便后续调用
            //Asz——为正筋选筋矩阵
            //Asf150——为负筋选筋矩阵间距150
            //Asf200——为负筋选筋矩阵间距200
            //Ass——为最后输出的配筋
            //As——为为peijinjisuan(M, cs, n, fy, fc, ft)中计算得到各点的计算配筋

            List<double[]> listAsf150 = fujinxuanjin(150, n, As, ft, fy, h, M, cs, C, rg);
            List<double[]> listAsf200 = fujinxuanjin(200, n, As, ft, fy, h, M, cs, C, rg);
            List<double[]> listAsz150 = zhengjinxuanjin(150, n, As, h, M, cs, C, rg);
            List<double[]> listAsz200 = zhengjinxuanjin(200, n, As, h, M, cs, C, rg);
            List<double[]> listAss = new List<double[]>();

            foreach (var varAsf in listAsf150)
            {
                listAss.Add(varAsf);
            }
            foreach (var varAsf in listAsf200)
            {
                listAss.Add(varAsf);
            }
            foreach (var varAsz in listAsz150)
            {
                listAss.Add(varAsz);
            }
            foreach (var varAsz in listAsz200)
            {
                listAss.Add(varAsz);
            }

            return listAss;
        }
    }
}
