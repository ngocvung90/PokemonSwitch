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
    class FinishedPopupVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Star _StarOne, _StarTwo, _StarThree, _StarFour, _StarFive;
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
        public Star StarFour
        {
            get
            {
                if (_StarFour == null) _StarFour = new Star();
                return _StarFour;
            }
            set
            {
                _StarFour = value;
                OnPropertyChanged("StarFour");
            }
        }

        public Star StarFive
        {
            get
            {
                if (_StarFive == null) _StarFive = new Star();
                return _StarFive;
            }
            set
            {
                _StarFive = value;
                OnPropertyChanged("StarFive");
            }
        }
        Dictionary<int, Star> dicIndexToStar = new Dictionary<int, Star>();
        public FinishedPopupVM()
        {
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
            for (int i = 1; i <= 3; i++)
                dicIndexToStar[i].Choose = true;
            for (int i = 4; i <= 5; i++)
                dicIndexToStar[i].Choose = false;
        }

        public void SetStar(int indexStar)
        {
            for (int i = 1; i <= indexStar; i++)
                dicIndexToStar[i].Choose = true;
            for (int i = indexStar + 1; i <= 5; i++)
                dicIndexToStar[i].Choose = false;

            OnPropertyChanged("StarOne");
            OnPropertyChanged("StarTwo");
            OnPropertyChanged("StarThree");
            OnPropertyChanged("StarFour");
            OnPropertyChanged("StarFive");
        }
        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var property = (MemberExpression)expression.Body;
            this.OnPropertyChanged(property.Member.Name);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
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
