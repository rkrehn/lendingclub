using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;


// API key: jzWKvgAd5CfhictJtZzMpYEaNRI=
// account number: 1633758

// webby: https://www.lendingclub.com/developers/submit-order.action

namespace Lending_Club_Investor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.lendingclub.com/api/investor/v1/accounts/1633758/orders");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // build variables
            var url = "https://api.lendingclub.com/api/investor/v1/accounts/1633758/summary";

            // authentication, contenttype, etc. per API
            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";
            webrequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:28.0) Gecko/20100101 Firefox/28.0";
            webrequest.Headers.Add("Authorization", "jzWKvgAd5CfhictJtZzMpYEaNRI=");

            // Setup download
            WebResponse response = webrequest.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);

            // read data
            string json = sr.ReadToEnd();

            int x = json.IndexOf("availableCash", 0);
            int y = json.IndexOf(",", x);

            json = json.Substring(x + 15, y - (x + 15));

            decimal calc = Convert.ToDecimal(json);
            decimal calc2 = Math.Round(calc / 25)*25;
            if (calc2 > calc) calc2 =- 25;

            txt_Cash.Text = calc2.ToString();

            // display available cashs
            // "availableCash\":
            // ,
        }
    }
}
