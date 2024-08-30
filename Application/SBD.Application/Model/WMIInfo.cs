using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Model
{
    public class WMIInfo
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string PosVID { get; set; }
        public string PosPID { get; set; }
    };
}
