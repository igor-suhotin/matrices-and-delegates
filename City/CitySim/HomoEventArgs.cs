using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySim
{
    public class HomoEventArgs : EventArgs
    {
        public enum HomoEventProp {Adult, Marriage};
        public readonly HomoEventProp Type;

        public HomoEventArgs(HomoEventProp prop)
        {
            Type = prop;
        }
    }
}
