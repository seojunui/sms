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
        SerialPort serial = new SerialPort();
        uti.Tray tray = new uti.Tray();
        com.Bluetooth BluetoothService = new com.Bluetooth();

        private BackgroundWorker thread = new BackgroundWorker();


        /*
        private string[] keystr = {
                                      "lc","rc","wh","back","tab","enter","shift","ctrl","alt","pause",
                                      "cap","han","chi","esc","space","pgu","pgd","end","home","left",
                                      "up","right","down","ps","insert","delete","0","1","2","3","4",
                                      "5","6","7","8","9","a","b","c","d","e","f","g","h","i","j","k",
                                      "l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","window",
                                      "menu","num0","num1","num2","num3","num4","num5","num6","num7","num8",
                                      "num9","nmul","+","-",".","mod","f1","f2","f3","f4","f5","f6","f7","f8",
                                      "f9","f10","f11","f12",//"numlock"
                                      //,"scrolllock","eback","enext","erefresh","eesc","eenter","ebook","ehome","mute","vdown","vup","hotkey1","hotkey2","sum","comma","min","dot"
                                  };
        private byte[] keyvalue = {
                                      0x1,0x2,0x4,0x8,0x9,0x0D,0x10,0x11,0x12,0x13,0x14,0x15,0x19,0x1B,
                                      0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,0x2C,0x2D,0x2E,0x30,
                                      0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,0x41,0x42,0x43,0x44,
                                      0x45,0x46,0x47,0x48,0x49,0x4A,0x4B,0x4C,0x4D,0x4E,0x4F,0x50,0x51,
                                      0x52,0x53,0x54,0x55,0x56,0x57,0x58,0x59,0x5A,0x5B,0x5D,0x60,0x61,
                                      0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x69,0x6A,0x6B,0x6D,0x6E,0x6F,
                                      0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,0x79,0x7A,0x7B,//0x90,
                                      //0x91,0xA6,0xA7,0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF,0xB6,0xB7,0xBB,0xBC,0xBD,0xBE
                                  };
        */
        
        
        public string g_sRecvData = String.Empty;
        int max;

        public MainWindow()
        {
            InitializeComponent();            
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);

            //progress.Maximum = keystr.Length;
            //max = keystr.Length;

            // 진행률 전송 여부
            thread.WorkerReportsProgress = true;

            // 작업 취소 여부
            thread.WorkerSupportsCancellation = true;

            // 작업 쓰레드 
            thread.DoWork += new DoWorkEventHandler(thread_DoWork);

            // 진행률 변경
            thread.ProgressChanged += new ProgressChangedEventHandler(thread_ProgressChanged);

            // 작업 완료
            thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_RunWorkerCompleted);
        }

        void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            string result = "작업이 완료되었습니다.";

            // 작업이 취소된 경우
            if (e.Cancelled)
            {
                result = "작업이 취소되었습니다.";
            }

            MessageBox.Show(result);
            this.Hide();
        }

        void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int value = e.ProgressPercentage;

            // 변경 값으로 갱신
            progress.Value = value;
            progressValue.Text = value.ToString() + "%";
        }

        void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i <= 90; i++)
            {
                // CancelAsync() 메서드가 호출되었다면 정지
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(50);

                    // UI 쓰레드에 접근
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                    {
                        //ellipseGrid.Children.Add(RandomEllipse());                       
                    });


                    // 진행률 변경 값 전송
                    worker.ReportProgress(i);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (thread.IsBusy)
            {
                thread.CancelAsync();
            }
            else
            {
                thread.RunWorkerAsync();
            }
            */
            BluetoothService.InitSerialPort("COM6");
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
    }
}
