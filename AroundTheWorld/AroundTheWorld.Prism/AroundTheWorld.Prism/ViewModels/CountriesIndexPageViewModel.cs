using AroundTheWorld.Prism.Helpers;
using AroundTheWorld.Prism.Models;
using AroundTheWorld.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AroundTheWorld.Prism.ViewModels
{
    public class CountriesIndexPageViewModel : ViewModelBase//, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private ObservableCollection<CountryItemViewModel> _countries;
        private List<Sorteable> _sorteables;
        private Sorteable _sorteable;


        public CountriesIndexPageViewModel(INavigationService navigationService,
                                            IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Choose a Country!";

            LoadCountries();
            Sorteables = SorteablesFilled();

        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set
            {
                SetProperty(ref _countries, value);
                //if (Sorteable.Key == 2)
                //{
                //    value.OrderByDescending(c => c.Area);
                //}
            }
        }

        public List<Sorteable> Sorteables
        {
            get => _sorteables;
            set => SetProperty(ref _sorteables, value);
        }

        public Sorteable Sorteable
        {
            get => _sorteable;
            set
            { 
                SetProperty(ref _sorteable, value);

                if (value.Key == 1)
                {
                    Countries = new ObservableCollection<CountryItemViewModel>(Countries.OrderBy(c => c.Name));
                }
                if (value.Key == 2)
                {
                    Countries = new ObservableCollection<CountryItemViewModel>(Countries.OrderByDescending(c => c.Area));
                }
                if (value.Key == 3)
                {
                    Countries = new ObservableCollection<CountryItemViewModel>(Countries.OrderByDescending(c => c.Population));
                }
                
                //Countries = Countries.OrderByDescending(c => c.Area);

            }
        }

        public List<Sorteable> SorteablesFilled()
        {
            var sorteables = new List<Sorteable>()
            {
                new Sorteable(){Key = 1, Value = "name"},
                new Sorteable(){Key = 2, Value = "Size"},
                new Sorteable(){Key = 3, Value = "Population"}
            };

            return sorteables;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadCountries() {

            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection();
            if (!connection)
            {
                IsRunning = false;
                if (string.IsNullOrEmpty(Settings.Countries))
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "You need access to internet to first load the countries", "Accept");
                    return;
                }
                //Countries = JsonConvert.DeserializeObject<ObservableCollection<CountryItemViewModel>>(Settings.Countries);
                return;


            }

            var response = await _apiService.GetCountriesInfoAsync<CountriesResponse>(url);

            if (response.IsSuccess)
            {
                IsRunning = false;

                var list = (List<CountriesResponse>)response.Result;
                
                Countries = new ObservableCollection<CountryItemViewModel>(list.Select(c => new CountryItemViewModel(_navigationService)
                {
                    Name = c.Name,
                    TopLevelDomain = c.TopLevelDomain,
                    NativeName = c.NativeName,
                    NumericCode = c.NumericCode,
                    Alpha2Code = c.Alpha2Code,
                    Alpha3Code = c.Alpha3Code,
                    Languages = c.Languages,
                    Latlng = c.Latlng,
                    CallingCodes = c.CallingCodes,
                    Capital = c.Capital,
                    AltSpellings = c.AltSpellings,
                    Area = c.Area,
                    Region = c.Region,
                    Subregion = c.Subregion,
                    RegionalBlocs = c.RegionalBlocs,
                    Population = c.Population,
                    Demonym = c.Demonym,
                    Gini = c.Gini,
                    Timezones = c.Timezones,
                    Borders = c.Borders,
                    Currencies = c.Currencies,
                    Translations = c.Translations,
                    Flag = c.Flag,
                    Cioc = c.Cioc

                }));
                Settings.Countries = JsonConvert.SerializeObject(Countries);

            }

        }

    }

    public class Sorteable
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}