using System;
namespace JuniperTaxApp.Core.Models
{
    public class CalculatedTaxAndRatesModel
    {
        public double TaxAmount { get; set; }
        public TaxRateModel TaxRateModel { get; set; }

        public CalculatedTaxAndRatesModel(double taxAmount, TaxRateModel taxRateModel)
        {
            TaxAmount = taxAmount;
            TaxRateModel = taxRateModel;
        }
    }
}
