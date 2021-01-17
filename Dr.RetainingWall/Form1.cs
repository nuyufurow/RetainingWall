using System;
using System.Windows.Forms;
using LinearAlgebra;
using LinearAlgebra.VectorAlgebra;

namespace Dr.RetainingWall
{
    public partial class mainForm : Form
    {
        private Output output = new Output();
        private Input input = new Input();
        public mainForm()
        {
            InitializeComponent();

            dgvInput.Rows.Add();
            dgvInput.Rows[0].Cells[0].Value = "4000";
            dgvInput.Rows[0].Cells[1].Value = "300";
            dgvInput.Rows[0].Cells[2].Value = "30";
            dgvInput.Rows[0].Cells[3].Value = "400";

            tbRongZhong.Text = input.r.ToString();
            tbFutuHoudu.Text = input.fh.ToString();
            tbHuohezai.Text = input.p0.ToString();
            tbHengzaiXishu.Text = input.rg.ToString();
            tbHuozaiXishu.Text = input.rq.ToString();
            tbT.Text = input.T.ToString();
        }

        private Input GetInputParameters()
        {
            Input input = new Input();
            int rowCounts = dgvInput.Rows.Count;
            double[] heights = new double[rowCounts];
            double[] widths = new double[rowCounts];
            int[] concreteGrades = new int[rowCounts];
            int[] rebarGrades = new int[rowCounts];
            if (rowCounts > 0 && rowCounts < 5)
            {
                for (int i = 0; i < rowCounts - 1; i++)
                {
                    string strHeight = dgvInput.Rows[i].Cells[0].Value.ToString();
                    string strWidth = dgvInput.Rows[i].Cells[1].Value.ToString();
                    string strConcreteGrade = dgvInput.Rows[i].Cells[2].Value.ToString();
                    string strRebarGrade = dgvInput.Rows[i].Cells[3].Value.ToString();
                    heights[i] = Util.ToDouble(strHeight);
                    widths[i] = Util.ToDouble(strWidth);
                    concreteGrades[i] = Util.ToInt(strConcreteGrade);
                    rebarGrades[i] = Util.ToInt(strRebarGrade);
                }
                input = new Input(rowCounts - 1, heights, widths, concreteGrades, rebarGrades);
            }
            else if (rowCounts >= 5)
            {
                MessageBox.Show("目前该软件只支持4层(含)以下挡土墙计算。");
                return null;
            }
            else
            {
                MessageBox.Show("请在表格中输入参数。");
                return null;
            }

            input.r = Util.ToDouble(tbRongZhong.Text);
            input.fh = Util.ToDouble(tbFutuHoudu.Text);
            input.p0 = Util.ToDouble(tbHuohezai.Text);
            input.rg = Util.ToDouble(tbHengzaiXishu.Text);
            input.rq = Util.ToDouble(tbHuozaiXishu.Text);
            return input;
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

        private void btnUnitStiffnessMatrix_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            Input ip = GetInputParameters();
            for (int i = 0; i < ip.n; i++)
            {
                Matrix mUnitK = StiffnessMatrix.PlaneFrameElementStiffness(ip.E[i], ip.A[i], ip.I[i], ip.H[i]);
                string strUnitK = mUnitK.ToString("e");
                rtbBrowser.AppendText("第" + (i + 1) + "层挡土墙单元刚度矩阵：\n");
                rtbBrowser.AppendText(strUnitK);
            }
        }

        private void btnTotalStiffnessMatrix_Click(object sender, EventArgs e)
        {
            rtbBrowser.AppendText("\n");
            Input ip = GetInputParameters();
            Matrix mK = StiffnessMatrix.zonggangjuzhen(ip.n, ip.E, ip.A, ip.I, ip.H);
            output.totalStiffness = mK;
            string strK = mK.ToString("e");
            rtbBrowser.AppendText("总刚度矩阵：\n");
            rtbBrowser.AppendText(strK);
        }

        private void btnLoadCalculation_Click(object sender, EventArgs e)
        {
            Input ip = GetInputParameters();
            if (ip == null)
            {
                return;
            }
            double[] mLoad = LoadCalculation.hezaijisuan(ip.n, ip.H, ip.r, ip.fh, ip.p0, ip.rg, ip.rq);
            output.Q = mLoad;

            rtbBrowser.AppendText("\n");
            string strLoad = Util.ToString(mLoad);
            rtbBrowser.AppendText("荷载：\n");
            rtbBrowser.AppendText(strLoad);
        }


        private void btnEquivalentNodeLoad_Click(object sender, EventArgs e)
        {
            Input ip = GetInputParameters();
            if (ip == null)
            {
                return;
            }
            double[] mLoad = LoadCalculation.hezaijisuan(ip.n, ip.H, ip.r, ip.fh, ip.p0, ip.rg, ip.rq);
            double[] f01 = LoadCalculation.dengxiaojiedianhezai01(ip.n, ip.H, mLoad);
            double[] f02 = LoadCalculation.dengxiaojiedianhezai02(ip.n, ip.H, mLoad);
            output.f01 = new Vector(f01, VectorType.Column);
            output.f02 = new Vector(f02, VectorType.Column);

            rtbBrowser.AppendText("\n");
            string strF01 = Util.ToString(f01);
            string strF02 = Util.ToString(f02);
            rtbBrowser.AppendText("等效节点荷载：\n");
            rtbBrowser.AppendText("f1:" + strF01 + "\n");
            rtbBrowser.AppendText("f2:" + strF02);
        }

        private void btnSimplifiedTotalStiffness_Click(object sender, EventArgs e)
        {
            Matrix K01 = BoundaryCondition.weiyibianjie01(input.n, output.totalStiffness);
            Matrix K02 = BoundaryCondition.weiyibianjie02(input.n, output.totalStiffness);
            output.K01 = K01;
            output.K02 = K02;

            rtbBrowser.AppendText("\n");
            string strK01 = K01.ToString();
            string strK02 = K02.ToString();
            rtbBrowser.AppendText("划行划列后的刚度矩阵：\n");
            rtbBrowser.AppendText("位移边界条件1：\n");
            rtbBrowser.AppendText(strK01 + "\n");
            rtbBrowser.AppendText("位移边界条件2：\n");
            rtbBrowser.AppendText(strK02);
        }

        private void btnInnerForceCal_Click(object sender, EventArgs e)
        {
            Vector M01 = InnerForceCalculation.neilijisuan01(output.totalStiffness, output.K01, output.f01, input.n, output.Q, input.H);
            Vector M02 = InnerForceCalculation.neilijisuan02(output.totalStiffness, output.K02, output.f02, input.n, output.Q, input.H);
            output.M01 = M01;
            output.M02 = M02;

            rtbBrowser.AppendText("\n");
            string strM01 = M01.ToString();
            string strM02 = M02.ToString();
            rtbBrowser.AppendText("内力计算：\n");
            rtbBrowser.AppendText("M01：\n");
            rtbBrowser.AppendText(strM01 + "\n");
            rtbBrowser.AppendText("M02：\n");
            rtbBrowser.AppendText(strM01);

        }

        private void btnInnerForceTiaofu_Click(object sender, EventArgs e)
        {
            Vector MM = InnerForceCalculation.neilitiaofu(output.M01, output.M02, input.T);
            output.MM = MM;

            rtbBrowser.AppendText("\n");
            string strMM = MM.ToString();
            rtbBrowser.AppendText("内力调幅：\n");
            rtbBrowser.AppendText(strMM);
        }

        private void btnMaxM_Click(object sender, EventArgs e)
        {
            Vector Mmax = InnerForceCalculation.kuazhongzuidaM(output.MM, input.n, output.Q, input.H);
            output.Mmax = Mmax;

            rtbBrowser.AppendText("\n");
            string strMmax = Mmax.ToString();
            rtbBrowser.AppendText("最大跨中弯矩：\n");
            rtbBrowser.AppendText(strMmax);
        }

        private void btnInnerForceZuhe_Click(object sender, EventArgs e)
        {
            Vector M = InnerForceCalculation.neilizuhe(input.n, output.MM, output.Mmax);
            output.M = M;

            rtbBrowser.AppendText("\n");
            string strM = M.ToString();
            rtbBrowser.AppendText("内力组合：\n");
            rtbBrowser.AppendText(strM);
        }
        private void btnClearBrowse_Click(object sender, EventArgs e)
        {
            rtbBrowser.Clear();
        }
    }
}
