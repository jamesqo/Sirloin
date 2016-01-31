using Sirloin.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public ListView TopView => this.topView;
        public ListView BottomView => this.bottomView;

        // Begin dependency property cruft

        // BottomViewSource:

        public object BottomViewSource
        {
            get { return this.GetValue<object>(BottomViewSourceProperty); }
            set { this.SetValue(BottomViewSourceProperty, value); }
        }

        public static DependencyProperty BottomViewSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(BottomViewSource), BottomViewSourcePropertyChanged);

        private static void BottomViewSourcePropertyChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.bottomView.ItemsSource = args.NewValue;
        }

        // FrameContent:

        public object FrameContent
        {
            get { return this.GetValue<object>(FrameContentProperty); }
            set { this.SetValue(FrameContentProperty, value); }
        }

        public static DependencyProperty FrameContentProperty { get; } =
            Dependency.Register<object, AppView>(nameof(FrameContent), FrameContentPropertyChanged);

        private static void FrameContentPropertyChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.frame.Content = args.NewValue;
        }

        // IsPaneOpen:

        public bool IsPaneOpen
        {
            get { return this.GetValue<bool>(IsPaneOpenProperty); }
            set { this.SetValue(IsPaneOpenProperty, value); }
        }

        public static DependencyProperty IsPaneOpenProperty { get; } =
            Dependency.Register<bool, AppView>(nameof(IsPaneOpen), IsPaneOpenPropertyChanged);

        private static void IsPaneOpenPropertyChanged(AppView o, IPropertyChangedArgs<bool> args)
        {
            o.splitView.IsPaneOpen = args.NewValue;
        }

        // TopViewSource:

        public object TopViewSource
        {
            get { return this.GetValue<object>(TopViewSourceProperty); }
            set { this.SetValue(TopViewSourceProperty, value); }
        }

        public static DependencyProperty TopViewSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(TopViewSource), TopViewSourcePropertyChanged);

        private static void TopViewSourcePropertyChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.topView.ItemsSource = args.NewValue;
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
