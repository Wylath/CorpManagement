using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ServicingModel : ObservableObject
    {
        private readonly Servicing _Servicing;

        public ServicingModel(Servicing Servicing)
        {
            _Servicing = Servicing;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Servicing.id;
            }
            set
            {
                if (_Servicing.id != value)
                    _Servicing.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public Vehicle idvehicle
        {
            get
            {
                return _Servicing.idvehicle;
            }
            set
            {
                if (_Servicing.idvehicle != value)
                    _Servicing.idvehicle = value;
                RaisePropertyChanged(() => idvehicle);
            }
        }

        public DateTime dateservicing
        {
            get
            {
                return _Servicing.dateservicing;
            }
            set
            {
                if (_Servicing.dateservicing != value)
                    _Servicing.dateservicing = value;
                RaisePropertyChanged(() => dateservicing);
            }
        }

        public float price
        {
            get
            {
                return _Servicing.price;
            }
            set
            {
                if (_Servicing.price != value)
                    _Servicing.price = value;
                RaisePropertyChanged(() => price);
            }
        }

        public Provider idprovider
        {
            get
            {
                return _Servicing.idprovider;
            }
            set
            {
                if (_Servicing.idprovider != value)
                    _Servicing.idprovider = value;
                RaisePropertyChanged(() => idprovider);
            }
        }

        public string description
        {
            get
            {
                return _Servicing.description;
            }
            set
            {
                if (_Servicing.description != value)
                    _Servicing.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public TypeServicing idtypeservicing
        {
            get
            {
                return _Servicing.idtypeservicing;
            }
            set
            {
                if (_Servicing.idtypeservicing != value)
                    _Servicing.idtypeservicing = value;
                RaisePropertyChanged(() => idtypeservicing);
            }
        }

        public int km
        {
            get
            {
                return _Servicing.km;
            }
            set
            {
                if (_Servicing.km != value)
                    _Servicing.km = value;
                RaisePropertyChanged(() => km);
            }
        }
    }
}
