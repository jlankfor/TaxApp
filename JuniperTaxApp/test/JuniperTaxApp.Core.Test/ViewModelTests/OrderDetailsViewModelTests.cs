using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Acr.UserDialogs;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Enums;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.ViewModels;
using Moq;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using MvvmCross.Views;
using NUnit.Framework;

namespace JuniperTaxApp.Core.Test.ViewModelTests
{
    public class OrderDetailsViewModelTests : MvxIoCSupportingTest
    {
        private Mock<IMvxNavigationService> _mvxNavigationService;
        private Mock<ITaxService> _taxService;
        private Mock<IUserDialogs> _userDialogs;

        [Test]
        public void TestViewModel()
        {
            base.Setup();

            _mvxNavigationService = new Mock<IMvxNavigationService>();
            _taxService = new Mock<ITaxService>();
            _userDialogs = new Mock<IUserDialogs>();
            _userDialogs = new Mock<IUserDialogs>();

            var dto = new TaxCalculationBodyDTO { };
            _taxService.Setup(s => s.GetTaxAmount(dto, CustomerType.BaseCustomer)).ReturnsAsync(12.00);
            _taxService.Setup(s => s.GetTaxRate(It.IsAny<string>(), CustomerType.BaseCustomer)).ReturnsAsync(new TaxRateModel(BuildTaxRateDTO()));

            IDictionary<string, int> properties = new Dictionary<string, int>();
            PropertyChangedEventHandler handler = new PropertyChangedEventHandler((s, e) => {
                if (!properties.ContainsKey(e.PropertyName))
                    properties.Add(e.PropertyName, 0);

                properties[e.PropertyName]++;
            });

            var viewModel = new OrderDetailsViewModel(_mvxNavigationService.Object, _taxService.Object);
            viewModel.PropertyChanged += handler;

            Assert.AreEqual("Order Entry", viewModel.OrderTitle);
            Assert.AreEqual("To Start Enter Order details below then press the Calculate Tax button", viewModel.Instructions);
            Assert.AreEqual("Origin ZIP Code", viewModel.OriginZipHeader);
            Assert.AreEqual("Destination ZIP Code", viewModel.DestinationZipHeader);
            Assert.AreEqual("Enter a 5 Digit Zip Code", viewModel.ZipHint);
            Assert.AreEqual("Origin State", viewModel.OriginStateHeader);
            Assert.AreEqual("Destination State", viewModel.DestinationStateHeader);
            Assert.AreEqual("Select a State", viewModel.StateHint);
            Assert.AreEqual("Order Amount", viewModel.OrderAmountHeader);
            Assert.AreEqual("Enter Order Amount (ex: 65.15)", viewModel.OrderAmountHint);

            Assert.AreEqual("Shipping Amount", viewModel.ShippingAmountHeader);
            Assert.AreEqual("Enter Shipping Amount (ex: 10.00)", viewModel.ShippingAmountHint);
            Assert.AreEqual("Calculate Tax", viewModel.CaculateTaxButtonText);

            SetParameters(viewModel);
            viewModel.CalculateTaxes.Execute(null);

            _mvxNavigationService.Verify(s => s.Navigate<CalculatedTaxViewModel, CalculatedTaxAndRatesModel>
                                  (It.IsAny<CalculatedTaxAndRatesModel>(),
                                  null,
                                   It.IsAny<CancellationToken>()));
        }

        private void SetParameters(OrderDetailsViewModel viewModel)
        {
            viewModel.OriginZIP = "12345";
            viewModel.DestinationZIP = "12345";
            viewModel.OriginState = "CO";
            viewModel.DestinationState = "CO";
            viewModel.OrderAmount = "10.00";
            viewModel.ShippingAmount = "10.00";
        }

        protected override void AdditionalSetup()
        {
            //this.mockDispatcher = new MockDispatcher();
            //this.Ioc.RegisterSingleton<IMvxViewDispatcher>(this.mockDispatcher);
            //this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(this.mockDispatcher);
        }

        private TaxRateDTO BuildTaxRateDTO()
        {
            return new TaxRateDTO()
            {
                TaxRate = new TaxRateDTO.Rate
                {
                    City = "Denver",
                    CityRate = "0.1",
                    CombinedDistrictRate = "0.1",
                    CombinedRate = "0.1",
                    Country = "US",
                    CountryRate = "0.1",
                    County = "Denver",
                    CountyRate = "0.1",
                    FreightTaxable = false,
                    State = "CO",
                    StateRate = "0.1",
                    Zip = "12345"
                }
            };
        }
    }
}
