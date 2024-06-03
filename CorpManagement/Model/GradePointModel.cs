using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class GradePointModel : ObservableObject
    {
        private readonly GradePoint _GradePoint;

        public GradePointModel(GradePoint GradePoint)
        {
            _GradePoint = GradePoint;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _GradePoint.id;
            }
            set
            {
                if (_GradePoint.id != value)
                    _GradePoint.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _GradePoint.name;
            }
            set
            {
                if (_GradePoint.name != value)
                    _GradePoint.name = value;
                RaisePropertyChanged(() => name);
            }
        }

        public int totalpoint
        {
            get
            {
                return _GradePoint.totalpoint;
            }
            set
            {
                if (_GradePoint.totalpoint != value)
                    _GradePoint.totalpoint = value;
                RaisePropertyChanged(() => totalpoint);
            }
        }
    }
}
