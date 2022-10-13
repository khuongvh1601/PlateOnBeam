using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;

namespace K_PlateOnAngleBeam
{
    //[Plugin("K_Plate_On_AngleBeam")]
    [PluginUserInterface("K_PlateOnAngleBeam.DataFormBolt")]

    public class DataFormBolt
    {
        [StructuresField("bangma")]
        public string bangma;

    }
}
