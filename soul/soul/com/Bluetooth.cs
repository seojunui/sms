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

        uti.KeyboardControl kbc = new uti.KeyboardControl();

        public Bluetooth()
        {
            serial = new SerialPort();
            //DataC = new DataManager();
        }
        
        public void InitSerialPort(string PortName)
        {
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
                    SetData(readData);
                    //DataC.SetData(readData);  
                    kbc.Start();
                }
            }
            catch (TimeoutException)
            {
                g_sRecvData = string.Empty;
            }
        }



    }
}
