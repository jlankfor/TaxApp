using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;

namespace JuniperTaxApp.Core.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxClient _taxClient;

        public TaxService(ITaxClient taxClient)
        {
            _taxClient = taxClient;
        }

        public async Task<TaxRateModel> GetTaxRate(string zip)
        {
            TaxRateDTO taxRate = await _taxClient.GetTaxRate(zip);
            return new TaxRateModel(taxRate);
        }


        public async Task<double> GetTaxAmount(TaxCalculationBodyDTO bodyDTO)
        {
            CalculatedTaxDTO taxCalculated = await _taxClient.CalculateTax(bodyDTO);
            return taxCalculated.Taxes.AmountToCollect;
        }

    }
}
