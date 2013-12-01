using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using soul.data;

namespace soul.com
{
    class Bluetooth : DataManager
    {
        SerialPort serial;
        string g_sRecvData = String.Empty;       

        public Bluetooth()
        {
            serial = new SerialPort();            
        }
        
        public void InitSerialPort(string PortName)
        {
            //string[] ports = SerialPort.GetPortNames(); 
            Console.WriteLine("open");
            serial.PortName = PortName;
            serial.BaudRate = 115200;
            serial.DataBits = 8;
            serial.Parity = Parity.None;
            serial.StopBits = StopBits.One;
            serial.ReadTimeout = 10000;
            serial.WriteTimeout = 10000;            
        }
        public void Start(){
            serial.Open();
            serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
        }
        
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string readData = serial.ReadExisting();
                
                if ((readData != string.Empty))
                {
                    Manage(readData);                    
                }
            }
            catch (TimeoutException)
            {
                g_sRecvData = string.Empty;
            }
        }

        public void Close()
        {
            if (this.serial.IsOpen)
                this.serial.Close();
        }

    }
}
