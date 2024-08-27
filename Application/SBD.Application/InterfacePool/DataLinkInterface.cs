using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.InterfacePool
{   /// <summary>
    /// 資料連線界面
    /// </summary>
     interface IDataLinkInterface
    {   
        /// <summary>
        /// 輸入資料D:\svn20180331\2024年研發案\SBD\SBD\SBD\InterfacePool\DataLinkInterface.cs
        /// </summary>
        /// <param name="UserBoardingPassData"></param>
        void InputDataUser(BoardingPassData UserBoardingPassData);


        /// <summary>
        /// 輸出資料
        /// </summary>
        /// <returns></returns>
         BoardingPassData  OutputDataUser();

    }
}
