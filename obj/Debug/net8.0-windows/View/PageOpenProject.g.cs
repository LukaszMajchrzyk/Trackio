﻿#pragma checksum "..\..\..\..\View\PageOpenProject.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7D81F9410974EBDE8983D619F84D92E8EF7C96EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using Trackio.View;


namespace Trackio.View {
    
    
    /// <summary>
    /// PageOpenProject
    /// </summary>
    public partial class PageOpenProject : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\View\PageOpenProject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagridIDsAndProjectsNames;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\View\PageOpenProject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonOpen;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\View\PageOpenProject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonCancel;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\View\PageOpenProject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frameOpenProject;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Trackio;component/view/pageopenproject.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PageOpenProject.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.datagridIDsAndProjectsNames = ((System.Windows.Controls.DataGrid)(target));
            
            #line 12 "..\..\..\..\View\PageOpenProject.xaml"
            this.datagridIDsAndProjectsNames.AutoGeneratedColumns += new System.EventHandler(this.dataGridHeaderNames);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\View\PageOpenProject.xaml"
            this.datagridIDsAndProjectsNames.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonOpen = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\View\PageOpenProject.xaml"
            this.buttonOpen.Click += new System.Windows.RoutedEventHandler(this.buttonOpenClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\View\PageOpenProject.xaml"
            this.buttonCancel.Click += new System.Windows.RoutedEventHandler(this.buttonCancel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.frameOpenProject = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

