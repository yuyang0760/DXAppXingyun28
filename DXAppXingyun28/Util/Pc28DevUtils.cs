using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXAppXingyun28.Util
{
    enum _着色
    {
        盈亏,
        正负
    }
    /// <summary>
    /// pc28 关于devexrepss的工具
    /// </summary>
    class Pc28DevUtils
    {
        /// <summary>
        /// 获取选择了哪些项
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="SelectedField"></param>
        /// <param name="nameField"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public List<string> GetWhichSelected(GridView gridView, string SelectedField, string nameField, object sender = null)
        {



            // 获取选择了哪些项目
            List<string> selectedList = new List<string>();

            int selectedHandle = -1;

            if (sender != null)
            {
                selectedHandle = gridView.GetSelectedRows()[0];
                // 当前项
                if (((DevExpress.XtraEditors.CheckEdit)sender).Checked)
                {
                    selectedList.Add(gridView.GetRowCellValue(selectedHandle, nameField).ToString());
                }
            }
            // 其他项
            string value = "";
            for (int i = 0; i < gridView.RowCount; i++)
            {
                value = gridView.GetDataRow(i)[SelectedField].ToString();
                // 跳过当前项
                if (i == selectedHandle) { continue; }
                if (value == "True")
                {
                    selectedList.Add(gridView.GetRowCellValue(i, nameField).ToString());

                }
            }
            return selectedList;
        }

        /// <summary>  
        /// GridView  显示行号   设置行号列的宽度  
        /// </summary>  
        /// <param name="gv">GridView 控件名称</param>  
        /// <param name="width">行号列的宽度 如果为null或为0 默认为30</param>  
        /// <param name="isStartBy0">是否以0开始</param>  
        public void _显示行号(DevExpress.XtraGrid.Views.Grid.GridView gv, int width, bool isStartBy0 = false)
        {
            if (isStartBy0)
            {
                gv.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gv_CustomDrawRowIndicatorBy0);
            }
            else
            {
                gv.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gv_CustomDrawRowIndicatorBy1);
            }
            if (width != 0)
            {
                gv.IndicatorWidth = width;
            }
            else
            {
                gv.IndicatorWidth = 30;
            }
        }
        //行号设置  
        private void gv_CustomDrawRowIndicatorBy0(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle).ToString();
            }
        }
        //行号设置  
        private void gv_CustomDrawRowIndicatorBy1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        /// <summary>
        /// 给走势图着色
        /// </summary>
        /// <param name="gridView"></param>
        public void _走势图着色(GridView gridView,(int startNumber,int endNumber) tag)
        {
            gridView.Tag = tag;
            gridView.CustomDrawCell += GridView_CustomDrawCell;
            gridView.CalcRowHeight += GridView_CalcRowHeight;

            // 设置宽度
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                gridView.Columns[i].Width = 22;
                gridView.Columns["日期"].Width = 140;
                gridView.Columns["期号"].Width = 60;

            }
        }
        public void _列着色(GridView gridView, string[] columnFields, _着色 type)
        {
            gridView.Tag = columnFields;
            if (type == _着色.正负)
            {
                gridView.CustomDrawCell += GridView_CustomDrawCell_正负;
            }
            if (type == _着色.盈亏)
            {
                gridView.CustomDrawCell += GridView_CustomDrawCell_盈亏;
            }

        }
        private void GridView_CustomDrawCell_盈亏(object sender, RowCellCustomDrawEventArgs e)
        {
            string[] columns = ((sender as GridView).Tag as string[]);
            if (columns.Contains(e.Column.FieldName))
            {
                if (e.CellValue != null && e.CellValue.ToString() == "亏")
                {
                    // #FFFBFF
                    e.Appearance.ForeColor = Color.FromArgb(27, 94, 0);
                    //e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                    //e.Appearance.BackColor = Color.FromArgb(51, 51, 255);
                }
            }
        }
        private void GridView_CustomDrawCell_正负(object sender, RowCellCustomDrawEventArgs e)
        {
            string[] columns = ((sender as GridView).Tag as string[]);
            if (columns.Contains(e.Column.FieldName))
            {
                if (e.CellValue != null && int.Parse(e.CellValue.ToString()) < 0)
                {
                    // #FFFBFF
                    e.Appearance.ForeColor = Color.FromArgb(27, 94, 0);
                    //e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                    //e.Appearance.BackColor = Color.FromArgb(51, 51, 255);
                }
            }
        }

        // 走势图颜色
        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            (int startNumber, int endNumber) rangOfmid = (ValueTuple<int,int> )(sender as GridView).Tag;


            // 10-17 和 单双大小中边 /3/4/5 大尾 小尾
            List<string> list1 = new List<string>();
            for (int i = rangOfmid.startNumber; i <= rangOfmid.endNumber; i++)
            {
                list1.Add(i.ToString());
            }
            List<string> list2 = new List<string>();
            for (int i = 0; i <= 27; i++)
            {
                if (!list1.Contains(i.ToString()))
                {
                    list2.Add(i.ToString());
                }
            }
            List<string> list3 = new List<string>() { "单", "双", "大", "小", "大尾", "小尾", "中", "边" };
            if (list1.Contains(e.Column.FieldName))
            {

                if (e.CellValue != null && e.CellValue.ToString() != "")
                {
                    // #FFFBFF
                    e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            if (list2.Contains(e.Column.FieldName))
            {
                if (e.CellValue != null && e.CellValue.ToString() != "")
                {
                    e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                    e.Appearance.BackColor = Color.FromArgb(51, 51, 255);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 250, 199);
                }
            }
            if (list3.Contains(e.Column.FieldName))
            {

                if (e.CellValue != null && e.CellValue.ToString() != "")
                {
                    // #FFFBFF
                    e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            if (e.Column.FieldName == "期号" || e.Column.FieldName == "日期")
            {
                e.Appearance.ForeColor = Color.FromArgb(2, 9, 11);
                e.Appearance.BackColor = Color.FromArgb(255, 223, 156);
            }
            if (e.Column.FieldName == "单" && e.CellValue.ToString() != "")
            {

                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(234, 38, 64);
            }
            if (e.Column.FieldName == "双" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(102, 51, 153);
            }
            if (e.Column.FieldName == "大" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(255, 128, 64);
            }
            if (e.Column.FieldName == "小" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(255, 0, 255);
            }
            if (e.Column.FieldName == "中" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
            }
            if (e.Column.FieldName == "边" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(51, 51, 255);
            }
            if (e.Column.FieldName == "大尾" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(51, 51, 255);
            }
            if (e.Column.FieldName == "小尾" && e.CellValue.ToString() != "")
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
                e.Appearance.BackColor = Color.FromArgb(234, 38, 64);
            }
        }
        // 走势图 表格高度
        private void GridView_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.RowHeight = 19;
        }


    }
}
