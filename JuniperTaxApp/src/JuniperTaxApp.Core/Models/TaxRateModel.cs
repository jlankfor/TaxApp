using System;
using JuniperTaxApp.Core.DTOs;

namespace JuniperTaxApp.Core.Models
{
    public class TaxRateModel
    {
        public string City { get; set; }

        public string CityRate { get; set; }

        public string CombinedDistrictRate { get; set; }

        public string CombinedRate { get; set; }

        public string Country { get; set; }

        public string CountryRate { get; set; }

        public string County { get; set; }

        public string CountyRate { get; set; }

        public bool FreightTaxable { get; set; }

        public string State { get; set; }

        public string StateRate { get; set; }

        public string Zip { get; set; }

        public TaxRateModel(TaxRateDTO taxRateDTO)
        {
            City = taxRateDTO.TaxRate.City;
            CityRate = taxRateDTO.TaxRate.CityRate;
            CombinedDistrictRate = taxRateDTO.TaxRate.CombinedDistrictRate;
            CombinedRate = taxRateDTO.TaxRate.CombinedRate;
            Country = taxRateDTO.TaxRate.Country;
            CountryRate = taxRateDTO.TaxRate.CountryRate;
            County = taxRateDTO.TaxRate.County;
            CountyRate = taxRateDTO.TaxRate.CountyRate;
            FreightTaxable = taxRateDTO.TaxRate.FreightTaxable;
            State = taxRateDTO.TaxRate.State;
            StateRate = taxRateDTO.TaxRate.StateRate;
            Zip = taxRateDTO.TaxRate.Zip;
        }
    }
}