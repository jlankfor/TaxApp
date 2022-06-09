using System;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.Resources;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace JuniperTaxApp.Core.ViewModels
{
    public class CalculatedTaxViewModel : MvxViewModel<CalculatedTaxAndRatesModel>
    {
        public CalculatedTaxViewModel()
        {
            SetupProperties();
        }

        public override void Prepare(CalculatedTaxAndRatesModel parameter)
        {
            CalculatedTaxAndRatesModel = parameter;
        }

        public CalculatedTaxAndRatesModel CalculatedTaxAndRatesModel { get; set; }

        public IMvxCommand TryNewOrder { get; private set; }

        public string PageTitle => StringResources.CalculatedTaxTitle;
        public string Location { get; set; }
        public string CombinedDistrictRate { get; set; }
        public string CombinedRate { get; set; }
        public string CityRate { get; set; }
        public string CountyRate { get; set; }
        public string StateRate { get; set; }
        public string CountryRate { get; set; }
        public string TaxOwed { get; set; }

        public void SetupProperties()
        {
            CombinedDistrictRate = string.Format(StringResources.CombinedDistrictRate, CalculatedTaxAndRatesModel.CombinedDistrictRate);
            CombinedRate = string.Format(StringResources.CombinedRate, CalculatedTaxAndRatesModel.CombinedRate);
            CityRate = string.Format(StringResources.CityRate, CalculatedTaxAndRatesModel.CityRate);
            CountyRate = string.Format(StringResources.CountyRate, CalculatedTaxAndRatesModel.CountyRate);
            StateRate = string.Format(StringResources.StateRate, CalculatedTaxAndRatesModel.StateRate);
            CountryRate = string.Format(StringResources.CountryRate, CalculatedTaxAndRatesModel.CountryRate);
            TaxOwed = string.Format(StringResources.TaxesDue, CalculatedTaxAndRatesModel.TaxesDue);
        }
    }
}
