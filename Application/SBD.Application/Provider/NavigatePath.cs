namespace SBD.Provider
{
    public static class NavigatePath
    {
        /// <summary>
        /// 掃描登機證(起始畫面)
        /// </summary>
        public const string Step1PageView = nameof(Step1PageView);

        /// <summary>
        /// 顯示登機證資料
        /// [取消] > Step1
        /// [資料確認] > Step3
        /// </summary>
        public const string Step2PageView = nameof(Step2PageView);

        /// <summary>
        /// 行李過磅,掃描行李面積和重量,顯示尺寸和重量
        /// [再一次] > Step1
        /// [資料確認] > Step4
        /// </summary>
        public const string Step3PageView = nameof(Step3PageView);

        /// <summary>
        /// 列印行李條碼
        /// [確認] > Step5
        /// </summary>
        public const string Step4PageView = nameof(Step4PageView);

        /// <summary>
        /// 啟動輸送帶前進存放行李
        /// [再一次] > Step3
        /// [行李存放] > Step6
        /// </summary>
        public const string Step5PageView = nameof(Step5PageView);

        /// <summary>
        /// 完成行李存放
        /// [下一件行李] > Step3
        /// [完成行李託運] > Step1
        /// </summary>
        public const string Step6PageView = nameof(Step6PageView);
    }
}
