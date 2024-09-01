﻿using System.Threading.Tasks;
using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure.Service
{
    public class DimensionCameraService(IWMIService wmiService) : IDimensionCameraService
    {
        public string ID { get; } = Config.DimensionCameraID;

        public bool IsConnected() => GetDeviceInformation() != null;

        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.SerialPortQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }
        public async Task<string> GetSize()
        {   
            await Task.Delay(5000);
            return string.Empty;
        }

        public async Task<int?> GetWieght()
        {
            await Task.Delay(1000);
            return 18;
        }
    }
}