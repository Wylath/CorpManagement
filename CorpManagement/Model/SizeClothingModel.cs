using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class SizeClothingModel : ObservableObject
    {
        private readonly SizeClothing _SizeClothing;

        public SizeClothingModel(SizeClothing SizeClothing)
        {
            _SizeClothing = SizeClothing;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _SizeClothing.id;
            }
            set
            {
                if (_SizeClothing.id != value)
                    _SizeClothing.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string size
        {
            get
            {
                return _SizeClothing.size;
            }
            set
            {
                if (_SizeClothing.size != value)
                    _SizeClothing.size = value;
                RaisePropertyChanged(() => size);
            }
        }
    }
}
