using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn.Models
{
    class Connection
    {
        //public string Id { get; set; }
        public string Type { get; set; }

        public string NameOne { get; set; }
        public string NameTwo { get; set; }
        public Coordinates Coordinates { get; set; }

        public Connection(string nameone,string nametwo, string type, Coordinates cor)
        {
            NameOne = nameone;
            NameTwo = nametwo;
            Type = type;
            Coordinates = cor;
        }
    }
}
