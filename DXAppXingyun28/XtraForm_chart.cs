using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DXAppXingyun28.Util;
using DevExpress.XtraCharts;

namespace DXAppXingyun28
{
    public partial class XtraForm_chart : DevExpress.XtraEditors.XtraForm
    {
        public DataTable chartDt;
        public XtraForm_chart(DataTable dt)
        {
            InitializeComponent();
            this.chartDt = dt;
            //computeChart();

        }

        public void computeChart()
        {
            chartControl_auto_chart.Series.Clear();
            // 计算 显示chart
            this.chartControl_auto_chart.AddBaseSeries("总盈亏",
                DevExpress.XtraCharts.ViewType.Line,
                chartDt,
                "日期",
                new string[] { "总盈亏" },
                true);
            this.chartControl_auto_chart.AddBaseSeries("开奖号码",
    DevExpress.XtraCharts.ViewType.Line,
    chartDt,
    "日期",
    new string[] { "开奖号码" },
    true);
            chartControl_auto_chart.SetCrosshair(true);       // 设置是否显示十字标
            chartControl_auto_chart.Legend.Direction = LegendDirection.LeftToRight;   // 说明文字
            chartControl_auto_chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;    // 说明文字
            chartControl_auto_chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top; // 说明文字
        }

        private void XtraForm_chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void checkEdit_topMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkEdit_topMost.Checked;
        }
    }
}