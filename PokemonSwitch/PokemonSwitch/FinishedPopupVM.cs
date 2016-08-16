using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch
{
    class FinishedPopupVM : ViewModelBase
    {
        private Star _StarOne, _StarTwo, _StarThree ;
        public Star StarOne
        {
            get { if (_StarOne == null) _StarOne = new Star();
                return _StarOne;
            }
            set
            {
                _StarOne = value;
                OnPropertyChanged("StarOne");
            }
        }
        public Star StarTwo
        {
            get
            {
                if (_StarTwo == null) _StarTwo = new Star();
                return _StarTwo;
            }
            set
            {
                _StarTwo = value;
                OnPropertyChanged("StarTwo");
            }
        }

        public Star StarThree
        {
            get
            {
                if (_StarThree == null) _StarThree = new Star();
                return _StarThree;
            }
            set
            {
                _StarThree = value;
                OnPropertyChanged("StarThree");
            }
        }
        Dictionary<int, Star> dicIndexToStar = new Dictionary<int, Star>();
        public FinishedPopupVM()
        {
            StarOne = new Star();
            StarTwo = new Star();
            StarThree = new Star();
            dicIndexToStar.Add(1, StarOne);
            dicIndexToStar.Add(2, StarTwo);
            dicIndexToStar.Add(3, StarThree);
        }

        public void SetStar(int indexStar)
        {
            for (int i = 1; i <= indexStar; i++)
                dicIndexToStar[i].Choose = true;
            for (int i = indexStar + 1; i <= 3; i++)
                dicIndexToStar[i].Choose = false;

            OnPropertyChanged("StarOne");
            OnPropertyChanged("StarTwo");
            OnPropertyChanged("StarThree");
        }
    }

    public class Star
    {
        private bool _choose = false;
        private bool _notChoose = false;
        public bool Choose
        {
            get { return _choose; }
            set
            {
                _choose = value; _notChoose = !_choose;
            }
        }

        public bool NotChoose
        {
            get { return _notChoose; }
            set
            {
                _notChoose = value;
            }
        }


    }
}
