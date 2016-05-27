using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PhysioNAO.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace PhysioNAO.ViewModel
{
    public class PatientConfigViewModel:ViewModelBase
    {
        private MainViewModel _parent;
        private Patient _selectedPatient;
        public Patient _new { get; set; }
        public ICommand NewPatientCommand { get; private set; }
        public ICommand AddPatientCommand { get; private set; }
        public bool _isSelected, _isNew;


        public PatientConfigViewModel(MainViewModel parent)
        {
            _parent = parent;
            NewPatientCommand = new RelayCommand(() => ExecuteNewPatient());
            AddPatientCommand = new RelayCommand(() => ExecuteAddPatient());
            IsSelected = false;
            IsNew = false;
            _new = new Patient();

        }

        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
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

        private void ExecuteNewPatient()
        {
            IsSelected = false;
            IsNew = true;
            RaisePropertyChanged("IsSelected");
        }

        private void ExecuteAddPatient()
        {
            Parent.Patients.Add(new Patient(_new.Name));
            _new.Name = "";
            RaisePropertyChanged("Parent.Patients");

        }
    }
}
