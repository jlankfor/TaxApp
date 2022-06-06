using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Interfaces;

namespace JuniperTaxApp.Core.Clients
{
    public class TaxClient : BaseClient, ITaxClient
    {
        private static readonly string BaseUrl = "https://api.taxjar.com/v2/";
        readonly string TaxResource = "taxes";
        readonly string RatesResource = "rates";

        public TaxClient() : base(BaseUrl)
        {
        }

        public Task<TaxRateDTO> GetTaxRate(string zip)
        {
            var route = BuildRoute(RatesResource, zip);
            return GetAsync<TaxRateDTO>(route);
        }

        public Task<CalculatedTaxDTO> CalculateTax(TaxCalculationBodyDTO taxCalculationBody)
        {
            var route = BuildRoute(TaxResource);
            return PostAsync<CalculatedTaxDTO>(route, taxCalculationBody);
        }
        
    }
}
