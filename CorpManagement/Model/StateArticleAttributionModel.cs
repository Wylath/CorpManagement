using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class StateArticleAttributionModel : ObservableObject
    {
        private readonly StateArticleAttribution _StateArticleAttribution;

        public StateArticleAttributionModel(StateArticleAttribution StateArticleAttribution)
        {
            _StateArticleAttribution = StateArticleAttribution;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _StateArticleAttribution.id;
            }
            set
            {
                if (_StateArticleAttribution.id != value)
                    _StateArticleAttribution.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _StateArticleAttribution.name;
            }
            set
            {
                if (_StateArticleAttribution.name != value)
                    _StateArticleAttribution.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
