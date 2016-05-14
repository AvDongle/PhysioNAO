using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PhysioNAO.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;

namespace PhysioNAO.ViewModel
{
    class WelcomeViewModel:ViewModelBase
    {
        public Patient cliff = new Patient();
        private MainViewModel Parent;
        //string _name;
        private bool _viewMode1 = true;
        private bool _viewMode2;

        public ICommand SecondViewCommand { get; private set; }



        public WelcomeViewModel(MainViewModel parent)
        {
            Parent = parent;
            SecondViewCommand = new RelayCommand(() => ExecuteSecondViewCommand());
        }

        private void ExecuteSecondViewCommand()
        {
            if (_viewMode1)
                Parent.CurrentViewModel = new PhysioViewModel();
            else
                Parent.CurrentViewModel = new PatientViewModel();
        }

        public bool ViewMode1
        {
            get
            {
                return _viewMode1;
            }
            set
            {
                _viewMode1 = value;
            }
        }

        public bool ViewMode2
        {
            get
            {
                return _viewMode2;
            }
            set
            {
                _viewMode2 = value;
            }
        }
    }
}
