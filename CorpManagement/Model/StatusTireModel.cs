using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class StatusTireModel : ObservableObject
    {
        private readonly StatusTire _StatusTire;

        public StatusTireModel(StatusTire StatusTire)
        {
            _StatusTire = StatusTire;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _StatusTire.id;
            }
            set
            {
                if (_StatusTire.id != value)
                    _StatusTire.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _StatusTire.name;
            }
            set
            {
                if (_StatusTire.name != value)
                    _StatusTire.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
