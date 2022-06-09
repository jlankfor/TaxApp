using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Enums;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.Resources;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JuniperTaxApp.Core.ViewModels
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        readonly ITaxService _taxService;

        public OrderDetailsViewModel(IMvxNavigationService mvxNavigationService, ITaxService taxService)
        {
            _mvxNavigationService = mvxNavigationService;
            _taxService = taxService;

            CalculateTaxes = new MvxAsyncCommand(CalculateTax);

            StatesList = Enum.GetNames(typeof(StateType)).ToList();
        }

        public IMvxCommand CalculateTaxes { get; private set; }

        public List<string> StatesList { get; set; }

        public string OrderTitle => StringResources.OrderDetailsTitle;
        public string Instructions => StringResources.GettingStarted;

        public string OriginZipHeader => StringResources.OriginZipCode;
        public string DestinationZipHeader => StringResources.DestinationZipCode;
        public string ZipHint => StringResources.ZipCodeHint;

        public string OriginStateHeader => StringResources.OriginState;
        public string DestinationStateHeader => StringResources.DestinationState;

        public string OrderAmountHeader => StringResources.OrderAmount;
        public string OrderAmountHint => StringResources.OrderAmountHint;

        public string ShippingAmountHeader => StringResources.ShippingAmount;
        public string ShippingAmountHint => StringResources.ShippingAmountHint;

        public string CaculateTaxButtonText => StringResources.CalculateButton;

        public string OriginZIP { get; set; }
        public string DesinationZIP { get; set; }

        public string OriginState { get; set; }
        public string DestinationState { get; set; }

        public double Amount { get; set; }
        public double ShippingAmount { get; set; }


        private async Task CalculateTax()
        {
            var rateTask = _taxService.GetTaxRate(OriginZIP);
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
                FromZip = OriginZIP,
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
