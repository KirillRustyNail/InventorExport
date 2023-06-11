using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn.Models
{
    class Coordinates
    {
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }

        public Coordinates(Double x, Double y, Double z)
        {

            X = x;
            Y = y;
            Z = z;
           
        }
    }
}
