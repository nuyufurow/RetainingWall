using System;
using System.Collections.Generic;
using LinearAlgebra;


namespace Dr.RetainingWall
{
    class Input
    {
        private Dictionary<int, double[]> m_DicConcrteProperty;  //混凝土属性集
        private Dictionary<int, double[]> m_DicRebarProperty;    //钢筋属性集
        public int m_ConcreteGrade = 30;                        //混凝土等级
        public int m_RebarGrade = 400;                          //钢筋等级
        public double m_E;                                      //混凝土弹性模量
        public double m_fc;                                     //抗压强度设计值
        public double m_ft;                                     //抗拉强度设计值
        public double m_fy;                                     //钢筋抗拉强度设计值
        public double m_ConcretePrice = 300;                    //混凝土每方价格
        public double m_RebarPrice = 4000;                      //钢筋每吨价格

        public int m_FloorCount = 1;                            //层数
        public double[] m_FloorHeights = { 4000 };              //层高
        public double[] m_WallWidths;                           //墙厚
        public double[] m_A;                                    //截面积
        public double[] m_I;                                    //截面惯性矩

        public double m_r = 0.00002;                            //填土容重N/mm3
        public double m_FutuHeight = 1500;                      //墙顶覆土厚度mm
        public double m_p0 = 0.005;                             //活荷载N/mm2
        public double m_rg = 1.3;                               //恒荷载分项系数
        public double m_rq = 1.5;                               //活荷载分项系数
        public double m_T = 0.75;                               //内力调幅系数

        public double[] m_SeismicGrade;                       //抗震等级
        public double m_RoofThickness = 160;                    //地下室顶板厚度
        public double m_cs = 30;                                //保护层厚度

        private void Init()
        {
            m_DicConcrteProperty.Add(20, new double[] {  9.6, 2.55 * Math.Pow(10, 5), 1.54, 1.10 });
            m_DicConcrteProperty.Add(25, new double[] { 11.9, 2.80 * Math.Pow(10, 5), 1.78, 1.27 });
            m_DicConcrteProperty.Add(30, new double[] { 14.3, 3.00 * Math.Pow(10, 5), 2.01, 1.43 });
            m_DicConcrteProperty.Add(35, new double[] { 16.7, 3.15 * Math.Pow(10, 5), 2.20, 1.57 });
            m_DicConcrteProperty.Add(40, new double[] { 19.1, 3.25 * Math.Pow(10, 5), 2.39, 1.71 });
            m_DicConcrteProperty.Add(45, new double[] { 21.1, 3.35 * Math.Pow(10, 5), 2.51, 1.80 });
            m_DicConcrteProperty.Add(50, new double[] { 23.1, 3.45 * Math.Pow(10, 5), 2.64, 1.89 });

            m_DicRebarProperty.Add(300, new double[] { 270 });
            m_DicRebarProperty.Add(335, new double[] { 300 });
            m_DicRebarProperty.Add(400, new double[] { 360 });
            m_DicRebarProperty.Add(500, new double[] { 435 });
        }

        public Input() { }

        public Input(int n, double[] heights, int concreteGrade, int rebarGrade)
        {
            m_DicConcrteProperty = new Dictionary<int, double[]>();
            m_DicRebarProperty = new Dictionary<int, double[]>();
            Init();
            m_ConcreteGrade = concreteGrade;
            m_RebarGrade = rebarGrade;
            double[] conProperties;
            m_DicConcrteProperty.TryGetValue(concreteGrade, out conProperties);
            m_fc = conProperties[0];
            m_ft = conProperties[3];
            m_E = conProperties[1];
            double[] rebProperties;
            m_DicRebarProperty.TryGetValue(rebarGrade, out rebProperties);
            m_fy = rebProperties[0];

            m_FloorCount = n;
            m_FloorHeights = heights;
        }

        public void SetWallWidth(double[] widths)
        {
            m_WallWidths = widths;
            int cout = widths.Length;
            m_A = new double[cout];
            m_I = new double[cout];
            for (int i = 0; i< cout; i++)
            {
                m_A[i] = 1000 * widths[i];
                m_I[i] = 1000 * Math.Pow(widths[i], 3) / 12.0;
            }
        }

    }
}
