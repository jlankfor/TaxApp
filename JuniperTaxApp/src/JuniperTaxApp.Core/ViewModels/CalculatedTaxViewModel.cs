using System.Threading.Tasks;
using JuniperTaxApp.Core.Models;
using JuniperTaxApp.Core.Resources;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JuniperTaxApp.Core.ViewModels
{
    public class CalculatedTaxViewModel : BaseViewModel<CalculatedTaxAndRatesModel>
    {
        readonly IMvxNavigationService _mvxNavigationService;

        public override void Prepare(CalculatedTaxAndRatesModel parameter)
        {
            CalculatedTaxAndRatesModel = parameter;
            SetupProperties();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public CalculatedTaxViewModel(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }

        public CalculatedTaxAndRatesModel CalculatedTaxAndRatesModel { get; set; }

        public IMvxCommand TryNewOrder { get; private set; }

        public string PageTitle => StringResources.CalculatedTaxTitle;
        public string TryNewOrderButton => StringResources.TryNewOrderButton;
        public string Location { get; set; }
        public string CombinedDistrictRate { get; set; }
        public string CombinedRate { get; set; }
        public string CityRate { get; set; }
        public string CountyRate { get; set; }
        public string StateRate { get; set; }
        public string CountryRate { get; set; }
        public string TaxOwed { get; set; }
        public string OrderAmount { get; set; }
        public string OrderAmountWithTax { get; set; }

        public void SetupProperties()
        {
            TryNewOrder = new MvxAsyncCommand(SetupNewOrder);

            Location = $"{CalculatedTaxAndRatesModel.TaxRateModel.City}, " +
                $"{CalculatedTaxAndRatesModel.TaxRateModel.County}, " +
                $"{CalculatedTaxAndRatesModel.TaxRateModel.State}, " +
                $"{CalculatedTaxAndRatesModel.TaxRateModel.Zip}";

            CombinedDistrictRate = CalculatedTaxAndRatesModel.CombinedDistrictRate;
            CombinedRate = CalculatedTaxAndRatesModel.CombinedRate;
            CityRate = CalculatedTaxAndRatesModel.CityRate;
            CountyRate = CalculatedTaxAndRatesModel.CountyRate;
            StateRate = CalculatedTaxAndRatesModel.StateRate;
            CountryRate = CalculatedTaxAndRatesModel.CountryRate;
            OrderAmount = CalculatedTaxAndRatesModel.OrderTotalAmount;
            TaxOwed = CalculatedTaxAndRatesModel.TaxesDue;
            OrderAmountWithTax = CalculatedTaxAndRatesModel.OrderTotalWithTax;
        }

        private async Task SetupNewOrder()
        {
            await _mvxNavigationService.Close(this);
        }
    }
}
