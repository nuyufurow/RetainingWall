using System;
using System.Windows.Forms;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;
using System.Collections.Generic;

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
            dgvInput.Rows[0].Cells[0].Value = input.m_FloorHeights[0];
            dgvInput.Rows[0].Cells[1].Value = input.m_RoofThickness[0];
            dgvInput.Rows.Add();
            dgvInput.Rows[1].Cells[0].Value = input.m_FloorHeights[1];
            dgvInput.Rows[1].Cells[1].Value = input.m_RoofThickness[1];
            dgvInput.Rows.Add();
            dgvInput.Rows[2].Cells[0].Value = input.m_FloorHeights[2];
            dgvInput.Rows[2].Cells[1].Value = input.m_RoofThickness[2];
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
            tbBarSpace.Text = input.m_BarSpace.ToString();
            tbCs.Text = input.m_cs.ToString();
        }

        private void RefreshInput()
        {
            int rowCounts = dgvInput.Rows.Count - 1;
            double[] heights = new double[rowCounts];
            double[] roofs = new double[rowCounts];
            if (rowCounts > 0 && rowCounts < 4)
            {
                for (int i = 0; i < rowCounts; i++)
                {
                    string strHeight = dgvInput.Rows[i].Cells[0].Value.ToString();
                    string strRoof = dgvInput.Rows[i].Cells[1].Value.ToString();
                    heights[i] = Util.ToDouble(strHeight);
                    roofs[i] = Util.ToDouble(strRoof);
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
            input.m_RoofThickness = roofs;
            input.m_BarSpace = Util.ToDouble(tbBarSpace.Text);
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

        private void btnChengben_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            rtbBrowser.AppendText("计算结果：[厚度][成本]");
            RefreshInput();

            List<double[]> wallWidths = WallWidth.Genereate(input.m_FloorCount);

            for (int i = 0 ; i < wallWidths.Count; i++)
            {
                input.SetWallWidth(wallWidths[i]);

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

                List<double>[] As = ReinforcementCalculation.peijinjisuan(
                    output.m_M, input.m_cs, input.m_FloorCount, input.m_WallWidths, input.m_fy, input.m_fc, input.m_ft);
                output.m_As = As;

                List<double[]> zuhejin = Zuhezhengfujin.zuhezhengfujin(input.m_FloorCount, output.m_As[0], input.m_ft, input.m_fy, input.m_WallWidths, output.m_M, input.m_cs, input.m_ConcreteGrade, input.m_rg);
                output.m_Zuhejin = zuhejin;

                List<double[]> shuipingjin = Shuipingjin.shuipingjin(input.m_FloorCount, input.m_WallWidths);
                output.m_Shuipingjin = shuipingjin;

                double[] cheng = Chengben.chengben(output.m_Zuhejin, output.m_Shuipingjin, input.m_FloorCount, input.m_ConcretePrice,
                    input.m_RebarPrice, input.m_WallWidths, input.m_FloorHeights, input.m_cs, input.m_RoofThickness,
                    input.m_SeismicGrade, input.m_ConcreteGrade, input.m_RebarGrade);

                string strWidths = Util.ToString(wallWidths[i]);
                string strChengben = Util.ToString(cheng);
                rtbBrowser.AppendText("\n");
                rtbBrowser.AppendText(strWidths);
                rtbBrowser.AppendText(strChengben);

            }
        }

    }
}
