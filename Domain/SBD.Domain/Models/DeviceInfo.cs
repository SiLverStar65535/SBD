namespace SBD.Domain.Models
{
    public class DeviceInfo
    {
        public eDevice Device { get; set; }
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string PosVID { get; set; }
        public string PosPID { get; set; }
    };
}
