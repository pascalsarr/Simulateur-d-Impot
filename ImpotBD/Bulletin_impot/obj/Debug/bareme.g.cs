#pragma checksum "..\..\bareme.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F3683BF9C8E6B9D39092D9ACB435AC4D58151FC3236D1C37C74FAD883ECF13A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Bulletin_impot;
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


namespace Bulletin_impot {
    
    
    /// <summary>
    /// bareme
    /// </summary>
    public partial class bareme : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\bareme.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editeur;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\bareme.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button boutonOuvrir;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\bareme.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button boutonEnregistrer;
        
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
            System.Uri resourceLocater = new System.Uri("/Bulletin_impot;component/bareme.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\bareme.xaml"
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
            this.editeur = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\bareme.xaml"
            this.editeur.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Editeur_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.boutonOuvrir = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\bareme.xaml"
            this.boutonOuvrir.Click += new System.Windows.RoutedEventHandler(this.BoutonOuvrir_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.boutonEnregistrer = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\bareme.xaml"
            this.boutonEnregistrer.Click += new System.Windows.RoutedEventHandler(this.BoutonEnregistrer_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

