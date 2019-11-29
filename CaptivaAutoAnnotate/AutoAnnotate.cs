

namespace Custom.InputAccel.UimFilterScript
{
   
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using Emc.InputAccel.ImageFilter;
    using Emc.InputAccel.UimScript;
    public class ScriptAutoAnnotate : UimScriptFilter
    {
        public ScriptAutoAnnotate()
			: base()
		{
		}
        /// <summary>
        /// Executes custom super filter and extends output values 
        /// </summary>
        /// <param name="imageData">Image data with source image stream</param>
        /// <param name="stepCustomValue">Custom script parameters.</param>
        /// <param name="uimScriptFilterContext">Image super filter object</param>
        /// <param name="outValues">Extra data's collection returned by the script</param>
        /// <returns>Image data object that contains result image. Must be released on client side</returns>
        public override ImageData ExecuteSuperFilter(ImageData imageData, string stepCustomValue, IUimScriptFilterContext uimScriptFilterContext, Dictionary<string, object> outValues)
        {
            //The filter to redact the selected area
            //Create the dictionary if it doesn't exist
            if (outValues == null)
            {
                outValues = new Dictionary<string, object>();
            }
            //Create a new rectangle
            Rect rect = new Rect();

            //Set the annotation Type
            String AnnotationType = "Redact";

            Int32 argbColourForegound = Color.Black.ToArgb();
            Int32 argbColourBackground = Color.Black.ToArgb();
            Double opacity = 1.0;
            String formatText = "NA";
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
                uimScriptFilterContext.CreateAndApplyAnnotation(rect, AnnotationType, argbColourForegound, argbColourBackground, opacity, formatText);
            }
            //return the image
            return imageData;
        }
    }
    }
    


