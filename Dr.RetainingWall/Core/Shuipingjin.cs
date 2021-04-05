using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr.RetainingWall
{
    class Shuipingjin
    {
        public static List<double[]> shuipingjin(int n, double[] h)
        {
            List<double[]> result = new List<double[]>();
            double roumin = 0.15 / 100;
            double[] diameters = { 12, 14, 16, 18, 20, 22, 25 };

            for (int i = 0; i < n; i++)
            {
                double Ass = 1000 * h[i] * roumin;
                for (int j = 0; j < 7; j++)
                {
                    double As = Math.PI * Math.Pow(diameters[j], 2) / 4 * 1000 / 150;
                    if (Ass < As)
                    {
                        result.Add(new double[] { diameters[j], 150, 0, 0, As, 0 });
                        break;
                    }
                }
            }
            return result;
        }
    }
}
