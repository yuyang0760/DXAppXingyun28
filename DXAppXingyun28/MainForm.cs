using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DXAppXingyun28.common;
using DXAppXingyun28.Util;
using DXAppXingyun28.View;
using DXAppXingyun28.ViewModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace DXAppXingyun28
{

    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Vieww View = new Vieww();                       // 视图

        DataTable autoTouZhuDt = new DataTable();       // 自动投注
        Pc28Utils Pc28Utils = new Pc28Utils();          // 工具类
        Pc28DevUtils Pc28DevUtils = new Pc28DevUtils(); // 工具类

        public MainForm()
        {
            InitializeComponent();

            //toggleSwitch_whichType.Visible = false;
            //Button_auto_模拟.Visible = false;
            //Button_auto_Chart.Visible = false;
            //Button_zidingyimoni.Visible = false;
            //tabPane2.Pages[0].PageVisible = false;
            //tabPane2.Pages[2].PageVisible = false;
            //tabPane1.Pages[1].PageVisible = false;
            //tabPane1.Pages[3].PageVisible = false;
            //tabPane1.Pages[4].PageVisible = false;
            //tabPane1.Pages[5].PageVisible = false;
            //Button_zidingyimoni.Visible = false;
            //labelControl_算法1.Visible = false;
            //spinEdit1.Visible = false;

            // 设置标题
            this.Text = View.SetTitle();
            labelControl_title.Text = View.SetTitle();
            // 设置皮肤
            View.SetSkin(defaultLookAndFeel1);
            SkinHelper.InitSkinGallery(galleryControl_skin);

            // 开始日期 结束日期
            dateEdit_startDate.DateTime = View.StartDateTime;
            dateEdit_endDate.DateTime = View.EndDateTime;
            radioGroup_statisticType.SelectedIndex = 1;

            // 初始化窗口状态
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.WindowState = FormWindowState.Maximized;

            // 加倍按钮
            this.Button_0d1.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_0d5.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_0d8.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_1d2.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_1d5.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_2.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_5.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_10.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_50.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_100.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_auto_selectAll.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_auto_fanxuan.Click += new System.EventHandler(this.Button_beishu_Click);
            this.Button_beishu.Click += new System.EventHandler(this.Button_beishu_Click);
            // 模拟自动投注 填写  左侧表格
            AddAutoTable();
            // 模拟自动投注 根据xml创建 单双大小按钮
            CreateButton_touZhuMoShi();
            // 模拟自动投注 创建赢了使用啥,输了使用啥的按钮
            Create_ziDongTouZhu();
            listBoxControl_cun.Items.AddRange(load_自动投注().ToArray());

            _创建自定义左边();
        }




        // 测试2
        private void button_test2_Click(object sender, EventArgs e)
        {
        }

        #region 自定义模拟

        private void _创建自定义左边()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("包含", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("数字", Type.GetType("System.Int32"));
            for (int i = 0; i <= 27; i++)
            {
                if (i >= 8 && i <= 19)
                {
                    dataTable.Rows.Add(new object[] { true, i });
                }
                else
                {
                    dataTable.Rows.Add(new object[] { false, i });
                }
            }

            gridControl_铅笔.DataSource = dataTable;
        }
        private void repositoryItemCheckEdit_铅笔_CheckedChanged(object sender, EventArgs e)
        {
            List<string> whichSelected = Pc28DevUtils.GetWhichSelected(gridView_铅笔, "包含", "数字", sender);
            _自定义模拟(whichSelected);
        }
        public DataTable _自定义模拟(DataTable db,
        double startDandan, List<int> _起始投注号码,
        out double _下期投注数量, out List<int> _下期投注号码

        )
        {
            DataTable everyDayDt = new DataTable();
            everyDayDt.Columns.Add("日期", Type.GetType("System.String"));
            everyDayDt.Columns.Add("最大值", Type.GetType("System.Double"));
            everyDayDt.Columns.Add("最小值", Type.GetType("System.Double"));
            everyDayDt.Columns.Add("结束值", Type.GetType("System.Double"));



            // 创建dataTable列
            DataTable re = new DataTable();
            re.Columns.Add("期号", Type.GetType("System.Int32"));
            re.Columns.Add("日期", Type.GetType("System.String"));
            re.Columns.Add("开奖号码", Type.GetType("System.Int32"));
            re.Columns.Add("盈亏", Type.GetType("System.String"));
            re.Columns.Add("个数", Type.GetType("System.Int32"));
            re.Columns.Add("投注号码", Type.GetType("System.String"));
            re.Columns.Add("投中概率", Type.GetType("System.String"));
            re.Columns.Add("投注数量", Type.GetType("System.Double"));
            re.Columns.Add("下期投注数量", Type.GetType("System.Double"));
            re.Columns.Add("下期投注号码", Type.GetType("System.String"));
            re.Columns.Add("盈利", Type.GetType("System.Double"));
            re.Columns.Add("总盈利", Type.GetType("System.Double"));
            re.Columns.Add("盈亏比例", Type.GetType("System.String"));

            // 投注数量
            double _投注数量 = startDandan;
            _下期投注数量 = startDandan;
            // 投注号码
            List<int> _投注号码 = _起始投注号码;
            _下期投注号码 = _起始投注号码;
            // 自定义个数
            int _个数 = 0;
            // 总盈利
            double _总盈利 = 0;
            List<double> _总盈利每天 = new List<double>();
            double gudingying = Pc28Utils._获取盈利百分比(_投注号码);
            // 遍历数据
            for (int i = db.Rows.Count - 1; i >= 0; i--)
            {
                _投注数量 = _下期投注数量;
                _投注号码 = _下期投注号码;

                double _投中概率 = Pc28Utils._获取投注信息ByCode(_投注号码)._概率;
                DateTime _日期 = Convert.ToDateTime(db.Rows[i]["日期"]);
                int _期号 = int.Parse(db.Rows[i]["期号"].ToString());
                int _开奖号码 = int.Parse(db.Rows[i]["pc28"].ToString());
                double _盈亏比例 = Pc28Utils._获取盈利百分比(_投注号码, _开奖号码);
                double _盈利 = Math.Floor(_投注数量 * _盈亏比例);
                string _盈亏Str = _盈利 >= 0 ? "赢" : "亏";
                _总盈利 += _盈利;
                _总盈利每天.Add(_总盈利);

                if (_投注号码.Contains(_开奖号码))
                {
                    if (_个数 >= 0)
                    {
                        _个数 = 0;
                        _下期投注数量 = startDandan;
                        _下期投注号码 = _投注号码;
                    }
                    else
                    {
                        _个数++;
                        _下期投注数量 = Math.Floor(_投注数量 * gudingying);
                        _下期投注号码 = _投注号码;
                    }
                }
                else
                {
                    _个数--;
                    _下期投注数量 = Math.Floor(_投注数量 / gudingying);
                    _下期投注号码 = _投注号码;
                }
                // 新的一天
                if (_日期.Hour == 23 && _日期.Minute == 55)
                {
                    // 计算这一天的最大值,最小值,结束值
                    everyDayDt.Rows.Add(_日期.ToString("yy-MM-dd"), _总盈利每天.Max(), _总盈利每天.Min(), _总盈利每天[_总盈利每天.Count - 1]);
                    _总盈利每天.Clear();
                    // 清零
                    _下期投注数量 = startDandan;
                    _个数 = 0;
                    _总盈利 = 0;

                }

                re.Rows.Add(new object[] { _期号, _日期, _开奖号码, _盈亏Str, _个数, string.Join(",", _投注号码), _投中概率.ToString("p"), _投注数量, _下期投注数量, string.Join(",", _下期投注号码), _盈利, _总盈利, _盈亏比例.ToString("p") });

            }
            (int _合适值, double _合适值总和) = _计算最小值的合适值(everyDayDt);
            everyDayDt.Rows.Add("最小值合适值", _合适值);
            everyDayDt.Rows.Add("合适值总和", _合适值总和);


            gridControl_算法1表格2.DataSource = everyDayDt;

            return re;

        }

        // 自定义模拟
        private void simpleButton_自定义模拟_Click(object sender, EventArgs e)
        {
            List<string> whichSelected = Pc28DevUtils.GetWhichSelected(gridView_铅笔, "包含", "数字");
            _自定义模拟(whichSelected);
        }

        private void _自定义模拟(List<string> whichSelected)
        {
            // 模拟
            DataTable dt = _自定义模拟(View.GetDataFromDb(),
                double.Parse(spinEdit1.Value.ToString()), whichSelected.Select<string, int>(x => int.Parse(x)).ToList()
              , out double _下期投注数量, out List<int> _下期投注号码

              );
            // 显示出来看看
            gridView_statistic2.Columns.Clear();
            gridControl_statistic2.DataSource = dt;
            gridView_statistic2.Columns["投注号码"].Visible = false;
            gridView_statistic2.Columns["下期投注号码"].Visible = false;
            gridView_statistic2.RefreshData();
            gridView_statistic2.BestFitColumns();
            gridView_statistic2.FocusedRowHandle = gridView_statistic2.RowCount - 1;
            // 图表
            chartControl_zidingyi.Series.Clear();
            Pc28Utils._图表ByDt(chartControl_zidingyi, ViewType.Line, dt, "日期", new string[] { "总盈利", "开奖号码" }, null);
            Pc28Utils._图表ByDt(chartControl_zidingyi, ViewType.Area, dt, "日期", null, new string[] { "个数" });
            // 保存
            Pc28Utils._计算保存到xml(_下期投注号码, _下期投注数量);
        }

        private (int _合适值, double _合适值总和) _计算最小值的合适值(DataTable dataTable)
        {
            Dictionary<int, double> dic = new Dictionary<int, double>();
            List<int> vv = new List<int>();
            for (int j = -100000; j > -200000000; j -= 100000)
            {
                int sum = 0;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    double min = double.Parse(dataTable.Rows[i]["最小值"].ToString());
                    if (j > min)
                    {
                        sum += j;
                    }
                }
                vv.Add(sum);
                dic.Add(j, sum);
            }
            double mink = dic.Values.Min();
            foreach (KeyValuePair<int, double> item in dic)
            {
                if (item.Value == mink)
                {
                    return (item.Key, item.Value);
                }
            }
            return (0, 0);
        }
        #endregion

        #region 模拟自动投注
        // 保存自动投注
        private void Button_cun_Click(object sender, EventArgs e)
        {
            if (textEdit_cun.Text.Trim() == "")
            {
                XtraMessageBox.Show("请填写名称");
                return;
            }
            string name = textEdit_cun.Text.Trim();
            // 赋值
            FillAutoTouZhu();
            // 1.载入文件
            XmlControl xmlControl = new XmlControl();
            xmlControl.LoadXmlFile(new System.IO.FileInfo("./xml/autotouzhu.xml"));
            // 重名
            if (listBoxControl_cun.Items.Contains(name))
            {
                DialogResult dr = XtraMessageBox.Show("重名,是否覆盖?", "重名", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                if (dr == DialogResult.OK)
                {
                    // 删除
                    XmlNode xmlNode = xmlControl.SelectSingleNode($"//touzhu[@name=\"{name}\"]");
                    xmlNode.ParentNode.RemoveChild(xmlNode);

                }
            }


            // 定义投注
            XmlElement xmlTouzhu = xmlControl.doc.CreateElement("touzhu");
            xmlTouzhu.SetAttribute("name", name);
            // 保存    View.AutoTouZhu到文件
            foreach (KeyValuePair<string, (string win, string fail, int[] code)> item in View.AutoTouZhu)
            {
                string touzhu = string.Join(",", item.Value.code);
                // 保存到文件
                // 2.添加节点
                // 细节
                XmlElement xmlxijie = xmlControl.doc.CreateElement("element");

                xmlxijie.SetAttribute("key", item.Key);
                xmlxijie.SetAttribute("win", item.Value.win);
                xmlxijie.SetAttribute("fail", item.Value.fail);
                xmlxijie.SetAttribute("touzhu", touzhu);

                // 添加节点
                xmlTouzhu.AppendChild(xmlxijie);
            }
            xmlControl.root.AppendChild(xmlTouzhu);
            // 保存
            xmlControl.Save("./xml/autotouzhu.xml");
            // 重名
            if (!listBoxControl_cun.Items.Contains(name))
            {
                // 添加到listbox
                int nn = listBoxControl_cun.Items.Add(name);
                listBoxControl_cun.SetSelected(nn, true);
            }
            textEdit_cun.Text = "";
        }
        // 删除
        private void toolStripMenuItem_deleteAuto_Click(object sender, EventArgs e)
        {
            string value = listBoxControl_cun.SelectedValue.ToString();
            int index = listBoxControl_cun.SelectedIndex;

            //  数据库删除
            XmlConfig xmlConfig = new XmlConfig("./xml/autotouzhu.xml");
            xmlConfig.Delete("touzhu", value);
            //  listbox 删除
            listBoxControl_cun.Items.RemoveAt(index);
            //listBoxControl_cun.Refresh();
        }
        // 自动投注右键菜单
        private void listBoxControl_cun_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = listBoxControl_cun.IndexFromPoint(new Point(e.X, e.Y));
                listBoxControl_cun.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < listBoxControl_cun.Items.Count)
                {
                    listBoxControl_cun.SelectedIndex = posindex;
                    contextMenuStrip_deleteAuto.Show(listBoxControl_cun, new Point(e.X, e.Y));
                }
                //listBoxControl_cun.Refresh();
            }
        }
        // 选择自动投注
        private void listBoxControl_cun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxControl_cun.SelectedIndex == -1) { return; }
            string name = listBoxControl_cun.SelectedValue.ToString();
            // 读取文件
            XmlControl xmlControl = new XmlControl();
            xmlControl.LoadXmlFile(new System.IO.FileInfo("./xml/autotouzhu.xml"));
            XmlNodeList xmlNodeList = xmlControl.SelectNodes($"//touzhu[@name=\"{name}\"]/element");
            foreach (XmlNode item in xmlNodeList)
            {
                string key = ((XmlElement)item).GetAttribute("key");
                string win = ((XmlElement)item).GetAttribute("win");
                string fail = ((XmlElement)item).GetAttribute("fail");
                string touzhu = ((XmlElement)item).GetAttribute("touzhu");
                int[] n = touzhu.Split(',').Select<string, int>(x => int.Parse(x)).ToArray<int>();
                // 给auto赋值

                View.AutoTouZhu[key] = (win, fail, n);
            }
            // 显示

            foreach (KeyValuePair<string, (string win, string fail, int[] code)> item in View.AutoTouZhu)
            {


                panelControl_zidongtouzhu.Controls[item.Key + "_ying"].Text = item.Value.win;
                panelControl_zidongtouzhu.Controls[item.Key + "_kui"].Text = item.Value.fail;
                int[] n = item.Value.Item3;
                for (int j = 0; j <= 27; j++)
                {
                    autoTouZhuDt.Rows[j][item.Key] = n[j] == 0 ? "" : n[j].ToString();
                }

                //View.AutoTouZhu["模式00"+yy.util.Util.buling(i,2)]=
            }
            sum();

        }
        XtraForm_chart xtraForm_Chart;
        private void Button_auto_Chart_Click(object sender, EventArgs e)
        {
            if (xtraForm_Chart == null)
            {
                xtraForm_Chart = new XtraForm_chart(null);
            }
            if (!xtraForm_Chart.Visible)
            {
                xtraForm_Chart.Visible = true;
            }
            //xtraForm_Chart.Show();
            xtraForm_Chart.Activate();
        }
        // 自动投注结果 的 颜色
        private void gridView_auto_result_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "盈亏")
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
        // 载入自动投注列表
        public List<string> load_自动投注()
        {
            List<string> l = new List<string>();
            // 读取文件
            XmlControl xmlControl = new XmlControl();
            xmlControl.LoadXmlFile(new System.IO.FileInfo("./xml/autotouzhu.xml"));
            XmlNodeList xmlNodeList = xmlControl.SelectNodes($"//touzhu");
            foreach (XmlNode item in xmlNodeList)
            {
                string name = ((XmlElement)item).GetAttribute("name");
                l.Add(name);
            }
            return l;
        }

        public void sum()
        {
            int sumAll = 0;
            for (int j = 0; j < autoTouZhuDt.Columns.Count; j++)
            {
                int sumColumn = 0;
                for (int i = 0; i <= 27; i++)
                {
                    sumColumn += int.Parse(autoTouZhuDt.Rows[i][j].ToString() == "" ? "0" : autoTouZhuDt.Rows[i][j].ToString());
                }
                autoTouZhuDt.Rows[28][j] = sumColumn;
                sumAll += sumColumn;
            }
            labelControl_sum.Text = "所有模式总和:" + sumAll.ToString();
        }
        private void Button_auto_模拟_Click(object sender, EventArgs e)
        {
            FillAutoTouZhu();
            // 2.根据规则AutoTouZhu,算出投注
            List<Odds> oddsList = Pc28Utils.GetOdds();
            DataTable table = new DataTable();
            table.Columns.Add("期号", Type.GetType("System.Int32"));
            table.Columns.Add("日期", Type.GetType("System.String"));
            table.Columns.Add("开奖号码", Type.GetType("System.Int32"));
            table.Columns.Add("模式", Type.GetType("System.String"));
            table.Columns.Add("盈亏", Type.GetType("System.Int32"));
            table.Columns.Add("总盈亏", Type.GetType("System.Int32"));

            DataTable dbtable = View.GetDataFromDb();
            string touZhu = "模式00";    // 开始投注
            int zongyingli = 0;
            for (int i = dbtable.Rows.Count - 1; i >= 0; i--)
            {
                int opencode = int.Parse(dbtable.Rows[i]["pc28"].ToString());
                int[] touzhuArr = View.AutoTouZhu[touZhu].code;
                int yingli = int.Parse(Math.Floor(oddsList[opencode].odds * touzhuArr[opencode]).ToString()) - touzhuArr.Sum();

                zongyingli += yingli;
                table.Rows.Add(new object[] {
                    dbtable.Rows[i]["期号"],
                    dbtable.Rows[i]["日期"],
                    opencode,
                    touZhu,
                    yingli,
                    zongyingli
                });
                // 下一期投注
                if (yingli >= 0)
                {
                    touZhu = View.AutoTouZhu[touZhu].win;
                }
                else
                {
                    touZhu = View.AutoTouZhu[touZhu].fail;
                }
            }
            // 显示行号

            Pc28DevUtils._显示行号(gridView_auto_result, 40, false);
            gridControl_auto_result.DataSource = table;
            gridView_auto_result.BestFitColumns();
            gridView_auto_result.FocusedRowHandle = gridView_auto_result.RowCount - 1;

            // chart
            if (xtraForm_Chart == null)
            {
                xtraForm_Chart = new XtraForm_chart(table);
                xtraForm_Chart.computeChart();
                xtraForm_Chart.Show();
                xtraForm_Chart.Activate();
            }
            else
            {
                xtraForm_Chart.chartDt = table;
                xtraForm_Chart.computeChart();
                xtraForm_Chart.Show();
            }


        }

        private void FillAutoTouZhu()
        {
            // 1.填写   View.AutoTouZhu 以便模拟遍历
            for (int i = 0; i < View.AutoTouZhu.Count; i++)
            {
                string buling = yy.util.Util.buling(i, 2);
                string ying = panelControl_zidongtouzhu.Controls["模式" + buling + "_ying"].Text;
                string kui = panelControl_zidongtouzhu.Controls["模式" + buling + "_kui"].Text;
                int[] n = new int[28];
                for (int j = 0; j <= 27; j++)
                {
                    n[j] = int.Parse(autoTouZhuDt.Rows[j]["模式" + buling].ToString() == "" ? "0" : autoTouZhuDt.Rows[j]["模式" + buling].ToString());
                }

                View.AutoTouZhu["模式" + buling] = (ying, kui, n);
                //View.AutoTouZhu["模式00"+yy.util.Util.buling(i,2)]=
            }
        }

        // 创建自动投注按钮
        private void Create_ziDongTouZhu()
        {
            panelControl_zidongtouzhu.Controls.Clear();

            int number = this.autoTouZhuDt.Columns.Count;
            int xJiange = 3;
            int yJiange = 3;
            for (int i = 0; i < number; i++)
            {
                //label
                LabelControl labelControl = new LabelControl()
                {
                    Name = "模式" + yy.util.Util.buling(i, 2),
                    Text = "模式" + yy.util.Util.buling(i, 2) + ":",
                    Width = 35,
                    Height = 15,
                    Tag = "模式" + yy.util.Util.buling(i, 2),
                    Location = new Point(0, i * (20 + yJiange))
                };
                //combobox 赢
                ComboBoxEdit comboBoxEdit_ying = new ComboBoxEdit()
                {
                    Name = "模式" + yy.util.Util.buling(i, 2) + "_ying",
                    Text = "模式00",
                    Width = 70,
                    Height = 20,
                    Tag = "模式" + yy.util.Util.buling(i, 2),
                    Location = new Point(40 + xJiange, i * (20 + yJiange)),
                };
                comboBoxEdit_ying.Properties.DropDownRows = 10;
                comboBoxEdit_ying.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                //combobox 亏
                ComboBoxEdit comboBoxEdit_kui = new ComboBoxEdit()
                {
                    Name = "模式" + yy.util.Util.buling(i, 2) + "_kui",
                    Text = "模式00",
                    Width = 70,
                    Height = 20,
                    Tag = "模式" + yy.util.Util.buling(i, 2),
                    Location = new Point(110 + xJiange, i * (20 + yJiange))
                };
                comboBoxEdit_kui.Properties.DropDownRows = 10;
                comboBoxEdit_kui.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

                for (int j = 0; j < this.autoTouZhuDt.Columns.Count; j++)
                {
                    string name = this.autoTouZhuDt.Columns[j].ColumnName;
                    comboBoxEdit_ying.Properties.Items.Add(name);
                    comboBoxEdit_kui.Properties.Items.Add(name);
                }

                //comboBoxEdit_ying.Click += ButtonTouZhuMoShi_Click;

                panelControl_zidongtouzhu.Controls.Add(labelControl);
                panelControl_zidongtouzhu.Controls.Add(comboBoxEdit_ying);
                panelControl_zidongtouzhu.Controls.Add(comboBoxEdit_kui);

                // 添加  AutoTouZhu
                for (int z = 0; z < autoTouZhuDt.Columns.Count; z++)
                {
                    View.AutoTouZhu["模式" + yy.util.Util.buling(z, 2)] = ("模式00", "模式00", new int[28]);
                }
            }
        }
        // 创建投注模式按钮
        private void CreateButton_touZhuMoShi()
        {
            panelControl_touZhuMoShi.Controls.Clear();
            XmlConfig xmlConfig = new XmlConfig("./xml/touzhumoshi.xml");
            Dictionary<string, string> allNode = xmlConfig.AllNode();
            int n = 0;
            int xJiange = 3;
            int yJiange = 3;
            int meiPaiShuLiang = 3;
            foreach (KeyValuePair<string, string> item in allNode)
            {
                //item.Key
                SimpleButton simpleButton = new SimpleButton()
                {
                    Name = item.Key,
                    Text = item.Key,
                    Width = 70,
                    Height = 20,
                    Tag = item.Value,
                    Location = new Point((n % meiPaiShuLiang) * (70 + xJiange), (n / 3) * (20 + yJiange))
                };
                simpleButton.Click += ButtonTouZhuMoShi_Click;
                simpleButton.ContextMenuStrip = this.contextMenuStrip_deleteTouZhuMoShi;
                n++;
                panelControl_touZhuMoShi.Controls.Add(simpleButton);
            }
        }
        // 按D,F添加值
        private void gridControl_autoTouZhu_KeyDown(object sender, KeyEventArgs e)
        {
          
            List<Odds> pc28Odds = Pc28Utils.GetOdds();

            int rowHandle = this.gridView_autoTouZhu.FocusedRowHandle;
            int columnIndex = this.gridView_autoTouZhu.FocusedColumn.AbsoluteIndex;
            if (rowHandle > 27) { return; }

            if (e.KeyCode == System.Windows.Forms.Keys.D)
            {
                this.autoTouZhuDt.Rows[rowHandle][columnIndex] = pc28Odds[rowHandle].touzhu;

            }
            if (e.KeyCode == System.Windows.Forms.Keys.F)
            {
                this.autoTouZhuDt.Rows[rowHandle][columnIndex] = "";
            }
            sum();
        }
        // 投注模式按钮,例如,单,双,大,小
        private void ButtonTouZhuMoShi_Click(object sender, EventArgs e)
        {

            //List<Odds> odds = yy.util.Util.ReadPc28Odds();
            string[] touzhuStr = ((SimpleButton)sender).Tag.ToString().Split(',');

            int columnIndex = gridView_autoTouZhu.FocusedColumn.AbsoluteIndex;

            for (int i = 0; i < touzhuStr.Length; i++)
            {
                autoTouZhuDt.Rows[i][columnIndex] = touzhuStr[i].ToString() == "0" ? "" : touzhuStr[i].ToString();
            }
            sum();
        }
        // 删除投注模式
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl.Name;
            // 在xml中删除
            XmlConfig xmlConfig = new XmlConfig("./xml/touzhumoshi.xml");
            xmlConfig.Delete(name);

            // 重新加载xml
            CreateButton_touZhuMoShi();

        }
        // 所有倍数的button
        private void Button_beishu_Click(object sender, EventArgs e)
        {
            List<Odds> pc28Odds = Pc28Utils.GetOdds();
            // 获取倍数,倍数存在tag里面
            double beishu = double.Parse(((SimpleButton)sender).Tag.ToString());
            string buttonText = ((SimpleButton)sender).Text;
            // 获取当前所在行和列
            //gridView_autoTouZhu.FocusedColumn
            //  赋值
            if (buttonText == "全选")
            {
                for (int i = 0; i <= 27; i++)
                {
                    this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] = Math.Floor(pc28Odds[i].touzhu * beishu);
                }
            }
            else
            if (buttonText == "反选")
            {
                for (int i = 0; i <= 27; i++)
                {
                    if (this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] == null
                        || this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName].ToString() == "")
                    {
                        this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] = Math.Floor(pc28Odds[i].touzhu * beishu);
                    }
                    else
                    {
                        this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] = "";
                    }
                }
            }
            else
            {
                for (int i = 0; i <= 27; i++)
                {

                    double l;
                    if (this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName].ToString() == "")
                    {
                        l = 0;
                    }
                    else
                    {
                        l = double.Parse(this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName].ToString());
                    }

                    double d = Math.Floor(l * beishu);
                    if (d == 0)
                    {
                        this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] = "";
                    }
                    else
                    {
                        this.autoTouZhuDt.Rows[i][gridView_autoTouZhu.FocusedColumn.FieldName] = d;
                    }


                }
            }
            sum();
        }
        // 添加投注模式
        private void Button_addTouZhuMoShi_Click(object sender, EventArgs e)
        {
            string name = textEdit_touZhuMoShi.Text;
            XmlConfig xmlConfig = new XmlConfig("./xml/touzhumoshi.xml");
            List<string> valueList = new List<string>();
            // 未填写
            if (name.Trim() == "") { XtraMessageBox.Show("请填写名称"); return; };
            // 重名
            if (xmlConfig.AllNode().Keys.Contains<string>(name))
            {
                DialogResult dialogResult = XtraMessageBox.Show("已经有这个名称,是否覆盖?", "重名", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            // 添加
            int columnIndex = gridView_autoTouZhu.FocusedColumn.AbsoluteIndex;
            for (int i = 0; i < this.autoTouZhuDt.Rows.Count; i++)
            {
                string v = this.autoTouZhuDt.Rows[i][columnIndex].ToString();
                if (v == "")
                {
                    valueList.Add("0");
                }
                else
                {
                    valueList.Add(v);
                }
            }
            xmlConfig.Add(name, string.Join(",", valueList));
            CreateButton_touZhuMoShi();
        }


        private void AddAutoTable()
        {
            autoTouZhuDt.Clear();
            // 行号

            Pc28DevUtils._显示行号(gridView_autoTouZhu, 40, true);
            // 添加列数
            int numOfmoshi = Properties.Settings.Default.NumOfmoshi;                        // 模式个数
            for (int i = 0; i < numOfmoshi; i++)
            {
                this.autoTouZhuDt.Columns.Add("模式" + yy.util.Util.buling(i, 2), Type.GetType("System.String"));
            }

            // 添加行数
            for (int i = 0; i <= 27; i++)
            {
                this.autoTouZhuDt.Rows.Add(new string[numOfmoshi]);
            }
            this.autoTouZhuDt.Rows.Add(new string[numOfmoshi]);
            // 显示
            gridControl_autoTouZhu.DataSource = this.autoTouZhuDt;
            // 设置列宽
            for (int i = 0; i < gridView_autoTouZhu.Columns.Count; i++)
            {
                gridView_autoTouZhu.Columns[i].MinWidth = 60;
            }

            gridView_autoTouZhu.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gridView_autoTouZhu.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        // 双击添加值
        private void gridControl_autoTouZhu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            List<Odds> pc28Odds = Pc28Utils.GetOdds();

            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView_autoTouZhu.CalcHitInfo(new Point(e.X, e.Y));
            if (hInfo.RowHandle == 28) { return; }
            if (e.Button == MouseButtons.Left && hInfo.InRowCell)
            {
                //gridView_autoTouZhu.SetFocusedValue(pc28Odds[hInfo.RowHandle].touzhu);
                this.autoTouZhuDt.Rows[hInfo.RowHandle][hInfo.Column.AbsoluteIndex] = pc28Odds[hInfo.RowHandle].touzhu;
            }
            if (e.Button == MouseButtons.Right && hInfo.InRowCell)
            {
                //gridView_autoTouZhu.SetFocusedValue("");
                this.autoTouZhuDt.Rows[hInfo.RowHandle][hInfo.Column.AbsoluteIndex] = "";
            }
            sum();
            //gridView_autoTouZhu.CloseEditor();
        }
        // spinEdit 倍数
        private void spinEdit_beishu_EditValueChanged(object sender, EventArgs e)
        {
            Button_beishu.Tag = spinEdit_beishu.Value;
        }
        #endregion

        #region 统计
        // 走势图 中 取消
        private void button_rangeOfMid_取消_Click(object sender, EventArgs e)
        {
            panelControl_rangOfMid.Visible = false;
        }
        // 走势图 按钮
        private void Button_zoushitu_Click(object sender, EventArgs e)
        {
            _走势图();
        }
        // 走势图 中 确定
        private void button_rangeOfMid_Click(object sender, EventArgs e)
        {
            View.rangeOfMiddle = (rangeTrackBarControl_mid.Value.Minimum, rangeTrackBarControl_mid.Value.Maximum);
            _走势图();
            panelControl_rangOfMid.Visible = false;
        }

        private void 自定义中的范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rangeTrackBarControl_mid.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(
                View.rangeOfMiddle.startNumber, View.rangeOfMiddle.endNumber);
            for (int i = 0; i <= 27; i++)
            {
                rangeTrackBarControl_mid.Properties.Labels.Add(new DevExpress.XtraEditors.Repository.TrackBarLabel(i.ToString(), i));
            }
            panelControl_rangOfMid.Visible = true;
        }
        // 开始期号
        private void spinEdit_startExpect_EditValueChanged(object sender, EventArgs e)
        {
            View.StartExpect = int.Parse(Math.Floor(spinEdit_startExpect.Value).ToString());
        }
        // 结束期号
        private void spinEdit_endExpect_EditValueChanged(object sender, EventArgs e)
        {
            View.EndExpect = int.Parse(Math.Floor(spinEdit_endExpect.Value).ToString());
        }
        public void _走势图()
        {
            // 显示 走势图
            gridControl_dataDbTable.DataSource = Pc28Utils._走势图(View.GetDataFromDb(), View.rangeOfMiddle);
            Pc28DevUtils._走势图着色(gridView_dataDbTable, View.rangeOfMiddle);
        }

        // 统计按钮
        private void Button_statistic_Click(object sender, EventArgs e)
        {
            View.GetDataFromDb();
            // 个数
            if (tabPane2.SelectedPageIndex == 1)
            {
                // 显示左边统计
                gridControl_statistic.DataSource = View.ComputeGeShu(View.DbDataTable);
                // 选择了哪些
                List<string> selectedList = Pc28DevUtils.GetWhichSelected(gridView_statistic, "isShowInChart", "名称");
                统计(selectedList);
            }
            // 数字
            if (tabPane2.SelectedPageIndex == 0)
            {
                // 显示数字
                gridControl_shuzi.DataSource = View.ComputeShuzi(View.DbDataTable);
                // 选择了哪些
                List<string> selectedList = Pc28DevUtils.GetWhichSelected(gridView_shuzi, "isContainNumber", "shuziName");
                数字(selectedList);
            }

        }
        // 数字
        private void 数字(List<string> selectedList)
        {

            List<int> select = selectedList.Select(x => int.Parse(x)).ToList<int>();

            DataTable _moniDataTable = Pc28Utils._统计(View.DbDataTable, select, "");
            // 显示chartItemData
            gridView_chartData.Columns.Clear();
            gridControl_chartData.DataSource = _moniDataTable;
            gridView_chartData.RefreshData();
            gridView_chartData.BestFitColumns();
            gridView_chartData.Columns["期号"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView_chartData.FocusedRowHandle = 0;
            // 显示chart
            List<string> yFields = new List<string>();
            List<string> ySecondFields = new List<string>();
            List<string> ySecondchazhi = new List<string>();

            for (int i = 0; i < _moniDataTable.Columns.Count; i++)
            {
                if (
                    _moniDataTable.Columns[i].ColumnName.Contains("个数") ||
                    _moniDataTable.Columns[i].ColumnName.Contains("标准值")

                    )
                {
                    yFields.Add(_moniDataTable.Columns[i].ColumnName);
                }
                if (_moniDataTable.Columns[i].ColumnName.Contains("总盈利"))
                {
                    ySecondFields.Add(_moniDataTable.Columns[i].ColumnName);
                }
                if (_moniDataTable.Columns[i].ColumnName.Contains("差值") ||
                      _moniDataTable.Columns[i].ColumnName.Contains("间隔")
                      )
                {
                    ySecondchazhi.Add(_moniDataTable.Columns[i].ColumnName);
                }
            }
            chartControl_statistic.Series.Clear();
            Pc28Utils._图表ByDt(chartControl_statistic, ViewType.Line, _moniDataTable, "日期", yFields.ToArray(), ySecondFields.ToArray());
            Pc28Utils._图表ByDt(chartControl_statistic, ViewType.Area, _moniDataTable, "日期", ySecondchazhi.ToArray(), null);

        }
        // 统计
        private void 统计(List<string> selectedList)
        {
            // 获取所有投注
            List<NumberStatistic> geShuList = View.GeShuList;

            // 新建一个dt,用于合并
            DataTable _moniDataTable = new DataTable();
            _moniDataTable.Columns.Add("期号", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add("日期", Type.GetType("System.String"));
            _moniDataTable.Columns.Add("pc28", Type.GetType("System.Int32"));
            _moniDataTable.PrimaryKey = new DataColumn[] { _moniDataTable.Columns["期号"], _moniDataTable.Columns["日期"], _moniDataTable.Columns["pc28"] };

            foreach (NumberStatistic item in geShuList)
            {
                if (selectedList.Contains(item.Name))
                {
                    DataTable _moni = Pc28Utils._统计(View.DbDataTable, item.TouZhuHaoMa, item.Name);

                    _moniDataTable.Merge(_moni);
                }
            }
            // 清空  
            gridView_chartData.Columns.Clear();
            gridControl_chartData.DataSource = _moniDataTable;
            gridView_chartData.RefreshData();
            gridView_chartData.BestFitColumns();
            gridView_chartData.Columns["期号"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView_chartData.FocusedRowHandle = 0;
            // 显示chart
            List<string> yFields = new List<string>();
            List<string> ySecondFields = new List<string>();
            List<string> ySecondchazhi = new List<string>();

            for (int i = 0; i < _moniDataTable.Columns.Count; i++)
            {
                if (
                    _moniDataTable.Columns[i].ColumnName.Contains("个数") ||
                    _moniDataTable.Columns[i].ColumnName.Contains("标准值")

                    )
                {
                    yFields.Add(_moniDataTable.Columns[i].ColumnName);
                }
                if (_moniDataTable.Columns[i].ColumnName.Contains("总盈利")
                    )
                {
                    ySecondFields.Add(_moniDataTable.Columns[i].ColumnName);
                }
                if (_moniDataTable.Columns[i].ColumnName.Contains("差值") ||
                    _moniDataTable.Columns[i].ColumnName.Contains("间隔"))
                {
                    ySecondchazhi.Add(_moniDataTable.Columns[i].ColumnName);
                }
            }
            chartControl_statistic.Series.Clear();
            Pc28Utils._图表ByDt(chartControl_statistic, ViewType.Line, _moniDataTable, "日期", yFields.ToArray(), ySecondFields.ToArray());
            Pc28Utils._图表ByDt(chartControl_statistic, ViewType.Area, _moniDataTable, "日期", ySecondchazhi.ToArray(), null);

        }
        // 前一天
        private void Button_lastDay_Click(object sender, EventArgs e)
        {
            dateEdit_startDate.DateTime = dateEdit_startDate.DateTime.AddDays(-1);
            dateEdit_endDate.DateTime = dateEdit_endDate.DateTime.AddDays(-1);
            //button_statistic.PerformClick();
        }
        // 后一天
        private void Button_nextDay_Click(object sender, EventArgs e)
        {
            dateEdit_startDate.DateTime = dateEdit_startDate.DateTime.AddDays(1);
            dateEdit_endDate.DateTime = dateEdit_endDate.DateTime.AddDays(1);
            //button_statistic.PerformClick();
        }
        // 最近期数
        private void SpinEdit_last_EditValueChanged(object sender, EventArgs e)
        {
            View.LastNumOfExpect = int.Parse(((SpinEdit)sender).Value.ToString());
        }
        // 开始日期
        private void DateEdit_startDate_EditValueChanged(object sender, EventArgs e)
        {
            View.StartDateTime = ((DateEdit)sender).DateTime;
        }
        // 结束日期
        private void DateEdit_endDate_EditValueChanged(object sender, EventArgs e)
        {
            View.EndDateTime = ((DateEdit)sender).DateTime;
        }
        // radio
        private void RadioGroup_statisticType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = ((RadioGroup)sender).SelectedIndex;
            if (selectIndex == 0)
            {
                View.WhichMethod = GetDataWhichMethod._根据最近期数获取;
            }
            if (selectIndex == 1)
            {
                View.WhichMethod = GetDataWhichMethod._根据日期获取;
            }
            if (selectIndex == 2)
            {
                View.WhichMethod = GetDataWhichMethod._根据期号获取;
            }
        }
        // 更新按钮
        XtraFormUpdate FormUpdate;
        private void Button_updateDb_Click(object sender, EventArgs e)
        {
            if (FormUpdate == null)
            {
                FormUpdate = new XtraFormUpdate(this);
            }
            if (FormUpdate.IsDisposed)
            {
                FormUpdate = new XtraFormUpdate(this);
            }

            FormUpdate.Show();
            FormUpdate.Activate();
        }
        // 统计 CheckedChanged
        private void CheckEdit_isShowInChart_CheckedChanged(object sender, EventArgs e)
        {
            // 这个for的功能是单选,注释掉可以变成多选
            for (int i = 0; i < gridView_statistic.RowCount; i++)
            {
                if (gridView_statistic.FocusedRowHandle == i) { continue; }
                gridView_statistic.SetRowCellValue(i, "isShowInChart", false);
            }
            // 
            List<string> selectedList = Pc28DevUtils.GetWhichSelected(gridView_statistic, "isShowInChart", "名称", sender);
            统计(selectedList);

            XmlConfig xmlConfig = new XmlConfig("xml/isShowInChart.xml");
            xmlConfig.DeleteAll("root");
            foreach (string item in selectedList)
            {
                xmlConfig.ModModify(item, "true");
            }

        }
        // 数字 CheckedChanged
        private void RepositoryItemCheckEdit_isContainNumber_CheckedChanged(object sender, EventArgs e)
        {

            List<string> selectedList = Pc28DevUtils.GetWhichSelected(gridView_shuzi, "isContainNumber", "shuziName", sender);
            数字(selectedList);

            XmlConfig xmlConfig = new XmlConfig("xml/isCantainNumber.xml");
            xmlConfig.DeleteAll("root");
            foreach (string item in selectedList)
            {
                xmlConfig.ModModify(item, "true");
            }
        }
        // 数字 自定义单元格颜色
        private void GridView_shuzi_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.Column.FieldName == "shuziNumber")
            {
                if (e.CellValue != null && e.CellValue.ToString() != "")
                {

                    int shuziNumber = int.Parse((gridView_shuzi.GetDataRow(e.RowHandle)["shuziNumber"]).ToString());
                    int shuziStandard = int.Parse((gridView_shuzi.GetDataRow(e.RowHandle)["shuziStandard"]).ToString());
                    if (shuziNumber < shuziStandard)
                    {
                        // #FFFBFF
                        e.Appearance.ForeColor = Color.Black;
                        e.Appearance.BackColor = Color.FromArgb(203, 204, 210);
                    }
                }
            }
        }
        // 个数 自定义颜色
        private void GridView_statistic_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "个数")
            {
                if (e.CellValue != null && e.CellValue.ToString() != "")
                {

                    int shuziNumber = int.Parse((gridView_statistic.GetDataRow(e.RowHandle)["个数"]).ToString());
                    int shuziStandard = int.Parse((gridView_statistic.GetDataRow(e.RowHandle)["标准"]).ToString());
                    if (shuziNumber < shuziStandard)
                    {
                        // #FFFBFF
                        e.Appearance.ForeColor = Color.Black;
                        e.Appearance.BackColor = Color.FromArgb(203, 233, 205);
                    }
                }
            }
        }

        #endregion

        #region 窗口最大化最小化移动设置
        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                navigationFrame_main.SelectedPage = navigationPage_autoTouZhu;
                Button_zoushitu.Enabled = false;
                button_statistic.Enabled = false;
                Button_zidingyimoni.Enabled = false;
                Button_auto_模拟.Enabled = true;
                Button_auto_Chart.Enabled = true;
                labelControl_算法1.Enabled = false;
                spinEdit1.Enabled = false;
            }
            else
            {
                navigationFrame_main.SelectedPage = navigationPage_statistic;
                Button_zoushitu.Enabled = true;
                button_statistic.Enabled = true;
                Button_zidingyimoni.Enabled = true;
                Button_auto_模拟.Enabled = false;
                Button_auto_Chart.Enabled = false;
                labelControl_算法1.Enabled = true;
                spinEdit1.Enabled = true;
            }
        }
        // 皮肤
        private void dropDownButton_skin_Click(object sender, EventArgs e)
        {
            if (galleryControl_skin.Visible)
            {

                galleryControl_skin.Visible = !galleryControl_skin.Visible;
                galleryControl_skin.SendToBack();
            }
            else
            {
                galleryControl_skin.Visible = !galleryControl_skin.Visible;
                galleryControl_skin.BringToFront();

            }
        }
        // 皮肤
        private void galleryControl_skin_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            XmlConfig xmlControl = new XmlConfig("./xml/skin.xml");

            dropDownButton_skin.Text = e.Item.Caption;
            xmlControl.ModModify("skin", e.Item.Caption);

        }
        // 关闭窗口
        private void Button_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        // 最大化窗口
        private void Button_max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
        }
        // 最小化窗口
        private void Button_min_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        bool beginMove = false;//初始化
        int currentXPosition;
        int currentYPosition;
        private void panelControl_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标     
            }
        }

        private void panelControl_title_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态                     currentYPosition = 0; 
                beginMove = false;
            }
        }

        private void panelControl_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void panelControl_title_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
        }

        #endregion

        #region 真实自动投注
        public bool IsChromeOpen()
        {
            if (driver == null)
            {
                return false;
            }
            try
            {
                string title = driver.Title;
                if (driver != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        IWebDriver driver;
        private void 投注模式编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsChromeOpen())
            {
                XtraMessageBox.Show("请点击打开chrome浏览器,并登陆");
                return;
            }
            try
            {
                if (gridView_autoTouZhu.FocusedColumn.AbsoluteIndex < 0) { return; }
                //gridControl_autoTouZhu
                double[] touzhu = new double[28];
                string saveName = gridView_autoTouZhu.FocusedColumn.FieldName;
                for (int i = 0; i <= 27; i++)
                {
                    string g = gridView_autoTouZhu.GetRowCellValue(i, gridView_autoTouZhu.FocusedColumn.FieldName).ToString();
                    if (g == "") { g = "0"; }
                    touzhu[i] = double.Parse(g);
                }

                // 导航
                driver.Url = "http://www.pceggs.com/play/pg28ModesEdit.aspx";
                driver.FindElement(By.XPath(@"//*[@id=""form1""]/div[4]/div[2]/table[3]/tbody/tr[2]/td/table/tbody/tr/td[3]/button[1]")).Click();

                for (int i = 0; i <= 27; i++)
                {
                    if (touzhu[i] == 0)
                    {
                        continue;
                    }
                    //IWebElement ee = driver.FindElement(By.Id("txt" + i.ToString()));
                    IWebElement touzhuButton = driver.FindElement(By.XPath($@"//*[@id=""form1""]/div[4]/div[2]/table[3]/tbody/tr[{i + 4}]/td[2]/input[1]"));
                    touzhuButton.SendKeys(touzhu[i].ToString());
                }
                ////点击保存
                driver.FindElement(By.XPath(@"//*[@id=""form1""]/div[4]/div[2]/table[3]/tbody/tr[2]/td/table/tbody/tr/td[3]/button[2]")).Click();
                // 填写保存名字
                driver.FindElement(By.Id("SaveModename")).SendKeys(saveName);
                //// 保存
                driver.FindElement(By.XPath(@"//*[@id=""Notice_btn""]/div[1]/a")).Click();
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.Message);
            }

        }

        private void 打开浏览器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string title = driver.Title;
                if (driver != null)
                {
                    DialogResult dr = XtraMessageBox.Show("您已打开chrome,点击确定重新打开chrome", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        driver.Quit();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }

            ChromeOptions options = new ChromeOptions();
            //options.AddArguments(@"user-data-dir=C:\Users\Administrator\AppData\Local\Google\Chrome\User Data");
            this.driver = new ChromeDriver(options);
            driver.Url = "http://www.pceggs.com";
        }
        #endregion
    }
}
