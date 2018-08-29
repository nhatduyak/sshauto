using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;


namespace SSHCHANGERFREE
{
    class CTLConfig
    {

        //Khoi tao bien ercom toan cuc
        public static string _port;
        public static string _pathfile;
   

      

        public static void GetConfiguration()
        {
           // string x = Guid.NewGuid().ToString();
            XmlDocument document = new XmlDocument();
            try
            {
            
                document.Load(Application.StartupPath + @"\Config.xml"); 
                _port = document.SelectSingleNode("//Port").Attributes["Value"].Value;
                _pathfile = document.SelectSingleNode("//PathFile").Attributes["Value"].Value;
               
            }
            catch (Exception exception)
            {
                CTLError.WriteError("CTLConfig getconfig", exception.Message);
                return ;
                throw new Exception(exception.Message);
            }
        }
        public static string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            string ip31 = "10." + ipAddress.ToString().Split('.')[1] + ".1.31";
            return ip31;
        }
       
      
        public void getMemoryAddr()
        {
            try
            {

            }
            catch (Exception ex)
            {
                CTLError.WriteError("getMemoryAddr in CTLConfig", ex.Message);
                return;
            }
        }
        
    }
}
