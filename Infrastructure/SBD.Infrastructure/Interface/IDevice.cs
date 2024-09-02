namespace SBD.Infrastructure.Interface
{ 
    /// <summary>
    /// 設備的基礎介面
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// 設備ID(ID取決於WMIQuery的屬性primaryKey)
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 取得當前設備的資訊
        /// </summary>
        object GetDeviceInformation();

        /// <summary>
        /// 取得當前設備的連接狀態
        /// </summary>
        bool IsConnected();
    }
}