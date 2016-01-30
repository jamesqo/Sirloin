using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Sirloin.Helpers
{
    internal static class Dependency
    {
        public static void BindTo(this DependencyObject obj, DependencyProperty prop, string path)
        {
            var binding = new Windows.UI.Xaml.Data.Binding
            {
                Path = new PropertyPath(path)
            };
            BindingOperations.SetBinding(obj, prop, binding);
        }

        public static PropertyChangedArgs<T> CreateGenericArgs<T>(DependencyPropertyChangedEventArgs original)
        {
            var oldValue = (T)original.OldValue;
            var newValue = (T)original.NewValue;
            return new PropertyChangedArgs<T>(oldValue, newValue, original.Property);
        }

        public static PropertyMetadata CreateMetadata<T, TOwner>(PropertyChangedCallback<T, TOwner> callback)
            where TOwner : DependencyObject
        {
            return new PropertyMetadata(default(T), WrapGenericCallback(callback));
        }

        public static T GetValue<T>(this DependencyObject obj, DependencyProperty prop) =>
            (T)obj.GetValue(prop);

        public static DependencyProperty Register<T, TOwner>(string name, PropertyChangedCallback<T, TOwner> callback)
            where TOwner : DependencyObject
        {
            var metadata = CreateMetadata(callback);
            return DependencyProperty.Register(name, typeof(T), typeof(TOwner), metadata);
        }

        public static DependencyProperty RegisterAttached<T, TOwner, TDeclaring>(string name, PropertyChangedCallback<T, TOwner> callback)
            where TOwner : DependencyObject
        {
            var metadata = CreateMetadata(callback);
            return DependencyProperty.RegisterAttached(name, typeof(T), typeof(TDeclaring), metadata);
        }

        public static PropertyChangedCallback WrapGenericCallback<T, TOwner>(PropertyChangedCallback<T, TOwner> original)
            where TOwner : DependencyObject
        {
            return (o, args) => original((TOwner)o, CreateGenericArgs<T>(args));
        }
    }
}
