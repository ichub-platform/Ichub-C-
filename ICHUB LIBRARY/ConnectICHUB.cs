using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static ICHUB_LIBRARY.Models;

namespace ICHUB_LIBRARY
{
    
    public class ConnectICHUB
    {
        private string Accesskey { get; set; }
        public Ichub_Api_response API_DATA { get; set; }
        //public  string Data { get; set; }
        public int Code { get; set; }
        public string Messenger { get; set; }
        public bool ConnectAPI { get; set; }
        public bool ConnectMQTT { get; set; }
        private string ClienID = Guid.NewGuid().ToString() + "C#";
        private ObservableCollection<ModelSendDevice> ICHUB_DATA;
        protected static MqttClient mqtt { get; set; }
        public ConnectICHUB(string key)
        {
            ConnectAPI = false;
            ConnectMQTT = false;
            Accesskey = key;
            Connect();
        }
        protected void Connect()
        {
            API();
        }
        private void Subscribemqtt(string topic)
        {
            mqtt.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
        private void Publishmqtt(string topic, string text)
        {
            mqtt.Publish(topic, Encoding.UTF8.GetBytes(text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
      
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                if (e.Message != null && e.Topic == Accesskey + "/device")
                {
                    string mqttdata = System.Text.Encoding.UTF8.GetString(e.Message);
                    ICHUB_DATA = JsonConvert.DeserializeObject<ObservableCollection<ModelSendDevice>>(mqttdata);
                    ModelSend dta = JsonConvert.DeserializeObject<ModelSend>(API_DATA.Data);
                    if (ICHUB_DATA != null)
                        foreach (var item in ICHUB_DATA)
                        {
                            foreach (var i in dta.Data)
                            {
                                if (item.ID * 378 == i.ID)
                                {
                                    item.VT = i.VT;
                                    item.DataShow = JsonConvert.DeserializeObject<DataBodetail>(i.Data);
                                    item.Unit = i.Unit;
                                    item.Name = i.Name;
                                    item.ID = i.ID;
                                    item.Setting = i.Setting;
                                    //item.DataSetting = JsonConvert.DeserializeObject<List<Data>>(i.Setting);
                                    var lstSetting = JsonConvert.DeserializeObject<List<Data>>(i.Setting).ToList();
                                    item.Setting = null;
                                    List<Timers> lsttimers = new List<Timers>();
                                    List<Control> lstcontrols = new List<Control>();
                                    List<ControlSensor> lstcontrolSensors = new List<ControlSensor>();
                                    foreach (var ite in lstSetting)
                                    {
                                        if(ite.t == "r") //on/off điều khiển "0|173|0|1"y
                                        {
                                            string[] authorsList = ite.d.Split('|');
                                            int status = int.Parse(authorsList[0]);
                                            int node = int.Parse(authorsList[1])*378;
                                            int Statusnode = int.Parse(authorsList[2]);
                                            int onoff = int.Parse(authorsList[3]);
                                            lstcontrols.Add(new Control(status, node, Statusnode, onoff));
                                        }
                                        if(ite.t=="d"|| ite.t == "t") // hẹn giờ
                                        {
                                            string[] authorsList = ite.d.Split('|');
                                            string time = authorsList[0];
                                            int timeint = int.Parse(time);
                                            int data = int.Parse(authorsList[1]);
                                            int laplai = int.Parse(authorsList[2]);
                                            int onoff = int.Parse(authorsList[3]);                                          
                                            int gio = timeint / 3600;
                                            int phut = (timeint % 3600)/60;
                                            string times = gio + ":" + phut;
                                            lsttimers.Add(new Timers(times, data, laplai, onoff));
                                        }
                                        if (ite.t == "c")// sensor điều khiển "0|>|170|1|1"
                                        {
                                            string[] authorsList = ite.d.Split('|');
                                            int values =int.Parse(authorsList[0]);
                                            int onoff = int.Parse(authorsList[4]);
                                            string compare = authorsList[1];
                                            int idnodecontrol = int.Parse(authorsList[2]) *378;
                                            int statusnodecontrol = int.Parse(authorsList[3]);
                                            lstcontrolSensors.Add(new ControlSensor(onoff, values, compare, idnodecontrol, statusnodecontrol));
                                        }
                                    }
                                    DataSettingDetail dataSettingDetail = new DataSettingDetail();
                                    dataSettingDetail.Timers = lsttimers;
                                    dataSettingDetail.Controls = lstcontrols;
                                    dataSettingDetail.ControlSensors = lstcontrolSensors;
                                    item.DataSettingDetail = dataSettingDetail;


                                }
                            }
                        }
                    else return;
                    OndataChange(ICHUB_DATA);
                }
                if (e.Message != null && e.Topic == Accesskey + "/updateapp")
                {
                    string settingdata = System.Text.Encoding.UTF8.GetString(e.Message);
                }    
            }
            catch(Exception err)
            {
                ErrorFonction(new Error(1, err.Message));
            }
        }
        private async void API()
        {
            string mac = ICHUB.GetMACAddress();
            if (mac == null) { ErrorFonction(new Error(2, "Can not connect server")); return; }
            await Task.Run(() => PostAsync(ICHUB.Url+ "GetInfoServerApp", "={\"Mac\":\""+ mac + "\",\"AccessKey\":\"" + Accesskey + "\"}"));
        }
        private async void PostAsync(string uri, string data)
        {
            ModelSend model = new ModelSend();
            try
            {
                //Ichub_Api_response api = new Ichub_Api_response();
                var httpClient = new HttpClient();
                string formData = data;
                try
                {
                    var result = httpClient.PostAsync(uri,
                    new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                    string content = await result.Content.ReadAsStringAsync();
                    API_DATA = JsonConvert.DeserializeObject<Ichub_Api_response>(content);
                }
                catch
                {
                    ErrorFonction(new Error(2, "Can not connect server"));
                }
                
                if (API_DATA.Status != 0) 
                { 
                    Code = 97;
                    Messenger = API_DATA.Data;
                    ConnectAPI = true;
                    ErrorFonction(new Error(1, API_DATA.Data));
                    return;
                }
                model = JsonConvert.DeserializeObject<ModelSend>(API_DATA.Data);
                mqtt = new MqttClient(model.Server.ServerName);
                Code = mqtt.Connect(ClienID, model.Server.UserName, model.Server.PassWord);
                if (Code == 0)
                {
                    ConnectMQTT = true;
                    mqtt.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                    var cancelToken = new CancellationToken();
                    await TryReconnectAsync(cancelToken);
                   // mqtt.ConnectionClosed += client_ConnectionClosed;
                    Subscribemqtt(Accesskey + "/device");
                    Subscribemqtt(Accesskey + "/updateapp");
                    
                    Subscribemqtt("updatedevice/" + Accesskey);
                    Publishmqtt(Accesskey + "/app", "{\"action\":\"a\",\"dulieu\":{}}");
                    ICHUB_DATA = model.Data;
                }
                else
                {
                    ConnectMQTT = false;
                    Code = 1;
                    Messenger = "Can not connect mqtt";
                    ErrorFonction(new Error(1, "Can not connect mqtt "));
                }
            }
            catch(Exception e)
            {
                ErrorFonction(new Error(2, e.Message ));
            }
        }
        private async Task TryReconnectAsync(CancellationToken cancellationToken)
        {
            var connected = mqtt.IsConnected;
            while (!connected && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    mqtt.Connect(ClienID);
                }
                catch
                {
                   
                }
                connected = mqtt.IsConnected;
                await Task.Delay(5000, cancellationToken);
            }
        }
        public void Senddata(int id, int data)
        {
            
            DataAPP dataAPP = new DataAPP();
            dataAPP.dulieu = new DataAppDetail();
            int type = CheckType(id);
            if (type != -1)
            {
                bool c = checkdata(data, type);
                if (c == false) return;
            }    
               
            if (type == -1)
            {
                Messenger = "ID không đúng";
                ErrorFonction(new Error(5, Messenger));
                return;
            }
            id = id / 378;
            switch (type)
            {
                case 1:
                    {
                        dataAPP.action = "o";
                        dataAPP.dulieu.data = data;
                        dataAPP.dulieu.id = id;
                        dataAPP.dulieu.s = data;
                        Publishmqtt(Accesskey + "/app", JsonConvert.SerializeObject(dataAPP));
                        break;
                    }
                case 2:
                    {
                        dataAPP.action = "s";
                        dataAPP.dulieu.data = data;
                        dataAPP.dulieu.id = id;
                        dataAPP.dulieu.s = data;
                        Publishmqtt(Accesskey + "/app", JsonConvert.SerializeObject(dataAPP));
                        break;
                    }
                case 3:
                    {
                        dataAPP.action = "d";
                        dataAPP.dulieu.data = data;
                        dataAPP.dulieu.id = id;
                        dataAPP.dulieu.s = data;
                        Publishmqtt(Accesskey + "/app", JsonConvert.SerializeObject(dataAPP));
                        break;
                    }
                case 4:
                    {
                        dataAPP.action = "t";
                        dataAPP.dulieu.data = data;
                        dataAPP.dulieu.id = id;
                        dataAPP.dulieu.s = data;
                        Publishmqtt(Accesskey + "/app", JsonConvert.SerializeObject(dataAPP));
                        break;
                   }
            }
        }
        public void SendSeting(int idnode, DataSettingDetail dataSetting)
        {
            try
            {
                int  check = CheckType(idnode);
                if(check==-1) 
                {
                    Messenger = "ID không đúng";
                    ErrorFonction(new Error(5, Messenger));
                    return; 
                }
                bool datacheck = checktypedata(check, dataSetting);
                Setting setting = new Setting();
                setting.id = idnode / 378;
                List<Data> lstData = new List<Data>();
                if(dataSetting.Timers!=null)
                foreach (var timer in dataSetting.Timers)
                {
                    //"{"id":214,"data":[{"d":"0 | 215 | 1 | 1","t":"r"},{"d":"45000 | 1 | 1 | 1","t":"t"},{"d":"1 | 217 | 29 | 1","t":"r"}]}"
                    Data data = new Data();
                    data.t = "t";
                    string[] timestring = timer.Time.Split(':');
                    int timeint = int.Parse(timestring[0]) + int.Parse(timestring[1]);
                    string d = timeint + "|" + timer.Status + "|" + timer.Repeat + "|" + timer.OnOff;
                    data.d = d;
                    lstData.Add(data);
                }
                if (dataSetting.Controls != null)
                    foreach (var control in dataSetting.Controls)
                    {
                        int checks = CheckType(control.ControlNode);
                        if (checks == -1)
                        {
                            Messenger = "ID nút bị điều khiển không đúng";
                            ErrorFonction(new Error(5, Messenger));
                            return;
                        }
                        Data data = new Data();
                    data.t = "r";
                    string dcontro = control.Status + "|" + (control.StatusNodeControl / 378) + "|" + control.StatusNodeControl + "|" + control.OnOff;
                    data.d = dcontro;
                    lstData.Add(data);
                    }
               if ( dataSetting.ControlSensors  != null)
                    foreach (var controlsensor in dataSetting.ControlSensors)
                {
                        int checkss = CheckType(controlsensor.IDNodeControl);
                        if (checkss == -1)
                        {
                            Messenger = "ID nút bị điều khiển không đúng";
                            ErrorFonction(new Error(5, Messenger));
                            return;
                        }
                        //"{"id":216,"data":[{"d":"0|>|214|1|1","t":"c"}]}"
                        Data data = new Data();
                    data.t = "c";
                    string datasensor = controlsensor.SetValue + "|" + controlsensor.getCompare() + "|" + (controlsensor.IDNodeControl / 378) + "|" + controlsensor.ValueNodeControl + "|" + controlsensor.OnOff;
                    lstData.Add(data);
                }
                setting.data = lstData;
                string datastring = JsonConvert.SerializeObject(setting);
                
                Publishmqtt(Accesskey + "/updateapp", datastring);
            }
            catch(Exception e)
            {
                ErrorFonction(new Error(7,e.Message));
            }
        }
        public void Reload()
        {
            Connect();
        }
        private int CheckType(int id  )
        {
            ModelSend datas = JsonConvert.DeserializeObject<ModelSend>(API_DATA.Data);
            int type = -1;
            foreach (var item in datas.Data)
            {
                if (item.ID == id)
                {
                    type = item.Type;
                     break;
                }
            }
         
            return type;
        }
        private bool checkdata(int data, int type)
        {
            bool check = true;
            switch (type)
            {
                case 1:
                    {
                        if (data > 0 && data < 1)
                        {
                            Messenger = "Nut on/off chỉ được truyền 0 và 1";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        if (data > 100 && data < 0)
                        {
                            Messenger = "Nut Dimer chỉ được truyền 0 đến 100";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                case 4:
                    {
                        if (data > 0 && data < 1)
                        {
                            Messenger = "Nut touch chỉ được truyền 0 và 1";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                default: break;
            }
            return check;
        }

        private bool checktypedata(int type, DataSettingDetail dataSettings )
        {
            bool check = true;
            switch (type)
            {
                case 1:
                    {
                        if (dataSettings.Timers != null)
                        {
                            Messenger = "Nut On/Off không thể có dữ liệu Timers";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (dataSettings.Timers != null || dataSettings.Controls!=null)
                        {
                            Messenger = "Nut Sensor không thể có dữ liệu Timers và Control";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (dataSettings.ControlSensors != null || dataSettings.Controls != null)
                        {
                            Messenger = "Nut Dimer không thể có dữ liệu ControlSensors và Control";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                       
                    }
                case 4:
                    {
                        if (dataSettings.Timers != null || dataSettings.ControlSensors != null)
                        {
                            Messenger = "Nut Touch không thể có dữ liệu ControlSensors và Timer";
                            ErrorFonction(new Error(4, Messenger));
                            check = false;
                        }
                        break;
                    }
                default: break;
            }
            return check;
        }
        private event EventHandler<ErrorArgs> _ErrorArgs;
        public event EventHandler<ErrorArgs> ErrorArgs
        {
            add { _ErrorArgs += value; }
            remove { _ErrorArgs -= value; }
        }
        void ErrorFonction(Error errors)
        {
            _ErrorArgs(this, new ErrorArgs(errors));
        }


        private event EventHandler<DataChangeArgs> _DataChange;
        public event EventHandler<DataChangeArgs> DataChange
        {
            add { _DataChange += value; }
            remove { _DataChange -= value; }                              
        }
        void OndataChange(ObservableCollection<ModelSendDevice> datasent)
        {
            try
            {
                _DataChange(this, new DataChangeArgs(datasent));
            }
            catch
            {
                ErrorFonction(new Error(7, "Loi du lieu"));
            }
                
        }
       
    }
}
