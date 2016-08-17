using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch
{
    class HeaderVM:ViewModelBase
    {
        private string level = "";
        public string Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        private string gate = "";
        public string Gate
        {
            get { return gate; }
            set
            {
                gate = value;
                OnPropertyChanged("Gate");
            }
        }

        private string _BestSolve = "";
        public string BestSolve
        {
            get { return _BestSolve; }
            set
            {
                _BestSolve = value;
                OnPropertyChanged("BestSolve");
            }
        }

    }
}
