using AroundTheWorld.Prism.Models;
using Prism.Navigation;

namespace AroundTheWorld.Prism.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CountriesResponse _country;

        public CountryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Info of: ";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("country"))
            {
                _country = parameters.GetValue<CountriesResponse>("country");
                Title = _country.Name;
            }
        }
    }
}
