﻿using Sirloin.Helpers;
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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Sirloin
{
    public sealed partial class AppView : UserControl
    {
        public AppView Current { get; private set; }

        public SplitView SplitView => this.splitView;
        public ListView TopView => this.topView;
        public ListView BottomView => this.bottomView;

        public AppView()
        {
            this.InitializeComponent();

            this.Loaded += (o, e) => Current = this;
        }
    }
}
