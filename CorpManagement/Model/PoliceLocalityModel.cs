using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class PoliceLocalityModel : ObservableObject
    {
        private readonly PoliceLocality _PoliceLocality;

        public PoliceLocalityModel(PoliceLocality PoliceLocality)
        {
            _PoliceLocality = PoliceLocality;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _PoliceLocality.id;
            }
            set
            {
                if (_PoliceLocality.id != value)
                    _PoliceLocality.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _PoliceLocality.name;
            }
            set
            {
                if (_PoliceLocality.name != value)
                    _PoliceLocality.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
