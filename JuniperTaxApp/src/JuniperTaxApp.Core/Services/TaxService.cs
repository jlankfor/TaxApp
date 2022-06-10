using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Enums;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;

namespace JuniperTaxApp.Core.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxClient _taxClient;
        private readonly ISecondTaxClient _secondTaxClient;

        public TaxService(ITaxClient taxClient, ISecondTaxClient secondTaxClient)
        {
            _taxClient = taxClient;
            _secondTaxClient = secondTaxClient;
        }

        public async Task<TaxRateModel> GetTaxRate(string zip, CustomerType customerType)
        {
            switch (customerType)
            {
                case CustomerType.BaseCustomer:
                    TaxRateDTO taxRate = await _taxClient.GetTaxRate(zip);
                    return new TaxRateModel(taxRate);
                case CustomerType.SecondCustomer:
                    TaxRateDTO secondTaxRate = await _secondTaxClient.GetTaxRate(zip);
                    return new TaxRateModel(secondTaxRate);
                default:
                    taxRate = await _taxClient.GetTaxRate(zip);
                    return new TaxRateModel(taxRate);
            }
        }


        public async Task<double> GetTaxAmount(TaxCalculationBodyDTO bodyDTO, CustomerType customerType)
        {
            switch (customerType)
            {
                case CustomerType.BaseCustomer:
                    CalculatedTaxDTO taxCalculated = await _taxClient.CalculateTax(bodyDTO);
                    return taxCalculated.Taxes.AmountToCollect;
                case CustomerType.SecondCustomer:
                    CalculatedTaxDTO secondTaxCalculated = await _secondTaxClient.CalculateTax(bodyDTO);
                    return secondTaxCalculated.Taxes.AmountToCollect;
                default:
                    taxCalculated = await _taxClient.CalculateTax(bodyDTO);
                    return taxCalculated.Taxes.AmountToCollect;
            }
        }
    }
}
