using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Sirloin
{
    class VectorChangedArgs : IVectorChangedEventArgs
    {
        public uint Index { get; }
        public CollectionChange CollectionChange { get; }

        public VectorChangedArgs(CollectionChange change, uint index)
        {
            this.Index = index;
            this.CollectionChange = change;
        }
    }
}
