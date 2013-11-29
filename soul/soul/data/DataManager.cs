using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace soul.data
{
    class DataManager
    {
        
        private bool keypacketSet = false;
        public static string readddd;
        string splitstr = "@";

        /*0번 / 1번 / 2번  */
        string[] divideData;

        public void SetData(string data)
        {

            readddd = data;
            //Console.WriteLine("data:");
        }    
    }
}
