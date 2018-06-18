using DXAppXingyun28.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXAppXingyun28.ViewModel
{
    /// <summary>
    /// 统计 例如,单,双,大,小,或者某一个数字的
    /// 个数,概率 等等
    /// </summary>
    class NumberStatistic
    {
        /// <summary>
        /// 投注号码
        /// </summary>
        public List<int> TouZhuHaoMa { get; set; } = new List<int>();
        /// <summary>
        /// 投注名称
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = false;
        /// <summary>
        /// 概率
        /// </summary>
        public double GaiLv { get; private set; } = 0;
        /// <summary>
        /// 最后间隔
        /// </summary>
        public int LastJianGe { get; private set; } = 0;
        /// <summary>
        /// 出现个数
        /// </summary>
        public int GeShu { get; private set; } = 0;
        /// <summary>
        /// 标准个数
        /// </summary>
        public int BiaoZhunGeShu { get; private set; } = 0;
        /// <summary>
        /// 总个数(也就是传过来的数据库的个数)
        /// </summary>
        public int ZongGeShu { get; private set; } = 0;

        public NumberStatistic(List<int> touZhuHaoMa, string name, bool isShow)
        {
            TouZhuHaoMa = touZhuHaoMa ?? throw new ArgumentNullException(nameof(touZhuHaoMa));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsShow = isShow;
        }
        /// <summary>
        /// 开始统计
        /// </summary>
        /// <param name="dt"></param>
        public void StartStatistic(DataTable dt, List<Odds> odds)
        {

            // [ 总个数 ]
            this.ZongGeShu = dt.Rows.Count;
            int geshu = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pc28 = int.Parse(dt.Rows[i]["pc28"].ToString());
                if (TouZhuHaoMa.Contains(pc28))
                {
                    geshu++;
                }
            }
            // [ 个数 ]
            this.GeShu = geshu;
            // [ 概率 ]
            double gailv = 0;
            for (int i = 0; i < TouZhuHaoMa.Count; i++)
            {
                gailv += odds[TouZhuHaoMa[i]].probability;
            }
            this.GaiLv = gailv;
            // [ 标准个数 ]
            this.BiaoZhunGeShu = int.Parse(Math.Floor(this.ZongGeShu * gailv).ToString());
            // [ 间隔 ]
            int jiange = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pc28 = int.Parse(dt.Rows[i]["pc28"].ToString());
                if (TouZhuHaoMa.Contains(pc28))
                {
                    this.LastJianGe = jiange;
                    break;
                }
                jiange++;
            }

        }
    }
}
