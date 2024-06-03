using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ArticleAttributionModel : ObservableObject
    {
        private readonly ArticleAttribution _ArticleAttribution;

        public ArticleAttributionModel(ArticleAttribution ArticleAttribution)
        {
            _ArticleAttribution = ArticleAttribution;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _ArticleAttribution.id;
            }
            set
            {
                if (_ArticleAttribution.id != value)
                    _ArticleAttribution.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public User iduser
        {
            get
            {
                return _ArticleAttribution.iduser;
            }
            set
            {
                if (_ArticleAttribution.iduser != value)
                    _ArticleAttribution.iduser = value;
                RaisePropertyChanged(() => iduser);
            }
        }

        public Article idarticle
        {
            get
            {
                return _ArticleAttribution.idarticle;
            }
            set
            {
                if (_ArticleAttribution.idarticle != value)
                    _ArticleAttribution.idarticle = value;
                RaisePropertyChanged(() => idarticle);
            }
        }

        public string serialnumber
        {
            get
            {
                return _ArticleAttribution.serialnumber;
            }
            set
            {
                if (_ArticleAttribution.serialnumber != value)
                    _ArticleAttribution.serialnumber = value;
                RaisePropertyChanged(() => serialnumber);
            }
        }

        public string specialnumber
        {
            get
            {
                return _ArticleAttribution.specialnumber;
            }
            set
            {
                if (_ArticleAttribution.specialnumber != value)
                    _ArticleAttribution.specialnumber = value;
                RaisePropertyChanged(() => specialnumber);
            }
        }

        public string description
        {
            get
            {
                return _ArticleAttribution.description;
            }
            set
            {
                if (_ArticleAttribution.description != value)
                    _ArticleAttribution.description = value;
                RaisePropertyChanged(() => description);
            }
        }

        public StateArticleAttribution state
        {
            get
            {
                return _ArticleAttribution.state;
            }
            set
            {
                if (_ArticleAttribution.state != value)
                    _ArticleAttribution.state = value;
                RaisePropertyChanged(() => state);
            }
        }
    }
}
