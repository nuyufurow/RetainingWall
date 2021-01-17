using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    class Output
    {
        public Matrix totalStiffness;
        public Matrix K01;
        public Matrix K02;

        public Vector f01;
        public Vector f02;

        public double[] Q;

        public Vector M01;
        public Vector M02;
        public Vector MM;
        public Vector Mmax;
        public Vector M;

    }
}
