using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_CSharp_AddIn
{
    class FilletRemover
    {
        Inventor.Application m_inventorAplication = Globals.invApp;
        double FilletRadius =0 ;

        public void RemoveFiller(double filletRadius)
        {
            Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;
            FilletRadius = filletRadius/10;

            Simple(ASs.ComponentDefinition.Occurrences);
        }

        //Model improvement by removing fillet
        private void Simple(Inventor.ComponentOccurrences incollect)
        {
            IEnumerator Em = incollect.GetEnumerator();
            Inventor.ComponentOccurrence objOc;
            Inventor.PartDocument desiredPart = null;
            Inventor.FilletFeature oFilletF = null;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                if (objOc.DefinitionDocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject)
                {
                    desiredPart = (Inventor.PartDocument)objOc.Definition.Document;

                    foreach (Inventor.PartFeature feature in desiredPart.ComponentDefinition.Features)
                    {
                        if (feature is Inventor.FilletFeature)
                        {
                            try
                            {

                                oFilletF = (Inventor.FilletFeature)feature;
                                Inventor.ParametersEnumerator asds = oFilletF.Parameters;
                                Inventor.Parameter One = asds[1];

                                double val = Convert.ToDouble(One.Value);

                                if (val < (FilletRadius))
                                {
                                    oFilletF.Delete();
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }
                    }
                }


                Simple((Inventor.ComponentOccurrences)objOc.SubOccurrences);
            }
        }


    }
}
