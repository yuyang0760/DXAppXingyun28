using DXAppXingyun28.common;
using DXAppXingyun28.domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Xml;
using yy.util;
namespace DXAppXingyun28.util
{

    class Bjkl8Util
    {
        public WebSourceUrlType webSourceUrlType { get; set; } = (WebSourceUrlType)Properties.Settings.Default.WebSourceUrlType;
        private static string TableName = "bjkl8";

        public Bjkl8Util() { }

        public Bjkl8Util(WebSourceUrlType sourceUrlType)
        {
            this.webSourceUrlType = sourceUrlType;
        }
        /// <summary>
        /// 转换源代码到model
        /// </summary>
        /// <param name="webSource"></param>
        /// <returns></returns>
        private List<Bjkl8> ConvertWebSourceToModel(string webSource)
        {
            if (webSource == null || webSource == string.Empty)
            {
                Console.WriteLine("源代码为空");
                return null;
            }
            if (this.webSourceUrlType == WebSourceUrlType._500)
            {
                // 解析 webresource
                XmlControl xmlControl = new XmlControl();
                xmlControl.LoadXmlString(webSource);
                XmlNodeList xmlNodeList = xmlControl.SelectNodes("row");
                List<Bjkl8> bJKL8List = new List<Bjkl8>();
                foreach (XmlNode item in xmlNodeList)
                {
                    Bjkl8 bJKL8 = new Bjkl8();
                    bJKL8.Expect = int.Parse(item.SelectSingleNode("@expect").Value);
                    bJKL8.Opentime = Convert.ToDateTime(item.SelectSingleNode("@opentime").Value);
                    string[] sArr = item.SelectSingleNode("@opencode").Value.Split(',');
                    List<int> list = new List<int>();
                    foreach (string item1 in sArr)
                    {
                        list.Add(int.Parse(item1));
                    }
                    bJKL8.Opencode = list;
                    bJKL8List.Add(bJKL8);
                }
                bJKL8List.Reverse();
                return bJKL8List;
            }
            if (this.webSourceUrlType == WebSourceUrlType._168kai)
            {
                // 解析json  webresource
                JObject jo = (JObject)JsonConvert.DeserializeObject(webSource);
                if (jo["errorCode"].ToString() != "0" || jo["result"]["businessCode"].ToString() != "0") { return null; }

                // 3.分析后赋值到 bJKL8List
                List<Bjkl8> bJKL8List = new List<Bjkl8>();
                foreach (JObject item in jo["result"]["data"])
                {
                    Bjkl8 bJKL8 = new Bjkl8();
                    bJKL8.Expect = int.Parse(item["preDrawIssue"].ToString());
                    bJKL8.Opentime = Convert.ToDateTime(item["preDrawTime"].ToString());
                    string[] sArr = item["preDrawCode"].ToString().Split(',');

                    // 排除意外
                    if (sArr.Length < 2) { continue; }

                    // 去掉最后一个
                    List<string> sArrList = sArr.Take(sArr.Length - 1).ToList<string>();
                    // 转换成int
                    List<int> list = sArrList.Select<string, int>(x => int.Parse(x)).ToList<int>();
                    // 排序
                    list.Sort();
                    bJKL8.Opencode = list;
                    bJKL8List.Add(bJKL8);
                }
                bJKL8List.Reverse();
                return bJKL8List;

            }
            return null;
        }


        /// <summary>
        /// 下载指定日期的数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Bjkl8> Download(DateTime date)
        {
            //Console.WriteLine($"正在下载 {date.ToString("yyyy-MM-dd")} 的数据");
            string url = "";
            if (this.webSourceUrlType == WebSourceUrlType._168kai)
            {
                // https://api.api68.com/LuckTwenty/getBaseLuckTwentyList.do?date=2018-05-01&lotCode=10014
                // 1.获取网页源代码
                string dateString = date.ToString("yyyy-MM-dd");
                url = $"https://api.api68.com/LuckTwenty/getBaseLuckTwentyList.do?date={dateString}&lotCode=10014";

            }
            if (this.webSourceUrlType == WebSourceUrlType._500)
            {
                // http://kaijiang.500.com/static/info/kaijiang/xml/kl8/20180507.xml
                url = "http://kaijiang.500.com/static/info/kaijiang/xml/kl8/" + date.ToString("yyyyMMdd") + ".xml";


            }
            return ConvertWebSourceToModel(yy.util.Util.GetWebSource(url, Encoding.UTF8));
        }
        /// <summary>
        /// 下载 从开始日期 到结束日期的数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Bjkl8> Download(DateTime startDate1, DateTime endDate1, IProgress<string> progress)
        {
            Console.WriteLine($"正在下载 {startDate1.ToString("yyyy-MM-dd")} 到 {endDate1.ToString("yyyy-MM-dd")} 的数据");
            DateTime startDate = Convert.ToDateTime(startDate1.ToString("yyyy-MM-dd"));
            DateTime endDate = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));

            List<Bjkl8> re = new List<Bjkl8>();
            while (DateTime.Compare(startDate, endDate) <= 0)
            {
                progress.Report("正在更新:" + startDate.ToString("yyyy-MM-dd"));
                List<Bjkl8> listBjkl8Item = Download(startDate);
                if (listBjkl8Item == null) { continue; }
                foreach (Bjkl8 item in listBjkl8Item)
                {
                    re.Add(item);
                }
                //Console.WriteLine(startDate.ToString("yyyy-MM-dd") + " 下载完成");
                startDate = startDate.AddDays(1);
            }
            progress.Report("更新完成");
            return re;
        }

 

        /// <summary>
        /// 根据最近期号获取数据
        /// </summary>
        /// <param name="lastNumOfExpect"></param>
        /// <returns></returns>
        public static DataTable GetDataFromDb(int lastNumOfExpect)
        {
            string sqlString = $"select pc28,opentime as 日期,expect as 期号 from {TableName} order by  expect desc" + $" limit {lastNumOfExpect}";
            // 获取数据库pc28数据
            DataTable dbDataTable = Sqlite3Helper.GetDs(Properties.Settings.Default.dbName, sqlString, TableName).Tables[TableName];
            // 更新字段 并且 返回
            return dbDataTable;
        }


        /// <summary>
        /// 根据 开始日期 和 结束日期 获取数据
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static DataTable GetDataFromDb(DateTime startDate, DateTime endDate)
        {
            string sqlString = $"select pc28,opentime as 日期,expect as 期号 from {TableName} where opentime between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "'" + "order by  expect desc";
            // 获取数据库pc28数据
            DataTable dbDataTable = Sqlite3Helper.GetDs(Properties.Settings.Default.dbName, sqlString, TableName).Tables[TableName];
            // 更新字段 并且 返回
            return dbDataTable;
        }
        /// <summary>
        /// 根据开始期号 和 结束期号 获取数据
        /// </summary>
        /// <param name="startExpect">开始期号</param>
        /// <param name="endExpect">结束期号</param>
        /// <returns></returns>
        public static DataTable GetDataFromDb(int startExpect, int endExpect)
        {
            string sqlString;
            if (endExpect <= 0 || endExpect <= startExpect)
            {
                sqlString = $"select pc28,opentime as 日期,expect as 期号 from {TableName} where expect >= {startExpect} " + "order by  expect desc";
            }
            else
            {
                sqlString = $"select pc28,opentime as 日期,expect as 期号 from {TableName} where expect >= {startExpect} and expect <= {endExpect} " + "order by  expect desc";
            }

            // 获取数据库pc28数据
            DataTable dbDataTable = Sqlite3Helper.GetDs(Properties.Settings.Default.dbName, sqlString, TableName).Tables[TableName];
            // 更新字段 并且 返回
            return dbDataTable;
        }
        /// <summary>
        /// 保存数据到数据库
        /// </summary>
        /// <param name="listItem"></param>
        public void SaveData(string filePath, List<Bjkl8> listItem, IProgress<string> progress)
        {
            SQLiteConnection cn = new SQLiteConnection("data source=" + filePath);
            cn.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(cn))
            {
                using (SQLiteTransaction tr = cn.BeginTransaction())
                {
                    cmd.CommandText = "replace INTO bjkl8(expect,opentime,opencode,pc28,bj28) " +
                        "VALUES(@expect,@opentime,@opencode,@pc28,@bj28)";

                    cmd.Parameters.Add("expect", DbType.Int32);
                    cmd.Parameters.Add("opentime", DbType.DateTime);
                    cmd.Parameters.Add("opencode", DbType.String);
                    cmd.Parameters.Add("pc28", DbType.Int32);
                    cmd.Parameters.Add("bj28", DbType.Int32);

                    for (int i = 0; i < listItem.Count; i++)
                    {
                        Bjkl8 item = listItem[i];
                        cmd.Parameters["expect"].Value = item.Expect;
                        cmd.Parameters["opentime"].Value = item.Opentime;
                        cmd.Parameters["opencode"].Value = item.OpencodeString;
                        cmd.Parameters["pc28"].Value = item.Pc28();
                        cmd.Parameters["bj28"].Value = item.Bj28();
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                }

                cmd.CommandText = "VACUUM";
                cmd.ExecuteNonQuery();
            }
            cn.Close();
            progress.Report("保存完成");
        }
    }
}
