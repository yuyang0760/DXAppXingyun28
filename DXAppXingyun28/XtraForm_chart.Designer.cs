namespace DXAppXingyun28
{
    partial class XtraForm_chart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_chart));
            this.chartControl_auto_chart = new DevExpress.XtraCharts.ChartControl();
            this.checkEdit_topMost = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_auto_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_topMost.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl_auto_chart
            // 
            this.chartControl_auto_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl_auto_chart.Legend.Name = "Default Legend";
            this.chartControl_auto_chart.Location = new System.Drawing.Point(2, 1);
            this.chartControl_auto_chart.Name = "chartControl_auto_chart";
            this.chartControl_auto_chart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_auto_chart.Size = new System.Drawing.Size(686, 450);
            this.chartControl_auto_chart.TabIndex = 0;
            // 
            // checkEdit1
            // 
            this.checkEdit_topMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEdit_topMost.Location = new System.Drawing.Point(613, 432);
            this.checkEdit_topMost.Name = "checkEdit1";
            this.checkEdit_topMost.Properties.Caption = "总在最前";
            this.checkEdit_topMost.Size = new System.Drawing.Size(75, 19);
            this.checkEdit_topMost.TabIndex = 1;
            this.checkEdit_topMost.CheckedChanged += new System.EventHandler(this.checkEdit_topMost_CheckedChanged);
            // 
            // XtraForm_chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 454);
            this.Controls.Add(this.checkEdit_topMost);
            this.Controls.Add(this.chartControl_auto_chart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XtraForm_chart";
            this.Text = "图表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraForm_chart_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_auto_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_topMost.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl_auto_chart;
        private DevExpress.XtraEditors.CheckEdit checkEdit_topMost;
    }
}