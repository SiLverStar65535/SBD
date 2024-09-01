namespace SBD.Domain.Interface
{
    public interface IDevice
    {
        string ID { get; }
        object GetDeviceInformation();
        bool IsConnected();
    }
}