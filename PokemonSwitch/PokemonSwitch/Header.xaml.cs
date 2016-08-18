using PokemonSwitch.Interface;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PokemonSwitch
{
    public partial class Header : Frame, IMapDelegate
    {
        public Header()
        {
            InitializeComponent();
        }

        SettingPopupPage settingPopup;
        SettingPopupVM settingPopupVM;
        private IMapDelegate _mapDelegate;
        public Header(IMapDelegate mapDelegate)
        {
            InitializeComponent();
            _mapDelegate = mapDelegate;

            var tapImage = new TapGestureRecognizer();
            tapImage.Tapped += TipForUser_Handler;
            imgTip.GestureRecognizers.Add(tapImage);

            tapImage = new TapGestureRecognizer();
            tapImage.Tapped += ResetMap_Handler;
            imgRetry.GestureRecognizers.Add(tapImage);

            tapImage = new TapGestureRecognizer();
            tapImage.Tapped += Setting_Handler;
            imgSetting.GestureRecognizers.Add(tapImage);

            settingPopup = new SettingPopupPage();
            settingPopupVM = new SettingPopupVM();
            settingPopup.BindingContext = settingPopupVM;
        }
        public void TipForUser()
        {
            _mapDelegate.TipForUser();
        }

        private void TipForUser_Handler(object sender, EventArgs e)
        {
            TipForUser();
        }

        private void ResetMap_Handler(object sender, EventArgs e)
        {
            ResetMap();
        }
        public void ResetMap()
        {
            _mapDelegate.ResetMap();
        }

        private async void Setting_Handler(object sender, EventArgs e)
        {
            settingPopupVM.Level = "Hard";
            settingPopupVM.Gate = "2/10";
            settingPopupVM.ImageSource1 = "Charizard.png";
            settingPopupVM.ImageSource2 = "Venusaur.png";

            await PopupNavigation.PushAsync(settingPopup);
        }
        public void SettingSaved()
        {
            throw new NotImplementedException();
        }
    }
}
