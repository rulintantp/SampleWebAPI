using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDb
{
    public class Student
    {
        private string nim;
        public string Nim
        {
            get { return nim; }
            set { nim = value; }
        }
        public string Nama { get; set; }
        public double Ipk { get; set; }
    }
}
