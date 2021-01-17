using System;
using System.Collections.Generic;
using LinearAlgebra;


namespace Dr.RetainingWall
{
    class Input
    {
        private Dictionary<int, double[]> dicConcrteProperty;//混凝土属性集
        private Dictionary<int, double[]> dicRebarProperty;//钢筋属性集
        private int[] C = { 30 };//混凝土等级
        private int[] F = { 400 };//钢筋等级
        public double[] E;//C30弹性模量+++
        private double[] fc;//C30抗压强度设计值+++
        private double[] fy;//钢筋抗拉强度设计值


        public double r = 0.00002;//填土容重N/mm3
        public double fh = 1500;//墙顶覆土厚度1500mm
        public double p0 = 0.005;//活荷载N/mm2
        public double rg = 1.3;//恒荷载分项系数
        public double rq = 1.5;//活荷载分项系数


        public int n=1;//层数1
        public double[] H = { 4000 };//层高4米
        private double[] h = { 300 };//单层墙厚300mm
        public double[] A;//截面积+++
        public double[] I;//截面惯性矩+++

        public double T = 0.75;//内力调幅系数

        //////////////////////////////////////////////////////private double F = 400;//钢筋等级为HRB400
        private double Qh = 300;//每方混凝土的单价
        private double Qg = 4000;//每吨钢筋的单价
        private double Z = 4;//挡墙抗震等级
        private double s = 160;//地下室顶板厚度
        private double cs = 30;//保护层厚度
        ////private double ft = hunningtu(4);//混凝土抗拉强度设计值

        //private double Q = hezaijisuan(n, H, r, hh, p0, rg, rq);//得到计算荷载
        //private double f01 = dengxiaojiedianhezai01(n, H, Q);//得到墙底固结时等效节点荷载，已划行划列
        //private double f02 = dengxiaojiedianhezai02(n, H, Q);//得到墙底铰结时等效节点荷载，已划行划列

        //private double K01 = weiyibianjie01(n, K);//得到划行划列之后，墙底固结的总刚度矩阵
        //private double K02 = weiyibianjie02(n, K);//得到划行划列之后，墙底铰结的总刚度矩阵

        //private double M01 = neilijisuan01(K, K01, f01, n, Q, H);//得到墙底固结时，各节点内力
        //private double M02 = neilijisuan02(K, K02, f02, n, Q, H);//得到墙底铰结时，各节点内力

        //private double MM = neilitiaofu(M01, M02, T);//进行内力调幅，得到配筋计算所需要个节点弯矩和剪力
        //private double Mmax = kuazhongzuidaM(MM, n, Q, H);//得到内力调幅后跨中最大弯矩
        //private double M = neilizuhe(n, MM, Mmax);//将负弯矩和正弯矩集成到一个列向量中，方便后续调用

        //private double As = peijinjisuan(M, cs, n, h, fy, fc, ft);//得到各节点和跨中的计算配筋值，满足最小配筋率
        //private double Ass = zuhezhengfujin(n, As, ft, fy, h, M, cs, C, rg);//得到间距150顶筋、间距200顶筋和底筋
        //private double Ashui = shuipingjin(n, h);//得到水平筋
        //private double Money = chengben(Ass, Ashui, n, Qh, Qg, h, H, cs, s, Z, C, F);//计算成本

        public Input()
        { 
        
        }

        public Input(int floorCount, double[] heights, double[] widths, int[] concreteGrades, int[] rebarGrades)
        {
            dicConcrteProperty = new Dictionary<int, double[]>();
            dicRebarProperty = new Dictionary<int, double[]>();
            Init();
            n = floorCount;
            H = heights;
            h = widths;
            C = concreteGrades;
            F = rebarGrades;  

            fc = new double[n];
            E = new double[n];
            fy = new double[n];
            A = new double[n];
            I = new double[n];
            for (int i = 0; i < n; i++)
            {
                double[] conProperties;
                dicConcrteProperty.TryGetValue(C[i], out conProperties);
                fc[i] = conProperties[0];
                E[i] = conProperties[1];
                double[] rebProperties;
                dicRebarProperty.TryGetValue(F[i], out rebProperties);
                fy[i] = rebProperties[0];

                A[i] = 1000 * h[i];
                I[i] = 1000 * Math.Pow(h[i], 3) / 12;
            }
        }

        private void Init()
        {
            dicConcrteProperty.Add(20, new double[] {  9.6, 2.55 * Math.Pow(10, 5), 1.54, 1.10 });
            dicConcrteProperty.Add(25, new double[] { 11.9, 2.80 * Math.Pow(10, 5), 1.78, 1.27 });
            dicConcrteProperty.Add(30, new double[] { 14.3, 3.00 * Math.Pow(10, 5), 2.01, 1.43 });
            dicConcrteProperty.Add(35, new double[] { 16.7, 3.15 * Math.Pow(10, 5), 2.20, 1.57 });
            dicConcrteProperty.Add(40, new double[] { 19.1, 3.25 * Math.Pow(10, 5), 2.39, 1.71 });
            dicConcrteProperty.Add(45, new double[] { 21.1, 3.35 * Math.Pow(10, 5), 2.51, 1.80 });
            dicConcrteProperty.Add(50, new double[] { 23.1, 3.45 * Math.Pow(10, 5), 2.64, 1.89 });

            dicRebarProperty.Add(300, new double[] { 270 });
            dicRebarProperty.Add(335, new double[] { 300 });
            dicRebarProperty.Add(400, new double[] { 360 });
            dicRebarProperty.Add(500, new double[] { 435 });
        }

    }
}
