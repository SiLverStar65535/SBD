using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBD.InterfacePool
{
    /// <summary>
    /// 畫面界面
    /// </summary>
  interface PageInterface
    {
        /// <summary>
        /// 工作秀的畫面
        /// </summary>
        void showWork();
        /// <summary>
        /// 工作關閉的畫面
        /// </summary>
        void CloseWork();
        /// <summary>
        /// 工作錯誤畫面畫面
        /// </summary>
        void NGWork();

        /// <summary>
        /// 下一站工作
        /// </summary>
        void NextWork();

        /// <summary>
        /// 回上一站工作
        /// </summary>
        void UPWork();


        // private WorkStepInterface mainWorkStep;

        new WorkStepInterface MainWorkStep { get; set; }
    }
}
