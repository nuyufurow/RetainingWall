namespace Dr.RetainingWall
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.floorHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wallWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConcreteGrade = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RebarGrade = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rtbBrowser = new System.Windows.Forms.RichTextBox();
            this.btnUnitStiffnessMatrix = new System.Windows.Forms.Button();
            this.btnTotalStiffnessMatrix = new System.Windows.Forms.Button();
            this.btnLoadCalculation = new System.Windows.Forms.Button();
            this.labRongzhong1 = new System.Windows.Forms.Label();
            this.tbRongZhong = new System.Windows.Forms.TextBox();
            this.labFutuHoudu1 = new System.Windows.Forms.Label();
            this.labHuohezai1 = new System.Windows.Forms.Label();
            this.labHenzaiXishu = new System.Windows.Forms.Label();
            this.labRongzhong2 = new System.Windows.Forms.Label();
            this.labFutuHoudu2 = new System.Windows.Forms.Label();
            this.tbFutuHoudu = new System.Windows.Forms.TextBox();
            this.tbHuohezai = new System.Windows.Forms.TextBox();
            this.labHuozaiXishu = new System.Windows.Forms.Label();
            this.tbHengzaiXishu = new System.Windows.Forms.TextBox();
            this.tbHuozaiXishu = new System.Windows.Forms.TextBox();
            this.labHuohezai2 = new System.Windows.Forms.Label();
            this.btnClearBrowse = new System.Windows.Forms.Button();
            this.btnEquivalentNodeLoad = new System.Windows.Forms.Button();
            this.btnSimplifiedTotalStiffness = new System.Windows.Forms.Button();
            this.btnInnerForceCal = new System.Windows.Forms.Button();
            this.lbT = new System.Windows.Forms.Label();
            this.tbT = new System.Windows.Forms.TextBox();
            this.btnInnerForceTiaofu = new System.Windows.Forms.Button();
            this.btnMaxM = new System.Windows.Forms.Button();
            this.btnInnerForceZuhe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInput
            // 
            this.dgvInput.AllowDrop = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.floorHeight,
            this.wallWidth,
            this.ConcreteGrade,
            this.RebarGrade});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInput.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInput.Location = new System.Drawing.Point(0, 0);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInput.RowHeadersWidth = 60;
            this.dgvInput.Size = new System.Drawing.Size(473, 180);
            this.dgvInput.TabIndex = 3;
            this.dgvInput.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvInput_RowStateChanged);
            // 
            // floorHeight
            // 
            this.floorHeight.Frozen = true;
            this.floorHeight.HeaderText = "层高(mm)";
            this.floorHeight.Name = "floorHeight";
            // 
            // wallWidth
            // 
            this.wallWidth.Frozen = true;
            this.wallWidth.HeaderText = "墙厚(mm)";
            this.wallWidth.Name = "wallWidth";
            // 
            // ConcreteGrade
            // 
            this.ConcreteGrade.Frozen = true;
            this.ConcreteGrade.HeaderText = "混凝土等级";
            this.ConcreteGrade.Items.AddRange(new object[] {
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.ConcreteGrade.Name = "ConcreteGrade";
            this.ConcreteGrade.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ConcreteGrade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RebarGrade
            // 
            this.RebarGrade.Frozen = true;
            this.RebarGrade.HeaderText = "钢筋等级";
            this.RebarGrade.Items.AddRange(new object[] {
            "300",
            "335",
            "400",
            "500"});
            this.RebarGrade.Name = "RebarGrade";
            this.RebarGrade.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RebarGrade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rtbBrowser
            // 
            this.rtbBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbBrowser.Location = new System.Drawing.Point(0, 261);
            this.rtbBrowser.Name = "rtbBrowser";
            this.rtbBrowser.ReadOnly = true;
            this.rtbBrowser.Size = new System.Drawing.Size(984, 300);
            this.rtbBrowser.TabIndex = 5;
            this.rtbBrowser.Text = "";
            // 
            // btnUnitStiffnessMatrix
            // 
            this.btnUnitStiffnessMatrix.Location = new System.Drawing.Point(15, 191);
            this.btnUnitStiffnessMatrix.Name = "btnUnitStiffnessMatrix";
            this.btnUnitStiffnessMatrix.Size = new System.Drawing.Size(100, 23);
            this.btnUnitStiffnessMatrix.TabIndex = 6;
            this.btnUnitStiffnessMatrix.Text = "单元刚度矩阵";
            this.btnUnitStiffnessMatrix.UseVisualStyleBackColor = true;
            this.btnUnitStiffnessMatrix.Click += new System.EventHandler(this.btnUnitStiffnessMatrix_Click);
            // 
            // btnTotalStiffnessMatrix
            // 
            this.btnTotalStiffnessMatrix.Location = new System.Drawing.Point(123, 191);
            this.btnTotalStiffnessMatrix.Name = "btnTotalStiffnessMatrix";
            this.btnTotalStiffnessMatrix.Size = new System.Drawing.Size(100, 23);
            this.btnTotalStiffnessMatrix.TabIndex = 7;
            this.btnTotalStiffnessMatrix.Text = "总刚度矩阵";
            this.btnTotalStiffnessMatrix.UseVisualStyleBackColor = true;
            this.btnTotalStiffnessMatrix.Click += new System.EventHandler(this.btnTotalStiffnessMatrix_Click);
            // 
            // btnLoadCalculation
            // 
            this.btnLoadCalculation.Location = new System.Drawing.Point(231, 191);
            this.btnLoadCalculation.Name = "btnLoadCalculation";
            this.btnLoadCalculation.Size = new System.Drawing.Size(100, 23);
            this.btnLoadCalculation.TabIndex = 8;
            this.btnLoadCalculation.Text = "荷载计算";
            this.btnLoadCalculation.UseVisualStyleBackColor = true;
            this.btnLoadCalculation.Click += new System.EventHandler(this.btnLoadCalculation_Click);
            // 
            // labRongzhong1
            // 
            this.labRongzhong1.AutoSize = true;
            this.labRongzhong1.Location = new System.Drawing.Point(513, 13);
            this.labRongzhong1.Name = "labRongzhong1";
            this.labRongzhong1.Size = new System.Drawing.Size(77, 12);
            this.labRongzhong1.TabIndex = 9;
            this.labRongzhong1.Text = "填土容重(r):";
            // 
            // tbRongZhong
            // 
            this.tbRongZhong.Location = new System.Drawing.Point(615, 9);
            this.tbRongZhong.Name = "tbRongZhong";
            this.tbRongZhong.Size = new System.Drawing.Size(100, 21);
            this.tbRongZhong.TabIndex = 10;
            // 
            // labFutuHoudu1
            // 
            this.labFutuHoudu1.AutoSize = true;
            this.labFutuHoudu1.Location = new System.Drawing.Point(513, 43);
            this.labFutuHoudu1.Name = "labFutuHoudu1";
            this.labFutuHoudu1.Size = new System.Drawing.Size(83, 12);
            this.labFutuHoudu1.TabIndex = 11;
            this.labFutuHoudu1.Text = "墙顶覆土厚度:";
            // 
            // labHuohezai1
            // 
            this.labHuohezai1.AutoSize = true;
            this.labHuohezai1.Location = new System.Drawing.Point(513, 73);
            this.labHuohezai1.Name = "labHuohezai1";
            this.labHuohezai1.Size = new System.Drawing.Size(95, 12);
            this.labHuohezai1.TabIndex = 12;
            this.labHuohezai1.Text = "地面活荷载(p0):";
            // 
            // labHenzaiXishu
            // 
            this.labHenzaiXishu.AutoSize = true;
            this.labHenzaiXishu.Location = new System.Drawing.Point(513, 103);
            this.labHenzaiXishu.Name = "labHenzaiXishu";
            this.labHenzaiXishu.Size = new System.Drawing.Size(107, 12);
            this.labHenzaiXishu.TabIndex = 13;
            this.labHenzaiXishu.Text = "恒载分项系数(rg):";
            // 
            // labRongzhong2
            // 
            this.labRongzhong2.AutoSize = true;
            this.labRongzhong2.Location = new System.Drawing.Point(722, 13);
            this.labRongzhong2.Name = "labRongzhong2";
            this.labRongzhong2.Size = new System.Drawing.Size(35, 12);
            this.labRongzhong2.TabIndex = 14;
            this.labRongzhong2.Text = "N/mm3";
            // 
            // labFutuHoudu2
            // 
            this.labFutuHoudu2.AutoSize = true;
            this.labFutuHoudu2.Location = new System.Drawing.Point(722, 43);
            this.labFutuHoudu2.Name = "labFutuHoudu2";
            this.labFutuHoudu2.Size = new System.Drawing.Size(17, 12);
            this.labFutuHoudu2.TabIndex = 15;
            this.labFutuHoudu2.Text = "mm";
            // 
            // tbFutuHoudu
            // 
            this.tbFutuHoudu.Location = new System.Drawing.Point(615, 39);
            this.tbFutuHoudu.Name = "tbFutuHoudu";
            this.tbFutuHoudu.Size = new System.Drawing.Size(100, 21);
            this.tbFutuHoudu.TabIndex = 16;
            // 
            // tbHuohezai
            // 
            this.tbHuohezai.Location = new System.Drawing.Point(615, 69);
            this.tbHuohezai.Name = "tbHuohezai";
            this.tbHuohezai.Size = new System.Drawing.Size(100, 21);
            this.tbHuohezai.TabIndex = 17;
            // 
            // labHuozaiXishu
            // 
            this.labHuozaiXishu.AutoSize = true;
            this.labHuozaiXishu.Location = new System.Drawing.Point(513, 133);
            this.labHuozaiXishu.Name = "labHuozaiXishu";
            this.labHuozaiXishu.Size = new System.Drawing.Size(107, 12);
            this.labHuozaiXishu.TabIndex = 18;
            this.labHuozaiXishu.Text = "活载分项系数(rq):";
            // 
            // tbHengzaiXishu
            // 
            this.tbHengzaiXishu.Location = new System.Drawing.Point(615, 99);
            this.tbHengzaiXishu.Name = "tbHengzaiXishu";
            this.tbHengzaiXishu.Size = new System.Drawing.Size(100, 21);
            this.tbHengzaiXishu.TabIndex = 19;
            // 
            // tbHuozaiXishu
            // 
            this.tbHuozaiXishu.Location = new System.Drawing.Point(615, 129);
            this.tbHuozaiXishu.Name = "tbHuozaiXishu";
            this.tbHuozaiXishu.Size = new System.Drawing.Size(100, 21);
            this.tbHuozaiXishu.TabIndex = 20;
            // 
            // labHuohezai2
            // 
            this.labHuohezai2.AutoSize = true;
            this.labHuohezai2.Location = new System.Drawing.Point(722, 73);
            this.labHuohezai2.Name = "labHuohezai2";
            this.labHuohezai2.Size = new System.Drawing.Size(35, 12);
            this.labHuohezai2.TabIndex = 21;
            this.labHuohezai2.Text = "N/mm2";
            // 
            // btnClearBrowse
            // 
            this.btnClearBrowse.Location = new System.Drawing.Point(640, 191);
            this.btnClearBrowse.Name = "btnClearBrowse";
            this.btnClearBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnClearBrowse.TabIndex = 22;
            this.btnClearBrowse.Text = "清屏";
            this.btnClearBrowse.UseVisualStyleBackColor = true;
            this.btnClearBrowse.Click += new System.EventHandler(this.btnClearBrowse_Click);
            // 
            // btnEquivalentNodeLoad
            // 
            this.btnEquivalentNodeLoad.Location = new System.Drawing.Point(339, 191);
            this.btnEquivalentNodeLoad.Name = "btnEquivalentNodeLoad";
            this.btnEquivalentNodeLoad.Size = new System.Drawing.Size(100, 23);
            this.btnEquivalentNodeLoad.TabIndex = 23;
            this.btnEquivalentNodeLoad.Text = "等效节点荷载";
            this.btnEquivalentNodeLoad.UseVisualStyleBackColor = true;
            this.btnEquivalentNodeLoad.Click += new System.EventHandler(this.btnEquivalentNodeLoad_Click);
            // 
            // btnSimplifiedTotalStiffness
            // 
            this.btnSimplifiedTotalStiffness.Location = new System.Drawing.Point(447, 191);
            this.btnSimplifiedTotalStiffness.Name = "btnSimplifiedTotalStiffness";
            this.btnSimplifiedTotalStiffness.Size = new System.Drawing.Size(100, 23);
            this.btnSimplifiedTotalStiffness.TabIndex = 24;
            this.btnSimplifiedTotalStiffness.Text = "化简后总刚";
            this.btnSimplifiedTotalStiffness.UseVisualStyleBackColor = true;
            this.btnSimplifiedTotalStiffness.Click += new System.EventHandler(this.btnSimplifiedTotalStiffness_Click);
            // 
            // btnInnerForceCal
            // 
            this.btnInnerForceCal.Location = new System.Drawing.Point(15, 224);
            this.btnInnerForceCal.Name = "btnInnerForceCal";
            this.btnInnerForceCal.Size = new System.Drawing.Size(100, 23);
            this.btnInnerForceCal.TabIndex = 25;
            this.btnInnerForceCal.Text = "内力计算";
            this.btnInnerForceCal.UseVisualStyleBackColor = true;
            this.btnInnerForceCal.Click += new System.EventHandler(this.btnInnerForceCal_Click);
            // 
            // lbT
            // 
            this.lbT.AutoSize = true;
            this.lbT.Location = new System.Drawing.Point(513, 163);
            this.lbT.Name = "lbT";
            this.lbT.Size = new System.Drawing.Size(101, 12);
            this.lbT.TabIndex = 26;
            this.lbT.Text = "内力调幅系数(T):";
            // 
            // tbT
            // 
            this.tbT.Location = new System.Drawing.Point(615, 159);
            this.tbT.Name = "tbT";
            this.tbT.Size = new System.Drawing.Size(100, 21);
            this.tbT.TabIndex = 27;
            // 
            // btnInnerForceTiaofu
            // 
            this.btnInnerForceTiaofu.Location = new System.Drawing.Point(123, 224);
            this.btnInnerForceTiaofu.Name = "btnInnerForceTiaofu";
            this.btnInnerForceTiaofu.Size = new System.Drawing.Size(100, 23);
            this.btnInnerForceTiaofu.TabIndex = 28;
            this.btnInnerForceTiaofu.Text = "内力调幅";
            this.btnInnerForceTiaofu.UseVisualStyleBackColor = true;
            this.btnInnerForceTiaofu.Click += new System.EventHandler(this.btnInnerForceTiaofu_Click);
            // 
            // btnMaxM
            // 
            this.btnMaxM.Location = new System.Drawing.Point(231, 224);
            this.btnMaxM.Name = "btnMaxM";
            this.btnMaxM.Size = new System.Drawing.Size(100, 23);
            this.btnMaxM.TabIndex = 29;
            this.btnMaxM.Text = "跨中最大弯矩";
            this.btnMaxM.UseVisualStyleBackColor = true;
            this.btnMaxM.Click += new System.EventHandler(this.btnMaxM_Click);
            // 
            // btnInnerForceZuhe
            // 
            this.btnInnerForceZuhe.Location = new System.Drawing.Point(339, 224);
            this.btnInnerForceZuhe.Name = "btnInnerForceZuhe";
            this.btnInnerForceZuhe.Size = new System.Drawing.Size(100, 23);
            this.btnInnerForceZuhe.TabIndex = 30;
            this.btnInnerForceZuhe.Text = "内力组合";
            this.btnInnerForceZuhe.UseVisualStyleBackColor = true;
            this.btnInnerForceZuhe.Click += new System.EventHandler(this.btnInnerForceZuhe_Click);
            // 
            // mainForm
            // 
            this.AccessibleName = "floor";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnInnerForceZuhe);
            this.Controls.Add(this.btnMaxM);
            this.Controls.Add(this.btnInnerForceTiaofu);
            this.Controls.Add(this.tbT);
            this.Controls.Add(this.lbT);
            this.Controls.Add(this.btnInnerForceCal);
            this.Controls.Add(this.btnSimplifiedTotalStiffness);
            this.Controls.Add(this.btnEquivalentNodeLoad);
            this.Controls.Add(this.btnClearBrowse);
            this.Controls.Add(this.labHuohezai2);
            this.Controls.Add(this.tbHuozaiXishu);
            this.Controls.Add(this.tbHengzaiXishu);
            this.Controls.Add(this.labHuozaiXishu);
            this.Controls.Add(this.tbHuohezai);
            this.Controls.Add(this.tbFutuHoudu);
            this.Controls.Add(this.labFutuHoudu2);
            this.Controls.Add(this.labRongzhong2);
            this.Controls.Add(this.labHenzaiXishu);
            this.Controls.Add(this.labHuohezai1);
            this.Controls.Add(this.labFutuHoudu1);
            this.Controls.Add(this.tbRongZhong);
            this.Controls.Add(this.labRongzhong1);
            this.Controls.Add(this.btnLoadCalculation);
            this.Controls.Add(this.btnTotalStiffnessMatrix);
            this.Controls.Add(this.btnUnitStiffnessMatrix);
            this.Controls.Add(this.rtbBrowser);
            this.Controls.Add(this.dgvInput);
            this.Name = "mainForm";
            this.Text = "挡土墙计算";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.RichTextBox rtbBrowser;
        private System.Windows.Forms.Button btnUnitStiffnessMatrix;
        private System.Windows.Forms.Button btnTotalStiffnessMatrix;
        private System.Windows.Forms.Button btnLoadCalculation;
        private System.Windows.Forms.DataGridViewTextBoxColumn floorHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn wallWidth;
        private System.Windows.Forms.DataGridViewComboBoxColumn ConcreteGrade;
        private System.Windows.Forms.DataGridViewComboBoxColumn RebarGrade;
        private System.Windows.Forms.Label labRongzhong1;
        private System.Windows.Forms.TextBox tbRongZhong;
        private System.Windows.Forms.Label labFutuHoudu1;
        private System.Windows.Forms.Label labHuohezai1;
        private System.Windows.Forms.Label labHenzaiXishu;
        private System.Windows.Forms.Label labRongzhong2;
        private System.Windows.Forms.Label labFutuHoudu2;
        private System.Windows.Forms.TextBox tbFutuHoudu;
        private System.Windows.Forms.TextBox tbHuohezai;
        private System.Windows.Forms.Label labHuozaiXishu;
        private System.Windows.Forms.TextBox tbHengzaiXishu;
        private System.Windows.Forms.TextBox tbHuozaiXishu;
        private System.Windows.Forms.Label labHuohezai2;
        private System.Windows.Forms.Button btnClearBrowse;
        private System.Windows.Forms.Button btnEquivalentNodeLoad;
        private System.Windows.Forms.Button btnSimplifiedTotalStiffness;
        private System.Windows.Forms.Button btnInnerForceCal;
        private System.Windows.Forms.Label lbT;
        private System.Windows.Forms.TextBox tbT;
        private System.Windows.Forms.Button btnInnerForceTiaofu;
        private System.Windows.Forms.Button btnMaxM;
        private System.Windows.Forms.Button btnInnerForceZuhe;
    }
}

