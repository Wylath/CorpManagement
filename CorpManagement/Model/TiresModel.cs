using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class TiresModel : ObservableObject
    {
        private readonly Tires _Tires;

        public TiresModel(Tires Tires)
        {
            _Tires = Tires;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Tires.id;
            }
            set
            {
                if (_Tires.id != value)
                    _Tires.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _Tires.name;
            }
            set
            {
                if (_Tires.name != value)
                    _Tires.name = value;
                RaisePropertyChanged(() => name);
            }
        }

        public StatusTire state
        {
            get
            {
                return _Tires.state;
            }
            set
            {
                if (_Tires.state != value)
                    _Tires.state = value;
                RaisePropertyChanged(() => state);
            }
        }

        public string description
        {
            get
            {
                return _Tires.description;
            }
            set
            {
                if (_Tires.description != value)
                    _Tires.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public int setnumber
        {
            get
            {
                return _Tires.setnumber;
            }
            set
            {
                if (_Tires.setnumber != value)
                    _Tires.setnumber = value;
                RaisePropertyChanged(() => setnumber);
            }
        }

        public string Dim1
        {
            get
            {
                return _Tires.Dim1;
            }
            set
            {
                if (_Tires.Dim1 != value)
                    _Tires.Dim1 = value;
                RaisePropertyChanged(() => Dim1);
            }
        }

        public string Dim2
        {
            get
            {
                return _Tires.Dim2;
            }
            set
            {
                if (_Tires.Dim2 != value)
                    _Tires.Dim2 = value;
                RaisePropertyChanged(() => Dim2);
            }
        }

        public string Dim3
        {
            get
            {
                return _Tires.Dim3;
            }
            set
            {
                if (_Tires.Dim3 != value)
                    _Tires.Dim3 = value;
                RaisePropertyChanged(() => Dim3);
            }
        }
    }
}
