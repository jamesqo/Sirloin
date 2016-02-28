﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typed.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sirloin.Helpers
{
    public sealed class Binding
    {
        private Binding()
        {
            Debug.Fail(@"This class shouldn't be instantiated.
Perhaps you were looking for Windows.UI.Xaml.Data.Binding?");
        }

        // Content

        public static DependencyProperty ContentProperty { get; } =
            Dependency.RegisterAttached<string, ContentControl, Binding>("Content", OnContentChanged);

        public static string GetContent(DependencyObject o) =>
            o.Get<string>(ContentProperty);

        public static void SetContent(DependencyObject o, string value) =>
            o.Set(ContentProperty, value);

        private static void OnContentChanged(ContentControl o, IPropertyChangedArgs<string> args) =>
            o.BindTo(ContentControl.ContentProperty, args.NewValue);

        // Height

        public static DependencyProperty HeightProperty { get; } =
            Dependency.RegisterAttached<string, FrameworkElement, Binding>("Height", OnHeightChanged);

        public static string GetHeight(DependencyObject o) =>
            o.Get<string>(HeightProperty);

        public static void SetHeight(DependencyObject o, string value) =>
            o.Set(HeightProperty, value);

        private static void OnHeightChanged(FrameworkElement o, IPropertyChangedArgs<string> args) =>
            o.BindTo(FrameworkElement.HeightProperty, args.NewValue);

        // Width

        public static DependencyProperty WidthProperty { get; } =
            Dependency.RegisterAttached<string, FrameworkElement, Binding>("Width", OnWidthChanged);

        public static string GetWidth(DependencyObject o) =>
            o.Get<string>(WidthProperty);

        public static void SetWidth(DependencyObject o, string value) =>
            o.Set(WidthProperty, value);

        private static void OnWidthChanged(FrameworkElement o, IPropertyChangedArgs<string> args) =>
            o.BindTo(FrameworkElement.WidthProperty, args.NewValue);
    }
}
