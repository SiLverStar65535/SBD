using SBD.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Service
{
    public class WMIService : IWMIService
    {
        public IList<WMIQuery> GetAllWMIQueries()
        {
            // 獲取當前 Assembly 中繼承自 WMIQuery 的所有類別
            var queryTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(WMIQuery)) && !t.IsAbstract); // 過濾抽象類別

            var queries = new List<WMIQuery>();

            foreach (var type in queryTypes)
            {
                // 使用 Activator 創建這些類型的實例
                var queryInstance = (WMIQuery)Activator.CreateInstance(type);
                queries.Add(queryInstance);
            }

            return queries;
        }
        public DataTable QueryWMI(string query)
        {
            var searcher = new ManagementObjectSearcher(query);
            var managementObjects = searcher.Get();
            return Search(managementObjects);
        }
        public async Task<DataTable> QueryWMIAsync(string query) => await Task.Run(() => QueryWMI(query));
        public IDictionary<string, object> QueryDevices(Type queryType)
        {
            var queryInstance = Activator.CreateInstance(queryType) as WMIQuery;
            var searcher = new ManagementObjectSearcher(queryInstance.QueryString);
            var managementObjects = searcher.Get();
            return Search(managementObjects, queryInstance.PrimaryKey);
        }
        public async Task<IDictionary<string, object>> QueryDevicesAsync(Type queryType) => await Task.Run(() => QueryDevices(queryType));
        public KeyValuePair<string, object> QueryDevice(Type queryType, string deviceID) => QueryDevices(queryType).SingleOrDefault(x => x.Key == deviceID);
        public async Task<KeyValuePair<string, object>> QueryDeviceAsync(Type queryType, string deviceID) => await Task.Run(() => QueryDevice(queryType, deviceID));

        private DataTable Search(ManagementObjectCollection managementObjects)
        {
            var result = new DataTable();
            if (managementObjects == null || managementObjects.Count == 0)
                return result;
            foreach (var managementObject in managementObjects)
            {
                if (result.Columns.Count == 0)
                {
                    foreach (var property in managementObject.Properties)
                    {
                        result.Columns.Add(property.Name, property.Value?.GetType() ?? typeof(string));
                    }
                }
                var row = result.NewRow();
                foreach (var property in managementObject.Properties)
                {
                    row[property.Name] = property.Value ?? DBNull.Value;
                }
                result.Rows.Add(row);
            }
            return result;
        }
        private IDictionary<string, object> Search(ManagementObjectCollection managementObjects, string queryID)
        {
            var results = new Dictionary<string, object>();

            if (managementObjects == null || managementObjects.Count == 0)
                return results;

            foreach (var managementObject in managementObjects)
            {
                var key = managementObject[queryID].ToString();
                var propertyDictionary = new Dictionary<string, object>();
                foreach (var property in managementObject.Properties)
                {
                    propertyDictionary[property.Name] = property.Value ?? DBNull.Value;
                }
                results[key] = propertyDictionary;
            }

            return results;
        }
    }
}


