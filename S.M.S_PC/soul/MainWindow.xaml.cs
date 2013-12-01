using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Ports;

using System.Runtime.InteropServices;

using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace soul
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {        
        uti.Tray tray = new uti.Tray();
        com.Bluetooth BluetoothService = new com.Bluetooth();
        
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BluetoothService.InitSerialPort("COM6");

            //BluetoothService.InitSerialPort(ports[ports.Length-1]);
            BluetoothService.Start();

            this.Hide();
            tray.setWindow(this);
            tray.Start();
            
        }

        /// <summary>
        /// 종료시 팝업창 활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (System.Windows.MessageBox.Show("종료 하시겠습니까?", "알림", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                
                e.Cancel = true;
                
            }
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames(); 
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tray.trayDispose();
            BluetoothService.Close();
        }
    }
}
