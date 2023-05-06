using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_CSharp_AddIn.Models;
using Newtonsoft.Json;
using System.IO;

namespace My_CSharp_AddIn
{
    class AssemblyRecord
    {
        public Assembly CurrentAssembly;
        public List<Component> components = new List<Component>();

        public void GetAssemble()
        {
            Inventor.Application m_inventorAplication = Globals.invApp;

            string CurrentAssembleName = m_inventorAplication.ActiveDocument.DisplayName;

            try
            {
                Inventor.AssemblyDocument assemblyDocument = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;
                GetComponent(assemblyDocument.ComponentDefinition.Occurrences, "no" , " ");
                CurrentAssembly = new Assembly(CurrentAssembleName, components);

            }
            catch (Exception)
            {

                throw;
            }

            doJson();
        }

        public void doJson()
        {
            string filePath = "C://Users//SeaRook//Desktop//gog";

            var assembleJson = JsonConvert.SerializeObject(CurrentAssembly);
            var pathOut = Path.Combine(filePath, CurrentAssembly.Name + ".json");    

            File.WriteAllText(pathOut, assembleJson);
        }

        private Assembly GetComponent(Inventor.ComponentOccurrences incollect, string SUB ,string SubAssebmbleName)
        {
            IEnumerator Em = incollect.GetEnumerator();

            Inventor.ComponentOccurrence objOc;
            Coordinates ComponentCoordinates;
            Assembly SubAssembly = null;
            Assembly TempSubAssembly = null;
            List<Component> SubComponents = new List<Component>();
            bool isAssemble = false; 

            while (Em.MoveNext() == true)
            {
                TempSubAssembly = null;
                ComponentCoordinates = null;
                isAssemble = false;

                objOc = (Inventor.ComponentOccurrence)Em.Current;
                    
                ComponentCoordinates = new Coordinates(objOc.RangeBox.MaxPoint.X, objOc.RangeBox.MaxPoint.Y, objOc.RangeBox.MaxPoint.Z);

                IEnumerator objconEnum = objOc.Constraints.GetEnumerator();
                Inventor.ComponentOccurrences temp = (Inventor.ComponentOccurrences)objOc.SubOccurrences;

                if (temp.Count>0)
                { 
                    TempSubAssembly = GetComponent((Inventor.ComponentOccurrences)objOc.SubOccurrences, "Sub" , objOc._DisplayName);
                    isAssemble = true;
                }
                

                List<Connection> connections;
                connections = GetConstrains(objconEnum);

                if (SUB != "Sub")
                {
                    Component currentComponent = new Component(objOc._DisplayName, ComponentCoordinates, connections, isAssemble, TempSubAssembly);
                    components.Add(currentComponent);
                }
                else 
                {
                    Component currentComponent = new Component(objOc._DisplayName, ComponentCoordinates, connections, isAssemble, TempSubAssembly);
                    SubComponents.Add(currentComponent);
                }
                

            }

            SubAssembly = new Assembly(SubAssebmbleName, SubComponents);

            return SubAssembly;




        }

        private List<Connection> GetConstrains(IEnumerator objconEnum)
        {
            Inventor.AssemblyConstraint oAsscon;

            List<Connection> connections = new List<Connection>();
            Connection connection;
            Coordinates coordinates;
            string NameOne;
            string NameTwo;

            while (objconEnum.MoveNext() == true)
            {
                connection = null;
                coordinates = null;

                oAsscon = (Inventor.AssemblyConstraint)objconEnum.Current;

                string Type = oAsscon.Name;

                Inventor.ComponentOccurrence One = (Inventor.ComponentOccurrence)oAsscon.OccurrenceOne;
                Inventor.ComponentOccurrence Two = (Inventor.ComponentOccurrence)oAsscon.OccurrenceTwo;

                if (One != null && Two != null)
                {
                    NameOne = One._DisplayName;
                    NameTwo = Two._DisplayName;

                    /*if (oAsscon.Type == Inventor.ObjectTypeEnum.kMateConstraintObject)
                    {
                        Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;

                        Inventor.FaceProxy faceProxy = (Inventor.FaceProxy)mateConstraint.EntityTwo;
                        if (faceProxy != null)
                        {
                            foreach (Inventor.Vertex vertex in faceProxy.NativeObject.Vertices)
                            {
                                var v = vertex.Point.X;
                                coordinates = new Coordinates(vertex.Point.X, vertex.Point.Y, vertex.Point.Z);
                            }
                        }
                    }

                    *//* Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;
                     Inventor.AngleConstraint angleConstraint = (Inventor.AngleConstraint)oAsscon;
                     Inventor.InsertConstraint insertConstraint = (Inventor.InsertConstraint)oAsscon;*//**/

                }
                else 
                {
                    NameOne = "null";
                    NameTwo = "null";
                }

                /*NameOne = "null";
                NameTwo = "null";*/

                connection = new Connection(NameOne, NameTwo, Type, coordinates);
                connections.Add(connection);
            }

            return connections;
        }
    }
}
