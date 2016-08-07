using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace PokemonSwitch
{
    public partial class FinishedPopupPage : PopupPage
    {
        public enum PopupChoosen
        {
            None = 0,
            Retry,
            NextMap
        }
        FinishedPopupVM _vm = new FinishedPopupVM();
        Map _mapDelegate = null;
        public PopupChoosen choosen = PopupChoosen.None;
        public FinishedPopupPage(int indexStar = 1)
        {
            InitializeComponent();
            Animation = new UserAnimation();
            BindingContext = _vm;
        }

        public void SetMapDelegate(Map map)
        {
            _mapDelegate = map;
        }

        public void SetStar(int indexStar)
        {
            _vm.SetStar(indexStar);
        }
        private async void OnClose(object sender, EventArgs e)
        {
            choosen = PopupChoosen.Retry;
            await PopupNavigation.PopAsync();
            _mapDelegate.NextMap(choosen);
        }

        private async void OnNextMap(object sender, EventArgs e)
        {
            choosen = PopupChoosen.NextMap;
            await PopupNavigation.PopAsync();
            _mapDelegate.NextMap(choosen);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            
        }
        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return false;
        }
    }
}
