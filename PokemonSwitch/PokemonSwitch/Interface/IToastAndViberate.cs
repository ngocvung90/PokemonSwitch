using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch.Interface
{
    public interface IToastAndViberate
    {
        void ShowToast(string toastString);
        void Vibration(int miliseconds);
    }
}
