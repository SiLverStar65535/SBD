using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBD.enumPool
{
    public enum PrintBarCodeStatus
    {/// <summary>
     /// 說明條碼列印
     /// </summary>
        HelipBarPrint,

        /// <summary>
        /// 條碼列印中
        /// </summary>
        BarPrinting,
        /// <summary>
        /// 完成條碼列印
        /// </summary>
        BarPrintFlish
    }
}