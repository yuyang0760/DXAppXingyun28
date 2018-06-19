using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace yy.util
{
    enum DownLoadSourceType
    {
        webClient,
        webRequest,
        httpWebRequest
    }
    class Util
    {

 
        /// <summary>
        ///  补0
        /// </summary>
        /// <param name="n">位数总长度</param>
        /// <returns></returns>
        public static string buling( int number, int n)
        {


            if (number.ToString().Length >= n)
            {
                return number.ToString();
            }
            else
            {
                return number.ToString().PadLeft(n, '0'); // 一共4位,位数不够时从左边开始用0补
            }
        }

        public static void SendEmail(string subject, string body)
        {
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress("1317030038@qq.com");
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress("13521022034@139.com"));
            //邮件标题。
            mailMessage.Subject = subject;
            //邮件内容。
            mailMessage.Body = body;

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient
            {
                //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
                Host = "smtp.qq.com",
                //使用安全加密连接。
                EnableSsl = true,
                //不和请求一块发送。
                UseDefaultCredentials = false,
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                Credentials = new NetworkCredential("1317030038@qq.com", "sicdkdbvtsnphfdi")
            };
            //发送
            client.Send(mailMessage);
        }
        /// <summary>
        /// 获取多个网页的网页源代码
        /// </summary>
        /// <param name="urlList">网址List</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static List<string> GetWebSource(List<string> urlList, Encoding encoding)
        {
            List<string> l = new List<string>();
            foreach (string item in urlList)
            {
                l.Add(GetWebSource(item, encoding));
            }
            return l;

        }


        /// <summary>
        /// 获取网页源代码 
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码</param>
        /// <param name="downLoadSourceType">下载方式</param>
        /// <returns></returns>
        public static string GetWebSource(string url, Encoding encoding, DownLoadSourceType downLoadSourceType = DownLoadSourceType.webClient)
        {
            if (downLoadSourceType == DownLoadSourceType.webClient)
            {
                return GetWebClient(url, encoding);
            }
            else if (downLoadSourceType == DownLoadSourceType.webRequest)
            {
                return GetWebRequest(url, encoding);
            }
            else if (downLoadSourceType == DownLoadSourceType.httpWebRequest)
            {
                return GetHttpWebRequest(url, encoding);
            }
            else
            {
                return GetWebClient(url, encoding);
            }

        }

        /// <summary>
        /// 获取网页源代码 WebClient 方式
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        private static string GetWebClient(string url, Encoding encoding)
        {

            try
            {
                WebClient webClient = new WebClient();
                webClient.Credentials = CredentialCache.DefaultCredentials;
                Stream stream = webClient.OpenRead(url);
                if (stream == null)
                {
                    return "";
                }
                StreamReader streamReader = new StreamReader(stream, encoding);
                string text = streamReader.ReadToEnd();
                if (text == null || text == "")
                {
                    return "";
                }
                stream.Close();
                webClient.Dispose();
                return text;
            }
            catch (Exception)
            {
                return "";
            }

        }

        /// <summary>
        /// 获取网页源代码 WebRequest 方式
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        private static string GetWebRequest(string url, Encoding encoding)
        {
            Uri uri = new Uri(url);
            WebRequest myReq = WebRequest.Create(uri);
            WebResponse result = myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, encoding);
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        /// <summary>
        /// 获取网页源代码 HttpWebRequest 方式
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        private static string GetHttpWebRequest(string url, Encoding encoding)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, encoding);
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }
    }
}
