using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.InputAccel.UimScript
{
    using Emc.InputAccel.CaptureClient;
    using Emc.InputAccel.UimScript;
    using System.Windows.Forms;
    using System.Xml;

    public class GetCoordinates : CustomCodeModule
    {
        public override void ExecuteTask(IClientTask task, IBatchContext batchContext)
        {
            //First Get the name of the current Step
            String StepName = task.BatchNode.StepData.StepName;
            //Now get the inputfiles for each page
            if (task.BatchNode.RootLevel > 0)
            {
                foreach (IBatchNode p in task.BatchNode.GetDescendantNodes(0))
                {
                    ReadPageXML(p);
                }
            }
            else
            {
                IBatchNode p = task.BatchNode;
                ReadPageXML(p);
            }

            //Now end
            task.CompleteTask();
            
        }
        public void ReadPageXML(IBatchNode p)
        {
            //Get the XML String
            string pageXML = p.NodeData.ValueSet.ReadString("PageXML");
            if (pageXML != "")
            {
                //Convert it to an XML Document
                XmlDocument XMLDoc = new XmlDocument();
                XMLDoc.LoadXml(pageXML);
                XmlNodeList Fields = XMLDoc.GetElementsByTagName("Index");
                if (Fields.Count ==1)
                {
                    //We're only going to do the 1 field right now
                    string X1 = Fields[0].Attributes.GetNamedItem("X1").Value;
                    string X2 = Fields[0].Attributes.GetNamedItem("X2").Value;
                    string Y1 = Fields[0].Attributes.GetNamedItem("Y1").Value;
                    string Y2 = Fields[0].Attributes.GetNamedItem("Y2").Value;
                    //Save the Values back
                    string Coordinates = X1 + "|" + X2 + "|" + Y1 + "|" + Y2;
                    p.NodeData.ValueSet.WriteString("Coordinates", Coordinates);
                }
                
            }
            

        }

        public override void StartModule(ICodeModuleStartInfo startInfo)
        {
            //Do Nothing
        }
    }
}
