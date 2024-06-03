using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ArticleModel : ObservableObject
    {
        private readonly Article _Article;

        public ArticleModel(Article Article)
        {
            _Article = Article;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _Article.id;
            }
            set
            {
                if (_Article.id != value)
                    _Article.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _Article.name;
            }
            set
            {
                if (_Article.name != value)
                    _Article.name = value;
                RaisePropertyChanged(() => name);
            }
        }

        public string refArticle
        {
            get
            {
                return _Article.refArticle;
            }
            set
            {
                if (_Article.refArticle != value)
                    _Article.refArticle = value;
                RaisePropertyChanged(() => refArticle);
            }
        }

        public Provider idprovider
        {
            get
            {
                return _Article.idprovider;
            }
            set
            {
                if (_Article.idprovider != value)
                    _Article.idprovider = value;
                RaisePropertyChanged(() => idprovider);
            }
        }

        public float price
        {
            get
            {
                return _Article.price;
            }
            set
            {
                if (_Article.price != value)
                    _Article.price = value;
                RaisePropertyChanged(() => price);
            }
        }

        public int amount
        {
            get
            {
                return _Article.amount;
            }
            set
            {
                if (_Article.amount != value)
                    _Article.amount = value;
                RaisePropertyChanged(() => amount);
            }
        }

        public int maxquantity
        {
            get
            {
                return _Article.maxquantity;
            }
            set
            {
                if (_Article.maxquantity != value)
                    _Article.maxquantity = value;
                RaisePropertyChanged(() => maxquantity);
            }
        }

        public ArticleType idtype
        {
            get
            {
                return _Article.idtype;
            }
            set
            {
                if (_Article.idtype != value)
                    _Article.idtype = value;
                RaisePropertyChanged(() => idtype);
            }
        }

        public string description
        {
            get
            {
                return _Article.description;
            }
            set
            {
                if (_Article.description != value)
                    _Article.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public int credit
        {
            get
            {
                return _Article.credit;
            }
            set
            {
                if (_Article.credit != value)
                    _Article.credit = value;
                RaisePropertyChanged(() => credit);
            }
        }
    }
}
