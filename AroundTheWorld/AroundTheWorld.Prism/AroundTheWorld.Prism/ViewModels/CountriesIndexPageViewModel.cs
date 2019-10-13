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
        private ObservableCollection<CountriesResponse> _countries;

        public CountriesIndexPageViewModel( INavigationService navigationService,
                                            IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Choose a Country!";

            LoadCountries();


        }

        public ObservableCollection<CountriesResponse> Countries
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
                Title = response.Message;

            }

            var list = (List<CountriesResponse>)response.Result;
            _countries = new ObservableCollection<CountriesResponse>(list);


        }

    }
}
