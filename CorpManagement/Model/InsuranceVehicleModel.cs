using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class InsuranceVehicleModel : ObservableObject
    {
        private readonly InsuranceVehicle _InsuranceVehicle;

        public InsuranceVehicleModel(InsuranceVehicle InsuranceVehicle)
        {
            _InsuranceVehicle = InsuranceVehicle;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _InsuranceVehicle.id;
            }
            set
            {
                if (_InsuranceVehicle.id != value)
                    _InsuranceVehicle.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public int insurancenumber
        {
            get
            {
                return _InsuranceVehicle.insurancenumber;
            }
            set
            {
                if (_InsuranceVehicle.insurancenumber != value)
                    _InsuranceVehicle.insurancenumber = value;
                RaisePropertyChanged(() => insurancenumber);
            }
        }

        public Provider idprovider
        {
            get
            {
                return _InsuranceVehicle.idprovider;
            }
            set
            {
                if (_InsuranceVehicle.idprovider != value)
                    _InsuranceVehicle.idprovider = value;
                RaisePropertyChanged(() => idprovider);
            }
        }

        public DateTime effectivedate
        {
            get
            {
                return _InsuranceVehicle.effectivedate;
            }
            set
            {
                if (_InsuranceVehicle.effectivedate != value)
                    _InsuranceVehicle.effectivedate = value;
                RaisePropertyChanged(() => effectivedate);
            }
        }

        public DateTime expiredate
        {
            get
            {
                return _InsuranceVehicle.expiredate;
            }
            set
            {
                if (_InsuranceVehicle.expiredate != value)
                    _InsuranceVehicle.expiredate = value;
                RaisePropertyChanged(() => expiredate);
            }
        }

        public bool active
        {
            get
            {
                return _InsuranceVehicle.active;
            }
            set
            {
                if (_InsuranceVehicle.active != value)
                    _InsuranceVehicle.active = value;
                RaisePropertyChanged(() => active);
            }
        }

        public string coverage
        {
            get
            {
                return _InsuranceVehicle.coverage;
            }
            set
            {
                if (_InsuranceVehicle.coverage != value)
                    _InsuranceVehicle.coverage = value;
                RaisePropertyChanged(() => coverage);
            }
        }

        public float price
        {
            get
            {
                return _InsuranceVehicle.price;
            }
            set
            {
                if (_InsuranceVehicle.price != value)
                    _InsuranceVehicle.price = value;
                RaisePropertyChanged(() => price);
            }
        }

        public string description
        {
            get
            {
                return _InsuranceVehicle.description;
            }
            set
            {
                if (_InsuranceVehicle.description != value)
                    _InsuranceVehicle.description = value;
                RaisePropertyChanged(() => description);
            }
        }
    }
}
