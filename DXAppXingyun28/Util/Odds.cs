using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXAppXingyun28.Util
{
    /// <summary>
    ///  赔率
    /// </summary>
    struct Odds
    {
        /// <summary>
        /// 开奖号码
        /// </summary>
        public int opencode;
        /// <summary>
        /// 赔率
        /// </summary>
        public double odds;
        /// <summary>
        /// 中奖概率
        /// </summary>
        public double probability;
        /// <summary>
        /// 投注
        /// </summary>
        public int touzhu;

        public Odds(int opencode, double odds, double probability,int touzhu)
        {
            this.opencode = opencode;
            this.odds = odds;
            this.probability = probability;
            this.touzhu = touzhu;
        }
    }
    


}
