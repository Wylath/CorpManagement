using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class InvoiceModel : ObservableObject
    {
        private readonly Invoice _Invoice;

        public InvoiceModel(Invoice Invoice)
        {
            _Invoice = Invoice;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Invoice.id;
            }
            set
            {
                if (_Invoice.id != value)
                    _Invoice.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public InsuranceVehicle idinsurance
        {
            get
            {
                return _Invoice.idinsurance;
            }
            set
            {
                if (_Invoice.idinsurance != value)
                    _Invoice.idinsurance = value;
                RaisePropertyChanged(() => idinsurance);
            }
        }

        public OrderArticle idorderarticle
        {
            get
            {
                return _Invoice.idorderarticle;
            }
            set
            {
                if (_Invoice.idorderarticle != value)
                    _Invoice.idorderarticle = value;
                RaisePropertyChanged(() => idorderarticle);
            }
        }

        public Servicing idservicing
        {
            get
            {
                return _Invoice.idservicing;
            }
            set
            {
                if (_Invoice.idservicing != value)
                    _Invoice.idservicing = value;
                RaisePropertyChanged(() => idservicing);
            }
        }

        public float price
        {
            get
            {
                return _Invoice.price;
            }
            set
            {
                if (_Invoice.price != value)
                    _Invoice.price = value;
                RaisePropertyChanged(() => price);
            }
        }

        public DateTime dateinvoice
        {
            get
            {
                return _Invoice.dateinvoice;
            }
            set
            {
                if (_Invoice.dateinvoice != value)
                    _Invoice.dateinvoice = value;
                RaisePropertyChanged(() => dateinvoice);
            }
        }

        public string description
        {
            get
            {
                return _Invoice.description;
            }
            set
            {
                if (_Invoice.description != value)
                    _Invoice.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public DateTime datepaid
        {
            get
            {
                return _Invoice.datepaid;
            }
            set
            {
                if (_Invoice.datepaid != value)
                    _Invoice.datepaid = value;
                RaisePropertyChanged(() => datepaid);
            }
        }

        public InvoiceType idtype
        {
            get
            {
                return _Invoice.idtype;
            }
            set
            {
                if (_Invoice.idtype != value)
                    _Invoice.idtype = value;
                RaisePropertyChanged(() => idtype);
            }
        }
    }
}
