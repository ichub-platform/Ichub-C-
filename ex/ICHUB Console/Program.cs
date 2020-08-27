using ICHUB_LIBRARY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICHUB_LIBRARY.Models;

namespace ICHUB_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Tao đối tượng kết nối truyền vào accesskey của project trên app ICHUB
                ConnectICHUB connectICHUB = new ConnectICHUB("EWXD111");
                //bắt sự kiện khi có thay đổi
                connectICHUB.DataChange += ChangeData;
                connectICHUB.ErrorArgs += ErrorErgs;
                Console.ReadKey();
            }
            catch (Exception s)
            {
                Console.WriteLine(s.Message);
            }
        }
        // Khi dữ liệu thay đổi sự kiện DataChangeArgs sẽ được gọi
        // Chú ý sự kiện chạy ở 1 luồn riêng
        static void ChangeData(object sender, DataChangeArgs e)
        {
            try
            {
                //Phân tích dữ liệu nhận dc
                foreach(var item in e.Data)
                {
                    Console.WriteLine("Tên:" + item.Name);
                    Console.WriteLine("Data:"+item.Data );                
                }
                Console.WriteLine("--------------------");
            }
            catch
            {

            }


        }
        // Khi phát sinh lỗi sự kiện ErrorArgs sẽ được gọi
        static void ErrorErgs(object senser, ErrorArgs e)
        {
            Console.WriteLine(JsonConvert.SerializeObject(e.Error.Detail));
        }

    }
}
