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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.floorHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtbBrowser = new System.Windows.Forms.RichTextBox();
            this.labRongzhong1 = new System.Windows.Forms.Label();
            this.tbRongZhong = new System.Windows.Forms.TextBox();
            this.labFutuThickness = new System.Windows.Forms.Label();
            this.labHuohezai = new System.Windows.Forms.Label();
            this.labHengzaiXishu = new System.Windows.Forms.Label();
            this.labRongzhong2 = new System.Windows.Forms.Label();
            this.labFutuThickness2 = new System.Windows.Forms.Label();
            this.tbFutuThickness = new System.Windows.Forms.TextBox();
            this.tbHuohezai = new System.Windows.Forms.TextBox();
            this.labHuozaiXishu = new System.Windows.Forms.Label();
            this.tbHengzaiXishu = new System.Windows.Forms.TextBox();
            this.tbHuozaiXishu = new System.Windows.Forms.TextBox();
            this.labHuohezai2 = new System.Windows.Forms.Label();
            this.btnClearBrowse = new System.Windows.Forms.Button();
            this.lbT = new System.Windows.Forms.Label();
            this.tbT = new System.Windows.Forms.TextBox();
            this.labConcreteGrade = new System.Windows.Forms.Label();
            this.cmbConcreteGrade = new System.Windows.Forms.ComboBox();
            this.labRebarGrade = new System.Windows.Forms.Label();
            this.cmbRebarGrade = new System.Windows.Forms.ComboBox();
            this.labConcretePrice = new System.Windows.Forms.Label();
            this.tbConcretePrice = new System.Windows.Forms.TextBox();
            this.labConcretePrice2 = new System.Windows.Forms.Label();
            this.labRebarPrice = new System.Windows.Forms.Label();
            this.tbRebarPrice = new System.Windows.Forms.TextBox();
            this.labRebarPrice2 = new System.Windows.Forms.Label();
            this.labSeismicGrade = new System.Windows.Forms.Label();
            this.cmbSeismicGrade = new System.Windows.Forms.ComboBox();
            this.labBarSpace = new System.Windows.Forms.Label();
            this.tbBarSpace = new System.Windows.Forms.TextBox();
            this.labRoofThickness2 = new System.Windows.Forms.Label();
            this.labCs = new System.Windows.Forms.Label();
            this.tbCs = new System.Windows.Forms.TextBox();
            this.labCs2 = new System.Windows.Forms.Label();
            this.btnChengben = new System.Windows.Forms.Button();
            this.cbxShowDetial = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInput
            // 
            this.dgvInput.AllowDrop = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.floorHeight,
            this.s});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInput.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvInput.Location = new System.Drawing.Point(2, 0);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvInput.RowHeadersWidth = 60;
            this.dgvInput.Size = new System.Drawing.Size(323, 160);
            this.dgvInput.TabIndex = 3;
            this.dgvInput.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvInput_RowStateChanged);
            // 
            // floorHeight
            // 
            this.floorHeight.Frozen = true;
            this.floorHeight.HeaderText = "层高(mm)";
            this.floorHeight.Name = "floorHeight";
            this.floorHeight.Width = 130;
            // 
            // s
            // 
            this.s.HeaderText = "地下室顶板厚(mm)";
            this.s.Name = "s";
            this.s.Width = 130;
            // 
            // rtbBrowser
            // 
            this.rtbBrowser.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtbBrowser.Location = new System.Drawing.Point(484, 0);
            this.rtbBrowser.Name = "rtbBrowser";
            this.rtbBrowser.ReadOnly = true;
            this.rtbBrowser.Size = new System.Drawing.Size(597, 561);
            this.rtbBrowser.TabIndex = 5;
            this.rtbBrowser.Text = "";
            // 
            // labRongzhong1
            // 
            this.labRongzhong1.AutoSize = true;
            this.labRongzhong1.Location = new System.Drawing.Point(15, 304);
            this.labRongzhong1.Name = "labRongzhong1";
            this.labRongzhong1.Size = new System.Drawing.Size(77, 12);
            this.labRongzhong1.TabIndex = 9;
            this.labRongzhong1.Text = "填土容重(r):";
            // 
            // tbRongZhong
            // 
            this.tbRongZhong.Location = new System.Drawing.Point(122, 298);
            this.tbRongZhong.Name = "tbRongZhong";
            this.tbRongZhong.Size = new System.Drawing.Size(100, 21);
            this.tbRongZhong.TabIndex = 10;
            // 
            // labFutuThickness
            // 
            this.labFutuThickness.AutoSize = true;
            this.labFutuThickness.Location = new System.Drawing.Point(15, 332);
            this.labFutuThickness.Name = "labFutuThickness";
            this.labFutuThickness.Size = new System.Drawing.Size(83, 12);
            this.labFutuThickness.TabIndex = 11;
            this.labFutuThickness.Text = "墙顶覆土厚度:";
            // 
            // labHuohezai
            // 
            this.labHuohezai.AutoSize = true;
            this.labHuohezai.Location = new System.Drawing.Point(15, 360);
            this.labHuohezai.Name = "labHuohezai";
            this.labHuohezai.Size = new System.Drawing.Size(95, 12);
            this.labHuohezai.TabIndex = 12;
            this.labHuohezai.Text = "地面活荷载(p0):";
            // 
            // labHengzaiXishu
            // 
            this.labHengzaiXishu.AutoSize = true;
            this.labHengzaiXishu.Location = new System.Drawing.Point(15, 388);
            this.labHengzaiXishu.Name = "labHengzaiXishu";
            this.labHengzaiXishu.Size = new System.Drawing.Size(107, 12);
            this.labHengzaiXishu.TabIndex = 13;
            this.labHengzaiXishu.Text = "恒载分项系数(rg):";
            // 
            // labRongzhong2
            // 
            this.labRongzhong2.AutoSize = true;
            this.labRongzhong2.Location = new System.Drawing.Point(228, 304);
            this.labRongzhong2.Name = "labRongzhong2";
            this.labRongzhong2.Size = new System.Drawing.Size(35, 12);
            this.labRongzhong2.TabIndex = 14;
            this.labRongzhong2.Text = "N/mm3";
            // 
            // labFutuThickness2
            // 
            this.labFutuThickness2.AutoSize = true;
            this.labFutuThickness2.Location = new System.Drawing.Point(228, 329);
            this.labFutuThickness2.Name = "labFutuThickness2";
            this.labFutuThickness2.Size = new System.Drawing.Size(17, 12);
            this.labFutuThickness2.TabIndex = 15;
            this.labFutuThickness2.Text = "mm";
            // 
            // tbFutuThickness
            // 
            this.tbFutuThickness.Location = new System.Drawing.Point(122, 326);
            this.tbFutuThickness.Name = "tbFutuThickness";
            this.tbFutuThickness.Size = new System.Drawing.Size(100, 21);
            this.tbFutuThickness.TabIndex = 16;
            // 
            // tbHuohezai
            // 
            this.tbHuohezai.Location = new System.Drawing.Point(122, 354);
            this.tbHuohezai.Name = "tbHuohezai";
            this.tbHuohezai.Size = new System.Drawing.Size(100, 21);
            this.tbHuohezai.TabIndex = 17;
            // 
            // labHuozaiXishu
            // 
            this.labHuozaiXishu.AutoSize = true;
            this.labHuozaiXishu.Location = new System.Drawing.Point(15, 416);
            this.labHuozaiXishu.Name = "labHuozaiXishu";
            this.labHuozaiXishu.Size = new System.Drawing.Size(107, 12);
            this.labHuozaiXishu.TabIndex = 18;
            this.labHuozaiXishu.Text = "活载分项系数(rq):";
            // 
            // tbHengzaiXishu
            // 
            this.tbHengzaiXishu.Location = new System.Drawing.Point(122, 382);
            this.tbHengzaiXishu.Name = "tbHengzaiXishu";
            this.tbHengzaiXishu.Size = new System.Drawing.Size(100, 21);
            this.tbHengzaiXishu.TabIndex = 19;
            // 
            // tbHuozaiXishu
            // 
            this.tbHuozaiXishu.Location = new System.Drawing.Point(122, 410);
            this.tbHuozaiXishu.Name = "tbHuozaiXishu";
            this.tbHuozaiXishu.Size = new System.Drawing.Size(100, 21);
            this.tbHuozaiXishu.TabIndex = 20;
            // 
            // labHuohezai2
            // 
            this.labHuohezai2.AutoSize = true;
            this.labHuohezai2.Location = new System.Drawing.Point(228, 360);
            this.labHuohezai2.Name = "labHuohezai2";
            this.labHuohezai2.Size = new System.Drawing.Size(35, 12);
            this.labHuohezai2.TabIndex = 21;
            this.labHuohezai2.Text = "N/mm2";
            // 
            // btnClearBrowse
            // 
            this.btnClearBrowse.Location = new System.Drawing.Point(364, 12);
            this.btnClearBrowse.Name = "btnClearBrowse";
            this.btnClearBrowse.Size = new System.Drawing.Size(100, 23);
            this.btnClearBrowse.TabIndex = 22;
            this.btnClearBrowse.Text = "清屏";
            this.btnClearBrowse.UseVisualStyleBackColor = true;
            this.btnClearBrowse.Click += new System.EventHandler(this.btnClearBrowse_Click);
            // 
            // lbT
            // 
            this.lbT.AutoSize = true;
            this.lbT.Location = new System.Drawing.Point(15, 444);
            this.lbT.Name = "lbT";
            this.lbT.Size = new System.Drawing.Size(101, 12);
            this.lbT.TabIndex = 26;
            this.lbT.Text = "内力调幅系数(T):";
            // 
            // tbT
            // 
            this.tbT.Location = new System.Drawing.Point(122, 438);
            this.tbT.Name = "tbT";
            this.tbT.Size = new System.Drawing.Size(100, 21);
            this.tbT.TabIndex = 27;
            // 
            // labConcreteGrade
            // 
            this.labConcreteGrade.AutoSize = true;
            this.labConcreteGrade.Location = new System.Drawing.Point(15, 192);
            this.labConcreteGrade.Name = "labConcreteGrade";
            this.labConcreteGrade.Size = new System.Drawing.Size(71, 12);
            this.labConcreteGrade.TabIndex = 31;
            this.labConcreteGrade.Text = "混凝土等级:";
            // 
            // cmbConcreteGrade
            // 
            this.cmbConcreteGrade.FormattingEnabled = true;
            this.cmbConcreteGrade.Items.AddRange(new object[] {
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.cmbConcreteGrade.Location = new System.Drawing.Point(122, 188);
            this.cmbConcreteGrade.Name = "cmbConcreteGrade";
            this.cmbConcreteGrade.Size = new System.Drawing.Size(100, 20);
            this.cmbConcreteGrade.TabIndex = 32;
            // 
            // labRebarGrade
            // 
            this.labRebarGrade.AutoSize = true;
            this.labRebarGrade.Location = new System.Drawing.Point(15, 220);
            this.labRebarGrade.Name = "labRebarGrade";
            this.labRebarGrade.Size = new System.Drawing.Size(59, 12);
            this.labRebarGrade.TabIndex = 33;
            this.labRebarGrade.Text = "钢筋等级:";
            // 
            // cmbRebarGrade
            // 
            this.cmbRebarGrade.FormattingEnabled = true;
            this.cmbRebarGrade.Items.AddRange(new object[] {
            "300",
            "335",
            "400",
            "500"});
            this.cmbRebarGrade.Location = new System.Drawing.Point(122, 215);
            this.cmbRebarGrade.Name = "cmbRebarGrade";
            this.cmbRebarGrade.Size = new System.Drawing.Size(100, 20);
            this.cmbRebarGrade.TabIndex = 34;
            // 
            // labConcretePrice
            // 
            this.labConcretePrice.AutoSize = true;
            this.labConcretePrice.Location = new System.Drawing.Point(15, 248);
            this.labConcretePrice.Name = "labConcretePrice";
            this.labConcretePrice.Size = new System.Drawing.Size(71, 12);
            this.labConcretePrice.TabIndex = 35;
            this.labConcretePrice.Text = "混凝土单价:";
            // 
            // tbConcretePrice
            // 
            this.tbConcretePrice.Location = new System.Drawing.Point(122, 242);
            this.tbConcretePrice.Name = "tbConcretePrice";
            this.tbConcretePrice.Size = new System.Drawing.Size(100, 21);
            this.tbConcretePrice.TabIndex = 36;
            // 
            // labConcretePrice2
            // 
            this.labConcretePrice2.AutoSize = true;
            this.labConcretePrice2.Location = new System.Drawing.Point(228, 246);
            this.labConcretePrice2.Name = "labConcretePrice2";
            this.labConcretePrice2.Size = new System.Drawing.Size(23, 12);
            this.labConcretePrice2.TabIndex = 37;
            this.labConcretePrice2.Text = "/m3";
            // 
            // labRebarPrice
            // 
            this.labRebarPrice.AutoSize = true;
            this.labRebarPrice.Location = new System.Drawing.Point(15, 276);
            this.labRebarPrice.Name = "labRebarPrice";
            this.labRebarPrice.Size = new System.Drawing.Size(59, 12);
            this.labRebarPrice.TabIndex = 38;
            this.labRebarPrice.Text = "钢筋单价:";
            // 
            // tbRebarPrice
            // 
            this.tbRebarPrice.Location = new System.Drawing.Point(122, 270);
            this.tbRebarPrice.Name = "tbRebarPrice";
            this.tbRebarPrice.Size = new System.Drawing.Size(100, 21);
            this.tbRebarPrice.TabIndex = 39;
            // 
            // labRebarPrice2
            // 
            this.labRebarPrice2.AutoSize = true;
            this.labRebarPrice2.Location = new System.Drawing.Point(228, 274);
            this.labRebarPrice2.Name = "labRebarPrice2";
            this.labRebarPrice2.Size = new System.Drawing.Size(17, 12);
            this.labRebarPrice2.TabIndex = 40;
            this.labRebarPrice2.Text = "/t";
            // 
            // labSeismicGrade
            // 
            this.labSeismicGrade.AutoSize = true;
            this.labSeismicGrade.Location = new System.Drawing.Point(15, 472);
            this.labSeismicGrade.Name = "labSeismicGrade";
            this.labSeismicGrade.Size = new System.Drawing.Size(59, 12);
            this.labSeismicGrade.TabIndex = 41;
            this.labSeismicGrade.Text = "抗震等级:";
            // 
            // cmbSeismicGrade
            // 
            this.cmbSeismicGrade.FormattingEnabled = true;
            this.cmbSeismicGrade.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbSeismicGrade.Location = new System.Drawing.Point(122, 466);
            this.cmbSeismicGrade.Name = "cmbSeismicGrade";
            this.cmbSeismicGrade.Size = new System.Drawing.Size(100, 20);
            this.cmbSeismicGrade.TabIndex = 42;
            // 
            // labBarSpace
            // 
            this.labBarSpace.AutoSize = true;
            this.labBarSpace.Location = new System.Drawing.Point(15, 500);
            this.labBarSpace.Name = "labBarSpace";
            this.labBarSpace.Size = new System.Drawing.Size(59, 12);
            this.labBarSpace.TabIndex = 43;
            this.labBarSpace.Text = "配筋间距:";
            // 
            // tbBarSpace
            // 
            this.tbBarSpace.Location = new System.Drawing.Point(122, 493);
            this.tbBarSpace.Name = "tbBarSpace";
            this.tbBarSpace.Size = new System.Drawing.Size(100, 21);
            this.tbBarSpace.TabIndex = 44;
            // 
            // labRoofThickness2
            // 
            this.labRoofThickness2.AutoSize = true;
            this.labRoofThickness2.Location = new System.Drawing.Point(228, 499);
            this.labRoofThickness2.Name = "labRoofThickness2";
            this.labRoofThickness2.Size = new System.Drawing.Size(17, 12);
            this.labRoofThickness2.TabIndex = 45;
            this.labRoofThickness2.Text = "mm";
            // 
            // labCs
            // 
            this.labCs.AutoSize = true;
            this.labCs.Location = new System.Drawing.Point(15, 528);
            this.labCs.Name = "labCs";
            this.labCs.Size = new System.Drawing.Size(71, 12);
            this.labCs.TabIndex = 46;
            this.labCs.Text = "保护层厚度:";
            // 
            // tbCs
            // 
            this.tbCs.Location = new System.Drawing.Point(122, 521);
            this.tbCs.Name = "tbCs";
            this.tbCs.Size = new System.Drawing.Size(100, 21);
            this.tbCs.TabIndex = 47;
            // 
            // labCs2
            // 
            this.labCs2.AutoSize = true;
            this.labCs2.Location = new System.Drawing.Point(228, 524);
            this.labCs2.Name = "labCs2";
            this.labCs2.Size = new System.Drawing.Size(17, 12);
            this.labCs2.TabIndex = 48;
            this.labCs2.Text = "mm";
            // 
            // btnChengben
            // 
            this.btnChengben.Location = new System.Drawing.Point(364, 56);
            this.btnChengben.Name = "btnChengben";
            this.btnChengben.Size = new System.Drawing.Size(100, 23);
            this.btnChengben.TabIndex = 52;
            this.btnChengben.Text = "计算";
            this.btnChengben.UseVisualStyleBackColor = true;
            this.btnChengben.Click += new System.EventHandler(this.btnChengben_Click);
            // 
            // cbxShowDetial
            // 
            this.cbxShowDetial.AutoSize = true;
            this.cbxShowDetial.Location = new System.Drawing.Point(364, 86);
            this.cbxShowDetial.Name = "cbxShowDetial";
            this.cbxShowDetial.Size = new System.Drawing.Size(96, 16);
            this.cbxShowDetial.TabIndex = 53;
            this.cbxShowDetial.Text = "显示详细信息";
            this.cbxShowDetial.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleName = "floor";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 561);
            this.Controls.Add(this.cbxShowDetial);
            this.Controls.Add(this.btnChengben);
            this.Controls.Add(this.labCs2);
            this.Controls.Add(this.tbCs);
            this.Controls.Add(this.labCs);
            this.Controls.Add(this.labRoofThickness2);
            this.Controls.Add(this.tbBarSpace);
            this.Controls.Add(this.labBarSpace);
            this.Controls.Add(this.cmbSeismicGrade);
            this.Controls.Add(this.labSeismicGrade);
            this.Controls.Add(this.labRebarPrice2);
            this.Controls.Add(this.tbRebarPrice);
            this.Controls.Add(this.labRebarPrice);
            this.Controls.Add(this.labConcretePrice2);
            this.Controls.Add(this.tbConcretePrice);
            this.Controls.Add(this.labConcretePrice);
            this.Controls.Add(this.cmbRebarGrade);
            this.Controls.Add(this.labRebarGrade);
            this.Controls.Add(this.cmbConcreteGrade);
            this.Controls.Add(this.labConcreteGrade);
            this.Controls.Add(this.tbT);
            this.Controls.Add(this.lbT);
            this.Controls.Add(this.btnClearBrowse);
            this.Controls.Add(this.labHuohezai2);
            this.Controls.Add(this.tbHuozaiXishu);
            this.Controls.Add(this.tbHengzaiXishu);
            this.Controls.Add(this.labHuozaiXishu);
            this.Controls.Add(this.tbHuohezai);
            this.Controls.Add(this.tbFutuThickness);
            this.Controls.Add(this.labFutuThickness2);
            this.Controls.Add(this.labRongzhong2);
            this.Controls.Add(this.labHengzaiXishu);
            this.Controls.Add(this.labHuohezai);
            this.Controls.Add(this.labFutuThickness);
            this.Controls.Add(this.tbRongZhong);
            this.Controls.Add(this.labRongzhong1);
            this.Controls.Add(this.rtbBrowser);
            this.Controls.Add(this.dgvInput);
            this.Name = "mainForm";
            this.Text = "挡土墙计算";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.RichTextBox rtbBrowser;
        private System.Windows.Forms.Label labRongzhong1;
        private System.Windows.Forms.TextBox tbRongZhong;
        private System.Windows.Forms.Label labFutuThickness;
        private System.Windows.Forms.Label labHuohezai;
        private System.Windows.Forms.Label labHengzaiXishu;
        private System.Windows.Forms.Label labRongzhong2;
        private System.Windows.Forms.Label labFutuThickness2;
        private System.Windows.Forms.TextBox tbFutuThickness;
        private System.Windows.Forms.TextBox tbHuohezai;
        private System.Windows.Forms.Label labHuozaiXishu;
        private System.Windows.Forms.TextBox tbHengzaiXishu;
        private System.Windows.Forms.TextBox tbHuozaiXishu;
        private System.Windows.Forms.Label labHuohezai2;
        private System.Windows.Forms.Button btnClearBrowse;
        private System.Windows.Forms.Label lbT;
        private System.Windows.Forms.TextBox tbT;
        private System.Windows.Forms.Label labConcreteGrade;
        private System.Windows.Forms.ComboBox cmbConcreteGrade;
        private System.Windows.Forms.Label labRebarGrade;
        private System.Windows.Forms.ComboBox cmbRebarGrade;
        private System.Windows.Forms.Label labConcretePrice;
        private System.Windows.Forms.TextBox tbConcretePrice;
        private System.Windows.Forms.Label labConcretePrice2;
        private System.Windows.Forms.Label labRebarPrice;
        private System.Windows.Forms.TextBox tbRebarPrice;
        private System.Windows.Forms.Label labRebarPrice2;
        private System.Windows.Forms.Label labSeismicGrade;
        private System.Windows.Forms.ComboBox cmbSeismicGrade;
        private System.Windows.Forms.Label labBarSpace;
        private System.Windows.Forms.TextBox tbBarSpace;
        private System.Windows.Forms.Label labRoofThickness2;
        private System.Windows.Forms.Label labCs;
        private System.Windows.Forms.TextBox tbCs;
        private System.Windows.Forms.Label labCs2;
        private System.Windows.Forms.Button btnChengben;
        private System.Windows.Forms.DataGridViewTextBoxColumn floorHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn s;
        private System.Windows.Forms.CheckBox cbxShowDetial;
    }
}

