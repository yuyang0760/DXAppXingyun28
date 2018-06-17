using DXAppXingyun28.Util;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXAppXingyun28.common
{
    class StatisticItem
    {
        public string Name { get; set; } = "";
        public int Number { get; set; } = 0;
        public int Jiange { get; set; } = 0;

        public bool IsSendEmail { get; set; } = false;
        public int LastNumberOfExpect = 600000;
        public bool IsShowInChart { get; set; } = false;
        public List<int> Code { get; set; } = new List<int>();
        

        public StatisticItem()
        {
         
        }
 
    }
}
