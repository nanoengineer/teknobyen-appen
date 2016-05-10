using System;

namespace Teknobyen.Services.SettingsService
{
    class SettingsService : ISettingsService
    {
        public static SettingsService Instance { get; }
        static SettingsService()
        {
            // implement singleton pattern
            Instance = Instance ?? new SettingsService();
        }

        Template10.Services.SettingsService.ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public int RoomNumber
        {
            get
            {
                return _helper.Read<int>(nameof(RoomNumber), 000, Template10.Services.SettingsService.SettingsStrategies.Roam);
            }

            set
            {
                _helper.Write<int>(nameof(RoomNumber), value, Template10.Services.SettingsService.SettingsStrategies.Roam);
            }
        }

        public string Name
        {
            get
            {
                return _helper.Read<string>(nameof(Name), "", Template10.Services.SettingsService.SettingsStrategies.Roam);
            }

            set
            {
                _helper.Write<string>(nameof(Name), value, Template10.Services.SettingsService.SettingsStrategies.Roam);
            }
        }

        public bool IsAdmin
        {
            get
            {
                return _helper.Read<bool>(nameof(IsAdmin), false, Template10.Services.SettingsService.SettingsStrategies.Roam);
            }

            set
            {
                _helper.Write<bool>(nameof(IsAdmin), value, Template10.Services.SettingsService.SettingsStrategies.Roam);
            }
        }

        public bool IsLoggedInToLaundrySite
        {
            get
            {
                return _helper.Read<bool>(nameof(IsLoggedInToLaundrySite), false);
            }

            set
            {
                _helper.Write<bool>(nameof(IsLoggedInToLaundrySite), value);
            }
        }
    }
}
