using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ProviderTypeModel : ObservableObject
    {
        private readonly ProviderType _ProviderType;

        public ProviderTypeModel(ProviderType ProviderType)
        {
            _ProviderType = ProviderType;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _ProviderType.id;
            }
            set
            {
                if (_ProviderType.id != value)
                    _ProviderType.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _ProviderType.name;
            }
            set
            {
                if (_ProviderType.name != value)
                    _ProviderType.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
