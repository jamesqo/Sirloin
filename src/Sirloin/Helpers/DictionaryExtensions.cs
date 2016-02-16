using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirloin.Helpers
{
    internal static class DictionaryExtensions
    {
        public static V GetOrDefault<K, V>(this IDictionary<K, V> dict, K key)
        {
            V result;
            dict.TryGetValue(key, out result);
            return result;
        }
    }
}
