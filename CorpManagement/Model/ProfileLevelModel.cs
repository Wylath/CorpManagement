using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class ProfileLevelModel : ObservableObject
    {
        private readonly ProfileLevel _ProfileLevel;

        public ProfileLevelModel(ProfileLevel ProfileLevel)
        {
            _ProfileLevel = ProfileLevel;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _ProfileLevel.id;
            }
            set
            {
                if (_ProfileLevel.id != value)
                    _ProfileLevel.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _ProfileLevel.name;
            }
            set
            {
                if (_ProfileLevel.name != value)
                    _ProfileLevel.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
