using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; //WIN32 사용
using System.Threading;
namespace soul.uti
{
    class MouseControl
    {
        private MousePoint nowMp;

        class MouseEvent
        {
            [DllImport("User32")]
            public static extern int GetCursorPos(out MousePoint pt);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int SetCursorPos(int x, int y);
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;


        private bool dragOn = false;

        public struct MousePoint
        {
            public int x;
            public int y;
            public MousePoint(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
        }

        public void Start(string MPoint)
        {
            try
            {
                MouseEvent.GetCursorPos(out nowMp);

                string[] convertData = MPoint.Split('\n');
                int x = Convert.ToInt32(convertData[0]);
                int y = Convert.ToInt32(convertData[1]);
                nowMp.x += x;
                nowMp.y += y;

                MouseEvent.SetCursorPos(nowMp.x, nowMp.y);
            }
            catch (Exception e)
            {
            }
        }


        public void Click(string MPoint)
        {

            try
            {
                MouseEvent.GetCursorPos(out nowMp);
                switch (MPoint)
                {
                    case "lc":
                        mouse_event(MOUSEEVENTF_LEFTDOWN, nowMp.x, nowMp.y, 0, 0);
                        //Thread.Sleep(30);
                        mouse_event(MOUSEEVENTF_LEFTUP, nowMp.x, nowMp.y, 0, 0);
                        break;
                    case "rc":
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, nowMp.x, nowMp.y, 0, 0);
                        //Thread.Sleep(30);
                        mouse_event(MOUSEEVENTF_RIGHTUP, nowMp.x, nowMp.y, 0, 0);
                        break;
                    case "drag":
                        if (!dragOn)
                        {
                            mouse_event(MOUSEEVENTF_LEFTDOWN, nowMp.x, nowMp.y, 0, 0);
                            dragOn = true;
                        }
                        else
                        {
                            mouse_event(MOUSEEVENTF_LEFTUP, nowMp.x, nowMp.y, 0, 0);
                            dragOn = false;
                        }

                        break;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
