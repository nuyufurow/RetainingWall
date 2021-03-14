using System;
using System.Windows.Forms;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;


namespace Dr.RetainingWall
{
    public partial class mainForm : Form
    {
        private Input input = new Input();
        private Output output = new Output();
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            dgvInput.Rows.Add();
            dgvInput.Rows[0].Cells[0].Value = "4000";
            cmbConcreteGrade.Text = input.m_ConcreteGrade.ToString();
            cmbRebarGrade.Text = input.m_RebarGrade.ToString();
            tbConcretePrice.Text = input.m_ConcretePrice.ToString();
            tbRebarPrice.Text = input.m_RebarPrice.ToString();
            tbRongZhong.Text = input.m_r.ToString();
            tbFutuThickness.Text = input.m_FutuHeight.ToString();
            tbHuohezai.Text = input.m_p0.ToString();
            tbHengzaiXishu.Text = input.m_rg.ToString();
            tbHuozaiXishu.Text = input.m_rq.ToString();
            tbT.Text = input.m_T.ToString();
            cmbSeismicGrade.Text = input.m_SeismicGrade.ToString();
            tbRoofThickness.Text = input.m_RoofThickness.ToString();
            tbCs.Text = input.m_cs.ToString();
        }

        private void RefreshInput()
        {
            int rowCounts = dgvInput.Rows.Count - 1;
            double[] heights = new double[rowCounts];
            if (rowCounts > 0 && rowCounts < 4)
            {
                for (int i = 0; i < rowCounts; i++)
                {
                    string strHeight = dgvInput.Rows[i].Cells[0].Value.ToString();
                    heights[i] = Util.ToDouble(strHeight);
                }
            }
            else if (rowCounts >= 4)
            {
                MessageBox.Show("目前该软件只支持4层(含)以下挡土墙计算。");
            }
            else
            {
                MessageBox.Show("请在表格中输入参数。");
            }
            int concreteGrade = Util.ToInt(cmbConcreteGrade.SelectedItem.ToString());
            int rebarGrade = Util.ToInt(cmbRebarGrade.SelectedItem.ToString());
            input = new Input(rowCounts, heights, concreteGrade, rebarGrade);
            input.m_ConcretePrice = Util.ToDouble(tbConcretePrice.Text);
            input.m_RebarPrice = Util.ToDouble(tbRebarPrice.Text);

            input.m_r = Util.ToDouble(tbRongZhong.Text);
            input.m_FutuHeight = Util.ToDouble(tbFutuThickness.Text);
            input.m_p0 = Util.ToDouble(tbHuohezai.Text);
            input.m_rg = Util.ToDouble(tbHengzaiXishu.Text);
            input.m_rq = Util.ToDouble(tbHuozaiXishu.Text);
            input.m_T = Util.ToDouble(tbT.Text);
            input.m_SeismicGrade = Util.ToInt(cmbSeismicGrade.Text);
            input.m_RoofThickness = Util.ToDouble(tbRoofThickness.Text);
            input.m_cs = Util.ToDouble(tbCs.Text);
        }
        private void btnClearBrowse_Click(object sender, EventArgs e)
        {
            rtbBrowser.Clear();
        }

        private void dgvInput_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            int rowCounts = dgvInput.Rows.Count;
            if (rowCounts > 5)
            {
                MessageBox.Show("软件只支持最多四层当土墙设计,多余参数无效。");
                return;
            }
            if (rowCounts > 1 && rowCounts < 6)
            {
                for (int i = 1; i < rowCounts; i++)
                {
                    dgvInput.Rows[i - 1].HeaderCell.Value = i.ToString() + "层";
                }
            }

        }

        private void btnTotalStiffnessMatrix_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();

            input.SetWallWidth(new double[]{ 250 });
            Matrix mK = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_K = mK;

            string strK = mK.ToString("e");
            rtbBrowser.AppendText("总刚度矩阵：\n");
            rtbBrowser.AppendText(strK);
        }

        private void btnLoadCalculation_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();

            double[] mLoad = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            output.m_Q = mLoad;

            string strLoad = Util.ToString(mLoad);
            rtbBrowser.AppendText("荷载：\n");
            rtbBrowser.AppendText(strLoad);
        }


        private void btnEquivalentNodeLoad_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);

            string strF01 = Util.ToString(f01);
            string strF02 = Util.ToString(f02);
            rtbBrowser.AppendText("等效节点荷载：\n");
            rtbBrowser.AppendText("f1:" + strF01 + "\n");
            rtbBrowser.AppendText("f2:" + strF02);
        }

        private void btnSimplifiedTotalStiffness_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);

            Matrix K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            Matrix K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_K01 = K01;
            output.m_K02 = K02;

            string strK01 = K01.ToString("e");
            string strK02 = K02.ToString("e");
            rtbBrowser.AppendText("划行划列后的刚度矩阵：\n");
            rtbBrowser.AppendText("位移边界条件1：\n");
            rtbBrowser.AppendText(strK01 + "\n");
            rtbBrowser.AppendText("位移边界条件2：\n");
            rtbBrowser.AppendText(strK02);
        }

        private void btnInnerForceCal_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);

            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);

            Matrix M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            Matrix M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M01 = M01;
            output.m_M02 = M02;

            string strM01 = M01.ToString("e");
            string strM02 = M02.ToString("e");
            rtbBrowser.AppendText("内力计算：\n");
            rtbBrowser.AppendText("M01：\n");
            rtbBrowser.AppendText(strM01 + "\n");
            rtbBrowser.AppendText("M02：\n");
            rtbBrowser.AppendText(strM02);

        }

        private void btnInnerForceTiaofu_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            Matrix MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);
            output.m_MM = MM;

            string strMM = MM.ToString("e");
            rtbBrowser.AppendText("内力调幅：\n");
            rtbBrowser.AppendText(strMM);
        }

        private void btnMaxM_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);

            double[] Mmax = InnerForceCalculation.kuazhongzuidaM(output.m_MM, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_Mmax = Mmax;

            string strMmax = Util.ToString(Mmax);
            rtbBrowser.AppendText("最大跨中弯矩：\n");
            rtbBrowser.AppendText(strMmax);
        }

        private void btnInnerForceZuhe_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);
            output.m_Mmax = InnerForceCalculation.kuazhongzuidaM(output.m_MM, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            double[] M = InnerForceCalculation.neilizuhe(input.m_FloorCount, output.m_MM, output.m_Mmax);
            output.m_M = M;

            string strM = Util.ToString(M);
            rtbBrowser.AppendText("内力组合：\n");
            rtbBrowser.AppendText(strM);
        }

        private void btnReinforcementCalculation_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 300 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);
            output.m_Mmax = InnerForceCalculation.kuazhongzuidaM(output.m_MM, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_M = InnerForceCalculation.neilizuhe(input.m_FloorCount, output.m_MM, output.m_Mmax);

            double[][] As = ReinforcementCalculation.peijinjisuan(
                output.m_M, input.m_cs, input.m_FloorCount, input.m_WallWidths, input.m_fy, input.m_fc, input.m_ft);
            output.m_As = As;

            //string strAs = Util.ToString(As[0]);
            //rtbBrowser.AppendText("配筋计算：\n");
            //rtbBrowser.AppendText(strAs);
        }

        private void btnZuhezhengfujin_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);
            output.m_Mmax = InnerForceCalculation.kuazhongzuidaM(output.m_MM, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_M = InnerForceCalculation.neilizuhe(input.m_FloorCount, output.m_MM, output.m_Mmax);

            double[][] As = ReinforcementCalculation.peijinjisuan(
                output.m_M, input.m_cs, input.m_FloorCount, input.m_WallWidths, input.m_fy, input.m_fc, input.m_ft);
            output.m_As = As;

            double[][] zuhejin = Zuhezhengfujin.zuhezhengfujin(input.m_FloorCount, output.m_As[0], input.m_ft, input.m_fy, input.m_WallWidths, output.m_M, input.m_cs, input.m_ConcreteGrade, input.m_rg);
            output.m_Zuhejin = zuhejin;

            rtbBrowser.AppendText("组合筋计算完毕：\n");

        }

        private void btnShuipingjin_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            RefreshInput();
            input.SetWallWidth(new double[] { 350, 250 });

            output.m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
            output.m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, output.m_Q);
            output.m_f01 = new Vector(f01, VectorType.Column);
            output.m_f02 = new Vector(f02, VectorType.Column);
            output.m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, output.m_K);
            output.m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, output.m_K);
            output.m_M01 = InnerForceCalculation.neilijisuan01(
                input.m_E, input.m_A, input.m_I, output.m_K01, output.m_f01, input.m_FloorCount, output.m_Q, input.m_FloorHeights);
            output.m_M02 = InnerForceCalculation.neilijisuan02(
                input.m_E, input.m_A, input.m_I, output.m_K02, output.m_f02, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_MM = InnerForceCalculation.neilitiaofu(output.m_M01, output.m_M02, input.m_T);
            output.m_Mmax = InnerForceCalculation.kuazhongzuidaM(output.m_MM, input.m_FloorCount, output.m_Q, input.m_FloorHeights);

            output.m_M = InnerForceCalculation.neilizuhe(input.m_FloorCount, output.m_MM, output.m_Mmax);

            double[][] As = ReinforcementCalculation.peijinjisuan(
                output.m_M, input.m_cs, input.m_FloorCount, input.m_WallWidths, input.m_fy, input.m_fc, input.m_ft);
            output.m_As = As;

            double[][] zuhejin = Zuhezhengfujin.zuhezhengfujin(input.m_FloorCount, output.m_As[0], input.m_ft, input.m_fy, input.m_WallWidths, output.m_M, input.m_cs, input.m_ConcreteGrade, input.m_rg);
            output.m_Zuhejin = zuhejin;

            double[] shuipingjin = Shuipingjin.shuipingjin(input.m_FloorCount, input.m_WallWidths);
            output.m_Shuipingjin = shuipingjin;

            string strShuipingjin = Util.ToString(shuipingjin);
            rtbBrowser.AppendText("水平筋计算：\n");
            rtbBrowser.AppendText(strShuipingjin);
        }

        private void btnChengben_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");

            double[] cheng = Chengben.chengben(output.m_Zuhejin, output.m_Shuipingjin, input.m_FloorCount, input.m_ConcretePrice,
                input.m_RebarPrice, input.m_WallWidths, input.m_FloorHeights, input.m_cs, input.m_RoofThickness,
                input.m_SeismicGrade, input.m_ConcreteGrade, input.m_RebarGrade);

            string strChengben = Util.ToString(cheng);
            rtbBrowser.AppendText("成本计算：\n");
            rtbBrowser.AppendText(strChengben);
        }
    }
}
