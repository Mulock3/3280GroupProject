﻿#pragma checksum "..\..\..\Main\wndMain.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A3B9C177A3EBAF52F3F7D5B50182FE3A8BF3E7DC1BEF3FEFA48DDBE639304E06"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GroupProject3280.Main;
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


namespace GroupProject3280.Main {
    
    
    /// <summary>
    /// wndMain
    /// </summary>
    public partial class wndMain : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miSearch;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miNew;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miEdit;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miDelete;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miUpdate;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblInvoiceNumber;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpInvoiceDate;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboItems;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgInvoiceItems;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemove;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTotal;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCost;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblInvoiceDateError;
        
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
            System.Uri resourceLocater = new System.Uri("/GroupProject3280;component/main/wndmain.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Main\wndMain.xaml"
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
            
            #line 8 "..\..\..\Main\wndMain.xaml"
            ((GroupProject3280.Main.wndMain)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.miSearch = ((System.Windows.Controls.MenuItem)(target));
            
            #line 21 "..\..\..\Main\wndMain.xaml"
            this.miSearch.Click += new System.Windows.RoutedEventHandler(this.InvoiceSearchMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.miNew = ((System.Windows.Controls.MenuItem)(target));
            
            #line 23 "..\..\..\Main\wndMain.xaml"
            this.miNew.Click += new System.Windows.RoutedEventHandler(this.InvoiceNewMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.miEdit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 24 "..\..\..\Main\wndMain.xaml"
            this.miEdit.Click += new System.Windows.RoutedEventHandler(this.InvoiceEditMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.miDelete = ((System.Windows.Controls.MenuItem)(target));
            
            #line 25 "..\..\..\Main\wndMain.xaml"
            this.miDelete.Click += new System.Windows.RoutedEventHandler(this.InvoiceDeletMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.miUpdate = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\..\Main\wndMain.xaml"
            this.miUpdate.Click += new System.Windows.RoutedEventHandler(this.ItemsUpdateMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblInvoiceNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.dpInvoiceDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 35 "..\..\..\Main\wndMain.xaml"
            this.dpInvoiceDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpInvoiceDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cboItems = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\..\Main\wndMain.xaml"
            this.cboItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboItems_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgInvoiceItems = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Main\wndMain.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnRemove = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Main\wndMain.xaml"
            this.btnRemove.Click += new System.Windows.RoutedEventHandler(this.btnRemove_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.lblTotal = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Main\wndMain.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\Main\wndMain.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.lblCost = ((System.Windows.Controls.Label)(target));
            return;
            case 17:
            this.lblInvoiceDateError = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

