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
        /*
        private string[] keystr = {
                                      "lc","rc","wh","back","tab","enter","shift","ctrl","alt","pause",
                                      "cap","han","chi","esc","space","pgu","pgd","end","home","left",
                                      "up","right","down","ps","insert","delete","0","1","2","3","4",
                                      "5","6","7","8","9","a","b","c","d","e","f","g","h","i","j","k",
                                      "l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","window",
                                      "menu","num0","num1","num2","num3","num4","num5","num6","num7","num8",
                                      "num9","nmul","+","-",".","mod","f1","f2","f3","f4","f5","f6","f7","f8",
                                      "f9","f10","f11","f12","numlock","scrolllock","eback","enext","erefresh",
                                      "eesc","eenter","ebook","ehome","mute","vdown","vup","hotkey1","hotkey2","+",
                                      ",","-",".",
                                  };
        private byte[] keyvalue = {
                                      0x1,0x2,0x4,0x8,0x9,0x0D,0x10,0x11,0x12,0x13,0x14,0x15,0x19,0x1B,
                                      0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,0x2C,0x2D,0x2E,0x30,
                                      0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,0x41,0x42,0x43,0x44,
                                      0x45,0x46,0x47,0x48,0x49,0x4A,0x4B,0x4C,0x4D,0x4E,0x4F,0x50,0x51,
                                      0x52,0x53,0x54,0x55,0x56,0x57,0x58,0x59,0x5A,0x5B,0x5D,0x60,0x61,
                                      0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x69,0x6A,0x6B,0x6D,0x6E,0x6F,
                                      0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,0x79,0x7A,0x7B,0x90,
                                      0x91,0xA6,0xA7,0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF,0xB6,0xB7,
                                      0xBB,0xBC,0xBD,0xBE,
                                  };
        */
        //private Dictionary<string, byte> keypacket = new Dictionary<string, byte>();
        /*{
            {"lc",0x1}, {"rc",0x2}, {"wh",0x4},{"back",0x8}, {"tab",0x9},{"enter",0x0D},{"shift",0x10},{"ctrl",0x11},
            {"alt",0x12},{"pause",0x13},{"cap",0x14},{"han",0x15},{"chi",0x19},{"esc",0x1B},{"space",0x20},{"pgu",0x21},
            {"pgd",0x22},{"end",0x23},{"home",0x24},{"left",0x25},{"up",0x26},{"right",0x27},{"down",0x28},{"ps",0x2C},
            {"insert",0x2D},{"delete",0x2E},{"0", 0x30},{"1", 0x31},{"2", 0x32},{"3", 0x33},{"4", 0x34},{"5", 0x35},
            {"6", 0x36},{"7", 0x37},{"8", 0x38},{"9", 0x39},{"a", 0x41},{"b", 0x42},{"c", 0x43},{"d", 0x44},{"e", 0x45},
            {"f", 0x46},{"g", 0x47},{"h", 0x48},{"i", 0x49},{"j", 0x4A},{"k", 0x4B},{"l", 0x4C},{"m", 0x4D},{"n", 0x4E},
            {"o", 0x4F},{"p", 0x50},{"q", 0x51},{"r", 0x52},{"s", 0x53},{"t", 0x54},{"u", 0x55},{"v", 0x56},{"w", 0x57},
            {"x", 0x58},{"y", 0x59},{"z", 0x5A},{"window",0x5B},{"menu",0x5D},{"num0",0x60},{"num1",0x61},{"num2",0x62},
            {"num3",0x63},{"num4",0x64},{"num5",0x65},{"num6",0x66},{"num7",0x67},{"num8",0x68},{"num9",0x69},
            {"nmul",0x6A},{"+",0x6B},{"-",0x6D},{".",0x6E},{"/",0x6F},{"f1",0x70},{"f2",0x71},{"f3",0x72},{"f4",0x73},
            {"f5",0x74},{"f6",0x75},{"f7",0x76},{"f8",0x77},{"f9",0x78},{"f10",0x79},{"f11",0x7A},{"f12",0x7B},{"numlock",0x90},
            {"scrolllock",0x91},{"eback",0xA6},{"enext",0xA7},{"erefresh",0xA8},{"eesc",0xA9},{"eenter",0xAA},{"ebook",0xAB},{"ehome",0xAC},
            {"mute",0xAD},{"vdown",0xAE},{"vup",0xAF},{"hotkey1",0xB6},{"hotkey2",0xB7},{"+",0xBB},{",",0xBC},{"-",0xBD},{".",0xBE},
        };*/

        private Dictionary<string, byte> keypacket = new Dictionary<string, byte>()
        {
            {"lc",0X1},{"rc",0X2},{"wh",0X4},{"back",0X8},{"tab",0X9},{"enter",0X0D},{"shift",0X10},{"ctrl",0X11},
            {"alt",0X12},{"pause",0X13},{"cap",0X14},{"han",0X15},{"chi",0X19},{"esc",0X1B},{"space",0X20},{"pgu",0X21},
            {"pgd",0X22},{"end",0X23},{"home",0X24},{"left",0X25},{"up",0X26},{"right",0X27},{"down",0X28},{"ps",0X2C},
            {"insert",0X2D},{"delete",0X2E},{"0",0X30},{"1",0X31},{"2",0X32},{"3",0X33},{"4",0X34},{"5",0X35},{"6",0X36},
            {"7",0X37},{"8",0X38},{"9",0X39},{"a",0X41},{"b",0X42},{"c",0X43},{"d",0X44},{"e",0X45},{"f",0X46},{"g",0X47},
            {"h",0X48},{"i",0X49},{"j",0X4A},{"k",0X4B},{"l",0X4C},{"m",0X4D},{"n",0X4E},{"o",0X4F},{"p",0X50},{"q",0X51},
            {"r",0X52},{"s",0X53},{"t",0X54},{"u",0X55},{"v",0X56},{"w",0X57},{"x",0X58},{"y",0X59},{"z",0X5A},{"*",0X6A},
            {"+",0X6B},{"-",0X6D},{".",0X6E},{"/",0X6F},{"f1",0X70},{"f2",0X71},{"f3",0X72},{"f4",0X73},{"f5",0X74},{"f6",0X75},
            {"f7",0X76},{"f8",0X77},{"f9",0X78},{"f10",0X79},{"f11",0X7A},{"f12",0X7B}
        };
        public KeyboardControl()
        {
        }

        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        
        public void Start()
        {
            try
            {
                Console.WriteLine(soul.data.DataManager.readddd);
                if (keypacket.ContainsKey(soul.data.DataManager.readddd))
                {
                    keybd_event(keypacket[soul.data.DataManager.readddd], 0, 0, 0);
                    keybd_event(keypacket[soul.data.DataManager.readddd], 0, 0x0002, 0);
                }
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
