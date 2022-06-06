using System;
using JuniperTaxApp.Core.Resources;

namespace JuniperTaxApp.Core.Models
{
    public class CalculatedTaxAndRatesModel
    {
        public double TaxAmount { get; set; }

        public TaxRateModel TaxRateModel { get; set; }

        public static string CityRate { get; set; }

        public static string CombinedDistrictRate { get; set; }

        public static string CombinedRate { get; set; }

        public static string CountryRate { get; set; }

        public static string CountyRate { get; set; }

        public static string StateRate { get; set; }

        public static string TaxesDue { get; set; }

        public CalculatedTaxAndRatesModel(double taxAmount, TaxRateModel taxRateModel)
        {
            TaxAmount = taxAmount;
            TaxRateModel = taxRateModel;

            SetupProperties();
        }

        private void SetupProperties()
        {
            CityRate = string.Format(StringResources.CityRate, TaxRateModel.CityRate);
            CombinedDistrictRate = string.Format(StringResources.CombinedDistrictRate, TaxRateModel.CombinedDistrictRate);
            CombinedRate = string.Format(StringResources.CombinedRate, TaxRateModel.CombinedRate);
            CountryRate = string.Format(StringResources.CountryRate, TaxRateModel.CountryRate);
            CountyRate = string.Format(StringResources.CountyRate, TaxRateModel.CountyRate);
            StateRate = string.Format(StringResources.StateRate, TaxRateModel.StateRate);
            TaxesDue = string.Format(StringResources.TaxesDue, TaxAmount);
        }
    }
}
