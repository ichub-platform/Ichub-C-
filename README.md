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
