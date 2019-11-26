using Emc.InputAccel.ImageFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public sealed class AutoAnnotate : ImageFilter
{
    public override string Name
    {
        get { return "AutoAnnotateFilter"; }
    }

    public override string DisplayName
    {
        get { return "Auto Annotate Filter"; }
    }

    public override bool MultiThreadAccessAllowed
    {
        get
        {
            return false;
        }
    }

    public override ImageData ApplyFilter(IFilterConfiguration configuration, ImageData imageData, Dictionary<string, object> outValues)
    {
        ImageData OutputImage = imageData;

        return OutputImage;
            
    }
}

