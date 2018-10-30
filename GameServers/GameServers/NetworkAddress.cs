using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace GameServers
{
    class NetworkAddress
    {
        /// <summary>
        /// 获得ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            IPAddress[] localIPs;
            localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection ipCollection = new StringCollection();
            foreach (var ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipCollection.Add(ip.ToString());
            }
            string[] ipArray = new string[ipCollection.Count];
            ipCollection.CopyTo(ipArray, 0);
            return ipArray[0];
        }

        [DllImport("Ws2_32.dll")]
        private static extern Int32 Inet_Addr(string ip);
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        /// <summary>
        /// 获得MAC地址
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string GetMacAddress(string ipAddress)
        {
            var ip = ipAddress;
            if (string.IsNullOrEmpty(ipAddress) || ipAddress == "127.0.0.1")
                ip = GetIPAddress();
            Int32 ldest = Inet_Addr(ip);
            Int64 mac = new Int64();
            Int32 len = 6;
            try
            {
                SendARP(ldest, 0, ref mac, ref len);
            }
            catch (Exception ex)
            {
                return "";
            }
            string originalMacAddress = Convert.ToString(mac, 16);
            if (originalMacAddress.Length < 12)
            {
                originalMacAddress = originalMacAddress.PadLeft(12, '0');
            }
            string macAddress;
            if (originalMacAddress != "0000" && originalMacAddress.Length == 12)
            {
                string mac1, mac2, mac3, mac4, mac5, mac6;
                mac1 = originalMacAddress.Substring(10, 2);
                mac2 = originalMacAddress.Substring(8, 2);
                mac3 = originalMacAddress.Substring(6, 2);
                mac4 = originalMacAddress.Substring(4, 2);
                mac5 = originalMacAddress.Substring(2, 2);
                mac6 = originalMacAddress.Substring(0, 2);
                macAddress = mac1 + "-" + mac2 + "-" + mac3 + "-" + mac4 + "-" + mac5 + "-" + mac6;
            }
            else
            {
                macAddress = "";
            }
            return macAddress.ToUpper();
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        /// <summary>
        /// 检查是否联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            int Desc = 0;
            return InternetGetConnectedState(Desc, 0);
        }

        /// <summary>
        /// 获取公网IP
        /// </summary>
        /// <returns>公网IP地址</returns>
        public static List<string> GetOuterIp()
        {
            List<string> ipInfo= new List<string>();
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string html = wc.DownloadString("http://ip.qq.com");
                    Match d = Regex.Match(html, "该IP所在地为：<span>([^<]+)</span>");
                    Match m = Regex.Match(html, "<span class=\"red\">([^<]+)</span>");
                    if (m.Success)
                    {
                        ipInfo.Add(m.Groups[1].Value);
                        if (d.Success)
                        {
                            var dValue = d.Groups[1].Value;
                            var newValue = dValue.Replace("&nbsp;", "");
                            if (newValue.Contains("您的IP没有分享记录"))
                                ipInfo.Add("获取IP所在地失败");
                            else
                                ipInfo.Add(newValue);
                        }
                    }
                    else
                    {
                        ipInfo.Add("获取公网IP失败");
                        ipInfo.Add("获取IP所在地失败");
                    }
                    return ipInfo;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
