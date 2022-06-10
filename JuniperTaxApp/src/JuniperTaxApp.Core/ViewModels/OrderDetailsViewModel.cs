using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
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
        private const int ZipLength = 5;

        public OrderDetailsViewModel(IMvxNavigationService mvxNavigationService, ITaxService taxService)
        {
            _mvxNavigationService = mvxNavigationService;
            _taxService = taxService;

            CalculateTaxes = new MvxAsyncCommand(CalculateTax);

            StatesList = Enum.GetNames(typeof(StateType)).ToList();
        }

        #region Properties

        public IMvxCommand CalculateTaxes { get; private set; }

        public List<string> StatesList { get; set; }

        public string OrderTitle => StringResources.OrderDetailsTitle;
        public string Instructions => StringResources.GettingStarted;

        public string OriginZipHeader => StringResources.OriginZipCode;
        public string DestinationZipHeader => StringResources.DestinationZipCode;
        public string ZipHint => StringResources.ZipCodeHint;

        public string OriginStateHeader => StringResources.OriginState;
        public string DestinationStateHeader => StringResources.DestinationState;
        public string StateHint => StringResources.SelectStateHint;

        public string OrderAmountHeader => StringResources.OrderAmount;
        public string OrderAmountHint => StringResources.OrderAmountHint;

        public string ShippingAmountHeader => StringResources.ShippingAmount;
        public string ShippingAmountHint => StringResources.ShippingAmountHint;

        public string CaculateTaxButtonText => StringResources.CalculateButton;

        private string _originZip;
        public string OriginZIP
        {
            get => _originZip;
            set
            {
                if(_originZip != value)
                {
                    _originZip = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _destinationZip;
        public string DestinationZIP
        {
            get => _destinationZip;
            set
            {
                if (_destinationZip != value)
                {
                    _destinationZip = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _originState;
        public string OriginState
        {
            get => _originState;
            set
            {
                if (_originState != value)
                {
                    _originState = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _destinationState;
        public string DestinationState
        {
            get => _destinationState;
            set
            {
                if (_destinationState != value)
                {
                    _destinationState = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _orderAmount;
        public string OrderAmount
        {
            get => _orderAmount;
            set
            {
                if (_orderAmount != value)
                {
                    _orderAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _shippingAmount;
        public string ShippingAmount
        {
            get => _shippingAmount;
            set
            {
                if (_shippingAmount != value)
                {
                    _shippingAmount = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        private async Task CalculateTax()
        {
            var areInputFieldsValid = ValidateInputs();

            if(areInputFieldsValid)
            {
                var rateTask = _taxService.GetTaxRate(OriginZIP, CustomerType.BaseCustomer);
                var taxAmountTask = _taxService.GetTaxAmount(CreateTaxAmountBody(), CustomerType.BaseCustomer);

                await Task.WhenAll(rateTask, taxAmountTask);

                var taxesAndRatesModel = new CalculatedTaxAndRatesModel(taxAmountTask.Result, rateTask.Result);
                ClearFields();

                await _mvxNavigationService.Navigate<CalculatedTaxViewModel, CalculatedTaxAndRatesModel>(taxesAndRatesModel);
            }
            else
            {
                UserDialogs.Instance.Alert(StringResources.InvalidFormDetails, StringResources.InvalidFormHeader, StringResources.OKButton);
            }
        }

        private TaxCalculationBodyDTO CreateTaxAmountBody()
        {
            return new TaxCalculationBodyDTO
            {
                FromCountry = "US",
                FromZip = OriginZIP,
                FromState = OriginState,
                ToCountry = "US",
                ToState = DestinationState,
                ToZip = DestinationZIP,
                Amount = Convert.ToDecimal(OrderAmount),
                Shipping = Convert.ToDecimal(ShippingAmount)
            };
        }

        private bool ValidateInputs()
        {
            var isValidOriginZip = int.TryParse(OriginZIP, out _) && OriginZIP.Length == ZipLength;
            var isValidDestinationZip = int.TryParse(OriginZIP, out _) && DestinationZIP.Length == ZipLength;
            var isValidOrderAmount = decimal.TryParse(OrderAmount, out _);
            var isValidShippingAmount = decimal.TryParse(ShippingAmount, out _);

            // alternitively could check index changed
            var isValidOriginState = !string.IsNullOrWhiteSpace(OriginState);
            var isValidDestinationState = !string.IsNullOrWhiteSpace(DestinationState);

            return isValidOriginZip && isValidDestinationZip && isValidOrderAmount && isValidShippingAmount && isValidOriginState && isValidDestinationState;
        }

        private void ClearFields()
        {
            OriginZIP = string.Empty;
            DestinationZIP = string.Empty;
            OrderAmount = string.Empty;
            ShippingAmount = string.Empty;
        }
    }
}
