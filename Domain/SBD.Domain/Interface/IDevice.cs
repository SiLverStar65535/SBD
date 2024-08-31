namespace SBD.Domain.Interface
{
    public interface IDevice
    {
        string DeviceID { get; }
        object GetDeviceInformation();
        bool IsConnected();
    }
}