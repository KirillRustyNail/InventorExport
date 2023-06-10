using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_CSharp_AddIn.Classes;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace My_CSharp_AddIn
{
    class AssemblyRecord
    {
        public Assembly CurrentAssembly;
        public List<Component> components = new List<Component>();
        /*public List<MatrixRot> Test = new List<MatrixRot>();*/
        Inventor.WorkPlanes oAssyPlane;
        Inventor.WorkPlane YZ;
        Inventor.WorkPlane XZ;
        Inventor.WorkPlane XY;
        public string JsonPath;
        public void GetAssemble(string path)
        {
            JsonPath = path;

            Inventor.Application m_inventorAplication = Globals.invApp;

            string CurrentAssembleName = m_inventorAplication.ActiveDocument.DisplayName;

            try
            {
                
                Inventor.AssemblyDocument assemblyDocument = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;
                Inventor.AssemblyComponentDefinition componentDefinition = (Inventor.AssemblyComponentDefinition)assemblyDocument.ComponentDefinition;
                oAssyPlane = componentDefinition.WorkPlanes;
                foreach (Inventor.WorkPlane plane in oAssyPlane)
                {
                    if (plane.Name == "YZ Plane") YZ = plane;
                    if (plane.Name == "XZ Plane") XZ = plane;
                    if (plane.Name == "XY Plane") XY = plane;
                }
                
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
            double xRotation, yRotation, zRotation ;
            double xRot, yRot, zRot;

            Inventor.Matrix matrix;

            while (Em.MoveNext() == true)
            {
                TempSubAssembly = null;
                ComponentCoordinates = null;
                isAssemble = false;
                Rotations = null;

                objOc = (Inventor.ComponentOccurrence)Em.Current;

                Inventor.ComponentOccurrences temp = (Inventor.ComponentOccurrences)objOc.SubOccurrences;
                
                Inventor.WorkPlane Part_YZ, Part_XZ,  Part_XY;
                Inventor.MeasureTools oTool = Globals.invApp.MeasureTools;

                var ptX = objOc.Transformation.Translation.X * 10.0;
                var ptY = objOc.Transformation.Translation.Y * 10.0;
                var ptZ = objOc.Transformation.Translation.Z * 10.0;

                matrix = objOc.Transformation;

              
                var RoatationAgles = CalculateRotation(matrix, objOc._DisplayName);
                
                Part_XZ = null;

                if (temp.Count > 0)
                {
                    Inventor.AssemblyComponentDefinition AsscomponentDefinition = (Inventor.AssemblyComponentDefinition)objOc.Definition;

                    foreach (Inventor.WorkPlane plane1 in AsscomponentDefinition.WorkPlanes)
                    {
                        if (plane1.Name == "YZ Plane") Part_YZ = plane1;
                        if (plane1.Name == "XZ Plane") Part_XZ = plane1;
                        if (plane1.Name == "XY Plane") Part_XY = plane1;
                    }
                }
                else
                {
                    Inventor.PartComponentDefinition part = (Inventor.PartComponentDefinition)objOc.Definition;

                    foreach (Inventor.WorkPlane plane1 in part.WorkPlanes)
                    {
                        if (plane1.Name == "YZ Plane") Part_YZ = plane1;
                        if (plane1.Name == "XZ Plane") Part_XZ = plane1;
                        if (plane1.Name == "XY Plane") Part_XY = plane1;
                    }
                }
                   

                Object workPlaneProxy = null;

                objOc.CreateGeometryProxy(Part_XZ, out workPlaneProxy);

                var angle = oTool.GetAngle(XZ, workPlaneProxy);

                angle = (angle * 180) / Math.PI;

                ComponentCoordinates = new Coordinates(ptX, ptY, ptZ);
                Rotations = new Coordinates(RoatationAgles[0], RoatationAgles[1], -angle);

                

                IEnumerator objconEnum = objOc.Constraints.GetEnumerator();
                

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


        List<double> CalculateRotation(Inventor.Matrix matrix , string name)
        {
           
            double aRotAnglesX, aRotAnglesY, aRotAnglesZ;

            double x1, x2, x3, y1, y2, y3, z1, z2, z3;
            double[] trans = new double[12];

            x1 = matrix.Cell[1, 1];
            x2 = matrix.Cell[2, 1];
            x3 = matrix.Cell[3, 1];

            y1 = matrix.Cell[1, 2];
            y2 = matrix.Cell[2, 2];
            y3 = matrix.Cell[3, 2];

            z1 = matrix.Cell[1, 3];
            z2 = matrix.Cell[2, 3];
            z3 = matrix.Cell[3, 3];

            double EulerOne, EulerTwo, EulerThree;

            matrix.GetMatrixData(ref trans);

            matrix.Invert();

            matrix.GetMatrixData(ref trans);

            if (trans[8] != 1 && trans[8] != -1)
                EulerOne = Math.Atan2(trans[9] / Math.Cos(Math.Asin(trans[8])), trans[10] / Math.Cos(Math.Asin(trans[8])));
            else if (trans[8] == -1)
                EulerOne = Math.Atan2(trans[1], trans[2]);
            else
                EulerOne = Math.Atan2(-trans[1], -trans[2]);

            if (trans[8] != 1 && trans[8] != -1)
                EulerTwo = -Math.Asin(trans[8]);
            else if (trans[8] == -1)
                EulerTwo = Math.PI / 2;
            else
                EulerTwo = -Math.PI / 2;

            if (trans[8] != 1 && trans[8] != -1)
                EulerThree = Math.Atan2(trans[4] / Math.Cos(Math.Asin(trans[8])), trans[0] / Math.Cos(Math.Asin(trans[8])));
            else
                EulerThree = 0;

            string res1 = x1 + ", " + x2 + ", " + x3;  
            string res2 = y1 + ", " + y2 + ", " + y3;
            string res3 = z1 + ", " + z2 + ", " + z3;


            EulerOne = -EulerOne * (180 / Math.PI);
            EulerTwo = EulerTwo * (180 / Math.PI);
            EulerThree = -EulerThree * (180 / Math.PI);
            List<double> res = new List<double> { EulerOne, EulerTwo, EulerThree };

            return res;

           /* MatrixRot rot = new MatrixRot(res1 , res2, res3, name);

            Test.Add(rot);*/

        }

   
    }
}
