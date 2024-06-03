using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class StatusVehicleModel : ObservableObject
    {
        private readonly StatusVehicle _StatusVehicle;

        public StatusVehicleModel(StatusVehicle StatusVehicle)
        {
            _StatusVehicle = StatusVehicle;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _StatusVehicle.id;
            }
            set
            {
                if (_StatusVehicle.id != value)
                    _StatusVehicle.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _StatusVehicle.name;
            }
            set
            {
                if (_StatusVehicle.name != value)
                    _StatusVehicle.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
