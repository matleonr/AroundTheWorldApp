using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AroundTheWorld.Prism.Helpers
{
    public class Settings
    {
        private const string _country = "Country";
        private const string _token = "Token";
        private const string _countries = "Countries";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Country
        {
            get => AppSettings.GetValueOrDefault(_country, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_country, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Countries
        {
            get => AppSettings.GetValueOrDefault(_countries, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_countries, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }
}
