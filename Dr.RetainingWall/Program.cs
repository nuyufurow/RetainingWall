using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Matrix a = new double[,]{ { 1, 2 }, { 3, 4 } };
            //Vector b = new Vector(new double[] { 5, 6 }, VectorType.Column);
            //var c = Vector.MatMulColVec(a, b);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
