using LinearAlgebra;
using System;
using Dr.RetainingWall;

class A
{
    //public static double[][] peijinxietiao150(double[][] A, double[] M, double cs, double[] h, int C, double rg)
    //{
    //    double[] d = { 12, 14, 16, 18, 20, 22, 25 };
    //    double[,] indexs = {
    //            { 1, 1, 1, 0, 0, 0, 0},
    //            { 1, 1, 1, 1, 0, 0, 0},
    //            { 1, 1, 1, 1, 1, 0, 0},
    //            { 0, 1, 1, 1, 1, 1, 0},
    //            { 0, 0, 1, 1, 1, 1, 1},
    //            { 0, 0, 0, 1, 1, 1, 1},
    //            { 0, 0, 0, 0, 1, 1, 1}
    //        };

    //    double[] amin = A[0];
    //    double[] amax = A[1];
    //    bool isExchange = false;
    //    if (A[0][0] >= A[1][0])
    //    {
    //        amin = A[1];
    //        amax = A[0];
    //        isExchange = true;
    //    }

    //    amin[0] = amax[0];
    //    for (int i = 0; i < 7; i++)
    //    {
    //        if (amin[0] == d[i])
    //        {
    //            double delta = 1000;
    //            for (int j = 0; j < 7; j++)
    //            {
    //                if (indexs[i, j] == 1)
    //                {
    //                    double area1 = Math.PI * Math.Pow(d[i], 2) / 4 * 1000 / 150 + Math.PI * Math.Pow(d[j], 2) / 4 * 1000 / 150;
    //                    double area2 = Math.PI * Math.Pow(d[i], 2) / 4 * 1000 / 150 + Math.PI * Math.Pow(d[j], 2) / 4 * 1000 / 300;
    //                    if (amin[4] < area2 && area2 - amin[4] < delta)
    //                    {
    //                        delta = area2 - amin[4];
    //                        amin[2] = d[j];
    //                        amin[3] = 300;
    //                        amin[4] = area2;
    //                        double[] dd = { amin[0], Math.Floor(1000 / amin[1]), amin[2], Math.Floor(1000 / amin[3]) };
    //                        double w1 = Zuhezhengfujin.liefeng1(M[2], cs, dd, amin[4], h[0], C, rg);
    //                        double w2 = Zuhezhengfujin.liefeng1(M[2], cs, dd, amin[4], h[1], C, rg);
    //                        double w = Math.Max(w1, w2);
    //                        amin[5] = w;
    //                    }
    //                    else if (amin[4] < area1 && area1 - amin[4] < delta)
    //                    {
    //                        delta = area1 - amin[4];
    //                        amin[2] = d[j];
    //                        amin[3] = 150;
    //                        amin[4] = area1;
    //                        double[] dd = { amin[0], Math.Floor(1000 / amin[1]), amin[2], Math.Floor(1000 / amin[3]) };
    //                        double w1 = Zuhezhengfujin.liefeng1(M[2], cs, dd, amin[4], h[0], C, rg);
    //                        double w2 = Zuhezhengfujin.liefeng1(M[2], cs, dd, amin[4], h[1], C, rg);
    //                        double w = Math.Max(w1, w2);
    //                        amin[5] = w;
    //                    }
    //                }
    //            }
    //            break;
    //        }
    //    }

    //    double[][] result = isExchange ? new double[][] { amax, amin } : new double[][] { amin, amax };

    //    return result;
    //}
}
