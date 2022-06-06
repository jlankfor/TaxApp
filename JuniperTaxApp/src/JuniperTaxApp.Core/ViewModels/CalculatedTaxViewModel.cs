using System;
using JuniperTaxApp.Core.Models;
using MvvmCross.ViewModels;

namespace JuniperTaxApp.Core.ViewModels
{
    public class CalculatedTaxViewModel : MvxViewModel<CalculatedTaxAndRatesModel>
    {
        public CalculatedTaxAndRatesModel CalculatedTaxAndRatesModel { get; set; }
        public string Location { get; set; }
        public string CombinedDistrictRate { get; set; }
        public string CombinedRate { get; set; }
        public string CityRate { get; set; }
        public string CountyRate { get; set; }
        public string StateRate { get; set; }
        public string CountryRate { get; set; }

        public CalculatedTaxViewModel()
        {
        }

        public override void Prepare(CalculatedTaxAndRatesModel parameter)
        {
            CalculatedTaxAndRatesModel = parameter;
        }

        public void SetupProperties()
        {

        }
    }
}
