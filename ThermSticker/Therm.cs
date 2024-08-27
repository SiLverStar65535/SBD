using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SBD.Domain.Models;

namespace Therm_Sticker
{
    /// <summary>
    /// version 1.2     update 2024/04/18
    /// </summary>
    public static class Therm
    {

        [DllImport("tsclibnet.dll", EntryPoint = "test")]
        public static extern string test();

        [DllImport("tsclibnet.dll", EntryPoint = "TscUDP_Run")]
        public static extern string TscUDP_Run();

        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "printer_barcode_qrcodeA")]
        public static extern int printer_barcode_qrcodeA(string type,
            string barcode_direction, string x_squre_size, string model,
            string error_capability, string mask, string entry_method,
            int y, int x, string text);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

        static TSCSDK.driver driver = new TSCSDK.driver();
        static string Text;
        //static int printlinesiz = 32;//列印行高

        public static void PrintData(string PrintName, string InputText)
        {

            //-------------------設定-----------------------------------
            driver.openport(PrintName);//印表機名稱
            driver.sendcommand("SIZE 51 mm, 451  mm");//紙張大小  寬,高
            driver.sendcommand("GAP 7.2mm,0mm");//兩張貼紙間隔
            driver.sendcommand("SPEED 3");//印出速度
            driver.sendcommand("DENSITY 15");//列印顏色深度
            driver.sendcommand("DIRECTION 1");//鏡像 
            driver.sendcommand("SET TEAR ON");
            driver.sendcommand("CODEPAGE UTF-8");
            driver.clearbuffer(); // 清除記憶體
            //------------------------------------------------------------


            //dpi x=300  *12 ,  y=200  *8
            //x-10mm
            int x = 0; //起始定位
            int y = 0;
            if (InputText == "")
                Text = InputText;
            else
                Text = InputText;


            Build_(46, 24, 1, 180, Text);//倒轉大 標籤+條碼


            Small_barcode(40, 41, 180, Text);//倒轉小 條碼
            Small_barcode(40, 59, 180, Text);//倒轉小 條碼


            Big_barcode(44, 70, 90, Text);//大 橫條碼
            Build_TSADomestic(38, 128, 180, Text);//松山國內線標誌DOWN
            Big_barcode(10, 130, 0, Text);//長 條碼
            Build_TSADomestic(12, 285, 0, Text);//松山國內線標誌UP
            Big_barcode(44, 315, 90, Text);//大 橫條碼



            Build_(5, 359, 1, 0, Text);//旅客資訊+條碼
            Build_(5, 401, 1, 0, Text);//旅客資訊標籤+條碼
            ClosePrint();

        }
        /// <summary>
        /// 行李列印資訊
        /// </summary>
        /// <param name="Loc_x"></param>
        /// <param name="Loc_y"></param>
        /// <param name="With_Bar"></param>
        /// <param name="upsidedown"></param>
        /// <param name="行李資訊"></param>
        private static void Build_(int Loc_x, int Loc_y, int With_Bar, int upsidedown, Luggage luggage)
        {

            Loc_x = Loc_x * 12;
            Loc_y = Loc_y * 12;
            if (upsidedown == 0)
            {


                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", " BAG. IDENTIFICATION TAG");//列印組字
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", luggage.boardingPassData.DepartureAirport+" -> "+ luggage.boardingPassData.ArrivalAirport);//列印組字
                driver.windowsfont(Loc_x + 360, Loc_y + 36, 64, 0, 2, 0, "Calibri,", luggage.boardingPassData.SeatNumber);//列印組字->
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", luggage.boardingPassData.PassengerName );//列印組字
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", " AE0381/02OCT KNH");//列印組字
                driver.windowsfont(Loc_x + 360, Loc_y, 64, 0, 2, 0, "Calibri,", luggage.boardingPassData.BoardingGate);//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容

                if (With_Bar == 1)
                {
                    Loc_x = Loc_x + 36;
                    Loc_y = Loc_y + 60 + 12;
                    driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "45", "0", "0", "4", "10", luggage .TagNumber );//列印BarCode---測試用
                    // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容

                    Loc_x = Loc_x + 30;
                    Loc_y = Loc_y + 46;
                    driver.windowsfont(Loc_x, Loc_y, 32, 0, 2, 0, "標楷體", luggage.FlightTagNumber );//碼文
                    //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
                }
            }
            else if (upsidedown == 180)
            {

                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", " BAG. IDENTIFICATION TAG");//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", luggage.boardingPassData.DepartureAirport + " -> " + luggage.boardingPassData.ArrivalAirport);//列印組字
                driver.windowsfont(Loc_x - 360, Loc_y - 36, 64, 180, 2, 0, "Calibri,", luggage.boardingPassData.SeatNumber);//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", luggage.boardingPassData.PassengerName);//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", " "+ luggage.boardingPassData.FlightNumber +" / "+ luggage.boardingPassData.DepartureTime +"  " + luggage.boardingPassData.ArrivalAirport );//列印組字
                driver.windowsfont(Loc_x - 360, Loc_y, 64, 180, 2, 0, "Calibri,", luggage.boardingPassData.BoardingGate);//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容


                if (With_Bar == 1)
                {
                    Loc_x = Loc_x - 36;
                    Loc_y = Loc_y - 60 - 12;
                    driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "45", "0", "180", "4", "10", luggage.TagNumber);//列印BarCode
                    // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容

                    Loc_x = Loc_x - 30;
                    Loc_y = Loc_y - 46;
                    driver.windowsfont(Loc_x, Loc_y, 32, 180, 2, 0, "標楷體", luggage.FlightTagNumber);//碼文
                    //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
                }
            }
        }

        private static void Build_(int Loc_x, int Loc_y, int With_Bar, int upsidedown, string text)
        {

            Loc_x = Loc_x * 12;
            Loc_y = Loc_y * 12;
            if (upsidedown == 0)
            {


                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", " BAG. IDENTIFICATION TAG");//列印組字
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", "松山 -> 金門");//列印組字
                driver.windowsfont(Loc_x + 360, Loc_y + 36, 64, 0, 2, 0, "Calibri,", "016");//列印組字
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", "CHAN/JACK");//列印組字
                Loc_y = Loc_y + 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", " AE0381/02OCT KNH");//列印組字
                driver.windowsfont(Loc_x + 360, Loc_y, 64, 0, 2, 0, "Calibri,", "G10");//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容

                if (With_Bar == 1)
                {
                    Loc_x = Loc_x + 36;
                    Loc_y = Loc_y + 60 + 12;
                    driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "45", "0", "0", "4", "10", text);//列印BarCode---測試用
                    // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容

                    Loc_x = Loc_x + 30;
                    Loc_y = Loc_y + 46;
                    driver.windowsfont(Loc_x, Loc_y, 32, 0, 2, 0, "標楷體", "AE    148274");//碼文
                    //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
                }
            }
            else if (upsidedown == 180)
            {

                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", " BAG. IDENTIFICATION TAG");//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", "松山 -> 金門");//列印組字
                driver.windowsfont(Loc_x - 360, Loc_y - 36, 64, 180, 2, 0, "Calibri,", "016");//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", "CHAN/JACK");//列印組字
                Loc_y = Loc_y - 45;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", " AE0381/02OCT KNH");//列印組字
                driver.windowsfont(Loc_x - 360, Loc_y, 64, 180, 2, 0, "Calibri,", "G10");//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容


                if (With_Bar == 1)
                {
                    Loc_x = Loc_x - 36;
                    Loc_y = Loc_y - 60 - 12;
                    driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "45", "0", "180", "4", "10", text);//列印BarCode
                    // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容

                    Loc_x = Loc_x - 30;
                    Loc_y = Loc_y - 46;
                    driver.windowsfont(Loc_x, Loc_y, 32, 180, 2, 0, "標楷體", "AE    148274");//碼文
                    //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
                }
            }
        }
        private static void Big_barcode(int Loc_x, int Loc_y, int rotation, string text)
        {
            Loc_x = Loc_x * 12;
            Loc_y = Loc_y * 12;
            if (rotation == 0)
                driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "1800", "0", "0", "4", "10", text);//列印BarCode--直的
                                                                                                            // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容
            else if (rotation == 90)
                driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "450", "0", "90", "4", "10", text);//列印BarCode--橫的
                                                                                                            // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容
        }

        private static void Small_barcode(int Loc_x, int Loc_y, int upsidedown, string text)
        {
            Loc_x = Loc_x * 12;
            Loc_y = Loc_y * 12;
            if (upsidedown == 0)
            {

                driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "70", "0", "0", "4", "10", text);//列印BarCode                              
                // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容
                Loc_x = Loc_x + 40;
                Loc_y = Loc_y + 71;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 2, 0, "標楷體", "AE    148274");//碼文
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
            }
            else if (upsidedown == 180)
            {
                driver.barcode(Loc_x.ToString(), Loc_y.ToString(), "25", "70", "0", "180", "4", "10", text);//列印BarCode                              
                // x,y,條碼種類,高度(dot),碼文顯示,旋轉,寬窄因子,寬窄因子,條碼內容
                Loc_x = Loc_x - 40;
                Loc_y = Loc_y - 71;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 2, 0, "標楷體", "AE    148274");//碼文
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
            }
        }
        private static void Build_TSADomestic(int Loc_x, int Loc_y, int upsidedown, string text)
        {

            Loc_x = Loc_x * 12;
            Loc_y = Loc_y * 12;
            if (upsidedown == 0)
            {


                driver.windowsfont(Loc_x - 5, Loc_y, 64, 0, 0, 0, "標楷體", "華 信 航 空");//列印組字
                Loc_y = Loc_y + 68;
                driver.windowsfont(Loc_x, Loc_y, 24, 0, 0, 0, "標楷體", "   MANDARIN AIRLINES");//列印組字
                //driver.windowsfont(Loc_x + 360, Loc_y + 36, 64, 0, 2, 0, "Calibri,", "040");//列印組字
                Loc_y = Loc_y + 30;
                driver.windowsfont(Loc_x, Loc_y, 88, 0, 2, 0, "標楷體", "KINMEN");//列印組字
                Loc_y = Loc_y + 92;
                driver.windowsfont(Loc_x + 2, Loc_y, 96, 0, 2, 0, "標楷體", "金 門");//列印組字
                Loc_y = Loc_y + 100;
                driver.windowsfont(Loc_x, Loc_y, 32, 0, 0, 0, "標楷體", " AE  148274");//列印組字
                //driver.windowsfont(Loc_x + 360, Loc_y, 64, 0, 2, 0, "Calibri,", "G01");//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容
            }
            else if (upsidedown == 180)
            {

                driver.windowsfont(Loc_x + 2, Loc_y, 64, 180, 0, 0, "標楷體", "華 信 航 空");//列印組字
                Loc_y = Loc_y - 68;
                driver.windowsfont(Loc_x, Loc_y, 24, 180, 0, 0, "標楷體", "   MANDARIN AIRLINES");//列印組字
                //driver.windowsfont(Loc_x - 360, Loc_y - 36, 64, 180, 2, 0, "Calibri,", "040");//列印組字
                Loc_y = Loc_y - 30;
                driver.windowsfont(Loc_x, Loc_y, 88, 180, 2, 0, "標楷體", "KINMEN");//列印組字
                Loc_y = Loc_y - 92;
                driver.windowsfont(Loc_x - 2, Loc_y, 96, 180, 2, 0, "標楷體", "金 門");//列印組字
                Loc_y = Loc_y - 100;
                driver.windowsfont(Loc_x, Loc_y, 32, 180, 0, 0, "標楷體", " AE  148274");//列印組字
                //driver.windowsfont(Loc_x - 360, Loc_y, 64, 180, 2, 0, "Calibri,", "G01");//列印組字
                //x,y,文字高度,旋轉,字體(粗斜),底線,字形,文字內容

            }
        }

        private static void ClosePrint()
        {
            driver.printlabel("1", "1");
            driver.closeport();
        }

    }

}
