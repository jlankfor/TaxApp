using System;
using Newtonsoft.Json;

namespace JuniperTaxApp.Core.DTOs
{
    public class TaxRateDTO
    {
        [JsonProperty("rate")]
        public Rate TaxRate { get; set; }

        public class Rate
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("city_rate")]
            public string CityRate { get; set; }

            [JsonProperty("combined_district_rate")]
            public string CombinedDistrictRate { get; set; }

            [JsonProperty("combined_rate")]
            public string CombinedRate { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("country_rate")]
            public string CountryRate { get; set; }

            [JsonProperty("county")]
            public string County { get; set; }

            [JsonProperty("county_rate")]
            public string CountyRate { get; set; }

            [JsonProperty("freight_taxable")]
            public bool FreightTaxable { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("state_rate")]
            public string StateRate { get; set; }

            [JsonProperty("zip")]
            public string Zip { get; set; }
        }
    }
}
