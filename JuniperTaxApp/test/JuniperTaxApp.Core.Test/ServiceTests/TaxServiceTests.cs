using System;
using System.Threading.Tasks;
using JuniperTaxApp.Core.DTOs;
using JuniperTaxApp.Core.Enums;
using JuniperTaxApp.Core.Interfaces;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.Services;
using Moq;
using MvvmCross.Tests;
using NUnit.Framework;

namespace JuniperTaxApp.Core.Test.ServiceTests
{
    public class TaxServiceTests : MvxIoCSupportingTest
    {
        private Mock<ITaxClient> _taxClient;
        private Mock<ISecondTaxClient> _secondTaxClient;

        [Test]
        public async Task TestTaxService()
        {
            base.Setup();

            _taxClient = new Mock<ITaxClient>();
            _secondTaxClient = new Mock<ISecondTaxClient>();

            var taxRateDTO = BuildTaxRateDTO();
            var taxBody = BuildTaxBody();
            var calculatedTaxDTO = CalculatedTaxDTO();

            _taxClient.Setup(s => s.GetTaxRate(It.IsAny<string>())).ReturnsAsync(taxRateDTO);
            _taxClient.Setup(s => s.CalculateTax(taxBody)).ReturnsAsync(calculatedTaxDTO);
            // would set up other calculators here

            var taxService = new TaxService(_taxClient.Object, _secondTaxClient.Object);

            var taxRate = await taxService.GetTaxRate("12345", CustomerType.BaseCustomer);
            _taxClient.Verify(c => c.GetTaxRate(It.IsAny<string>()), Times.Once());

            var taxResult = new TaxRateModel(taxRateDTO);
            // some conversion from dto to a model is happening here so not the same object so testing state
            Assert.AreEqual(taxResult.State, taxRate.State);

            var taxAmount = await taxService.GetTaxAmount(taxBody, CustomerType.BaseCustomer);
            _taxClient.Verify(c => c.CalculateTax(taxBody), Times.Once());

            Assert.AreEqual(1.00, taxAmount);
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

        private TaxCalculationBodyDTO BuildTaxBody()
        {
            return new TaxCalculationBodyDTO()
            {
                FromCountry = "US",
                FromZip = "12345",
                FromState = "CO",
                ToCountry = "US",
                ToState = "CO",
                ToZip = "80111",
                Amount = 10.00m,
                Shipping = 1.00m
            };
        }

        private CalculatedTaxDTO CalculatedTaxDTO()
        {
            return new CalculatedTaxDTO()
            {
                Taxes = new CalculatedTaxDTO.Tax
                {
                    AmountToCollect = 1.00,
                    FreightTaxable = false,
                    HasNexus = false,
                    OrderTotalAmount = 10.00,
                    Rate = 0,
                    Shipping = 1.00,
                    TaxSource = "",
                    TaxableAmount = 10.00,
                    Jurisdictions = new CalculatedTaxDTO.Jurisdictions
                    {
                        City = "Denver",
                        Country = "US",
                        County = "Denver",
                        State = "CO"
                    }
                }
            };
        }
    }
}
