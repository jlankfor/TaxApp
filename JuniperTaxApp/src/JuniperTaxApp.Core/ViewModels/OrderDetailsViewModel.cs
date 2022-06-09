using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class OrderDetailsViewModel : BaseViewModel, INotifyPropertyChanged
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
        public string DesinationZIP
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                FromState = OriginState,
                ToCountry = "US",
                ToState = DestinationState,
                ToZip = DesinationZIP,
                Amount = Convert.ToDecimal(OrderAmount),
                Shipping = Convert.ToDecimal(ShippingAmount)
            };
        }
    }
}
