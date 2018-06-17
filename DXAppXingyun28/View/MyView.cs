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
                dbDataTable= Bjkl8.GetDataFromDb(this.LastNumOfExpect);

            }
            if (this.WhichMethod == GetDataWhichMethod._根据日期获取)
            {
                dbDataTable = Bjkl8.GetDataFromDb(this.StartDateTime,this.EndDateTime);
            }
            if (this.WhichMethod == GetDataWhichMethod._根据期号获取)
            {
                dbDataTable = Bjkl8.GetDataFromDb(this.StartExpect,this.EndExpect);
            }
            Notice.MyNotice(dbDataTable);
            this.DbDataTable = dbDataTable;
            return dbDataTable;
        }

        // 计算左边个数
        public DataTable GetStatisticData()
        {
   
            this.Statistic = new Statistic( GetDataFromDb());
            return this.Statistic.Show();
        }

        // 计算左边数字
        public DataTable ComputeShuzi(DataTable db)
        {
            //Console.WriteLine("你点击了测试按钮");
            // 定义一个DataTable
            DataTable dataTable = new DataTable("shuzi");
            dataTable.Columns.Add("isContainNumber", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("shuziName", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziNumber", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziStandard", Type.GetType("System.Int32"));
            dataTable.Columns.Add("shuziAllNumber", Type.GetType("System.Int32"));
            // 计算正常概率的数字的个数
            // 1. 获得概率
            // 这些投注的概率
            List<Odds> pc28Odds = Pc28Utils.GetOdds();
            XmlConfig xmlConfig = new XmlConfig("./xml/isCantainNumber.xml");

            for (int i = 0; i <= 27; i++)
            {
                int number = 0;
                for (int j = 0; j < db.Rows.Count; j++)
                {
                    int pc28 = int.Parse(db.Rows[j]["pc28"].ToString());
                    if (pc28 == i) { number++; }
                }
                dataTable.Rows.Add(new object[] { xmlConfig.Search(i.ToString()) == "true" ? true : false, i, number, int.Parse(Math.Floor(db.Rows.Count * pc28Odds[i].probability).ToString()), db.Rows.Count });
            }
            // 显示
            return dataTable;
        }
    }
}
