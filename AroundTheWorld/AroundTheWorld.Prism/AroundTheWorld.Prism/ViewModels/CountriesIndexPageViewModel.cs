using AroundTheWorld.Prism.Models;
using AroundTheWorld.Prism.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AroundTheWorld.Prism.ViewModels 
{
    public class CountriesIndexPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private ObservableCollection<CountryItemViewModel> _countries;

        public CountriesIndexPageViewModel( INavigationService navigationService,
                                            IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Choose a Country!";

            LoadCountries();


        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public async void LoadCountries() {

            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();

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

            }

            


        }

    }
}
