using DevExpress.Utils;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DXAppXingyun28.Util
{
    /// <summary>
    /// 基于.NET 3.5的Chart工具类;对应的DevExpress版本：12.1.7;
    /// </summary>
    public static class ChartUtils
    {


        /// <summary>
        /// 添加基本的Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series的名称</param>
        /// <param name="seriesType">Series的类型</param>
        /// <param name="dataSource">Series的绑定数据源</param>
        /// <param name="argumentDataMember">ArgumentDataMember绑定字段名称</param>
        /// <param name="valueDataMembers">ValueDataMembers的绑定字段数组</param>
        /// <param name="visible">Series是否可见</param>
        /// <returns>Series</returns>
        public static Series GetSeries(string seriesName, ViewType seriesType, object dataSource, string argumentDataMember, string[] valueDataMembers, bool visible)
        {
            Series _baseSeries = new Series(seriesName, seriesType);
            _baseSeries.ArgumentDataMember = argumentDataMember;
            _baseSeries.ValueDataMembers.AddRange(valueDataMembers);
            _baseSeries.DataSource = dataSource;
            _baseSeries.LegendTextPattern = "{S}";
            _baseSeries.Visible = visible;
            return _baseSeries;
        }

        /// <summary>
        /// 添加基本的Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series的名称</param>
        /// <param name="seriesType">Series的类型</param>
        /// <param name="dataSource">Series的绑定数据源</param>
        /// <param name="argumentDataMember">ArgumentDataMember绑定字段名称</param>
        /// <param name="valueDataMembers">ValueDataMembers的绑定字段数组</param>
        /// <param name="visible">Series是否可见</param>
        /// <returns>Series</returns>
        public static Series AddBaseSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string argumentDataMember, string[] valueDataMembers, bool visible)
        {
            Series _baseSeries = new Series(seriesName, seriesType);
            _baseSeries.ArgumentDataMember = argumentDataMember;
            _baseSeries.ValueDataMembers.AddRange(valueDataMembers);
            _baseSeries.DataSource = dataSource;
            _baseSeries.LegendTextPattern = "{S}";
            _baseSeries.Visible = visible;
            chat.Series.Add(_baseSeries);
            return _baseSeries;
        }
        /// <summary>
        /// 设置SeriesTemplate参数
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dataSource">SeriesTemplate的绑定数据源</param>
        /// <param name="argumentDataMember">ArgumentDataMember绑定字段名称</param>
        /// <param name="valueDataMembers">ValueDataMembers的绑定字段数组</param>
        /// <param name="visible">SeriesTemplate是否可见</param>
        /// <returns>SeriesBase</returns>
        public static SeriesBase SetSeriesTemplate(this ChartControl chart, object dataSource, string argumentDataMember, string[] valueDataMembers, bool visible)
        {
            chart.SeriesTemplate.ValueDataMembers.AddRange(valueDataMembers);
            chart.SeriesTemplate.ArgumentDataMember = argumentDataMember;
            chart.SeriesTemplate.Visible = visible;
            chart.DataSource = dataSource;
            return chart.SeriesTemplate;
        }
        // 这个不太对
        public static void AddSecondaryAxisX(this ChartControl chart, Series series)
        {
            SecondaryAxisX myAxisX = new SecondaryAxisX("my X-Axis");
            ((XYDiagram)chart.Diagram).SecondaryAxesX.Add(myAxisX);

            // Assign the series2 to the created axes. 
            ((LineSeriesView)series.View).AxisX = myAxisX;

        }
        public static void AddSecondaryAxisY(this ChartControl chart, Series series)
        {
            if (chart.Diagram == null) { return; }
            chart.Series.Add(series);
            SecondaryAxisY myAxisY = new SecondaryAxisY("my Y-Axis");
            ((XYDiagram)chart.Diagram).SecondaryAxesY.Clear();
            ((XYDiagram)chart.Diagram).SecondaryAxesY.Add(myAxisY);

           
            // Assign the series2 to the created axes. 
            ((LineSeriesView)series.View).AxisY = myAxisY;
            myAxisY.Title.Text = series.Name;
            myAxisY.Title.Visibility = DefaultBoolean.True;
            myAxisY.Title.TextColor = Color.Blue;
            myAxisY.Label.TextColor = Color.Blue;

            myAxisY.Color = Color.Blue;

        }
        public static void AddSecondaryAxisY(this ChartControl chart, List<Series> seriesList)
        {
            chart.Series.AddRange(seriesList.ToArray());
            // Create two secondary axes, and add them to the chart's Diagram. 
            SecondaryAxisY myAxisY = new SecondaryAxisY("my Y-Axis");
            if (chart.Diagram == null) { return; }
            ((XYDiagram)chart.Diagram).SecondaryAxesY.Clear();
            ((XYDiagram)chart.Diagram).SecondaryAxesY.Add(myAxisY);

            // Assign the series2 to the created axes. 
            string names = "";
            foreach (var item in seriesList)
            {
                ((LineSeriesView)item.View).AxisY = myAxisY;
                names += " " + item.Name;
            }


            // Customize the appearance of the secondary axes (optional). 
            myAxisY.Title.Text = names;
            myAxisY.Title.Visibility = DefaultBoolean.True;
            myAxisY.Title.TextColor = Color.Blue;
            myAxisY.Label.TextColor = Color.Blue;

            myAxisY.Color = Color.Blue;


        }
        /// <summary>
        /// 增加数据筛选
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="columnName">列名称</param>
        /// <param name="value">列名称对应的筛选数值</param>
        /// <param name="dataFilterCondition">DataFilterCondition枚举</param>
        public static void AddDataFilter(this SeriesBase series, string columnName, object value, DataFilterCondition dataFilterCondition)
        {
            series.DataFilters.Add(new DataFilter(columnName, value.GetType().FullName, dataFilterCondition, value));
        }

        /// <summary>
        /// 设置X轴Lable角度
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="angle">角度</param>
        public static void SetXLableAngle(this ChartControl chart, int angle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _xyDiagram = (XYDiagram)chart.Diagram;
                if (_xyDiagram != null)
                    _xyDiagram.AxisX.Label.Angle = angle;
            }
        }
        /// <summary>
        ///  设置Y轴Lable角度
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="angle">角度</param>
        public static void SetYLableAngle(this ChartControl chart, int angle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _xyDiagram = (XYDiagram)chart.Diagram;
                _xyDiagram.AxisY.Label.Angle = angle;
            }
        }
        /// <summary>
        /// 设置ColorEach
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="colorEach">是否设置成ColorEach</param>
        public static void SetColorEach(this SeriesBase series, bool colorEach)
        {
            SeriesViewColorEachSupportBase colorEachView = (SeriesViewColorEachSupportBase)series.View;
            if (colorEachView != null)
            {
                colorEachView.ColorEach = colorEach;
            }
        }
        /// <summary>
        /// 设置是否显示十字标线
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="crosshair">是否显示十字标线</param>
        public static void SetCrosshair(this ChartControl chart, bool crosshair)
        {
            chart.CrosshairEnabled = crosshair ? DefaultBoolean.True : DefaultBoolean.False;
            chart.CrosshairOptions.ShowArgumentLabels = crosshair;
            chart.CrosshairOptions.ShowArgumentLine = crosshair;
            chart.CrosshairOptions.ShowValueLabels = crosshair;
            chart.CrosshairOptions.ShowValueLine = crosshair;
        }

        /// <summary>
        /// 添加ChartControl的Title文字
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">Title文字</param>
        /// <param name="visible">是否显示</param>
        /// <param name="titlePosition">Title位置</param>
        public static void AddTitle(this ChartControl chart, string title, bool visible, ChartTitleDockStyle titlePosition)
        {
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Text = title;
            _chartTitle.Visibility = visible ? DefaultBoolean.True : DefaultBoolean.False;
            _chartTitle.Dock = titlePosition;
            chart.Titles.Add(_chartTitle);
        }
        /// <summary>
        /// 先删除Chart的Title，然后添加新的Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">Title文字</param>
        /// <param name="titlePosition">Title位置</param>
        public static void ClearThenAddTitle(this ChartControl chart, string title, ChartTitleDockStyle titlePosition)
        {
            chart.Titles.Clear();
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Text = title;
            _chartTitle.Visibility = DefaultBoolean.True;
            _chartTitle.Dock = titlePosition;
            chart.Titles.Add(_chartTitle);
        }
        /// <summary>
        /// 创建Drill-Down样式的Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">title文字</param>
        /// <param name="visible">是否可见</param>
        public static void AddDrillDownTitle(this ChartControl chart, string title, bool visible)
        {
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Alignment = StringAlignment.Near;
            _chartTitle.EnableAntialiasing = DefaultBoolean.False;
            _chartTitle.Font = new Font("Tahoma", 10F, FontStyle.Underline);
            _chartTitle.Indent = 20;
            _chartTitle.Text = title;
            _chartTitle.TextColor = Color.RoyalBlue;
            _chartTitle.Visibility = DefaultBoolean.False;
            chart.Titles.Add(_chartTitle);
        }
        /// <summary>
        /// 饼状Series设置成百分比显示
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="valueLegendType">Series对应Lengend显示类型</param>
        /// <param name="lengendPointView">Series对应Lengend PointView类型</param>
        public static void SetPiePercentage(this SeriesBase series, NumericFormat valueLegendType, PointView lengendPointView)
        {
            if (series.View is PieSeriesView || series.View is Pie3DSeriesView)
            {
                PiePointOptions _piePointOptions = (PiePointOptions)series.Label.PointOptions;
                if (_piePointOptions != null)
                {
                    _piePointOptions.PercentOptions.ValueAsPercent = true;
                    _piePointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    _piePointOptions.ValueNumericOptions.Precision = 0;
                    series.LegendPointOptions.ValueNumericOptions.Format = valueLegendType;
                    series.LegendPointOptions.PointView = lengendPointView;
                }
            }
        }
        /// <summary>
        /// Lable格式化设置
        /// 【{A} Use it to display a series point arguments 】
        /// 【{V} Use it to display a series point values】
        /// 【{S} Use it to display the name of the series】
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="formatString">Lable格式化设置；【{A}{V}{S}】</param>
        public static void CustomLable(this SeriesBase series, string formatString)
        {
            if (series.LabelsVisibility != DefaultBoolean.True)
                series.LabelsVisibility = DefaultBoolean.True;
            //series.Label.PointOptions.Pattern = formatString;
            series.Label.TextPattern = formatString;

        }
        /// <summary>
        /// 十字标线的Lable格式化设置
        /// 【{A} Use it to display a series point arguments 】
        /// 【{V} Use it to display a series point values】
        /// 【{S} Use it to display the name of the series】
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="formatString">CrosshairLabel格式化设置；【{A}{V}{S}】</param>
        public static void CustomCrosshairLabel(this SeriesBase series, string formatString)
        {
            if (series.CrosshairEnabled != DefaultBoolean.True)
                series.CrosshairEnabled = DefaultBoolean.True;
            series.CrosshairLabelPattern = formatString;
        }
        /// <summary>
        /// 将X轴格式化成时间轴
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dateTimeMeasureUnit">X轴刻度单位</param>
        /// <param name="dateTimeGridAlignment">X轴刻度间距的单位</param>
        public static void SetAxisXTime(this ChartControl chart, DateTimeMeasureUnit dateTimeMeasureUnit, DateTimeGridAlignment dateTimeGridAlignment)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.DateTimeScaleOptions.MeasureUnit = dateTimeMeasureUnit;//X轴刻度单位
                    _diagram.AxisX.DateTimeScaleOptions.GridAlignment = dateTimeGridAlignment;//X轴刻度间距
                }
            }
        }
        /// <summary>
        /// 将X轴格式化成时间轴
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dateTimeMeasureUnit">X轴刻度单位</param>
        /// <param name="dateTimeGridAlignment">X轴刻度间距的单位</param>
        /// <param name="formatString">时间格式；eg:yyyy-MM</param>
        public static void SetAxisXTime(this ChartControl chart, DateTimeMeasureUnit dateTimeMeasureUnit, DateTimeGridAlignment dateTimeGridAlignment, string formatString)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.DateTimeScaleOptions.MeasureUnit = dateTimeMeasureUnit;//X轴刻度单位
                    _diagram.AxisX.DateTimeScaleOptions.GridAlignment = dateTimeGridAlignment;//X轴刻度间距
                    // 这里可能有问题
                    _diagram.AxisX.Label.TextPattern = formatString;
                }
            }
        }
        /// <summary>
        /// 设置ChartControl滚动条【默认X,Y轴都出现】
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        public static ScrollBarOptions SetScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.EnableAxisXScrolling = true;
                    _diagram.EnableAxisYScrolling = true;
                    _diagram.EnableAxisXZooming = true;
                    _diagram.EnableAxisYZooming = true;
                    ScrollBarOptions _scrollBarOptions = _diagram.DefaultPane.ScrollBarOptions;
                    _scrollBarOptions.BackColor = backColor;
                    _scrollBarOptions.BarColor = barColor;
                    _scrollBarOptions.BorderColor = borderColor;
                    _scrollBarOptions.BarThickness = barThickness;
                    return _scrollBarOptions;
                }
            }
            return null;
        }
        /// <summary>
        /// 设置ChartControl X轴滚动条
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        /// <param name="barAlignment">滚动条位置</param>
        public static void SetAxisXScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness, ScrollBarAlignment barAlignment)
        {
            ScrollBarOptions _scrollBarOptions = SetScrollBar(chart, backColor, barColor, borderColor, barThickness);
            if (_scrollBarOptions != null)
            {
                _scrollBarOptions.XAxisScrollBarAlignment = barAlignment;
                _scrollBarOptions.XAxisScrollBarVisible = true;
                _scrollBarOptions.YAxisScrollBarVisible = false;
            }
        }
        /// <summary>
        /// 设置ChartControl Y轴滚动条
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        /// <param name="barAlignment">滚动条位置</param>
        public static void SetAxisYScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness, ScrollBarAlignment barAlignment)
        {
            ScrollBarOptions _scrollBarOptions = SetScrollBar(chart, backColor, barColor, borderColor, barThickness);
            if (_scrollBarOptions != null)
            {
                _scrollBarOptions.XAxisScrollBarVisible = false;
                _scrollBarOptions.YAxisScrollBarVisible = true;
                _scrollBarOptions.YAxisScrollBarAlignment = barAlignment;
            }
        }
        /// <summary>
        /// 设置X轴Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="titleText">Title文字</param>
        /// <param name="titleColor">Title文字颜色</param>
        public static void SetAxisXTitle(this ChartControl chart, string titleText, Color titleColor, Font font)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.Title.Visibility = DefaultBoolean.True;     // 可见
                    _diagram.AxisX.Title.Alignment = StringAlignment.Center;    // 居中对齐
                    _diagram.AxisX.Title.Text = titleText;                      // title文本
                    _diagram.AxisX.Title.TextColor = titleColor;                // 颜色
                    _diagram.AxisX.Title.EnableAntialiasing = DefaultBoolean.True;     // 是否反锯齿
                    _diagram.AxisX.Title.Font = font; // 字体
                }
            }
        }
        /// <summary>
        /// 设置Y轴Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="titleText">Title文字</param>
        /// <param name="titleColor">Title文字颜色</param>
        public static void SetAxisYTitle(this ChartControl chart, string titleText, Color titleColor, Font font)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisY.Title.Visibility = DefaultBoolean.True;     // 可见
                    _diagram.AxisY.Title.Alignment = StringAlignment.Center;    // 居中对齐
                    _diagram.AxisY.Title.Text = titleText;                      // 标题
                    _diagram.AxisY.Title.TextColor = titleColor;                // 标题颜色
                    _diagram.AxisY.Title.EnableAntialiasing = DefaultBoolean.True;     // 是否反锯齿
                    _diagram.AxisY.Title.Font = font; // 字体
                }
            }
        }
        /// <summary>
        /// 创建基准线ConstantLine
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="ctAxisValue">基准线数值</param>
        /// <param name="ctLegendText">基准线图例文字</param>
        /// <param name="ctTitle">基准线文字</param>
        /// <param name="ctTitleColor">基准线字体颜色</param>
        /// <param name="ctLineColor">基准线颜色</param>
        /// <param name="ctLineStyle">基准线样式</param>
        public static void CreateConstantLine(this ChartControl chart, int ctAxisValue, string ctLegendText, string ctTitle, Color ctTitleColor, Color ctLineColor, DashStyle ctLineStyle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    ConstantLine _ctLine = new ConstantLine();

                    _ctLine.AxisValue = ctAxisValue;
                    _ctLine.Visible = true;
                    _ctLine.ShowInLegend = true;
                    _ctLine.LegendText = ctLegendText;
                    _ctLine.ShowBehind = false;

                    _ctLine.Title.Visible = true;
                    _ctLine.Title.Text = ctTitle;
                    _ctLine.Title.TextColor = ctTitleColor;
                    _ctLine.Title.EnableAntialiasing = DefaultBoolean.False;
                    _ctLine.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    _ctLine.Title.ShowBelowLine = true;
                    _ctLine.Title.Alignment = ConstantLineTitleAlignment.Far;

                    _ctLine.Color = ctLineColor;
                    _ctLine.LineStyle.DashStyle = ctLineStyle;
                    _ctLine.LineStyle.Thickness = 2;

                    _diagram.AxisY.ConstantLines.Add(_ctLine);
                }
            }
        }
        /// <summary>
        /// 创建Strip [条带]
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="strip">Strip</param>
        /// <param name="stripLable">Strip文字</param>
        /// <param name="stripLengend">Strip对应的Lengend文字</param>
        /// <param name="stripColor">Strip颜色</param>
        /// <param name="stripStyle">Strip填充样式</param>
        public static void CreateStrip(this ChartControl chart, Strip strip, string stripLable, string stripLengend, Color stripColor, FillMode stripStyle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null && strip != null)
                {
                    _diagram.AxisY.Strips.Add(strip);

                    _diagram.AxisY.Strips[0].Visible = true;
                    _diagram.AxisY.Strips[0].ShowAxisLabel = true;
                    _diagram.AxisY.Strips[0].AxisLabelText = stripLable;
                    _diagram.AxisY.Strips[0].ShowInLegend = true;
                    _diagram.AxisY.Strips[0].LegendText = stripLengend;

                    _diagram.AxisY.Strips[0].Color = stripColor;
                    _diagram.AxisY.Strips[0].FillStyle.FillMode = stripStyle;
                }
            }
        }
        /// <summary>
        /// 自定义ChartControl的Tooltip
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="tooltip">ToolTipController</param>
        /// <param name="tooltipTitle">ToolTipController的Title</param>
        /// <param name="paramter">委托</param>
        public static void CustomToolTip(this ChartControl chart, MouseEventArgs e, ToolTipController tooltip, string tooltipTitle, System.Func<string, double[], string> paramter)
        {
            ChartHitInfo _hitInfo = chart.CalcHitInfo(e.X, e.Y);
            SeriesPoint _point = _hitInfo.SeriesPoint;
            if (_point != null)
            {
                string _msg = paramter(_point.Argument, _point.Values);
                tooltip.ShowHint(_msg, tooltipTitle);
            }
            else
            {
                tooltip.HideHint();
            }
        }
        /// <summary>
        /// 设置Legend位于底部并居中
        /// </summary>
        /// <param name="legend">Legend</param>
        public static void SetBottomCenter(this Legend legend)
        {
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
        }
        /// <summary>
        /// 设置饼状图的Lable位置
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="lablePosition">PieSeriesLabelPosition枚举</param>
        public static void SetLablePosition(this SeriesBase series, PieSeriesLabelPosition lablePosition)
        {
            if (series.Label is PieSeriesLabel)
            {
                PieSeriesLabel _label = series.Label as PieSeriesLabel;
                _label.Position = lablePosition;
            }
            //if (series.Label is Pie3DSeriesLabel)
            //{
            //    Pie3DSeriesLabel _label = series.Label as Pie3DSeriesLabel;
            //    _label.Position = lablePosition;
            //}
        }
        /// <summary>
        /// 饼状图突出设置
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="pieSeries">Series【仅仅适用于PieSeriesView】</param>
        /// <param name="explodeMode">突出模式【枚举】</param>
        /// <param name="explodedValue">突出间距</param>
        /// <param name="dragPie">是否可以拖动突出饼状</param>
        public static void SetPieExplode(this ChartControl chart, SeriesBase pieSeries, PieExplodeMode explodeMode, int explodedValue, bool dragPie)
        {
            if (pieSeries.View is PieSeriesView)
            {
                if (!chart.RuntimeHitTesting)
                    chart.RuntimeHitTesting = true;

                PieSeriesView _pieView = pieSeries.View as PieSeriesView;
                _pieView.ExplodeMode = explodeMode;
                _pieView.ExplodedDistancePercentage = explodedValue;
                _pieView.RuntimeExploding = dragPie;
            }
        }
        /// <summary>
        /// chart钻取实现【在MouseClick事件中实现】
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="backKeyWord">返回主Series的关键字</param>
        /// <param name="gotoHandler">向下钻取委托</param>
        /// <param name="backHandler">返回主Series的委托</param>
        public static void DrillDownHelper(this ChartControl chart, MouseEventArgs e, string backKeyWord, Action<SeriesPoint> gotoHandler, Action<SeriesPoint> backHandler)
        {
            //eg:
            //private void chartLh_MouseClick(object sender, MouseEventArgs e)
            //{
            //    ChartControl _curChart = sender as ChartControl;
            //    _curChart.DrillDownHelper(
            //        e,
            //        "返回",
            //        point =>
            //        {
            //            string _argument = point.Argument.ToString();
            //            if (_curChart.Series["pieSeries"].Visible)
            //            {
            //                _curChart.Series["pieSeries"].Visible = false;
            //                _curChart.SeriesTemplate.Visible = true;
            //                if (_curChart.SeriesTemplate.DataFilters.Count == 0)
            //                    _curChart.SeriesTemplate.AddDataFilter("categoryName", _argument, DataFilterCondition.Equal);
            //                else
            //                    _curChart.SeriesTemplate.DataFilters[0].Value = _argument;
            //                _curChart.Titles[1].Visible = true;
            //                _curChart.Titles[0].Visible = false;
            //            }
            //        },
            //        point =>
            //        {
            //            _curChart.Titles[0].Visible = true;
            //            _curChart.Series["pieSeries"].Visible = true;
            //            _curChart.SeriesTemplate.Visible = false;
            //        });
            //}
            ChartHitInfo _hitInfo = chart.CalcHitInfo(e.X, e.Y);
            SeriesPoint _point = _hitInfo.SeriesPoint;
            if (_point != null)
            {
                gotoHandler(_point);
            }
            ChartTitle link = _hitInfo.ChartTitle;
            if (link != null && link.Text.StartsWith(backKeyWord))
            {
                link.Visibility = DefaultBoolean.False;
                backHandler(_point);
            }
        }
    }
}