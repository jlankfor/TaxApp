using System;
using System.Collections.Generic;
using JuniperTaxApp.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuniperTaxApp.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class CalculatedTaxPage : MvxContentPage<CalculatedTaxViewModel>
    {
        public CalculatedTaxPage()
        {
            InitializeComponent();
        }
    }
}
