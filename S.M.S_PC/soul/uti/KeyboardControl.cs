using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; //WIN32 사용

//using soul.data;

namespace soul.uti
{
    class KeyboardControl
    {
        private Dictionary<string, byte> keypacket = new Dictionary<string, byte>()
        {
            {"Backspace",0X8},{"Tab",0X9},{"Enter",0X0D},{"Shift",0X10},{"Ctrl",0X11},
            {"Alt",0X12},{"pause",0X13},{"한/영",0X15},{"한자",0X19},{"Esc",0X1B},{"space",0X20},{"PgUp",0X21},
            {"PgDn",0X22},{"End",0X23},{"Home",0X24},{"left",0X25},{"up",0X26},{"right",0X27},{"down",0X28},{"Prt SC",0X2C},
            {"Insert",0X2D},{"delete",0X2E},{"0",0X30},{"1",0X31},{"2",0X32},{"3",0X33},{"4",0X34},{"5",0X35},{"6",0X36},
            {"7",0X37},{"8",0X38},{"9",0X39},{"a",0X41},{"b",0X42},{"c",0X43},{"d",0X44},{"e",0X45},{"f",0X46},{"g",0X47},
            {"h",0X48},{"i",0X49},{"j",0X4A},{"k",0X4B},{"l",0X4C},{"m",0X4D},{"n",0X4E},{"o",0X4F},{"p",0X50},{"q",0X51},
            {"r",0X52},{"s",0X53},{"t",0X54},{"u",0X55},{"v",0X56},{"w",0X57},{"x",0X58},{"y",0X59},{"z",0X5A},{"*",0X6A},
            {"+",0X6B},{"-",0X6D},{".",0X6E},{"/",0X6F},{"f1",0X70},{"f2",0X71},{"f3",0X72},{"f4",0X73},{"f5",0X74},{"f6",0X75},
            {"f7",0X76},{"f8",0X77},{"f9",0X78},{"f10",0X79},{"f11",0X7A},{"f12",0X7B} ,{"CapsLock",0x14}
        };

        public KeyboardControl()
        {

        }

        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public void Start(string keyValue)
        {
            try
            {
                //Console.WriteLine(keyValue); 
                if (keypacket.ContainsKey(keyValue))
                {
                    keybd_event(keypacket[keyValue], 0, 0, 0);
                    keybd_event(keypacket[keyValue], 0, 0x0002, 0);
                }
                else
                {
                    string resultHex = string.Empty;
                    byte[] arr_byteStr = Encoding.Default.GetBytes(keyValue);

                    if (arr_byteStr[0] >= 65 && arr_byteStr[0] <= 90)
                    {
                        if (!System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
                        {
                            keybd_event(keypacket["CapsLock"], 0, 0, 0);
                            keybd_event(keypacket["CapsLock"], 0, 0x0002, 0);
                        }

                        keybd_event(arr_byteStr[0], 0, 0, 0);
                        keybd_event(arr_byteStr[0], 0, 0x0002, 0);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

    }
}
