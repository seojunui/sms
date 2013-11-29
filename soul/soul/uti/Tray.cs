using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace soul.uti
{
    class Tray
    {
        private System.Windows.Threading.DispatcherTimer trayToolTiptimer;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Window window;
        public Tray()
        {
            notify = new System.Windows.Forms.NotifyIcon();
            notify.Icon = soul.Properties.Resources.trayicon;
        }

        public void setWindow(System.Windows.Window _window)
        {
            this.window = _window;
        }

        public void Start()
        {
            try
            {                
                notify.Visible = true;

                notify.DoubleClick +=
                    delegate(object senders, EventArgs args)
                    {
                        //this.Show();
                        //this.WindowState = WindowState.Normal;
                        //this.Close();
                        this.window.Close();
                    };

                trayToolTiptimer = new System.Windows.Threading.DispatcherTimer();
                trayToolTiptimer.Interval = new TimeSpan(0, 0, 1);
                trayToolTiptimer.Tick += new EventHandler(trayToolTiptimer_Tick);
                trayToolTiptimer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("tray Exeption");
                trayToolTiptimer.Stop();
            }
        }

        private void trayToolTiptimer_Tick(object sender, EventArgs e)
        {
            notify.BalloonTipTitle = "S.M.S";
            notify.BalloonTipText = "Window Control start";
            notify.ShowBalloonTip(500);

            trayToolTiptimer.Stop();
        }

    }
}
