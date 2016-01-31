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
        public ListView UpperView => this.upperView;
        public ListView LowerView => this.lowerView;

        // Begin dependency property cruft

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

        // LowerItems:

        public ObservableList LowerItems =>
            this.GetValue<ObservableList>(LowerItemsProperty);

        public static DependencyProperty LowerItemsProperty { get; } =
            Dependency.Register<ObservableList, AppView>(nameof(LowerItems), LowerItemsPropertyChanged);

        private static void LowerItemsPropertyChanged(AppView o, IPropertyChangedArgs<ObservableList> args)
        {
            var src = args.NewValue;
            var dest = o.lowerView.Items;
            dest.ReplaceWith(src);
        }

        // LowerSource:

        public object LowerSource
        {
            get { return this.GetValue<object>(LowerSourceProperty); }
            set { this.SetValue(LowerSourceProperty, value); }
        }

        public static DependencyProperty LowerSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(LowerSource), LowerSourcePropertyChanged);

        private static void LowerSourcePropertyChanged(AppView o, IPropertyChangedArgs<object> args)
        {
            o.lowerView.ItemsSource = args.NewValue;
        }

        // UpperItems:

        public ObservableList UpperItems => 
            this.GetValue<ObservableList>(UpperItemsProperty);

        public static DependencyProperty UpperItemsProperty { get; } =
            Dependency.Register<ObservableList, AppView>(nameof(UpperItems), UpperItemsPropertyChanged);

        private static void UpperItemsPropertyChanged(AppView o, IPropertyChangedArgs<ObservableList> args)
        {
            var src = args.NewValue;
            var dest = o.upperView.Items;
            dest.ReplaceWith(src);
        }

        // UpperSource:

        public object UpperSource
        {
            get { return this.GetValue<object>(UpperSourceProperty); }
            set { this.SetValue(UpperSourceProperty, value); }
        }

        public static DependencyProperty UpperSourceProperty { get; } =
            Dependency.Register<object, AppView>(nameof(UpperSource), UpperSourcePropertyChanged);

        private static void UpperSourcePropertyChanged(AppView o, IPropertyChangedArgs<object> args)
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
