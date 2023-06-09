using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_CSharp_AddIn.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace My_CSharp_AddIn
{
    class AssemblyRecord
    {
        public Assembly CurrentAssembly;
        public List<Component> components = new List<Component>();
        public string JsonPath;
        public void GetAssemble(string path)
        {
            JsonPath = path;

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

        private void doJson()
        {
           

            var assembleJson = JsonConvert.SerializeObject(CurrentAssembly);
            var pathOut = Path.Combine(JsonPath, CurrentAssembly.Name.Substring(0, CurrentAssembly.Name.Length - 4) + ".json");

            if (!Directory.Exists(JsonPath))
            {
                Directory.CreateDirectory(JsonPath);
            }
            File.WriteAllText(pathOut, assembleJson);
        }

        private Assembly GetComponent(Inventor.ComponentOccurrences incollect, string SUB ,string SubAssebmbleName)
        {
            IEnumerator Em = incollect.GetEnumerator();

            Inventor.ComponentOccurrence objOc;
            Coordinates ComponentCoordinates;
            Coordinates Rotations;
            Assembly SubAssembly = null;
            Assembly TempSubAssembly = null;
            List<Component> SubComponents = new List<Component>();
            bool isAssemble = false;

            double x1, x2, x3, y1, y2, y3, z1, z2, z3;
            double xRotationRadians, yRotationRadians, zRotationRadians;
            double xRotation, yRotation, zRotation;

            Inventor.Matrix matrix;

            while (Em.MoveNext() == true)
            {
                TempSubAssembly = null;
                ComponentCoordinates = null;
                isAssemble = false;
                Rotations = null;

                objOc = (Inventor.ComponentOccurrence)Em.Current;

                Inventor.MassProperties mass = objOc.MassProperties;

                var ptX = objOc.Transformation.Translation.X * 10.0;
                var ptY = objOc.Transformation.Translation.Y * 10.0;
                var ptZ = objOc.Transformation.Translation.Z * 10.0;

                matrix = objOc.Transformation;

                x1 = matrix.Cell[1, 1];
                x2 = matrix.Cell[1, 2];
                x3 = matrix.Cell[1, 3];

                y1 = matrix.Cell[2, 1];
                y2 = matrix.Cell[2, 2];
                y3 = matrix.Cell[2, 3];

                z1 = matrix.Cell[3, 1];
                z2 = matrix.Cell[3, 2];
                z3 = matrix.Cell[3, 3];

                CalculateRotation(matrix, objOc._DisplayName);

                /* x1 = matrix.Cell[1, 1];
                 x2 = matrix.Cell[2, 1];
                 x3 = matrix.Cell[3, 1];

                 y1 = matrix.Cell[1, 2];
                 y2 = matrix.Cell[2, 2];
                 y3 = matrix.Cell[3, 2];

                 z1 = matrix.Cell[1, 3];
                 z2 = matrix.Cell[2, 3];
                 z3 = matrix.Cell[3, 3];*/

                xRotationRadians = Math.Atan2(y3, z3);
                yRotationRadians = Math.Atan2(-x3, Math.Sqrt(y3 * y3 + z3 * z3));


                int signx1 = Math.Sign(x1);
                int signx2 = Math.Sign(x2);
                int signx3 = Math.Sign(x3);
                int signy1 = Math.Sign(y1);
                int signy2 = Math.Sign(y2);
                int signy3 = Math.Sign(y3);
                int signz1 = Math.Sign(z1);
                int signz2 = Math.Sign(z2);
                int signz3 = Math.Sign(z3);


                if (signx3 >= 1)
                { 
                    zRotationRadians = Math.Atan2(x2, x1);
                }
                else 
                {
                    zRotationRadians = Math.Atan2(y1, -y2);
                }

                xRotation = -xRotationRadians * (180 / Math.PI);
                yRotation = yRotationRadians * (180 / Math.PI);
                zRotation = -zRotationRadians * (180 / Math.PI);

                var XRotationRadians1 = Math.Atan2(y3, z3);
                var YRotationRadians1 = Math.Atan2(-x3, Math.Sqrt(y3 * y3 + z3 * z3));
                var ZRotationRadians1 = Math.Atan2(x2, x1);
                var ZRotationRadians2 = Math.Atan2(y1, -y2);

                var Test1 = -XRotationRadians1 * (180 / Math.PI);
                var Test2 = -YRotationRadians1 * (180 / Math.PI);
                var Test3 = -ZRotationRadians1 * (180 / Math.PI);
                var Test4 = -ZRotationRadians2 * (180 / Math.PI);


                ComponentCoordinates = new Coordinates(ptX, ptY, ptZ);
                Rotations = new Coordinates(xRotation, yRotation, zRotation);

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

                     Component currentComponent = new Component(objOc._DisplayName.Replace(":", "_"), ComponentCoordinates,Rotations, connections, isAssemble, TempSubAssembly);
                    components.Add(currentComponent);
                }
                else 
                {
                    Component currentComponent = new Component(objOc._DisplayName.Replace(":", "_"), ComponentCoordinates, Rotations, connections, isAssemble, TempSubAssembly);
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

            double MinOneX, MinOneY, MinOneZ, MinTwoX, MinTwoY, MinTwoZ;
            double MaxOneX, MaxOneY, MaxOneZ, MaxTwoX, MaxTwoY, MaxTwoZ;
            double MinX, MinY, MinZ, MaxX, MaxY, MaxZ;
            double X_avg, Y_avg, Z_avg;

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

                    MinOneX = One.RangeBox.MinPoint.X;
                    MinOneY = One.RangeBox.MinPoint.Y;
                    MinOneZ = One.RangeBox.MinPoint.Z;

                    MaxOneX = One.RangeBox.MaxPoint.X;
                    MaxOneY = One.RangeBox.MaxPoint.Y;
                    MaxOneZ = One.RangeBox.MaxPoint.Z;

                    MinTwoX = Two.RangeBox.MinPoint.X;
                    MinTwoY = Two.RangeBox.MinPoint.Y;
                    MinTwoZ = Two.RangeBox.MinPoint.Z;

                    MaxTwoX = Two.RangeBox.MaxPoint.X;
                    MaxTwoY = Two.RangeBox.MaxPoint.Y;
                    MaxTwoZ = Two.RangeBox.MaxPoint.Z;

                    MinX = Math.Max(MinOneX, MinTwoX);
                    MinY = Math.Max(MinOneY, MinTwoY);
                    MinZ = Math.Max(MinOneZ, MinTwoZ);

                    MaxX = Math.Min(MaxOneX, MaxTwoX);
                    MaxY = Math.Min(MaxOneY, MaxTwoY);
                    MaxZ = Math.Min(MaxOneZ, MaxTwoZ);


                    X_avg = (MinX + MaxX) / 2;
                    Y_avg = (MinY + MaxY) / 2;
                    Z_avg = (MinZ + MaxZ) / 2;

                    
                    coordinates = new Coordinates(X_avg*10, Y_avg*10, Z_avg* 10);

                    /*if (oAsscon.Type == Inventor.ObjectTypeEnum.kMateConstraintObject)
                    {
                        Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;

                        Inventor.FaceProxy faceProxy = (Inventor.FaceProxy)mateConstraint.EntityTwo;
                        if (faceProxy != null)
                        {
                            Inventor.WorkAxisProxy oAxis = null;

                            oAxis = (Inventor.WorkAxisProxy)oAsscon.EntityOne;

                        }
                    }*/
                }
                else 
                {
                    NameOne = "null";
                    NameTwo = "null";
                }

                connection = new Connection(NameOne, NameTwo, Type, coordinates);
                connections.Add(connection);
            }

            return connections;
        }


        void CalculateRotation(Inventor.Matrix matrix , string name)
        {
           
            double aRotAnglesX, aRotAnglesY, aRotAnglesZ;

            double x1, x2, x3, y1, y2, y3, z1, z2, z3;

            x1 = matrix.Cell[1, 1];
            x2 = matrix.Cell[2, 1];
            x3 = matrix.Cell[3, 1];

            y1 = matrix.Cell[1, 2];
            y2 = matrix.Cell[2, 2];
            y3 = matrix.Cell[2, 3];

            z1 = matrix.Cell[1, 3];
            z2 = matrix.Cell[2, 3];
            z3 = matrix.Cell[3, 3];


            /*aRotAnglesX = Math.Atan2(z2, z3);
            aRotAnglesY = 


            aRotAnglesX = aRotAnglesX * (180 / Math.PI);
            aRotAnglesY = aRotAnglesY * (180 / Math.PI);
            aRotAnglesZ = aRotAnglesZ * (180 / Math.PI);*/

            /*xRotationRadians = Math.Atan2(y3, z3);
            yRotationRadians = Math.Atan2(-x3, Math.Sqrt(y3 * y3 + z3 * z3));
            yRotationRadians = Math.Atan2(-x3, Math.Sqrt(y3 * y3 + z3 * z3));
            zRotationRadians = Math.Atan2(x2, x1);
            zRotationRadians = Math.Atan2(y1, -y2);*/

            MessageBox.Show(name + "\n" +"X = " + aRotAnglesX + " \n" + "Y = " + aRotAnglesY + " \n" + "Z = " + aRotAnglesZ + " \n");
        }

   
    }
}
