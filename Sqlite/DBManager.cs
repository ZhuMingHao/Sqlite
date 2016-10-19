using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Sqlite.Models;
using System.Reflection;
using Windows.UI.Popups;

namespace Sqlite
{
    public class DBManager : SingletonProvider<DBManager>
    {
        
        /// <summary>
        /// 初始化并创建一张表
        /// </summary>
        ///  
        ///泛型方法
        public void Init<T>()
        {
           // DbFilePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DbFileName);
            using (var db = GetDbConnection())
            {
                db.CreateTable<T>();
            }
        }
        /// <summary>
        /// 根据路径创建db文件
        /// 如果创建就读取dbquery
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public SQLiteConnection GetDbConnection()
        {
            var con = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbFilePath);
            return con;
        }
        /// <summary>
        /// /获取表的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<TableQuery<T>>  GetTableValue<T>() where T : class
        {
           // StringBuilder sb = new StringBuilder();
            using (var db = GetDbConnection())
            {
                return await Task.Run(()=> db.Table<T>()); ;
                
                //foreach (var item in list)
                //{
                //    PropertyInfo id = item.GetType().GetProperty("Id");
                //    PropertyInfo name = item.GetType().GetProperty("UserName");
                //    PropertyInfo age = item.GetType().GetProperty("Age");
                //    PropertyInfo address = item.GetType().GetProperty("Address");
                //    sb.AppendLine($"{id.GetValue(item, null)} {age.GetValue(item, null)} {name.GetValue(item, null)} {address.GetValue(item, null)}");
                //}
                //await new MessageDialog(sb.ToString()).ShowAsync();
            }
        }
        //插入数据库
        public int InsertSqlite<T>(T item)
        {
            int result = 0;
            using (var db = GetDbConnection())
            {
                result = db.Insert(item);
            }
            return result;
        }
        /// <summary>
        /// 删除一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DeleteItem<T>(T item)
        {
            PropertyInfo propertyInfo = item.GetType().GetProperty("userId");
            int ID = (int)propertyInfo.GetValue(item, null);
            using (var db = GetDbConnection())
            {
                db.Delete<T>(ID);
            }
        }

    }
}
