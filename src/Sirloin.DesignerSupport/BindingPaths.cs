using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typed.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sirloin.Internal
{
    public sealed class BindingPaths
    {
        private BindingPaths()
        {
            throw new InvalidOperationException("This class shouldn't be instantiated.");
        }

        // Content

        public static readonly DependencyProperty ContentProperty =
            Dependency.RegisterAttached<string, ContentControl, BindingPaths>("Content", ContentChanged);

        public static string GetContent(DependencyObject obj) => obj.Get<string>(ContentProperty);

        public static void SetContent(DependencyObject obj, string value) => obj.Set(ContentProperty, value);

        private static void ContentChanged(ContentControl control, IPropertyChangedArgs<string> args)
        {
            control.SetBinding(ContentControl.ContentProperty, args.NewValue);
        }

        // Height

        public static readonly DependencyProperty HeightProperty =
            Dependency.RegisterAttached<string, FrameworkElement, BindingPaths>("Height", HeightChanged);

        public static string GetHeight(DependencyObject obj) => obj.Get<string>(HeightProperty);

        public static void SetHeight(DependencyObject obj, string value) => obj.Set(HeightProperty, value);

        private static void HeightChanged(FrameworkElement element, IPropertyChangedArgs<string> args)
        {
            element.SetBinding(FrameworkElement.HeightProperty, args.NewValue);
        }

        // Width

        public static readonly DependencyProperty WidthProperty =
            Dependency.RegisterAttached<string, FrameworkElement, BindingPaths>("Width", WidthChanged);

        public static string GetWidth(DependencyObject obj) => obj.Get<string>(WidthProperty);

        public static void SetWidth(DependencyObject obj, string value) => obj.Set(WidthProperty, value);

        private static void WidthChanged(FrameworkElement element, IPropertyChangedArgs<string> args)
        {
            element.SetBinding(FrameworkElement.WidthProperty, args.NewValue);
        }
    }
}
