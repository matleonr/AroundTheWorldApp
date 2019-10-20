﻿using System;
using AroundTheWorld.Prism.Helpers;
using AroundTheWorld.Prism.Models;
using AroundTheWorld.Prism.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AroundTheWorld.Prism.Views
{
    public partial class CountryPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;

        public CountryPage(
            IGeolocatorService geolocatorService)
        {
            InitializeComponent();
            _geolocatorService = geolocatorService;
            MoveMapToCurrentCountryAsync();


        }

        private async void MoveMapToCurrentCountryAsync()
        {
            var country = JsonConvert.DeserializeObject<CountriesResponse>(Settings.Country);
            var position = new Position(
                country.Latlng[0],
                country.Latlng[1]);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                position,
                Distance.FromKilometers(100)));
        }
    }
}
