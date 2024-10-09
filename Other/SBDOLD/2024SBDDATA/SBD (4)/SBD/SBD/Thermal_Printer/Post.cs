using log4net;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermal_Printer
{
    /// <summary>
    /// version 1.2     update 2024/04/18
    /// </summary>
    public static class Post
    {
        private static ILog log = LogManager.GetLogger(typeof(Post));

        static SerialPort com = new SerialPort();

        /// <summary>
        /// comport放印表機的port號(格式:COM1)，
        /// List_data放你想列印的資料會自動排列
        /// </summary>
        public static void CallByCommandCode
            (string comport, List<string> List_data)
        {
            log.Info("Start print data.");

            //string timedata = DateTime.Now.ToString("yyyy-MM-dd");
            //string timehhmmss = DateTime.Now.ToString("hh:mm:ss");

            // initial com parameter ------------------------------------------------------
            com.PortName = comport;
            com.BaudRate = int.Parse("9600");
            com.Parity = Parity.None;
            com.DataBits = 8;
            com.StopBits = StopBits.One;
            com.Handshake = Handshake.None;
            com.Open();
            // check com hardware handshaking-----------------------------------------------
            // 以下資料需在整頁模式運作 -----------------------------------------------
            string ss = "";                       //定義列印用的字串
            //ss += SelectPageMode;               //整頁模式
            //ss += SelectAlignCenter;            //文字置中
            //ss += SelectFontBoldMode;           //文字加粗
            //ss += SelectGlobalFontW2H2;         //文字兩倍寬兩倍高
            //ss += "\n";                         //送行
            //ss += "BAGGAGE RECORD";             //"手改托行李清單"
            //ss += "\n\n";
            //ss += CancelFontBoldMode;           //取消粗體
            ss += PosFormat.SelectGlobalFontW1H1;         //1倍寬1倍高
            //ss += timedata + "            " + timehhmmss;
            //ss += "\n";
            byte[] bb = System.Text.Encoding.GetEncoding("Big5").GetBytes(ss);
            com.Write(bb, 0, bb.Length);
            bb.Initialize();

            //-----------------------------------------------------------------

            //ss += SelectAlignCenter + "----------------------------";
            //ss += SelectGlobalFontW2H2;         //文字兩倍寬兩倍高
            //ss += "\n\n";
            ss += Builder_(List_data) + "\n";
            //ss += "\n";
            //ss += "\n";
            //ss += PaperFeed12Dots;              //送紙12點
            //ss += SelectGlobalFontW1H1;
            //ss += CancelFontBoldMode;
            ss += "--------以上是測試結果--------\n";   
            ss += "ver0.1";  //

            ss += PosFormat.FeedPartialCut1;                          //送紙及切紙

            log.Info("Sending print data.");
            bb = System.Text.Encoding.GetEncoding("Big5").GetBytes(ss);
            com.Write(bb, 0, bb.Length);
            bb.Initialize();
            // Exit:
            com.Close();

            log.Info("Finish print data.");
        }

        private static string Builder_(List<string> Input)
        {
            log.Info("Start building data.");
            string Result = "";

            for(int i = 0; i <= Input.Count-1; i++)
            {
                Result += Input[i];
                Result += "\n";
            }

            log.Info("Finish building data.");
            return Result;
        }
        
        /// <summary>
        /// 成功則回傳comport號，失敗則回傳空字串。
        /// </summary>
        /// <returns></returns>
        public static string Comport_check(int com)
        {
            log.Info("----------"+"Start printer initinalize"+"----------");

            // initial com parameter
            Post.com.PortName = "COM" + com;
            Post.com.BaudRate = 9600;
            Post.com.Parity = Parity.None;
            Post.com.DataBits = 8;
            Post.com.StopBits = StopBits.One;
            Post.com.Handshake = Handshake.None;
            {
                try
                {
                    Post.com.Open();
                    byte[] combb = System.Text.Encoding.GetEncoding("Big5").GetBytes(PosFormat.RealTimeRequestPrinterStatus1);
                    Post.com.Write(combb, 0, combb.Length);
                    int comb = Post.com.ReadByte();
                    Post.com.Close();
                    if ("18".Equals(comb.ToString("D")) == true)
                    {
                        log.Info("COM" + com.ToString() + " success.");
                        log.Info("----------" + "Printer initinalize success" + "----------");
                        return Post.com.PortName;
                    }
                }
                catch
                {
                    log.Info("Try COM"+ com.ToString()+".");
                }
            }
            
            log.Warn("----------" + "Printer initinalize Fail" + "----------");
            return "";
        }
    }
}
