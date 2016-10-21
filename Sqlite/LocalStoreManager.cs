using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sqlite
{
    public class LocalStoreManager : SingletonProvider<LocalStoreManager>
    {
        private static string Manager = "Manager";
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private ApplicationDataContainer container = ApplicationData.Current.LocalSettings.CreateContainer(Manager, ApplicationDataCreateDisposition.Always);
        private static bool Exist = ApplicationData.Current.LocalSettings.Containers.ContainsKey(Manager);
        public async Task<bool> SetObjectForKey(string key, object obj)
        {
            bool result = false;
            if (Exist)
            {
                await Task.Run(() =>
                {
                    localSettings.Containers[Manager].Values[key] = obj;
                });
            }
            else
            {
                container = ApplicationData.Current.LocalSettings.CreateContainer(Manager, ApplicationDataCreateDisposition.Always);
                Exist = true;
                await Task.Run(() =>
                {
                    localSettings.Containers[Manager].Values[key] = obj;
                });
            }
            result = await Task.Run(() => localSettings.Containers[Manager].Values.ContainsKey(key));
            return result;
        }

        public async Task<object> GetObjectForKey(string key)
        {
            object obj = new object();
            if (Exist)
            {
                obj = await Task.Run(() => container.Values[key]);
            }
            else
            {
                return null;
            }
            return obj;
        }
        public async Task DeletContainer()
        {
            if (Exist)
            {
                await Task.Run(() => localSettings.DeleteContainer(Manager));
                Exist = false;
            }
        }
    }
}
