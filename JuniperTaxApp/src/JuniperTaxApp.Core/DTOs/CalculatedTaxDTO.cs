using System;
using Newtonsoft.Json;

namespace JuniperTaxApp.Core.DTOs
{
    public class CalculatedTaxDTO
    {
        [JsonProperty("tax")]
        public Tax Taxes { get; set; }

        public class Jurisdictions
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("county")]
            public string County { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }
        }

        public class Tax
        {
            [JsonProperty("amount_to_collect")]
            public double AmountToCollect { get; set; }

            [JsonProperty("freight_taxable")]
            public bool FreightTaxable { get; set; }

            [JsonProperty("has_nexus")]
            public bool HasNexus { get; set; }

            [JsonProperty("jurisdictions")]
            public Jurisdictions Jurisdictions { get; set; }

            [JsonProperty("order_total_amount")]
            public double OrderTotalAmount { get; set; }

            [JsonProperty("rate")]
            public double Rate { get; set; }

            [JsonProperty("shipping")]
            public double Shipping { get; set; }

            [JsonProperty("tax_source")]
            public string TaxSource { get; set; }

            [JsonProperty("taxable_amount")]
            public double TaxableAmount { get; set; }
        }


    }
}
