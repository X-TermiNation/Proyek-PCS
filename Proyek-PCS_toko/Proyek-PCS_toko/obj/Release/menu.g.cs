﻿#pragma checksum "..\..\menu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "37182FC64114F40389B7E6736F5F77971DE58417F5AF6708201F050D7A3F3FD9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Proyek_PCS_toko;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Proyek_PCS_toko {
    
    
    /// <summary>
    /// menu
    /// </summary>
    public partial class menu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnmasterbarang;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnmastertransaksi;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnreport;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnlogout;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Proyek-PCS_toko;component/menu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\menu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnmasterbarang = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\menu.xaml"
            this.btnmasterbarang.Click += new System.Windows.RoutedEventHandler(this.btnmasterbarang_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnmastertransaksi = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\menu.xaml"
            this.btnmastertransaksi.Click += new System.Windows.RoutedEventHandler(this.btnmastertransaksi_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnreport = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\menu.xaml"
            this.btnreport.Click += new System.Windows.RoutedEventHandler(this.btnreport_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnlogout = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\menu.xaml"
            this.btnlogout.Click += new System.Windows.RoutedEventHandler(this.btnlogout_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
