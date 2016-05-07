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
                return _helper.Read<int>(nameof(RoomNumber), 000);
            }

            set
            {
                _helper.Write<int>(nameof(RoomNumber), value);
            }
        }
    }
}
