using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PhysioNAO.ViewModel
{
    public class PatientViewModel:ViewModelBase
    {
        private ViewModelBase Parent;

        public PatientViewModel(MainViewModel parent)
        {
            Parent = parent;
        }
    }
}
