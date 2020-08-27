# Ichub-Csharp
thư viện c# cho ICHUB PLATFORM

source Arduino :https://github.com/DOE-Ichub/Ichub_platform

# lấy danh sách Accesskey project
```cpp
  public List<string> GetlstAccesskey()
        {
            LoginICHUB loginICHUB = new LoginICHUB();
            List<string> lstAccesskey = new List<string>();
            var content = loginICHUB.Login("usernam", "password").Result; // tài khoản đăng nhập trên app
            //content là danh sách các project bạn có thể lấy đucợ tên prọect và các thuộc tính khác
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
 Bắt sự kiện
 ```cpp
       private  void ChangeData(object sender, DataChangeArgs e )
        {
            try {
                this.Dispatcher.Invoke(() =>
                {
                  var data = e.Data;
                  //khi có thai dổi sẽ chạy ở đây
                  //e.Data là danh sách các node kèm thuộc tính.
                  //Tất cả dữ liệu của dự án có trong đây, phân tích ra và  đưa vào frontend
                  // có thể luu dữ liệu vào db của bạn
                });
                
            }
            catch
            {

            }
        }
        private void ErrorErgs  (object senser,ErrorArgs e)
        {
            MessageBox.Show(e.Error.Detail);//khi có lỗi sẽ chạy ở đây
        }
```
        
        
Điều khiển
```cpp
ICHUBData.Senddata(82026, data);
//82026 là id node trên app, data là dữ liệu điều khiển 
//on-off chỉ được truyền 0 hoặc 1, dimer truyền 0 đến 100, sensor truyền số nguyên 

```

Cài đặt hẹn giờ và điều khiển

```cpp
DataSettingDetail dataSettingDetail = new DataSettingDetail();
            ICHUBData.SendSeting(81648, dataSettingDetail); 
            // 81648 id node trên app,
            //dataSettingDetail list gồm 3 loại setting: hẹn giờ, nut điều khiển nút, sensor điều khiển nút.
            //On-Off chỉ có dữ liệu hẹn giờ và điều khiển nút khác.
            //Sensor: chỉ có dữ liệu điều khiển nút.
            //dimer : chỉ hẹn giờ
            //touch chỉ điều khiển nút khác.
