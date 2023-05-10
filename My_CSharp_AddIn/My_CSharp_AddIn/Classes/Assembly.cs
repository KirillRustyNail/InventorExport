using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn.Models
{
    class Assembly
    {
        //public string Id { get; set; }-vp 
        public string Name { get; set; }
        public List<Component> Components { get; set; }


        public Assembly(string name, List<Component> components)
        {

            Name = name;
            Components = components;

        }
    }
}
