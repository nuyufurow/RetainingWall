using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;
using System.Collections.Generic;

namespace Dr.RetainingWall
{
    class Output
    {
        public double[] m_WallWidths;                           //墙厚

        public Matrix m_K;
        public double[] m_Q;
        public Vector m_f01;
        public Vector m_f02;
        public Matrix m_K01;
        public Matrix m_K02;
        public double[,] m_M01;
        public double[,] m_M02;
        public Matrix m_MM;
        public double[] m_Mmax;
        public double[] m_M;
        public List<double>[] m_As;

        public List<double[]> m_Zuhejin;
        public List<double[]> m_Shuipingjin;

        public double[] m_Chengben;

        public void SetWallWidth(double[] widths)
        {
            m_WallWidths = widths;
        }


        public Output() { }
    }
}
