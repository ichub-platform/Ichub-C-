
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ICHUB_LIBRARY
{
   public class Models
    {
        public class Ichub_Api_response
        {
            public int Status { get; set; }
            public string Data { get; set; }


        }
        public class ModelSend
        {
            public ObservableCollection<ModelSendDevice> Data { get; set; }
            public Server Server { get; set; }
        }

        public class LoginModel
        {
            public List<GetDevice_Result> lstDevices { get; set; }
            public List<GetLocation_Result> lstLocations { get; set; }
            public User UserInfo { get; set; }
        }
        public partial class User
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Pass { get; set; }
            public string Email { get; set; }
            public string SDT { get; set; }
            public string UserImage { get; set; }
            public Nullable<bool> Status { get; set; }
            public Nullable<System.DateTime> NgayTao { get; set; }
            public string FullName { get; set; }
            public string IDFaceBook { get; set; }
            public string NotiID { get; set; }
            public string AccessKey { get; set; }
            public Nullable<int> UserType { get; set; }
        }
        public class DeviceInfo
        {
            public string Mac { get; set; }
            public string AccessKey { get; set; }
        }
        public partial class GetLocation_Result
        {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public Nullable<int> UserID { get; set; }
            public Nullable<System.DateTime> DateTime { get; set; }
            public Nullable<bool> Status { get; set; }
        }
        public partial class GetDevice_Result
        {
            public string DeviceID { get; set; }
            public string DeviceName { get; set; }
            public Nullable<int> MaLoai { get; set; }
            public Nullable<System.DateTime> NgayTao { get; set; }
            public Nullable<bool> TrangThai { get; set; }
            public Nullable<int> LocationID { get; set; }
            public Nullable<int> ServerID { get; set; }
            public string imageloai { get; set; }
            public string tenloai { get; set; }
            public string LocationName { get; set; }
            public string PhianBanMoi { get; set; }
            public string LinkUpdate { get; set; }
        }
        public class DataBodetail
        {
            public string IconOn { get; set; }
            public string IconOff { get; set; }
            public string VT { get; set; }
            public string Unit { get; set; }
            public int Timereding { get; set; }
            public SettingBoaddetail Setting { get; set; }
            public bool OnOff { get; set; }

        }
        public class Timers
        {
            public string Time { get; set; }
            public int Status { get; set; }
            public int Repeat { get; set; }
            public int OnOff { get; set; }
            public Timers(string time,int status,int repeat, int onoff)
            {
                Time = time;
                Status = status;
                Repeat = repeat;
                OnOff = onoff;
            }

        }
        public class Control
        { 
                public int Status { get; set; }
                public int ControlNode { get; set; }
                public int StatusNodeControl { get; set; }
                public int OnOff { get; set; }
            public Control(int status,int controlNode, int statusNodeControl,int onoff) 
            {
                Status = status;
                ControlNode = controlNode;
                StatusNodeControl = statusNodeControl;
                OnOff = onoff;
            }
        }

        public partial class SettingBoaddetail
        {
            public long TimerID { get; set; }
            public Nullable<int> BoardDetailID { get; set; }
            public string Data { get; set; }
            public Nullable<bool> Status { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
        }
        public class ModelSendDevice
        {
            public int ID { get; set; }
            public int Type { get; set; }
            public string VT { get; set; }
            public string Unit { get; set; }
            public int Timereding { get; set; }
            public  string Setting { get; set; }
            //public List<Data> DataSetting { get; set; }
            public DataSettingDetail DataSettingDetail { get; set; }


            public string Data { get; set; }
            public DataBodetail DataShow { get; set; }
            public string Name { get; set; }

        }
        public class DataSettingDetail
        {
            public List<Timers> Timers { get; set; }
            public List<Control> Controls { get; set; }
            public List<ControlSensor> ControlSensors { get; set; }
        }
        public class ControlSensor
        {
            public int OnOff { get; set; }
            public int SetValue { get; set; }
            private string Compare ;
            public int IDNodeControl { get; set; }
            public int ValueNodeControl { get; set; }
            public void  SetCompare( string compare)
            {
                if(compare=="<" || compare==">" || compare == "=")
                {
                    Compare = compare;
                }
                else
                {
                    Compare = "=";
                }    
            }
            public string getCompare()
            {
                return Compare;
            }
            public ControlSensor  (int onoff,int setvalue, string compare, int idnodecontrol,int   valuenodevontrol)
            {
                OnOff = onoff;
                SetValue = setvalue;
               SetCompare(compare);
                IDNodeControl = idnodecontrol;
                ValueNodeControl = valuenodevontrol;
            }
        }
        public class Setting
        {
            public int id { get; set; }
            public List<Data> data { get; set; }
          //  public DataUser  {get;set;}
        }
        public class Data
        {
            public string t { get; set; }
            public string d { get; set; }
        }
        public  class Server
        {
            public string ServerName { get; set; }
            public string IP { get; set; }
            public Nullable<int> Type { get; set; }
            public Nullable<bool> Status { get; set; }
            public Nullable<System.DateTime> DateCreacte { get; set; }
            public int ServerID { get; set; }
            public string UserName { get; set; }
            public string PassWord { get; set; }
        }
        public class DataAPP
        {
            public string action { get; set; }
            public DataAppDetail dulieu { get; set; }
        }
        public  class DataAppDetail
        {
            public int id { get; set; }
            public int s { get; set; }
            public int data { get; set; }
        }
        public class Error
        {
            public int Code { get; set; }
            public string Detail { get; set; }
            public Error(int code, string detail)
            {
                Code = code;
                Detail = detail;
            }
        }
        public class DataChangeArgs : EventArgs
        {
            public ObservableCollection<ModelSendDevice> Data { get; set; }
            public DataChangeArgs(ObservableCollection<ModelSendDevice> data)
            {
                Data = data;
            }
        }
        public class ErrorArgs : EventArgs
        {
            public Error Error { get; set; }
            public ErrorArgs(Error errors)
            {
                Error = errors;
            }
        }
        public partial class Project
        {
            public int BoardID { get; set; }
            public string BoardName { get; set; }
            public Nullable<int> BoardType { get; set; }
            public Nullable<int> UserID { get; set; }
            public Nullable<System.DateTime> DateCreate { get; set; }
            public Nullable<bool> Status { get; set; }
            public string Mac { get; set; }
            public string KeyConnect { get; set; }
            public Nullable<int> ServerID { get; set; }
        }
        public class ICHUB
        {
            static public string Url = "http://ichub-api-csharp.doe.vn/";
           
            public static string GetMACAddress()
            {
                try
                {              
                        string url = "http://checkip.dyndns.org";
                        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        string response = sr.ReadToEnd().Trim();
                        string[] a = response.Split(':');
                        string a2 = a[1].Substring(1);
                        string[] a3 = a2.Split('<');
                        string a4 = a3[0];
                        return a4;
                }
                catch
                {
                    return null;
                }
            }
        }

    }
}
