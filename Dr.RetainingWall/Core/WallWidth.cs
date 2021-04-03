using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr.RetainingWall
{
    class WallWidth
    {
        public static List<double[]> Genereate(int n)
        {
            double[] widths = { 400, 350, 300, 250 };
            List<double[]> wallWidths = new List<double[]>();

            if (n == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    wallWidths.Add(new double[] { widths[i] });
                }
            }
            else if (n == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        wallWidths.Add(new double[] { widths[i], widths[j] });
                    }
                }
            }
            else if (n == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = i; j < 4; j++)
                    {
                        for (int k = j; k < 4; k++)
                        {
                            wallWidths.Add(new double[] { widths[i], widths[j], widths[k] });
                        }
                    }
                }
            }
            else if (n == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = i; j < 4; j++)
                    {
                        for (int k = j; k < 4; k++)
                        {
                            for (int m = k; m < 4; m++)
                            {
                                wallWidths.Add(new double[] { widths[i], widths[j], widths[k], widths[m] });
                            }
                        }
                    }
                }
            }
            return wallWidths;
        }
    }
}
