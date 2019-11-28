using Emc.InputAccel.ImageFilter;
using Emc.InputAccel.UimScript;
using Emc.InputAccel.CaptureClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Custom.InputAccel.UimFilterScript
{
    public class ScriptAutoAnnotate : UimScriptFilter
    {
        public ScriptAutoAnnotate()
			: base()
		{
		}
    public override ImageData ExecuteSuperFilter(ImageData imageData, string stepCustomValue, IUimScriptFilterContext uimScriptFilterContext, Dictionary<string, object> outValues)
        {
            //The filter to redact the selected area

            //Create a new rectangle
            Rect rect = new Rect();

            //Set the annotation Type
            String AnnotationType = "Redact";
            Int32 argbColourForegound = 1;
            Int32 argbColourBackground = 1;
            Double opacity = 1.0;
            String formtatText = "N/A";
            string[] arrSplit;

            //Check that there are some coordinates
            if (stepCustomValue != "")
            {
                arrSplit = stepCustomValue.Split("|"[0]);
                //X1,X2,X3,X4
                //x and width
                rect.X = Convert.ToDouble(arrSplit[0]);
                rect.Width = (Convert.ToDouble(arrSplit[1]) - rect.X);
                //y and height
                rect.Y = Convert.ToDouble(arrSplit[2]);
                rect.Height = (Convert.ToDouble(arrSplit[3]) - rect.Y);
                //Now apply the filter
                uimScriptFilterContext.CreateAndApplyAnnotation(rect, AnnotationType, argbColourForegound, argbColourBackground, opacity, formtatText);
            }
            //return the image
            return imageData;
        }
    }
    }
    


