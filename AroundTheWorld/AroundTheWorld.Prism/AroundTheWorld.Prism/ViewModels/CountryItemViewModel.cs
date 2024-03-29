﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using AroundTheWorld.Prism.Models;
using System;
using AroundTheWorld.Prism.Helpers;

namespace AroundTheWorld.Prism.ViewModels
{
    public class CountryItemViewModel : CountriesResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ?? (_selectCountryCommand = new DelegateCommand(SelectCountry));

        private async void SelectCountry()
        {
            var parameters = new NavigationParameters();
            parameters.Add("country", this);
            Settings.Country = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("CountryPage");
        }

    }
}
