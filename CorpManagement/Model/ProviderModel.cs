using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ProviderModel : ObservableObject
    {
        private readonly Provider _Provider;

        public ProviderModel(Provider Provider)
        {
            _Provider = Provider;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Provider.id;
            }
            set
            {
                if (_Provider.id != value)
                    _Provider.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _Provider.name;
            }
            set
            {
                if (_Provider.name != value)
                    _Provider.name = value;
                RaisePropertyChanged(() => name);
            }
        }

        public int phone
        {
            get
            {
                return _Provider.phone;
            }
            set
            {
                if (_Provider.phone != value)
                    _Provider.phone = value;
                RaisePropertyChanged(() => phone);
            }
        }

        public string mail
        {
            get
            {
                return _Provider.mail;
            }
            set
            {
                if (_Provider.mail != value)
                    _Provider.mail = value;
                RaisePropertyChanged(() => mail);
            }
        }

        public string street
        {
            get
            {
                return _Provider.street;
            }
            set
            {
                if (_Provider.street != value)
                    _Provider.street = value;
                RaisePropertyChanged(() => street);
            }
        }

        public string housenumber
        {
            get
            {
                return _Provider.housenumber;
            }
            set
            {
                if (_Provider.housenumber != value)
                    _Provider.housenumber = value;
                RaisePropertyChanged(() => housenumber);
            }
        }

        public string postalcode
        {
            get
            {
                return _Provider.postalcode;
            }
            set
            {
                if (_Provider.postalcode != value)
                    _Provider.postalcode = value;
                RaisePropertyChanged(() => postalcode);
            }
        }

        public string town
        {
            get
            {
                return _Provider.town;
            }
            set
            {
                if (_Provider.town != value)
                    _Provider.town = value;
                RaisePropertyChanged(() => town);
            }
        }

        public string description
        {
            get
            {
                return _Provider.description;
            }
            set
            {
                if (_Provider.description != value)
                    _Provider.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public ProviderType idtype
        {
            get
            {
                return _Provider.idtype;
            }
            set
            {
                if (_Provider.idtype != value)
                    _Provider.idtype = value;
                RaisePropertyChanged(() => idtype);
            }
        }
    }
}
