using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Public;

namespace Weekly.Model
{
    public class DailyContent : ObservableObject
    {
        public int Number { get; set; }

        #region [Content]
        private string _content;

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }

        }
        #endregion

        #region [Hour]
        private double _hour;

        public double Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                OnPropertyChanged("Hour");
            }

        }
        #endregion

        #region [Category]
        private string  _category;

        public string  Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }

        }
        #endregion


        #region [State]
        private bool _state;

        public bool State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }

        }
        #endregion


    }


}
