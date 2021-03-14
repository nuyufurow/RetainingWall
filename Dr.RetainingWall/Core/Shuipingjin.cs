using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr.RetainingWall
{
    class Shuipingjin
    {
        public static double[] shuipingjin(int n, double[] h)
        {
            double roumin = 0.15 / 100;
            double[] diameters = { 12, 14, 16, 18, 20, 22, 25 };
            if (n == 1)
            {
                double Ass = 1000 * h[0] * roumin;
                for (int i = 0; i < 7; i++)
                {
                    double As = Math.PI * Math.Pow(diameters[i], 2) / 4 * 1000 / 150;
                    if (Ass < As)
                    {
                        return new double[] { diameters[i], 150, 0, 0, As, 0 };

                    }

                }
            }
            else if (n == 2)
            {

            }
            else if (n == 3)
            {

            }
            else if (n == 4)
            { 
            
            
            }


            return new double[]{ };
        
        }


    }
}
