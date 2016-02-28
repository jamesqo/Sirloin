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
            Dependency.RegisterAttached<string, ContentControl, BindingPaths>("Content", OnContentChanged);

        public static string GetContent(DependencyObject o) =>
            o.Get<string>(ContentProperty);

        public static void SetContent(DependencyObject o, string value) =>
            o.Set(ContentProperty, value);

        private static void OnContentChanged(ContentControl o, IPropertyChangedArgs<string> args) =>
            o.SetBinding(ContentControl.ContentProperty, args.NewValue);

        // Height

        public static readonly DependencyProperty HeightProperty =
            Dependency.RegisterAttached<string, FrameworkElement, BindingPaths>("Height", OnHeightChanged);

        public static string GetHeight(DependencyObject o) =>
            o.Get<string>(HeightProperty);

        public static void SetHeight(DependencyObject o, string value) =>
            o.Set(HeightProperty, value);

        private static void OnHeightChanged(FrameworkElement o, IPropertyChangedArgs<string> args) =>
            o.SetBinding(FrameworkElement.HeightProperty, args.NewValue);

        // Width

        public static readonly DependencyProperty WidthProperty =
            Dependency.RegisterAttached<string, FrameworkElement, BindingPaths>("Width", OnWidthChanged);

        public static string GetWidth(DependencyObject o) =>
            o.Get<string>(WidthProperty);

        public static void SetWidth(DependencyObject o, string value) =>
            o.Set(WidthProperty, value);

        private static void OnWidthChanged(FrameworkElement o, IPropertyChangedArgs<string> args) =>
            o.SetBinding(FrameworkElement.WidthProperty, args.NewValue);
    }
}
