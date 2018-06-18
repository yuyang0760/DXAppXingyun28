using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DXAppXingyun28.common;
using DXAppXingyun28.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using yy.util;
using 使用API播放音乐;

namespace DXAppXingyun28.Util
{
    enum _常用投注
    {
        _中,
        _边,
        _大,
        _小,
        _单,
        _双,
        _3余0,
        _3余1,
        _3余2,
        _4余0,
        _4余1,
        _4余2,
        _4余3,
        _5余0,
        _5余1,
        _5余2,
        _5余3,
        _5余4,
        _9到18,
        _8到19
    }

    /// <summary>
    /// pc28工具类
    /// </summary>
    class Pc28Utils
    {
        /// <summary>
        /// pc28赔率,投注,概率
        /// </summary>
        /// <returns></returns>
        public static List<Odds> GetOdds()
        {
            List<Odds> re = new List<Odds>();
            double[] oddds = new double[28] { 1000, 333.33, 166.67, 100, 66.67, 47.62, 35.71, 27.78, 22.22, 18.18, 15.87, 14.49, 13.7, 13.33, 13.33, 13.7, 14.49, 15.87, 18.18, 22.22, 27.78, 35.71, 47.62, 66.67, 100, 166.67, 333.33, 1000 };
            for (int i = 0; i <= 27; i++)
            {
                Odds odds = new Odds();
                odds.opencode = i;
                odds.odds = oddds[i];
                odds.probability = Math.Round(1000 / oddds[i] / 1000, 4);
                odds.touzhu = int.Parse(Math.Floor(odds.probability * 10000).ToString());
                re.Add(odds);
            }
            return re;
        }

        public static List<(string name, List<int> haoMa)> Get_常用投注()
        {
            List<(string name, List<int> haoMa)> list = new List<(string name, List<int> haoMa)>();
            list.Add(("单", GetCode(x => x % 2 == 1)));
            list.Add(("双", GetCode(x => x % 2 == 0)));
            list.Add(("大", GetCode(x => x >= 14)));
            list.Add(("小", GetCode(x => x <= 13)));
            list.Add(("中", GetCode(x => x >= 10 && x <= 17)));
            list.Add(("边", GetCode(x => x <= 9 || x >= 18)));
            list.Add(("3余0", GetCode(x => x % 3 == 0)));
            list.Add(("3余1", GetCode(x => x % 3 == 1)));
            list.Add(("3余2", GetCode(x => x % 3 == 2)));
            list.Add(("4余0", GetCode(x => x % 4 == 0)));
            list.Add(("4余1", GetCode(x => x % 4 == 1)));
            list.Add(("4余2", GetCode(x => x % 4 == 2)));
            list.Add(("4余3", GetCode(x => x % 4 == 3)));
            list.Add(("5余0", GetCode(x => x % 5 == 0)));
            list.Add(("5余1", GetCode(x => x % 5 == 1)));
            list.Add(("5余2", GetCode(x => x % 5 == 2)));
            list.Add(("5余3", GetCode(x => x % 5 == 3)));
            list.Add(("5余4", GetCode(x => x % 5 == 4)));
            list.Add(("8-19", GetCode(x => x >= 8 && x <= 19)));
            list.Add(("去12-15", GetCode(x => x <= 11 || x >= 16)));
            list.Add(("去11-16", GetCode(x => x <= 10 || x >= 17)));

            return list;

        }

        public static bool IsTrue(string s)
        {
            if (s == null) { return false; }
            if (s.Trim().ToLower() == "true") { return true; }
            return false;
        }

        public static List<int> GetCode(Func<int, bool> func)
        {
            List<int> list = new List<int>();
            for (int i = 0; i <= 27; i++)
            {
                if (func.Invoke(i)) { list.Add(i); }
            }
            return list;
        }

        /// <summary>
        /// 根据数据库数据[pc28,期号,日期] 和 投注号码 统计出一些数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="touzhuCode">投注号码</param>
        public DataTable _统计(DataTable db, List<int> touzhuCode, string name)
        {

            DataTable _moniDataTable = new DataTable();
            if (name == null) { name = ""; }
            _moniDataTable.Columns.Add("期号", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add("日期", Type.GetType("System.String"));
            _moniDataTable.Columns.Add("pc28", Type.GetType("System.Int32"));
            _moniDataTable.PrimaryKey = new DataColumn[] { _moniDataTable.Columns["期号"], _moniDataTable.Columns["日期"], _moniDataTable.Columns["pc28"] };

            // 要显示的表格
            _moniDataTable.Columns.Add(name + "_盈利", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add(name + "_总盈利", Type.GetType("System.Int32"));

            _moniDataTable.Columns.Add(name + "_概率", Type.GetType("System.String"));
            _moniDataTable.Columns.Add(name + "_投注", Type.GetType("System.String"));
            _moniDataTable.Columns.Add(name + "_间隔", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add(name + "_个数", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add(name + "_标准值", Type.GetType("System.Int32"));
            _moniDataTable.Columns.Add(name + "_差值", Type.GetType("System.Int32"));

            // 计算
            touzhuCode.Sort(((a, b) => a.CompareTo(b)));
            // 投注
            string touzhu = string.Join(",", touzhuCode);
            // 概率
            List<Odds> pc28Odds = GetOdds();
            var (_总概率, _投注总和) = _获取投注信息ByCode(touzhuCode);

            double pc28SumOfprobability = _总概率;
            // 总和
            int touzhuSum = _投注总和;

            #region 为 计算标准线 做准备
            // 计算出现的次数
            int chuXian = int.Parse(Math.Floor(db.Rows.Count * pc28SumOfprobability).ToString());
            if (chuXian == 0) { chuXian = 1; }
            // 计算间隔
            double jiange = db.Rows.Count * 1.0 / chuXian;

            // next
            int NextNum = int.Parse(Math.Round(jiange).ToString());
            int numberbiaozhun = 1;
            #endregion

            // 计算表格中的数据
            int number = 0;
            int zongyingli = 0;
            int _n期没开 = -1;
            for (int i = db.Rows.Count - 1; i >= 0; i--)
            {
                int yingli = 0;
                _n期没开++;
                if (touzhuCode.Contains(int.Parse(db.Rows[i]["pc28"].ToString()))) { _n期没开 = 0; number++; }
                int opencode = int.Parse(db.Rows[i]["pc28"].ToString());
                if (touzhuCode.Contains(opencode))
                {
                    // 包含说明赢利 ,计算盈利了多少
                    // 1. 投了多少 2. 获得多少
                    yingli = (int)(pc28Odds[opencode].touzhu * pc28Odds[opencode].odds - touzhuSum);
                }
                else
                {
                    // 没包含说明没盈利,计算亏了多少
                    yingli = 0 - touzhuSum;

                }
                // 这是总盈利
                zongyingli += yingli;
                // 计算标准线
                int biaozhunxian = 0;
                // 如果遇到下一个数字,标准线就+1,并且改变下一个数字的值
                if (NextNum == (db.Rows.Count - 1 - i))
                {
                    biaozhunxian = ++numberbiaozhun;
                    NextNum = int.Parse(Math.Round(jiange * numberbiaozhun).ToString());
                }
                else
                {
                    biaozhunxian = numberbiaozhun;
                }
                _moniDataTable.Rows.Add(new object[] { db.Rows[i]["期号"], db.Rows[i]["日期"], db.Rows[i]["pc28"], yingli, zongyingli, pc28SumOfprobability.ToString("P"), touzhu, _n期没开, number, biaozhunxian, number - biaozhunxian });

            }

            return _moniDataTable;

        }


        public void _图表ByDt(ChartControl chartControl, ViewType viewType, DataTable dataSource, string xField, string[] yFields, string[] secondyFields)
        {
            // 十字 里面
            chartControl.SetCrosshair(true);       // 设置是否显示十字标
            chartControl.Legend.Direction = LegendDirection.LeftToRight;   // 说明文字
            chartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;    // 说明文字
            chartControl.Legend.AlignmentVertical = LegendAlignmentVertical.Top; // 说明文字

            if (yFields != null)
            {


                // 添加 第一个y轴
                for (int i = 0; i < yFields.Length; i++)
                {
                    Series _baseSeries = new Series(yFields[i], viewType);
                    _baseSeries.ArgumentDataMember = xField;
                    _baseSeries.ValueDataMembers.AddRange(yFields[i]);
                    _baseSeries.DataSource = dataSource;
                    _baseSeries.LegendTextPattern = "{S}";
                    _baseSeries.Visible = true;
                    chartControl.Series.Add(_baseSeries);
                }
            }
            if (secondyFields == null) { return; }
            if (chartControl.Diagram == null) { return; }
            ((XYDiagram)chartControl.Diagram).SecondaryAxesY.Clear();
            for (int i = 0; i < secondyFields.Length; i++)
            {

                Series _baseSeries = new Series(secondyFields[i], viewType);
                _baseSeries.ArgumentDataMember = xField;
                _baseSeries.ValueDataMembers.AddRange(secondyFields[i]);
                _baseSeries.DataSource = dataSource;
                _baseSeries.LegendTextPattern = "{S}";
                _baseSeries.Visible = true;
                // 定义 第二y轴
                SecondaryAxisY myAxisY = new SecondaryAxisY("my Y-Axis");
                ((XYDiagram)chartControl.Diagram).SecondaryAxesY.Add(myAxisY);
                ((LineSeriesView)_baseSeries.View).AxisY = myAxisY;
                chartControl.Series.Add(_baseSeries);
            }

        }


        /// <summary>
        /// 获取投注号码 获取一些信息
        /// </summary>
        /// <param name="touzhuCode">投注号码1,3,4,5 注意这里是号码,不是投注数量</param>
        /// <param name="number">总数量</param>
        /// <returns></returns>
        public (double _概率, int _投注总和) _获取投注信息ByCode(List<int> touzhuCode)
        {
            double probabilityAll = 0;
            int sum = 0;
            List<Odds> oddsList = GetOdds();
            for (int i = 0; i < touzhuCode.Count; i++)
            {

                // 计算总概率
                probabilityAll += oddsList[touzhuCode[i]].probability;
                sum += oddsList[touzhuCode[i]].touzhu;

            }
            return (probabilityAll, sum);
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns></returns>
        public static string NumberToChinese(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "零";
                    break;
            }
            if (str.Length > 1)
            {
                switch (str.Length)
                {
                    case 2:
                    case 6:
                        res += "十";
                        break;
                    case 3:
                    case 7:
                        res += "百";
                        break;
                    case 4:
                        res += "千";
                        break;
                    case 5:
                        res += "万";
                        break;
                    default:
                        res += "";
                        break;
                }
                res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            }
            if (res.Length == 3 && res[0] == '一')
            {
                res = res[1].ToString() + res[2].ToString();
            }
            return res;
        }


        /// <summary>
        /// 获取盈利百分比
        /// </summary>
        /// <param name="touzhuCode">投注号码</param>
        /// <param name="opencode">开奖号码</param>
        /// <returns></returns>
        public double _获取盈利百分比(List<int> touzhuCode, int opencode)
        {

            if (!touzhuCode.Contains(opencode))
            {
                return -1;
            }

            double sum = 0;
            List<Odds> oddsList = GetOdds();
            for (int i = 0; i < touzhuCode.Count; i++)
            {
                // 计算总概率
                sum += oddsList[touzhuCode[i]].touzhu;
            }
            return (oddsList[opencode].odds * oddsList[opencode].touzhu - sum) / sum;

        }
        /// <summary>
        /// 获取盈利百分比
        /// </summary>
        /// <param name="touzhuCode">投注号码</param>
        /// <param name="opencode">开奖号码</param>
        /// <returns></returns>
        public double _获取盈利百分比(List<int> touzhuCode)
        {

            double sum = 0;
            List<Odds> oddsList = GetOdds();
            for (int i = 0; i < touzhuCode.Count; i++)
            {

                // 计算总概率
                sum += oddsList[touzhuCode[i]].probability;

            }
            return (1 - sum) / sum;

        }


        public void _计算保存到xml(List<int> _投注号码, double _投注数量)
        {
            // 下期投注,放到xml中
            XmlConfig xmlConfig = new XmlConfig("./xml/autotouzhuReal.xml");
            string[] touzhu = xmlConfig.Search("touzhuCode").Split(',');
            double[] touzhuNumber = _计算投注数量(int.Parse(Math.Floor(_投注数量).ToString()), _投注号码);

            // 保存到xml
            xmlConfig.ModModify("touzhuNumber", string.Join(",", touzhuNumber));
        }


        /// <summary>
        /// 获取数据库 数据
        /// </summary>
        /// <param name="whichMethod">那种方法</param>
        /// <returns></returns>
        public DataTable GetDataFromDb(string dbName, string tableName, GetDataWhichMethod WhichMethod, int LastNumOfExpect, DateTime StartDateTime, DateTime EndDateTime, int StartExpect, int EndExpect)
        {
            string sqlString = "";

            if (WhichMethod == GetDataWhichMethod._根据最近期数获取)
            {
                sqlString = $"select pc28,opentime as 日期,expect as 期号 from {tableName} order by  expect desc" + $" limit {LastNumOfExpect}";


            }
            if (WhichMethod == GetDataWhichMethod._根据日期获取)
            {
                sqlString = $"select pc28,opentime as 日期,expect as 期号 from {tableName} where opentime between '" + StartDateTime.ToString("yyyy-MM-dd") + "' and '" + EndDateTime.AddDays(1).ToString("yyyy-MM-dd") + "'" + "order by  expect desc";
            }
            if (WhichMethod == GetDataWhichMethod._根据期号获取)
            {
                if (EndExpect == 0 || EndExpect <= StartExpect)
                {
                    sqlString = $"select pc28,opentime as 日期,expect as 期号 from {tableName} where expect >= {StartExpect} " + "order by  expect desc";
                }
                else
                {
                    sqlString = $"select pc28,opentime as 日期,expect as 期号 from {tableName} where expect >= {StartExpect} and expect <= {EndExpect} " + "order by  expect desc";
                }
            }
            // 获取数据库pc28数据
            DataTable dbDataTable = Sqlite3Helper.GetDs(dbName, sqlString, tableName).Tables[tableName];
            // 更新字段 并且 返回
            return dbDataTable;
        }


        /// <summary>
        /// 梭哈 根据投注数量和投注号码,计算各个蛋蛋投注数量
        /// </summary>
        /// <param name="dandan">蛋蛋数量 如 1111</param>
        /// <param name="touzhuCode">投注号码,如10,11,12,13</param>
        /// <returns></returns>
        public double[] _计算投注数量(int dandan, List<int> touzhuCode)
        {
            double[] re = new double[28];
            List<Odds> oddsList = GetOdds();
            double sum = 0;
            for (int i = 0; i < touzhuCode.Count; i++)
            {
                sum += oddsList[touzhuCode[i]].touzhu;
            }
            double meiyixiang = dandan / sum;

            for (int i = 0; i < touzhuCode.Count; i++)
            {
                re[touzhuCode[i]] = Math.Floor(meiyixiang * oddsList[touzhuCode[i]].touzhu);
            }

            return re;
        }

        /// <summary>
        /// 获取常用投注
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<int> _获取常用投注(_常用投注 d)
        {
            List<int> re = new List<int>();
            for (int i = 0; i <= 27; i++)
            {
                switch (d)
                {
                    case _常用投注._中:
                        if (i >= 10 && i <= 17) { re.Add(i); }
                        break;
                    case _常用投注._边:
                        if (i < 10 || i > 17) { re.Add(i); }
                        break;
                    case _常用投注._大:
                        if (i > 13) { re.Add(i); }
                        break;
                    case _常用投注._小:
                        if (i < 14) { re.Add(i); }
                        break;
                    case _常用投注._单:
                        if (i % 2 == 1) { re.Add(i); }
                        break;
                    case _常用投注._双:
                        if (i % 2 == 0) { re.Add(i); }
                        break;
                    case _常用投注._3余0:
                        if (i % 3 == 0) { re.Add(i); }

                        break;
                    case _常用投注._3余1:
                        if (i % 3 == 1) { re.Add(i); }

                        break;
                    case _常用投注._3余2:
                        if (i % 3 == 2) { re.Add(i); }

                        break;
                    case _常用投注._4余0:
                        if (i % 4 == 0) { re.Add(i); }

                        break;
                    case _常用投注._4余1:
                        if (i % 4 == 1) { re.Add(i); }

                        break;
                    case _常用投注._4余2:
                        if (i % 4 == 2) { re.Add(i); }

                        break;
                    case _常用投注._4余3:
                        if (i % 4 == 3) { re.Add(i); }

                        break;
                    case _常用投注._5余0:
                        if (i % 5 == 0) { re.Add(i); }

                        break;
                    case _常用投注._5余1:
                        if (i % 5 == 1) { re.Add(i); }

                        break;
                    case _常用投注._5余2:
                        if (i % 5 == 2) { re.Add(i); }

                        break;
                    case _常用投注._5余3:
                        if (i % 5 == 3) { re.Add(i); }

                        break;
                    case _常用投注._5余4:
                        if (i % 5 == 4) { re.Add(i); }

                        break;
                    case _常用投注._9到18:
                        if (i >= 9 && i <= 18) { re.Add(i); }

                        break;
                    case _常用投注._8到19:
                        if (i >= 8 && i <= 19) { re.Add(i); }

                        break;
                }
            }
            return re;
        }

        /// <summary>
        /// 将数据库数据[pc28,日期,期号] 转换为 走势图 数据
        /// </summary>
        /// <param name="db">数据库数据</param>
        /// <returns></returns>
        public DataTable _走势图(DataTable db, (int startNumber, int endNumber) rangeOfMiddle)
        {
            DataTable dtTable = new DataTable();
            // 添加列
            dtTable.Columns.Add("期号", Type.GetType("System.Int32"));
            dtTable.Columns.Add("日期", Type.GetType("System.String"));
            for (int i = 0; i <= 27; i++)
            {
                dtTable.Columns.Add(i.ToString(), Type.GetType("System.Int32"));
            }
            dtTable.Columns.Add("单", Type.GetType("System.String"));
            dtTable.Columns.Add("双", Type.GetType("System.String"));
            dtTable.Columns.Add("中", Type.GetType("System.String"));
            dtTable.Columns.Add("边", Type.GetType("System.String"));
            dtTable.Columns.Add("大", Type.GetType("System.String"));
            dtTable.Columns.Add("小", Type.GetType("System.String"));
            dtTable.Columns.Add("大尾", Type.GetType("System.String"));
            dtTable.Columns.Add("小尾", Type.GetType("System.String"));
            dtTable.Columns.Add("/3", Type.GetType("System.Int32"));
            dtTable.Columns.Add("/4", Type.GetType("System.Int32"));
            dtTable.Columns.Add("/5", Type.GetType("System.Int32"));

            for (int i = 0; i < db.Rows.Count; i++)
            {
                string date = db.Rows[i]["日期"].ToString();
                int pc28 = int.Parse(db.Rows[i]["pc28"].ToString());
                int expect = int.Parse(db.Rows[i]["期号"].ToString());
                // 添加新行
                DataRow newDataRow = dtTable.NewRow();
                // 赋值
                newDataRow["日期"] = date;
                newDataRow["期号"] = expect;
                newDataRow[pc28.ToString()] = pc28;
                newDataRow["单"] = pc28 % 2 == 1 ? "单" : "";
                newDataRow["双"] = pc28 % 2 == 0 ? "双" : "";
                newDataRow["大"] = pc28 >= 14 ? "大" : "";
                newDataRow["小"] = pc28 <= 13 ? "小" : "";
                newDataRow["中"] = (pc28 >= rangeOfMiddle.startNumber && pc28 <= rangeOfMiddle.endNumber) ? "中" : "";
                newDataRow["边"] = (pc28 < rangeOfMiddle.startNumber || pc28 > rangeOfMiddle.endNumber) ? "边" : "";
                newDataRow["/3"] = pc28 % 3;
                newDataRow["/4"] = pc28 % 4;
                newDataRow["/5"] = pc28 % 5;
                newDataRow["大尾"] = pc28 % 10 >= 5 ? "大" : "";
                newDataRow["小尾"] = pc28 % 10 <= 4 ? "小" : "";

                dtTable.Rows.Add(newDataRow);
            }
            return dtTable;
        }


    }
}
