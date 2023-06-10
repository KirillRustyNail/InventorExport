using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn.Classes
{
    class MatrixRot
    {
        public string name { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }



        public MatrixRot(string One , string Two, string Three, string name)
        {
            this.One = One;
            this.Two = Two;
            this.Three = Three;
            this.name = name;
        }
    }
}
