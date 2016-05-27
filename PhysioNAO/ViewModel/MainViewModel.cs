using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhysioNAO.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PhysioNAO.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private ViewModelBase _lastViewModel;

        private bool _nothomepage = false;
        private bool _physiohomepage = false;
        private bool _patienthomepage = false;

        private ObservableCollection<Patient> _patients = new ObservableCollection<Patient>();
        private ObservableCollection<ExerciseSet> _sets = new ObservableCollection<ExerciseSet>();
        private ObservableCollection<Exercise> _exercises = new ObservableCollection<Exercise>();


        public ViewModelBase CurrentViewModel
        {

            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;

                _lastViewModel = _currentViewModel;
                _currentViewModel = value;

                if (_currentViewModel is WelcomeViewModel)
                    NotHomepage = false;
                else
                {
                    NotHomepage = true;
                }
                if ((_currentViewModel is PatientConfigViewModel) || (_currentViewModel is ExerciseConfigViewModel))
                    PhysioHomepage = true;
                else
                    PhysioHomepage = false;

                if (_currentViewModel is PatientViewModel)
                    PatientHomepage = true;
                else
                    PatientHomepage = false;

                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public ICommand HomeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand PatientCommand { get; private set; }
        public ICommand ExerciseCommand { get; private set; }
        public ICommand SetsCommand { get; private set; }

        public MainViewModel()
        {
            CurrentViewModel = new WelcomeViewModel(this);
            HomeCommand = new RelayCommand(() => ExecuteHomeCommand());
            ExitCommand = new RelayCommand<Window>(this.ExecuteExitCommand);
            BackCommand = new RelayCommand(() => ExecuteBackCommand());
            PatientCommand = new RelayCommand(() => ExecutePatientCommand());
            ExerciseCommand = new RelayCommand(() => ExecuteExerciseCommand());
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
            SetsCommand = new RelayCommand(() => ExecuteSetsCommand());

            LoadPatient();
            LoadExercise();
            LoadSet();
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            SavePatient();
            SaveExercise();
            SaveSet();
        }

        public bool NotHomepage
        {
            get
            {
                return _nothomepage;
            }
            set
            {
                _nothomepage = value;
                RaisePropertyChanged("NotHomePage");
            }
        }

        public bool PatientHomepage
        {
            get
            {
                return _patienthomepage;
            }
            set
            {
                _patienthomepage = value;
                RaisePropertyChanged("PatientHomePage");
            }
        }

        public bool PhysioHomepage
        {
            get
            {
                return _physiohomepage;
            }
            set
            {
                _physiohomepage = value;
                RaisePropertyChanged("PhysioHomePage");
            }
        }



        private void ExecuteHomeCommand()
        {
            CurrentViewModel = new WelcomeViewModel(this);
        }

        private void ExecuteSetsCommand()
        {
            CurrentViewModel = new SetConfigViewModel(this);
        }

        private void ExecutePatientCommand()
        {
            CurrentViewModel = new PatientConfigViewModel(this);
        }

        private void ExecuteExerciseCommand()
        {
            CurrentViewModel = new ExerciseConfigViewModel(this);
        }


        private void ExecuteBackCommand()
        {
            CurrentViewModel = _lastViewModel;
        }

        private void ExecuteExitCommand(Window w)
        {
            w.Close();
        }

        public ObservableCollection<Patient> Patients
        {
            get
            {
                return _patients;
            }
        }

        public ObservableCollection<Exercise> Exercises
        {
            get
            {
                return _exercises;
            }
        }

        public ObservableCollection<ExerciseSet> ExerciseSets
        {
            get
            {
                return _sets;
            }
        }


        private void SavePatient()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Patient>));
            using (StreamWriter wr = new StreamWriter("patients.xml"))
                xs.Serialize(wr, _patients);
        }

        private void LoadPatient()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Patient>));

            using (StreamReader rd = new StreamReader("patients.xml"))
                _patients = xs.Deserialize(rd) as ObservableCollection<Patient>;
        }

        private void SaveExercise()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Exercise>));
            using (StreamWriter wr = new StreamWriter("exercises.xml"))
                xs.Serialize(wr, _exercises);
        }

        private void LoadExercise()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Exercise>));

            using (StreamReader rd = new StreamReader("exercises.xml"))
                _exercises = xs.Deserialize(rd) as ObservableCollection<Exercise>;
        }

        private void SaveSet()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ExerciseSet>));
            using (StreamWriter wr = new StreamWriter("sets.xml"))
                xs.Serialize(wr, _sets);
        }

        private void LoadSet()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ExerciseSet>));

            using (StreamReader rd = new StreamReader("sets.xml"))
                _sets = xs.Deserialize(rd) as ObservableCollection<ExerciseSet>;
        }
    }
}