using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace soul.data
{
    class DataManager
    {
        private Dictionary<string, byte> keypacket = new Dictionary<string, byte>()
        {
            {",",0xBC} ,{"`",0xC0},{"/",0xBF},{"[",0xDB},{"]",0xDD},{";",0xBA},{"'",0xDE},{"window",0X5C},{"=",187}            
        };

        uti.KeyboardControl kbc = new uti.KeyboardControl();
        uti.MouseControl mc = new uti.MouseControl();

        public void Manage(string data)
        {
            //Console.WriteLine("data:");
            string[] divideData = data.Split('\\');

            switch (divideData[0])
            {
                case "k":   //키보드 컨트롤러 실행
                    kbc.Start(divideData[1]);
                    break;
                case "m":   //마우스 컨트롤러 실행
                    mc.Start(divideData[1]);
                    break;
                case "c":   //클릭 이벤트 실행
                    mc.Click(divideData[1]);
                    break;
                case "k2":
                    kbc.Start(divideData[1], keypacket);
                    break;
                default:
                    break;
            }
        }
    }
}
