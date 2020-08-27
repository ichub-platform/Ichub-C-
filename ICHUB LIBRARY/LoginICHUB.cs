using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ICHUB_LIBRARY.Models;

namespace ICHUB_LIBRARY
{
   public class LoginICHUB
    {
        
        private List<Project> LoginData;
        public async Task<List<Project>> Login(string username,string password)
        {
            //  string mac = ICHUB.GetMACAddress();
            try
            {
                await Task.Run(() => LoginAsync(ICHUB.Url + "Login", "={\"Username\":\"" + username + "\",\"Password\":\"" + password + "\"}"));
                return LoginData;
            }
            catch(Exception e)
            {
                return null;
            }
         }
        private async void LoginAsync(string uri, string data)
        {
           
            try
            {
                //Ichub_Api_response api = new Ichub_Api_response();
                var httpClient = new HttpClient();
                string formData = data;
              
                    var result = httpClient.PostAsync(uri,
                    new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                    string content = await result.Content.ReadAsStringAsync();
                    Ichub_Api_response dta = JsonConvert.DeserializeObject<Ichub_Api_response>(content);
                    if (dta.Status == 0)
                    {
                        LoginData = JsonConvert.DeserializeObject<List<Project>>(dta.Data);
                       
                    }
                    else
                    {
                        LoginData = null;
                       
                    }    
                   
             


            }
            catch
            {
                LoginData = null;
               
            }
        }
    }
   
}
