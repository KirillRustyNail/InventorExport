using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_CSharp_AddIn.Classes
{
    class Component
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        
        public Coordinates Coordinates { get; set; }
        public Coordinates Rotation { get;set; }
        public List<Connection> Connections { get; set; }
        public bool IsAssembly { get; set; }
        public Assembly Assembly { get; set; }


        public Component(string name, Coordinates cor, Coordinates rotation, List<Connection> connections, bool isAssemble, Assembly assembly)
        {
            Name = name;            
            Coordinates = cor;
            Rotation = rotation;
            Connections = connections;
            IsAssembly = isAssemble;
            Assembly = assembly;
        }
    
    }
}
