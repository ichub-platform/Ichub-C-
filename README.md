# Ichub-Csharp
thư viện c# cho ICHUB PLATFORM

source Arduino :https://github.com/DOE-Ichub/Ichub_platform

# lấy danh sách Accesskey project
```cpp
  public List<string> GetlstAccesskey()
        {
            LoginICHUB loginICHUB = new LoginICHUB();
            List<string> lstAccesskey = new List<string>();
            var content = loginICHUB.Login("quoc", "123456789").Result;
            foreach (var i in content)
            {
                lstAccesskey.Add(i.KeyConnect);
            }
            return lstAccesskey;
        }
```
# Kết nối project và điều khiển
Khởi tạo đối tượng 
```cpp
  ConnectICHUB ICHUBData = new ConnectICHUB("PPGD98"); 
```
khởi tạo sự kiện
```cpp
 ICHUBData.DataChange += ChangeData;
 ICHUBData.ErrorArgs += ErrorErgs;
 ```
 ```cpp
       private  void ChangeData(object sender, DataChangeArgs e )
        {
            try {
                this.Dispatcher.Invoke(() =>
                {
                    lv.ItemsSource = e.Data;
                    Led1 =int.Parse(e.Data[0].Data);
                    Led2 = int.Parse(e.Data[1].Data);
                    if (Led1 ==1) btnled1.Content = "ON";
                    else btnled1.Content = "OFF";
                    if (Led2 == 1) btnled2.Content = "ON";
                    else btnled2.Content = "OFF";
                    sl.Value= int.Parse(e.Data[3].Data);
                    sltxt.Text = int.Parse(e.Data[3].Data).ToString();
                });
                
            }
            catch
            {

            }
        }
        private void ErrorErgs  (object senser,ErrorArgs e)
        {
            MessageBox.Show(e.Error.Detail);
        }
        ```
