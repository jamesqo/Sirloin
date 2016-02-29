using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Sirloin.DesignerSupport
{
    public static class DependencyObjectExtensions
    {
        public static void SetBinding(this DependencyObject obj, DependencyProperty prop, string input)
        {
            var binding = BindingHelpers.Parse(input);
            BindingOperations.SetBinding(obj, prop, binding);
        }
    }
}
