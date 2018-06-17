

using System;
using System.Runtime.InteropServices;

namespace 使用API播放音乐
{
    public class MP3Player
    {
        public MP3Player() { }

        public MP3Player(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }


        /// <summary>   
        /// 文件地址   
        /// </summary>   
        public string FilePath;

        /// <summary>   
        /// 播放   
        /// </summary>   
        public void Play()
        {
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + FilePath + " alias media", "", 0, 0);
            mciSendString("play media wait", "", 0, 0);
        }
        /// <summary>   
        /// 播放   
        /// </summary>   
        public void Play(string filePath)
        {
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + filePath + " alias media", "", 0, 0);
            mciSendString("play media wait", "", 0, 0);
        }
        /// <summary>   
        /// 播放   
        /// </summary>   
        public void PlayAsync()
        {
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + FilePath + " alias media", "", 0, 0);
            mciSendString("play media", "", 0, 0);
        }
        /// <summary>   
        /// 播放   
        /// </summary>   
        public void PlayAsync(string filePath)
        {
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + filePath + " alias media", "", 0, 0);
            mciSendString("play media", "", 0, 0);
        }
        /// <summary>   
        /// 暂停   
        /// </summary>   
        public void Pause()
        {
            mciSendString("pause media", "", 0, 0);
        }

        /// <summary>   
        /// 停止   
        /// </summary>   
        public void Stop()
        {
            mciSendString("close media", "", 0, 0);
        }

        /// <summary>   
        /// API函数   
        /// </summary>   
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
         string lpstrCommand,
         string lpstrReturnString,
         int uReturnLength,
         int hwndCallback
        );
    }
}
