using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class VehicleModel : ObservableObject
    {
        private readonly Vehicle _Vehicle;

        public VehicleModel(Vehicle Vehicle)
        {
            _Vehicle = Vehicle;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Vehicle.id;
            }
            set
            {
                if (_Vehicle.id != value)
                    _Vehicle.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _Vehicle.name;
            }
            set
            {
                if (_Vehicle.name != value)
                    _Vehicle.name = value;
                RaisePropertyChanged(() => name);
            }
        }

        public string numberplate
        {
            get
            {
                return _Vehicle.numberplate;
            }
            set
            {
                if (_Vehicle.numberplate != value)
                    _Vehicle.numberplate = value;
                RaisePropertyChanged(() => numberplate);
            }
        }

        public PoliceLocality idpolicelocality
        {
            get
            {
                return _Vehicle.idpolicelocality;
            }
            set
            {
                if (_Vehicle.idpolicelocality != value)
                    _Vehicle.idpolicelocality = value;
                RaisePropertyChanged(() => idpolicelocality);
            }
        }

        public DateTime saledate
        {
            get
            {
                return _Vehicle.saledate;
            }
            set
            {
                if (_Vehicle.saledate != value)
                    _Vehicle.saledate = value;
                RaisePropertyChanged(() => saledate);
            }
        }

        public DateTime lastcontrol
        {
            get
            {
                return _Vehicle.lastcontrol;
            }
            set
            {
                if (_Vehicle.lastcontrol != value)
                    _Vehicle.lastcontrol = value;
                RaisePropertyChanged(() => lastcontrol);
            }
        }

        public int kmlastcontrol
        {
            get
            {
                return _Vehicle.kmlastcontrol;
            }
            set
            {
                if (_Vehicle.kmlastcontrol != value)
                    _Vehicle.kmlastcontrol = value;
                RaisePropertyChanged(() => kmlastcontrol);
            }
        }

        public DateTime nextcontrol
        {
            get
            {
                return _Vehicle.nextcontrol;
            }
            set
            {
                if (_Vehicle.nextcontrol != value)
                    _Vehicle.nextcontrol = value;
                RaisePropertyChanged(() => nextcontrol);
            }
        }

        public Tires idtires
        {
            get
            {
                return _Vehicle.idtires;
            }
            set
            {
                if (_Vehicle.idtires != value)
                    _Vehicle.idtires = value;
                RaisePropertyChanged(() => idtires);
            }
        }

        public Fuel fueltype
        {
            get
            {
                return _Vehicle.fueltype;
            }
            set
            {
                if (_Vehicle.fueltype != value)
                    _Vehicle.fueltype = value;
                RaisePropertyChanged(() => fueltype);
            }
        }

        public string vehicletype
        {
            get
            {
                return _Vehicle.vehicletype;
            }
            set
            {
                if (_Vehicle.vehicletype != value)
                    _Vehicle.vehicletype = value;
                RaisePropertyChanged(() => vehicletype);
            }
        }

        public StatusVehicle status
        {
            get
            {
                return _Vehicle.status;
            }
            set
            {
                if (_Vehicle.status != value)
                    _Vehicle.status = value;
                RaisePropertyChanged(() => status);
            }
        }

        public string description
        {
            get
            {
                return _Vehicle.description;
            }
            set
            {
                if (_Vehicle.description != value)
                    _Vehicle.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public InsuranceVehicle idinsurance
        {
            get
            {
                return _Vehicle.idinsurance;
            }
            set
            {
                if (_Vehicle.idinsurance != value)
                    _Vehicle.idinsurance = value;
                RaisePropertyChanged(() => idinsurance);
            }
        }
    }
}
