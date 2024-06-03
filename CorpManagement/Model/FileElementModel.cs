using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class FileElementModel : ObservableObject
    {
        private readonly FileElement _FileElement;

        public FileElementModel(FileElement FileElement)
        {
            _FileElement = FileElement;
            RaisePropertyChanged();
        }

        public string FilesPath
        {
            get
            {
                return _FileElement.FilesPath;
            }
            set
            {
                if (_FileElement.FilesPath != value)
                    _FileElement.FilesPath = value;
                RaisePropertyChanged(() => FilesPath);
            }
        }

        public string Name
        {
            get
            {
                return _FileElement.Name;
            }
            set
            {
                if (_FileElement.Name != value)
                    _FileElement.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}
