using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn
{
    class Part
    {
        public int Id { get; set; }
        public double coordinateX { get; set; }
        public double coordinateY { get; set; }
        public double coordinateZ { get; set; }

        public Part UnionPart { get; set; }

        public String Constraints;

        
    }
}
