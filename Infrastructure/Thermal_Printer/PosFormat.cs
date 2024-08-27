using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermal_Printer
{
    static class PosFormat
    {
        /// <summary>
        /// 回傳狀態
        /// </summary>
        public  const string RealTimeRequestPrinterStatus1 = "\x10\x04\x04";
        // printer command define ------------------------------------------------------

        /// <summary>
        ///選擇整頁模式
        /// </summary>
        public const string SelectPageMode = "\x1B" + "\x2";

        public const string PrintAndCancelPageMode = "\x1B" + "\x3";

        public const string PrintNvImage = "\x1C" + "p" + "\x1" + "\x0";
        // Position Control Functions ------------------------------------------------------
        public const string SetAbsolutePrintPosition16Dots = "\x1B" + "$" + "\x10" + "\x0";
        public const string SetAbsolutePrintPosition40Dots = "\x1B" + "$" + "\x28" + "\x0";
        public static byte[] SetAbsolutePrintPosition208Dots = { 0x1B, (byte)'$', 208, 0 };
        public static byte[] SetAbsolutePrintPosition248Dots = { 0x1B, (byte)'$', 248, 0 };
        // cut Control Functions ------------------------------------------------------
        public const string PartialCut1 = "\x1B" + "m";
        /// <summary>
        /// 送紙及切紙
        /// </summary>
        public const string FeedPartialCut1 = "\x1D" + "VB" + "\x0";
        // Font Control Functions ------------------------------------------------------
        /// <summary>
        /// 設定字體大小 寬1高1 dot
        /// </summary>
        public const string SelectGlobalFontW1H1 = "\x1D" + "!" + "\x0";
        /// <summary>
        /// 設定字體大小 寬1高2 dot
        /// </summary>
        public const string SelectGlobalFontW1H2 = "\x1D" + "!" + "\x01";
        /// <summary>
        /// 設定字體大小 寬2高1 dot
        /// </summary>
        public const string SelectGlobalFontW2H1 = "\x1D" + "!" + "\x10";
        /// <summary>
        /// 設定字體大小 寬2高2 dot
        /// </summary>
        public const string SelectGlobalFontW2H2 = "\x1D" + "!" + "\x11";
        /// <summary>
        /// 設定字體大小 寬2高3 dot
        /// </summary>
        public const string SelectGlobalFontW2H3 = "\x1D" + "!" + "\x23";
        /// <summary>
        /// 設定字體 粗體
        /// </summary>
        public const string SelectFontBoldMode = "\x1B" + "E" + "\x1";
        /// <summary>
        /// 取消粗體
        /// </summary>
        public const string CancelFontBoldMode = "\x1B" + "E" + "\x0";
        // Paper Feed Functions ------------------------------------------------------
        /// <summary>
        /// 送紙24點
        /// </summary>
        public const string PaperFeed24Dots = "\x1B" + "J" + "\x18";
        /// <summary>
        /// 送紙12點
        /// </summary>
        public const string PaperFeed12Dots = "\x1B" + "J" + "\x0C";
        /// <summary>
        /// 指針倒退 -- 100 dots
        /// </summary>
        public const string PaperBackFeed100Dots = "\x1B" + "K" + "\x64";
        /// <summary>
        /// 指針倒退 -- 31 dots
        /// </summary>
        public const string PaperBackFeed31Dots = "\x1B" + "K" + "\x1F";
        /// <summary>
        /// 指針倒退 -- 72 dots
        /// </summary>
        public const string PaperBackFeed72Dots = "\x1B" + "K" + "\x48";
        // Align Format Functions ------------------------------------------------------
        /// <summary>
        /// 設定文字靠左
        /// </summary>
        public const string SelectAlignLeft = "\x1B" + "a" + "\x0";
        /// <summary>
        /// 設定文字至中
        /// </summary>
        public const string SelectAlignCenter = "\x1B" + "a" + "\x1";
        /// <summary>
        /// 設定文字靠右
        /// </summary>
        public const string SelectAlignRight = "\x1B" + "a" + "\x2";
        // 1D Barcode Function -----------------------------------------------------------
        /// <summary>
        /// 設定條碼寬度為 1 dot
        /// </summary>
        public const string SelectBarCodeWidthDot1 = "\x1D" + "w" + "\x2";
        /// <summary>
        /// 設定條碼高度為 48 dot
        /// </summary>
        public const string SelectBarCodeHighDot48 = "\x1D" + "h" + "\x48";
        /// <summary>
        ///  設定條碼寬度為 3 dot
        /// </summary>
        public const string SelectBarCodeWidthDot3 = "\x1D" + "w" + "\x3";
        /// <summary>
        ///  設定條碼高度為 12 dot
        /// </summary>
        public const string SelectBarCodeHighDot12 = "\x1D" + "h" + "\x12";
        /// <summary>
        /// 開始列印39code條碼，需搭配PrnBarCode39End使用。
        /// </summary>
        public const string PrnBarCode39Start = "\x1D" + "k" + "\x4";
        /// <summary>
        /// 結束列印39code條碼，需搭配PrnBarCode39Start使用。
        /// </summary>
        public const string PrnBarCode39End = "\x0";

        // QR Code Function -----------------------------------------------------------
        public const string SelectQrCodeSize3Dot = "\x1D" + "(k" + "\x3\x0\x31\x43\x3";
        public const string SelectQrCodeSize4Dot = "\x1D" + "(k" + "\x3\x0\x31\x43\x4";
        public const string SelectQrCodeErrorCorrectionLevelL = "\x1D" + "(k" + "\x3\x0\x31\x45\x30";
        public const string SelectQrCodeVersion6 = "\x1D" + "(k" + "\x3\x0\x31\x76\x6";
        public const string QrCodeDataStoreLeading = "\x1D" + "(k" + "\x3\x0\x31\x50\x30";
        public const string QrDataPrint = "\x1D" + "(k" + "\x3\x0\x31\x51\x30";
        public const string SelectQrCodeSize_TEST = "\x1D" + "(k" + "\x3\x0\x31\x43\x5";//----size for Qrcode

    }
}
