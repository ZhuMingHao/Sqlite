using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Sqlite.Models;
using System.Diagnostics;
using Windows.UI.Popups;
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DBManager manager = DBManager.Instance;
        private LocalStoreManager StoreManager = LocalStoreManager.Instance;
        public MainPage()
        {
            this.InitializeComponent();

        }

        private async void readSQlite_Click(object sender, RoutedEventArgs e)
        {
            //string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, App.DbFileName);
            //
            //using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            //{
            //    TableQuery<Person>list =db.Table<Person>();            
            //    foreach (var item in list)
            //    {
            //        sb.AppendLine($"{item.Id} {item.UserName} {item.Age} {item.Address}");
            //    }
            //    await new MessageDialog(sb.ToString()).ShowAsync();
            //}
             StringBuilder sb = new StringBuilder();
            var list =  manager.GetTableValue<Person>();
           
            foreach (Person item in list)
            {
                sb.AppendLine($"{item.Id} {item.UserName} {item.Age} {item.Address}");
            }
            await new MessageDialog(sb.ToString()).ShowAsync();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            manager.Init<Person>();
            manager.InsertSqlite<Person>(new Person { Address = "北京", Age = 20, SomeProperty = "哈哈", UserName = "王小明" });

        }

        private async void Lunch_Click(object sender, RoutedEventArgs e)
        {
            //var uriBing = new Uri("lifecycle:");
            ////var promptOptions = new Windows.System.LauncherOptions();
            ////promptOptions.TreatAsUntrusted = true;
            //var options = new Windows.System.LauncherOptions();
            //var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

          await StoreManager.SetObjectForKey("hello","zhuminghao");
        }

        private async void GetValue_Click(object sender, RoutedEventArgs e)
        {
           var str =  await StoreManager.GetObjectForKey("hello");
            Debug.WriteLine(str);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await StoreManager.DeletContainer();
        }
    }
}
