using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class FuelModel : ObservableObject
    {
        private readonly Fuel _Fuel;

        public FuelModel(Fuel Fuel)
        {
            _Fuel = Fuel;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Fuel.id;
            }
            set
            {
                if (_Fuel.id != value)
                    _Fuel.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _Fuel.name;
            }
            set
            {
                if (_Fuel.name != value)
                    _Fuel.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
