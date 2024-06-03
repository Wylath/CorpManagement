using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class InvoiceTypeModel : ObservableObject
    {
        private readonly InvoiceType _InvoiceType;

        public InvoiceTypeModel(InvoiceType InvoiceType)
        {
            _InvoiceType = InvoiceType;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _InvoiceType.id;
            }
            set
            {
                if (_InvoiceType.id != value)
                    _InvoiceType.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _InvoiceType.name;
            }
            set
            {
                if (_InvoiceType.name != value)
                    _InvoiceType.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
