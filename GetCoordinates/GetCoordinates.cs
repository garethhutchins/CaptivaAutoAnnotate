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
            //Convert it to an XML Document
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(pageXML);
            XmlNodeList Fields = XMLDoc.GetElementsByTagName("Index");
            string X1 = Fields[0].Attributes.GetNamedItem("X1").Value;
            MessageBox.Show(X1);
        }

        public override void StartModule(ICodeModuleStartInfo startInfo)
        {
            //Do Nothing
        }
    }
}
