using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PhysioNAO.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace PhysioNAO.ViewModel
{
    public class SetConfigViewModel : ViewModelBase
    {
        private MainViewModel _parent;
        private ExerciseSet _selectedSet;
        private Exercise _selectedExercise;
        public ExerciseSet _new { get; set; }
        public ICommand NewSetCommand { get; private set; }
        public ICommand AddSetCommand { get; private set; }
        public ICommand AddExerciseCommand { get; private set; }
        public ICommand RemoveAddExerciseCommand { get; private set; }
        public bool _isSelected, _isNew;


        public SetConfigViewModel(MainViewModel parent)
        {
            _parent = parent;
            NewSetCommand = new RelayCommand(() => ExecuteNewExerciseSet());
            AddSetCommand = new RelayCommand(() => ExecuteAddExerciseSet());
            AddExerciseCommand = new RelayCommand(() => ExecuteAddExercise());

            IsSelected = false;

            IsNew = false;
            _new = new ExerciseSet();

        }

        public ExerciseSet SelectedSet
        {
            get
            {
                return _selectedSet;
            }
            set
            {
                _selectedSet = value;
                RaisePropertyChanged("SelectedSet");
                IsNew = false;
                IsSelected = true;
                RaisePropertyChanged("IsNew");
                RaisePropertyChanged("IsSelected");

            }
        }

        public Exercise SelectedExercise
        {
            get
            {
                return _selectedExercise;
            }
            set
            {
                _selectedExercise = value;
                RaisePropertyChanged("SelectedExercise");
            }
        }

        public MainViewModel Parent
        {
            get
            {
                return _parent;
            }
        }

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                if (value == true)
                {
                    IsSelected = false;
                    _isNew = value;
                }
                else
                    _isNew = false;

                RaisePropertyChanged("IsNew");
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value == true)
                {
                    IsNew = false;
                    _isSelected = value;
                }
                else
                    _isSelected = false;
                RaisePropertyChanged("IsSelected");
            }
        }

        private void ExecuteNewExerciseSet()
        {
            IsSelected = false;
            IsNew = true;
            RaisePropertyChanged("IsSelected");
        }

        private void ExecuteAddExerciseSet()
        {
            Parent.ExerciseSets.Add(new ExerciseSet(_new.Name,_new.Exercises));
            _new.Name = "";
            RaisePropertyChanged("Parent.ExerciseSets");

        }

        private void ExecuteAddExercise()
        {
            _new.Exercises.Add(_selectedExercise);
            RaisePropertyChanged("_new.exercises");
        }


    }
}
