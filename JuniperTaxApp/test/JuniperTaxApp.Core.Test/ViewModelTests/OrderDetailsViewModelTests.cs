using System.Threading;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.ViewModels;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;

namespace JuniperTaxApp.Core.Test.ViewModelTests
{
    public class OrderDetailsViewModelTests : MvxIoCSupportingTest
    {
        private Mock<IMvxNavigationService> _mvxNavigationService;
        private Mock<ITaxService> _taxService;

        [Test]
        public void TestViewModel()
        {
            base.Setup();

            // todo setup
            _mvxNavigationService = new Mock<IMvxNavigationService>();
            _taxService = new Mock<ITaxService>();

            var dto = new TaxCalculationBodyDTO { };
            _taxService.Setup(s => s.GetTaxAmount(dto)).ReturnsAsync(12.00);
            _taxService.Setup(s => s.GetTaxRate(It.IsAny<string>())).ReturnsAsync(new Models.TaxRateModel(new TaxRateDTO { }));

            var viewModel = new OrderDetailsViewModel(_mvxNavigationService.Object, _taxService.Object);

            Assert.AreEqual("Order Entry", viewModel.OrderTitle);
            Assert.AreEqual("To Start Enter Order details below then press the Calculate Tax button", viewModel.Instructions);
            Assert.AreEqual("Origin ZIP Code", viewModel.OriginZipHeader);
            Assert.AreEqual("Destination ZIP Code", viewModel.DestinationZipHeader);
            Assert.AreEqual("Enter a 5 Digit Zip Code", viewModel.ZipHint);
            Assert.AreEqual("Origin State", viewModel.OriginStateHeader);
            Assert.AreEqual("Destination State", viewModel.DestinationStateHeader);
            Assert.AreEqual("Select a State", viewModel.StateHint);
            Assert.AreEqual("Order Amount", viewModel.OrderAmountHeader);
            Assert.AreEqual("Enter Order Amount", viewModel.OrderAmountHint);

            Assert.AreEqual("Shipping Amount", viewModel.ShippingAmountHeader);
            Assert.AreEqual("Enter Shipping Amount", viewModel.ShippingAmountHint);
            Assert.AreEqual("Calculate Tax", viewModel.CaculateTaxButtonText);

            viewModel.CalculateTaxes.Execute(null);

            _mvxNavigationService.Verify(s => s.Navigate<CalculatedTaxViewModel, CalculatedTaxAndRatesModel>
                                  (It.IsAny<CalculatedTaxAndRatesModel>(),
                                  null,
                                   It.IsAny<CancellationToken>()));
        }
    }
}
