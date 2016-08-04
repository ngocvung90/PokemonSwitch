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
            Close = 0,
            NextMap
        }
        FinishedPopupVM _vm = new FinishedPopupVM();
        public PopupChoosen choosen = PopupChoosen.Close;
        public FinishedPopupPage(int indexStar = 1)
        {
            InitializeComponent();
            Animation = new UserAnimation();
            BindingContext = _vm;
        }

        public void SetStar(int indexStar)
        {
            _vm.SetStar(indexStar);
        }
        private void OnClose(object sender, EventArgs e)
        {
            choosen = PopupChoosen.Close;
            PopupNavigation.PopAsync();
        }

        private void OnNextMap(object sender, EventArgs e)
        {
            choosen = PopupChoosen.NextMap;
        }
    }
}
