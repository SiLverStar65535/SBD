using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;
using System;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SBD
{
    public class MainWindowViewModel : BindableBase
    {
       
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel()
        {
           
        }
        public MainWindowViewModel(IRegionManager regionManager)
        {
         
            _regionManager = regionManager;
            ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }
     
        public string DeviceString { get; set; }
        


        private DelegateCommand<NaviInfo> _navigateCommand;
        public DelegateCommand<NaviInfo> NavigateCommand => _navigateCommand ??= new DelegateCommand<NaviInfo>(ExecuteNavigateCommand);
        private void ExecuteNavigateCommand(NaviInfo navigationPath)
        {
            if (navigationPath == null)
                throw new ArgumentNullException();
            
            _regionManager.RequestNavigate(
                  navigationPath.RegionName
                , navigationPath.NaviViewName
                , navigationPath.NavigationParameters);


            switch(navigationPath.NaviViewName)
            {
                case NavigatePath.Step1PageView:
                    StaticData.CurrentStep = "1" ;
                    break;
                case NavigatePath.Step2PageView:
                    StaticData.CurrentStep = "2";
                    break;
                case NavigatePath.Step3PageView:
                    StaticData.CurrentStep = "3";
                    break;
                case NavigatePath.Step4PageView:
                    StaticData.CurrentStep = "4";
                    break;
                case NavigatePath.Step5PageView:
                    StaticData.CurrentStep = "5";
                    break;
                case NavigatePath.Step6PageView:
                    StaticData.CurrentStep = "6";
                    break;

            }
           
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(ExecuteLoadedCommand);
        private void ExecuteLoadedCommand()
        {
            //强制使用英文输入法
            // 檢查comport與VID、PID，並檢查是否與預定的VID和PID匹配。

            if (Application.Current.MainWindow != null)
            {
                InputMethod.SetIsInputMethodEnabled(Application.Current.MainWindow, false);
            }

            // 清除之前掃描的COM端口列表和顯示結果
            DeviceString = string.Empty;

            // 使用WMI來搜索系統中的所有設備，篩選出含有COM端口描述的設備
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");
            //var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity ");
            var managmentObjects = searcher.Get();
            // 正則表達式，用於從設備描述中提取COM端口編號
            var comRegex = new Regex(@"\(COM(\d+)\)");

            try
            {
                var comCount = managmentObjects.Count;
                // 遍歷搜索結果
                foreach (var managmentObject in managmentObjects)
                {
                    // 獲取設備描述信息
                    string caption = managmentObject["Caption"].ToString();

                    // 使用正則表達式匹配COM端口
                    var matchCom = comRegex.Match(caption);

                    if (matchCom.Success)
                    {
                        // 成功匹配後提取COM端口號碼
                        string comPortNumberStr = matchCom.Groups[1].Value;
                        // 嘗試將提取的端口號碼字符串轉換為整數
                        if (int.TryParse(comPortNumberStr, out int comPortNumber))
                        {
                            // 提取設備ID，用於進一步匹配VID和PID
                            string deviceId = managmentObject["DeviceID"].ToString();
                            // 正則表達式，用於從設備ID中提取VID和PID
                            var vidPidRegex = new Regex(@"VID_([0-9A-F]+)&PID_([0-9A-F]+)");
                            var matchVidPid = vidPidRegex.Match(deviceId);

                            var isMatched = matchVidPid.Success;
                            var PosVID = matchVidPid.Groups[1].Value;
                            var PosPID = matchVidPid.Groups[2].Value;

                            // 檢查VID和PID是否與配置文件中設定的相符
                            if (isMatched && PosVID == Config.PosVID && PosPID == Config.PosPID)
                            {
                                // 組合顯示文字並更新界面上的顯示
                                string displayText = $"COM{comPortNumber} (VID={Config.PosVID}, PID={Config.PosPID}) detected and saved.";
                                DeviceString += displayText + "\n";
                            }
                        }
                    }
                }
                RaisePropertyChanged(nameof(DeviceString));

            }
            catch (ManagementException ex)
            {
                // 處理可能的異常，並在異常發生時彈出提示
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }

        private DelegateCommand _unloadedCommand;
        public DelegateCommand UnloadedCommand => _unloadedCommand ??= new DelegateCommand(ExcuteUnloadedCommand);
        private void ExcuteUnloadedCommand()
        {
        }
    }
}
