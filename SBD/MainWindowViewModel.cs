using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;

namespace SBD
{
    public static class ApplicationCommands
    {
        public static CompositeCommand NavigateCommand = new CompositeCommand();
       
    }
    public class MainWindowViewModel
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

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ??= new DelegateCommand<string>(ExecuteNavigateCommand);
        private void ExecuteNavigateCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(ExecuteLoadedCommand);
        private void ExecuteLoadedCommand()
        {
            //强制使用英文输入法
            if (InputMethod.Current != null)
            {
                InputMethod.Current.ImeState = InputMethodState.Off;
            }
            // 檢查comport與VID、PID，並檢查是否與預定的VID和PID匹配。
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en-EU");
            if (Application.Current.MainWindow != null) 
            {
                InputMethod.SetIsInputMethodEnabled(Application.Current.MainWindow, false);
            }
 
            // 清除之前掃描的COM端口列表和顯示結果
            //comPorts.Clear();
            //ComText.Text = "";

            // 使用WMI來搜索系統中的所有設備，篩選出含有COM端口描述的設備
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");
            // 正則表達式，用於從設備描述中提取COM端口編號
            var comRegex = new Regex(@"\(COM(\d+)\)");

            try
            {
                // 遍歷搜索結果
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // 獲取設備描述信息
                    string caption = queryObj["Caption"].ToString();
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
                            string deviceId = queryObj["DeviceID"].ToString();
                            // 正則表達式，用於從設備ID中提取VID和PID
                            var vidPidRegex = new Regex(@"VID_([0-9A-F]+)&PID_([0-9A-F]+)");
                            var matchVidPid = vidPidRegex.Match(deviceId);
                            // 檢查VID和PID是否與配置文件中設定的相符
                            if (matchVidPid.Success && matchVidPid.Groups[1].Value == Config.PosVID && matchVidPid.Groups[2].Value == Config.PosPID)
                            {
                                // 如果匹配成功，設定全域變數為提取的COM端口號碼
                                //PosComPort_scanRes = comPortNumber;
                                // 組合顯示文字並更新界面上的顯示
                                string displayText = $"COM{comPortNumber} (VID={Config.PosVID}, PID={Config.PosPID}) detected and saved.";
                                //ComText.Text += displayText + "\n";
                            }
                        }
                    }
                }
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


    
        private DelegateCommand<TextCompositionEventArgs> _previewTextInputCommand;
        public DelegateCommand<TextCompositionEventArgs> PreviewTextInputCommand => _previewTextInputCommand ??= new DelegateCommand<TextCompositionEventArgs>(ExcutePreviewTextInputCommand);
        private void ExcutePreviewTextInputCommand(TextCompositionEventArgs args)
        {
        
        }


        private DelegateCommand<KeyEventArgs> _keyDownCommand;
        public DelegateCommand<KeyEventArgs> KeyDownCommand => _keyDownCommand ??= new DelegateCommand<KeyEventArgs>(ExcuteKeyDownCommand);
        private void ExcuteKeyDownCommand(KeyEventArgs args)
        {


        }

    }
}
