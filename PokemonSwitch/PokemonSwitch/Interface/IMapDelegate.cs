using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch.Interface
{
    public interface IMapDelegate
    {
        void TipForUser();
        void ResetMap();
        void SettingSaved();
    }
}
