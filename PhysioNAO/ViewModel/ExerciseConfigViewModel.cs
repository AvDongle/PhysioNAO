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
    public class ExerciseConfigViewModel : ViewModelBase
    {
        private MainViewModel _parent;
        private Exercise _selectedExercise;
        public Exercise _new { get; set; }
        public ICommand NewExerciseCommand { get; private set; }
        public ICommand AddExerciseCommand { get; private set; }
        public ICommand DeleteExerciseCommand { get; private set; }

        public bool _isSelected, _isNew;


        public ExerciseConfigViewModel(MainViewModel parent)
        {
            _parent = parent;
            NewExerciseCommand = new RelayCommand(() => ExecuteNewExercise());
            AddExerciseCommand = new RelayCommand(() => ExecuteAddExercise());
            DeleteExerciseCommand = new RelayCommand(() => ExecuteDeleteExercise());

            IsSelected = false;
            IsNew = false;
            _new = new Exercise();

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
                IsNew = false;
                IsSelected = true;
                RaisePropertyChanged("IsNew");
                RaisePropertyChanged("IsSelected");

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

        private void ExecuteNewExercise()
        {
            IsSelected = false;
            IsNew = true;
            RaisePropertyChanged("IsSelected");
        }

        private void ExecuteAddExercise()
        {
            Parent.Exercises.Add(new Exercise(_new.Name));
            _new.Name = "";
            RaisePropertyChanged("Parent.Exercises");

        }

        private void ExecuteDeleteExercise()
        {
            Parent.Exercises.Remove(SelectedExercise);
            RaisePropertyChanged("Parent.Exercises");

        }
    }
}