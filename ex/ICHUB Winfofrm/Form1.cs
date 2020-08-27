using ICHUB_LIBRARY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ICHUB_LIBRARY.Models;

namespace IchubApp
{
    public partial class Form1 : Form
    {
        //khoi tao doi tuong ichub
        ConnectICHUB connectICHUB = new ConnectICHUB("EWXD111");
        int ssled = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           //khoi tao event
            connectICHUB.DataChange += ChangeData;
            connectICHUB.ErrorArgs += ErrorErgs;
        }
      private void ChangeData(object sender, DataChangeArgs e)
        {
            try
            {              
                btnLed1.Invoke((Action)delegate // gep luong
                {
                    //var data = e.Data;

                   // btnLed1.Text = e.Data[0].Data;
                    lbledname.Text = e.Data[0].Name;
                    ssled = int.Parse(e.Data[0].Data);
                    if(e.Data[0].Data == "1")
                    {
                        btnLed1.BackgroundImage = GetImage(e.Data[0].DataShow.IconOn);
                    }
                    else
                        btnLed1.BackgroundImage = GetImage(e.Data[0].DataShow.IconOff);



                    trbdimer.Value =int.Parse(e.Data[1].Data);
                    lbdimername.Text = e.Data[1].Name;
                    lbdimerdata.Text = e.Data[1].Data;

                    lbss.Text = e.Data[2].Data;
                    lbssname.Text = e.Data[2].Name;
                    lbuint.Text = e.Data[2].Unit;
                });

            }
            catch
            {

            }
        }
        private void ErrorErgs(object senser, ErrorArgs e)
        {
            MessageBox.Show(e.Error.Detail);//khi có lỗi sẽ chạy ở đây
        }

        private void btnLed1_Click(object sender, EventArgs e)
        {
            if (ssled == 0) 
            {
                connectICHUB.Senddata(86184, 1);
            }
            else
            {
                connectICHUB.Senddata(86184,0);
            }
            
        }

        private void trbdimer_MouseCaptureChanged(object sender, EventArgs e)
        {
            int dimer = trbdimer.Value;
            connectICHUB.Senddata(86562,dimer);
        }


        public Image GetImage(string url)
        {
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                return Bitmap.FromStream(stream);
            }
        }
    }
}
