using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace yy.util
{
    class Sqlite3Helper
    {
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="filePath">文件地址 例如:d:\test\123.db</param>
        public static void CreateDB(string filePath)
        {
            SQLiteConnection cn = new SQLiteConnection("data source=" + filePath);
            cn.Open();
            cn.Close();


        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="filePath">文件地址 例如:d:\test\123.db</param>
        public static void DeleteDB(string filePath)
        {

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="command"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static DataSet GetDs(string filePath,string command, string tablename="")
        {
            DataSet dataSet = new DataSet();
            SQLiteConnection cn = new SQLiteConnection("data source=" + filePath);
            cn.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(command, cn))
            {
                using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                {
                    if (string.Empty.Equals(tablename))
                    {
                        sQLiteDataAdapter.Fill(dataSet);
                    }
                    else
                    {
                        sQLiteDataAdapter.Fill(dataSet, tablename);
                    }
                }
            }
            cn.Close();
            return dataSet;
        }

        /// <summary>
        /// 运行一条语句(插入,删除,更新,replace)
        /// INSERT INTO t1(id,age) VALUES('99999',11)
        /// REPLACE INTO t1(id,age) VALUES(@id,@age)
        /// DELETE FROM t1 WHERE id='99999'
        /// delete
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sqlString"></param>
        public static void ExecuteSqlString(string filePath,string sqlString)
        {
            SQLiteConnection cn = new SQLiteConnection("data source=" + filePath);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = cn;
            // 插入一条数据
            cmd.CommandText = sqlString;
            cmd.ExecuteNonQuery();

            cmd.CommandText = "VACUUM";
            cmd.ExecuteNonQuery();

            cn.Close();
        }
        //---事务
        public static void TransActionOperate(SQLiteConnection cn, SQLiteCommand cmd)
        {
            using (SQLiteTransaction tr = cn.BeginTransaction())
            {
                string s = "";
                int n = 0;
                cmd.CommandText = "INSERT INTO t2(id,score) VALUES(@id,@score)";
                cmd.Parameters.Add("id", DbType.String);
                cmd.Parameters.Add("score", DbType.Int32);
                for (int i = 0; i < 10; i++)
                {
                    s = i.ToString();
                    n = i;
                    cmd.Parameters[0].Value = s;
                    cmd.Parameters[1].Value = n;
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
            }
        }
    }
}
