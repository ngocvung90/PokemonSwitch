using PokemonSwitch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PokemonSwitch
{
    public partial class MapFooter : Frame, IMapDelegate
    {
        private IMapDelegate _mapDelegate;
        public MapFooter(IMapDelegate mapDelegate)
        {
            InitializeComponent();
            _mapDelegate = mapDelegate;
        }

        public void ResetMap()
        {
            throw new NotImplementedException();
        }

        public void SettingSaved()
        {
            throw new NotImplementedException();
        }

        public void TipForUser()
        {
            _mapDelegate.TipForUser();
        }

        private void ButtonTip_Clicked(object sender, EventArgs e)
        {
            TipForUser();
        }
    }
}
