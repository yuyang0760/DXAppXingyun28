using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraCharts;
using DXAppXingyun28.common;

using DXAppXingyun28.util;
using DXAppXingyun28.Util;
using DXAppXingyun28.ViewModel;
using yy.util;

namespace DXAppXingyun28.View
{
    class Vieww
    {
        public int EndExpect { get; set; } = 1;         // 结束期号
        public int StartExpect { get; set; } = 1;       // 开始期号
        public DateTime StartDateTime { get; set; } = DateTime.Now;       // 开始时间
        public DateTime EndDateTime { get; set; } = DateTime.Now;         // 结束时间
        public int LastNumOfExpect { get; set; } = 1000;                  // 查询最近多少期
        public GetDataWhichMethod WhichMethod { get; set; } = GetDataWhichMethod._根据日期获取; // 用什么方法获取数据


        public DataTable DbDataTable;                       // 数据库 数据
        public Statistic Statistic;                         // 统计类
        public List<NumberStatistic> ShuZiList = new List<NumberStatistic>();             // 数字List
        public List<NumberStatistic> GeShuList = new List<NumberStatistic>();             // 个数List

        public Dictionary<string, (string win, string fail, int[] code)> AutoTouZhu = new Dictionary<string, (string win, string fail, int[] code)>();

        public (int startNumber, int endNumber) rangeOfMiddle = (10, 17);    // 定义中的范围

        public Vieww()
        {

        }

        /// <summary>
        /// 获取数据库 数据
        /// </summary>
        /// <param name="whichMethod">那种方法</param>
        /// <returns></returns>
        public DataTable GetDataFromDb()
        {
            DataTable dbDataTable = null;

            if (this.WhichMethod == GetDataWhichMethod._根据最近期数获取)
            {
                dbDataTable = Bjkl8.GetDataFromDb(this.LastNumOfExpect);

            }
            if (this.WhichMethod == GetDataWhichMethod._根据日期获取)
            {
                dbDataTable = Bjkl8.GetDataFromDb(this.StartDateTime, this.EndDateTime);
            }
            if (this.WhichMethod == GetDataWhichMethod._根据期号获取)
            {
                dbDataTable = Bjkl8.GetDataFromDb(this.StartExpect, this.EndExpect);
            }
            Notice.MyNotice(dbDataTable);
            this.DbDataTable = dbDataTable;
            return dbDataTable;
        }

        // 计算左边统计个数
        public DataTable GetStatisticData()
        {

            this.Statistic = new Statistic(GetDataFromDb());
            return this.Statistic.Show();
        }
        // 计算左边统计个数
        public DataTable ComputeGeShu(DataTable db)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("isShowInChart", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("名称", Type.GetType("System.String"));
            dataTable.Columns.Add("间隔", Type.GetType("System.Int32"));
            dataTable.Columns.Add("个数", Type.GetType("System.Int32"));
            dataTable.Columns.Add("标准", Type.GetType("System.Int32"));
            dataTable.Columns.Add("最近N期", Type.GetType("System.Int32"));

            // 1. 获得概率
            List<Odds> pc28Odds = Pc28Utils.GetOdds();
            XmlConfig xmlConfig = new XmlConfig("xml/isShowInChart.xml");
            // 2. 计算
            GeShuList.Clear();
            List<(string name, List<int> haoMa)> cy_List = Pc28Utils.Get_常用投注();
            for (int i = 0; i < cy_List.Count; i++)
            {
                NumberStatistic numberStatistic = new NumberStatistic(cy_List[i].haoMa, cy_List[i].name, xmlConfig.Search(i.ToString()) == "true" ? true : false);
                numberStatistic.StartStatistic(db, pc28Odds);
                // 添加
                GeShuList.Add(numberStatistic);
                dataTable.Rows.Add(new object[] { numberStatistic.IsShow, numberStatistic.Name, numberStatistic.LastJianGe, numberStatistic.GeShu, numberStatistic.BiaoZhunGeShu, numberStatistic.ZongGeShu });
            }
            return dataTable;
        }
        // 计算左边数字
        public DataTable ComputeShuzi(DataTable db)
        {
            // 定义一个DataTable
            DataTable dataTable = new DataTable("shuzi");
            dataTable.Columns.Add("isContainNumber", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("shuziName", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziNumber", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziStandard", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziAllNumber", Type.GetType("System.Int32"));
            // 1. 获得概率
            List<Odds> pc28Odds = Pc28Utils.GetOdds();
            XmlConfig xmlConfig = new XmlConfig("./xml/isCantainNumber.xml");
            // 2. 计算
            ShuZiList.Clear();
            for (int i = 0; i <= 27; i++)
            {
                NumberStatistic numberStatistic = new NumberStatistic(new List<int>() { i }, i.ToString(), xmlConfig.Search(i.ToString()) == "true" ? true : false);
                numberStatistic.StartStatistic(db, pc28Odds);
                // 添加
                ShuZiList.Add(numberStatistic);
                dataTable.Rows.Add(new object[] { numberStatistic.IsShow, numberStatistic.Name, numberStatistic.GeShu, numberStatistic.BiaoZhunGeShu, numberStatistic.ZongGeShu });
            }
            // 显示
            return dataTable;
        }
    }
}
