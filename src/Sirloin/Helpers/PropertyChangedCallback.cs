using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Sirloin.Helpers
{
    internal delegate void PropertyChangedCallback<in T, in TOwner>(TOwner o, IPropertyChangedArgs<T> args)
        where TOwner : DependencyObject;
}
