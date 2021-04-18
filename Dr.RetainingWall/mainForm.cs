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
            //dgvInput.Rows.Add();
            //dgvInput.Rows[3].Cells[0].Value = input.m_FloorHeights[3];
            //dgvInput.Rows[3].Cells[1].Value = input.m_RoofThickness[3];
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
            if (rowCounts > 0 && rowCounts <= 4)
            {
                for (int i = 0; i < rowCounts; i++)
                {
                    string strHeight = dgvInput.Rows[i].Cells[0].Value.ToString();
                    string strRoof = dgvInput.Rows[i].Cells[1].Value.ToString();
                    heights[i] = Util.ToDouble(strHeight);
                    roofs[i] = Util.ToDouble(strRoof);
                }
            }
            else if (rowCounts > 4)
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

        private void pritResult(Output[] outputs)
        {
            int n = input.m_FloorCount;

            double min150 = 10000;
            double min200 = 10000;
            int index150 = 0, index200 = 0;
            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i].m_Chengben.Length < 8)
                {
                    continue;
                }

                if (outputs[i].m_Chengben[6] < min150)
                {
                    min150 = outputs[i].m_Chengben[6];
                    index150 = i;
                }

                if (outputs[i].m_Chengben[7] < min200)
                {
                    min200 = outputs[i].m_Chengben[7];
                    index200 = i;
                }
            }
            rtbBrowser.AppendText("========================================================\n");
            rtbBrowser.AppendText("150mm间距最优方案：\n");
            string strWidths = Util.ToString(outputs[index150].m_WallWidths);
            rtbBrowser.AppendText("墙厚：\n" + "  " + strWidths + "\n");
            List<double[]> listZuhejin = outputs[index150].m_Zuhejin;
            rtbBrowser.AppendText("负筋：\n");
            for (int i = 0; i < listZuhejin.Count; i++)
            {
                if (i < n + 1)
                {

                    string strPeijin = Util.ToString(listZuhejin[i]);
                    rtbBrowser.AppendText("  " + strPeijin + "\n");
                }
            }
            rtbBrowser.AppendText("正筋：\n");
            for (int i = 0; i < listZuhejin.Count; i++)
            {
                if (i > 2 * n + 1 && i < 4 * n - 1)
                {

                    string strPeijin = Util.ToString(listZuhejin[i]);
                    rtbBrowser.AppendText("  " + strPeijin + "\n");
                }
            }

            rtbBrowser.AppendText("水平筋：\n");
            foreach (var shuipingjin in outputs[index150].m_Shuipingjin)
            {
                string strPeijin = Util.ToString(shuipingjin);
                rtbBrowser.AppendText("  " + strPeijin + "\n");
            }

            rtbBrowser.AppendText("成本：\n  [顶筋 底筋 水平筋 混凝土 总计] (单位：元)\n");
            double[] cb150 = {
                outputs[index150].m_Chengben[0],
                outputs[index150].m_Chengben[2],
                outputs[index150].m_Chengben[4],
                outputs[index150].m_Chengben[5],
                outputs[index150].m_Chengben[6]};
            string strCb150 = Util.ToString(cb150);
            rtbBrowser.AppendText("  " + strCb150 + "\n");

            rtbBrowser.AppendText("\n========================================================\n");
            rtbBrowser.AppendText("200mm间距最优方案：\n");
            string strWidths200 = Util.ToString(outputs[index200].m_WallWidths);
            rtbBrowser.AppendText("墙厚：\n" + "  " + strWidths200 + "\n");
            List<double[]> listZuhejin200 = outputs[index200].m_Zuhejin;
            rtbBrowser.AppendText("负筋：\n");
            for (int i = 0; i < listZuhejin200.Count; i++)
            {
                if (i > n && i < 2 * n + 2)
                {

                    string strPeijin = Util.ToString(listZuhejin200[i]);
                    rtbBrowser.AppendText("  " + strPeijin + "\n");
                }
            }
            rtbBrowser.AppendText("正筋：\n");
            for (int i = 0; i < listZuhejin200.Count; i++)
            {
                if (i > 3 * n + 1)
                {

                    string strPeijin = Util.ToString(listZuhejin200[i]);
                    rtbBrowser.AppendText("  " + strPeijin + "\n");
                }
            }

            rtbBrowser.AppendText("水平筋：\n");
            foreach (var shuipingjin in outputs[index200].m_Shuipingjin)
            {
                string strPeijin = Util.ToString(shuipingjin);
                rtbBrowser.AppendText("  " + strPeijin + "\n");
            }

            rtbBrowser.AppendText("成本：\n  [顶筋 底筋 水平筋 混凝土 总计] (单位：元)\n");
            double[] cb200 = {
                outputs[index200].m_Chengben[1],
                outputs[index200].m_Chengben[3],
                outputs[index200].m_Chengben[4],
                outputs[index200].m_Chengben[5],
                outputs[index200].m_Chengben[7]};
            string strCb200 = Util.ToString(cb200);
            rtbBrowser.AppendText("  " + strCb200 + "\n");

        }

        private void btnChengben_Click(object sender, EventArgs e)
        {
            RefreshInput();

            List<double[]> wallWidths = WallWidth.Genereate(input.m_FloorCount);
            Output[] outputs = new Output[wallWidths.Count];

            for (int i = 0; i < wallWidths.Count; i++)
            {
                input.SetWallWidth(wallWidths[i]);
                outputs[i] = new Output();
                outputs[i].SetWallWidth(wallWidths[i]);

                outputs[i].m_K = StiffnessMatrix.zonggangjuzhen(input.m_FloorCount, input.m_E, input.m_A, input.m_I, input.m_FloorHeights);
                outputs[i].m_Q = LoadCalculation.hezaijisuan(input.m_FloorCount, input.m_FloorHeights, input.m_r, input.m_FutuHeight, input.m_p0, input.m_rg, input.m_rq);
                double[] f01 = LoadCalculation.dengxiaojiedianhezai01(input.m_FloorCount, input.m_FloorHeights, outputs[i].m_Q);
                double[] f02 = LoadCalculation.dengxiaojiedianhezai02(input.m_FloorCount, input.m_FloorHeights, outputs[i].m_Q);
                outputs[i].m_f01 = new Vector(f01, VectorType.Column);
                outputs[i].m_f02 = new Vector(f02, VectorType.Column);
                outputs[i].m_K01 = BoundaryCondition.weiyibianjie01(input.m_FloorCount, outputs[i].m_K);
                outputs[i].m_K02 = BoundaryCondition.weiyibianjie02(input.m_FloorCount, outputs[i].m_K);
                outputs[i].m_M01 = InnerForceCalculation.neilijisuan(false,
                    input.m_E, input.m_A, input.m_I, outputs[i].m_K01, outputs[i].m_f01, input.m_FloorCount, outputs[i].m_Q, input.m_FloorHeights);
                outputs[i].m_M02 = InnerForceCalculation.neilijisuan(true,
                    input.m_E, input.m_A, input.m_I, outputs[i].m_K02, outputs[i].m_f02, input.m_FloorCount, outputs[i].m_Q, input.m_FloorHeights);

                outputs[i].m_MM = InnerForceCalculation.neilitiaofu(outputs[i].m_M01, outputs[i].m_M02, input.m_T);
                outputs[i].m_Mmax = InnerForceCalculation.kuazhongzuidaM(outputs[i].m_MM, input.m_FloorCount, outputs[i].m_Q, input.m_FloorHeights);

                outputs[i].m_M = InnerForceCalculation.neilizuhe(input.m_FloorCount, outputs[i].m_MM, outputs[i].m_Mmax);

                List<double>[] As = ReinforcementCalculation.peijinjisuan(
                    outputs[i].m_M, input.m_cs, input.m_FloorCount, input.m_WallWidths, input.m_fy, input.m_fc, input.m_ft);
                outputs[i].m_As = As;

                List<double[]> zuhejin = Zuhezhengfujin.zuhezhengfujin(input.m_FloorCount, outputs[i].m_As[0], input.m_ft, input.m_fy, input.m_WallWidths, outputs[i].m_M, input.m_cs, input.m_ConcreteGrade, input.m_rg);
                outputs[i].m_Zuhejin = zuhejin;

                List<double[]> shuipingjin = Shuipingjin.shuipingjin(input.m_FloorCount, input.m_WallWidths);
                outputs[i].m_Shuipingjin = shuipingjin;

                double[] cb = Chengben.chengben(outputs[i].m_Zuhejin, outputs[i].m_Shuipingjin, input.m_FloorCount, input.m_ConcretePrice,
                    input.m_RebarPrice, input.m_WallWidths, input.m_FloorHeights, input.m_cs, input.m_RoofThickness,
                    input.m_SeismicGrade, input.m_ConcreteGrade, input.m_RebarGrade);
                outputs[i].m_Chengben = cb;

                if (cbxShowDetial.Checked == true)
                {
                    string strWidths = Util.ToString(wallWidths[i]);
                    string strChengben = Util.ToString(cb);
                    rtbBrowser.AppendText(strWidths);
                    rtbBrowser.AppendText(strChengben);
                    rtbBrowser.AppendText("\n");
                }
            }

            pritResult(outputs);
        }

    }
}
