// Updated by XamlIntelliSenseFileGenerator 23.3.2024. 20:52:38
#pragma checksum "..\..\..\Pages\EditDriverPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5517E937D42C79BB15BF207427BFB47159017D3943392DF6206119BCC45A8BDA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Content_Management_System.Pages;
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


namespace Content_Management_System.Pages
{


    /// <summary>
    /// EditDriverPage
    /// </summary>
    public partial class EditDriverPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Content Management System;component/pages/editdriverpage.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Pages\EditDriverPage.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button BackButton;
        internal System.Windows.Controls.Primitives.ToggleButton BoldToggleButton;
        internal System.Windows.Controls.Primitives.ToggleButton ItalicToggleButton;
        internal System.Windows.Controls.Primitives.ToggleButton UnderlineToggleButton;
        internal System.Windows.Controls.ComboBox FontFamilyComboBox;
        internal System.Windows.Controls.ComboBox FontSizeComboBox;
        internal System.Windows.Shapes.Rectangle ColorPreviewRectangle;
        internal System.Windows.Controls.ComboBox FontColorComboBox;
        internal System.Windows.Controls.TextBox DriverNameTextBox;
        internal System.Windows.Controls.Label DriverNameErrorLabel;
        internal System.Windows.Controls.TextBox DriverNumberTextBox;
        internal System.Windows.Controls.Label DriverNumberErrorLabel;
        internal System.Windows.Controls.Button SelectPictureButton;
        internal System.Windows.Controls.Label DriverPictureErrorLabel;
        internal System.Windows.Media.ImageBrush PreviewPictureBrush;
        internal System.Windows.Controls.RichTextBox DriverDescriptionRichTextBox;
        internal System.Windows.Controls.Label DescriptionRichTextBoxError;
        internal System.Windows.Controls.Label CharacterCounterLabel;
        internal System.Windows.Controls.Button AddDriverButton;
    }
}

