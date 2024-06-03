using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class UserModel : ObservableObject
    {
        private readonly User _User;

        public UserModel(User User)
        {
            _User = User;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _User.id;
            }
            set
            {
                if (_User.id != value)
                    _User.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string lastname
        {
            get
            {
                return _User.lastname;
            }
            set
            {
                if (_User.lastname != value)
                    _User.lastname = value;
                RaisePropertyChanged(() => lastname);
            }
        }

        public string firstname
        {
            get
            {
                return _User.firstname;
            }
            set
            {
                if (_User.firstname != value)
                    _User.firstname = value;
                RaisePropertyChanged(() => firstname);
            }
        }

        public int matricule
        {
            get
            {
                return _User.matricule;
            }
            set
            {
                if (_User.matricule != value)
                    _User.matricule = value;
                RaisePropertyChanged(() => matricule);
            }
        }

        public ProfileLevel idprofilelevel
        {
            get
            {
                return _User.idprofilelevel;
            }
            set
            {
                if (_User.idprofilelevel != value)
                    _User.idprofilelevel = value;
                RaisePropertyChanged(() => idprofilelevel);
            }
        }

        public int pointarticle
        {
            get
            {
                return _User.pointarticle;
            }
            set
            {
                if (_User.pointarticle != value)
                    _User.pointarticle = value;
                RaisePropertyChanged(() => pointarticle);
            }
        }

        public GradePoint gradepoint
        {
            get
            {
                return _User.gradepoint;
            }
            set
            {
                if (_User.gradepoint != value)
                    _User.gradepoint = value;
                RaisePropertyChanged(() => gradepoint);
            }
        }

        public bool status
        {
            get
            {
                return _User.status;
            }
            set
            {
                if (_User.status != value)
                    _User.status = value;
                RaisePropertyChanged(() => status);
            }
        }

        public DateTime lastupdatepoint
        {
            get
            {
                return _User.lastupdatepoint;
            }
            set
            {
                if (_User.lastupdatepoint != value)
                    _User.lastupdatepoint = value;
                RaisePropertyChanged(() => lastupdatepoint);
            }
        }
    }
}
