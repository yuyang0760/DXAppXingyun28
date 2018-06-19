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
using yy.util;
using DXAppXingyun28.util;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DXAppXingyun28.domain;
using System.Threading;
using DXAppXingyun28.Util;
using 使用API播放音乐;

namespace DXAppXingyun28
{
    public partial class XtraFormUpdate : DevExpress.XtraEditors.XtraForm
    {
        private string currentUpdate = "bjkl8";
        private string dbfilePath = Properties.Settings.Default.dbName;
        private Progress<string> progress;
        private MainForm mainForm;
        // 测试按钮
        private void button1_Click(object sender, EventArgs e)
        {
            (mainForm.Controls["panelControl_statisticConfig"].Controls["simpleButton_zidingyimoni"] as SimpleButton).PerformClick();
        }
        
        #region 初始化等
        public XtraFormUpdate(MainForm mainForm)
        {
            this.mainForm = mainForm;
            //simpleButton_zidingyimoni
            InitializeComponent();
            this.rb_bjkl8.CheckedChanged += new System.EventHandler(this.Rb_CheckedChanged);
            this.rb_pk10.CheckedChanged += new System.EventHandler(this.Rb_CheckedChanged);

            // 初始化 progress
            progress = new Progress<string>();
            progress.ProgressChanged += Progress_ProgressChanged;
            // timer
            timerUpdateDb.Interval = Properties.Settings.Default.TimerUpdateDbInterval;
        }
        // 关闭窗口
        private void XtraFormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("formClosing");
            this.checkEdit_autoUpdate.Checked = false;
            this.timerUpdateDb.Stop();

        }
        // 总在最上
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkEdit1.Checked;
        }
        #endregion

        #region 更新数据库部分
        private void Progress_ProgressChanged(object sender, string e)
        {
            // 显示进度
            labelControl_updateDb.Text = e == "保存完成" ? "" : e;
            if (e == "保存完成")
            {
                this.button_update.Enabled = true;
 
                // 显示 走势图
                mainForm._走势图();
                // 统计按钮
                if (checkEdit_autotongji.Checked)
                {
                    (mainForm.Controls["panelControl_statisticConfig"].Controls["button_statistic"] as SimpleButton).PerformClick();
                }
                // 自定义模拟
                if (checkEdit_zidingmoni.Checked) 
                {
                    (mainForm.Controls["panelControl_statisticConfig"].Controls["Button_zidingyimoni"] as SimpleButton).PerformClick();
                }
                // 模拟自动投注
                if (checkEdit_autozidong.Checked)
                {
                    (mainForm.Controls["panelControl_statisticConfig"].Controls["Button_auto_模拟"] as SimpleButton).PerformClick();
                }

            }
        }


        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                currentUpdate = ((RadioButton)sender).Tag.ToString();

            }
        }

        // 查找
        private void Button_search_Click(object sender, EventArgs e)
        {
            // "SELECT distinct(date(shiJian)) FROM " + type
            string tablename = currentUpdate;
            string command = "SELECT distinct(date(opentime)) as 日期 FROM " + tablename;

            gridControl_search.DataSource = Sqlite3Helper.GetDs(dbfilePath, command, tablename).Tables[tablename];
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        public void UpdateTask()
        {
            // 查询数据的最新日期
            string tablename = currentUpdate;
            string command = "select max(opentime) from " + tablename;

            DataTable dt = Sqlite3Helper.GetDs(dbfilePath, command, tablename).Tables[tablename];
            string lastDateTimeString = dt.Rows[0][0].ToString() == "" ? "2011-01-01" : dt.Rows[0][0].ToString();

            Bjkl8Util bjkl8OrPk10 = new Bjkl8Util();
            // 利用反射 获取 Bjkl8 或 Pk10 对象
            //Type type = Type.GetType("DXAppXingyun28.util." + currentUpdate.Substring(0, 1).ToUpper() + currentUpdate.Substring(1));
            //dynamic bjkl8OrPk10 = type.Assembly.CreateInstance(type.ToString());

            // 根据最新日期 保存
            bjkl8OrPk10.SaveData(dbfilePath, bjkl8OrPk10.Download(Convert.ToDateTime(lastDateTimeString), DateTime.Now, progress), progress);
           
            // 等待5秒
             

        }

        // 更新
        private void Button_update_Click(object sender, EventArgs e)
        {
            this.button_update.Enabled = false;
            Task task = new Task(UpdateTask);
            task.Start();

        }

        #endregion

        #region 显示部分
        // timer
        private int shengyuSeconds;             // 剩余秒数
        private DateTime opentimeNext;          // 下一次开奖时间
        private string showString;              // 显示的字符串
        Bjkl8 bjklItem = new Bjkl8();
        private bool isReport = false;          // 是否qq自动报告
        private bool isAutoUpdateDb = false;    // 是否自动更新数据库
        private bool isPlayNumber = false;    // 是否自动更新数据库
        
        private void TimerUpdateDb_Tick(object sender, EventArgs e)
        {
            //button_update.PerformClick();
            if (shengyuSeconds <= 0.9)
            {
                timerUpdateDb.Stop();
                // 1.从网上获取数据
                string webSource = yy.util.Util.GetWebSource("https://api.api68.com/LuckTwenty/getBaseLuckTewnty.do?lotCode=10014", Encoding.UTF8);

                if (webSource == null || webSource == "") {
                    timerUpdateDb.Start();
                    return;
                }

                // 2. 分析后显示剩余多少时间 preDrawCode
                JObject jo = (JObject)JsonConvert.DeserializeObject(webSource);
                if (jo["errorCode"].ToString() != "0" || jo["result"]["businessCode"].ToString() != "0") { return; }

                int expect = int.Parse(jo["result"]["data"]["preDrawIssue"].ToString());
                int expectNext = int.Parse(jo["result"]["data"]["drawIssue"].ToString());
                string opencodeString = jo["result"]["data"]["preDrawCode"].ToString();
                DateTime opentime = Convert.ToDateTime(jo["result"]["data"]["preDrawTime"]);
                this.opentimeNext = Convert.ToDateTime(jo["result"]["data"]["drawTime"]);
                int expectNextCount = int.Parse(jo["result"]["data"]["drawCount"].ToString());
                int totalCount = int.Parse(jo["result"]["data"]["totalCount"].ToString());

                // 2.1 赋值到bjjkl8item
                bjklItem.Expect = expect;
                bjklItem.Opentime = opentime;
                bjklItem.Opencode = opencodeString.Split(',')
                    .Take<string>(opencodeString.Split(',').Length - 1)
                    .Select<string, int>(x => int.Parse(x)).ToList<int>();

                // 剩余时间
                shengyuSeconds = (int)(this.opentimeNext - DateTime.Now).TotalSeconds;

                labelControl_UpdateTIme.Text = "正在获取开奖..";
                isReport = true;
                isAutoUpdateDb = true;
                isPlayNumber = true;
                timerUpdateDb.Start();

            }
            else
            {
                // 2.1 分析显示时间
                shengyuSeconds = (int)(this.opentimeNext - DateTime.Now).TotalSeconds;
                showString =
                    "  时间:" + bjklItem.Opentime.ToString("MM-dd HH:mm") +
                    "  号码: <b>" + bjklItem.Pc28() +
                    "</b>  下一期开奖还剩: " + shengyuSeconds+" 秒";
                labelControl_UpdateTIme.Text = showString;
                // 显示并自动更新数据库
                if (checkEdit_autoUpdate.Checked && isAutoUpdateDb)
                {
                    button_update.PerformClick();
                    isAutoUpdateDb = false;

                }
                // 发送报告
                if (checkEdit_autoReport.Checked && isReport)
                {
 
                    SendKeys.Send(textEdit_autoReport.Text+ bjklItem.Pc28().ToString());
                    SendKeys.Send("{enter}");
                    isReport = false;
                }
                // 播放数字
                if (checkEdit_isPlayNumber.Checked && isPlayNumber)
                {
                    isPlayNumber = false;
                    MP3Player mP3Player = new MP3Player();

                    mP3Player.FilePath = $"./NumberVoc/{bjklItem.Pc28()}.wav";
                    mP3Player.PlayAsync();
                }
            }

        }

        //checkEdit 显示并自动更新
        private void CheckEditAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_autoUpdate.Checked)
            {
                this.timerUpdateDb.Start();
            }
            else
            {
                this.timerUpdateDb.Stop();
            }
        }
#endregion

    }
}