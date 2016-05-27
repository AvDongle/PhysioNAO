using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace PhysioNAO.Model
{
    public class ExerciseSet : GalaSoft.MvvmLight.ObservableObject
    {
        #region Fields
        string _name;
        private ObservableCollection<Exercise> _exercises;
        #endregion //Fields

        public ExerciseSet()
        {
            _exercises = new ObservableCollection<Exercise>();
        }

        public ExerciseSet(string name, ObservableCollection<Exercise> exercises)
        {
            _name = name;
            _exercises = exercises;
        }

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

        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set
            {
                if (value != _exercises)
                {
                    _exercises = value;
                    RaisePropertyChanged("Exercises");
                }
            }
        }
        #endregion //properties
    }
}
