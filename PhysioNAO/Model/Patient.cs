using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PhysioNAO.Model
{
    class Patient : GalaSoft.MvvmLight.ObservableObject
    {
        #region Fields
        string _name;
        string _exerciseset;
        #endregion //Fields

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        #endregion //properties
    }
}
