using System;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.ViewModels;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;

namespace JuniperTaxApp.Core.Test.ViewModelTests
{
    [TestFixture]
    public class CalculatedTaxViewModelTests : MvxIoCSupportingTest
    {
        private Mock<IMvxNavigationService> _mvxNavigationService;

        [Test]
        public void TestViewModel()
        {
            base.Setup(); 

            var taxRateModel = new TaxRateModel(new DTOs.TaxRateDTO
            {
                TaxRate = new DTOs.TaxRateDTO.Rate
                {
                    City = "Denver",
                    CityRate = "0",
                    CombinedDistrictRate = "0",
                    CombinedRate = "0",
                    Country = "US",
                    CountryRate = "0",
                    County = "Denver County",
                    CountyRate = "0",
                    FreightTaxable = false,
                    State = "CO",
                    StateRate = "0",
                    Zip = "12345"
                }
            });

            var viewModel = new CalculatedTaxViewModel(_mvxNavigationService.Object);
            viewModel.CalculatedTaxAndRatesModel = new Models.CalculatedTaxAndRatesModel(10.00, taxRateModel);


            Assert.AreEqual("Tax Amount and Rates", viewModel.PageTitle);
            Assert.AreEqual("Try New Order", viewModel.TryNewOrderButton);
            Assert.AreEqual("", viewModel.Location);
            Assert.AreEqual("Combined District Rate: {0}", viewModel.CombinedDistrictRate);
            Assert.AreEqual("Combined Rate: {0}", viewModel.CombinedRate);
            Assert.AreEqual("City Rate: {0}", viewModel.CityRate);
            Assert.AreEqual("County Rate: {0}", viewModel.CountyRate);
            Assert.AreEqual("State Rate: {0}", viewModel.StateRate);
            Assert.AreEqual("Country Rate: {0}", viewModel.CountryRate);
            Assert.AreEqual("Taxes Due: ${0}", viewModel.TaxOwed);

            viewModel.TryNewOrder.Execute(null);
            // test navigation
        }
    }
}