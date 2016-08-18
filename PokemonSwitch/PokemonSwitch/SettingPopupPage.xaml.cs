using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PokemonSwitch
{
    public partial class SettingPopupPage : PopupPage
    {
        FinishedPopupPage finishPage;

        public SettingPopupPage()
        {
            InitializeComponent();
            Animation = new UserAnimation();

            finishPage = new FinishedPopupPage();

            var tapImage = new TapGestureRecognizer();
            tapImage.Tapped += SettingLevel_Handler;
            imgSettingLevel.GestureRecognizers.Add(tapImage);

        }

        private async void SettingLevel_Handler(object sender, EventArgs e)
        {
            finishPage.SetStar(2);
            await PopupNavigation.PushAsync(finishPage);
        }

    }
}
