namespace LineChartTEST
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn2D = new System.Windows.Forms.Button();
            this.btn3D = new System.Windows.Forms.Button();
            this.tbUpdateInterval = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateInterval = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnStreamSe = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStreamDe = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFilePathDe = new System.Windows.Forms.Button();
            this.btnFilePathSe = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            this.chart1.BorderlineColor = System.Drawing.Color.ForestGreen;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            this.chart1.BorderSkin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.chart1.BorderSkin.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chart1.BorderSkin.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderSkin.BorderWidth = 23;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea3.Area3DStyle.Inclination = 20;
            chartArea3.Area3DStyle.IsRightAngleAxes = false;
            chartArea3.Area3DStyle.PointDepth = 200;
            chartArea3.Area3DStyle.Rotation = 50;
            chartArea3.Area3DStyle.WallWidth = 21;
            chartArea3.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea3.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea3.BackColor = System.Drawing.Color.White;
            chartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea3.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            chartArea3.InnerPlotPosition.Auto = false;
            chartArea3.InnerPlotPosition.Height = 85.39538F;
            chartArea3.InnerPlotPosition.Width = 88.93334F;
            chartArea3.InnerPlotPosition.X = 9.31973F;
            chartArea3.InnerPlotPosition.Y = 3.5906F;
            chartArea3.Name = "Defalut";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 87.7291F;
            chartArea3.Position.Width = 90.15778F;
            chartArea3.Position.X = 4.536885F;
            chartArea3.Position.Y = 5.508361F;
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.DataSource = this.chart1.Annotations;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(6, 7);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart1.Name = "chart1";
            series3.BorderColor = System.Drawing.Color.Red;
            series3.BorderWidth = 2;
            series3.ChartArea = "Defalut";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Firebrick;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(575, 423);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "s";
            // 
            // btn2D
            // 
            this.btn2D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2D.Enabled = false;
            this.btn2D.Location = new System.Drawing.Point(443, 441);
            this.btn2D.Name = "btn2D";
            this.btn2D.Size = new System.Drawing.Size(59, 53);
            this.btn2D.TabIndex = 6;
            this.btn2D.Text = "2D";
            this.btn2D.UseVisualStyleBackColor = true;
            this.btn2D.Click += new System.EventHandler(this.btn2D_Click);
            // 
            // btn3D
            // 
            this.btn3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn3D.Location = new System.Drawing.Point(508, 441);
            this.btn3D.Name = "btn3D";
            this.btn3D.Size = new System.Drawing.Size(59, 53);
            this.btn3D.TabIndex = 5;
            this.btn3D.Text = "3D";
            this.btn3D.UseVisualStyleBackColor = true;
            this.btn3D.Click += new System.EventHandler(this.btn3D_Click);
            // 
            // tbUpdateInterval
            // 
            this.tbUpdateInterval.Location = new System.Drawing.Point(6, 27);
            this.tbUpdateInterval.Name = "tbUpdateInterval";
            this.tbUpdateInterval.Size = new System.Drawing.Size(65, 21);
            this.tbUpdateInterval.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnUpdateInterval);
            this.groupBox1.Controls.Add(this.tbUpdateInterval);
            this.groupBox1.Location = new System.Drawing.Point(320, 441);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 54);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Interval";
            // 
            // btnUpdateInterval
            // 
            this.btnUpdateInterval.Location = new System.Drawing.Point(77, 27);
            this.btnUpdateInterval.Name = "btnUpdateInterval";
            this.btnUpdateInterval.Size = new System.Drawing.Size(34, 21);
            this.btnUpdateInterval.TabIndex = 10;
            this.btnUpdateInterval.Text = "ms";
            this.btnUpdateInterval.UseVisualStyleBackColor = true;
            this.btnUpdateInterval.Click += new System.EventHandler(this.btnUpdateInterval_Click);
            // 
            // chart2
            // 
            this.chart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chart2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart2.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            this.chart2.BorderlineColor = System.Drawing.Color.ForestGreen;
            this.chart2.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart2.BorderlineWidth = 2;
            this.chart2.BorderSkin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.chart2.BorderSkin.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chart2.BorderSkin.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart2.BorderSkin.BorderWidth = 23;
            this.chart2.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea4.Area3DStyle.Inclination = 20;
            chartArea4.Area3DStyle.IsRightAngleAxes = false;
            chartArea4.Area3DStyle.PointDepth = 200;
            chartArea4.Area3DStyle.Rotation = 50;
            chartArea4.Area3DStyle.WallWidth = 21;
            chartArea4.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea4.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea4.BackColor = System.Drawing.Color.White;
            chartArea4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea4.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            chartArea4.InnerPlotPosition.Auto = false;
            chartArea4.InnerPlotPosition.Height = 85.39538F;
            chartArea4.InnerPlotPosition.Width = 88.93334F;
            chartArea4.InnerPlotPosition.X = 9.31973F;
            chartArea4.InnerPlotPosition.Y = 3.5906F;
            chartArea4.Name = "Defalut";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 87.7291F;
            chartArea4.Position.Width = 90.15778F;
            chartArea4.Position.X = 4.536885F;
            chartArea4.Position.Y = 5.508361F;
            this.chart2.ChartAreas.Add(chartArea4);
            this.chart2.DataSource = this.chart2.Annotations;
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(6, 4);
            this.chart2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart2.Name = "chart2";
            series4.BorderColor = System.Drawing.Color.Red;
            series4.BorderWidth = 2;
            series4.ChartArea = "Defalut";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Firebrick;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(575, 404);
            this.chart2.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(291, 454);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(282, 35);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add Data";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnStreamSe
            // 
            this.btnStreamSe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStreamSe.Location = new System.Drawing.Point(6, 22);
            this.btnStreamSe.Name = "btnStreamSe";
            this.btnStreamSe.Size = new System.Drawing.Size(58, 53);
            this.btnStreamSe.TabIndex = 12;
            this.btnStreamSe.Text = "Serialize";
            this.btnStreamSe.UseVisualStyleBackColor = true;
            this.btnStreamSe.Click += new System.EventHandler(this.btnSerialize_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(291, 413);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(282, 39);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStreamDe
            // 
            this.btnStreamDe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStreamDe.Location = new System.Drawing.Point(70, 22);
            this.btnStreamDe.Name = "btnStreamDe";
            this.btnStreamDe.Size = new System.Drawing.Size(55, 53);
            this.btnStreamDe.TabIndex = 14;
            this.btnStreamDe.Text = "De - serialize";
            this.btnStreamDe.UseVisualStyleBackColor = true;
            this.btnStreamDe.Click += new System.EventHandler(this.btnDeserialize_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnStreamSe);
            this.groupBox2.Controls.Add(this.btnStreamDe);
            this.groupBox2.Location = new System.Drawing.Point(17, 407);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 82);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stream";
            // 
            // btnFilePathDe
            // 
            this.btnFilePathDe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePathDe.Location = new System.Drawing.Point(70, 22);
            this.btnFilePathDe.Name = "btnFilePathDe";
            this.btnFilePathDe.Size = new System.Drawing.Size(55, 53);
            this.btnFilePathDe.TabIndex = 14;
            this.btnFilePathDe.Text = "De - serialize";
            this.btnFilePathDe.UseVisualStyleBackColor = true;
            this.btnFilePathDe.Click += new System.EventHandler(this.btnFilePathDe_Click);
            // 
            // btnFilePathSe
            // 
            this.btnFilePathSe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePathSe.Location = new System.Drawing.Point(6, 22);
            this.btnFilePathSe.Name = "btnFilePathSe";
            this.btnFilePathSe.Size = new System.Drawing.Size(58, 53);
            this.btnFilePathSe.TabIndex = 12;
            this.btnFilePathSe.Text = "Serialize";
            this.btnFilePathSe.UseVisualStyleBackColor = true;
            this.btnFilePathSe.Click += new System.EventHandler(this.btnFilePathSe_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnFilePathSe);
            this.groupBox3.Controls.Add(this.btnFilePathDe);
            this.groupBox3.Location = new System.Drawing.Point(154, 407);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 82);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FilePath";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(595, 529);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btn3D);
            this.tabPage1.Controls.Add(this.btn2D);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(587, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "2D & 3D";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart2);
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(587, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serialization";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnUpdateInterval;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 531);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "MSChart Test Project";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btn2D;
        private System.Windows.Forms.Button btn3D;
        private System.Windows.Forms.TextBox tbUpdateInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpdateInterval;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnStreamSe;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStreamDe;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFilePathDe;
        private System.Windows.Forms.Button btnFilePathSe;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

