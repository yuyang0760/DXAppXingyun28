using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DXAppXingyun28.Util;

namespace DXAppXingyun28
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 界面汉化
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            BonusSkins.Register();
            SkinManager.EnableFormSkins();
 
     
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Application.Run(new MainForm());
        }
    }
}
