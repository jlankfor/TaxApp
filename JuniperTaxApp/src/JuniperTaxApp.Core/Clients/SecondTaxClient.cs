using System;
using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Interfaces;

namespace JuniperTaxApp.Core.Clients
{
    public class SecondTaxClient : BaseClient, ISecondTaxClient
    {
        // purposefully not implemented, placeholder tax calculator

        public SecondTaxClient() : base("someOtherAddress")
        {
        }

        public Task<CalculatedTaxDTO> CalculateTax(TaxCalculationBodyDTO taxCalculationBody)
        {
            // purposefully not implemented, placeholder tax calculator

            throw new NotImplementedException();
        }
        public Task<TaxRateDTO> GetTaxRate(string zip)
        {
            // purposefully not implemented, placeholder tax calculator

            throw new NotImplementedException();
        }
    }
}
