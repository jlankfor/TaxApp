using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JuniperTaxApp.Core.ViewModels
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        readonly ITaxService _taxService;

        public IMvxCommand DestroyPlanetCommand { get; private set; }

        public string ZIP { get; set; }
        public double Amount { get; set; }
        public double ShippingAmount { get; set; }

        public OrderDetailsViewModel(IMvxNavigationService mvxNavigationService, ITaxService taxService)
        {
            _mvxNavigationService = mvxNavigationService;
            _taxService = taxService;

            DestroyPlanetCommand = new MvxAsyncCommand(CalculateTax);
        }

        private async Task CalculateTax()
        {
            var rateTask = _taxService.GetTaxRate(ZIP);
            var taxAmountTask = _taxService.GetTaxAmount(CreateTaxAmountBody());

            await Task.WhenAll(rateTask, taxAmountTask);

            var taxesAndRatesModel = new CalculatedTaxAndRatesModel(taxAmountTask.Result, rateTask.Result);

            await _mvxNavigationService.Navigate<CalculatedTaxViewModel, CalculatedTaxAndRatesModel>(taxesAndRatesModel);
        }

        private TaxCalculationBodyDTO CreateTaxAmountBody()
        {
            return new TaxCalculationBodyDTO
            {
                FromCountry = "US",
                FromZip = ZIP,
                FromState = "",
                ToCountry = "US",
                ToState = "",
                ToZip = "",
                Amount = Amount,
                Shipping = ShippingAmount
            };
        }
    }
}
