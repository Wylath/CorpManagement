using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ArticleTypeModel : ObservableObject
    {
        private readonly ArticleType _ArticleType;

        public ArticleTypeModel(ArticleType ArticleType)
        {
            _ArticleType = ArticleType;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _ArticleType.id;
            }
            set
            {
                if (_ArticleType.id != value)
                    _ArticleType.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _ArticleType.name;
            }
            set
            {
                if (_ArticleType.name != value)
                    _ArticleType.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
