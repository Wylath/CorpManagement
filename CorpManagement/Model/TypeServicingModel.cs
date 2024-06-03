using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class TypeServicingModel : ObservableObject
    {
        private readonly TypeServicing _TypeServicing;

        public TypeServicingModel(TypeServicing TypeServicing)
        {
            _TypeServicing = TypeServicing;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _TypeServicing.id;
            }
            set
            {
                if (_TypeServicing.id != value)
                    _TypeServicing.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _TypeServicing.name;
            }
            set
            {
                if (_TypeServicing.name != value)
                    _TypeServicing.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
