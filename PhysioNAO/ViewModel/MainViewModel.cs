using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

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
                    NotHomepage = true;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public ICommand HomeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand BackCommand { get; private set; }



        public MainViewModel()
        {
            CurrentViewModel = new WelcomeViewModel(this);
            HomeCommand = new RelayCommand(() => ExecuteHomeCommand());
            ExitCommand = new RelayCommand<Window>(this.ExecuteExitCommand);
            BackCommand = new RelayCommand(() => ExecuteBackCommand());

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
            }
        }

        private void ExecuteHomeCommand()
        {
            CurrentViewModel = new WelcomeViewModel(this);
            MessageBox.Show(CurrentViewModel.ToString());
        }

        private void ExecuteBackCommand()
        {
            CurrentViewModel = _lastViewModel;
        }

        private void ExecuteExitCommand(Window w)
        {
            w.Close();
        }



    }
}