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
    public partial class FinishedPopupPage : PopupPage
    {
        public Star StarOne, StarTwo, StarThree, StarFour, StarFive;
        Dictionary<int, Star> dicIndexToStar = new Dictionary<int, Star>();
        public FinishedPopupPage(int indexStar = 5)
        {
            InitializeComponent();
            StarOne = new Star();
            StarTwo = new Star();
            StarThree = new Star();
            StarFour = new Star();
            StarFive = new Star();
            dicIndexToStar.Add(1, StarOne);
            dicIndexToStar.Add(2, StarTwo);
            dicIndexToStar.Add(3, StarThree);
            dicIndexToStar.Add(4, StarFour);
            dicIndexToStar.Add(5, StarFive);
            for (int i = 1; i <= indexStar; i++)
                dicIndexToStar[i].Choose = true;
            for (int i = indexStar + 1; i <= 5; i++)
                dicIndexToStar[i].Choose = false;
            Animation = new UserAnimation();
        }
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }

    public class Star
    {
        private bool _choose = false;
        private bool _notChoose = false;
        public bool Choose
        {
            get { return _choose; }
            set { _choose = value; _notChoose = !_choose; }
        }

        public bool NotChoose
        {
            get { return _notChoose; }
        }
    }
}
