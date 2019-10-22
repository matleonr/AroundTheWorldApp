using System.Collections.ObjectModel;
using AroundTheWorld.Prism.Helpers;
using AroundTheWorld.Prism.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace AroundTheWorld.Prism.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CountriesResponse _country;
        private ObservableCollection<Language> _languages;
        private Translations _translations;
        private ObservableCollection<Currency> _currencies;
        private ObservableCollection<RegionalBloc> _regionalBlocs;

        public CountryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            var country = JsonConvert.DeserializeObject<CountriesResponse>(Settings.Country);
            Country = country;
            Title = country.Name;
            Translations = new Translations();
            Translations = country.Translations;
            Languages = new ObservableCollection<Language>(country.Languages);
            Currencies = new ObservableCollection<Currency>(country.Currencies);
            RegionalBlocs = new ObservableCollection<RegionalBloc>(country.RegionalBlocs);
        }

        public CountriesResponse Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public Translations Translations
        {
            get => _translations;
            set => SetProperty(ref _translations, value);
        }

        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        public ObservableCollection<RegionalBloc> RegionalBlocs
        {
            get => _regionalBlocs;
            set => SetProperty(ref _regionalBlocs, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //if (parameters.ContainsKey("country"))
            //{
            //    Country = parameters.GetValue<CountriesResponse>("country");
            //    Title = Country.Name;
            //    Translations = new Translations();
            //    Translations = Country.Translations;
            //    Languages = new ObservableCollection<Language>(Country.Languages);
            //}
        }
    }
}
