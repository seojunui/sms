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
            //this.components = new System.ComponentModel.Container();
            //this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.menuItem1 });

            // Initialize menuItem1 
            //this.menuItem1.Index = 0;
            //this.menuItem1.Text = "E&xit";
            //this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            this.notify = new System.Windows.Forms.NotifyIcon();
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

        public void trayDispose()
        {            
            trayToolTiptimer.Stop();
            notify.Dispose();
        }       
    }
}
