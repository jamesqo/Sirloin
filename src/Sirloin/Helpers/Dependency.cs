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
        public static void BindTo(this DependencyObject obj, DependencyProperty prop, string binding)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var dict = ParseBindingParams(binding, comparer);
            var bind = CreateBindingFromParams(dict);

            BindingOperations.SetBinding(obj, prop, bind);
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

        private static Windows.UI.Xaml.Data.Binding CreateBindingFromParams(IDictionary<string, string> dict)
        {
            // Parse the enums using Enum.TryParse
            BindingMode mode;
            Enum.TryParse(dict.GetOrDefault("Mode"), out mode);

            RelativeSourceMode sourceMode;
            Enum.TryParse(dict.GetOrDefault("RelativeSource"), out sourceMode);

            UpdateSourceTrigger trigger;
            Enum.TryParse(dict.GetOrDefault("UpdateSourceTrigger"), out trigger);

            return new Windows.UI.Xaml.Data.Binding
            {
                /* Converter = SOMETHING, */
                ConverterLanguage = dict.GetOrDefault("ConverterLanguage"),
                ConverterParameter = dict.GetOrDefault("ConverterParameter"),
                ElementName = dict.GetOrDefault("ElementName"),
                FallbackValue = dict.GetOrDefault("FallbackValue"),
                Mode = mode,
                Path = new PropertyPath(dict.GetOrDefault("Path")),
                RelativeSource = new RelativeSource
                {
                    Mode = sourceMode
                },
                Source = dict.GetOrDefault("Source"),
                TargetNullValue = dict.GetOrDefault("TargetNullValue"),
                UpdateSourceTrigger = trigger
            };
        }

        private static Dictionary<string, string> ParseBindingParams(string binding, IEqualityComparer<string> comparer = null)
        {
            var array = binding.Split(',');
            return array
                .Select(ParseBindingParam)
                .ToDictionary(p => p.Key, p => p.Value, comparer);
        }

        private static KeyValuePair<string, string> ParseBindingParam(string param)
        {
            var delims = new[] { '=' };
            var results = param.Trim().Split(delims, 2);

            var key = results[0].Trim();
            var value = results[1].Trim();
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
