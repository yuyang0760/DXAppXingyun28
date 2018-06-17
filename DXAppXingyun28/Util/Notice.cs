using DXAppXingyun28.common;
using DXAppXingyun28.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 使用API播放音乐;

namespace DXAppXingyun28.Util
{
    /// <summary>
    /// pc28通知类
    /// </summary>
    class Notice
    {
        /// <summary>
        /// 根据数据库数据,计算 并通知
        /// </summary>
        /// <param name="dbDataTable"></param>
        internal static void MyNotice(DataTable db)
        {
            // 最近2期未出现8-19
            _N期未出现(db, 2, 8, 19);
            // 获取shuziName
            Vieww vieww = new Vieww();
            DataTable shuziDt = vieww.ComputeShuzi(db);
            DataTable geshuDt = new Statistic(db).Show();

            // 5余 差值 -6
            _求5余(geshuDt, -6);

            // 50% 差值 -11
            _百分之50(shuziDt, -11);

        }

        private static void _百分之50(DataTable shuziDt, int chazhi)
        {

        }

        private static void _求5余(DataTable geshuDt, int chazhi)
        {
            for (int i = 0; i < geshuDt.Rows.Count; i++)
            {
                if (geshuDt.Rows[i]["名称"].ToString().Contains("5余"))
                {
                    if (int.Parse(geshuDt.Rows[i]["个数"].ToString()) - int.Parse(geshuDt.Rows[i]["标准"].ToString()) <= chazhi)
                    {
                        MP3Player mP3Player = new MP3Player();
                        mP3Player.PlayAsync("./NumberVoc/清碎.wav");
                    }

                }
            }
        }

        private static void _5余(DataTable shuziDt, int chazhi)
        {
            int geshu50 = 0;
            int biaozhun50 = 0;
            int geshu51 = 0;
            int biaozhun51 = 0;
            int geshu52 = 0;
            int biaozhun52 = 0;
            int geshu53 = 0;
            int biaozhun53 = 0;
            int geshu54 = 0;
            int biaozhun54 = 0;
            for (int i = 0; i < shuziDt.Rows.Count; i++)
            {
                if (int.Parse(shuziDt.Rows[i]["shuziName"].ToString()) % 5 == 0)
                {
                    geshu50 += int.Parse(shuziDt.Rows[i]["shuziNumber"].ToString());
                    biaozhun50 += int.Parse(shuziDt.Rows[i]["shuziStandard"].ToString());
                }
                if (int.Parse(shuziDt.Rows[i]["shuziName"].ToString()) % 5 == 1)
                {
                    geshu51 += int.Parse(shuziDt.Rows[i]["shuziNumber"].ToString());
                    biaozhun51 += int.Parse(shuziDt.Rows[i]["shuziStandard"].ToString());
                }
                if (int.Parse(shuziDt.Rows[i]["shuziName"].ToString()) % 5 == 2)
                {
                    geshu52 += int.Parse(shuziDt.Rows[i]["shuziNumber"].ToString());
                    biaozhun52 += int.Parse(shuziDt.Rows[i]["shuziStandard"].ToString());
                }
                if (int.Parse(shuziDt.Rows[i]["shuziName"].ToString()) % 5 == 3)
                {
                    geshu53 += int.Parse(shuziDt.Rows[i]["shuziNumber"].ToString());
                    biaozhun53 += int.Parse(shuziDt.Rows[i]["shuziStandard"].ToString());
                }
                if (int.Parse(shuziDt.Rows[i]["shuziName"].ToString()) % 5 == 4)
                {
                    geshu54 += int.Parse(shuziDt.Rows[i]["shuziNumber"].ToString());
                    biaozhun54 += int.Parse(shuziDt.Rows[i]["shuziStandard"].ToString());
                }
            }

            if (geshu50 - biaozhun50 <= chazhi || geshu51 - biaozhun51 <= chazhi || geshu52 - biaozhun52 <= chazhi ||
                geshu53 - biaozhun53 <= chazhi || geshu54 - biaozhun54 <= chazhi)
            {
                MP3Player mP3Player = new MP3Player();
                mP3Player.PlayAsync("./NumberVoc/清碎.wav");
            }
        }

        private static void _N期未出现(DataTable db, int N, int startNumber, int endNumber)
        {
            if (db.Rows.Count >= N)
            {
                bool isN = false;
                for (int i = 0; i <= N - 1; i++)
                {
                    int last = int.Parse(db.Rows[i]["pc28"].ToString());
                    if (last >= startNumber && last <= endNumber)
                    {
                        isN = true;
                        return;
                    }
                }
                if (!isN)
                {
                    MP3Player mP3Player = new MP3Player();
                    mP3Player.PlayAsync("./NumberVoc/清碎.wav");
                    //yy.util.Util.SendEmail($"{N}期未出现{startNumber}-{endNumber}", db.Rows[0]["日期"] + $" {N}期未出现{startNumber}-{endNumber}");
                }
            }
        }
    }
}
