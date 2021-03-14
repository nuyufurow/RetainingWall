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



        public static double[] chengben(double[][] Ass, double[] Ashui, int n, double Qh, double Qg,
            double[] h, double[] H, double cs, double[] s, double[] Z, int CC, int F)
        {
            if (n == 1)
            {
                double la150 = maoguchangdu(CC, F, Ass[0][0]);   //间距150结果的la
                double la200 = maoguchangdu(CC, F, Ass[1][0]);   //间距200结果的la
                double laz = maoguchangdu(CC, F, Ass[2][0]);
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

            return new double[]{};  
        }

    }
}
