using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMF
{
    public struct Model
    {
        public Rage.Model BaseModel { get; }

        public Model(Rage.Model model)
        {
            BaseModel = model;
        }
    }
}
