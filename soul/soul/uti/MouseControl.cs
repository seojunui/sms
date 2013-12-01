using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; //WIN32 사용

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

        public void start()
        {
            MouseEvent.GetCursorPos(out nowMp);
            nowMp.x += Convert.ToInt32(data.DataManager.readddd);
            nowMp.y += Convert.ToInt32(data.DataManager.readddd);
        }
    }
}
