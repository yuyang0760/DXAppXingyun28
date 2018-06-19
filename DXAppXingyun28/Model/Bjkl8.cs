using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXAppXingyun28.domain
{
    /// <summary>
    /// 表示北京快乐8的每一期开奖的数据
    /// 包括开奖时间,期号,号码,pc28号码,bj28号码等等
    /// </summary>
    class Bjkl8 
    {
        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime Opentime { get; set; }
        /// <summary>
        /// 开奖号码
        /// </summary>
        public List<int> Opencode { get  ; set  ; }
        /// <summary>
        /// 开奖期号
        /// </summary>
        public int Expect { get  ; set  ; }

        public string OpencodeString
        {
            get
            {
                return string.Join(",", Opencode);
            }
        }
        public int Pc28()
        {
            
            int[] n= Opencode.ToArray();
            int num = 0;
            for (int i = 1; i <= 6; i++)
            {
                num += n[i - 1];
            }
            num %= 10;
            int num2 = 0;
            for (int i = 7; i <= 12; i++)
            {
                num2 += n[i - 1];
            }
            num2 %= 10;
            int num3 = 0;
            for (int i = 13; i <= 18; i++)
            {
                num3 += n[i - 1];
            }
            num3 %= 10;
            return num + num2 + num3;
        }

        public int Bj28( )
        {
            int[] n = Opencode.ToArray();

            int num = 0;
            for (int i = 2; i <= 17; i += 3)
            {
                num += n[i - 1];
            }
            num %= 10;
            int num2 = 0;
            for (int i = 3; i <= 18; i += 3)
            {
                num2 += n[i - 1];
            }
            num2 %= 10;
            int num3 = 0;
            for (int i = 4; i <= 19; i += 3)
            {
                num3 += n[i - 1];
            }
            num3 %= 10;
            return num + num2 + num3;
        }

        public int Bj16()
        {
            int[] n = Opencode.ToArray();

            int num = 0;
            for (int i = 1; i <= 16; i += 3)
            {
                num += n[i - 1];
            }
            num = num % 6 + 1;
            int num2 = 0;
            for (int i = 2; i <= 17; i += 3)
            {
                num2 += n[i - 1];
            }
            num2 = num2 % 6 + 1;
            int num3 = 0;
            for (int i = 3; i <= 18; i += 3)
            {
                num3 += n[i - 1];
            }
            num3 = num3 % 6 + 1;
            return num + num2 + num3;
        }

        public string BJ36ZN()
        {
            int[] n = Opencode.ToArray();

            int num = 0;
            for (int i = 2; i <= 17; i += 3)
            {
                num += n[i - 1];
            }
            num %= 10;
            int num2 = 0;
            for (int i = 3; i <= 18; i += 3)
            {
                num2 += n[i - 1];
            }
            num2 %= 10;
            int num3 = 0;
            for (int i = 4; i <= 19; i += 3)
            {
                num3 += n[i - 1];
            }
            num3 %= 10;
            string result = "";
            if (num == num2 && num2 == num3)
            {
                result = "豹子";
            }
            else if (num == num2 || num2 == num3 || num == num3)
            {
                result = "对子";
            }
            else
            {
                int[] array = new int[3]
                {
                num,
                num2,
                num3
                };
                Array.Sort(array);
                if (array[0] - array[1] == -1 && array[1] - array[2] == -1)
                {
                    result = "顺子";
                }
                else if (array[0] == 0 && array[1] == 8 && array[2] == 9)
                {
                    result = "顺子";
                }
                else if (array[0] == 0 && array[1] == 1 && array[2] == 9)
                {
                    result = "顺子";
                }
                else if (array[0] - array[1] == -1 && array[1] - array[2] != -1)
                {
                    result = "半顺";
                }
                else if (array[0] == 0 && array[2] == 9)
                {
                    result = "半顺";
                }
                else if (array[1] - array[2] == -1 && array[0] - array[1] != -1)
                {
                    result = "半顺";
                }
                else if (array[1] - array[2] != -1 && array[0] - array[1] != -1)
                {
                    result = "杂六";
                }
            }
            return result;
        }

        public int Bj36()
        {
            int[] n = Opencode.ToArray();

            int num = 0;
            for (int i = 2; i <= 17; i += 3)
            {
                num += n[i - 1];
            }
            num %= 10;
            int num2 = 0;
            for (int i = 3; i <= 18; i += 3)
            {
                num2 += n[i - 1];
            }
            num2 %= 10;
            int num3 = 0;
            for (int i = 4; i <= 19; i += 3)
            {
                num3 += n[i - 1];
            }
            num3 %= 10;
            int result = 0;
            if (num == num2 && num2 == num3)
            {
                result = 0;
            }
            else if (num == num2 || num2 == num3 || num == num3)
            {
                result = 1;
            }
            else
            {
                int[] array = new int[3]
                {
                num,
                num2,
                num3
                };
                Array.Sort(array);
                if (array[0] - array[1] == -1 && array[1] - array[2] == -1)
                {
                    result = 2;
                }
                else if (array[0] == 0 && array[1] == 8 && array[2] == 9)
                {
                    result = 2;
                }
                else if (array[0] == 0 && array[1] == 1 && array[2] == 9)
                {
                    result = 2;
                }
                else if (array[0] - array[1] == -1 && array[1] - array[2] != -1)
                {
                    result = 3;
                }
                else if (array[0] == 0 && array[2] == 9)
                {
                    result = 3;
                }
                else if (array[1] - array[2] == -1 && array[0] - array[1] != -1)
                {
                    result = 3;
                }
                else if (array[1] - array[2] != -1 && array[0] - array[1] != -1)
                {
                    result = 4;
                }
            }
            return result;
        }

    }
}
