using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirloin.Helpers
{
    internal static class CollectionExtensions
    {
        public static void ReplaceWith<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();

            foreach (var item in items)
                collection.Add(item);
        }
    }
}
