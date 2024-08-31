using SBD.Infrastructure.Internel.Interface;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Internel.Service
{
    public enum DeviceType
    {
        Processor,
        PhysicalMemory,
        DiskDrive,
        NetworkAdapter,
        VideoController,
        DesktopMonitor,
        SoundDevice,
        Battery,
        PowerSupply,
        PointingDevice,
        Keyboard,
        Printer,
        Fan,
        TemperatureProbe,
        USBHub,
        SerialPort,
        ParallelPort,
        PnPEntity,
        OperatingSystem,
        ComputerSystem,
        BIOS
    }
    public class WMIQuery(DeviceType deviceType, string queryString, string description)
    {
        public DeviceType DeviceType { get; set; } = deviceType;
        public string QueryString { get; set; } = queryString;
        public string Description { get; set; } = description;
    }
    public class DiskDriveQuery() : WMIQuery(DeviceType.DiskDrive, "SELECT * FROM Win32_DiskDrive", "硬碟資訊，如型號、介面類型、大小等。");
    public class LogicalDiskQuery() : WMIQuery(DeviceType.DiskDrive, "SELECT * FROM Win32_LogicalDisk", "邏輯磁碟資訊，如驅動器號、文件系統、空間使用情況等。");
    public class CDROMDriveQuery() : WMIQuery(DeviceType.DiskDrive, "SELECT * FROM Win32_CDROMDrive", "光碟機資訊。");
    public class ProcessorQuery() : WMIQuery(DeviceType.Processor, "SELECT * FROM Win32_Processor", "處理器 (CPU) 資訊，如名稱、製造商、時脈速度等。");
    public class PhysicalMemoryQuery() : WMIQuery(DeviceType.PhysicalMemory, "SELECT * FROM Win32_PhysicalMemory", "物理記憶體 (RAM) 資訊，如容量、速度、製造商等。");
    public class NetworkAdapterQuery() : WMIQuery(DeviceType.NetworkAdapter, "SELECT * FROM Win32_NetworkAdapter", "網路介面卡資訊，如名稱、MAC 地址、狀態等。");
    public class NetworkAdapterConfigurationQuery() : WMIQuery(DeviceType.NetworkAdapter, "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE", "網路介面卡配置資訊，如 IP 地址、子網路遮罩、DNS 等。");
    public class NetworkConnectionQuery() : WMIQuery(DeviceType.NetworkAdapter, "SELECT * FROM Win32_NetworkConnection", "網路連接資訊。");
    public class VideoControllerQuery() : WMIQuery(DeviceType.VideoController, "SELECT * FROM Win32_VideoController", "顯示卡 (GPU) 資訊，如名稱、顯示記憶體、解析度支援等。");
    public class DesktopMonitorQuery() : WMIQuery(DeviceType.DesktopMonitor, "SELECT * FROM Win32_DesktopMonitor", "顯示器資訊，如型號、最大解析度等。");
    public class SoundDeviceQuery() : WMIQuery(DeviceType.SoundDevice, "SELECT * FROM Win32_SoundDevice", "音訊設備資訊，如聲卡名稱、製造商等。");
    public class BatteryQuery() : WMIQuery(DeviceType.Battery, "SELECT * FROM Win32_Battery", "電池資訊，如電池狀態、充電狀況等。");
    public class PowerSupplyQuery() : WMIQuery(DeviceType.PowerSupply, "SELECT * FROM Win32_PowerSupply", "電源資訊。");
    public class PointingDeviceQuery() : WMIQuery(DeviceType.PointingDevice, "SELECT * FROM Win32_PointingDevice", "滑鼠或其他指向設備的資訊。");
    public class KeyboardQuery() : WMIQuery(DeviceType.Keyboard, "SELECT * FROM Win32_Keyboard", "鍵盤資訊。");
    public class ComputerSystemQuery() : WMIQuery(DeviceType.ComputerSystem, "SELECT * FROM Win32_ComputerSystem", "整體電腦系統資訊，如製造商、型號、總記憶體等。");
    public class OperatingSystemQuery() : WMIQuery(DeviceType.OperatingSystem, "SELECT * FROM Win32_OperatingSystem", "操作系統資訊，如名稱、版本、安裝路徑等。");
    public class BIOSQuery() : WMIQuery(DeviceType.BIOS, "SELECT * FROM Win32_BIOS", "BIOS 資訊，如版本、製造商、發行日期等。");
    public class PrinterQuery() : WMIQuery(DeviceType.Printer, "SELECT * FROM Win32_Printer", "印表機資訊，如名稱、狀態、預設印表機等。");
    public class PrinterConfigurationQuery() : WMIQuery(DeviceType.Printer, "SELECT * FROM Win32_PrinterConfiguration", "印表機配置。");
    public class FanQuery() : WMIQuery(DeviceType.Fan, "SELECT * FROM Win32_Fan", "系統風扇資訊。");
    public class TemperatureProbeQuery() : WMIQuery(DeviceType.TemperatureProbe, "SELECT * FROM Win32_TemperatureProbe", "溫度探針資訊。");
    public class USBHubQuery() : WMIQuery(DeviceType.USBHub, "SELECT * FROM Win32_USBHub", "USB端口。");
    public class ParallelPortQuery() : WMIQuery(DeviceType.ParallelPort, "SELECT * FROM Win32_ParallelPort", "並口。");
    public class SerialPortQuery() : WMIQuery(DeviceType.SerialPort, "SELECT * FROM Win32_SerialPort", "串口。");
    public class PnPEntityQuery() : WMIQuery(DeviceType.PnPEntity, "SELECT * FROM Win32_PnPEntity", "熱插拔口");
    public class COMPortQuery() : WMIQuery(DeviceType.SerialPort, "SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'", "COM端口。");

    public class WMIService : IWMIService
    {

        public IDictionary<string, object> QueryWMI(string query)
        {
            var searcher = new ManagementObjectSearcher(query);
            var managementObjects = searcher.Get();
            return Search(managementObjects);
        }

        public async Task<IDictionary<string, object>> QueryWMIAsync(string query)
        {
            return await Task.Run(() => QueryWMI(query));
        }
     
        public IDictionary<string, object> QueryDevices<T>() where T : WMIQuery, new()
        {
            var query = new T().QueryString;
            var searcher = new ManagementObjectSearcher(query);
            var managementObjects = searcher.Get();
            return Search(managementObjects);
        }

        public async Task<IDictionary<string, object>> QueryDevicesAsync<T>( ) where T : WMIQuery, new()
        {
            return await Task.Run(QueryDevices<T>);
        }

        public object QueryDevice<T>(string deviceID)
        {
            throw new NotImplementedException();
        }

        public async Task<object> QueryDeviceAsync<T>(string deviceID)
        {
            throw new NotImplementedException();
        }


        private IDictionary<string, object> Search(ManagementObjectCollection managementObjects)
        {
            var results = new Dictionary<string, object>();

            if (managementObjects == null || managementObjects.Count == 0)
                return results;

            foreach (var managementObject in managementObjects)
            {
                var deviceId = managementObject["DeviceID"]?.ToString();
                if (deviceId == null)
                    continue;

                var propertyDictionary = new Dictionary<string, object>();
                foreach (var property in managementObject.Properties)
                {
                    if (property.Name != "DeviceID") // Exclude DeviceID from properties dictionary
                    {
                        propertyDictionary[property.Name] = property.Value ?? DBNull.Value;
                    }
                }

                results[deviceId] = propertyDictionary;
            }

            return results;
        }




        
    }
}


