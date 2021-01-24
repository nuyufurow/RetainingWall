using LinearAlgebra;

namespace Dr.RetainingWall
{
    class Zuhezhengfujin
    {




        public static double[] zuhezhengfujin(double n, double[] As, double ft, double fy, double h, Matrix M, double cs, double C, double rg)
        {
            //此子函数用于将正筋选筋和负筋选筋结果组合成一个矩阵，方便后续调用
            //Asz——为正筋选筋矩阵
            //Asf150——为负筋选筋矩阵间距150
            //Asf200——为负筋选筋矩阵间距200
            //Ass——为最后输出的配筋
            //As——为为peijinjisuan(M, cs, n, fy, fc, ft)中计算得到各点的计算配筋
            if (n == 1)
            {
                //Asz = zhengjinxuanjin(n, As, h, M, cs, C, rg);
                //Asf150 = fujinxuanjin150(n, As, ft, fy, h, M, cs, C, rg);
                //Asf200 = fujinxuanjin200(n, As, ft, fy, h, M, cs, C, rg);
                //Ass =[Asf150;
                //Asf200;
                //Asz];




            }


        //    elseif n== 2
        //Asz = zhengjinxuanjin(n, As, h, M, cs, C, rg);
        //    Asf150 = fujinxuanjin150(n, As, ft, fy, h, M, cs, C, rg);
        //    Asf200 = fujinxuanjin200(n, As, ft, fy, h, M, cs, C, rg);
        //    Ass =[Asf150;
        //    Asf200;
        //    Asz];

        //    elseif n== 3
        //Asz = zhengjinxuanjin(n, As, h, M, cs, C, rg);
        //    Asf150 = fujinxuanjin150(n, As, ft, fy, h, M, cs, C, rg);
        //    Asf200 = fujinxuanjin200(n, As, ft, fy, h, M, cs, C, rg);
        //    Ass =[Asf150;
        //    Asf200;
        //    Asz];

        //    elseif n== 4
        //Asz = zhengjinxuanjin(n, As, h, M, cs, C, rg);
        //    Asf150 = fujinxuanjin150(n, As, ft, fy, h, M, cs, C, rg);
        //    Asf200 = fujinxuanjin200(n, As, ft, fy, h, M, cs, C, rg);
        //    Ass =[Asf150;
        //    Asf200;
        //    Asz];

            return new double[] { }
        }


    }
}
