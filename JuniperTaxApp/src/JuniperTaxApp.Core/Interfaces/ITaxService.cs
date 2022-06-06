using System;
using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Models;

namespace JuniperTaxApp.Core.Interfaces
{
    public interface ITaxService
    {
        Task<TaxRateModel> GetTaxRate(string zip);
        Task<double> GetTaxAmount(TaxCalculationBodyDTO bodyDTO);
    }
}
