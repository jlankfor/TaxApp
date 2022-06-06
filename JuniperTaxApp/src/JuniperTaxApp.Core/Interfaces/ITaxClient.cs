using System;
using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;

namespace JuniperTaxApp.Core.Interfaces
{
    public interface ITaxClient
    {
        Task<TaxRateDTO> GetTaxRate(string zip);
        Task<CalculatedTaxDTO> CalculateTax(TaxCalculationBodyDTO taxCalculationBody);
    }
}
