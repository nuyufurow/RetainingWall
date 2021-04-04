using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr.RetainingWall
{
    class Chengben
    {
        private static double maoguchangdu(int C, int F, double d)
        {
            int[] Cs = { 20, 25, 30, 35, 40, 45, 50 };
            int[] Fs = { 300, 335, 400, 500 };
            int[,] multiples = {
                { 39, 38,  0,  0 },
                { 34, 33, 40, 48 },
                { 30, 29, 35, 43 },
                { 28, 27, 32, 39 },
                { 25, 25, 29, 36 },
                { 24, 23, 28, 34 },
                { 23, 22, 27, 32 }};

            for (int i = 0; i < 7; i++)
            {
                if (Cs[i] == C)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Fs[j] == F)
                        {
                            return multiples[i, j] * d;
                        }
                    }
                }
            }
            return 0;
        }

        private static double maoguchangduE(int Z, int C, int F, double d)
        {
            int[] Cs = { 20, 25, 30, 35, 40, 45, 50 };
            int[] Fs = { 300, 335, 400, 500 };
            int[,] multiples1 = {
                { 45, 44,  0,  0 },
                { 39, 38, 46, 55 },
                { 35, 33, 40, 49 },
                { 32, 31, 37, 45 },
                { 29, 29, 33, 41 },
                { 28, 26, 32, 39 },
                { 26, 25, 31, 37 }};

            int[,] multiples2 = {
                { 45, 44,  0,  0 },
                { 39, 38, 46, 55 },
                { 35, 33, 40, 49 },
                { 32, 31, 37, 45 },
                { 29, 29, 33, 41 },
                { 28, 26, 32, 39 },
                { 26, 25, 31, 37 }};

            int[,] multiples3 = {
                { 41, 40,  0,  0 },
                { 36, 35, 42, 50 },
                { 32, 30, 37, 45 },
                { 29, 28, 34, 41 },
                { 26, 26, 30, 38 },
                { 25, 24, 29, 36 },
                { 24, 23, 28, 34 }};

            int[,] multiples4 = {
                { 39, 38,  0,  0 },
                { 34, 33, 40, 48 },
                { 30, 29, 35, 43 },
                { 28, 27, 32, 39 },
                { 25, 25, 29, 36 },
                { 24, 23, 28, 34 },
                { 23, 22, 27, 32 }};

            int[,] multiples;
            if (Z == 1)
            {
                multiples = multiples1;
            }
            else if (Z == 2)
            {
                multiples = multiples2;
            }
            else if (Z == 3)
            {
                multiples = multiples2;
            }
            else if (Z == 4)
            {
                multiples = multiples2;
            }
            else
            {
                throw new Exception("抗震等级只能是1、2、3、4");
            }

            for (int i = 0; i < 7; i++)
            {
                if (Cs[i] == C)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Fs[j] == F)
                        {
                            return multiples[i, j] * d;
                        }
                    }
                }
            }
            return 0;
        }


        public static double[] chengben(double[][] Ass, double[] Ashui, int n, double Qh, double Qg,
            double[] h, double[] H, double cs, double[] s, int Z, int CC, int F)
        {
            if (n == 1)
            {
                double la150 = maoguchangdu(CC, F, Ass[0][0]);   //间距150结果的la
                double la200 = maoguchangdu(CC, F, Ass[1][0]);   //间距200结果的la
                double laz = maoguchangdu(CC, F, Ass[1][0]);
                double Lft150 = H[0] - s[0] + la150;                //Lft为间距150通长顶筋的长度
                double Lft200 = H[0] - s[0] + la200;                //Lft为间距200通长顶筋的长度
                double Lff = Math.Round((H[0] - s[0]) / 3, 1) + 10; //Lff为附加顶筋的长度，对十位四舍五入取整
                double Lz = H[0] - s[0] + laz;                      //Lz为底筋的长度

                double Qs = 1 * 2 * Ashui[4] * H[0] * 7.85 * Qg * Math.Pow(10, -9);//水平筋费用

                double Qft150 = (1000 / Ass[0][1]) * (Math.Pow(Ass[0][0] / 1000, 2) * Math.PI / 4) * (Lft150 / 1000) * 7.85 * Qg;//Qft为通长顶筋间距150费用
                double Qff150 = (1000 / Ass[0][3]) * (Math.Pow(Ass[0][2] / 1000, 2) * Math.PI / 4) * (Lff / 1000) * 7.85 * Qg;//Qff为附加顶筋间距150费用
                double Qft200 = (1000 / Ass[2][1]) * (Math.Pow(Ass[2][0] / 1000, 2) * Math.PI / 4) * (Lft200 / 1000) * 7.85 * Qg;//Qft为通长顶筋间距200费用
                double Qff200 = (1000 / Ass[2][3]) * (Math.Pow(Ass[2][2] / 1000, 2) * Math.PI / 4) * (Lff / 1000) * 7.85 * Qg;//Qff为附加顶筋间距200费用
                double Qz = (1000 / Ass[4][1]) * (Math.Pow(Ass[4][0] / 1000, 2) * Math.PI / 4) * (Lz / 1000) * 7.85 * Qg;//Qz为底筋费用
                double Qt = (1 * h[0] / 1000) * (H[0] / 1000) * Qh;//Qt为混凝土费用
                double QF150 = Qft150 + Qff150;// QF150为间距150所有钢筋的费用
                double QF200 = Qft200 + Qff200;// QF200为间距200所有钢筋的费用
                double[] Q = new double[] { QF150, QF200, Qz, Qs, Qt, QF150 + Qz + Qs + Qt, QF200 + Qz + Qs + Qt };
                return Q;
            }
            else if (n == 2)
            {
                double Lff1 = Math.Round((H[0] - s[0]) / 3, 1) + 10;//1层层高净高三分之一
                double Lff2 = Math.Round((H[1] - s[1]) / 3, 1) + 10;//2层层高净高三分之一
                double LffB = 2 * Math.Max(Lff1, Lff2) + s[0];//B点附加筋总长度
                double LffA = (H[0] - s[0]) / 3;//A点附加筋总长度

                double Lft1150 = H[0] + LffB + Math.Max(35 * Math.Max(Ass[0][0], Ass[2][0]), 500);//Lft为1层150通长顶筋的长度
                double Lft1200 = H[0] + LffB + Math.Max(35 * Math.Max(Ass[3][0], Ass[5][0]), 500);//Lft为1层150通长顶筋的长度
                double Lft2150 = H[1] - Math.Max(Lff1, Lff2) - s[1] - Math.Max(35 * Math.Max(Ass[0][0], Ass[2][0]), 500) + maoguchangdu(CC, F, Ass[2][0]);//Lft为2层间距150通长顶筋的长度
                double Lft2200 = H[1] - Math.Max(Lff1, Lff2) - s[1] - Math.Max(35 * Math.Max(Ass[3][0], Ass[5][0]), 500) + maoguchangdu(CC, F, Ass[5][0]);//Lft为2层间距200通长顶筋的长度
                double Lz1, Lz2;
                if (h[0] == h[1])//若1、2层墙厚相等，则用焊接或分别锚固
                {
                    if (Ass[6][1] == Ass[7][1])//一层和二层的底筋模数若相等，则采用电焊
                    {
                        Lz1 = H[0] + 500 + Math.Max(35 * Math.Max(Ass[6][0], Ass[7][0]), 500);//Lz为1层底筋的长度
                        Lz2 = H[1] - 500 - Math.Max(35 * Math.Max(Ass[6][0], Ass[7][0]), 500) - s[1] + maoguchangdu(CC, F, Ass[7][0]);//Lz为2层底筋的长度
                    }
                    else//一层和二层的底筋模数不同，则分别锚固
                    {
                        double laE1 = maoguchangduE(Z, CC, F, Ass[6][0]);
                        double laE2 = maoguchangduE(Z, CC, F, Ass[7][0]);
                        Lz1 = H[0] - s[0] + 1.2 * laE1;            //一层底筋长度，忽略基础中的长度
                        double laz = maoguchangdu(CC, F, Ass[7][0]);
                        Lz2 = H[1] - s[1] + laz + 1.2 * laE2;      //二层底筋长度
                    }
                }
                else//1、2层墙厚不相等，采用分别锚固
                {
                    Lz1 = H[0] - cs + 12 * Ass[6][0];
                    double laE2 = maoguchangduE(Z, CC, F, Ass[7][0]);
                    Lz2 = H[1] - s[1] + maoguchangdu(CC, F, Ass[7][0]) + 1.2 * laE2;
                }

                double Qft150 = ((1000 / Ass[0][1]) * (Math.Pow((Ass[0][0] / 1000), 2) * Math.PI / 4) * (Lft1150 / 1000) + (1000 / Ass[2][1]) * (Math.Pow((Ass[2][0] / 1000), 2) * Math.PI / 4) * (Lft2150 / 1000)) * 7.85 * Qg;  //Qft为通长顶筋间距150费用
                double Qff150 = ((1000 / Ass[0][3]) * (Math.Pow((Ass[0][2] / 1000), 2) * Math.PI / 4) * (LffA / 1000) + (1000 / Ass[1][3]) * (Math.Pow((Ass[1][2] / 1000), 2) * Math.PI / 4) * (LffB / 1000)) * 7.85 * Qg;        //Qff为附加顶筋间距150费用
                double Qft200 = ((1000 / Ass[3][1]) * (Math.Pow((Ass[3][0] / 1000), 2) * Math.PI / 4) * (Lft1200 / 1000) + (1000 / Ass[5][1]) * (Math.Pow((Ass[5][0] / 1000), 2) * Math.PI / 4) * (Lft2200 / 1000)) * 7.85 * Qg;  //Qft为通长顶筋间距200费用
                double Qff200 = ((1000 / Ass[3][3]) * (Math.Pow((Ass[3][2] / 1000), 2) * Math.PI / 4) * (LffA / 1000) + (1000 / Ass[4][3]) * (Math.Pow((Ass[4][2] / 1000), 2) * Math.PI / 4) * (LffB / 1000)) * 7.85 * Qg;        //Qff为附加顶筋间距200费用

                double Qz = ((1000 / Ass[6][1]) * (Math.Pow((Ass[6][0] / 1000), 2) * Math.PI / 4) * (Lz1 / 1000) + (1000 / Ass[7][1]) * (Math.Pow((Ass[7][0] / 1000), 2) * Math.PI / 4) * (Lz2 / 1000)) * 7.85 * Qg;              //Qz为底筋费用
                double Qs = (1 * 2 * Ashui[0] * H[0] + 1 * 2 * Ashui[1] * H[1]) * 7.85 * Qg * Math.Pow(10, -9);//水平筋的费用
                double Qt = ((1 * h[0] / 1000) * (H[0] / 1000) + (1 * h[1] / 1000) * (H[1] / 1000)) * Qh;//Qt为混凝土费用
                double QF150 = Qft150 + Qff150;//QF150为间距150所有钢筋的费用
                double QF200 = Qft200 + Qff200;//QF200为间距200所有钢筋的费用
                double[] Q = { QF150, QF200, Qz, Qt, Qs, QF150 + Qz + Qt + Qs, QF200 + Qz + Qt + Qs };
                        //150顶筋费用 200顶筋费用 底筋费用 砼费用 水平筋费用 间距150总费用 间距200总费用
            }

            return new double[]{};  
        }

    }
}
