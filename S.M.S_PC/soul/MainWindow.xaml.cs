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

        private BackgroundWorker thread = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 윈도우 로드시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BluetoothService.InitSerialPort("COM11");            
            BluetoothService.Start();
            
            this.Hide();
            tray.setWindow(this);
            tray.Start();

            
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
            
            /*
            //btnStart.Content = "Start";
            string result = "작업이 완료되었습니다.";

            // 작업이 취소된 경우
            if (e.Cancelled)
            {
                result = "작업이 취소되었습니다.";
            }

            MessageBox.Show(result);
            */
        }

        void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            /*
            int value = e.ProgressPercentage;

            // 변경 값으로 갱신
            progress.Value = value;
            progressValue.Text = value.ToString() + "%";
             */ 
        }

        void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;



            /*
            for (int i = 0; i <= max; i++)
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
                        ellipseGrid.Children.Add(RandomEllipse());
                    });

                    // 진행률 변경 값 전송
                    worker.ReportProgress(i);
                }
            }*/
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

        /// <summary>
        /// 윈도우 종료시 자원 해제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            tray.trayDispose();
            BluetoothService.Close();
        }
    }
}
