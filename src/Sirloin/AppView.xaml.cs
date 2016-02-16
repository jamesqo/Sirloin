using Sirloin.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Typed.Xaml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Sirloin
{
    [ContentProperty(Name = nameof(FrameContent))]
    public sealed partial class AppView : UserControl
    {
        public static AppView Current { get; private set; }

        // Expose the GUI elements via properties
        public Frame Frame => this.frame;
        public SplitView SplitView => this.splitView;
        public ListView UpperView => this.upperView;
        public ListView LowerView => this.lowerView;

        // Expose the Items as regular CLR properties
        public ItemCollection LowerItems => lowerView.Items;
        public ItemCollection UpperItems => upperView.Items;

        // Begin dependency property cruft

        // FrameContent:

        public object FrameContent
        {
            get { return this.Get<object>(FrameContentProperty); }
            set { this.Set(FrameContentProperty, value); }
        }

        public static DependencyProperty FrameContentProperty { get; } =
            Dependency.Register<object, AppView>(nameof(FrameContent), FrameContentChanged);

        private static void FrameContentChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.frame.Content = args.NewValue;
        }

        // IsPaneOpen:

        public bool IsPaneOpen
        {
            get { return this.Get<bool>(IsPaneOpenProperty); }
            set { this.Set(IsPaneOpenProperty, value); }
        }

        public static DependencyProperty IsPaneOpenProperty { get; } =
            Dependency.Register<bool, AppView>(nameof(IsPaneOpen), IsPaneOpenChanged);

        private static void IsPaneOpenChanged(AppView o, IPropertyChangedArgs<bool> args)
        {
            o.splitView.IsPaneOpen = args.NewValue;
        }

        // LowerSource:

        public object LowerSource
        {
            get { return this.Get<object>(LowerSourceProperty); }
            set { this.Set(LowerSourceProperty, value); }
        }

        public static DependencyProperty LowerSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(LowerSource), LowerSourceChanged);

        private static void LowerSourceChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.lowerView.ItemsSource = args.NewValue;
        }

        // UpperSource:

        public object UpperSource
        {
            get { return this.Get<object>(UpperSourceProperty); }
            set { this.Set(UpperSourceProperty, value); }
        }

        public static DependencyProperty UpperSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(UpperSource), UpperSourceChanged);

        private static void UpperSourceChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.upperView.ItemsSource = args.NewValue;
        }

        // End dependency property cruft

        public AppView()
        {
            this.InitializeComponent();

            this.Loaded += (o, e) => Current = this;
        }

        private void OnHamburgerClicked(object sender, RoutedEventArgs e)
        {
            var splitView = this.splitView; // save a field access
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }
    }
}
