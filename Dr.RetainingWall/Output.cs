using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;
using System.Collections.Generic;

namespace Dr.RetainingWall
{
    class Output
    {
        public Matrix m_K;
        public double[] m_Q;
        public Vector m_f01;
        public Vector m_f02;
        public Matrix m_K01;
        public Matrix m_K02;
        public Matrix m_M01;
        public Matrix m_M02;
        public Matrix m_MM;
        public double[] m_Mmax;
        public double[] m_M;
        public List<double>[] m_As;

        public List<double[]> m_Zuhejin;
        public List<double[]> m_Shuipingjin;
    }
}
