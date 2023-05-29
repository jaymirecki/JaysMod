using JaysModFramework.Clothing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JaysModFramework
{
    public class Config

    {
        public bool InteractionMenuModuleEnabled { get; set; } = false;
        public bool BigMapModuleEnabled { get; set; } = false;
        public bool SirenModuleEnabled { get; set; } = false;
    }
}
