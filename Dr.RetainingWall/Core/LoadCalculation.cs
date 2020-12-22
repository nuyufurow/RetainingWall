using MatrixMult;

namespace Dr.RetainingWall
{
    class LoadCalculation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">挡墙层数</param>
        /// <param name="H">每层挡墙层高</param>
        /// <param name="r">填土容重</param>
        /// <param name="h">覆土顶厚度</param>
        /// <param name="p0">地面活荷载</param>
        /// <param name="rg">恒载分项系数</param>
        /// <param name="rq">活载分项系数</param>
        /// <returns></returns>
        static Matrix hezaijisuan(int n, Matrix H, double r, double h, double p0, double rg, double rq)
        {
            Matrix Q = new Matrix();
            if (n == 1)
            {
                double qmax0g = r * 0.5 * (h + H.Values[1, 1]);    //恒载标准值最大值,静止土压力系数为0.5
                double qmin0g = r * 0.5 * h;                       //恒载标准值最小值,静止土压力系数为0.5
                double q0q = p0 * 0.5;                             //活载标准值,静止土压力系数为0.5

                double qmaxg = qmax0g * rg;                        //恒载设计值
                double qming = qmin0g * rg;                        //恒载设计值
                double qq = q0q * rq;                              //活载设计值

                double qA = qmaxg + qq;                            //A点荷载设计值
                double qB = qming + qq;                            //B点荷载设计值


                Q = new Matrix(new double[,] { { qA, qB } });
            }
            else if (n == 2)
            {
                double qmax0g = r * 0.5 * (h + H.Values[1, 1] + H.Values[1, 2]);     //恒载标准值最大值,静止土压力系数为0.5
                double qmin0g = r * 0.5 * h;                                         //恒载标准值最小值,静止土压力系数为0.5
                double q0q = p0 * 0.5;                                               //活载标准值,静止土压力系数为0.5

                double qmaxg = qmax0g * rg;                                          //恒载设计值
                double qming = qmin0g * rg;                                          //恒载设计值
                double qq = q0q * rq;                                                //活载设计值

                double qA = qmaxg + qq;                                              //A点荷载设计值
                double qB = r * 0.5 * (h + H.Values[1, 2]) * rg + qq;                //B点荷载设计值
                double qC = qming + qq;                                              //C点荷载设计值


                Q = new Matrix(new double[,] { { qA, qB, qC } });
            }
            else if (n == 3)
            {
                double qmax0g = r * 0.5 * (h + H.Values[1, 1] + H.Values[1, 2] + H.Values[1, 3]);     //恒载标准值最大值,静止土压力系数为0.5
                double qmin0g = r * 0.5 * h;                                                          //恒载标准值最小值,静止土压力系数为0.5
                double q0q = p0 * 0.5;                                                                //活载标准值,静止土压力系数为0.5

                double qmaxg = qmax0g * rg;                                                           //恒载设计值最大值
                double qming = qmin0g * rg;                                                           //恒载设计值最小值
                double qq = q0q * rq;                                                                 //活载设计值

                double qA = qmaxg + qq;                                                               //A点荷载设计值
                double qB = r * 0.5 * (h + H.Values[1, 2] + H.Values[1, 3]) * rg + qq;                //B点荷载设计值
                double qC = r * 0.5 * (h + H.Values[1, 3]) * rg + qq;                                 //C点荷载设计值
                double qD = qming + qq;                                                               //D点荷载设计值


                Q = new Matrix(new double[,] { { qA, qB, qC, qD } });
            }
            else if (n == 4)
            {
                double qmax0g = r * 0.5 * (h + H.Values[1, 1] + H.Values[1, 2] + H.Values[1, 3]);        //恒载标准值最大值,静止土压力系数为0.5
                double qmin0g = r * 0.5 * h;                                                             //恒载标准值最小值,静止土压力系数为0.5
                double q0q = p0 * 0.5;                                                                   //活载标准值,静止土压力系数为0.5

                double qmaxg = qmax0g * rg;                                                              //恒载设计值最大值
                double qming = qmin0g * rg;                                                              //恒载设计值最小值
                double qq = q0q * rq;                                                                    //活载设计值

                double qA = qmaxg + qq;                                                                  //A点荷载设计值
                double qB = r * 0.5 * (h + H.Values[1, 2] + H.Values[1, 3] + H.Values[1, 4]) * rg + qq;  //B点荷载设计值
                double qC = r * 0.5 * (h + H.Values[1, 3] + H.Values[1, 4]) * rg + qq;                   //C点荷载设计值
                double qD = r * 0.5 * (h + H.Values[1, 4]) * rg + qq;                                    //D点荷载设计值
                double qE = qming + qq;                                                                  //E点荷载设计值


                Q = new Matrix(new double[,] { { qA, qB, qC, qD, qE } });
            }

            return Q;
        }

        /// <summary> 
        /// 输出等效节点荷载设计值，弯矩以逆时针为正
        /// 此子函数用于墙底固结
        /// </summary>
        /// <param name="n"></param>
        /// <param name="H"></param>
        /// <param name="Q"></param>
        /// <returns></returns>
        static Matrix dengxiaojiedianhezai01(int n, Matrix H, Matrix Q)
        {
            Matrix f = new Matrix();
            if (n == 1)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);   //A点等效节点弯矩
                double Mb = (Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12;      //B点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                     //A点等效节点剪力
                double Fb = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1];                     //B点等效节点剪力
                f.Values = new double[,] { { 0, Mb } };
            }
            else if (n == 2)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);   //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12)
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));         //B点等效节点弯矩
                double Mc = (Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12;      //C点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                     //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1]
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                          //B点等效节点剪力
                double Fc = (Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2];                     //C点等效节点剪力
                f.Values = new double[,] { { 0, Mb, 0, Mc } };
            }
            else if (n == 3)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);    //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12)
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));          //B点等效节点弯矩
                double Mc = -(((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12)
                    - ((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 20 + Q[1, 4] * H[1, 3] * H[1, 3] / 12));          //C点等效节点弯矩
                double Md = (Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 30 + Q[1, 4] * H[1, 3] * H[1, 3] / 12;       //D点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                      //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1]
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                           //B点等效节点剪力
                double Fc = -((Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2]
                    + (Q[1, 3] - Q[1, 4]) * H[1, 3] * 7 / 20 + 0.5 * Q[1, 4] * H[1, 3]);                           //C点等效节点剪力
                double Fd = (Q[1, 3] - Q[1, 4]) * H[1, 3] * 3 / 20 + 0.5 * Q[1, 4] * H[1, 3];                      //D点等效节点剪力
                f.Values = new double[,] { { 0, Mb, 0, Mc, 0, Md } };
            }
            else if (n == 4)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);    //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12)
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));          //B点等效节点弯矩
                double Mc = -(((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12)
                    - ((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 20 + Q[1, 4] * H[1, 3] * H[1, 3] / 12));          //C点等效节点弯矩
                double Md = -(((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 30 + Q[1, 4] * H[1, 3] * H[1, 3] / 12)
                    - ((Q[1, 4] - Q[1, 5]) * H[1, 4] * H[1, 4] / 20 + Q[1, 5] * H[1, 4] * H[1, 4] / 12));          //D点等效节点弯矩
                double Me = (Q[1, 4] - Q[1, 5]) * H[1, 4] * H[1, 4] / 30 + Q[1, 5] * H[1, 4] * H[1, 4] / 12;       //E点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                      //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1]
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                           //B点等效节点剪力
                double Fc = -((Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2]
                    + (Q[1, 3] - Q[1, 4]) * H[1, 3] * 7 / 20 + 0.5 * Q[1, 4] * H[1, 3]);                           //C点等效节点剪力
                double Fd = -((Q[1, 3] - Q[1, 4]) * H[1, 3] * 3 / 20 + 0.5 * Q[1, 4] * H[1, 3]
                    + (Q[1, 4] - Q[1, 5]) * H[1, 4] * 7 / 20 + 0.5 * Q[1, 5] * H[1, 4]);                           //D点等效节点剪力
                double Fe = (Q[1, 4] - Q[1, 5]) * H[1, 4] * 3 / 20 + 0.5 * Q[1, 5] * H[1, 4];                      //E点等效节点剪力
                f.Values = new double[,] { { 0, Mb, 0, Mc, 0, Md, 0, Me } };
            }

            return f;
        }

        static Matrix dengxiaojiedianhezai02(int n, Matrix H, Matrix Q)
        {
            Matrix f = new Matrix();
            if (n == 1)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);     //A点等效节点弯矩
                double Mb = (Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12;        //B点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                       //A点等效节点剪力
                double Fb = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1];                       //B点等效节点剪力
                f.Values = new double[,] { { Ma, 0, Mb } };
            }
            else if (n == 2)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);      //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12)
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));            //B点等效节点弯矩
                double Mc = (Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12;         //C点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                        //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1]
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                             //B点等效节点剪力
                double Fc = (Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2];                        //C点等效节点剪力
                f.Values = new double[,] { { Ma, 0, Mb, 0, Mc } };
            }
            else if (n == 3)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);       //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12)
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));             //B点等效节点弯矩
                double Mc = -(((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12)
                    - ((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 20 + Q[1, 4] * H[1, 3] * H[1, 3] / 12));             //C点等效节点弯矩
                double Md = (Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 30 + Q[1, 4] * H[1, 3] * H[1, 3] / 12;          //D点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                         //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1]
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                              //B点等效节点剪力
                double Fc = -((Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2]
                    + (Q[1, 3] - Q[1, 4]) * H[1, 3] * 7 / 20 + 0.5 * Q[1, 4] * H[1, 3]);                              //C点等效节点剪力
                double Fd = (Q[1, 3] - Q[1, 4]) * H[1, 3] * 3 / 20 + 0.5 * Q[1, 4] * H[1, 3];                         //D点等效节点剪力
                f.Values = new double[,] { { Ma, 0, Mb, 0, Mc, 0, Md } };
            }
            else if (n == 4)
            {
                double Ma = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 20 + Q[1, 2] * H[1, 1] * H[1, 1] / 12);       //A点等效节点弯矩
                double Mb = -(((Q[1, 1] - Q[1, 2]) * H[1, 1] * H[1, 1] / 30 + Q[1, 2] * H[1, 1] * H[1, 1] / 12) 
                    - ((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 20 + Q[1, 3] * H[1, 2] * H[1, 2] / 12));             //B点等效节点弯矩
                double Mc = -(((Q[1, 2] - Q[1, 3]) * H[1, 2] * H[1, 2] / 30 + Q[1, 3] * H[1, 2] * H[1, 2] / 12) 
                    - ((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 20 + Q[1, 4] * H[1, 3] * H[1, 3] / 12));             //C点等效节点弯矩
                double Md = -(((Q[1, 3] - Q[1, 4]) * H[1, 3] * H[1, 3] / 30 + Q[1, 4] * H[1, 3] * H[1, 3] / 12) 
                    - ((Q[1, 4] - Q[1, 5]) * H[1, 4] * H[1, 4] / 20 + Q[1, 5] * H[1, 4] * H[1, 4] / 12));             //D点等效节点弯矩
                double Me = (Q[1, 4] - Q[1, 5]) * H[1, 4] * H[1, 4] / 30 + Q[1, 5] * H[1, 4] * H[1, 4] / 12;          //E点等效节点弯矩
                double Fa = (Q[1, 1] - Q[1, 2]) * H[1, 1] * 7 / 20 + 0.5 * Q[1, 2] * H[1, 1];                         //A点等效节点剪力
                double Fb = -((Q[1, 1] - Q[1, 2]) * H[1, 1] * 3 / 20 + 0.5 * Q[1, 2] * H[1, 1] 
                    + (Q[1, 2] - Q[1, 3]) * H[1, 2] * 7 / 20 + 0.5 * Q[1, 3] * H[1, 2]);                              //B点等效节点剪力
                double Fc = -((Q[1, 2] - Q[1, 3]) * H[1, 2] * 3 / 20 + 0.5 * Q[1, 3] * H[1, 2] 
                    + (Q[1, 3] - Q[1, 4]) * H[1, 3] * 7 / 20 + 0.5 * Q[1, 4] * H[1, 3]);                              //C点等效节点剪力
                double Fd = -((Q[1, 3] - Q[1, 4]) * H[1, 3] * 3 / 20 + 0.5 * Q[1, 4] * H[1, 3] 
                    + (Q[1, 4] - Q[1, 5]) * H[1, 4] * 7 / 20 + 0.5 * Q[1, 5] * H[1, 4]);                              //D点等效节点剪力
                double Fe = (Q[1, 4] - Q[1, 5]) * H[1, 4] * 3 / 20 + 0.5 * Q[1, 5] * H[1, 4];                         //E点等效节点剪力
                f.Values = new double[,] { { Ma, 0, Mb, 0, Mc, 0, Md, 0, Me } };
            }

            return f;
        }


    }
}
