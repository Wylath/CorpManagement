using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class OrderArticleModel : ObservableObject
    {
        private readonly OrderArticle _OrderArticle;

        public OrderArticleModel(OrderArticle OrderArticle)
        {
            _OrderArticle = OrderArticle;
            RaisePropertyChanged();
        }

        public int idorder
        {
            get
            {
                return _OrderArticle.idorder;
            }
            set
            {
                if (_OrderArticle.idorder != value)
                    _OrderArticle.idorder = value;
                RaisePropertyChanged(() => idorder);
            }
        }

        public User iduser
        {
            get
            {
                return _OrderArticle.iduser;
            }
            set
            {
                if (_OrderArticle.iduser != value)
                    _OrderArticle.iduser = value;
                RaisePropertyChanged(() => iduser);
            }
        }

        public Article idarticle
        {
            get
            {
                return _OrderArticle.idarticle;
            }
            set
            {
                if (_OrderArticle.idarticle != value)
                    _OrderArticle.idarticle = value;
                RaisePropertyChanged(() => idarticle);
            }
        }

        public int amount
        {
            get
            {
                return _OrderArticle.amount;
            }
            set
            {
                if (_OrderArticle.amount != value)
                    _OrderArticle.amount = value;
                RaisePropertyChanged(() => amount);
            }
        }

        public DateTime orderdate
        {
            get
            {
                return _OrderArticle.orderdate;
            }
            set
            {
                if (_OrderArticle.orderdate != value)
                    _OrderArticle.orderdate = value;
                RaisePropertyChanged(() => orderdate);
            }
        }

        public DateTime datereceived
        {
            get
            {
                return _OrderArticle.datereceived;
            }
            set
            {
                if (_OrderArticle.datereceived != value)
                    _OrderArticle.datereceived = value;
                RaisePropertyChanged(() => datereceived);
            }
        }

        public OrderStatus status
        {
            get
            {
                return _OrderArticle.status;
            }
            set
            {
                if (_OrderArticle.status != value)
                    _OrderArticle.status = value;
                RaisePropertyChanged(() => status);
            }
        }

        public string description
        {
            get
            {
                return _OrderArticle.description;
            }
            set
            {
                if (_OrderArticle.description != value)
                    _OrderArticle.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public SizeClothing sizeclothing
        {
            get
            {
                return _OrderArticle.sizeclothing;
            }
            set
            {
                if (_OrderArticle.sizeclothing != value)
                    _OrderArticle.sizeclothing = value;
                RaisePropertyChanged(() => sizeclothing);
            }
        }
    }
}
