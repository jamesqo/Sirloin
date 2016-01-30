using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Sirloin.Helpers
{
    internal sealed class PropertyChangedArgs<T> : EventArgs, IPropertyChangedArgs<T>
    {
        public T OldValue { get; }
        public T NewValue { get; }
        public DependencyProperty Property { get; }

        public PropertyChangedArgs(T oldValue, T newValue, DependencyProperty property)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Property = property;
        }
    }
}
