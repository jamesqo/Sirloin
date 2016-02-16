﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Sirloin.Helpers
{
    internal static class DependencyObjectExtensions
    {
        public static void BindTo(this DependencyObject obj, DependencyProperty prop, string binding)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var dict = ParseBindingParams(binding, comparer);
            var bind = CreateBindingFromParams(dict);

            BindingOperations.SetBinding(obj, prop, bind);
        }

        private static Windows.UI.Xaml.Data.Binding CreateBindingFromParams(IDictionary<string, string> dict)
        {
            var binding = new Windows.UI.Xaml.Data.Binding();

            // Parse the enums first
            BindingMode mode;
            if (Enum.TryParse(dict.GetOrDefault("Mode"), out mode)) binding.Mode = mode;

            RelativeSourceMode sourceMode;
            if (Enum.TryParse(dict.GetOrDefault("RelativeSource"), out sourceMode))
            {
                binding.RelativeSource = new RelativeSource
                {
                    Mode = sourceMode
                };
            }

            UpdateSourceTrigger trigger;
            if (Enum.TryParse(dict.GetOrDefault("UpdateSourceTrigger"), out trigger))
            {
                binding.UpdateSourceTrigger = trigger;
            }

            // Then the other stuff
            // var converter = SOMETHING;
            var converterLanguage = dict.GetOrDefault("ConverterLanguage");
            var converterParameter = dict.GetOrDefault("ConverterParameter");
            var elementName = dict.GetOrDefault("ElementName");
            var fallbackValue = dict.GetOrDefault("FallbackValue");
            var path = dict.GetOrDefault("Path");
            var source = dict.GetOrDefault("Source");
            var targetNullValue = dict.GetOrDefault("TargetNullValue");

            // And now for the null checks...
            if (converterLanguage != null)
                binding.ConverterLanguage = converterLanguage;
            if (converterParameter != null)
                binding.ConverterParameter = converterParameter;
            if (elementName != null)
                binding.ElementName = elementName;
            if (fallbackValue != null)
                binding.FallbackValue = fallbackValue;
            if (path != null)
                binding.Path = new PropertyPath(path);
            if (source != null)
                binding.Source = source;
            if (targetNullValue != null)
                binding.TargetNullValue = targetNullValue;

            // Done!
            return binding;
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
