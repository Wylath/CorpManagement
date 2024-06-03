using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class User
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public int matricule { get; set; }
        public ProfileLevel idprofilelevel { get; set; }
        public int pointarticle { get; set; }
        public GradePoint gradepoint { get; set; }
        public bool status { get; set; }
        public DateTime lastupdatepoint { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="matricule"></param>
        /// <param name="idprofilelevel"></param>
        /// <param name="pointarticle"></param>
        /// <param name="gradepoint"></param>
        public User(int id, string lastname, string firstname, int matricule, ProfileLevel idprofilelevel, int pointarticle, GradePoint gradepoint, bool status)
        {
            this.id = id;
            this.lastname = lastname;
            this.firstname = firstname;
            this.matricule = matricule;
            this.idprofilelevel = idprofilelevel;
            this.pointarticle = pointarticle;
            this.gradepoint = gradepoint;
            this.status = status;
        }

        /// <summary>
        /// Constructor with last date update point and user id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="matricule"></param>
        /// <param name="idprofilelevel"></param>
        /// <param name="pointarticle"></param>
        /// <param name="gradepoint"></param>
        /// <param name="status"></param>
        /// <param name="lastupdatepoint"></param>
        public User(int id, string lastname, string firstname, int matricule, ProfileLevel idprofilelevel, int pointarticle, GradePoint gradepoint, bool status, DateTime lastupdatepoint) : this(id, lastname, firstname, matricule, idprofilelevel, pointarticle, gradepoint, status)
        {
            this.lastupdatepoint = lastupdatepoint;
        }

        /// <summary>
        /// Constructor with only id parameter
        /// </summary>
        /// <param name="id"></param>
        public User(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor for new user
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="matricule"></param>
        /// <param name="idprofilelevel"></param>
        /// <param name="pointarticle"></param>
        /// <param name="gradepoint"></param>
        public User(string lastname, string firstname, int matricule, ProfileLevel idprofilelevel, int pointarticle, GradePoint gradepoint, bool status)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.matricule = matricule;
            this.idprofilelevel = idprofilelevel;
            this.pointarticle = pointarticle;
            this.gradepoint = gradepoint;
            this.status = status;
        }

        /// <summary>
        /// Constructor with last update point for new user
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="matricule"></param>
        /// <param name="idprofilelevel"></param>
        /// <param name="pointarticle"></param>
        /// <param name="gradepoint"></param>
        /// <param name="status"></param>
        /// <param name="lastupdatepoint"></param>
        public User(string lastname, string firstname, int matricule, ProfileLevel idprofilelevel, int pointarticle, GradePoint gradepoint, bool status, DateTime lastupdatepoint) : this(lastname, firstname, matricule, idprofilelevel, pointarticle, gradepoint, status)
        {
            this.lastupdatepoint = lastupdatepoint;
        }
    }
}
