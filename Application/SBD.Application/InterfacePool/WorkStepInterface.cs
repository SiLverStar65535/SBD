using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.InterfacePool
{   /// <summary>
    /// 工作程序
    /// </summary>
    public interface WorkStepInterface
    {
        BoardingPassData boardingPassData { get; set; }

        /// <summary>
        /// 掃描登機證(起始畫面)
        /// </summary>
        void Step0();
        /// <summary>
        /// 顯示登機證資料
        /// </summary>
        void Step1();
        /// <summary>
        /// 行李過磅,量行李面積預備
        /// </summary>
        void Step2();
        /// <summary>
        /// 行李過磅,量行李面積
        /// </summary>
        /// <param name="行李資料"></param>
        void Step3(Luggage luggage);
        /// <summary>
        /// 量完面積,顯示尺寸和重量
        /// </summary>
        /// <param name="行李資料"></param>
        void Step4(Luggage luggage);
        /// <summary>
        ///確認後列印行李條碼 
        /// </summary>
        void Step5(Luggage luggage);
        /// <summary>
        /// 二次確認,行李過磅,量行李面積,掃描行李
        /// </summary>
        void Step6();
        /// <summary>
        ///啟動輸送帶前進,列印收據
        /// </summary>
        void Step7();

        /// <summary>
        ///流程錯誤處理
        /// </summary>
        void StepNG();

    }
}
