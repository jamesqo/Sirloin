using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirloin
{
    public sealed class MenuItem
    {
        public string Label { get; set; }
        public string Symbol { get; set; }
        public Type DestPage { get; set; }
        public object Argument { get; set; }
    }
}
