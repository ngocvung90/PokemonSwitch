using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch
{
    class SettingPopupVM : ViewModelBase
    {
        private string _level = "";
        public string Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }
        private string _gate = "";
        public string Gate
        {
            get { return _gate; }
            set
            {
                _gate = value;
                OnPropertyChanged("Gate");
            }
        }

        private string _imageSource1 = "";
        public string ImageSource1
        {
            get { return _imageSource1; }
            set
            {
                _imageSource1 = value;
                OnPropertyChanged("ImageSource1");
            }
        }

        private string _imageSource2 = "";
        public string ImageSource2
        {
            get { return _imageSource2; }
            set
            {
                _imageSource2 = value;
                OnPropertyChanged("ImageSource2");
            }
        }

    }
}
