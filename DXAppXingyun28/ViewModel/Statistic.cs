using DXAppXingyun28.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace DXAppXingyun28.common
{
    /// <summary>
    ///  统计 类
    /// </summary>
    class Statistic
    {

        // 统计字典 
        public List<StatisticItem> StatisticItemList = new List<StatisticItem>();
        public Statistic(DataTable db)
        {
            StartStatistic(db);
        }

        /// <summary>
        /// 开始统计
        /// </summary>
        public void StartStatistic(DataTable db)
        {
            StatisticItemList.Clear();
            XmlConfig xmlConfig = new XmlConfig("xml/isShowInChart.xml");


            // 初始化 statisticItem (添加出了numberTable以外的其他数据)
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("单")), Name = "单", Code = getCode(x => x % 2 == 1), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("双")), Name = "双", Code = getCode(x => x % 2 == 0), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("大")), Name = "大", Code = getCode(x => x >= 14), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("小")), Name = "小", Code = getCode(x => x <= 13), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("中")), Name = "中", Code = getCode(x => x >= 10 && x <= 17), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("边")), Name = "边", Code = getCode(x => x <= 9 || x >= 18), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("3余0")), Name = "3余0", Code = getCode(x => x % 3 == 0), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("3余1")), Name = "3余1", Code = getCode(x => x % 3 == 1), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("3余2")), Name = "3余2", Code = getCode(x => x % 3 == 2), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("4余0")), Name = "4余0", Code = getCode(x => x % 4 == 0), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("4余1")), Name = "4余1", Code = getCode(x => x % 4 == 1), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("4余2")), Name = "4余2", Code = getCode(x => x % 4 == 2), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("4余3")), Name = "4余3", Code = getCode(x => x % 4 == 3), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("5余0")), Name = "5余0", Code = getCode(x => x % 5 == 0), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("5余1")), Name = "5余1", Code = getCode(x => x % 5 == 1), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("5余2")), Name = "5余2", Code = getCode(x => x % 5 == 2), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("5余3")), Name = "5余3", Code = getCode(x => x % 5 == 3), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("5余4")), Name = "5余4", Code = getCode(x => x % 5 == 4), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("8-19")), Name = "8-19", Code = getCode(x => x >= 8 && x <= 19), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("去12-15")), Name = "去12-15", Code = getCode(x => x <= 11 || x >= 16), LastNumberOfExpect = db.Rows.Count });
            StatisticItemList.Add(new StatisticItem { IsShowInChart = isTrue(xmlConfig.Search("去11-16")), Name = "去11-16", Code = getCode(x => x <= 10 || x >= 17), LastNumberOfExpect = db.Rows.Count });

            for (int i = 0; i < db.Rows.Count; i++)
            {
                int pc28 = int.Parse(db.Rows[i]["pc28"].ToString());
                for (int j = 0; j < StatisticItemList.Count; j++)
                {
                    if (StatisticItemList[j].Code.Contains(pc28))
                    {
                        StatisticItemList[j].Number++;
                    }
                }
            }
            for (int j = 0; j < StatisticItemList.Count; j++)
            {
                StatisticItemList[j].Jiange = computeJianGe(db, StatisticItemList[j].Code);
            }
        }

        public bool isTrue(string s)
        {
            if (s == null) { return false; }
            if (s.Trim().ToLower() == "true") { return true; }
            return false;
        }

        public List<int> getCode(Func<int, bool> func)
        {
            List<int> list = new List<int>();
            for (int i = 0; i <= 27; i++)
            {
                if (func.Invoke(i)) { list.Add(i); }
            }
            return list;
        }

        public int computeJianGe(DataTable db, List<int> l)
        {
            int sum = 0;
            for (int i = 0; i < db.Rows.Count; i++)
            {
                if (l.Contains(int.Parse(db.Rows[i]["pc28"].ToString())))
                {
                    break;
                }
                sum++;
            }
            return sum;

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        public DataTable Show()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("isShowInChart", Type.GetType("System.Boolean"));
            dt.Columns.Add("名称", Type.GetType("System.String"));
            dt.Columns.Add("间隔", Type.GetType("System.Int32"));
            dt.Columns.Add("个数", Type.GetType("System.Int32"));
            dt.Columns.Add("标准", Type.GetType("System.Int32"));
            dt.Columns.Add("最近N期", Type.GetType("System.Int32"));

            // 计算标准个数
            // 计算正常概率的数字的个数
            // 1. 获得概率
            // 这些投注的概率
            List<Odds> pc28Odds = Pc28Utils.GetOdds();

            foreach (StatisticItem item in StatisticItemList)
            {
                double allProbability = 0;
                foreach (int codeItem in item.Code)
                {
                    allProbability += pc28Odds[codeItem].probability;
                }

                dt.Rows.Add(new object[] { item.IsShowInChart, item.Name, item.Jiange, item.Number, int.Parse(Math.Floor(item.LastNumberOfExpect * allProbability).ToString()), item.LastNumberOfExpect });
            }
            return dt;
        }


    }
}
